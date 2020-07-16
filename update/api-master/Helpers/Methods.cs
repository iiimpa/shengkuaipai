using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Helpers
{
    public class Methods
    {
        public string DictToJson(Dictionary<string, int> @params)
        {
            try
            {
                string json = "{";
                foreach (var item in @params)
                {
                    json += $"\"{item.Key}\":\"{item.Value}\",";
                }
                json = json.Remove(json.LastIndexOf(','), 1);
                json += "}";
                return json;
            }
            catch
            {
                return "";
            }
        }
        public string[] StringToArray(string @params)
        {
            return @params.Split(',');
        }
        public int[] StringToIntArray(string @params)
        {
            string[] data = @params.Split(',');
            int[] array = new int[data.Length];
            int index = 0;
            foreach (string item in data)
            {
                array[index] = Convert.ToInt32(item);
                index++;
            }
            return array;
        }
        public Dictionary<string, int> StringToDict(string @params)
        {
            return @params.Trim(new char[] { '{', '}' }).Split(',').ToDictionary(s => s.Split(':')[0].TrimStart('"').TrimEnd('"'), s => (int)Convert.ToInt32(s.Split(':')[1].TrimStart('"').TrimEnd('"')));
        }
        public Dictionary<string, string> StringToDictStr(string @params)
        {
            return @params.Trim(new char[] { '{', '}' }).Split(',').ToDictionary(s => s.Split(':')[0].TrimStart('"').TrimEnd('"'), s => s.Split(':')[1].TrimStart('"').TrimEnd('"'));
        }
        public T DicToObj<T>(Dictionary<string, int> dic) where T : new()
        {
            var result = new T();
            foreach (var d in dic)
            {
                var filed = d.Key;
                try
                {
                    var value = d.Value;
                    result.GetType().GetProperty(filed).SetValue(result, value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return result;
        }
        public string ArrayToStr(int[] @params)
        {
            string result = "";
            foreach (var item in @params)
            {
                result += item.ToString() + ",";
            }
            result = result.TrimEnd(',');
            return result;
        }
        public string[] Division(string @params)
        {
            string[] striparr = @params.Split(new string[] { "\n" }, StringSplitOptions.None);
            striparr = striparr.Where(s => !string.IsNullOrEmpty(s)).ToArray();
            return striparr;
        }
        public bool isTimes(Dictionary<string, int> times)
        {
            foreach (var item in times)
            {
                if (item.Value != 0)
                    return true;
            }
            return false;
        }
        public bool isRange(Dictionary<string, int> @params)
        {
            foreach (var item in @params)
            {
                if (item.Value != 0)
                    return true;
            }
            return false;
        }
        public int DateDiff(DateTime dateStart, DateTime dateEnd)
        {
            DateTime start = Convert.ToDateTime(dateStart.ToShortDateString());
            DateTime end = Convert.ToDateTime(dateEnd.ToShortDateString());
            TimeSpan sp = end.Subtract(start);
            return sp.Days;
        }
        public RankResponse GetRank(RankHelper rankhelper, Models.Order order)
        {
            int point = 100;
            int rank = 100;
            if (order.platform == Platform.Baidu)
            {
                point = rankhelper.GetPoint(rankhelper.PointBaiduRank(order.keyword), "baidupc");
                try
                {
                    rank = rankhelper.QueryPcBaiduRank(order.keyword, order.domain).GetRank();
                }
                catch
                {
                    rank = 100;
                }

            }
            if (order.platform == Platform.MBaidu)
            {
                point = rankhelper.GetPoint(rankhelper.PointBaiduRank(order.keyword), "baidumb");
                try
                {
                    rank = rankhelper.QueryMobileBaiduRank(order.keyword, order.domain).GetRank();
                }
                catch
                {
                    rank = 100;
                }

            }
            if (order.platform == Platform.Pc360)
            {
                point = rankhelper.GetPoint(rankhelper.PointBaiduRank(order.keyword), "sopc");
                try
                {
                    rank = rankhelper.QueryPc360Rank(order.keyword, order.domain).GetRank();
                }
                catch
                {
                    rank = 100;
                }

            }
            if (order.platform == Platform.Sogou)
            {
                point = rankhelper.GetPoint(rankhelper.PointBaiduRank(order.keyword), "sogoupc");
                try
                {
                    rank = rankhelper.QueryPcSogouRank(order.keyword, order.domain).GetRank();
                }
                catch
                {
                    rank = 100;
                }
            }
            if (order.platform == Platform.MSogou)
            {
                point = rankhelper.GetPoint(rankhelper.PointBaiduRank(order.keyword), "sogoumb");
                try
                {
                    rank = rankhelper.QueryMobileSogouRank(order.keyword, order.domain).GetRank();
                }
                catch
                {
                    rank = 100;
                }
            }
            return new RankResponse() { rank = rank, point = point };
        }
    }
}
