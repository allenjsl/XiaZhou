using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.Ashx
{
    /// <summary>
    /// 返回图片数据
    /// </summary>
    public class GetImg : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            string filepath = Utils.GetQueryStringValue("f");
            if (!String.IsNullOrEmpty(filepath))
                filepath = HttpContext.Current.Server.UrlDecode(filepath);
            int width = Utils.GetInt(Utils.GetQueryStringValue("w"));
            int height = Utils.GetInt(Utils.GetQueryStringValue("h"));
            byte[] imgbyte = TuPian.ImgInfo(filepath, width, height);
            if (imgbyte != null)
            {
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ContentType = "image/Jpeg";
                HttpContext.Current.Response.AddHeader("Content-Length", imgbyte.Length.ToString());
                HttpContext.Current.Response.BinaryWrite(imgbyte);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
