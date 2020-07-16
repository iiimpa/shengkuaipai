using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Helpers;
using System.Linq;
using WebApi.Models;
using YYApi.Communications;
using YYApi.Controllers;
using YYApi.Helpers;
using System.Text.Json;

namespace Controllers
{
    public class ApiController : BaseController
    {
        private DataContext Db { get; set; }
        private IConfiguration Config { get; set; }

        public ApiController(DataContext db, IConfiguration config)
        {
            Config = config;
            Db = db;
        }

        /// <summary>
        /// 获取相关任务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/api/task/check"), ApiDoc("Runner", "获取任务")]
        public CheckTaskResponse CheckTask([FromBody] CheckTaskRequest request)
        {
            ErrorWhen(request.Token != Config["Runner:Token"], 401);
            var task = Db.Tasks.Where(x => x.ScheduleTime < DateTime.Now && x.Status == TaskStatus.Wait).FirstOrDefault();
            ErrorWhen(task == null, 204);
            task.Status = TaskStatus.Running;
            task.RequestTime = DateTime.Now;
            Db.Update(task);
            Db.SaveChanges();
            var order = Db.Orders.Find(task.OrderId);
            var plan = Db.Plans.Find(order.PlanId);
            return new CheckTaskResponse
            {
                Id = task.Id,
                Platform = order.Platform,
                Keyword = order.Keyword,
                Domain = order.Domain,
                Times = JsonSerializer.Deserialize<int[]>(plan.Times)
            };
        }

        /// <summary>
        /// 上报任务状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/api/task/report"), ApiDoc("Runner", "上报任务结果")]
        public BaseResponse ReportTask([FromBody]ReportResultRequest request)
        {
            ErrorWhen(request.Token != Config["Runner:Token"], 401);
            var task = Db.Tasks.Find(request.Id);
            ErrorWhen(task == null, 400);
            task.Ua = request.Ua;
            task.Proxy = request.Proxy;
            task.Resolution = request.Resolution;
            task.Status = request.Status;
            task.FinishTime = DateTime.Now;
            int realCost;
            switch (request.Status)
            {
                case TaskStatus.Success:
                    realCost = task.Cost;
                    break;
                case TaskStatus.NotFound:
                case TaskStatus.SurfaceError:
                    realCost = task.Cost * 7 / 10;
                    break;
                default:
                    realCost = 0;
                    break;
            }
            if (realCost > 0)
            {
                var order = Db.Orders.Find(task.OrderId);
                order.Balance -= realCost;
                order.UpdatedAt = DateTime.Now;
                Db.Update(order);
                task.RealCost = realCost;
            }
            Db.Update(task);
            Db.SaveChanges();
            return new BaseResponse();
        }
    }

    public class CheckTaskRequest
    {
        public string Token { get; set; }
    }

    public class CheckTaskResponse : BaseResponse
    {
        public int Id { get; set; }
        public Platform Platform { get; set; }
        public string Keyword { get; set; }
        public string Domain { get; set; }
        public int[] Times { get; set; }
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
