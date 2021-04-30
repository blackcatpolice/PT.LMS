using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pagetechs.Framework.Wx.Pay.ViewModel;
using Pagetechs.Framework.Wx.Pay.WxPayAPI;
using PageTechsLMS.DataCore.Orders.Interfaces;
using PageTechsLMS.Service.Settings;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageTechsLMS.Service.Wx
{
    public class WxPayService
    {
        SettingService settingService;
        public WxPayService(SettingService _settingService)
        {
            settingService = _settingService;
            WxPayConfig.APPID = _settingService.WxPay.AppKey;
            WxPayConfig.APPSECRET = _settingService.WxPay.AppSecret;
            WxPayConfig.MCHID = _settingService.WxPay.MchId;
            WxPayConfig.KEY = _settingService.WxPay.Key;
        }

        public (string, string) CreateOrder(string openId, IOrderBase order, string notifyurl)
        {
            JsApiPay jsApiPay = new JsApiPay();
            var outTradeNo = WxPayApi.GenerateOutTradeNo();
            var orderDict = new Dictionary<string, string>();

            orderDict.Add("fee", order.Fee.ToString());
            orderDict.Add("name", order.Name);
            orderDict.Add("notify_url", notifyurl);
            orderDict.Add("openid", openId); 
            orderDict.Add("tag", order.Tag);
            orderDict.Add("desc", order.Desc);
            orderDict.Add("out_trade_no", outTradeNo);
            var wxPayData = jsApiPay.GetUnifiedOrderResult(orderDict);


            return (GetJsApiParameters(wxPayData), outTradeNo);
        }

        public (Stream, string) NativeCreateOrder(string openId, IOrderBase order, string notifyUrl)
        {
            NativePay nativePay = new NativePay();
            var outTradeNo = WxPayApi.GenerateOutTradeNo();

            var orderDict = new Dictionary<string, string>();
            orderDict.Add("fee", order.Fee.ToString());
            orderDict.Add("name", order.Name);
            orderDict.Add("notify_url", notifyUrl);
            orderDict.Add("tag", order.Tag ?? String.Empty);
            orderDict.Add("desc", order.Desc?? String.Empty);
            orderDict.Add("out_trade_no", outTradeNo);
            orderDict.Add("productId", order.Id.ToString());

            var payUrl = nativePay.GetPayUrl(orderDict);
            return (MakeQRCode(payUrl), outTradeNo);
        }

        /// <summary>
        /// 统一下下单接口, 返回发起支付的Package
        /// </summary>
        /// <param name="unifierViewModel"></param>
        /// <returns></returns>
        public string UnifiedOrder(UnifierOrderViewModel unifierViewModel)
        {
            var wxPayData = new WxPayData();
            wxPayData.SetValue("appid", WxPayConfig.APPID);
            wxPayData.SetValue("mch_id", WxPayConfig.MCHID);
            wxPayData.SetValue("body", unifierViewModel.Body);
            wxPayData.SetValue("attach", unifierViewModel.Attach);
            wxPayData.SetValue("out_trade_no", unifierViewModel.OutTradeNo);
            wxPayData.SetValue("total_fee", unifierViewModel.TotalFee);
            wxPayData.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));
            wxPayData.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
            wxPayData.SetValue("goods_tag", unifierViewModel.Tag);
            wxPayData.SetValue("trade_type", "JSAPI");
            wxPayData.SetValue("openid", unifierViewModel.OpenId);

            var result = WxPayApi.UnifiedOrder(wxPayData, unifierViewModel.NotifyUrl);

            var jsonResult = GetJsApiParameters(result);

            return jsonResult;
        }

        public string GetJsApiParameters(WxPayData unifiedOrderResult)
        {

            WxPayData jsApiParam = new WxPayData();
            jsApiParam.SetValue("appId", unifiedOrderResult.GetValue("appid"));
            jsApiParam.SetValue("timeStamp", WxPayApi.GenerateTimeStamp());
            jsApiParam.SetValue("nonceStr", WxPayApi.GenerateNonceStr());
            jsApiParam.SetValue("package", "prepay_id=" + unifiedOrderResult.GetValue("prepay_id"));
            jsApiParam.SetValue("signType", "MD5");
            jsApiParam.SetValue("paySign", jsApiParam.MakeSign());

            string parameters = jsApiParam.ToJson();

            return parameters;
        }

        public string GenOutTradeNo()
        {
            return WxPayApi.GenerateOutTradeNo();
        }

        public string ProcessNotify(Stream IntpuStream, out WxPayData notifyData)
        {
            var notifyProcessor = new ResultNotify();
            //WxPayData notifyData;
            var responseStr = notifyProcessor.ProcessNotify(IntpuStream, out notifyData);
            return responseStr;
        }

        public Stream MakeQRCode(string str)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();

            QRCodeData qrCodeData = qrGenerator.CreateQrCode(str, QRCodeGenerator.ECCLevel.L);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(30);
            //qrCodeImage.Save(AppDomain.CurrentDomain.BaseDirectory + "testqrcode1.jpg");
            //保存为PNG到内存流  
            MemoryStream ms = new MemoryStream();
            qrCodeImage.Save(ms, ImageFormat.Jpeg);
            //var bytes = new byte[ms.Length];

            //File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "testqrcode3.jpg", bytes);
            return ms;
            ////输出二维码图片
            //Response.BinaryWrite(ms.GetBuffer());
            //Response.End();
        }
    }
}
