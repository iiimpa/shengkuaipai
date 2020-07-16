using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YYApi.Controllers;
using YYApi.Helpers;
using YYApi.Communications;
using WebApi.Models;
using WebApi.Helpers;
using System.IO;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Task = WebApi.Models.Task;
using TaskStatus = WebApi.Helpers.TaskStatus;
using System.Collections.Specialized;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi.RequestParams;
using Org.BouncyCastle.Utilities;

namespace WebApi.Controllers
{
    public class PublicController : BaseController
    {
        private DataContext _db { get; set; }
        private Alipay _alipay { get; set; }
        private IConfiguration _config { get; }
        private Methods _methods { get; set; }
        private RankHelper RankHelper { get; }
        public PublicController(DataContext db, Alipay alipay, IConfiguration configuration, Methods methods, RankHelper rankhelper)
        {
            _db = db;
            _alipay = alipay;
            _config = configuration;
            _methods = methods;
            RankHelper = rankhelper;
        }

        /// <summary>
        /// 支付宝回调
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/public/payback/alipay")]
        public string AlipayNotify([FromForm] Dictionary<string, string> @params)
        {
            foreach (var item in @params)
            {
                Console.WriteLine($"key:{item.Key}  value:{item.Value}");
            }
            if (_alipay.SignCheck(@params))
            {
                var trade = _db.Trades.Where(x => x.trade_no == @params["out_trade_no"] && x.trade_type == TradeType.Recharge).FirstOrDefault();
                if (trade != null)
                {
                    trade.status = true;
                    _db.Trades.Update(trade);
                    var user = _db.Users.Find(trade.user_id);
                    user.coin += trade.coin;
                    _db.Users.Update(user);
                    _db.SaveChanges();
                    return "success";
                }
            }
            return "faild";
        }

        /// <summary>
        /// 任务生成端获取订单
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/public/order/get")]
        public BaseResponse<object> TaskCreate([FromBody] RunnerParams @params)
        {
            ErrorWhen(@params.token == _config["Token"], 400, "身份验证失败！");
            Order order;
            try
            {
                order = _db.Orders.Where(x => x.create_status == CreateStatus.Waiting && x.status == OrderStatus.Paied && x.task_create_time >= DateTime.Today && x.task_create_time < DateTime.Today.AddDays(1)).First();
                //order = _db.Orders.Where(x => x.create_status == CreateStatus.Waiting).First();
            }
            catch
            {
                return null;
            }
            order.create_status = CreateStatus.Running;
            _db.Update(order);
            if (_db.SaveChanges() > 0)
            {
                return new BaseResponse<object>() { Data = order };
            }
            else
            {
                order = null;
                return new BaseResponse<object>()
                {
                    Data = order
                };
            }
        }

        /// <summary>
        /// 检测执行中任务，超过5分钟重新执行
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/public/task/inspect")]
        public BaseResponse TaskInspect([FromBody] RunnerParams @params)
        {
            ErrorWhen(@params.token == _config["Token"], 400, "身份验证失败！");
            Task[] tasks = _db.Tasks.Where(x => x.schedule_time >= DateTime.Today && x.schedule_time < DateTime.Now && x.status == TaskStatus.Running).ToArray();
            int count = 0;
            foreach (var item in tasks)
            {
                TimeSpan sp = DateTime.Now.Subtract(item.schedule_time);
                int minutes = sp.Hours * 60 + sp.Minutes;
                if (minutes >= 5)
                {
                    item.status = TaskStatus.Wait;
                    _db.Tasks.Update(item);
                    if (_db.SaveChanges() > 0)
                        count++;
                }
            }
            return new BaseResponse() { Message = $"检测成功,{count}条任务已重新执行！" };
        }

