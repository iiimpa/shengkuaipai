using System;
using WebApi.Models;
using YYApi.Communications;
using YYApi.Helpers;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Community;
using YYApi.Controllers;
using Microsoft.Extensions.Hosting;
using System.IO;
using Community;
using Org.BouncyCastle.Ocsp;

namespace Controllers
{
    [CheckAuth("admin")]
    public class AdminController : BaseController
    {
        private DataContext Db { get; }
        private IHostEnvironment Env { get; }
        public AdminController(DataContext db, IHostEnvironment env)
        {
            Db = db;
            Env = env;
        }

        /// <summary>
        /// 检查是否为管理员
        /// </summary>
        /// <returns></returns>
        [HttpPost("/admin/user/info"), ApiDoc("后台", "管理员检查")]
        public AdminUserInfoResponse CheckAdmin()
        {
            var admin = Db.Users.Find(LoginInfo.Id);
            ErrorWhenNull(admin, 401);
            ErrorWhen(LoginInfo.Type != "admin", 403);

            return new AdminUserInfoResponse
            {
                Id = admin.Id,
                Account = admin.Account
            };
        }

        [HttpPost("/admin/dashboard"), ApiDoc("后台", "控制台数据")]
        public AdminDashboardResponse Dashboard()
        {
            var dayStart = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            var dayEnd = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            return new AdminDashboardResponse
            {
                AllUserCount = Db.Users.Count(),
                AllAgentCount = Db.Users.Where(x => x.IsAgent == 1).Count(),
                TodayRegisterCount = Db.Users.Where(x => x.CreatedAt > dayStart && x.CreatedAt < dayEnd).Count(),
                TodayKeywordCount = Db.Orders.Where(x => x.Status == 0).Count(),
                TodayRechargeCount = Db.Trades.Where(x => x.Type == TradeTypeEnum.Recharge && x.Status == 1 && x.CreatedAt > dayStart && x.CreatedAt < dayEnd).Sum(x => x.Amount)
            };
        }

        /// <summary>
        /// 获取参数列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("/admin/config/list"), ApiDoc("后台", "获取参数列表")]
        public BaseResponse<Config[]> GetConfigs()
        {
            return new BaseResponse<Config[]>
            {
                Data = Db.Configs.ToArray()
            };
        }

        /// <summary>
        /// 需改参数值
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/admin/config/edit"), ApiDoc("后台", "修改参数值")]
        public BaseResponse EditConfig([FromBody]AdminEditConfigRequest request)
        {
            var config = Db.Configs.Find(request.Id);
            ErrorWhenNull(config, 400);
            config.Value = request.Value;
            Db.Update(config);
            Db.SaveChanges();
            return new BaseResponse();
        }

        //seo知识列表
        [HttpPost("/admin/knowledge/list"), ApiDoc("后台", "知识列表")]
        public BaseResponse<Knowledge[]> KnowledgeList([FromBody]AdminKnowledgeListRequest request)
        {
            if (string.IsNullOrEmpty(request.Category) || request.Category == "全部")
            {
                return new BaseResponse<Knowledge[]>
                {
                    Data = Db.Knowledges.ToArray()
                };
            }
            else
            {
                return new BaseResponse<Knowledge[]>
                {
                    Data = Db.Knowledges.Where(x => x.Category == request.Category).ToArray()
                };
            }
        }

        //添加seo知识
        [HttpPost("/admin/knowledge/add"), ApiDoc("后台", "添加知识")]
        public BaseResponse AddSeo([FromBody]Knowledge request)
        {
            Db.Add(request);
            Db.SaveChanges();
            return new BaseResponse();
        }

        //删除seo知识
        [HttpPost("/admin/knowledge/delete"), ApiDoc("后台", "删除知识")]
        public BaseResponse DeleteSeo([FromBody]UserOnlyIdRequest request)
        {
            var r = Db.Knowledges.Find(request.Id);
            Db.Remove(r);
            Db.SaveChanges();
            return new BaseResponse();
        }

        //问答列表
        [HttpPost("/admin/qa/list"), ApiDoc("后台", "问答列表")]
        public BaseResponse<Question[]> QAList()
        {
            return new BaseResponse<Question[]>
            {
                Data = Db.Questions.ToArray()
            };
        }

        //添加问答
        [HttpPost("/admin/qa/add"), ApiDoc("后台", "添加问答")]
        public BaseResponse AddQA([FromBody]Question request)
        {
            Db.Add(request);
            Db.SaveChanges();
            return new BaseResponse();
        }

