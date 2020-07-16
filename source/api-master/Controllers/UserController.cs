using System;
using System.Linq;
using System.Text.Json;
using Community;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Org.BouncyCastle.Ocsp;
using WebApi.Helpers;
using WebApi.Models;
using YYApi.Communications;
using YYApi.Controllers;
using YYApi.Helpers;

namespace WebApi.Controllers
{
    [CheckAuth(new[] { "user", "admin" })]
    public class UserController : BaseController
    {
        private DataContext Db { get; }
        private RankHelper RankHelper { get; }
        private Alipay Alipay { get; }

        public UserController(DataContext db, RankHelper rankHelper, Alipay alipay)
        {
            Db = db;
            Alipay = alipay;
            RankHelper = rankHelper;
        }

        /// <summary>
        /// 用户信息接口
        /// </summary>
        /// <returns></returns>
        [HttpPost("/user/info"), ApiDoc("用户", "用户信息")]
        public BaseResponse<User> Info()
        {
            var user = Db.Users.Find(LoginInfo.Id);
            user.Password = null;
            return new BaseResponse<User>
            {
                Data = user
            };
        }

        /// <summary>
        /// 获取点击方案
        /// </summary>
        /// <returns></returns>
        [HttpPost("/user/plan/list"), ApiDoc("用户", "获取点击方案")]
        public BaseResponse<Plan[]> PlanList()
        {
            return new BaseResponse<Plan[]>
            {
                Data = Db.Plans.ToArray()
            };
        }

        /// <summary>
        /// 获取点击方案
        /// </summary>
        /// <returns></returns>
        [HttpPost("/user/click-plan/list"), ApiDoc("用户", "获取点击次数方案")]
        public BaseResponse<ClickPlan[]> ClickPlanList()
        {
            return new BaseResponse<ClickPlan[]>
            {
                Data = Db.ClickPlans.ToArray()
            };
        }

