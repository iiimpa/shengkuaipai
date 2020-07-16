using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.RequestParams;
using YYApi.Communications;
using YYApi.Controllers;

namespace WebApi.Controllers
{
    public class TaskController : BaseController
    {
        public DataContext _db { get; set; }
        private IDistributedCache _redis { get; set; }
        private IConfiguration _config { get; set; }
        public TaskController(DataContext db, IDistributedCache redis, IConfiguration config)
        {
            _db = db;
            _redis = redis;
            _config = config;
        }

        /// <summary>
        /// 每日重置任务
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpPost("/task/reset")]
        public BaseResponse TaskReset([FromBody] TaskParams @params)
        {
            ErrorWhen(@params.token == _config["Token"], 400, "身份验证失败！");
            string time = DateTime.Today.ToString("yyyy-MM-dd 23:55:00");
            DateTime datetime = Convert.ToDateTime(time);
            if (DateTime.Now >= datetime && DateTime.Now < DateTime.Today.AddDays(1))
            {
                Models.Task[] tasks = _db.Tasks.Where(x => x.schedule_time < DateTime.Today.AddDays(1) && (x.status == Helpers.TaskStatus.Wait || x.status == Helpers.TaskStatus.Running)).ToArray();
                _db.RemoveRange(tasks);
                if (_db.SaveChanges() > 0)
                    return new BaseResponse() { Code = 200, Message = "删除成功！" };
                return new BaseResponse() { Code = 400, Message = "删除失败!" };
            }
            return new BaseResponse() { Code = 400, Message = "删除失败!" };
        }
    }
}