        //删除问答
        [HttpPost("/admin/qa/delete"), ApiDoc("后台", "删除问答")]
        public BaseResponse DeleteQA([FromBody]UserOnlyIdRequest request)
        {
            var r = Db.Questions.Find(request.Id);
            Db.Remove(r);
            Db.SaveChanges();
            return new BaseResponse();
        }

        //案例列表
        [HttpPost("/admin/case/list"), ApiDoc("后台", "案例列表")]
        public BaseResponse<Case[]> CaseList()
        {
            return new BaseResponse<Case[]>
            {
                Data = Db.Cases.ToArray()
            };
        }

        //添加案例
        [HttpPost("/admin/case/add"), ApiDoc("后台", "添加案例")]
        public BaseResponse AddCase([FromBody]Case request)
        {
            Db.Add(request);
            Db.SaveChanges();
            return new BaseResponse();
        }

        //删除案例
        [HttpPost("/admin/case/delete"), ApiDoc("后台", "删除案例")]
        public BaseResponse DeleteCase([FromBody]UserOnlyIdRequest request)
        {
            var r = Db.Cases.Find(request.Id);
            Db.Remove(r);
            Db.SaveChanges();
            return new BaseResponse();
        }

        //点击计划列表
        [HttpPost("/admin/click-plan/list"), ApiDoc("后台", "点击计划列表")]
        public BaseResponse<ClickPlan[]> ClickPlanList()
        {
            return new BaseResponse<ClickPlan[]>
            {
                Data = Db.ClickPlans.ToArray()
            };
        }

        //添加点击计划
        [HttpPost("/admin/click-plan/add"), ApiDoc("后台", "添加点击计划")]
        public BaseResponse AddClickPlan([FromBody]ClickPlan request)
        {
            Db.Add(request);
            Db.SaveChanges();
            return new BaseResponse();
        }

        //删除点击计划
        [HttpPost("/admin/click-plan/delete"), ApiDoc("后台", "删除点击计划")]
        public BaseResponse DeleteClickPlan([FromBody]UserOnlyIdRequest request)
        {
            var r = Db.ClickPlans.Find(request.Id);
            Db.Remove(r);
            Db.SaveChanges();
            return new BaseResponse();
        }

        //运行方案列表
        [HttpPost("/admin/plan/list"), ApiDoc("后台", "点击方案列表")]
        public BaseResponse<Plan[]> PlanList()
        {
            return new BaseResponse<Plan[]>
            {
                Data = Db.Plans.ToArray()
            };
        }

        //添加运行方案
        [HttpPost("/admin/plan/add"), ApiDoc("后台", "添加点击方案")]
        public BaseResponse AddRunPlan([FromBody]Plan request)
        {
            Db.Add(request);
            Db.SaveChanges();
            return new BaseResponse();
        }

        //删除运行方案
        [HttpPost("/admin/plan/delete"), ApiDoc("后台", "删除点击方案")]
        public BaseResponse DeleteRunPlan([FromBody]UserOnlyIdRequest request)
        {
            var r = Db.Plans.Find(request.Id);
            Db.Remove(r);
            Db.SaveChanges();
            return new BaseResponse();
        }

        //用户列表
        [HttpPost("/admin/user/list"), ApiDoc("后台", "用户列表")]
        public BaseResponse<User[]> UserList()
        {
            return new BaseResponse<User[]>
            {
                Data = Db.Users.ToArray()
            };
        }

        //设置用户为代理
        [HttpPost("/admin/user/set-agent"), ApiDoc("后台", "设置用户为代理")]
        public BaseResponse UserSetAgent([FromBody]UserOnlyIdRequest request)
        {
            var u = Db.Users.Find(request.Id);
            u.IsAgent = 1;
            Db.Update(u);
            Db.SaveChanges();
            return new BaseResponse();
        }

        //设置用户为管理员(或者取消)
        [HttpPost("/admin/user/set-admin"), ApiDoc("后台", "设置用户为后台用户")]
        public BaseResponse UserSetAdmin([FromBody]UserOnlyIdRequest request)
        {
            var u = Db.Users.Find(request.Id);
            var level = 0;
            if (u.Level == 0)
            {
                level = 1;
            }
            u.Level = level;
            Db.Update(u);
            Db.SaveChanges();
            return new BaseResponse();
        }

        //提现列表
        [HttpPost("/admin/log/withdraw"), ApiDoc("后台", "提现记录")]
        public BaseResponse<Trade[]> WithdrawLog()
        {
            return new BaseResponse<Trade[]>
            {
                Data = Db.Trades.Where(x => x.Type == TradeTypeEnum.Withdraw).ToArray()
            };
        }


