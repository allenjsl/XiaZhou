using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyouSoft.Common.ValidateNumberAndChar;
using EyouSoft.Common;
using System.Web.Services;

namespace Web.Ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    /// 
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2011-9-26
    /// 说明： 获取联系人信息
    public class Contact : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string q = Utils.GetQueryStringValue("name");
            context.Response.ContentType = "text/plain";
            context.Response.Write("联系人信息");
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
