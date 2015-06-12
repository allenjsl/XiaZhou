﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Web.Ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ReceiveJob : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";           
            string type = EyouSoft.Common.Utils.GetQueryStringValue("type");
            string Id=EyouSoft.Common.Utils.GetQueryStringValue("Id");
            if (type == "receive")
            {
                if (Id != "" && !string.IsNullOrEmpty(Id))
                {
                    bool result = new EyouSoft.BLL.TourStructure.BTour().UpdateTourStatus(Id, EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置);
                    if (result)
                    {
                        context.Response.Write("接收成功!");
                    }
                    else
                    {
                        context.Response.Write("接收失败!");
                    }
                }
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
