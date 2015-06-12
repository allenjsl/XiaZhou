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
    /// 计调中心-出境,地接-用车安排
    /// 创建人：李晓欢
    /// 创建时间：2011-09-09
    /// </summary>
    public partial class OperaterCarList : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        //支付方式 状态
        protected string panyMent = string.Empty;
        protected string status = string.Empty;
        //车型
        protected System.Text.StringBuilder CarModelList = new System.Text.StringBuilder();
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
            //权限判断
            PowerControl();

            UserId = this.SiteUserInfo.UserId;
            EyouSoft.BLL.SysStructure.BSys bsys = new EyouSoft.BLL.SysStructure.BSys();
            if (bsys.IsExistsMenu2(this.SiteUserInfo.SysId, EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源预控_车辆预控))
            {
                this.SupplierControl1.Flag = 1;
            }
            //用车类型
            InitCarType();
            querenUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.用车确认单);

            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("action");
            if (doType != "")
            {
                //存在ajax请求
                switch (doType)
                {
                    case "save":
                        Response.Clear();
                        Response.Write(PageSave());
                        Response.End();
                        break;
                    case "delete":
                        Response.Clear();
                        Response.Write(DelOperatCar());
                        Response.End();
                        break;
                    case "update":
                        GetCarModel();
                        break;
                    default: break;
                }
            }
            #endregion

            PageInit();

        }


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
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排用车))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排用车, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排用车);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排用车))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排用车, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排用车);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排用车))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排用车, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排用车);
                    break;
            }
        }
        #endregion

        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            if (!string.IsNullOrEmpty(tourId))
            {
                ListPower = this.panView.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(tourId, SiteUserInfo.UserId);
                if (ListPower) ListPower = panView.Visible = Privs_AnPai;

                this.SupplierControl1.TourID = tourId;
                IList<EyouSoft.Model.PlanStructure.MPlan> CarList = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourId);
                if (CarList != null && CarList.Count > 0)
                {
                    this.repCarList.DataSource = CarList;
                    this.repCarList.DataBind();
                }
                else
                {
                    this.phdShowList.Visible = false;
                }
            }
        }
        #endregion

        #region 绑定用车类型
        /// <summary>
        /// 绑定用车类型
        /// </summary>
        protected void InitCarType()
        {
            //用车类型
            this.ddlUserCarType.Items.Clear();
            List<EnumObj> CarState = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanCarType));
            if (CarState != null && CarState.Count > 0)
            {
                for (int i = 0; i < CarState.Count; i++)
                {
                    ListItem stateitem = new ListItem();
                    stateitem.Text = CarState[i].Text;
                    stateitem.Value = CarState[i].Value;
                    this.ddlUserCarType.Items.Add(stateitem);
                }
            }
        }
        #endregion

        #region 删除已安排的车队
        /// <summary>
        /// 删除已安排的车队
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        protected string DelOperatCar()
        {
            string planId = Utils.GetQueryStringValue("PlanId");
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

        #region 获取车辆实体
        /// <summary>
        /// 获取车辆实体
        /// </summary>
        protected void GetCarModel()
        {
            string planId = Utils.GetQueryStringValue("PlanId");
            if (!string.IsNullOrEmpty(planId))
            {
                EyouSoft.Model.PlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.BLL.PlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车, planId);
                if (baseinfo != null)
                {
                    this.hidSueID.Value = baseinfo.SueId;
                    this.hidSueNum.Value = baseinfo.Num.ToString();
                    this.SupplierControl1.HideID = baseinfo.SourceId;
                    this.SupplierControl1.Name = baseinfo.SourceName;
                    if (!string.IsNullOrEmpty(baseinfo.SueId.Trim()))
                    {
                        this.SupplierControl1.HideID_zyyk = baseinfo.SueId;
                        this.SupplierControl1.isyukong = "1";
                    }
                    else
                    {
                        this.SupplierControl1.isyukong = "0";
                    }
                    this.txtContectName.Text = baseinfo.ContactName;
                    this.txtContectPhone.Text = baseinfo.ContactPhone;
                    this.txtContectFax.Text = baseinfo.ContactFax;
                    this.txtUseCarTime.Text = UtilsCommons.GetDateString(baseinfo.StartDate, ProviderToDate);
                    this.txtUseCarTime2.Text = UtilsCommons.GetDateString(baseinfo.EndDate, ProviderToDate);
                    this.txttime1.Text = baseinfo.StartTime;
                    this.txttime2.Text = baseinfo.EndTime;
                    if (baseinfo.PlanCar != null)
                    {
                        this.ddlUserCarType.Items.FindByValue(((int)baseinfo.PlanCar.VehicleType).ToString()).Selected = true;
                        this.txtCarNumber.Text = baseinfo.PlanCar.CarNumber;
                        this.txtDirverName.Text = baseinfo.PlanCar.Driver;
                        this.txDirverPhone.Text = baseinfo.PlanCar.DriverPhone;
                        if (!string.IsNullOrEmpty(baseinfo.SueId.Trim()))
                        {
                            CarModelList.Append("<option value='" + baseinfo.PlanCar.CarId + "," + baseinfo.PlanCar.Models + "'>" + baseinfo.PlanCar.Models + "</option>");
                            EyouSoft.Model.SourceStructure.MSourceSueCar carSueModel = new EyouSoft.BLL.SourceStructure.BSourceControl().GetModelByCarId(baseinfo.SueId, this.SiteUserInfo.CompanyId);
                            if (carSueModel != null)
                            {
                                int nums = carSueModel.ControlNum - carSueModel.AlreadyNum;
                                this.hidCarNum.Value = (nums + baseinfo.Num).ToString();
                            }
                        }
                        else
                        {
                            EyouSoft.Model.SourceStructure.MSourceMotorcade carModel = new EyouSoft.BLL.SourceStructure.BSource().GetMotorcadeModel(baseinfo.SourceId);
                            if (carModel != null)
                            {
                                if (carModel.CarList != null && carModel.CarList.Count > 0)
                                {
                                    for (int i = 0; i < carModel.CarList.Count; i++)
                                    {
                                        if (carModel.CarList[i].CarId == baseinfo.PlanCar.CarId)
                                        {
                                            CarModelList.Append("<option selected='selected' value='" + carModel.CarList[i].CarId + "," + carModel.CarList[i].TypeName + "'>" + carModel.CarList[i].TypeName + "</option>");
                                        }
                                        else
                                        {
                                            CarModelList.Append("<option value='" + carModel.CarList[i].CarId + "," + carModel.CarList[i].TypeName + "'>" + carModel.CarList[i].TypeName + "</option>");
                                        }
                                        this.hiddriverInfo.Value += carModel.CarList[i].CarId + "," + carModel.CarList[i].CarNumber + "," + carModel.CarList[i].Driver + "," + carModel.CarList[i].DriverTel + "|";
                                    }
                                }
                            }
                        }
                    }
                    this.txtCostParticu.Text = baseinfo.CostDetail;
                    this.txtUseCarNums.Text = baseinfo.Num.ToString();
                    this.txttotalMoney.Text = Utils.FilterEndOfTheZeroDecimal(baseinfo.Confirmation);
                    this.txtTravel.Text = baseinfo.ReceiveJourney;
                    this.txtGuidNotes.Text = baseinfo.GuideNotes;
                    this.txtOtherRemark.Text = baseinfo.Remarks;
                    this.ddlProfit.Items.FindByValue(baseinfo.IsRebate == true ? "0" : "1").Selected = true;
                    panyMent = ((int)baseinfo.PaymentType).ToString();
                    status = ((int)baseinfo.Status).ToString();
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        protected string PageSave()
        {
            #region 表单赋值
            string msg = string.Empty;
            string seterrorMsg = string.Empty;
            //车队
            string CarName = Utils.GetFormValue(this.SupplierControl1.ClientText);
            string CarId = Utils.GetFormValue(this.SupplierControl1.ClientValue);
            //联系人 联系电话 联系传真
            string contectName = Utils.GetFormValue(this.txtContectName.UniqueID);
            string contectPhone = Utils.GetFormValue(this.txtContectPhone.UniqueID);
            string contectFax = Utils.GetFormValue(this.txtContectFax.UniqueID);
            //用车时间开始 开始时间点 结束 结束时间点
            DateTime startTime = Utils.GetDateTime(Utils.GetFormValue(this.txtUseCarTime.UniqueID));
            string timeHours1 = Utils.GetFormValue(this.txttime1.UniqueID);
            DateTime endTime = Utils.GetDateTime(Utils.GetFormValue(this.txtUseCarTime2.UniqueID));
            string timeHours2 = Utils.GetFormValue(this.txttime2.UniqueID);
            //用车类型 车型 车牌号
            string useCarType = Utils.GetFormValue(this.ddlUserCarType.UniqueID);
            string carTypeId = Utils.GetFormValue("selCarModel").Split(',')[0];
            string carType = Utils.GetFormValue("selCarModel").Split(',')[1];
            string carNumber = Utils.GetFormValue(this.txtCarNumber.UniqueID);
            //司机 司机电话
            string driverName = Utils.GetFormValue(this.txtDirverName.UniqueID);
            string driverPhone = Utils.GetFormValue(this.txDirverPhone.UniqueID);
            //费用明细 用车数量  结算费用
            string CostParticu = Utils.GetFormValue(this.txtCostParticu.UniqueID);
            string useCarnums = Utils.GetFormValue(this.txtUseCarNums.UniqueID);
            decimal totalMoney = Utils.GetDecimal(Utils.GetFormValue(this.txttotalMoney.UniqueID));

            //接待行程 导游需知 其它备注 返利 支付方式 状态
            string travel = Utils.GetFormValue(this.txtTravel.UniqueID);
            string guidNotes = Utils.GetFormValue(this.txtGuidNotes.UniqueID);
            string otherRemark = Utils.GetFormValue(this.txtOtherRemark.UniqueID);
            bool profit = Utils.GetFormValue(this.ddlProfit.UniqueID) == "0" ? true : false;
            #endregion

            #region 表单验证
            if (string.IsNullOrEmpty(CarId) && string.IsNullOrEmpty(CarName))
            {
                msg += "请选择车队!<br/>";
            }
            if (string.IsNullOrEmpty(startTime.ToString()))
            {
                msg += "请输入用车开始时间天!<br/>";
            }
            /*if (string.IsNullOrEmpty(timeHours1) || timeHours1 == "点时间")
            {
                msg += "请输入用车开始时间点！<br/>";
            }*/
            /*if (string.IsNullOrEmpty(endTime.ToString()))
            {
                msg += "请输入用车结束时间天！<br/>";
            }*/
            /*if (string.IsNullOrEmpty(timeHours2) || timeHours2 == "点时间")
            {
                msg += "请输入用车结束时间点!<br/>";
            }*/
            if (useCarType.ToString() == "-1")
            {
                msg += "请选择用车类型!<br/>";
            }
            if (string.IsNullOrEmpty(CostParticu))
            {
                msg += "请输入费用明细！<br/>";
            }
            if (totalMoney <= 0)
            {
                msg += "请输入结算费用！<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("selPenyMent")))
            {
                msg += "请选择支付方式！<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("SelStatus")))
            {
                msg += "请选择状态！<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue(this.ddlProfit.UniqueID)))
            {
                msg += "请选择是否返利！<br/>";
            }
            if (msg != "")
            {
                seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                return seterrorMsg;
            }
            #endregion

            #region 实体赋值
            EyouSoft.Model.PlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.Model.PlanStructure.MPlanBaseInfo();
            baseinfo.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            baseinfo.CompanyId = this.SiteUserInfo.CompanyId;
            baseinfo.Confirmation = totalMoney;
            baseinfo.PlanCost = totalMoney;
            baseinfo.ContactFax = contectFax;
            baseinfo.ContactName = contectName;
            baseinfo.ContactPhone = contectPhone;
            baseinfo.CostDetail = CostParticu;
            baseinfo.StartTime = (timeHours1 == "点时间" ? "" : timeHours1);
            baseinfo.StartDate = startTime;
            baseinfo.EndTime = (timeHours2 == "点时间" ? "" : timeHours2);
            baseinfo.EndDate = endTime;
            baseinfo.GuideNotes = guidNotes;
            baseinfo.IsRebate = profit;
            baseinfo.IssueTime = System.DateTime.Now;
            baseinfo.Num = Utils.GetInt(useCarnums);
            baseinfo.PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)Utils.GetInt(Utils.GetFormValue("selPenyMent"));
            baseinfo.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("SelStatus"));
            baseinfo.SourceId = Utils.GetFormValue(this.SupplierControl1.ClientValue);
            baseinfo.SourceName = Utils.GetFormValue(this.SupplierControl1.ClientText);
            if (Utils.GetFormValue(this.SupplierControl1.ClientIsyukong) == "1")
            {
                baseinfo.SueId = Utils.GetFormValue(this.SupplierControl1.ClientzyykValue);
            }
            baseinfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车;
            baseinfo.TourId = Utils.GetQueryStringValue("tourId");
            baseinfo.ReceiveJourney = travel;
            baseinfo.PlanCar = new EyouSoft.Model.PlanStructure.MPlanCar();
            baseinfo.PlanCar.VehicleType = (EyouSoft.Model.EnumType.PlanStructure.PlanCarType)Enum.Parse(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanCarType), useCarType);
            baseinfo.PlanCar.Models = carType;
            baseinfo.PlanCar.CarId = carTypeId;
            baseinfo.PlanCar.Driver = driverName;
            baseinfo.PlanCar.DriverPhone = driverPhone;
            baseinfo.PlanCar.CarNumber = carNumber;
            baseinfo.Remarks = otherRemark;
            baseinfo.OperatorId = this.SiteUserInfo.UserId;
            baseinfo.OperatorName = this.SiteUserInfo.Name;
            #endregion

            #region 提交操作
            string editid = Utils.GetQueryStringValue("planId");
            EyouSoft.BLL.PlanStructure.BPlan bll = new EyouSoft.BLL.PlanStructure.BPlan();
            int result = 0;
            if (editid != "" && editid != null)
            {
                baseinfo.PlanId = editid;
                baseinfo.PlanCar.PlanId = editid;
                result = bll.UpdPlan(baseinfo);
                if (result == 1)
                {
                    msg += "修改成功!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else if (result == 0)
                {
                    msg += "修改失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
                else if (result == -2)
                {
                    msg += "预控数量不足,修改失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            else
            {
                result = bll.AddPlan(baseinfo);
                if (result == 1)
                {
                    msg += "添加成功!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else if (result == 0)
                {
                    msg += "添加失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
                else if (result == -2)
                {
                    msg += "预控数量不足,添加失败!";
                    seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            #endregion

            return seterrorMsg;
        }
        #endregion
    }
}
