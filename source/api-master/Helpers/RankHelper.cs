using System;
using System.Net;
using System.Text.Json;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Helpers
{
    /// <summary>
    /// 获取排名信息
    /// </summary>
    public class RankHelper
    {
        private string Key { get; }
        public RankHelper(string key)
        {
            Key = key;
        }

        /// <summary>
        /// 查询结果
        /// </summary>
        public class ResultResponse
        {
            public class ResultItem
            {
                public class RankItem
                {
                    public string RankStr { get; set; }
                    public string Title { get; set; }
                    public string Url { get; set; }
                }
                public string CollectCount { get; set; }
                public string CrawlTime { get; set; }
                public RankItem[] Ranks { get; set; }
            }
            public int StateCode { get; set; }
            public string Reason { get; set; }
            public ResultItem Result { get; set; }

            /// <summary>
            /// 解析结果中的排名
            /// </summary>
            /// <returns></returns>
            public int GetRank()
            {
                if (Result.Ranks != null)
                {
                    var item = Result.Ranks[0];
                    if (item != null)
                    {
                        var tmp = item.RankStr.Split("-");
                        if (tmp.Length == 2)
                        {
                            return (int.Parse(tmp[0]) - 1) * 10 + int.Parse(tmp[1]);
                        }
                    }
                }
                return 0;
            }
        }

        /// <summary>
        /// PC搜狗
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ResultResponse QueryPcSogouRank(string keyword, string domain)
        {
            var url = $"http://apidata.chinaz.com/CallAPI/SogouPcRanking?key={Key}&domainName={domain}&keyword={keyword}";
            var Http = new WebClient();
            var resp = Http.DownloadString(url);

            return JsonSerializer.Deserialize<ResultResponse>(resp);
        }

        /// <summary>
        /// 移动搜狗
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ResultResponse QueryMobileSogouRank(string keyword, string domain)
        {
            var url = $"http://apidata.chinaz.com/CallAPI/SogouMobileRanking?key={Key}&domainName={domain}&keyword={keyword}";
            var Http = new WebClient();
            var resp = Http.DownloadString(url);
            return JsonSerializer.Deserialize<ResultResponse>(resp);
        }

        /// <summary>
        /// PC百度
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ResultResponse QueryPcBaiduRank(string keyword, string domain)
        {
            var url = $"http://apidata.chinaz.com/CallAPI/BaiduPcRanking?key={Key}&domainName={domain}&keyword={keyword}";
            var Http = new WebClient();
            var resp = Http.DownloadString(url);
            return JsonSerializer.Deserialize<ResultResponse>(resp);
        }

        /// <summary>
        /// 移动百度
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ResultResponse QueryMobileBaiduRank(string keyword, string domain)
        {
            var url = $"http://apidata.chinaz.com/CallAPI/BaiduMobileRanking?key={Key}&domainName={domain}&keyword={keyword}";
            var Http = new WebClient();
            var resp = Http.DownloadString(url);
            return JsonSerializer.Deserialize<ResultResponse>(resp);
        }

        /// <summary>
        /// PC360
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ResultResponse QueryPc360Rank(string keyword, string domain)
        {
            var url = $"http://apidata.chinaz.com/CallAPI/SoPcRanking?key={Key}&domainName={domain}&keyword={keyword}";
            var Http = new WebClient();
            var resp = Http.DownloadString(url);
            return JsonSerializer.Deserialize<ResultResponse>(resp);
        }

        /// <summary>
        /// 移动360
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public ResultResponse QueryMobile360Rank(string keyword, string domain)
        {
            var url = $"http://apidata.chinaz.com/CallAPI/SoMobileRanking?key={Key}&domainName={domain}&keyword={keyword}";
            var Http = new WebClient();
            var resp = Http.DownloadString(url);
            return JsonSerializer.Deserialize<ResultResponse>(resp);
        }
    }


    /// <summary>
    ///  添加到IOC中
    /// </summary>
    public static class RankHelperExtensions
    {
        public static IServiceCollection AddRankHelper(this IServiceCollection builder, string key)
        {
            return builder.AddSingleton(new RankHelper(key));
        }
    }
}