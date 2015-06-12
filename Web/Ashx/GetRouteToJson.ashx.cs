using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EyouSoft.Common;
using System.Text;

namespace Web.Ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// Create by DYZ 
    /// 获取线路信息，返回json
    /// </summary>
    public class GetRouteToJson : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string routeID = Utils.GetQueryStringValue("routeID");
            StringBuilder json = new StringBuilder();
            if (routeID != "")
            {
                EyouSoft.Model.SourceStructure.MRoute model = new EyouSoft.BLL.SourceStructure.BSource().GetRouteModel(routeID);
                if (model != null)
                {
                    #region 注释代码
                    //json.Append("{\"result\":\"1\",\"data\":{");

                    //json.Append("\"AdultPrice\":\"" + Utils.FilterEndOfTheZeroDecimal(model.AdultPrice) + "\",");
                    //json.Append("\"AreaId\":\"" + model.AreaId.ToString() + "\",");
                    //json.Append("\"ChildrenPrice\":\"" + Utils.FilterEndOfTheZeroDecimal(model.ChildrenPrice) + "\",");
                    //json.Append("\"CompanyId\":\"" + model.CompanyId + "\",");
                    //json.Append("\"Days\":\"" + model.Days.ToString() + "\",");
                    //json.Append("\"DepartureTraffic\":\"" + context.Server.HtmlEncode(model.DepartureTraffic.Trim()) + "\",");
                    //json.Append("\"IsTourOrSubentry\":\"" + model.IsTourOrSubentry.ToString().ToLower() + "\",");
                    //json.Append("\"LineIntro\":\"" + context.Server.HtmlEncode(model.LineIntro.Trim()) + "\",");
                    //json.Append("\"OperatorId\":\"" + model.OperatorId + "\",");
                    //json.Append("\"OtherPrice\":\"" + Utils.FilterEndOfTheZeroDecimal(model.OtherPrice) + "\",");
                    //json.Append("\"ReturnTraffic\":\"" + context.Server.HtmlEncode(model.ReturnTraffic.Trim()) + "\",");
                    //json.Append("\"Service\":\"" + context.Server.HtmlEncode(model.Service.Trim()) + "\",");
                    //json.Append("\"SetMode\":\"" + context.Server.HtmlEncode(model.SetMode.Trim()) + "\",");
                    //json.Append("\"TotalPrice\":\"" + Utils.FilterEndOfTheZeroDecimal(model.TotalPrice) + "\",");
                    //json.Append("\"TripAdvantage\":\"" + context.Server.HtmlEncode(model.TripAdvantage.Trim()) + "\",");

                    ////附件
                    //if (model.Attach != null)
                    //{
                    //    json.Append("\"Attach\":\"" + model.Attach.FilePath + "\",");
                    //}
                    //else
                    //{
                    //    json.Append("\"Attach\":\"\",");
                    //}

                    //#region 行程安排
                    //string planModelList = string.Empty;
                    //if (model.PlanModelList != null && model.PlanModelList.Count > 0)
                    //{
                    //    planModelList = "[";
                    //    for (int i = 0; i < model.PlanModelList.Count; i++)
                    //    {
                    //        planModelList += "{";
                    //        planModelList += "\"Breakfast\":\"" + model.PlanModelList[i].Breakfast.ToString().ToLower() + "\",";
                    //        planModelList += "\"Content\":\"" + context.Server.HtmlEncode(model.PlanModelList[i].Content.Trim()) + "\",";
                    //        planModelList += "\"Days\":\"" + model.PlanModelList[i].Days.ToString() + "\",";
                    //        planModelList += "\"Hotel\":\"" + context.Server.HtmlEncode(model.PlanModelList[i].Hotel.Trim()) + "\",";
                    //        planModelList += "\"Traffic\":\"" + context.Server.HtmlEncode(model.PlanModelList[i].Traffic.Trim()) + "\",";
                    //        planModelList += "\"Lunch\":\"" + model.PlanModelList[i].Lunch.ToString().ToLower() + "\",";
                    //        planModelList += "\"Section\":\"" + context.Server.HtmlEncode(model.PlanModelList[i].Section.Trim()) + "\",";
                    //        planModelList += "\"Supper\":\"" + model.PlanModelList[i].Supper.ToString().ToLower() + "\",";

                    //        #region 行程照片
                    //        if (model.PlanModelList[i].FilePath != "")
                    //        {
                    //            planModelList += "\"FilePath\":\"" + model.PlanModelList[i].FilePath + "\",";
                    //        }
                    //        else
                    //        {
                    //            planModelList += "\"FilePath\":\"\",";
                    //        }
                    //        #endregion

                    //        #region 行程景点
                    //        string planSpotModel = string.Empty;
                    //        if (model.PlanModelList[i].TourPlanSpot != null && model.PlanModelList[i].TourPlanSpot.Count > 0)
                    //        {
                    //            planSpotModel = "[";
                    //            for (int j = 0; j < model.PlanModelList[i].TourPlanSpot.Count; j++)
                    //            {
                    //                planSpotModel += "{";
                    //                planSpotModel += "\"SpotId\":\"" + model.PlanModelList[i].TourPlanSpot[j].SpotId + "\",";
                    //                planSpotModel += "\"SpotName\":\"" + context.Server.HtmlEncode(model.PlanModelList[i].TourPlanSpot[j].SpotName.Trim()) + "\"";
                    //                planSpotModel += "},";
                    //            }
                    //            planSpotModel = planSpotModel.Substring(0, planSpotModel.Length - 1);
                    //            planSpotModel += "]";
                    //        }
                    //        if (planSpotModel == "")
                    //        {
                    //            planModelList += "\"PlanSpotModel\":\"" + planSpotModel + "\"";
                    //        }
                    //        else
                    //        {
                    //            planModelList += "\"PlanSpotModel\":" + planSpotModel + "";
                    //        }
                    //        #endregion

                    //        planModelList += "},";
                    //    }
                    //    planModelList = planModelList.Substring(0, planModelList.Length - 1);
                    //    planModelList += "]";
                    //}
                    //else
                    //{
                    //    planModelList = "\"\"";
                    //}
                    //json.Append("\"PlanModelList\":" + planModelList + ",");
                    //#endregion

                    //#region  包含项目
                    //string standardModelList = string.Empty;
                    //if (model.StandardModelList != null && model.StandardModelList.Count > 0)
                    //{
                    //    standardModelList = "[";
                    //    for (int j = 0; j < model.StandardModelList.Count; j++)
                    //    {
                    //        standardModelList += "{";
                    //        standardModelList += "\"Standard\":\"" + context.Server.HtmlEncode(model.StandardModelList[j].Standard.Trim()) + "\",";
                    //        standardModelList += "\"Type\":\"" + ((int)model.StandardModelList[j].Type).ToString() + "\",";
                    //        standardModelList += "\"Unit\":\"" + ((int)model.StandardModelList[j].Unit).ToString() + "\",";
                    //        standardModelList += "\"UnitPrice\":\"" + Utils.FilterEndOfTheZeroDecimal(model.StandardModelList[j].UnitPrice) + "\"";
                    //        standardModelList += "},";
                    //    }
                    //    standardModelList = standardModelList.Substring(0, standardModelList.Length - 1);
                    //    standardModelList += "]";
                    //}
                    //else
                    //{
                    //    standardModelList = "\"\"";
                    //}
                    //json.Append("\"StandardModelList\":" + standardModelList + ",");
                    //#endregion

                    //#region 线路服务
                    //string servicesModel = string.Empty;
                    //if (model.ServicesModel != null)
                    //{
                    //    servicesModel = "{";
                    //    servicesModel += "\"OwnExpense\":\"" + context.Server.HtmlEncode(model.ServicesModel.OwnExpense.Trim()) + "\",";
                    //    servicesModel += "\"ChildServiceItem\":\"" + context.Server.HtmlEncode(model.ServicesModel.ChildServiceItem.Trim()) + "\",";
                    //    servicesModel += "\"NoNeedItem\":\"" + context.Server.HtmlEncode(model.ServicesModel.NoNeedItem.Trim()) + "\",";
                    //    servicesModel += "\"InsiderInfor\":\"" + context.Server.HtmlEncode(model.ServicesModel.InsiderInfor.Trim()) + "\",";
                    //    servicesModel += "\"NeedAttention\":\"" + context.Server.HtmlEncode(model.ServicesModel.NeedAttention.Trim()) + "\",";
                    //    servicesModel += "\"ShoppingItem\":\"" + context.Server.HtmlEncode(model.ServicesModel.ShoppingItem.Trim()) + "\",";
                    //    servicesModel += "\"WarmRemind\":\"" + context.Server.HtmlEncode(model.ServicesModel.WarmRemind.Trim()) + "\"";
                    //    servicesModel += "}";
                    //}
                    //json.Append("\"ServicesModel\":" + servicesModel + ",");
                    //#endregion

                    //#region 签证附件
                    //if (model.VisaInfoList != null && model.VisaInfoList.Count > 0)
                    //{
                    //    string visaStr = "[";
                    //    for (int i = 0; i < model.VisaInfoList.Count; i++)
                    //    {
                    //        visaStr += "{\"Name\":\"" + context.Server.HtmlEncode(model.VisaInfoList[i].Name.Trim()) + "\",\"FilePath\":\"" + model.VisaInfoList[i].FilePath + "\",\"Downloads\":\"" + model.VisaInfoList[i].Downloads.ToString() + "\"},";
                    //    }
                    //    if (visaStr.Length > 0)
                    //    {
                    //        visaStr = visaStr.Substring(0, visaStr.Length - 1);
                    //    }
                    //    visaStr += "]";
                    //    json.Append("\"VisaInfoList\":" + visaStr + "");
                    //}
                    //else
                    //{
                    //    json.Append("\"VisaInfoList\":\"\"");
                    //}
                    //#endregion

                    //json.Append("}}");
                    #endregion 
                    json = new StringBuilder("{\"result\":\"1\",\"data\":" + Newtonsoft.Json.JsonConvert.SerializeObject(model) + "}");
                }
            }
            if (json.ToString() == string.Empty)
            {
                context.Response.Write("{\"result\":\"0\",\"data\":\"\"}");
                return;
            }
            context.Response.Write(json);
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
