using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using YYApi.Communications;
using YYApi.Controllers;
using YYApi.Helpers;

namespace WebApi.Controllers
{
    public class AuthController : BaseController
    {
        private DataContext Db { get; }
        private Auth Auth { get; }

        public AuthController(DataContext db, Auth auth)
        {
            Db = db;
            Auth = auth;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/auth/register"), ApiDoc("认证", "注册")]
        public AuthApi.RegisterAndLoginResponse Register([FromBody] AuthApi.RegisterRequest request)
        {
            if (Db.Users.Any(x => x.Account == request.Account))
            {
                Error(400, "用户已经存在");
            }

            var inviter = Db.Users.Find(request.Invite);

            var user = new User
            {
                Account = request.Account,
                Password = request.Password,
                Cell = request.Cell,
                Pid = inviter == null ? 0 : inviter.Id
            };
            Db.Add(user);
            Db.SaveChanges();
            return new AuthApi.RegisterAndLoginResponse
            {
                Token = Auth.Set(user.Id)
            };
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/auth/login"), ApiDoc("认证", "登录")]
        public AuthApi.RegisterAndLoginResponse Login([FromBody] AuthApi.LoginRequest request)
        {
            var user = Db.Users.FirstOrDefault(x => x.Account == request.Account);
            if (user == null)
            {
                Error(400, "用户名密码错误");
            }

            var log = new LoginLog
            {
                UserId = user.Id,
                Ip = HttpContext.Connection.RemoteIpAddress.ToString(),
            };
            if (user.Password == request.Password)
            {
                log.Success = true;
                user.UpdatedAt = DateTime.Now;
                Db.Add(log);
                Db.Update(user);
                Db.SaveChanges();
            }
            else
            {
                log.Success = false;
                Db.Add(log);
                Db.SaveChanges();
                Error(400, "用户名密码错误");
            }

            var role = user.Level == 0 ? "user" : "admin";

            return new AuthApi.RegisterAndLoginResponse
            {
                Token = Auth.Set(user.Id, role)
            };
        }
    }

    public class AuthApi
    {
        public class RegisterRequest
        {
            [Required] public string Account { get; set; }
            [Required] public string Password { get; set; }
            [Required] public string Cell { get; set; }
            public int Invite { get; set; }
        }

        public class LoginRequest
        {
            [Required] public string Account { get; set; }
            [Required] public string Password { get; set; }
        }

        public class RegisterAndLoginResponse : BaseResponse
        {
            public string Token { get; set; }
        }
    }
}