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
    /// 计调中心—地接，出境，购物安排
    /// 创建人：李晓欢
    /// 创建时间：2011-09-16
    /// </summary>
    public partial class OperaterShoppingList : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        protected string status = string.Empty;
        //确认单
        protected string querenUrl = string.Empty;
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
            querenUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.购物确认单);

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
                        Response.Write(DeleteShop());
                        Response.End();
                        break;
                    case "update":
                        GetShopModel();
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

        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void DataInit()
        {
            string tourId = Utils.GetQueryStringValue("tourId");           
            if (!string.IsNullOrEmpty(tourId))
            {
                ListPower = this.panView.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(tourId, SiteUserInfo.UserId);
                if (ListPower) ListPower = panView.Visible = Privs_AnPai;

                IList<EyouSoft.Model.PlanStructure.MPlan> list = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourId);
                if (list != null && list.Count > 0)
                {
                    this.repShoppingList.DataSource = list;
                    this.repShoppingList.DataBind();
                }
                else
                {
                    this.phdShowList.Visible = false;
                }
            }
        }
        #endregion

        #region 删除安排的计调项
        /// <summary>
        /// 删除安排的计调项
        /// </summary>
        /// <param name="planID">计调项id</param>
        /// <returns></returns>
        protected string DeleteShop()
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

        #region 获取安排的购物计调项实体
        /// <summary>
        /// 获取安排的购物计调项实体
        /// </summary>
        /// <param name="planID">计调项id</param>
        protected void GetShopModel()
        {
            string planId = Utils.GetQueryStringValue("planId");
            if (!string.IsNullOrEmpty(planId))
            {
                EyouSoft.Model.PlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.BLL.PlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物, planId);
                if (baseinfo != null)
                {
                    this.SupplierControl1.HideID = baseinfo.SourceId;
                    this.SupplierControl1.Name = baseinfo.SourceName;
                    this.txtContactName.Text = baseinfo.ContactName;
                    this.txtContactPhone.Text = baseinfo.ContactPhone;
                    this.txtContactFax.Text = baseinfo.ContactFax;
                    this.txtPeopleNums.Text = baseinfo.Num.ToString();
                    this.txtProfitStand.Text = baseinfo.ServiceStandard;
                    this.txtGuidNotes.Text = baseinfo.GuideNotes;
                    this.txtOtherRemark.Text = baseinfo.Remarks;
                    status = ((int)baseinfo.Status).ToString();
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 购物计调项保存
        /// </summary>
        /// <returns></returns>
        protected string PageSave()
        {
            #region 表单取值
            string msg = string.Empty;
            string setErrorMsg = string.Empty;
            //购物点
            string sourceID = Utils.GetFormValue(this.SupplierControl1.ClientValue);
            string sourceName = Utils.GetFormValue(this.SupplierControl1.ClientText);
            //联系人 电话 传真
            string contactName = Utils.GetFormValue(this.txtContactName.UniqueID);
            string contactFax = Utils.GetFormValue(this.txtContactFax.UniqueID);
            string contactPhome = Utils.GetFormValue(this.txtContactPhone.UniqueID);
            //人头数
            int nums = Utils.GetInt(Utils.GetFormValue(this.txtPeopleNums.UniqueID));
            //返利标准 导游须知 其它备注
            string profitStandard = Utils.GetFormValue(this.txtProfitStand.UniqueID);
            string guidNotes = Utils.GetFormValue(this.txtGuidNotes.UniqueID);
            string otherMarks = Utils.GetFormValue(this.txtOtherRemark.UniqueID);
            #endregion

            #region 验证
            if (string.IsNullOrEmpty(sourceID) && string.IsNullOrEmpty(sourceName))
            {
                msg += "请选择购物店!<br/>";
            }
            if (nums < 0)
            {
                msg += "请输入人头数！<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("selStatus")))
            {
                msg += "请选择状态!<br/>";
            }
            if (msg != "")
            {
                setErrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                return setErrorMsg;
            }
            #endregion

            #region 提交
            EyouSoft.Model.PlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.Model.PlanStructure.MPlanBaseInfo();
            baseinfo.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            baseinfo.CompanyId = this.SiteUserInfo.CompanyId;
            baseinfo.ContactFax = contactFax;
            baseinfo.ContactName = contactName;
            baseinfo.ContactPhone = contactPhome;
            baseinfo.Num = nums;
            baseinfo.ServiceStandard = profitStandard;
            baseinfo.SourceId = sourceID;
            baseinfo.SourceName = sourceName;
            baseinfo.GuideNotes = guidNotes;
            baseinfo.Remarks = otherMarks;
            baseinfo.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("selStatus"));
            baseinfo.SueId = "";
            baseinfo.TourId = Utils.GetQueryStringValue("tourId");
            baseinfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物;
            baseinfo.IssueTime = System.DateTime.Now;
            baseinfo.OperatorId = this.SiteUserInfo.UserId;
            baseinfo.OperatorName = this.SiteUserInfo.Name;

            string planID = Utils.GetQueryStringValue("planId");
            if (!string.IsNullOrEmpty(planID))
            {
                baseinfo.PlanId = planID;
                if (new EyouSoft.BLL.PlanStructure.BPlan().UpdPlan(baseinfo) > 0)
                {
                    msg += "修改成功！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else
                {
                    msg += "修改失败！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            else
            {
                if (new EyouSoft.BLL.PlanStructure.BPlan().AddPlan(baseinfo) > 0)
                {
                    msg += "添加成功！";
                    setErrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else
                {
                    msg += "添加失败！";
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
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排购物))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排购物, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排购物);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排购物))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排购物, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排购物);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排购物))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排购物, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排购物);
                    break;
            }
        }
        #endregion
    }
}