        [HttpPost("/user/order/cancel"), ApiDoc("用户", "取消订单")]
        public BaseResponse CancelOrder([FromBody] UserOnlyIdRequest request)
        {
            var o = Db.Orders.Find(request.Id);
            ErrorWhenNull(o, 400);
            ErrorWhen(o.UserId != LoginInfo.Id, 403);
            var u = Db.Users.Find(o.UserId);
            o.Status = OrderStatus.Refund;
            u.Coin += o.Balance;
            o.Balance = 0;
            Db.Update(u);
            Db.Update(o);
            var t = Db.Tasks.Where(x => x.OrderId == o.Id && x.Status == TaskStatus.Wait).ToArray();
            Db.RemoveRange(t);
            Db.SaveChanges();
            return new BaseResponse();
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/user/order/add"), ApiDoc("用户", "创建点击订单")]
        public BaseResponse AddOrder([FromBody] UserOrderAddRequest request)
        {
            var user = Db.Users.Find(LoginInfo.Id);
            var plan = Db.Plans.Find(request.Plan);
            ErrorWhenNull(plan, 400, "没有找到指定的点击方案");
            var oneDay = request.Time.Sum();
            var allDay = oneDay;
            for (var i = 1; i < request.Days; i++)
            {
                oneDay = (int)(oneDay * (1 + request.Raise * 0.01));
                allDay += oneDay;
            }
            var amount = allDay * plan.Price;
            ErrorWhen(amount > user.Coin, 400, $"您的余额不足,无法创建本计划,您的余额:{user.Coin} ,计划花费: {amount}");
            user.Coin -= amount;
            Db.Update(user);
            var rand = new Random();
            var orderNo = DateTime.Now.ToString("yyyymmdd") + rand.Next(1000001, 9999999);
            var order = new Order
            {
                OrderNo = orderNo,
                UserId = user.Id,
                StartRank = request.Rank,
                Platform = request.Platform,
                PlanId = plan.Id,
                Keyword = request.Keyword,
                Domain = request.Domain,
                Days = request.Days,
                Amount = amount,
                Balance = amount,
                Status = OrderStatus.Paied,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now
            };
            Db.Add(order);
            Db.SaveChanges();
            var now = DateTime.Now;
            var baseTime = new DateTime(now.Year, now.Month, now.Day);
            //计算当天的
            for (var i = now.Hour + 1; i < 24; i++)
            {
                var times = request.Time[i];
                if (times > 0)
                {
                    var calcTime = baseTime.AddHours(i);
                    //开始创建计划
                    for (var ti = 0; ti < times; ti++)
                    {
                        var task = new Task
                        {
                            OrderId = order.Id,
                            Status = TaskStatus.Wait,
                            Cost = plan.Price,
                            RealCost = 0,
                            ScheduleTime = calcTime.AddMinutes(rand.Next(1, 60)),
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        };
                        Db.Add(task);
                    }
                }
            }
            Db.SaveChanges();
            //计算计划剩余天数的任务
            for (var i = 1; i < request.Days; i++)
            {
                var otherTime = baseTime.AddDays(i);
                for (var ai = 0; ai < 24; ai++)
                {
                    var times = request.Time[ai];
                    if (times > 0)
                    {
                        var calcTime = otherTime.AddHours(ai);
                        //开始创建计划
                        for (var ti = 0; ti < times; ti++)
                        {
                            var task = new Task
                            {
                                OrderId = order.Id,
                                Status = TaskStatus.Wait,
                                Cost = plan.Price,
                                RealCost = 0,
                                ScheduleTime = calcTime.AddMinutes(rand.Next(1, 60)),
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now
                            };
                            Db.Add(task);
                        }
                    }
                }
            }
            Db.SaveChanges();
            return new BaseResponse();
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("/user/order/list"), ApiDoc("用户", "订单列表")]
        public BaseResponse<Order[]> OrderList()
        {
            return new BaseResponse<Order[]>
            {
                Data = Db.Orders.Where(x => x.UserId == LoginInfo.Id).OrderByDescending(x => x.CreatedAt).ToArray()
            };
        }

        /// <summary>
        /// 获取关键词排名
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/user/rank"), ApiDoc("用户", "获取关键词排名")]
        public BaseResponse<int> GetRank([FromBody]GetRankRequest request)
        {
            var resp = new BaseResponse<int>();
            RankHelper.ResultResponse result;
            switch (request.Platform)
            {
                case Platform.Baidu:
                    result = RankHelper.QueryPcBaiduRank(request.Keyword, request.Domain);
                    resp.Data = result.GetRank();
                    break;
                case Platform.MBaidu:
                    result = RankHelper.QueryMobileBaiduRank(request.Keyword, request.Domain);
                    resp.Data = result.GetRank();
                    break;
                case Platform.Pc360:
                    result = RankHelper.QueryPc360Rank(request.Keyword, request.Domain);
                    resp.Data = result.GetRank();
                    break;
                case Platform.M360:
                    result = RankHelper.QueryMobile360Rank(request.Keyword, request.Domain);
                    resp.Data = result.GetRank();
                    break;
                case Platform.Sogou:
                    result = RankHelper.QueryPcSogouRank(request.Keyword, request.Domain);
                    resp.Data = result.GetRank();
                    break;
                case Platform.MSogou:
                    result = RankHelper.QueryMobileSogouRank(request.Keyword, request.Domain);
                    resp.Data = result.GetRank();
                    break;
            }
            return resp;
        }

        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/user/task/list"), ApiDoc("用户", "任务列表")]
        public BaseResponse<Task[]> TaskList([FromBody] UserTaskListRequest request)
        {
            return new BaseResponse<Task[]>
            {
                Data = Db.Tasks.Where(x => x.OrderId == request.OrderId).OrderBy(x => x.ScheduleTime).ToArray()
            };
        }

        /// <summary>
        /// 用户修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/user/change-password"), ApiDoc("用户", "修改密码")]
        public BaseResponse ChangePassword([FromBody]UserChangePasswordRequest request)
        {
            var u = Db.Users.Find(LoginInfo.Id);
            ErrorWhenNull(u, 400);
            ErrorWhen(u.Password != request.Password, 400, "当前密码不正确");
            u.Password = request.NewPassword;
            Db.Update(u);
            Db.SaveChanges();
            return new BaseResponse();
        }


        /// <summary>
        /// 修改绑定支付宝
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/user/change-alipay"), ApiDoc("用户", "修改支付宝")]
        public BaseResponse ChangeAlipay([FromBody]UserChangeAlipayRequest request)
        {
            var u = Db.Users.Find(LoginInfo.Id);
            ErrorWhenNull(u, 400);
            ErrorWhen(u.Password != request.Password, 400, "当前密码不正确");
            u.Alipay = request.Alipay;
            Db.Update(u);
            Db.SaveChanges();
            return new BaseResponse();
        }

        /// <summary>
        /// 获取下级列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/user/downlist"), ApiDoc("用户", "获取下级列表")]
        public BaseResponse<User[]> GetDown([FromBody]UserGetDownRequest request)
        {
            return new BaseResponse<User[]>
            {
                Data = Db.Users.Where(x => x.Pid == request.Id).ToArray()
            };
        }

        /// <summary>
        /// 获取交易记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/user/trades"), ApiDoc("用户", "获取交易列表")]
        public BaseResponse<Trade[]> GetTrade([FromBody]UserGetTradeRequest request)
        {
            return new BaseResponse<Trade[]>
            {
                Data = Db.Trades.Where(x => x.UserId == LoginInfo.Id && x.Type == request.Type).ToArray()
            };
        }

        /// <summary>
        /// 获取用户充值方案
        /// </summary>
        /// <returns></returns>
        [HttpPost("/user/recharge/plans"), ApiDoc("用户", "获取充值方案")]
        public BaseResponse<RechargePlan[]> GetRechargePlans()
        {
            return new BaseResponse<RechargePlan[]>
            {
                Data = Db.RechargePlans.ToArray()
            };
        }

        /// <summary>
        /// 请求支付链接
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/user/recharge/query"), ApiDoc("用户", "请求支付链接")]
        public BaseResponse<string> DoRecharge([FromBody]UserRechargeRequest request)
        {
            var plan = Db.RechargePlans.Find(request.Rid);
            ErrorWhenNull(plan, 400);
            var oid = Guid.NewGuid().ToString();
            var trade = new Trade
            {
                Type = TradeTypeEnum.Recharge,
                UserId = LoginInfo.Id,
                TradeNo = oid,
                Coin = plan.GiftCoin + plan.RealCoin,
                Amount = plan.Amount,
                RelationId = plan.Id,
                Description = "充值"
            };
            Db.Add(trade);
            Db.SaveChanges();
            var resp = Alipay.Pay(oid, (double)plan.Amount, request.BackUrl);
            return new BaseResponse<string> { Data = resp };
        }

        [HttpPost("/user/withdraw/query"), ApiDoc("用户", "提现")]
        public BaseResponse DoWithdraw()
        {
            var u = Db.Users.Find(LoginInfo.Id);
            var trade = new Trade
            {
                Type = TradeTypeEnum.Withdraw,
                UserId = u.Id,
                Amount = u.Balance,
                Description = $"{u.Account} => {u.Alipay}"
            };
            u.Balance = 0;
            Db.Update(u);
            Db.Add(trade);
            Db.SaveChanges();
            return new BaseResponse();
        }

    }
}
