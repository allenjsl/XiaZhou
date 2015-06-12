using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyouSoft.Common;

namespace Web.Ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>


    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：戴银柱
    /// 创建时间：2011-9-20
    /// 说明：处理销售员，计调员，员工等输入匹配
    public class GetOrderSells : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string q = Utils.GetQueryStringValue("q");
            context.Response.ContentType = "text/plain";
            context.Response.Write("张三|1\n李四|2\n");
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
