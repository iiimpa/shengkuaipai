using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using YYApi.Controllers;
using YYApi.Helpers;
using YYApi.Communications;
using StackExchange.Redis;
using Microsoft.Extensions.Caching.Distributed;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Text;
using WebApi.Helpers;
using Aop.Api.Domain;
using Newtonsoft.Json.Linq;
using WebApi.RequestParams;

namespace WebApi.Controllers
{
    public class AuthController : BaseController
    {

        private DataContext Db { get; }
        private Auth Auth { get; }
        private IDistributedCache redis { get; set; }
        private IConfiguration _config { get; set; }
        private IHttpContextAccessor _httpcontext { get; set; }
        private Methods _methods { get; set; }
        public AuthController(DataContext db, Auth auth, IDistributedCache rds, IConfiguration config, IHttpContextAccessor httpcontext, Methods methods)
        {
            Db = db;
            Auth = auth;
            redis = rds;
            _config = config;
            _httpcontext = httpcontext;
            _methods = methods;
        }
        /// <summary>
        /// 短信验证码
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/auth/shortmsg")]
        public BaseResponse<object> ShortMsg([FromBody] ShortMsgParams @params)
        {
            string TemplateCode = "";
            if (@params.type == 0)
            {
                TemplateCode = _config["ShortMsg:TemplateCode:register"];
            }
            else if (@params.type == 1)
            {
                @params.tel = Db.Users.Find(LoginInfo.Id).telephone;
                TemplateCode = _config["ShortMsg:TemplateCode:update"];
            }
            Random random = new Random();
            string result = "";
            for (int i = 0; i < 6; i++)
            {
                result += random.Next(1, 10).ToString();
            }
            string json = $"{{'code':'{result}'}}";
            string guid = Guid.NewGuid().ToString();
            IClientProfile profile = DefaultProfile.GetProfile(_config["ShortMsg:regionId"], _config["ShortMsg:accessKeyId"], _config["ShortMsg:accessSecret"]);
            DefaultAcsClient client = new DefaultAcsClient(profile);
            CommonRequest request = new CommonRequest();
            request.Method = MethodType.POST;
            request.Domain = _config["ShortMsg:Domain"];
            request.Version = _config["ShortMsg:Version"];
            request.Action = _config["ShortMsg:Action"];
            // request.Protocol = ProtocolType.HTTP;
            request.AddQueryParameters("PhoneNumbers", @params.tel);
            request.AddQueryParameters("SignName", _config["ShortMsg:SignName"]);

            request.AddQueryParameters("TemplateCode", TemplateCode);
            request.AddQueryParameters("TemplateParam", json);
            try
            {
                CommonResponse response = client.GetCommonResponse(request);
                Dictionary<string, string> resp = _methods.StringToDictStr(System.Text.Encoding.Default.GetString(response.HttpResponse.Content));
                if (resp["Message"] == "OK")
                {
                    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
                    options.AbsoluteExpiration = DateTime.Now.AddMinutes(20);
                    redis.Set(guid, Encoding.Unicode.GetBytes(result), options);
                    return new BaseResponse<object>() { Data = new { verify_key = guid, tel = @params.tel } };
                }
                return new BaseResponse<object>() { Code = 204 };
            }
            catch (ServerException e)
            {
                return new BaseResponse<object>() { Code = 500, Data = e.ToString(), Message = "失败！" };
            }
            catch (ClientException e)
            {
                return new BaseResponse<object>() { Code = 500, Data = e.ToString(), Message = "失败！" };
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/auth/login")]
        public RegisterAndLoginResponse Login([FromBody] LoginRequest request)
        {
            var user = Db.Users.FirstOrDefault(x => x.telephone == request.Tel);
            if (user == null)
            {
                Error(400, "用户名密码错误");
            }
            var log = new LoginLog
            {
                user_id = user.id,
                login_ip = HttpContext.Connection.RemoteIpAddress.ToString(),
                login_time = DateTime.Now
            };
            if (user.password == request.Password)
            {
                log.success = true;
                Db.Add(log);
                Db.Update(user);
                Db.SaveChanges();
            }
            else
            {
                log.success = false;
                Db.Add(log);
                Db.SaveChanges();
                Error(400, "用户名密码错误");
            }
            var role = user.roles.Split(',')[0];
            return new RegisterAndLoginResponse
            {
                Token = Auth.Set(user.id, role)
            };
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/auth/register")]
        public RegisterAndLoginResponse Register([FromBody] RegisterRequest @params)
        {
            string verify_code = "";
            try
            {
                verify_code = Encoding.Unicode.GetString(redis.Get(@params.verifyCode_key));
                Console.WriteLine(verify_code);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            ErrorWhen(verify_code != @params.verifyCode, code: 200, message: "验证码错误！");
            if (Db.Users.Any(x => x.telephone == @params.Tel))
                Error(400, "该手机号已被其他用户绑定！");
            User[] parent = Db.Users.Where(x => @params.Url.Contains(x.url)).ToArray();
            User[] admin = Db.Users.Where(x => x.roles.Contains("admin")).ToArray();
            int parent_id = parent.Length == 0 ? admin[0].id : Convert.ToInt32(parent[0].id);
            int max_id = Db.Users.Select(x => x.id).Max();
            string account = (Convert.ToInt32(Db.Users.Find(max_id).account) + 1).ToString();
            var user = new User
            {
                account = account,
                password = @params.Password,
                telephone = @params.Tel,
                parent_id = parent_id,
                nickname = $"用户{account}",
                QQ = @params.qq
            };
            Db.Add(user);
            if (Db.SaveChanges() > 0)
            {
                var parent_tb = Db.Users.Find(parent_id);
                parent_tb.proxy_person_num += 1;
                Db.Update(parent_tb);
                Db.SaveChanges();
            }
            return new RegisterAndLoginResponse
            {
                Token = Auth.Set(user.id)
            };
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/auth/pwdup")]
        public BaseResponse PwdUp([FromBody] PwdUp @params)
        {
            string verify_code = "";
            try
            {
                verify_code = Encoding.Unicode.GetString(redis.Get(@params.verifyCode_key));
                Console.WriteLine(verify_code);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            ErrorWhen(verify_code != @params.verifyCode, code: 204, message: "验证码错误！");
            User user = Db.Users.Find(LoginInfo.Id);
            if (user.password == @params.Password)
            {
                user.password = @params.newpwd;
                Db.Users.Update(user);
                Db.SaveChanges();
                return new BaseResponse() { Code = 200, Message = "修改成功！" };
            }
            else
                return new BaseResponse() { Code = 204, Message = "原密码错误，请重试！" };
        }

    }

    public class RegisterRequest
    {
        [Required] public string Tel { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string Url { get; set; }
        [Required] public string verifyCode { get; set; }
        [Required] public string verifyCode_key { get; set; }
        [Required] public string qq { get; set; }
    }
    public class ShortMsgParams
    {
        public int type { get; set; }
        public string tel { get; set; }
    }
    public class PwdUp : RegisterRequest
    {
        public string newpwd { get; set; }
    }
    public class LoginRequest
    {
        [Required] public string Tel { get; set; }
        [Required] public string Password { get; set; }
    }
    public class RegisterAndLoginResponse : BaseResponse
    {
        public string Token { get; set; }
    }
}