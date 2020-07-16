using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YYApi.Controllers;
using YYApi.Helpers;
using Microsoft.Extensions.Configuration;
using YYApi.Communications;
using WebApi.RequestParams;
using WebApi.Models;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [CheckAuth(new string[] { "admin", "agent" })]
    public class AdminController : BaseController
    {
        private Methods _methods { get; set; }
        private IConfiguration _configuration;
        private DataContext _db;
        public AdminController(DataContext Db, IConfiguration configuration, Methods methods)
        {
            _db = Db;
            _configuration = configuration;
            _methods = methods;
        }

        /// <summary>
        /// 添加点击方案
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/admin/click/add")]
        public BaseResponse ClickPlanAdd([FromBody]ClickAddParams[] @params)
        {
            foreach (var item in @params)
            {
                string content = "";
                foreach (int i in item.plan_content)
                {
                    content += i.ToString() + ",";
                }
                content = content.Remove(content.LastIndexOf(','), 1);
                ClickPlan clickPlan = new ClickPlan()
                {
                    plan_name = item.plan_name,
                    user_id = LoginInfo.Id,
                    times = content,
                    clickLimtCount = _methods.DictToJson(item.clickLimtCount)
                };
                _db.ClickPlans.Add(clickPlan);
            }
            _db.SaveChanges();
            return new BaseResponse();
        }

        /// <summary>
        /// 添加停留方案
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("admin/residen/add")]
        public BaseResponse ResidenPlanAdd([FromBody] ResidenAddParams[] @params)
        {
            foreach (var item in @params)
            {
                ResidenPlan residenPlan = new ResidenPlan()
                {
                    user_id = LoginInfo.Id,
                    name = item.name,
                    randomWaitCount = _methods.DictToJson(item.randomWaitCount),
                    randomJumpCount = _methods.DictToJson(item.randomJumpCount),
                    price = item.price
                };
                _db.ResidencePlans.Add(residenPlan);
            }
            _db.SaveChanges();
            return new BaseResponse();
        }

        /// <summary>
        /// 添加推荐方案
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/admin/reco/add")]
        public BaseResponse RecoPlanAdd([FromBody] RecoAddParams[] @params)
        {
            foreach (var item in @params)
            {
                string content = item.click_id.ToString() + "," + item.stay_id.ToString();
                RecommendationPlan recoPlan = new RecommendationPlan()
                {
                    user_id = LoginInfo.Id,
                    content = content,
                    name = item.name
                };
                _db.RecommendationPlans.Add(recoPlan);
            }
            _db.SaveChanges();
            return new BaseResponse();
        }

        /// <summary>
        /// 添加充值方案
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [CheckAuth(new string[] { "admin" })]
        [HttpPost("/admin/Recharge/add")]
        public BaseResponse RechargePlanAdd([FromBody] RechargeAddParams[] @params)
        {
            foreach (RechargeAddParams item in @params)
            {
                RechargePlan rechargePlan = new RechargePlan()
                {
                    name = item.name,
                    amount = item.amount,
                    real_coin = item.real_coin,
                    gift_coin = item.gift_coin
                };
                _db.RechargePlans.Add(rechargePlan);
            }
            _db.SaveChanges();
            return new BaseResponse();
        }

        /// <summary>
        /// 删除点击方案
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/admin/click/del")]
        public BaseResponse ClickPlanDel([FromBody]PlanDelParams @params)
        {
            foreach (int item in @params.id)
            {
                var clickPlan = _db.ClickPlans.Find(item);
                _db.ClickPlans.Remove(clickPlan);
            }
            _db.SaveChanges();
            return new BaseResponse();
        }

        /// <summary>
        /// 删除停留方案
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/admin/residen/del")]
        public BaseResponse ResidenPlanDel([FromBody]PlanDelParams @params)
        {
            foreach (int item in @params.id)
            {
                var residen = _db.ResidencePlans.Find(item);
                _db.ResidencePlans.Remove(residen);
            }
            _db.SaveChanges();
            return new BaseResponse();
        }

        /// <summary>
        /// 删除充值方案
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/admin/recharge/del")]
        public BaseResponse RechargePlanDel([FromBody] PlanDelParams @params)
        {
            foreach (int item in @params.id)
            {
                var recharge = _db.RechargePlans.Find(item);
                _db.RechargePlans.Remove(recharge);
            }
            _db.SaveChanges();
            return new BaseResponse();
        }

        /// <summary>
        /// 设置为代理
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/admin/agent/set")]
        public BaseResponse AgentSet([FromBody] AgentSetParams @params)
        {
            var user = _db.Users.Find(@params.user_id);
            ErrorWhen(user.roles.Contains("agent"), 400, "该用户已是代理用户，无需重复添加！");
            user.roles.Insert(0, "agent,");
            user.update_time = DateTime.Now;
            _db.Update(user);
            _db.SaveChanges();
            var up_user = _db.Users.Find(user.parent_id);
            up_user.proxy_person_num += 1;
            _db.Update(up_user);
            _db.SaveChanges();
            return new BaseResponse();
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/admin/user/list")]
        public BaseResponse<object> UserList([FromBody] UserListParams @params)
        {
            @params.pageindex = @params.pageindex == 0 ? 1 : @params.pageindex;
            @params.pagesize = @params.pagesize == 0 ? 20 : @params.pagesize;
            if (@params.id == 0)
            {
                if (@params.type == 1)
                {
                    var query =
                        from a in _db.Users
                        where a.roles.Contains("agent")
                        select new
                        {
                            account = a.account,
                            avatar = a.avatar,
                            coin = a.coin,
                            create_time = a.create_time,
                            discount = a.discount,
                            email = a.email,
                            id = a.id,
                            qq = a.QQ,
                            in_blicklist = a.in_blacklist,
                            level = a.level,
                            login_time = a.login_time,
                            nickname = a.nickname,
                            parent_id = a.parent_id,
                            proxy_person_num = a.proxy_person_num,
                            roles = _methods.StringToArray(a.roles),
                            telephone = a.telephone,
                            update_time = a.update_time,
                            url = a.url,
                            cash_out = a.cash_out,
                            cash_in = a.cash_in,
                            price = a.price,
                            alipay = a.alipay,
                            amount = (from b in _db.Trades
                                      where b.user_id == a.id
                                      where b.status
                                      select b.amount).Sum()
                        };
                    var result = query.Skip((@params.pageindex - 1) * @params.pagesize).Take(@params.pagesize).ToArray();
                    int total = query.ToArray().Length;
                    var responses = new
                    {
                        total = total,
                        data = result
                    };
                    return new BaseResponse<object>()
                    {
                        Data = responses
                    };
                }
                else if (@params.type == 2)
                {
                    var query =
                        from a in _db.Users
                        where a.parent_id == LoginInfo.Id
                        select new
                        {
                            account = a.account,
                            avatar = a.avatar,
                            coin = a.coin,
                            create_time = a.create_time,
                            discount = a.discount,
                            email = a.email,
                            id = a.id,
                            qq = a.QQ,
                            in_blicklist = a.in_blacklist,
                            level = a.level,
                            login_time = a.login_time,
                            nickname = a.nickname,
                            parent_id = a.parent_id,
                            proxy_person_num = a.proxy_person_num,
                            roles = _methods.StringToArray(a.roles),
                            telephone = a.telephone,
                            update_time = a.update_time,
                            url = a.url,
                            cash_out = a.cash_out,
                            cash_in = a.cash_in,
                            price = a.price,
                            alipay = a.alipay,
                            amount = (from b in _db.Trades
                                      where b.user_id == a.id
                                      where b.status
                                      select b.amount).Sum()
                        };
                    var result = query.Skip((@params.pageindex - 1) * @params.pagesize).Take(@params.pagesize).ToArray();
                    int total = query.ToArray().Length;
                    var responses = new
                    {
                        total = total,
                        data = result
                    };
                    return new BaseResponse<object>()
                    {
                        Data = responses
                    };
                }
                else if (@params.type == 0)
                {
                    var query =
                        from a in _db.Users
                        where !a.roles.Contains("admin")
                        where !a.roles.Contains("agent")
                        select new
                        {
                            account = a.account,
                            avatar = a.avatar,
                            coin = a.coin,
                            create_time = a.create_time,
                            discount = a.discount,
                            email = a.email,
                            id = a.id,
                            qq = a.QQ,
                            in_blicklist = a.in_blacklist,
                            level = a.level,
                            login_time = a.login_time,
                            nickname = a.nickname,
                            parent_id = a.parent_id,
                            proxy_person_num = a.proxy_person_num,
                            roles = _methods.StringToArray(a.roles),
                            telephone = a.telephone,
                            update_time = a.update_time,
                            url = a.url,
                            cash_out = a.cash_out,
                            cash_in = a.cash_in,
                            price = a.price,
                            alipay = a.alipay,
                            amount = (from b in _db.Trades
                                      where b.user_id == a.id
                                      where b.status
                                      select b.amount).Sum()
                        };
                    var result = query.Skip((@params.pageindex - 1) * @params.pagesize).Take(@params.pagesize).ToArray();
                    int total = query.ToArray().Length;
                    var responses = new
                    {
                        total = total,
                        data = result
                    };
                    return new BaseResponse<object>()
                    {
                        Data = responses
                    };
                }
                else
                {
                    return new BaseResponse<object>()
                    {
                        Code = 200,
                        Message = "参数错误!"
                    };
                }
            }
            else
            {
                if (@params.type == 1)
                {
                    var query =
                        from a in _db.Users
                        where a.parent_id == @params.id
                        where !a.roles.Contains("agent")
                        select new
                        {
                            account = a.account,
                            avatar = a.avatar,
                            coin = a.coin,
                            create_time = a.create_time,
                            discount = a.discount,
                            email = a.email,
                            id = a.id,
                            qq = a.QQ,
                            in_blicklist = a.in_blacklist,
                            level = a.level,
                            login_time = a.login_time,
                            nickname = a.nickname,
                            parent_id = a.parent_id,
                            proxy_person_num = a.proxy_person_num,
                            roles = _methods.StringToArray(a.roles),
                            telephone = a.telephone,
                            update_time = a.update_time,
                            url = a.url,
                            cash_out = a.cash_out,
                            cash_in = a.cash_in,
                            price = a.price,
                            alipay = a.alipay,
                            amount = (from b in _db.Trades
                                      where b.user_id == a.id
                                      where b.status
                                      select b.amount).Sum()
                        };
                    var result = query.Skip((@params.pageindex - 1) * @params.pagesize).Take(@params.pagesize).ToArray();
                    int total = query.ToArray().Length;
                    var responses = new
                    {
                        total = total,
                        data = result
                    };
                    return new BaseResponse<object>()
                    {
                        Data = responses
                    };
                }
                else if (@params.type == 2)
                {
                    var query =
                        from a in _db.Users
                        where a.parent_id == @params.id
                        select new
                        {
                            account = a.account,
                            avatar = a.avatar,
                            coin = a.coin,
                            create_time = a.create_time,
                            discount = a.discount,
                            email = a.email,
                            id = a.id,
                            qq = a.QQ,
                            in_blicklist = a.in_blacklist,
                            level = a.level,
                            login_time = a.login_time,
                            nickname = a.nickname,
                            parent_id = a.parent_id,
                            proxy_person_num = a.proxy_person_num,
                            roles = _methods.StringToArray(a.roles),
                            telephone = a.telephone,
                            update_time = a.update_time,
                            url = a.url,
                            cash_out = a.cash_out,
                            cash_in = a.cash_in,
                            price = a.price,
                            alipay = a.alipay,
                            amount = (from b in _db.Trades
                                      where b.user_id == a.id
                                      where b.status
                                      select b.amount).Sum()
                        };
                    var result = query.Skip((@params.pageindex - 1) * @params.pagesize).Take(@params.pagesize).ToArray();
                    int total = query.ToArray().Length;
                    var responses = new
                    {
                        total = total,
                        data = result
                    };
                    return new BaseResponse<object>()
                    {
                        Data = responses
                    };
                }
                else if (@params.type == 0)
                {
                    var query =
                        from a in _db.Users
                        where !a.roles.Contains("admin")
                        where !a.roles.Contains("agent")
                        select new
                        {
                            account = a.account,
                            avatar = a.avatar,
                            coin = a.coin,
                            create_time = a.create_time,
                            discount = a.discount,
                            email = a.email,
                            id = a.id,
                            qq = a.QQ,
                            in_blicklist = a.in_blacklist,
                            level = a.level,
                            login_time = a.login_time,
                            nickname = a.nickname,
                            parent_id = a.parent_id,
                            proxy_person_num = a.proxy_person_num,
                            roles = _methods.StringToArray(a.roles),
                            telephone = a.telephone,
                            update_time = a.update_time,
                            url = a.url,
                            cash_out = a.cash_out,
                            cash_in = a.cash_in,
                            price = a.price,
                            alipay = a.alipay,
                            amount = (from b in _db.Trades
                                      where b.user_id == a.id
                                      where b.status
                                      select b.amount).Sum()
                        };
                    var result = query.Skip((@params.pageindex - 1) * @params.pagesize).Take(@params.pagesize).ToArray();
                    int total = query.ToArray().Length; var responses = new
                    {
                        total = total,
                        data = result
                    };
                    return new BaseResponse<object>()
                    {
                        Data = responses
                    };
                }
                else
                {
                    return new BaseResponse<object>()
                    {
                        Code = 200,
                        Message = "参数错误!"
                    };
                }
            }
        }

        /// <summary>
        /// 概览
        /// </summary>
        /// <returns></returns>
        [HttpGet("/admin/dashboard")]
        public BaseResponse<object> DashBoard()
        {
            DateTime min_time = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            DateTime max_time = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            int allUserCount = _db.Users.ToArray().Length;
            int allAgentCount = _db.Users.Where(x => x.roles.Contains("agent")).ToArray().Length;
            int todayRegisterCount = _db.Users.Where(x => x.create_time >= min_time && x.create_time <= max_time).ToArray().Length;
            Trade[] trades = _db.Trades.Where(x => x.create_time >= min_time && x.create_time <= max_time && x.status && x.status).ToArray();
            double todayRechargeCount = 0.00;
            foreach (Trade item in trades)
            {
                todayRechargeCount += item.amount;
            }
            int todayKeywordCount = _db.Tasks.Where(x => x.create_time >= min_time && x.create_time <= max_time).AsEnumerable().GroupBy(x => x.keyword).ToArray().Length;
            int todayAgentCount = _db.Users.Where(x => x.update_time >= min_time && x.update_time <= max_time).ToArray().Length;
            var response = new
            {
                allUserCount = allUserCount,
                allAgentCount = allAgentCount,
                todayRegisterCount = todayRegisterCount,
                todayRechargeCount = todayRechargeCount,
                todayKeywordCount = todayKeywordCount,
                todayAgentCount = todayAgentCount
            };
            return new BaseResponse<object>()
            {
                Data = response
            };
        }
    }
}