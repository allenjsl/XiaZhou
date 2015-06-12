using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyouSoft.Common;

namespace Web.Ashx
{
    /// <summary>
    /// 获取员工
    /// </summary>
    /// 创建人：徐从栎
    /// 创建时间：2011-9-19
    public class GetPeople : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string q = Utils.GetQueryStringValue("q");
            context.Response.ContentType = "text/plain";
            context.Response.Write("张三|1\n李四|2\n王五|3\n");
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