        /// <summary>
        /// 生产任务
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/public/task/add")]
        public BaseResponse TaskAdd([FromBody] TaskAddParams[] @params)
        {
            Console.WriteLine($"本次生产任务编号{@params[0].order_id},生产{@params.Length}条任务！");
            ErrorWhen(@params[0].token == _config["Token"], 400, "身份验证失败！");
            var order = _db.Orders.Find(@params[0].order_id);
            if (order.create_status != CreateStatus.Running || (order.task_create_time >= DateTime.Today.AddDays(1) || order.task_create_time < DateTime.Today))
                return new BaseResponse();
            var user = _db.Users.Find(order.user_id);
            if (user.frozen_time < DateTime.Today)
            {
                user.frozen = user.coin;
                user.frozen_time = DateTime.Today;
                _db.Users.Update(user);
                _db.SaveChanges();
            }
            var frozen = user.frozen;
            foreach (var item in @params)
            {
                Platform platform = Platform.Baidu;
                if (item.platform == 0)
                    platform = Platform.Baidu;
                else if (item.platform == 1)
                    platform = Platform.Sogou;
                else if (item.platform == 2)
                    platform = Platform.Pc360;
                else if (item.platform == 3)
                    platform = Platform.MBaidu;
                else if (item.platform == 4)
                    platform = Platform.MSogou;
                else if (item.platform == 5)
                    platform = Platform.M360;
                Task task = new Task
                {
                    order_id = item.order_id,
                    status = TaskStatus.Wait,
                    schedule_time = Convert.ToDateTime(item.schedule_time),
                    cost = 1,
                    optimization = item.optimization,
                    mode = item.mode,
                    create_time = DateTime.Now,
                    domain = item.domain,
                    keyword = item.keyword,
                    platform = platform,
                    jump_times = item.jump_times,
                    stay_time = _methods.ArrayToStr(item.stay_time),
                    xiongzhang = item.xiongzhang
                };
                if (frozen > 0)
                {
                    _db.Tasks.Add(task);
                    if (_db.SaveChanges() > 0)
                        frozen -= 1;
                }
            }
            user.frozen = frozen;
            _db.Users.Update(user);
            _db.SaveChanges();
            return new BaseResponse();
        }

