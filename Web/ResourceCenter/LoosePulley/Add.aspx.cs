using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.SourceStructure;
using EyouSoft.Model.EnumType.SourceStructure;
using EyouSoft.Model.EnumType.PlanStructure;
using EyouSoft.BLL.SourceStructure;
using EyouSoft.Common.Page;
using System.Text;
using EyouSoft.Model.EnumType.PrivsStructure;


namespace Web.ResourceCenter.LoosePulley
{
    /// <summary>
    /// 游轮预控-添加-
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-7
    public partial class Add : BackPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            #region ajax操作

            string doType = Utils.GetQueryStringValue("doType");
            if (doType.Length > 0)
            {
                Ajax(doType);
            }

            #endregion
            //权限验证
            PowerControl();

            //根据ID初始化页面
            PageInit();
        }

        /// <summary>
        /// Ajax操作
        /// </summary>
        /// <param name="doType">操作类型</param>
        private void Ajax(string doType)
        {
            switch (doType)
            {
                case "GetShipCompany":
                    GetShipCompany();
                    break;
                case "Save":
                    Save();
                    break;
            }
        }
        /// <summary>
        /// 酒店房型下拉
        /// </summary>
        private void GetShipCompany()
        {
            MSourceShip model = new BSource().GetShipModel(Utils.GetQueryStringValue("sourceId"));
            if (model != null && model.SubShipList != null && model.SubShipList.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<option value=-1>-请选择-</option>");
                foreach (MSourceSubShip item in model.SubShipList)
                {
                    sb.Append("<option value=" + item.SubId + ">" + item.ShipName + "</option>");
                }
                AjaxResponse(sb.ToString());
            }
            AjaxResponse("<option value=-1>-无可选择房型-</option>");
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit()
        {
            ControlType1.SourceControlCategory = SourceControlCategory.游轮;
            SupplierControl1.CallBack = "AddPage.SupplierControlCollBack";
            SupplierControl1.SupplierType = PlanProject.国内游轮;

        }

        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!CheckGrant(Privs.资源预控_游船预控_新增))
            {
                Utils.ResponseNoPermit(Privs.资源预控_游船预控_新增, true);
                return;
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

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            MSourceSueShip model = new MSourceSueShip();
            string msg = string.Empty;
            if (GetPageVal(model, ref msg))
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson(new BSourceControl().AddSueShip(model) ? "1" : "-1", "添加失败!"));
            }
            else
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("-1", msg));
            }
        }
        /// <summary>
        /// 获取页面数据
        /// </summary>
        private bool GetPageVal(MSourceSueShip model, ref string msg)
        {
            #region 实体赋值
            model.DeptId = SiteUserInfo.DeptId;
            //公司编号
            model.CompanyId = CurrentUserCompanyID;
            //操作时间
            model.IssueTime = DateTime.Now;
            //操作员
            model.OperatorId = SiteUserInfo.UserId;
            //预控数量
            model.ControlNum = Utils.GetInt(Utils.GetFormValue(txt_ControlNum.ClientID));
            //游船公司名称
            model.ShipCompany = Utils.GetFormValue(SupplierControl1.ClientText);
            //游船公司编号
            model.SourceId = Utils.GetFormValue(SupplierControl1.ClientValue);
            //游船名称
            model.ShipName = Utils.GetFormValue("ddl_ShipCompanyName");
            model.SubId = Utils.GetFormValue("ddl_ShipCompany");
            //登船日期
            model.GoShipTime = Utils.GetDateTime(Utils.GetFormValue(txt_GoShipTime.ClientID));
            //最后保留日期
            model.LastTime = Utils.GetDateTime(Utils.GetFormValue(txt_lastTime.ClientID));
            //单价
            model.UnitPrice = Utils.GetDecimal(Utils.GetFormValue(txt_UnitPrice.ClientID));
            //总价
            model.TotalPrice = Utils.GetDecimal(Utils.GetFormValue(txt_TotalPrice.ClientID));
            //预付金额
            model.Advance = Utils.GetDecimal(Utils.GetFormValue(txt_Advance.ClientID));
            //航线
            model.ShipRoute = Utils.GetFormValue(txt_ShipRoute.ClientID);
            //景点
            model.ShipSpot = Utils.GetFormValue(txt_ShipSpot.ClientID);
            //备注
            model.Remark = Utils.GetFormValue(txt_Remark.ClientID);
            #endregion
            #region 验证
            msg += model.ShipCompany.Length > 0 ? string.Empty : "游船公司不能为空！<br/>";
            msg += model.SourceId.Length > 0 ? string.Empty : "游船公司编号不能为空！<br/>";
            msg += model.ShipName.Length > 0 ? string.Empty : "游船名称不能为空！<br/>";
            msg += model.GoShipTime != DateTime.MinValue ? string.Empty : "登船日期不能为空！<br/>";
            msg += model.LastTime != DateTime.MinValue ? string.Empty : "最后保留日期格式错误！<br/>";
            msg += model.UnitPrice > 0 ? string.Empty : "单价不能为空！<br/>";
            msg += model.ControlNum > 0 ? string.Empty : "控船数量不能为0！<br/>";
            #endregion
            //预控类型
            if (Utils.GetFormValue("ControlType").Length > 0)
            {
                model.SourceControlType = (SourceControlType)Utils.GetInt(Utils.GetFormValue("ControlType"));
                switch (model.SourceControlType)
                {
                    case SourceControlType.单控:
                        //单控团号
                        string[] tourList = Utils.GetFormValue("TourList").Split('|');
                        IList<MSourceSueTour> tourNoList = new List<MSourceSueTour>();
                        foreach (string item in tourList)
                        {
                            if (item.Length > 0 && item.Split(',').Length == 2)
                            {
                                tourNoList.Add(new MSourceSueTour { SourceType = SourceControlCategory.游轮, TourId = item.Split(',')[1] });
                            }
                        }
                        model.TourNoList = tourNoList;
                        msg += model.TourNoList.Count > 0 ? string.Empty : "单控状态下请选择团号！<br/>";
                        break;
                    case SourceControlType.总控:

                        //共享类型
                        if (Utils.GetIntSign(Utils.GetFormValue("ShareType"), -1) > 0)
                        {
                            model.ShareType = (ShareType)Utils.GetInt(Utils.GetFormValue("ShareType"));
                            if (model.ShareType == ShareType.不共享)
                            {
                                IList<MSourceSueOperator> sourceSueOperatorList = new List<MSourceSueOperator>();
                                string[] ids = Utils.GetFormValue("Ids").Split(',');
                                foreach (string item in ids)
                                {
                                    sourceSueOperatorList.Add(new MSourceSueOperator { SourceType = SourceControlCategory.游轮, OperatorId = item });
                                }
                                //预控人员
                                model.OperatorList = sourceSueOperatorList;
                                msg += model.OperatorList.Count > 0 ? string.Empty : "总控--不共享状态下请选择用车人员！<br/>";
                            }
                        }
                        else
                        {
                            msg += "总控状态下请选择共享类型！<br/>";
                        }
                        break;
                }
            }
            else
            {
                msg += "预控类型不能为空！<br/>";
            }
            return msg.Length <= 0;

        }
    }
}
