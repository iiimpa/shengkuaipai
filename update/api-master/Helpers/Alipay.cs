using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using Aop.Api.Util;
using WebApi.Models;

namespace WebApi.Helpers
{
    public class Alipay
    {
        private DefaultAopClient Client { get; }
        private readonly string Gateway = "https://openapi.alipay.com/gateway.do";
        private string PublicKey { get; }
        private string NotifyUrl { get; }

        public Alipay(string appId, string privKey, string pubKey, string notifyUrl)
        {
            Client = new DefaultAopClient(Gateway, appId, privKey, "json", "1.0", "RSA2", pubKey);
            NotifyUrl = notifyUrl;
            PublicKey = pubKey;
        }

        public string Pay(string oid, double amount, string returnUrl, string subject = "应用充值", string body = "应用充值")
        {
            var model = new AlipayTradePagePayModel
            {
                Body = body,
                Subject = subject,
                TotalAmount = amount.ToString(),
                OutTradeNo = oid,
                ProductCode = "FAST_INSTANT_TRADE_PAY"
            };
            Console.WriteLine(JsonSerializer.Serialize(model));
            var request = new AlipayTradePagePayRequest();
            request.SetReturnUrl(returnUrl);
            request.SetNotifyUrl(NotifyUrl);
            request.SetBizModel(model);
            return Gateway + "?" + Client.SdkExecute(request).Body;
        }

        public bool SignCheck(Dictionary<string, string> data)
        {
            return AlipaySignature.RSACheckV1(data, PublicKey, "UTF-8", "RSA2", false);
        }
    }
}
