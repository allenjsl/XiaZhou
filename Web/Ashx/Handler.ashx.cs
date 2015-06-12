using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EyouSoft.Common;
using System.Text;

namespace EyouSoft.Web.Ashx
{
    /// <summary>
    /// 公共处理程序
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Handler : IHttpHandler
    {
        #region attributes
        /// <summary>
        /// 公司编号
        /// </summary>
        string CompanyId = string.Empty;
        #endregion

        public void ProcessRequest(HttpContext context)
        {
            string dotype = Utils.GetQueryStringValue("dotype");

            var domain = EyouSoft.Security.Membership.UserProvider.GetDomain();
            if (domain == null) Utils.RCWE("");
            CompanyId = domain.CompanyId;

            switch (dotype)
            {
                case "GetHeTongHao": GetHeTongHao(); break;
                case "GetJiDiaoAnPaiFuDongNeiRong": GetJiDiaoAnPaiFuDongNeiRong(); break;
                case "DeleteGys": DeleteGys(); break;
                default: break;
            }
        }

        #region private members
        /// <summary>
        /// 获取合同号
        /// </summary>
        void GetHeTongHao()
        {
            StringBuilder s = new StringBuilder();
            var info = new EyouSoft.Model.ConStructure.MAutocompleteChaXunInfo();
            info.Length = 10;
            info.HeTongCode = Utils.GetQueryStringValue("q");
            info.HeTongId = Utils.GetQueryStringValue("yuanhetongid");
            info.LeiXing = null;
            info.Status = EyouSoft.Model.EnumType.ConStructure.ContractStatus.领用;

            var items = new EyouSoft.BLL.ConStructure.BContractNum().GetAutocompleteHeTongs(CompanyId, info);

            if (items == null || items.Count == 0) Utils.RCWE("未匹配到该合同号| ");

            foreach (var item in items)
            {
                s.Append(item.ContractCode + "|" + item.ContractId + "\n");
            }

            items = null;

            Utils.RCWE(s.ToString());
        }

        /// <summary>
        /// 获取计调安排浮动内容
        /// </summary>
        void GetJiDiaoAnPaiFuDongNeiRong()
        {
            string s = Utils.GetQueryStringValue("tourid");

            var items = new EyouSoft.BLL.PlanStructure.BPlan().GetJiDiaoAnPaiFuDongs(s, null);

            Utils.RCWE(Newtonsoft.Json.JsonConvert.SerializeObject(items));
        }

        /// <summary>
        /// 删除供应商信息
        /// </summary>
        void DeleteGys()
        {
            string s = Utils.GetQueryStringValue("deletegysids");
            if (string.IsNullOrEmpty(s)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请求异常！"));

            string[] items = s.Split(',');
            if (items == null || items.Length == 0) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请求异常！"));

            EyouSoft.Model.SSOStructure.MUserInfo uinfo = null;
            bool isLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out uinfo);

            if (!isLogin) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请求异常！"));

            int privs = -9;

            var gysLeXing = new EyouSoft.BLL.GysStructure.BGys().GetGysLeiXing(items[0]);
            if (!gysLeXing.HasValue) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请求异常！"));

            switch (gysLeXing.Value)
            {
                case EyouSoft.Model.EnumType.SourceStructure.SourceType.餐馆: privs = (int)EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_餐馆_删除; break;
                case EyouSoft.Model.EnumType.SourceStructure.SourceType.车队: privs = (int)EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_车队_删除; break;
                case EyouSoft.Model.EnumType.SourceStructure.SourceType.地接社: privs = (int)EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_地接社_删除; break;
                case EyouSoft.Model.EnumType.SourceStructure.SourceType.购物: privs = (int)EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_购物_删除; break;
                case EyouSoft.Model.EnumType.SourceStructure.SourceType.景点: privs = (int)EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_景点_删除; break;
                case EyouSoft.Model.EnumType.SourceStructure.SourceType.酒店: privs = (int)EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_酒店_删除; break;
                case EyouSoft.Model.EnumType.SourceStructure.SourceType.票务: privs = (int)EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_票务_删除; break;
                case EyouSoft.Model.EnumType.SourceStructure.SourceType.其他: privs = (int)EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_其它_删除; break;
                case EyouSoft.Model.EnumType.SourceStructure.SourceType.游轮: privs = (int)EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_游轮_删除; break;
                default: break;
            }

            if (privs==-9) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "请求异常！"));

            if (!EyouSoft.Security.Membership.UserProvider.IsGrant(uinfo.Privs, privs)) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "没有删除权限！"));

            int f1 = 0;
            int f2 = 0;

            var bll = new EyouSoft.BLL.GysStructure.BGys();

            foreach (var item in items)
            {
                if (bll.Delete(uinfo.CompanyId, item) == 1)
                {
                    f1++;
                }
                else
                {
                    f2++;
                }
            }

            bll = null;

            if (f1 > 0 && f2 == 0) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "删除成功！"));
            if (f1 > 0 && f2 > 0) Utils.RCWE(UtilsCommons.AjaxReturnJson("1", f1 + "个信息删除成功，" + f2 + "个信息不允许删除。"));
            if (f1 == 0 && f2 > 0) Utils.RCWE(UtilsCommons.AjaxReturnJson("0", f2 + "个信息不允许删除。"));

            Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "删除失败"));
        }
        #endregion

        #region IsReusable
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion
    }
}
