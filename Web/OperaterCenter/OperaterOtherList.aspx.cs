using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.OperaterCenter
{
    /// <summary>
    /// 计调中心-出境,地接-其它安排
    /// 创建人：李晓欢
    /// 创建时间:2011-09-17
    /// </summary>
    public partial class OperaterOtherList : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        //支付方式 状态
        protected string panyMent = string.Empty;
        protected string status = string.Empty;
        //确认单
        protected string querenUrl = string.Empty;
        //预控编号
        protected string SueID = string.Empty;
        //登录人
        protected string UserId = string.Empty;
        /// <summary>
        /// 列表操作
        /// </summary>
        protected bool ListPower = false;
        /// <summary>
        /// 安排权限
        /// </summary>
        bool Privs_AnPai = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //权限判断
            PowerControl();
            
            UserId = this.SiteUserInfo.UserId;
            if (new EyouSoft.BLL.SysStructure.BSys().IsExistsMenu2(this.SiteUserInfo.SysId, EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源预控_其他预控))
            {
                this.supplierControl1.Flag = 1;
                this.supplierControl1.TourID = Utils.GetQueryStringValue("TourId");
            }
            querenUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.其它安排确认单);

            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("action");
            if (doType != "")
            {
                //存在ajax请求
                switch (doType)
                {
                    case "delete":
                        Response.Clear();
                        Response.Write(DeletetOther());
                        Response.End();
                        break;
                    case "update":
                        GetOtherPlan();
                        break;
                    case "save":
                        Response.Clear();
                        Response.Write(PageSave());
                        Response.End();
                        break;
                    default: break;
                }
            }
            #endregion

            DataInit();
        }

        #region 绑定安排的计调项其它列表
        /// <summary>
        /// 绑定安排的计调项其它列表
        /// </summary>
        /// <param name="tourID">团号</param>
        protected void DataInit()
        {
            string tourId = Utils.GetQueryStringValue("tourId");            
            if (!string.IsNullOrEmpty(tourId))
            {
                ListPower = this.panView.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(tourId, SiteUserInfo.UserId);
                if (ListPower) ListPower = panView.Visible = Privs_AnPai;

                this.supplierControl1.TourID = tourId;
                IList<EyouSoft.Model.PlanStructure.MPlan> list = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourId);
                if (list != null && list.Count > 0)
                {
                    this.repOtherList.DataSource = list;
                    this.repOtherList.DataBind();
                }
                else
                {
                    this.phdShowList.Visible = false;
                }
            }
        }
        #endregion

        #region 删除其它计调项
        /// <summary>
        /// 删除其它计调项
        /// </summary>
        /// <param name="planID">计调项id</param>
        /// <returns></returns>
        protected string DeletetOther()
        {
            string planId = Utils.GetQueryStringValue("planId");
            string msg = string.Empty;
            if (new EyouSoft.BLL.PlanStructure.BPlan().DelPlan(planId))
            {
                msg = UtilsCommons.AjaxReturnJson("1", "删除成功！");
            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("0", "删除失败！");
            }
            return msg;
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            var tourType = new EyouSoft.BLL.TourStructure.BTour().GetTourType(Utils.GetQueryStringValue("tourid"));

            switch (tourType)
            {
                case EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.出境团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排其它))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排其它, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排其它);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排其它))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排其它, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排其它);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排其它))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排其它, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排其它);
                    break;
            }
        }
        #endregion

        #region 获取其它计调项实体
        /// <summary>
        /// 获取其它计调项实体
        /// </summary>
        /// <param name="planID">计调项id</param>
        protected void GetOtherPlan()
        {
            string planId = Utils.GetQueryStringValue("planId");
            if (!string.IsNullOrEmpty(planId))
            {
                EyouSoft.Model.PlanStructure.MPlanBaseInfo baseModel = new EyouSoft.BLL.PlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它, planId);
                if (baseModel != null)
                {
                    this.supplierControl1.HideID = baseModel.SourceId;
                    this.supplierControl1.Name = baseModel.SourceName;

                    if (!string.IsNullOrEmpty(baseModel.SueId.Trim()))
                    {
                        this.supplierControl1.HideID_zyyk = baseModel.SueId;
                        this.supplierControl1.isyukong = "1";
                        SueID = baseModel.SueId;
                        var sueHotel = new EyouSoft.BLL.SourceStructure.BSourceControl().GetModelByOtherId(baseModel.SueId, this.SiteUserInfo.CompanyId);
                        if (sueHotel != null)
                        {
                            this.hidUserNum.Value = (sueHotel.ControlNum - sueHotel.AlreadyNum + baseModel.Num).ToString();
                        }
                    }
                    else
                    {
                        this.supplierControl1.isyukong = "0";
                    }

                    this.txtContactFax.Text = baseModel.ContactFax;
                    this.txtContactName.Text = baseModel.ContactName;
                    this.txtContactPhone.Text = baseModel.ContactPhone;
                    this.txtOutProject.Text = baseModel.CostDetail;
                    this.txtTotalPrices.Text = Utils.FilterEndOfTheZeroDecimal(baseModel.Confirmation);
                    this.txtNums.Text = baseModel.Num.ToString();
                    this.txtGuisNotes.Text = baseModel.GuideNotes;
                    this.txtOtherMark.Text = baseModel.Remarks;
                    panyMent = ((int)baseModel.PaymentType).ToString();
                    status = ((int)baseModel.Status).ToString();
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 其它计调项保存
        /// </summary>
        /// <returns></returns>
        protected string PageSave()
        {
            #region 表单取值
            string msg = string.Empty;
            string setErrorMsg = string.Empty;
            //供应商
            string sourceId = Utils.GetFormValue(this.supplierControl1.ClientValue);
            string sourceName = Utils.GetFormValue(this.supplierControl1.ClientText);
            //联系人 电话 传真
            string contactName = Utils.GetFormValue(this.txtContactName.UniqueID);
            string contactPhone = Utils.GetFormValue(this.txtContactPhone.UniqueID);
            string contactFax = Utils.GetFormValue(this.txtContactFax.UniqueID);
            //支出项目 人数
            string costDetail = Utils.GetFormValue(this.txtOutProject.UniqueID);
            int Peoplenums = Utils.GetInt(Utils.GetFormValue(this.txtNums.UniqueID));
            //结算费用
            decimal totalPrices = Utils.GetDecimal(Utils.GetFormValue(this.txtTotalPrices.UniqueID));
            //导游需知 其它备注
            string guidNoted = Utils.GetFormValue(this.txtGuisNotes.UniqueID);
            string otherRemark = Utils.GetFormValue(this.txtOtherMark.UniqueID);
            #endregion

            #region 后台验证
            if (string.IsNullOrEmpty(sourceId) && string.IsNullOrEmpty(sourceName))
            {
                msg += "请选择供应商!<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("selPanyMent")))
            {
                msg += "请选择支付方式！<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("selStatus")))
            {
                msg += "请选择状态！<br/>";
            }
            if (totalPrices <= 0)
            {
                msg += "请输入结算费用！";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                setErrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                return setErrorMsg;
            }
            #endregion

            #region 实体赋值
            EyouSoft.Model.PlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.Model.PlanStructure.MPlanBaseInfo();
            baseinfo.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            baseinfo.CompanyId = this.SiteUserInfo.CompanyId;
            baseinfo.Confirmation = totalPrices;
            baseinfo.PlanCost = totalPrices;
            baseinfo.ContactFax = contactFax;
            baseinfo.ContactName = contactName;
            baseinfo.ContactPhone = contactPhone;
            baseinfo.CostDetail = costDetail;
            baseinfo.GuideNotes = guidNoted;
            baseinfo.IssueTime = System.DateTime.Now;
            baseinfo.Num = Peoplenums;
            baseinfo.PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)Utils.GetInt(Utils.GetFormValue("selPanyMent"));
            baseinfo.Remarks = otherRemark;
            baseinfo.SourceId = sourceId;
            baseinfo.SourceName = sourceName;
            baseinfo.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("selStatus"));
            if (Utils.GetFormValue(this.supplierControl1.ClientIsyukong) == "1")
            {
                baseinfo.SueId = Utils.GetFormValue(this.supplierControl1.ClientzyykValue);
            }
            baseinfo.TourId = Utils.GetQueryStringValue("tourId");
            baseinfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它;
            baseinfo.OperatorName = this.SiteUserInfo.Name;
            baseinfo.OperatorId = this.SiteUserInfo.UserId;
            #endregion

            #region 提交操作
            string planID = Utils.GetQueryStringValue("planId");
            if (!string.IsNullOrEmpty(planID))
            {
                baseinfo.PlanId = planID;
                var r = new EyouSoft.BLL.PlanStructure.BPlan().UpdPlan(baseinfo);
                if ( r==1)
                {
                    msg += "修改成功";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else if (r==0)
                {
                    msg += "修改失败!";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
                else if (r == -2)
                {
                    msg += "预控数量不足,修改失败!";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            else
            {
                var r = new EyouSoft.BLL.PlanStructure.BPlan().AddPlan(baseinfo);
                if ( r==1)
                {
                    msg += "添加成功！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else if (r==0)
                {
                    msg += "添加失败!";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
                else if (r == -2)
                {
                    msg += "预控数量不足,修改失败!";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            #endregion

            return setErrorMsg;
        }
        #endregion
    }
}
