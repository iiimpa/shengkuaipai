using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using YYApi.Communications;
using YYApi.Controllers;
using YYApi.Helpers;
using WebApi.RequestParams;
using WebApi.Helpers;
using Task = WebApi.Models.Task;
using static WebApi.RankHelper;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Newtonsoft.Json.Linq;

namespace WebApi.Controllers
{
    [CheckAuth(new string[] { "admin", "agent", "operator" })]
    public class UserController : BaseController
    {
        private DataContext Db { get; }
        private RankHelper RankHelper { get; }
        private IConfiguration _Configuration { get; }
        private Alipay _alipay { get; set; }
        private Methods _methods { get; set; }
        private IDistributedCache _redis { get; set; }

        public UserController(DataContext db, RankHelper rankHelper, IConfiguration Configuration, Alipay alipay, Methods methods, IDistributedCache redis)
        {
            Db = db;
            RankHelper = rankHelper;
            _Configuration = Configuration;
            _alipay = alipay;
            _methods = methods;
            _redis = redis;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("/user/info")]
        public BaseResponse<object> Info()
        {
            DateTime time_begin = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:00"));
            DateTime time_over = Convert.ToDateTime(Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            var query = from user in Db.Users
                        where user.id == LoginInfo.Id
                        select new
                        {
                            user_id = user.id,
                            nickname = user.nickname,
                            account = user.account,
                            email = user.email,
                            coin = user.coin,
                            price = user.price,
                            cash_in = user.cash_in,
                            cash_out = user.cash_out,
                            tel = user.telephone,
                            roles = _methods.StringToArray(user.roles),
                            level = user.level,
                            avatar = user.avatar,
                            parent_id = user.parent_id,
                            login_time = user.login_time,
                            create_time = user.create_time,
                            proxy_person_num = user.proxy_person_num,
                            last_cash_amount = (from trade in Db.Trades
                                                orderby trade.create_time descending
                                                where trade.user_id == LoginInfo.Id
                                                where trade.trade_type == TradeType.Withdraw
                                                select trade.amount).First(),
                            this_month_cash = (from trade in Db.Trades
                                               where trade.user_id == LoginInfo.Id
                                               where trade.create_time >= time_begin
                                               where trade.create_time <= time_over
                                               where trade.trade_type == TradeType.Withdraw
                                               select trade.amount).Sum(),
                            total_cash_count = (from trade in Db.Trades
                                                where trade.user_id == LoginInfo.Id
                                                where trade.trade_type == TradeType.Withdraw
                                                select trade).Count()
                        };
            var response = query.FirstOrDefault();
            return new BaseResponse<object>
            {
                Data = response
            };
        }

        /// <summary>
        /// 获取点击方案
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/user/click/list")]
        public BaseResponse<object> ClickPlanList([FromBody] PlanParams @params)
        {
            if (@params.id != 0)
            {
                ClickPlan clickPlan = Db.ClickPlans.Find(@params.id);
                string[] content = clickPlan.times.Split(',');
                int[] times = new int[content.Length];
                foreach (string item in content)
                {
                    times.Append(Convert.ToInt32(item));
                }
                var rseponse = new
                {
                    id = clickPlan.id,
                    user_id = clickPlan.user_id,
                    plan_name = clickPlan.plan_name,
                    content = times,
                    clickLimtCount = _methods.DicToObj<PlanResponse>(_methods.StringToDict(clickPlan.clickLimtCount)),
                    create_time = clickPlan.create_time
                };
                return new BaseResponse<object>
                {
                    Data = rseponse
                };
            }
            @params.pagesize = @params.pagesize == 0 ? 20 : @params.pagesize;
            @params.pageindex = @params.pageindex == 0 ? 1 : @params.pageindex;
            ClickPlan[] clickPlans = Db.ClickPlans.Skip((@params.pageindex - 1) * @params.pagesize).Take(@params.pagesize).ToArray();
            int total = Db.ClickPlans.ToArray().Length;
            object[] responses = new object[clickPlans.Length];
            int gere = 0;
            foreach (ClickPlan item in clickPlans)
            {
                string[] content = item.times.Split(',');
                int[] times = new int[content.Length];
                int i = 0;
                foreach (string time in content)
                {
                    times[i] = Convert.ToInt32(time);
                    i++;
                }
                var response = new
                {
                    id = item.id,
                    user_id = item.user_id,
                    plan_name = item.plan_name,
                    content = times,
                    clickLimtCount = _methods.DicToObj<PlanResponse>(_methods.StringToDict(item.clickLimtCount)),
                    create_time = item.create_time
                };
                responses[gere] = response;
                gere++;
            }
            var result = new
            {
                total = total,
                data = responses
            };
            return new BaseResponse<object>
            {
                Data = result
            };
        }

        /// <summary>
        /// 获取停留方案
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/user/residen/list")]
        public BaseResponse<object> ResidenPlanList([FromBody] PlanParams @params)
        {

            if (@params.id != 0)
            {
                ResidenPlan residenPlan = Db.ResidencePlans.Find(@params.id);
                var reponse = new
                {
                    id = residenPlan.id,
                    user_id = residenPlan.user_id,
                    name = residenPlan.name,
                    randomWaitCount = _methods.DicToObj<PlanResponse>(_methods.StringToDict(residenPlan.randomWaitCount)),
                    randomJumpCount = _methods.DicToObj<PlanResponse>(_methods.StringToDict(residenPlan.randomJumpCount)),
                    price = residenPlan.price,
                    create_time = residenPlan.create_time
                };
                return new BaseResponse<object>
                {
                    Data = reponse
                };
            }
            @params.pagesize = @params.pagesize == 0 ? 20 : @params.pagesize;
            @params.pageindex = @params.pageindex == 0 ? 1 : @params.pageindex;
            ResidenPlan[] residenPlans = Db.ResidencePlans.Skip((@params.pageindex - 1) * @params.pagesize).Take(@params.pagesize).ToArray();
            int total = Db.ResidencePlans.ToArray().Length;
            object[] responses = new object[residenPlans.Length];
            int gere = 0;
            foreach (ResidenPlan item in residenPlans)
            {
                var response = new
                {
                    id = item.id,
                    user_id = item.user_id,
                    plan_name = item.name,
                    randomWaitCount = _methods.DicToObj<PlanResponse>(_methods.StringToDict(item.randomWaitCount)),
                    randomJumpCount = _methods.DicToObj<PlanResponse>(_methods.StringToDict(item.randomJumpCount)),
                    price = item.price,
                    create_time = item.create_time
                };
                responses[gere] = response;
                gere++;
            }
            var result = new
            {
                total = total,
                data = responses
            };
            return new BaseResponse<object>
            {
                Data = result
            };
        }

        /// <summary>
        /// 获取推荐方案
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/user/reco/list")]
        public BaseResponse<object> RecoPlanList([FromBody] PlanParams @params)
        {
            if (@params.id != 0)
            {
                var recommendation = Db.RecommendationPlans.Find(@params.id);
                int[] result = Array.ConvertAll<string, int>(recommendation.content.Split(','), delegate (string s) { return Convert.ToInt32(s); });
                var click_plan = Db.ClickPlans.Find(result[0]);
                var stay_plan = Db.ResidencePlans.Find(result[1]);
                int[] click_list = Array.ConvertAll<string, int>(click_plan.times.Split(','), delegate (string s) { return Convert.ToInt32(s); });
                var response = new
                {
                    id = recommendation.id,
                    user_id = recommendation.user_id,
                    create_time = recommendation.create_time,
                    name = recommendation.name,
                    click_plan = new
                    {
                        name = click_plan.plan_name,
                        user_id = click_plan.user_id,
                        click_list = click_list,
                        clickLimtCount = _methods.DicToObj<PlanResponse>(_methods.StringToDict(click_plan.clickLimtCount)),
                        create_time = click_plan.create_time
                    },
                    stay_plan = new
                    {
                        name = stay_plan.name,
                        user_id = stay_plan.user_id,
                        randomWaitCount = _methods.DicToObj<PlanResponse>(_methods.StringToDict(stay_plan.randomWaitCount)),
                        randomJumpCount = _methods.DicToObj<PlanResponse>(_methods.StringToDict(stay_plan.randomJumpCount)),
                        create_time = stay_plan.create_time
                    }
                };
                return new BaseResponse<object>()
                {
                    Data = response
                };
            }
            @params.pagesize = @params.pagesize == 0 ? 20 : @params.pagesize;
            @params.pageindex = @params.pageindex == 0 ? 1 : @params.pageindex;
            RecommendationPlan[] recos = Db.RecommendationPlans.Skip((@params.pageindex - 1) * @params.pagesize).Take(@params.pagesize).ToArray();
            int total = Db.RecommendationPlans.ToArray().Length;
            object[] responses = Array.ConvertAll<RecommendationPlan, object>(recos, delegate (RecommendationPlan reco)
            {
                int[] result = Array.ConvertAll<string, int>(reco.content.Split(','), delegate (string s) { return Convert.ToInt32(s); });
                ClickPlan click_plan = Db.ClickPlans.Find(result[0]);
                ResidenPlan stay_plan = Db.ResidencePlans.Find(result[1]);
                var resp = new
                {
                    id = reco.id,
                    user_id = reco.user_id,
                    create_time = reco.create_time,
                    name = reco.name,
                    click_plan = new
                    {
                        name = click_plan.plan_name,
                        user_id = click_plan.user_id,
                        click_list = Array.ConvertAll<string, int>(click_plan.times.Split(','), delegate (string s) { return Convert.ToInt32(s); }),
                        clickLimtCount = _methods.DicToObj<PlanResponse>(_methods.StringToDict(click_plan.clickLimtCount)),
                        create_time = click_plan.create_time
                    },
                    stay_plan = new
                    {
                        name = stay_plan.name,
                        user_id = stay_plan.user_id,
                        randomWaitCount = _methods.DicToObj<PlanResponse>(_methods.StringToDict(stay_plan.randomWaitCount)),
                        randomJumpCount = _methods.DicToObj<PlanResponse>(_methods.StringToDict(stay_plan.randomJumpCount)),
                        create_time = stay_plan.create_time
                    }
                };
                return resp;
            });
            var results = new
            {
                total = total,
                data = responses
            };
            return new BaseResponse<object>()
            {
                Data = results
            };
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("user/order/cancel")]
        public BaseResponse CancelOrder([FromBody] CancelOrder @params)
        {
            foreach (int id in @params.id)
            {
                var order = Db.Orders.Find(id);
                if (order == null)
                    continue;
                if (order.user_id != LoginInfo.Id)
                    continue;
                var user = Db.Users.Find(LoginInfo.Id);
                order.status = OrderStatus.Refund;
                Db.Update(order);
                Db.Update(user);
                var task = Db.Tasks.Where(x => x.order_id == order.id && x.status == Helpers.TaskStatus.Wait).ToArray();
                Db.RemoveRange(task);
                Db.SaveChanges();
            }
            return new BaseResponse();
        }

        /// <summary>
        /// 用户充值
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/user/recharge/query")]
        public BaseResponse<string> RechrageQuery([FromBody] RechargeParams @params)
        {
            var plan = Db.RechargePlans.Find(@params.Pid);
            ErrorWhenNull(plan, 400);
            var guid = Guid.NewGuid().ToString();
            Trade trade = new Trade()
            {
                trade_no = guid,
                trade_type = TradeType.Recharge,
                user_id = LoginInfo.Id,
                plan_id = @params.Pid,
                amount = plan.amount,
                coin = plan.real_coin + plan.gift_coin,
                description = "充值",
                create_time = DateTime.Now,
                status = false
            };
            Db.Trades.Add(trade);
            Db.SaveChanges();
            var resp = _alipay.Pay(guid, plan.amount, @params.Backurl);
            return new BaseResponse<string> { Data = resp };
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/user/order/add")]
        public BaseResponse OrderAdd([FromBody] OrderAddParams[] @params)
        {
            string[] duplicate = null;
            foreach (OrderAddParams item in @params)
            {
                string[] domain = _methods.Division(item.domain);
                string[] xiongzhang = _methods.Division(item.xiongzhang);
                string[] keyword = _methods.Division(item.keyword);
                for (int i = 0; i < keyword.Length; i++)
                {
                    keyword[i] = keyword[i].Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                }
                int index = 0;
                duplicate = new string[keyword.Length];
                if (domain.Length != 0)
                    domain = domain.Distinct().ToArray();
                if (xiongzhang.Length != 0)
                    xiongzhang = xiongzhang.Distinct().ToArray();
                if (keyword.Length != 0)
                    keyword = keyword.Distinct().ToArray();
                if (domain.Length == 1 && keyword.Length >= 1)
                {
                    if (xiongzhang.Length == 0)
                    {
                        for (int i = 0; i < keyword.Length; i++)
                        {
                            Order order = new Order()
                            {
                                serial_no = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                                user_id = LoginInfo.Id,
                                platform = item.platform,
                                keyword = keyword[i],
                                domain = domain[0],
                                mode = item.type,
                                optimization = item.optimization,
                                xiongzhang = "",
                                days = item.days,
                                fixed_click = item.fixed_click,
                                status = OrderStatus.Paied,
                                times = _methods.DictToJson(item.clickPlan),
                                clickLimtCount = _methods.DictToJson(item.clickLimtCount),
                                randomJumpCount = _methods.DictToJson(item.randomJumpCount),
                                randomWaitCount = _methods.DictToJson(item.randomWaitCount),
                                increase = _methods.DictToJson(item.increase),
                                percentage = _methods.DictToJson(item.percentage),
                                group_id = item.group_id
                            };
                            if (Db.Orders.Any(x => x.keyword == order.keyword && x.platform == order.platform && x.domain == order.domain && x.status == OrderStatus.Paied && x.mode == order.mode && x.group_id == order.group_id))
                            {
                                duplicate[index] = order.keyword;
                                index++;
                                continue;
                            }
                            Db.Orders.Add(order);
                            Db.SaveChanges();
                        }
                    }
                    else if (xiongzhang.Length == 1)
                    {
                        for (int i = 0; i < keyword.Length; i++)
                        {
                            Order order = new Order()
                            {
                                serial_no = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                                user_id = LoginInfo.Id,
                                platform = item.platform,
                                keyword = keyword[i],
                                domain = domain[0],
                                mode = item.type,
                                optimization = item.optimization,
                                xiongzhang = xiongzhang[0],
                                days = item.days,
                                fixed_click = item.fixed_click,
                                status = OrderStatus.Paied,
                                times = _methods.DictToJson(item.clickPlan),
                                clickLimtCount = _methods.DictToJson(item.clickLimtCount),
                                randomJumpCount = _methods.DictToJson(item.randomJumpCount),
                                randomWaitCount = _methods.DictToJson(item.randomWaitCount),
                                increase = _methods.DictToJson(item.increase),
                                percentage = _methods.DictToJson(item.percentage),
                                group_id = item.group_id
                            };
                            if (Db.Orders.Any(x => x.keyword == order.keyword && x.platform == order.platform && x.domain == order.domain && x.status == OrderStatus.Paied && x.mode == order.mode && x.group_id == order.group_id))
                            {
                                duplicate[index] = order.keyword;
                                index++;
                                continue;
                            }
                            Db.Orders.Add(order);
                            Db.SaveChanges();
                        }
                    }
                    else if (xiongzhang.Length == keyword.Length)
                    {
                        for (int i = 0; i < keyword.Length; i++)
                        {
                            Order order = new Order()
                            {
                                serial_no = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                                user_id = LoginInfo.Id,
                                platform = item.platform,
                                keyword = keyword[i],
                                domain = domain[0],
                                mode = item.type,
                                optimization = item.optimization,
                                xiongzhang = xiongzhang[i],
                                days = item.days,
                                fixed_click = item.fixed_click,
                                status = OrderStatus.Paied,
                                times = _methods.DictToJson(item.clickPlan),
                                clickLimtCount = _methods.DictToJson(item.clickLimtCount),
                                randomJumpCount = _methods.DictToJson(item.randomJumpCount),
                                randomWaitCount = _methods.DictToJson(item.randomWaitCount),
                                increase = _methods.DictToJson(item.increase),
                                percentage = _methods.DictToJson(item.percentage),
                                group_id = item.group_id
                            };
                            if (Db.Orders.Any(x => x.keyword == order.keyword && x.platform == order.platform && x.domain == order.domain && x.status == OrderStatus.Paied && x.mode == order.mode && x.group_id == order.group_id))
                            {
                                duplicate[index] = order.keyword;
                                index++;
                                continue;
                            }
                            Db.Orders.Add(order);
                            Db.SaveChanges();
                        }
                    }
                    else
                    {
                        return new BaseResponse() { Message = "参数错误" };
                    }
                }
                else
                {
                    if (xiongzhang.Length == 0)
                    {
                        for (int i = 0; i < keyword.Length; i++)
                        {
                            Order order = new Order()
                            {
                                serial_no = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                                user_id = LoginInfo.Id,
                                platform = item.platform,
                                keyword = keyword[i],
                                domain = domain[i],
                                mode = item.type,
                                optimization = item.optimization,
                                xiongzhang = "",
                                days = item.days,
                                fixed_click = item.fixed_click,
                                status = OrderStatus.Paied,
                                times = _methods.DictToJson(item.clickPlan),
                                clickLimtCount = _methods.DictToJson(item.clickLimtCount),
                                randomJumpCount = _methods.DictToJson(item.randomJumpCount),
                                randomWaitCount = _methods.DictToJson(item.randomWaitCount),
                                increase = _methods.DictToJson(item.increase),
                                percentage = _methods.DictToJson(item.percentage),
                                group_id = item.group_id
                            };
                            if (Db.Orders.Any(x => x.keyword == order.keyword && x.platform == order.platform && x.domain == order.domain && x.status == OrderStatus.Paied && x.mode == order.mode && x.group_id == order.group_id))
                            {
                                duplicate[index] = order.keyword;
                                index++;
                                continue;
                            }
                            Db.Orders.Add(order);
                            Db.SaveChanges();
                        }
                    }
                    else if (xiongzhang.Length == 1)
                    {
                        for (int i = 0; i < keyword.Length; i++)
                        {
                            Order order = new Order()
                            {
                                serial_no = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                                user_id = LoginInfo.Id,
                                platform = item.platform,
                                keyword = keyword[i],
                                domain = domain[i],
                                mode = item.type,
                                optimization = item.optimization,
                                xiongzhang = xiongzhang[0],
                                days = item.days,
                                fixed_click = item.fixed_click,
                                status = OrderStatus.Paied,
                                times = _methods.DictToJson(item.clickPlan),
                                clickLimtCount = _methods.DictToJson(item.clickLimtCount),
                                randomJumpCount = _methods.DictToJson(item.randomJumpCount),
                                randomWaitCount = _methods.DictToJson(item.randomWaitCount),
                                increase = _methods.DictToJson(item.increase),
                                percentage = _methods.DictToJson(item.percentage),
                                group_id = item.group_id
                            };
                            if (Db.Orders.Any(x => x.keyword == order.keyword && x.platform == order.platform && x.domain == order.domain && x.status == OrderStatus.Paied && x.mode == order.mode && x.group_id == order.group_id))
                            {
                                duplicate[index] = order.keyword;
                                index++;
                                continue;
                            }
                            Db.Orders.Add(order);
                            Db.SaveChanges();
                        }
                    }
                    else if (xiongzhang.Length == keyword.Length)
                    {
                        for (int i = 0; i < keyword.Length; i++)
                        {
                            Order order = new Order()
                            {
                                serial_no = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                                user_id = LoginInfo.Id,
                                platform = item.platform,
                                keyword = keyword[i],
                                domain = domain[i],
                                mode = item.type,
                                optimization = item.optimization,
                                xiongzhang = xiongzhang[i],
                                days = item.days,
                                fixed_click = item.fixed_click,
                                status = OrderStatus.Paied,
                                times = _methods.DictToJson(item.clickPlan),
                                clickLimtCount = _methods.DictToJson(item.clickLimtCount),
                                randomJumpCount = _methods.DictToJson(item.randomJumpCount),
                                randomWaitCount = _methods.DictToJson(item.randomWaitCount),
                                increase = _methods.DictToJson(item.increase),
                                percentage = _methods.DictToJson(item.percentage),
                                group_id = item.group_id
                            };
                            if (Db.Orders.Any(x => x.keyword == order.keyword && x.platform == order.platform && x.domain == order.domain && x.status == OrderStatus.Paied && x.mode == order.mode && x.group_id == order.group_id))
                            {
                                duplicate[index] = order.keyword;
                                index++;
                                continue;
                            }
                            Db.Orders.Add(order);
                            Db.SaveChanges();
                        }
                    }
                    else
                    {
                        return new BaseResponse() { Message = "参数错误" };
                    }
                }
            }
            return new BaseResponse<object>() { Data = duplicate };

        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/user/order/list")]
        public BaseResponse<object> OrderList([FromBody] OrderListParams @params)
        {
            @params.keyword = @params.keyword == null ? "" : @params.keyword;
            @params.time_begin = @params.time_begin == "" ? null : @params.time_begin;
            @params.time_over = @params.time_over == "" ? null : @params.time_over;
            @params.pageindex = @params.pageindex == 0 ? 1 : @params.pageindex;
            @params.pagesize = @params.pagesize == 0 ? 20 : @params.pagesize;
            DateTime time_begin = @params.time_begin == null ? DateTime.MinValue : Convert.ToDateTime(@params.time_begin);
            DateTime time_over = @params.time_over == null ? DateTime.MaxValue : Convert.ToDateTime(@params.time_over);
            if (@params.group_id != 0)
            {
                if (@params.status == 0)
                {
                    Order[] orders = Db.Orders.Where(x => x.user_id == LoginInfo.Id && x.keyword.Contains(@params.keyword) && x.group_id == @params.group_id && x.create_time >= time_begin && x.create_time <= time_over).Skip((@params.pageindex - 1) * @params.pagesize).Take(@params.pagesize).ToArray();
                    int total = Db.Orders.Where(x => x.user_id == LoginInfo.Id && x.keyword.Contains(@params.keyword) && x.group_id == @params.group_id && x.create_time >= time_begin && x.create_time <= time_over).ToArray().Length;
                    var responses = new
                    {
                        total = total,
                        data = orders
                    };
                    return new BaseResponse<object>()
                    {
                        Data = responses
                    };
                }
                else if (@params.status == 3)
                {
                    Order[] orders = Db.Orders.Where(x => x.user_id == LoginInfo.Id && x.keyword.Contains(@params.keyword) && x.group_id == @params.group_id && x.create_time.AddDays(x.days + 7) >= DateTime.Today).Skip((@params.pageindex - 1) * @params.pagesize).Take(@params.pagesize).ToArray();
                    int total = Db.Orders.Where(x => x.user_id == LoginInfo.Id && x.keyword.Contains(@params.keyword) && x.group_id == @params.group_id && x.create_time.AddDays(x.days + 7) >= DateTime.Today).ToArray().Length;
                    var responses = new
                    {
                        total = total,
                        data = orders
                    };
                    return new BaseResponse<object>()
                    {
                        Data = responses
                    };
                }
                else
                {
                    OrderStatus status;
                    if (@params.status == 1)
                        status = OrderStatus.Paied;
                    else if (@params.status == 2)
                        status = OrderStatus.Finish;
                    else
                        status = OrderStatus.Refund;
                    Order[] orders = Db.Orders.Where(x => x.user_id == LoginInfo.Id && x.group_id == @params.group_id && x.keyword.Contains(@params.keyword) && x.create_time >= time_begin && x.create_time <= time_over && x.status == status).Skip((@params.pageindex - 1) * @params.pagesize).Take(@params.pagesize).ToArray();
                    int total = Db.Orders.Where(x => x.user_id == LoginInfo.Id && x.group_id == @params.group_id && x.keyword.Contains(@params.keyword) && x.create_time >= time_begin && x.create_time <= time_over && x.status == status).ToArray().Length;
                    var responses = new
                    {
                        total = total,
                        data = orders
                    };
                    return new BaseResponse<object>()
                    {
                        Data = responses
                    };
                }
            }
            else
            {
                if (@params.status == 0)
                {
                    Order[] orders = Db.Orders.Where(x => x.user_id == LoginInfo.Id && x.create_time >= time_begin && x.keyword.Contains(@params.keyword) && x.create_time <= time_over).Skip((@params.pageindex - 1) * @params.pagesize).Take(@params.pagesize).ToArray();
                    int total = Db.Orders.Where(x => x.user_id == LoginInfo.Id && x.create_time >= time_begin && x.keyword.Contains(@params.keyword) && x.create_time <= time_over).ToArray().Length;
                    var responses = new
                    {
                        total = total,
                        data = orders
                    };
                    return new BaseResponse<object>()
                    {
                        Data = responses
                    };
                }
                else if (@params.status == 3)
                {
                    Order[] orders = Db.Orders.Where(x => x.user_id == LoginInfo.Id && x.keyword.Contains(@params.keyword) && x.create_time.AddDays(x.days + 7) >= DateTime.Today).Skip((@params.pageindex - 1) * @params.pagesize).Take(@params.pagesize).ToArray();
                    int total = Db.Orders.Where(x => x.user_id == LoginInfo.Id && x.keyword.Contains(@params.keyword) && x.create_time.AddDays(x.days + 7) >= DateTime.Today).ToArray().Length;
                    var responses = new
                    {
                        total = total,
                        data = orders
                    };
                    return new BaseResponse<object>()
                    {
                        Data = responses
                    };
                }
                else
                {
                    OrderStatus status;
                    if (@params.status == 1)
                        status = OrderStatus.Paied;
                    else if (@params.status == 2)
                        status = OrderStatus.Finish;
                    else
                        status = OrderStatus.Refund;
                    Order[] orders = Db.Orders.Where(x => x.user_id == LoginInfo.Id && x.create_time >= time_begin && x.keyword.Contains(@params.keyword) && x.create_time <= time_over && x.status == status).Skip((@params.pageindex - 1) * @params.pagesize).Take(@params.pagesize).ToArray();
                    int total = Db.Orders.Where(x => x.user_id == LoginInfo.Id && x.create_time >= time_begin && x.keyword.Contains(@params.keyword) && x.create_time <= time_over && x.status == status).ToArray().Length;
                    var responses = new
                    {
                        total = total,
                        data = orders
                    };
                    return new BaseResponse<object>()
                    {
                        Data = responses
                    };
                }
            }
        }

        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/user/task/list")]
        public BaseResponse<object> TaskList([FromBody] TaskListParams @params)
        {
            @params.time_begin = @params.time_begin == "" ? null : @params.time_begin;
            @params.time_over = @params.time_over == "" ? null : @params.time_over;
            @params.pageindex = @params.pageindex == 0 ? 1 : @params.pageindex;
            @params.pagesize = @params.pagesize == 0 ? 20 : @params.pagesize;
            DateTime time_begin = @params.time_begin == null ? DateTime.MinValue : Convert.ToDateTime(@params.time_begin);
            DateTime time_over = @params.time_over == null ? DateTime.MaxValue : Convert.ToDateTime(@params.time_over);
            Task[] tasks = Db.Tasks.Where(x => x.create_time >= time_begin && x.create_time <= time_over && x.order_id == @params.order_id).Skip((@params.pageindex - 1) * @params.pagesize).Take(@params.pagesize).OrderBy(x => x.schedule_time).ToArray();
            int total = Db.Tasks.Where(x => x.create_time >= time_begin && x.create_time <= time_over && x.order_id == @params.order_id).ToArray().Length;
            var responses = new
            {
                total = total,
                data = tasks
            };
            return new BaseResponse<object>()
            {
                Data = responses
            };
        }

        /// <summary>
        /// 订单续费
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/user/order/renew")]
        public BaseResponse OrderRenew([FromBody] OrderRenewParams @params)
        {
            var order = Db.Orders.Find(@params.order_id);
            order.status = OrderStatus.Paied;
            order.days += @params.days;
            Db.Update(order);
            Db.SaveChanges();
            return new BaseResponse();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/user/change/password")]
        public BaseResponse ChangePassword([FromBody] ChangePasswordParams @params)
        {
            var user = Db.Users.Find(LoginInfo.Id);
            if (user.password != @params.password)
            {
                return new BaseResponse() { Code = 204, Message = "原密码输入错误，请重新输入！" };
            }
            else
            {
                user.password = @params.new_password;
                Db.Update(user);
                Db.SaveChanges();
                return new BaseResponse() { Code = 200, Message = "修改成功！" };
            }


        }

        /// <summary>
        /// 获取交易记录
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/user/trades/list")]
        public BaseResponse<object> TradeList([FromBody] TradeListParams @params)
        {
            @params.time_begin = @params.time_begin == "" ? null : @params.time_begin;
            @params.time_over = @params.time_over == "" ? null : @params.time_over;
            @params.pageindex = @params.pageindex == 0 ? 1 : @params.pageindex;
            @params.pagesize = @params.pagesize == 0 ? 20 : @params.pagesize;
            DateTime time_begin = @params.time_begin == null ? DateTime.MinValue : Convert.ToDateTime(@params.time_begin);
            DateTime time_over = @params.time_over == null ? DateTime.MaxValue : Convert.ToDateTime(@params.time_over);
            if (@params.status == 0)
            {
                Trade[] trades = Db.Trades.Where(x => x.user_id == LoginInfo.Id && x.create_time >= time_begin && x.create_time <= time_over).Skip((@params.pageindex - 1) * @params.pagesize).Take(@params.pagesize).ToArray();
                int total = Db.Trades.Where(x => x.user_id == LoginInfo.Id && x.create_time >= time_begin && x.create_time <= time_over).ToArray().Length;
                var responses = new
                {
                    total = total,
                    data = trades
                };
                return new BaseResponse<object>() { Data = responses };
            }
            else
            {
                TradeType status;
                if (@params.status == 1)
                    status = TradeType.Recharge;
                else if (@params.status == 2)
                    status = TradeType.Withdraw;
                else if (@params.status == 3)
                    status = TradeType.Refund;
                else if (@params.status == 4)
                    status = TradeType.Cost;
                else
                    status = TradeType.Commission;
                Trade[] trades = Db.Trades.Where(x => x.user_id == LoginInfo.Id && x.create_time >= time_begin && x.create_time <= time_over && x.trade_type == status).Skip((@params.pageindex - 1) * @params.pagesize).Take(@params.pagesize).ToArray();
                int total = Db.Trades.Where(x => x.user_id == LoginInfo.Id && x.create_time >= time_begin && x.create_time <= time_over && x.trade_type == status).ToArray().Length;
                var responses = new
                {
                    total = total,
                    data = trades
                };
                return new BaseResponse<object>()
                {
                    Data = responses
                };
            }
        }

        /// <summary>
        /// 获取充值方案列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("/user/recharge/list")]
        public BaseResponse<object> RechargeList()
        {
            var query = from b in Db.RechargePlans
                        select new
                        {
                            id = b.id,
                            url = b.url
                        };
            return new BaseResponse<object>()
            {
                Data = query.ToArray()
            };
        }

        /// <summary>
        /// 获取分组列表
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/user/group/list")]
        public BaseResponse<object> GroupList([FromBody] GroupListParams @params)
        {
            Group[] groups = Db.Groups.Where(x => x.user_id == LoginInfo.Id).Skip((@params.pageindex - 1) * @params.pagesize).Take(@params.pagesize).ToArray();
            object[] item = new object[groups.Length];
            int i = 0;
            foreach (var group in groups)
            {
                int count = Db.Orders.Where(x => x.group_id == group.id).ToArray().Length;
                var result = new
                {
                    id = group.id,
                    group_name = group.group_name,
                    create_time = group.create_time,
                    order_count = count
                };
                item[i] = result;
                i++;
            }
            int total = Db.Groups.ToArray().Length;
            var response = new
            {
                data = item,
                total = total
            };
            return new BaseResponse<object>()
            {
                Data = response
            };
        }

        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/user/group/del")]
        public BaseResponse GroupDel([FromBody] DelParams @params)
        {
            foreach (int id in @params.id)
            {
                Group group = Db.Groups.Find(id);
                Order[] orders = Db.Orders.Where(x => x.group_id == group.id).ToArray();
                foreach (Order order in orders)
                {
                    Task[] tasks = Db.Tasks.Where(x => x.order_id == order.id).ToArray();
                    foreach (Task task in tasks)
                    {
                        Db.Tasks.Remove(task);
                        Db.SaveChanges();
                    }
                    Db.Orders.Remove(order);
                    Db.SaveChanges();
                }
                Db.Groups.Remove(group);
                Db.SaveChanges();
            }
            return new BaseResponse();
        }

        /// <summary>
        /// 清空分组
        /// </summary>
        /// <returns></returns>
        [HttpGet("/user/group/empty")]
        public BaseResponse GroupEmpty()
        {
            Group[] groups = Db.Groups.Where(x => x.user_id == LoginInfo.Id).ToArray();
            foreach (Group group in groups)
            {
                Order[] orders = Db.Orders.Where(x => x.group_id == group.id).ToArray();
                foreach (Order order in orders)
                {
                    Task[] tasks = Db.Tasks.Where(x => x.order_id == order.id).ToArray();
                    foreach (Task task in tasks)
                    {
                        Db.Tasks.Remove(task);
                        Db.SaveChanges();
                    }
                    Db.Orders.Remove(order);
                    Db.SaveChanges();
                }
                Db.Groups.Remove(group);
                Db.SaveChanges();
            }
            return new BaseResponse();
        }

        /// <summary>
        /// 添加分组
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/user/group/add")]
        public BaseResponse GroupAdd([FromBody] GroupAddParams[] @params)
        {
            foreach (var item in @params)
            {
                Group group = new Group()
                {
                    user_id = LoginInfo.Id,
                    cover = item.cover,
                    details = item.details,
                    group_name = item.group_name
                };
                Db.Groups.Add(group);
                Db.SaveChanges();
            }
            return new BaseResponse();
        }

        /// <summary>
        /// 清空订单
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/user/order/empty")]
        public BaseResponse OrderEmpty([FromBody] OrderEmptyParams @params)
        {
            Order[] orders = Db.Orders.Where(x => x.group_id == @params.id).ToArray();
            foreach (var item in orders)
            {
                Db.Orders.Remove(item);
                Db.SaveChanges();
            }
            return new BaseResponse();
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/user/order/del")]
        public BaseResponse OrderDel([FromBody] OrderDelParams[] @params)
        {
            if (@params.Length == 0)
            {
                return new BaseResponse() { Code = 204, Message = "请选择需要删除的订单" };
            }
            else
            {
                foreach (var item in @params)
                {
                    Order order = Db.Orders.Find(item.id);
                    Task[] tasks = Db.Tasks.Where(x => x.order_id == order.id).ToArray();
                    Db.Tasks.RemoveRange(tasks);
                    Db.Orders.Remove(order);
                    Db.SaveChanges();
                }
                return new BaseResponse() { Code = 200, Message = "成功" };
            }
        }

        /// <summary>
        /// 获取排名
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/user/order/rank")]
        public BaseResponse<object> GetRank([FromBody] GetRankParams @params)
        {
            User user = Db.Users.Find(LoginInfo.Id);
            ErrorWhen(user.coin < 0.2, 204, "余额不足！");
            user.coin -= 0.2;
            Db.SaveChanges();
            Order order = Db.Orders.Find(@params.id);
            int point = 100;
            int rank = 100;
            if (order.platform == Platform.Baidu)
            {
                point = RankHelper.GetPoint(RankHelper.PointBaiduRank(order.keyword), "baidupc");
                try
                {
                    rank = RankHelper.QueryPcBaiduRank(order.keyword, order.domain).GetRank();
                }
                catch
                {
                    rank = 100;
                }

            }
            if (order.platform == Platform.MBaidu)
            {
                point = RankHelper.GetPoint(RankHelper.PointBaiduRank(order.keyword), "baidumb");
                try
                {
                    rank = RankHelper.QueryMobileBaiduRank(order.keyword, order.domain).GetRank();
                }
                catch
                {
                    rank = 100;
                }

            }
            if (order.platform == Platform.Pc360)
            {
                point = RankHelper.GetPoint(RankHelper.PointBaiduRank(order.keyword), "sopc");
                try
                {
                    rank = RankHelper.QueryPc360Rank(order.keyword, order.domain).GetRank();
                }
                catch
                {
                    rank = 100;
                }

            }
            if (order.platform == Platform.Sogou)
            {
                point = RankHelper.GetPoint(RankHelper.PointBaiduRank(order.keyword), "sogoupc");
                try
                {
                    rank = RankHelper.QueryPcSogouRank(order.keyword, order.domain).GetRank();
                }
                catch
                {
                    rank = 100;
                }
            }
            if (order.platform == Platform.MSogou)
            {
                point = RankHelper.GetPoint(RankHelper.PointBaiduRank(order.keyword), "sogoumb");
                try
                {
                    rank = RankHelper.QueryMobileSogouRank(order.keyword, order.domain).GetRank();
                }
                catch
                {
                    rank = 100;
                }
            }
            order.current_rank = rank;
            order.point = point;
            Db.Orders.Update(order);
            Db.SaveChanges();
            return new BaseResponse<object>()
            {
                Data = new
                {
                    point = point,
                    rank = rank
                }
            };
        }

        /// <summary>
        /// 订单编辑
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/user/order/edit")]
        public BaseResponse OrderEdit([FromBody] OrderEditParams @params)
        {

            Order order = Db.Orders.Find(@params.id);
            order.platform = @params.platform;
            order.keyword = @params.keyword;
            order.domain = @params.domain;
            order.days = @params.days;
            order.title = @params.title;
            order.xiongzhang = @params.xiongzhang;
            if (@params.clickLimtCount["max"] != 0)
            {
                order.clickLimtCount = _methods.DictToJson(@params.clickLimtCount);
            }
            bool codition = false;
            foreach (var item in @params.clickPlan)
            {
                if (item.Value != 0)
                {
                    codition = true;
                    break;
                }
            }
            if (codition)
            {
                order.times = _methods.DictToJson(@params.clickPlan);
            }
            if (@params.fixed_click != 0)
            {
                order.fixed_click = @params.fixed_click;
            }
            order.randomJumpCount = _methods.DictToJson(@params.randomJumpCount);
            order.randomWaitCount = _methods.DictToJson(@params.randomWaitCount);
            order.increase = _methods.DictToJson(@params.increase);
            order.percentage = _methods.DictToJson(@params.percentage);
            Db.Orders.Update(order);
            Db.SaveChanges();
            return new BaseResponse();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        [HttpPost("/user/pwd/change")]
        public BaseResponse Changepwd([FromBody] ChangePasswordParams @params)
        {
            string verify_code = "";
            try
            {
                verify_code = Encoding.Unicode.GetString(_redis.Get(@params.verifyCode_key));
                Console.WriteLine(verify_code);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            ErrorWhen(verify_code != @params.verify_code, code: 200, message: "验证码错误！");
            var user = Db.Users.Find(LoginInfo.Id);
            if (user.password == @params.password)
            {
                user.password = @params.new_password;
                Db.Users.Update(user);
                if (Db.SaveChanges() > 0)
                {
                    return new BaseResponse() { Code = 200, Message = "修改成功！" };
                }
                else
                {
                    return new BaseResponse() { Code = 204, Message = "错误代码204！" };
                }
            }
            else
            {
                return new BaseResponse() { Code = 204, Message = "原密码不匹配，请重试！" };
            }
        }

        /// <summary>
        /// 获取每日免费分析次数
        /// </summary>
        /// <returns></returns>
        [HttpGet("/user/analysis/get")]
        public BaseResponse Getanalysis()
        {
            User user = Db.Users.Find(LoginInfo.Id);
            if (user.analysis_date < DateTime.Today)
            {

            }
            if (user.analysis_date >= DateTime.Today && user.analysis_date < DateTime.Today.AddDays(1))
            {
                return new BaseResponse<object>() { Data = user.analysis_times };
            }
            return new BaseResponse<object>() { Data = 0 };
        }

        /// <summary>
        /// 一键分析
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/user/analysis/data")]
        public BaseResponse Analysis([FromBody] AnalysisParams @params)
        {
            User user = Db.Users.Find(LoginInfo.Id);
            if (user.analysis_times > 0 && user.analysis_date >= DateTime.Today && user.analysis_date < DateTime.Today.AddDays(1))
            {
                user.analysis_times -= 1;
                if (user.analysis_times == 0)
                {
                    user.analysis_times = 2;
                    user.analysis_date = DateTime.Today.AddDays(1);
                }
                Db.Update(user);
                Db.SaveChanges();
            }
            else
            {
                if (user.coin < 12)
                    return new BaseResponse() { Code = 204, Message = "余额不足" };
                user.coin -= 12;
                Db.Update(user);
                Db.SaveChanges();
            }
            int id = 1;
            _5118apis apis = new _5118apis(Db, _Configuration);
            if (@params.platform == Platform.Baidu)
            {
                JObject result = apis.Get("baidupc", @params.url, @params.pageindex, @params.pagesize);
                foreach (var item in result["data"]["baidupc"])
                {
                    item["domain"] = @params.url;
                    item["index"] = id;
                    id++;
                    int index = Convert.ToInt32(item["baidu_index"]);
                    item["opt_status"] = "未优化";
                    if (Db.Orders.Any(x => x.domain == @params.url && x.keyword == item["keyword"].ToString()))
                        item["opt_status"] = "已优化";
                    if (index >= 0 && index <= 50)
                        item["opt_times"] = "1-3";
                    else if (index > 50 && index <= 200)
                        item["opt_times"] = "5-8";
                    else if (index > 200 && index <= 500)
                        item["opt_times"] = "20-30";
                    else if (index > 500 && index <= 1000)
                        item["opt_times"] = "50-60";
                    else if (index > 1000)
                        item["opt_times"] = "100-120";
                    else
                        item["opt_times"] = "5-8";
                }
                return new BaseResponse<object>() { Data = result.ToString() };
            }
            if (@params.platform == Platform.MBaidu)
            {
                JObject result = apis.Get("baidumobile", @params.url, @params.pageindex, @params.pagesize);
                foreach (var item in result["data"]["baidumobile"])
                {
                    item["domain"] = @params.url;
                    item["index"] = id;
                    id++;
                    int index = Convert.ToInt32(item["baidu_mobile_index"]);
                    item["opt_status"] = "未优化";
                    if (Db.Orders.Any(x => x.domain == @params.url && x.keyword == item["keyword"].ToString()))
                        item["opt_status"] = "已优化";
                    if (index >= 0 && index <= 50)
                        item["opt_times"] = "1-3";
                    else if (index > 50 && index <= 200)
                        item["opt_times"] = "5-8";
                    else if (index > 200 && index <= 500)
                        item["opt_times"] = "20-30";
                    else if (index > 500 && index <= 1000)
                        item["opt_times"] = "50-60";
                    else if (index > 1000)
                        item["opt_times"] = "100-120";
                    else
                        item["opt_times"] = "5-8";
                }
                return new BaseResponse<object>() { Data = result.ToString() };
            }
            if (@params.platform == Platform.Pc360)
            {
                JObject result = apis.Get("haosou", @params.url, @params.pageindex, @params.pagesize);
                foreach (var item in result["data"]["haosou"])
                {
                    item["domain"] = @params.url;
                    item["index"] = id;
                    id++;
                    int index = Convert.ToInt32(item["haosou"]);
                    item["opt_status"] = "未优化";
                    if (Db.Orders.Any(x => x.domain == @params.url && x.keyword == item["keyword"].ToString()))
                        item["opt_status"] = "已优化";
                    item["opt_times"] = "5-8";
                }
                return new BaseResponse<object>() { Data = result.ToString() };
            }
            return new BaseResponse() { Message = "无数据" };
        }
    }
}