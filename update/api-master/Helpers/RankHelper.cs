using System;
using System.Net;
using System.Text.Json;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi
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
                return 100;
            }
            public int GetRank(string domain, string keyword)
            {
                if (Result.Ranks != null)
                {
                    foreach (var item in Result.Ranks)
                    {
                        if (item != null)
                        {
                            if (item.Title.Contains(keyword) && item.Url == domain)
                            {
                                var tmp = item.RankStr.Split("-");
                                if (tmp.Length == 2)
                                {
                                    return (int.Parse(tmp[0]) - 1) * 10 + int.Parse(tmp[1]);
                                }
                            }
                        }
                    }
                }
                return 100;
            }
        }

        public class PointResponse
        {
            public class Reobj
            {
                public string StartDate { get; set; }
                public string EndDate { get; set; }
                public string BaiduAll { get; set; }
                public string BaiduPc { get; set; }
                public string BaiduMobile { get; set; }
                public string SoPc { get; set; }
                public string SogouAll { get; set; }
                public string SogouPc { get; set; }
                public string SogouMobile { get; set; }
            }
            public int StateCode { get; set; }
            public string Reason { get; set; }
            public Reobj Result { get; set; }
        }
        public int GetPoint(PointResponse res, string type)
        {
            if (res == null)
            {
                return 0;
            }
            int now = DateTime.Now.Day;
            string[] array = null;
            if (type == "baidupc")
                array = res.Result.BaiduPc.Split(',');
            if (type == "baidumb")
                array = res.Result.BaiduMobile.Split(',');
            if (type == "sogoupc")
                array = res.Result.SogouPc.Split(',');
            if (type == "sogoumb")
                array = res.Result.SogouMobile.Split(',');
            if (type == "sopc")
                return Convert.ToInt32(res.Result.SoPc);
            try
            {
                return Convert.ToInt32(array[now - 1]);
            }
            catch
            {
                return Convert.ToInt32(array[0]);
            }
        }

        public PointResponse PointBaiduRank(string keyword)
        {
            var url = $"http://apidata.chinaz.com/CallAPI/BaiduIndex?key={Key}&keyword={keyword}";
            var Http = new WebClient();
            var resp = Http.DownloadString(url);
            try
            {
                return JsonSerializer.Deserialize<PointResponse>(resp);
            }
            catch
            {
                return null;
            }

        }

        public PointResponse PointSogouRank(string keyword)
        {
            var url = $"http://apidata.chinaz.com/CallAPI/SogouIndex?key={Key}&keyword={keyword}";
            var Http = new WebClient();
            var resp = Http.DownloadString(url);
            try
            {
                return JsonSerializer.Deserialize<PointResponse>(resp);
            }
            catch
            {
                return null;
            }
        }

        public PointResponse Point360Rank(string keyword)
        {
            var url = $"http://apidata.chinaz.com/CallAPI/SogouIndex?key={Key}&keyword={keyword}";
            var Http = new WebClient();
            var resp = Http.DownloadString(url);
            try
            {
                return JsonSerializer.Deserialize<PointResponse>(resp);
            }
            catch
            {
                return null;
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
            try
            {
                return JsonSerializer.Deserialize<ResultResponse>(resp);
            }
            catch
            {
                return null;
            }

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