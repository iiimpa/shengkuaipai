using System;
using Community;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using YYApi.Communications;
using System.Linq;
using YYApi.Helpers;
using System.Collections.Generic;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    public class PublicController
    {
        private DataContext Db { get; }
        private Alipay Alipay { get; }

        public PublicController(DataContext db, Alipay alipay)
        {
            Alipay = alipay;
            Db = db;
        }

        /// <summary>
        /// 获取知识分类
        /// </summary>
        /// <returns></returns>
        [HttpPost("/public/kn/categories"), ApiDoc("前台", "获取知识分类")]
        public BaseResponse<string[]> KnowledgeCategories()
        {
            return new BaseResponse<string[]>
            {
                Data = Db.Knowledges.Select(x => x.Category).Distinct().ToArray()
            };
        }

        /// <summary>
        /// 获取知识列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/public/kn/list"), ApiDoc("前台", "获取知识列表")]
        public BaseResponse<Knowledge[]> KnowledgeList([FromBody]PublicKnowledgeListRequest request)
        {
            return new BaseResponse<Knowledge[]>
            {
                Data = Db.Knowledges.Where(x => x.Category == request.Category).ToArray()
            };
        }

        /// <summary>
        /// 获取知识详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/public/kn/detail"), ApiDoc("前台", "获取知识详情")]
        public BaseResponse<Knowledge> KnowledgeDetail([FromBody]PublicKnowledgeDetailRequest request)
        {
            return new BaseResponse<Knowledge>
            {
                Data = Db.Knowledges.Find(request.Id)
            };
        }

        /// <summary>
        /// 获取问答列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("/public/qas"), ApiDoc("前台", "获取问答列表")]
        public BaseResponse<Question[]> QAList()
        {
            return new BaseResponse<Question[]>
            {
                Data = Db.Questions.ToArray()
            };
        }

        /// <summary>
        /// 获取案例列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("/public/cases"), ApiDoc("前台", "获取案例列表")]
        public BaseResponse<Case[]> CaseList()
        {
            return new BaseResponse<Case[]>
            {
                Data = Db.Cases.ToArray()
            };
        }


        /// <summary>
        /// 获取首页轮播图
        /// </summary>
        /// <returns></returns>
        [HttpPost("/public/carousel"), ApiDoc("前台", "获取首页轮播图")]
        public BaseResponse<Carousel[]> CarouselList()
        {
            return new BaseResponse<Carousel[]>
            {
                Data = Db.Carousels.ToArray()
            };
        }

        [HttpPost("/public/friendlink"), ApiDoc("前台", "获取友情链接")]
        public BaseResponse<FriendLink[]> FriendLink()
        {
            return new BaseResponse<FriendLink[]>
            {
                Data = Db.FriendLinks.ToArray()
            };
        }

        /// <summary>
        /// 支付宝回调
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("/public/payback/alipay"), ApiDoc("支付", "支付宝异步通知")]
        public string AlipayNotify([FromForm]Dictionary<string, string> request)
        {
            if (Alipay.SignCheck(request))
            {

                var trade = Db.Trades.Where(x => x.TradeNo == request["out_trade_no"] && x.Status == 0).FirstOrDefault();
                if (trade != null)
                {
                    //获取设置
                    var upAgentTotal = int.Parse(Db.Configs.Where(x => x.Key == "upAgentTotal").Select(x => x.Value).FirstOrDefault());
                    var upRate = int.Parse(Db.Configs.Where(x => x.Key == "upRate").Select(x => x.Value).FirstOrDefault());
                    var upupRate = int.Parse(Db.Configs.Where(x => x.Key == "upupRate").Select(x => x.Value).FirstOrDefault());
                    var upupupRate = int.Parse(Db.Configs.Where(x => x.Key == "upupupRate").Select(x => x.Value).FirstOrDefault());
                    //更新订单状态
                    trade.Status = 1;
                    trade.UpdatedAt = DateTime.Now;
                    Db.Update(trade);
                    //给用户加钱
                    var u = Db.Users.Find(trade.UserId);
                    u.TotalRecharge += trade.Amount;
                    u.Coin += trade.Coin;
                    u.UpdatedAt = DateTime.Now;
                    if (u.TotalRecharge > upAgentTotal)
                    {
                        u.IsAgent = 1;
                    }
                    Db.Update(u);

                    //给用户的上级加钱
                    var uu = Db.Users.Find(u.Pid);
                    if (uu != null)
                    {
                        var uut = new Trade
                        {
                            Amount = trade.Amount * (upRate / 100),
                            UserId = uu.Id,
                            Type = TradeTypeEnum.Commission,
                            RelationId = u.Id,
                            Status = 1
                        };
                        Db.Add(uut);
                        uu.Balance += uut.Amount;
                        Db.Update(uu);

                        //给用户的上上级加钱
                        var uuu = Db.Users.Find(uu.Pid);
                        if (uuu != null)
                        {
                            var uuut = new Trade
                            {
                                Amount = trade.Amount * (upupRate / 100),
                                UserId = uuu.Id,
                                Type = TradeTypeEnum.Commission,
                                RelationId = u.Id,
                                Status = 1
                            };
                            Db.Add(uuut);
                            uuu.Balance += uuut.Amount;
                            Db.Update(uuu);

                            //给用户的上上上级加钱
                            var uuuu = Db.Users.Find(uuu.Pid);
                            if (uuuu != null)
                            {
                                var uuuut = new Trade
                                {
                                    Amount = trade.Amount * (upupupRate / 100),
                                    UserId = uuuu.Id,
                                    Type = TradeTypeEnum.Commission,
                                    RelationId = u.Id,
                                    Status = 1
                                };
                                Db.Add(uuuut);
                                uuuu.Balance += uuuut.Amount;
                                Db.Update(uuuu);
                            }
                        }
                    }
                    Db.SaveChanges();
                    return "success";
                }
            }
            return "failed";
        }


    }
}
