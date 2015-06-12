using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Web.Ashx
{
    /// <summary>
    /// 团队状态设置
    /// </summary>
    /// 创建人:柴逸宁
    /// 创建时间:2012-3-16
    /// 参数:
    /// tourID     团号
    /// intStatus  团队状态(枚举)
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class TourStatusSet : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
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
