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
    /// 计调中心-地接,出境-领料安排
    /// 创建人：李晓欢
    /// 创建时间：2011-09-16
    /// </summary>
    public partial class OperaterPickingList : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        //支付方式 状态
        protected string panyMent = string.Empty;
        protected string status = string.Empty;
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
            //权限验证
            PowerControl();
            UserId = this.SiteUserInfo.UserId;

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
                        Response.Write(DeletePick());
                        Response.End();
                        break;
                    case "update":
                        GetPickModel();
                        break;
                    case "save":
                        Response.Clear();
                        Response.Write(PageSave());
                        Response.End();
                        break;
                    default:
                        break;
                }
            }
            #endregion

            DataInit();
        }

        #region 绑定安排的计调领料项
        /// <summary>
        /// 页面初始化
        /// 团号 
        /// </summary>
        protected void DataInit()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            if (!string.IsNullOrEmpty(tourId))
            {
                ListPower = this.panView.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(tourId, SiteUserInfo.UserId);
                if (ListPower) ListPower = panView.Visible = Privs_AnPai;

                IList<EyouSoft.Model.PlanStructure.MPlan> list = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.领料, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourId);
                if (list != null && list.Count > 0)
                {
                    this.repPickList.DataSource = list;
                    this.repPickList.DataBind();
                }
                else
                {
                    this.phdShowList.Visible = false;
                }
            }
        }
        #endregion

        #region 删除领料计调项
        /// <summary>
        /// 删除领料计调项
        /// </summary>
        /// <param name="planID">计调项id</param>
        /// <returns></returns>
        protected string DeletePick()
        {
            string planId = Utils.GetQueryStringValue("planId");
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(planId))
            {
                if (new EyouSoft.BLL.PlanStructure.BPlan().DelPlan(planId))
                {
                    msg = UtilsCommons.AjaxReturnJson("1", "删除成功！");
                }
                else
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "删除失败！");
                }
            }
            return msg;
        }
        #endregion

        #region 获取领料实体
        /// <summary>
        /// 获取领料实体
        /// </summary>
        /// <param name="planID"></param>
        protected void GetPickModel()
        {
            string planId = Utils.GetQueryStringValue("planId");
            if (!string.IsNullOrEmpty(planId))
            {
                EyouSoft.Model.PlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.BLL.PlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.领料, planId);
                if (baseinfo != null)
                {
                    this.txtNums.Text = baseinfo.Num.ToString();
                    if (baseinfo.PlanGood != null)
                    {
                        this.txtUnitPrices.Text = Utils.FilterEndOfTheZeroDecimal(baseinfo.PlanGood.Price);
                    }
                    this.txtTotalPrices.Text = Utils.FilterEndOfTheZeroDecimal(baseinfo.Confirmation);
                    //领料人
                    this.txtGuidNotes.Text = baseinfo.GuideNotes;
                    this.txtOtherRemarks.Text = baseinfo.Remarks;
                    panyMent = ((int)baseinfo.PaymentType).ToString();
                    status = ((int)baseinfo.Status).ToString();
                    //领料内容 id
                    this.SelectObject1.ObjectID = baseinfo.SourceId;
                    this.SelectObject1.ObjectName = baseinfo.SourceName;
                    //领料人name                    
                    this.hrSelect.HrSelectName = baseinfo.ContactName;
                    //领料人id
                    this.hrSelect.HrSelectID = baseinfo.PlanGood.UserId;
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存领料计调项
        /// </summary>
        /// <returns></returns>
        protected string PageSave()
        {
            #region  表单取值
            string msg = string.Empty;
            string setErrorMsg = string.Empty;
            //领料id name
            string pickid = Utils.GetFormValue(this.SelectObject1.SelectIDClient);
            string pickName = Utils.GetFormValue(this.SelectObject1.SelectNameClient);
            //数量
            int pickNums = Utils.GetInt(Utils.GetFormValue(this.txtNums.UniqueID));
            //单价 结算费用
            decimal prices = Utils.GetDecimal(Utils.GetFormValue(this.txtUnitPrices.UniqueID));
            decimal totalPrices = Utils.GetDecimal(Utils.GetFormValue(this.txtTotalPrices.UniqueID));
            //领料人
            string pickNames = Utils.GetFormValue(this.hrSelect.HrSelectNameClient);
            string pickIds = Utils.GetFormValue(this.hrSelect.HrSelectIDClient);
            //导游需知 其它备注
            string guidNotes = Utils.GetFormValue(this.txtGuidNotes.UniqueID);
            string otherMarks = Utils.GetFormValue(this.txtOtherRemarks.UniqueID);
            #endregion

            #region 后台验证
            if (string.IsNullOrEmpty(pickid) && string.IsNullOrEmpty(pickName))
            {
                msg += "请选择领料内容!<br/>";
            }
            if (pickNums <= 0)
            {
                msg += "请输入领料数量！<br/>";
            }
            if (prices <= 0)
            {
                msg += "请输入单价！<br/>";
            }
            if (totalPrices <= 0)
            {
                msg += "请输入结算费用！<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("selStatus")))
            {
                msg += "请选择状态！<br/>";
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
            baseinfo.PlanGood = new EyouSoft.Model.GovStructure.MGovGoodUse();
            baseinfo.PlanGood.GoodId = pickid;
            baseinfo.PlanGood.GoodName = pickName;
            baseinfo.PlanGood.UserId = pickIds;
            baseinfo.PlanGood.Price = prices;
            baseinfo.Remarks = otherMarks;
            baseinfo.GuideNotes = guidNotes;
            baseinfo.SourceId = pickid;
            baseinfo.SourceName = pickName;
            baseinfo.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("selStatus"));
            baseinfo.TourId = Utils.GetQueryStringValue("tourId");
            baseinfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.领料;
            baseinfo.ContactName = pickNames;
            baseinfo.DeptId = this.SiteUserInfo.DeptId;
            baseinfo.OperatorId = this.SiteUserInfo.UserId;
            baseinfo.OperatorName = this.SiteUserInfo.Name;
            baseinfo.Num = pickNums;
            baseinfo.IssueTime = System.DateTime.Now;
            baseinfo.PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)Utils.GetInt(Utils.GetFormValue("selPanyMent"));
            #endregion

            #region 提交操作
            string planID = Utils.GetQueryStringValue("planId");
            int result = 0;
            if (!string.IsNullOrEmpty(planID))
            {
                baseinfo.PlanGood.PlanId = planID;
                baseinfo.PlanId = planID;
                result = new EyouSoft.BLL.PlanStructure.BPlan().UpdPlan(baseinfo);
                if (result == 1)
                {
                    msg += "修改成功！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else if (result == 0)
                {
                    msg += "修改失败！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
                else if (result == -1)
                {
                    msg += "领料不足，修改失败！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            else
            {
                result = new EyouSoft.BLL.PlanStructure.BPlan().AddPlan(baseinfo);
                if (result == 1)
                {
                    msg += "添加成功！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else if (result == 0)
                {
                    msg += "添加失败！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
                else if (result == -1)
                {
                    msg += "领料不足，添加失败！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            #endregion

            return setErrorMsg;
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
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排领料))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排领料, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排领料);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排领料))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排领料, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排领料);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排领料))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排领料, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排领料);
                    break;
            }
        }
        #endregion
    }
}