using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using System.Collections.Generic;
using EyouSoft.Model.SourceStructure;

namespace Web.ResourceManage.Ground
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2012-3-16
    /// 说明: 资源管理： 地接社-分配帐号
    public partial class Account : BackPage
    {
        #region attributes
        /// <summary>
        /// 供应商编号
        /// </summary>
        string GysId = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            GysId = Utils.GetQueryStringValue("sourceid");

            if (string.IsNullOrEmpty(GysId)) EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", "请求异常!");

            #region 处理AJAX请求
            string dotype = Utils.GetQueryStringValue("type");

            switch (dotype)
            {
                case "save": FenPei(); break;
                default: break;
            }
            #endregion

            InitInfo();

        }

        /// <summary>
        /// 初始化供应商账号信息
        /// </summary>
        private void InitInfo()
        {
            if (string.IsNullOrEmpty(GysId)) return;
            
            var info = new EyouSoft.BLL.GysStructure.BGys().GetGysUserInfo(GysId);
            if (info == null) return;

            this.txtAccount.ReadOnly = true;
            this.txtAccount.Attributes["style"] = "background-color: #dadada";
            txtAccount.Text = info.Username;
            txtPwd.Attributes.Add("value", info.Pwd);
            txtEdit.Value = "1";

            string areaid = string.Empty;
            string areaname = string.Empty;

            if (info.Areas != null && info.Areas.Count > 0)
            {
                foreach (var item in info.Areas)
                {
                    areaid += item.AreaId + ",";
                    areaname += item.AreaName + ",";
                }

                if (!string.IsNullOrEmpty(areaid)) areaid = areaid.TrimEnd(',');
                if (!string.IsNullOrEmpty(areaname)) areaname = areaname.TrimEnd(',');
            }

            LineAreaSelect1.LineAreaSelectID = areaid;
            LineAreaSelect1.LineAreaSelectName = areaname;
        }

        /// <summary>
        /// 分配帐号
        /// </summary>
        /// <returns></returns>
        void FenPei()
        {
            var info = new EyouSoft.BLL.GysStructure.BGys().GetGysUserInfo(GysId);
            bool IsAdd = info == null;
            string account = Utils.GetFormValue(this.txtAccount.UniqueID);
            string pwd = Utils.GetFormValue(this.txtPwd.UniqueID);
            if (string.IsNullOrEmpty(account))
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "帐号不能为空!"));
            }
            if (string.IsNullOrEmpty(pwd))
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "密码不能为空!"));
            }
            IList<EyouSoft.Model.SourceStructure.MSourceTravelRouteArea> TravelRouteArea = new List<EyouSoft.Model.SourceStructure.MSourceTravelRouteArea>();
            string[] routeAreaList = Utils.GetFormValue(this.LineAreaSelect1.LineAreaSelectIDClient).Split(',');
            string[] routeAreaName = Utils.GetFormValue(this.LineAreaSelect1.LineAreaSelectNameClient).Split(',');
            if (routeAreaName.Length > 0 && routeAreaList.Length > 0)
            {
                for (int i = 0; i < routeAreaList.Length; i++)
                {
                    TravelRouteArea.Add(new EyouSoft.Model.SourceStructure.MSourceTravelRouteArea { RouteAreaId = Utils.GetInt(routeAreaList[i].ToString()), SourceId = GysId, RouteAreaName = routeAreaName[i].ToString().Trim() });
                }
            }

            EyouSoft.BLL.SourceStructure.BSource bll = new EyouSoft.BLL.SourceStructure.BSource();
            int result = 0;
            if (IsAdd)
            {
                result = bll.AddAccountToTravel(GysId, this.SiteUserInfo.CompanyId, account, pwd, this.SiteUserInfo.UserId, this.SiteUserInfo.Username, this.SiteUserInfo.DeptId, TravelRouteArea);
            }
            else
            {
                result = bll.UpdateTravelAccountStatus(GysId, EyouSoft.Model.EnumType.ComStructure.UserStatus.正常, pwd, TravelRouteArea, true);
            }

            if (result == 1)
            {
                if (IsAdd)
                {
                    RCWE(EyouSoft.Common.UtilsCommons.AjaxReturnJson("1", "分配成功"));
                }
                else
                {
                    RCWE(EyouSoft.Common.UtilsCommons.AjaxReturnJson("1", "修改成功"));
                }
            }
            else if (result == -1)
            {
                RCWE(EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", "此帐号已存在,请重新分配!"));
            }
            else
            {
                RCWE(EyouSoft.Common.UtilsCommons.AjaxReturnJson("0", "分配失败，请重试!"));
            }
        }

        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
    }
}