        //充值记录
        [HttpPost("/admin/log/recharge"), ApiDoc("后台", "充值记录")]
        public BaseResponse<Trade[]> RechargeLog()
        {
            return new BaseResponse<Trade[]>
            {
                Data = Db.Trades.Where(x => x.Type == TradeTypeEnum.Recharge).ToArray()
            };
        }


        //充值方案列表
        [HttpPost("/admin/recharge/plan"), ApiDoc("后台", "充值方案管理")]
        public BaseResponse<RechargePlan[]> GetRechargePlan()
        {
            return new BaseResponse<RechargePlan[]>
            {
                Data = Db.RechargePlans.ToArray()
            };
        }

        //添加充值方案
        [HttpPost("/admin/recharge/add"), ApiDoc("后台", "添加充值方案")]
        public BaseResponse AddRechargePlan([FromBody]RechargePlan request)
        {
            Db.Add(request);
            Db.SaveChanges();
            return new BaseResponse();
        }

        //删除方案列表
        [HttpPost("/admin/recharge/delete"), ApiDoc("后台", "删除充值方案")]
        public BaseResponse DeleteRechargePlan([FromBody]UserOnlyIdRequest request)
        {
            var rp = Db.RechargePlans.Find(request.Id);
            Db.Remove(rp);
            Db.SaveChanges();
            return new BaseResponse();
        }


        //上传图片
        [HttpPost("/admin/upload"), ApiDoc("后台", "上传文件")]
        public BaseResponse<string> Upload()
        {
            if (!Request.HasFormContentType || Request.Form.Files == null)
            {
                Error(400);
            }
            var file = Request.Form.Files.First();
            var filePath = Env.ContentRootPath + "/wwwroot/uploads/";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string ext = file.FileName.Split(".").Last();
            var fileName = Guid.NewGuid().ToString() + "." + ext;
            using (var stream = new FileStream(filePath + fileName, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return new BaseResponse<string>
            {
                Data = $"{Request.Scheme}://{Request.Host.Value}/uploads/{fileName}"
            };

        }

        //轮播图列表
        [HttpPost("/admin/carousel/list"), ApiDoc("后台", "轮播图列表")]
        public BaseResponse<Carousel[]> GetCarousel()
        {
            return new BaseResponse<Carousel[]>
            {
                Data = Db.Carousels.ToArray()
            };
        }

        //添加轮播图
        [HttpPost("/admin/carousel/add"), ApiDoc("后台", "添加轮播图")]
        public BaseResponse EditCarousel([FromBody]Carousel request)
        {
            Db.Add(request);
            Db.SaveChanges();
            return new BaseResponse();
        }

        //删除轮播图
        [HttpPost("/admin/carousel/delete"), ApiDoc("后台", "删除轮播图")]
        public BaseResponse DeleteCarousel([FromBody]UserOnlyIdRequest request)
        {
            var c = Db.Carousels.Find(request.Id);
            Db.Remove(c);
            Db.SaveChanges();
            return new BaseResponse();
        }


        //轮播图列表
        [HttpPost("/admin/friend/list"), ApiDoc("后台", "友情链接列表")]
        public BaseResponse<FriendLink[]> GetFriendLink()
        {
            return new BaseResponse<FriendLink[]>
            {
                Data = Db.FriendLinks.ToArray()
            };
        }

        //添加轮播图
        [HttpPost("/admin/friend/add"), ApiDoc("后台", "添加友情链接")]
        public BaseResponse EditFriendLink([FromBody]FriendLink request)
        {
            Db.Add(request);
            Db.SaveChanges();
            return new BaseResponse();
        }

        //删除轮播图
        [HttpPost("/admin/friend/delete"), ApiDoc("后台", "删除友情链接")]
        public BaseResponse DeleteFriendLink([FromBody]UserOnlyIdRequest request)
        {
            var c = Db.FriendLinks.Find(request.Id);
            Db.Remove(c);
            Db.SaveChanges();
            return new BaseResponse();
        }

        //编辑用户信息
        [HttpPost("/admin/user/edit"), ApiDoc("后台", "修改用户信息")]
        public BaseResponse UserEdit([FromBody]AdminUserEditRequest request)
        {
            var u = Db.Users.Find(request.Id);
            ErrorWhenNull(u, 404);
            if (request.Password != null)
            {
                u.Password = request.Password;
            }
            u.Cell = request.Cell;
            u.Coin = request.Coin;
            u.Alipay = request.Alipay;
            Db.Update(u);
            Db.SaveChanges();
            return new BaseResponse();
        }

    }
}
