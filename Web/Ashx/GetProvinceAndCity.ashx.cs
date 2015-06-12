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
    /// </summary>

    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：戴银柱
    /// 创建时间：2011-9-20
    /// 说明：处理国家，省份，城市，县区
    public class GetProvinceAndCity : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string getType = Utils.GetQueryStringValue("get");

            StringBuilder sb = new StringBuilder();

            int gID = Utils.GetInt(Utils.GetQueryStringValue("gid"), 1);
            int pID = Utils.GetInt(Utils.GetQueryStringValue("pid"));
            int cID = Utils.GetInt(Utils.GetQueryStringValue("cid"));
            int xID = Utils.GetInt(Utils.GetQueryStringValue("xid"));
            string companyID = Utils.GetQueryStringValue("companyID");
            EyouSoft.BLL.ComStructure.BComCity bll = new EyouSoft.BLL.ComStructure.BComCity();

            switch (getType)
            {

                case "g":

                    IList<EyouSoft.Model.SysStructure.MSysCountry> gList = new EyouSoft.BLL.ComStructure.BComCity().GetGuoJias(companyID);
                    if (gList != null && gList.Count > 0)
                    {
                        sb.Append("{\"list\":[");
                        for (int i = 0; i < gList.Count; i++)
                        {
                            sb.Append("{\"id\":\"" + gList[i].CountryId.ToString() + "\",\"name\":\"" + gList[i].Name + "\"},");
                        }
                        if (sb.Length > 1)
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }
                        sb.Append("]}");
                    }
                    else {
                        sb.Append("{\"list\":[]}");
                    }
                    break;
                case "p":
                    IList<EyouSoft.Model.SysStructure.MSysProvince> pList = bll.GetProvince(gID, companyID);
                    if (pList != null && pList.Count > 0)
                    {
                        sb.Append("{\"list\":[");
                        for (int i = 0; i < pList.Count; i++)
                        {
                            sb.Append("{\"id\":\"" + pList[i].ProvinceId.ToString() + "\",\"name\":\"" + pList[i].Name + "\"},");
                        }
                        if (sb.Length > 1)
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }
                        sb.Append("]}");
                    }
                    else
                    {
                        sb.Append("{\"list\":[]}");
                    }

                    break;
                case "c":
                    IList<EyouSoft.Model.SysStructure.MSysCity> cList = bll.GetCity(pID, companyID);
                    if (cList != null && cList.Count > 0)
                    {
                        sb.Append("{\"list\":[");
                        for (int i = 0; i < cList.Count; i++)
                        {
                            sb.Append("{\"id\":\"" + cList[i].CityId.ToString() + "\",\"name\":\"" + cList[i].Name + "\"},");
                        }
                        if (sb.Length > 1)
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }
                        sb.Append("]}");
                    }
                    else
                    {
                        sb.Append("{\"list\":[]}");
                    }
                    break;
                case "x":
                    IList<EyouSoft.Model.SysStructure.MSysDistrict> xList = bll.GetDistrict(cID, companyID);
                    if (xList != null && xList.Count > 0)
                    {
                        sb.Append("{\"list\":[");
                        for (int i = 0; i < xList.Count; i++)
                        {
                            sb.Append("{\"id\":\"" + xList[i].DistrictId.ToString() + "\",\"name\":\"" + xList[i].Name + "\"},");
                        }
                        if (sb.Length > 1)
                        {
                            sb.Remove(sb.Length - 1, 1);
                        }
                        sb.Append("]}");
                    }
                    else
                    {
                        sb.Append("{\"list\":[]}");
                    }
                    break;
            }

            context.Response.Write(sb.ToString());
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