        /// <summary>
        /// 生产任务后上报订单状态
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/public/order/report")]
        public BaseResponse OrderReport([FromBody] ReportParams @params)
        {
            ErrorWhen(@params.token == _config["Token"], 400, "身份验证失败！");
            Order order = _db.Orders.Find(@params.order_id);
            var percentage = _methods.StringToDict(order.percentage);
            var increase = _methods.StringToDict(order.increase);
            var times = _methods.StringToDict(order.times);

            //判断数量增加，百分比增加
            if (increase.ContainsKey("days"))
            {
                //判断增加周期
                if (_methods.DateDiff(DateTime.Today, order.create_time) != 0 && _methods.DateDiff(DateTime.Today, order.create_time) % increase["days"] == 0)
                {
                    //判断增加类型
                    if (increase.ContainsKey("fixed"))
                    {
                        //判断点击范围，固定点击，按时点击
                        if (_methods.isTimes(times))
                        {
                            for (int i = 0; i < times.Count; i++)
                            {
                                times[$"t{i}"] += increase["fixed"];
                            }
                            order.times = _methods.DictToJson(times);
                            _db.Orders.Update(order);
                            _db.SaveChanges();
                        }
                        else if (order.fixed_click != 0)
                        {
                            order.fixed_click += increase["fixed"];
                            _db.Orders.Update(order);
                            _db.SaveChanges();
                        }
                        else if (_methods.isRange(_methods.StringToDict(order.clickLimtCount)))
                        {
                            var click = _methods.StringToDict(order.clickLimtCount);
                            click["min"] += increase["fixed"];
                            click["max"] += increase["fixed"];
                            order.clickLimtCount = _methods.DictToJson(click);
                            _db.Orders.Update(order);
                            _db.SaveChanges();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        Random rand = new Random();
                        int num = rand.Next(increase["min"], increase["max"] + 1);
                        //判断点击范围，固定点击，按时点击
                        if (_methods.isTimes(times))
                        {
                            for (int i = 0; i < times.Count; i++)
                            {
                                times[$"t{i}"] += num;
                            }
                            order.times = _methods.DictToJson(times);
                            _db.Orders.Update(order);
                            _db.SaveChanges();
                        }
                        else if (order.fixed_click != 0)
                        {
                            order.fixed_click += num;
                            _db.Orders.Update(order);
                            _db.SaveChanges();
                        }
                        else if (_methods.isRange(_methods.StringToDict(order.clickLimtCount)))
                        {
                            var click = _methods.StringToDict(order.clickLimtCount);
                            click["min"] += num;
                            click["max"] += num;
                            order.clickLimtCount = _methods.DictToJson(click);
                            _db.Orders.Update(order);
                            _db.SaveChanges();
                        }
                        else
                        {

                        }
                    }
                }
                else
                { }
            }
            else if (percentage.ContainsKey("days"))
            {
                //判断增加周期
                if (_methods.DateDiff(DateTime.Today, order.create_time) != 0 && _methods.DateDiff(DateTime.Today, order.create_time) % percentage["days"] == 0)
                {
                    //判断增加类型
                    if (percentage.ContainsKey("fixed"))
                    {
                        //判断点击范围，固定点击，按时点击
                        if (_methods.isTimes(times))
                        {
                            for (int i = 0; i < times.Count; i++)
                            {
                                times[$"t{i}"] += percentage["fixed"];
                            }
                            order.times = _methods.DictToJson(times);
                            _db.Orders.Update(order);
                            _db.SaveChanges();
                        }
                        else if (order.fixed_click != 0)
                        {
                            order.fixed_click += percentage["fixed"];
                            _db.Orders.Update(order);
                            _db.SaveChanges();
                        }
                        else if (_methods.isRange(_methods.StringToDict(order.clickLimtCount)))
                        {
                            var click = _methods.StringToDict(order.clickLimtCount);
                            click["min"] += percentage["fixed"];
                            click["max"] += percentage["fixed"];
                            order.clickLimtCount = _methods.DictToJson(click);
                            _db.Orders.Update(order);
                            _db.SaveChanges();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        Random rand = new Random();
                        int num = rand.Next(percentage["min"], percentage["max"] + 1);
                        //判断点击范围，固定点击，按时点击
                        if (_methods.isTimes(times))
                        {
                            for (int i = 0; i < times.Count; i++)
                            {
                                times[$"t{i}"] += num;
                            }
                            order.times = _methods.DictToJson(times);
                            _db.Orders.Update(order);
                            _db.SaveChanges();
                        }
                        else if (order.fixed_click != 0)
                        {
                            order.fixed_click += num;
                            _db.Orders.Update(order);
                            _db.SaveChanges();
                        }
                        else if (_methods.isRange(_methods.StringToDict(order.clickLimtCount)))
                        {
                            var click = _methods.StringToDict(order.clickLimtCount);
                            click["min"] += num;
                            click["max"] += num;
                            order.clickLimtCount = _methods.DictToJson(click);
                            _db.Orders.Update(order);
                            _db.SaveChanges();
                        }
                        else
                        {

                        }
                    }
                }
                else
                { }
            }
            else
            {

            }

            if (Convert.ToDateTime(order.create_time.AddDays(order.days).ToString("yyyy-MM-dd 00:00:00")) < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00")))
            {
                order.create_status = CreateStatus.Done;
                _db.Orders.Update(order);
                _db.SaveChanges();
            }
            else
            {
                order.create_status = CreateStatus.Waiting;
                order.task_create_time = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));
                _db.Orders.Update(order);
                _db.SaveChanges();
            }
            return new BaseResponse();
        }

        /// <summary>
        /// 上报任务状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/api/task/report")]
        public BaseResponse ReportTask([FromBody] ReportResultRequest request)
        {
            ErrorWhen(request.Token != _config["Runner:Token"], 401);
            var task = _db.Tasks.Find(request.Id);
            var order = _db.Orders.Find(task.order_id);
            int times = task.stay_time.Split(",").Length;
            order.ipcount += times;
            ErrorWhen(task == null, 400);
            task.user_agent = request.Ua;
            task.proxy_ip = request.Proxy;
            task.resolution = request.Resolution;
            task.status = request.Status;
            task.finish_time = DateTime.Now;
            double realCost;
            switch (request.Status)
            {
                case TaskStatus.Success:
                    realCost = task.cost;
                    break;
                case TaskStatus.NotFound:
                case TaskStatus.SurfaceError:
                    realCost = task.cost * 7 / 10;
                    break;
                default:
                    realCost = 0;
                    break;
            }
            if (realCost > 0)
            {
                order.update_time = DateTime.Now;
                _db.Update(order);
            }
            task.realcost = realCost;
            _db.Update(task);
            _db.Update(order);
            _db.SaveChanges();
            return new BaseResponse();
        }

        /// <summary>
        /// 获取相关任务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/api/task/check"), ApiDoc("Runner", "获取任务")]
        public CheckTaskResponse CheckTask([FromBody] CheckTaskRequest request)
        {
            ErrorWhen(request.Token != _config["Runner:Token"], 401);
            var task = _db.Tasks.Where(x => x.schedule_time < DateTime.Now && x.status == TaskStatus.Wait).OrderBy(x => x.schedule_time).FirstOrDefault();
            if (task == null)
            {
                return new CheckTaskResponse() { Code = 204, Message = "暂无任务！" };
            }
            else
            {
                task.status = TaskStatus.Running;
                task.request_time = DateTime.Now;
                try
                {
                    var order = _db.Orders.Find(task.order_id);
                    var user = _db.Users.Find(order.user_id);
                    if (user.coin < task.cost)
                    {
                        task.status = TaskStatus.Failed;
                        _db.Tasks.Update(task);
                        _db.SaveChanges();
                        return new CheckTaskResponse() { Code = 204, Message = "余额不足！" };
                    }
                    user.coin -= task.cost;
                    _db.Update(task);
                    _db.SaveChanges();
                    return new CheckTaskResponse
                    {
                        Id = task.id,
                        Platform = task.platform,
                        Keyword = task.keyword,
                        mode = task.mode,
                        optimization = task.optimization,
                        Domain = task.domain,
                        Times = _methods.StringToIntArray(task.stay_time),
                        jump_times = task.jump_times,
                        xiongzhang = task.xiongzhang
                    };
                }
                catch
                {
                    _db.Tasks.Remove(task);
                    _db.SaveChanges();
                    return new CheckTaskResponse() { Code = 204, Message = "暂无任务！" };
                }

            }
        }

        /// <summary>
        /// 每日刷新排名
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/public/rank/get")]
        public BaseResponse GetRank([FromBody] GetRankParams @params)
        {
            var order = _db.Orders.Find(@params.id);
            var user = _db.Users.Find(order.user_id);
            ErrorWhen(user.coin < 0.2, 204, "余额不足！");
            user.coin -= 0.2;
            _db.SaveChanges();
            var rankinfo = _methods.GetRank(RankHelper, order);
            if (order.create_time >= DateTime.Today && order.create_time < DateTime.Today.AddDays(1))
            {
                order.start_rank = rankinfo.rank;
            }
            order.current_rank = rankinfo.rank;
            order.point = rankinfo.point;
            _db.Orders.Update(order);
            if (_db.SaveChanges() > 0)
            {
                return new BaseResponse() { Message = "成功" };
            }
            return new BaseResponse() { Message = "失败" };
        }

        [HttpGet("/public/utils")]
        public BaseResponse Utils()
        {
            var tasks = _db.Tasks.Where(x => x.mode == Mode.Create_keyword && x.status == TaskStatus.Wait).ToArray();
            foreach (var item in tasks)
            {
                item.optimization = item.keyword + item.domain;
            }
            _db.UpdateRange();
            if (_db.SaveChanges() > 0)
                return new BaseResponse();
            return new BaseResponse() { Code = 405, Message = "修改失败！" };
        }

        public class RunnerParams
        {
            public string token { get; set; }
        }
        public class ReportParams
        {
            public int order_id { get; set; }
            public string token { get; set; }
        }
        public class ChargeParams
        {
            public string token { get; set; }
            public int user_id { get; set; }
            public double price { get; set; }
        }
        public class TaskAddParams
        {
            public string token { get; set; }
            public int order_id { get; set; }
            public string schedule_time { get; set; }
            public double cost { get; set; }
            public Mode mode { get; set; }
            public string domain { get; set; }
            public string optimization { get; set; }
            public int platform { get; set; }
            public int jump_times { get; set; }
            public int[] stay_time { get; set; }
            public string xiongzhang { get; set; }
            public string keyword { get; set; }
        }
        public class CheckTaskResponse : BaseResponse
        {
            public int Id { get; set; }
            public Platform Platform { get; set; }
            public string Keyword { get; set; }
            public string Domain { get; set; }
            public Mode mode { get; set; }
            public string optimization { get; set; }
            public int[] Times { get; set; }
            public int jump_times { get; set; }
            public string xiongzhang { get; set; }
        }
        public class CheckTaskRequest
        {
            public string Token { get; set; }
        }
        public class ReportResultRequest : CheckTaskRequest
        {
            public int Id { get; set; }
            public string Ua { get; set; }
            public string Proxy { get; set; }
            public string Resolution { get; set; }
            public TaskStatus Status { get; set; }
        }
    }
}