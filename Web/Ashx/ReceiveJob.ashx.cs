using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EyouSoft.Common;

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
            string type = Utils.GetQueryStringValue("type");
            //团号
            string tourID = Utils.GetQueryStringValue("tourId");
            //公司id
            string comID = Utils.GetQueryStringValue("com");
            //操作人
            string Operator = Utils.GetQueryStringValue("Operator");
            //操作人id
            string OperatorID = Utils.GetQueryStringValue("OperatorID");
            //操作人部门id
            string OperatDepID = Utils.GetQueryStringValue("OperatDepID");
            if (type == "receive")
            {
                if (!string.IsNullOrEmpty(tourID) && !string.IsNullOrEmpty(comID) && !string.IsNullOrEmpty(Operator) && !string.IsNullOrEmpty(OperatorID) && !string.IsNullOrEmpty(OperatDepID))
                {
                    EyouSoft.Model.TourStructure.MTourStatusChange TourStatusChange = new EyouSoft.Model.TourStructure.MTourStatusChange();
                    TourStatusChange.CompanyId = comID;
                    TourStatusChange.TourId = tourID;
                    TourStatusChange.TourStatus = EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置;
                    TourStatusChange.Operator = Operator;
                    TourStatusChange.OperatorId = OperatorID;
                    TourStatusChange.DeptId = Utils.GetInt(OperatDepID);
                    bool result = new EyouSoft.BLL.TourStructure.BTour().UpdateTourStatus(TourStatusChange);
                    if (result)
                    {
                        context.Response.Write("{\"result\":\"" + result + "\",\"msg\":\"接收成功!\"}");
                    }
                    else
                    {
                        context.Response.Write("{\"result\":\"" + result + "\",\"msg\":\"接收失败!!\"}");
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
