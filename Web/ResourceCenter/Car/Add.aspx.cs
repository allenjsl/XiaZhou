using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

using EyouSoft.BLL.SourceStructure;
using EyouSoft.Model.SourceStructure;
using EyouSoft.Model.EnumType.SourceStructure;
using System.Text;
using EyouSoft.Common.Page;
using EyouSoft.Model.EnumType.PrivsStructure;


namespace Web.ResourceCenter.Car
{
    /// <summary>
    /// 车辆预控-添加-
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
                case "GetCarType":
                    CarType();
                    break;
                case "Save":
                    Save();
                    break;
            }
        }
        /// <summary>
        /// 车型下拉
        /// </summary>
        private void CarType()
        {
            MSourceMotorcade model = new BSource().GetMotorcadeModel(Utils.GetQueryStringValue("sourceId"));
            if (model != null && model.CarList != null && model.CarList.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<option value=-1>-请选择-</option>");
                foreach (MSourceCar item in model.CarList)
                {
                    sb.Append("<option value=" + item.CarId + ">" + item.TypeName + "</option>");
                }
                PageResponse(sb.ToString());
            }
            PageResponse("<option value=-1>-无可选择车型-</option>");
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            ControlType1.SourceControlCategory = SourceControlCategory.车辆;
            SupplierControl1.CallBack = "AddPage.SupplierControlCollBack";
            SupplierControl1.SupplierType = EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车;

        }
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!CheckGrant(Privs.资源预控_车辆预控_新增))
            {
                Utils.ResponseNoPermit(Privs.资源预控_车辆预控_新增, true);
                return;
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            MSourceSueCar model = new MSourceSueCar();
            string msg = string.Empty;
            if (GetPageVal(model, ref msg))
            {
                PageResponse(UtilsCommons.AjaxReturnJson(new BSourceControl().AddSueCar(model) ? "1" : "-1", "添加失败!"));
            }
            else
            {
                PageResponse(UtilsCommons.AjaxReturnJson("-1", msg));
            }
        }
        /// <summary>
        /// 获取页面数据
        /// </summary>
        /// <param name="model">保存数据实体</param>
        /// <param name="msg">验证提示</param>
        /// <returns>验证结果（true验证通过）</returns>
        private bool GetPageVal(MSourceSueCar model, ref string msg)
        {
            #region 实体赋值
            model.DeptId = SiteUserInfo.DeptId;
            //车辆名称
            model.SourceName = Utils.GetFormValue(SupplierControl1.ClientText);
            //车辆编号
            model.SourceId = Utils.GetFormValue(SupplierControl1.ClientValue);
            //车型
            model.TypeName = Utils.GetFormValue("CarTypeName");
            model.CarId = Utils.GetFormValue("CarTypeId");
            //天数
            model.DaysNum = Utils.GetInt(Utils.GetFormValue(txt_day.ClientID));
            //单价
            model.UnitPrice = Utils.GetDecimal(Utils.GetFormValue(txt_unitPrice.ClientID));
            //总价
            model.TotalPrice = Utils.GetDecimal(Utils.GetFormValue(txt_totalPrice.ClientID));
            //公司编号
            model.CompanyId = CurrentUserCompanyID;
            //操作时间
            model.IssueTime = DateTime.Now;
            //操作员
            model.OperatorId = SiteUserInfo.UserId;
            //预控数量
            model.ControlNum = Utils.GetInt(txt_quantity.Text);
            //最后保留日期
            model.LastTime = Utils.GetDateTime(Utils.GetFormValue(txt_lastTime.ClientID));
            //有效期--始
            model.SueStartTime = Utils.GetDateTime(Utils.GetFormValue(txt_sueStartTime.ClientID));
            //有效期--终
            model.SueEndTime = Utils.GetDateTime(Utils.GetFormValue(txt_sueEndTime.ClientID));
            model.Remark = Utils.GetFormValue(txt_remark.ClientID);
            #endregion
            #region 验证
            msg += model.SourceName.Length > 0 && model.SourceId.Length > 0 ? string.Empty : "车队名称异常,请使用选用功能选取！<br/>";
            msg += model.TypeName.Length > 0 ? string.Empty : "车型不能为空！<br/>";
            msg += model.DaysNum > 0 ? string.Empty : "天数不正确！<br/>";
            msg += model.ControlNum > 0 ? string.Empty : "预控数量不能为0！<br/>";
            msg += model.LastTime != DateTime.MinValue ? string.Empty : "最后保留日期格式错误！<br/>";
            msg += model.SueStartTime != DateTime.MinValue ? string.Empty : "预控时间格式错误！<br/>";
            msg += model.SueEndTime != DateTime.MinValue ? string.Empty : "预控时间格式错误！<br/>";
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
                                tourNoList.Add(new MSourceSueTour { SourceType = SourceControlCategory.车辆, TourId = item.Split(',')[1] });
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
                                    sourceSueOperatorList.Add(new MSourceSueOperator { SourceType = SourceControlCategory.车辆, OperatorId = item });
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
        /// 页面返回
        /// </summary>
        /// <param name="ret"></param>
        private void PageResponse(string ret)
        {
            Response.Clear();
            Response.Write(ret);
            Response.End();
        }
    }
}
