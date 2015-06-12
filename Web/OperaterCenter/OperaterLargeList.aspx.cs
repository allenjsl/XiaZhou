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
    /// 计调中心-出境,地接-大交通飞机，汽车，火车
    /// 创建人:李晓欢
    /// 创建时间：2011-09-16
    /// </summary>
    public partial class OperaterLargeList : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        //支付方式 状态
        protected string PantMent = string.Empty;
        protected string Status = string.Empty;
        protected string PanyMentT = string.Empty;
        protected string StatusT = string.Empty;
        protected string panyMentB = string.Empty;
        protected string StatusB = string.Empty;
        //大交通确认单
        protected string querenAirUrl = string.Empty;
        protected string querenTrainUrl = string.Empty;
        protected string querenBusUrl = string.Empty;
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
            querenAirUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.机票确认单);
            querenTrainUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.火车确认单);
            querenBusUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.汽车确认单);

            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("action");
            //存在ajax请求
            switch (doType)
            {
                case "saveAir": PageSave(); break;
                case "deleteAir": DeleteAir(); break;
                case "updateAir": GetAirModel(); break;
                case "saveTrain": PageSaveTrain(); break;
                case "deleteTrain": DeleteTrain(); break;
                case "updateTrain": GetTrainModel(); break;
                case "deleteBus": deleteBus(); break;
                case "updateBus": GetModelBus(); break;
                case "saveBus": PageSaveBus(); break;
                default: break;
            }
            #endregion

            DataInit();
            DataInitTrain();
            DataInitBus();
        }

        #region  飞机
        #region 初始化舱位 乘客类型
        /// <summary>
        /// 初始化舱位 乘客类型
        /// </summary>
        /// <param name="selected">选中的id</param>
        /// <returns></returns>
        protected string seleSpaceTypeHtml(string selected)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            List<EnumObj> spaceType = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanLargeSeatType));
            if (spaceType != null && spaceType.Count > 0)
            {
                sb.Append("<select name=\"seleSpaceType\" class=\"inputselect\" valid=\"required\" errmsg=\"*请选择舱位!\">");
                for (int i = 0; i < spaceType.Count; i++)
                {
                    if (spaceType[i].Value == selected)
                    {
                        sb.Append("<option value='" + spaceType[i].Value + "' selected='selected'>" + spaceType[i].Text + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value='" + spaceType[i].Value + "'>" + spaceType[i].Text + "</option>");
                    }
                }
                sb.Append("</select>");
            }
            return sb.ToString();
        }
        protected string PassengerstypeHtml(string Selected)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            List<EnumObj> PassengerType = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanLargeAdultsType));
            if (PassengerType != null && PassengerType.Count > 0)
            {
                sb.Append("<select name=\"Passengerstype\" class=\"inputselect\" valid=\"required\" errmsg=\"*请选择乘客类型!\">");
                for (int i = 0; i < PassengerType.Count; i++)
                {
                    if (PassengerType[i].Value == Selected)
                    {
                        sb.Append("<option value='" + PassengerType[i].Value + "' selected='selected'>" + PassengerType[i].Text + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value='" + PassengerType[i].Value + "'>" + PassengerType[i].Text + "</option>");
                    }
                }
                sb.Append("</select>");
            }
            return sb.ToString();
        }
        #endregion

        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void DataInitTrain()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            if (!string.IsNullOrEmpty(tourId))
            {
                ListPower = this.panView.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(tourId, SiteUserInfo.UserId);
                if (ListPower) ListPower = panView.Visible = Privs_AnPai;

                IList<EyouSoft.Model.PlanStructure.MPlan> AirList = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourId);
                if (AirList != null && AirList.Count > 0)
                {
                    this.repAirlist.DataSource = AirList;
                    this.repAirlist.DataBind();
                }
                else
                {
                    this.panShowListFrist.Visible = false;
                }
            }
        }
        #endregion

        #region 删除飞机
        /// <summary>
        /// 删除飞机
        /// </summary>
        /// <param name="ID">计调id</param>
        /// <returns></returns>
        void DeleteAir()
        {
            string planId = Utils.GetQueryStringValue("planIdAir");
            string mesg = "";
            if (!string.IsNullOrEmpty(planId))
            {
                if (new EyouSoft.BLL.PlanStructure.BPlan().DelPlan(planId))
                {
                    mesg = UtilsCommons.AjaxReturnJson("1", "删除成功！");
                }
                else
                {
                    mesg = UtilsCommons.AjaxReturnJson("0", "删除失败!");
                }
            }
            
            RCWE(mesg);
        }
        #endregion

        #region 获取飞机实体
        /// <summary>
        /// 获取飞机实体
        /// </summary>
        protected void GetAirModel()
        {
            string planId = Utils.GetQueryStringValue("planIdAir");
            if (!string.IsNullOrEmpty(planId))
            {
                EyouSoft.Model.PlanStructure.MPlanBaseInfo AirModel = new EyouSoft.BLL.PlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机, planId);
                if (AirModel != null)
                {
                    this.supplierControl1.HideID = AirModel.SourceId;
                    this.supplierControl1.Name = AirModel.SourceName;
                    this.txtContentFax.Text = AirModel.ContactFax;
                    this.txtContentName.Text = AirModel.ContactName;
                    this.txtContentPhone.Text = AirModel.ContactPhone;
                    if (AirModel.PlanLargeTime != null && AirModel.PlanLargeTime.Count > 0)
                    {
                        this.tabHolderView.Visible = false;
                        this.repFilght.DataSource = AirModel.PlanLargeTime;
                        this.repFilght.DataBind();
                    }
                    else
                    {
                        this.tabHolderView.Visible = true;
                    }
                    this.txtCostAccount.Text = Utils.FilterEndOfTheZeroDecimal(AirModel.Confirmation);
                    this.txtCostParticu.Text = AirModel.CostDetail;
                    this.txtGuidNotes.Text = AirModel.GuideNotes;
                    this.txtOtherMark.Text = AirModel.Remarks;
                    PantMent = ((int)AirModel.PaymentType).ToString();
                    Status = ((int)AirModel.Status).ToString();
                }
            }
        }
        #endregion

        #region 保存飞机信息
        void PageSave()
        {
            #region 表单赋值
            string message = string.Empty;
            string seterrorMsg = string.Empty;
            //出票点
            string votesID = Utils.GetFormValue(this.supplierControl1.ClientValue);
            //出票点id
            string votesName = Utils.GetFormValue(this.supplierControl1.ClientText);
            //联系人 电话 传真
            string contectName = Utils.GetFormValue(this.txtContentName.UniqueID);
            string contectPhone = Utils.GetFormValue(this.txtContentPhone.UniqueID);
            string contectFax = Utils.GetFormValue(this.txtContentFax.UniqueID);
            //出发时间 出发地 目的地 航班号
            string[] startdate = Utils.GetFormValues("txtstartTime");
            //string[] startTime = Utils.GetFormValues("txtstartHours");
            string[] startPlace = Utils.GetFormValues("txtstartPlace");
            string[] endplace = Utils.GetFormValues("txtendPlace");
            string[] filghtNum = Utils.GetFormValues("txtFilghtnumbers");
            //舱位 乘客类型 人数  价格 保险 机建费 附加费 折扣 小计
            string[] seleSpaceType = Utils.GetFormValues("seleSpaceType");
            string[] Passengerstype = Utils.GetFormValues("Passengerstype");
            string[] peopleNums = Utils.GetFormValues("txtpeopleNums");
            string[] prices = Utils.GetFormValues("txtprices");
            //string[] Insurance = Utils.GetFormValues("txtInsurance");
            //string[] jiJianFei = Utils.GetFormValues("txtjiJianFei");
            //string[] FujiaFei = Utils.GetFormValues("txtFujiaFei");
            //string[] Zekou = Utils.GetFormValues("txtZekou");
            string[] TotalPrices = Utils.GetFormValues("txtXiaoJi");
            string[] txtFeiJiHangBanBeiZhu = Utils.GetFormValues("txtFeiJiHangBanBeiZhu");
            //结算费用 费用明细
            decimal totalPrices = Utils.GetDecimal(Utils.GetFormValue(this.txtCostAccount.UniqueID));
            string costDetail = Utils.GetFormValue(this.txtCostParticu.UniqueID);
            //导游需知 其它备注
            string guidNotes = Utils.GetFormValue(this.txtGuidNotes.UniqueID);
            string otherRemark = Utils.GetFormValue(this.txtOtherMark.UniqueID);
            #endregion

            #region 验证
            if (string.IsNullOrEmpty(votesID) && string.IsNullOrEmpty(votesName))
            {
                message += "请选择出票点!<br/>";
            }
            if (startdate.Length > 0)
            {
                for (int i = 0; i < startdate.Length; i++)
                {
                    if (string.IsNullOrEmpty(startdate[i]))
                    {
                        message += "*请选择出发日期!<br/>";
                    }
                }
            }
            /*if (startTime.Length > 0)
            {
                for (int i = 0; i < startTime.Length; i++)
                {
                    if (string.IsNullOrEmpty(startTime[i]))
                    {
                        message += "*请输入出发时间！<br/>";
                    }
                }
            }
            if (startPlace.Length > 0)
            {
                for (int i = 0; i < startPlace.Length; i++)
                {
                    if (string.IsNullOrEmpty(startPlace[i]))
                    {
                        message += "*请输入出发地!<br/>";
                    }
                }
            }
            if (endplace.Length > 0)
            {
                for (int i = 0; i < startPlace.Length; i++)
                {
                    if (string.IsNullOrEmpty(endplace[i]))
                    {
                        message += "*请输入目的地!<br/>";
                    }
                }
            }
            if (filghtNum.Length > 0)
            {
                for (int i = 0; i < filghtNum.Length; i++)
                {
                    if (string.IsNullOrEmpty(filghtNum[i]))
                    {
                        message += "*请输入航班号!<br/>";
                    }
                }
            }

            if (seleSpaceType.Length > 0)
            {
                for (int i = 0; i < seleSpaceType.Length; i++)
                {
                    if (string.IsNullOrEmpty(seleSpaceType[i]))
                    {
                        message += "*请选择舱位!<br/>";
                    }
                }
            }
            if (Passengerstype.Length > 0)
            {
                for (int i = 0; i < Passengerstype.Length; i++)
                {
                    if (string.IsNullOrEmpty(Passengerstype[i]))
                    {
                        message += "*请选择乘客类型!<br/>";
                    }
                }
            }
            if (peopleNums.Length > 0)
            {
                for (int i = 0; i < peopleNums.Length; i++)
                {
                    if (string.IsNullOrEmpty(peopleNums[i]) || Utils.GetDecimal(peopleNums[i]) <= 0)
                    {
                        message += "*请输入人数!<br/>";
                    }
                }
            }
            if (prices.Length > 0)
            {
                for (int i = 0; i < prices.Length; i++)
                {
                    if (string.IsNullOrEmpty(prices[i]) || Utils.GetDecimal(prices[i]) <= 0)
                    {
                        message += "*请输入价格!<br/>";
                    }
                }
            }
            if (Insurance.Length > 0)
            {
                for (int i = 0; i < Insurance.Length; i++)
                {
                    if (string.IsNullOrEmpty(Insurance[i]) || Utils.GetDecimal(Insurance[i]) <= 0)
                    {
                        message += "*请输入保险!<br/>";
                    }
                }
            }
            if (jiJianFei.Length > 0)
            {
                for (int i = 0; i < jiJianFei.Length; i++)
                {
                    if (string.IsNullOrEmpty(jiJianFei[i]) || Utils.GetDecimal(jiJianFei[i]) <= 0)
                    {
                        message += "*请输入机建费!<br/>";
                    }
                }
            }
            if (FujiaFei.Length > 0)
            {
                for (int i = 0; i < FujiaFei.Length; i++)
                {
                    if (string.IsNullOrEmpty(FujiaFei[i]) || Utils.GetDecimal(FujiaFei[i]) <= 0)
                    {
                        message += "*请输入附加费!<br/>";
                    }
                }
            }
            if (Zekou.Length > 0)
            {
                for (int i = 0; i < Zekou.Length; i++)
                {
                    if (string.IsNullOrEmpty(Zekou[i]) || Utils.GetDecimal(Zekou[i]) <= 0)
                    {
                        message += "*请输入折扣!<br/>";
                    }
                }
            }
            if (TotalPrices.Length > 0)
            {
                for (int i = 0; i < TotalPrices.Length; i++)
                {
                    if (string.IsNullOrEmpty(TotalPrices[i]) || Utils.GetDecimal(TotalPrices[i]) <= 0)
                    {
                        message += "*请输入小计费用!<br/>";
                    }
                }
            }*/
            if (string.IsNullOrEmpty(Utils.GetFormValue("SelPamyMent")))
            {
                message += "请选择支付方式！<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("SelStatus")))
            {
                message += "请选择状态!<br/>";
            }
            if (message != "")
            {
                seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + message + "");
                RCWE(seterrorMsg);
            }
            #endregion

            #region 实体赋值
            EyouSoft.Model.PlanStructure.MPlanBaseInfo AirBase = new EyouSoft.Model.PlanStructure.MPlanBaseInfo();
            AirBase.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            AirBase.CompanyId = this.SiteUserInfo.CompanyId;
            AirBase.Confirmation = totalPrices;
            AirBase.ContactFax = contectFax;
            AirBase.ContactName = contectName;
            AirBase.ContactPhone = contectPhone;
            AirBase.CostDetail = costDetail;
            AirBase.GuideNotes = guidNotes;
            AirBase.IssueTime = System.DateTime.Now;
            AirBase.PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)Utils.GetInt(Utils.GetFormValue("SelPamyMent"));
            //航班
            int nums = 0;
            AirBase.PlanLargeTime = new List<EyouSoft.Model.PlanStructure.MPlanLargeTime>();
            for (int i = 0; i < startdate.Length; i++)
            {
                EyouSoft.Model.PlanStructure.MPlanLargeTime largTime = new EyouSoft.Model.PlanStructure.MPlanLargeTime();
                largTime.Departure = startPlace[i];
                largTime.DepartureTime = Utils.GetDateTimeNullable(startdate[i]);
                largTime.Destination = endplace[i];
                largTime.Numbers = filghtNum[i];
                //largTime.Time = startTime[i];
                largTime.AdultsType = (EyouSoft.Model.EnumType.PlanStructure.PlanLargeAdultsType)Enum.Parse(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanLargeAdultsType), Passengerstype[i]);
                //largTime.Discount = float.Parse(Zekou[i]);
                //largTime.Fee = Utils.GetDecimal(jiJianFei[i]);
                //largTime.Insurance = Utils.GetDecimal(Insurance[i]);
                largTime.PayNumber = Utils.GetInt(peopleNums[i]);
                nums += EyouSoft.Common.Utils.GetInt(peopleNums[i]);
                largTime.FarePrice = Utils.GetDecimal(prices[i]);
                largTime.SeatType = (EyouSoft.Model.EnumType.PlanStructure.PlanLargeSeatType)Enum.Parse(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanLargeSeatType), seleSpaceType[i]);
                largTime.SumPrice = Utils.GetDecimal(TotalPrices[i]);
                //largTime.Surcharge = Utils.GetDecimal(FujiaFei[i]);
                largTime.BeiZhu = txtFeiJiHangBanBeiZhu[i];
                AirBase.PlanLargeTime.Add(largTime);
            }
            AirBase.Num = nums;
            AirBase.Remarks = otherRemark;
            AirBase.SourceId = votesID;
            AirBase.SourceName = votesName;
            AirBase.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("SelStatus"));
            AirBase.SueId = "";
            AirBase.TourId = Utils.GetQueryStringValue("tourId");
            AirBase.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机;
            AirBase.PlanCost = totalPrices;
            AirBase.OperatorId = this.SiteUserInfo.UserId;
            AirBase.OperatorName = this.SiteUserInfo.Name;
            #endregion

            #region 提交操作
            if (Utils.GetQueryStringValue("action") == "saveAir")
            {
                string planID = Utils.GetQueryStringValue("planIdAir");
                if (planID != null && !string.IsNullOrEmpty(planID))
                {
                    AirBase.PlanId = planID;
                    if (new EyouSoft.BLL.PlanStructure.BPlan().UpdPlan(AirBase) > 0)
                    {
                        message += "修改成功!";
                        seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + message + "");
                    }
                    else
                    {
                        message += "修改失败！";
                        seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + message + "");
                    }
                }
                else
                {
                    if (new EyouSoft.BLL.PlanStructure.BPlan().AddPlan(AirBase) > 0)
                    {
                        message += "添加成功!";
                        seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + message + "");
                    }
                    else
                    {
                        message += "添加失败！";
                        seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + message + "");
                    }
                }
            }
            RCWE(seterrorMsg);
            #endregion
        }
        #endregion
        #endregion

        #region 火车

        #region 初始化
        /// <summary>
        /// 初始化安排的火车计调项列表
        /// </summary>
        /// <param name="tourID">团号</param>
        protected void DataInit()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            if (!string.IsNullOrEmpty(tourId))
            {
                ListPower = this.panView1.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(tourId, SiteUserInfo.UserId);
                if (ListPower) ListPower = panView1.Visible = Privs_AnPai;

                IList<EyouSoft.Model.PlanStructure.MPlan> trainList = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourId);
                if (trainList != null && trainList.Count > 0)
                {
                    this.reptrainlist.DataSource = trainList;
                    this.reptrainlist.DataBind();
                }
                else
                {
                    this.panShowListSecond.Visible = false;
                }
            }
        }
        #endregion

        # region 删除安排的火车计调项
        /// <summary>
        /// 删除安排的火车计调项
        /// </summary>
        /// <param name="ID">计调id</param>
        /// <returns></returns>
        void DeleteTrain()
        {
            string planId = Utils.GetQueryStringValue("planIdTrainId");
            string msg = "";
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
            RCWE(msg);
        }
        #endregion

        #region 获取安排的火车实体
        /// <summary>
        /// 获取安排的火车实体
        /// </summary>
        /// <param name="ID">计调id</param>
        protected void GetTrainModel()
        {
            string planId = Utils.GetQueryStringValue("planIdTrainId");
            if (!string.IsNullOrEmpty(planId))
            {
                EyouSoft.Model.PlanStructure.MPlanBaseInfo TrainModel = new EyouSoft.BLL.PlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车, planId);
                if (TrainModel != null)
                {
                    this.supplierControl2.HideID = TrainModel.SourceId;
                    this.supplierControl2.Name = TrainModel.SourceName;
                    this.txtContentFax1.Text = TrainModel.ContactFax;
                    this.txtContentName1.Text = TrainModel.ContactName;
                    this.txtContentPhone1.Text = TrainModel.ContactPhone;
                    this.txtCostDetail.Text = TrainModel.CostDetail;
                    if (TrainModel.PlanLargeTime != null && TrainModel.PlanLargeTime.Count > 0)
                    {
                        this.tabHolderView1.Visible = false;
                        this.repcoachlist.DataSource = TrainModel.PlanLargeTime;
                        this.repcoachlist.DataBind();
                    }
                    else
                    {
                        this.tabHolderView1.Visible = true;
                    }
                    this.txtTotalPrices1.Text = Utils.FilterEndOfTheZeroDecimal(TrainModel.Confirmation);
                    this.txtGuidNotes1.Text = TrainModel.GuideNotes;
                    this.txtOtherRemark1.Text = TrainModel.Remarks;
                    PanyMentT = ((int)TrainModel.PaymentType).ToString();
                    StatusT = ((int)TrainModel.Status).ToString();
                }
            }
        }
        #endregion

        #region 保存 火车
        /// <summary>
        /// 火车表单 保存
        /// </summary>
        /// <returns></returns>
        void PageSaveTrain()
        {
            #region 表单赋值
            string msg = string.Empty;
            string seterrorMsg = string.Empty;
            //票务id name
            string trainID = Utils.GetFormValue(this.supplierControl2.ClientValue);
            string trainName = Utils.GetFormValue(this.supplierControl2.ClientText);
            //联系人 电话 传真
            string contectName = Utils.GetFormValue(this.txtContentName1.UniqueID);
            string contectPhone = Utils.GetFormValue(this.txtContentPhone1.UniqueID);
            string contectFax = Utils.GetFormValue(this.txtContentFax1.UniqueID);
            //费用明细
            string costDetailB = Utils.GetFormValue(this.txtCostDetail.UniqueID);
            //班次 出发日期 出发时间 出发地 目的地 车次 座位标准 付费数量 免费数量
            string[] startDate = Utils.GetFormValues("txtStartTimeTrain");
            string[] startTime = Utils.GetFormValues("txtStartHoursTrain");
            string[] startPlace = Utils.GetFormValues("txtStartPlaceTrain");
            string[] endPlace = Utils.GetFormValues("txtendPlaceTrain");
            string[] coachNums = Utils.GetFormValues("txtcoachNumTrain");
            string[] seatStandTrain = Utils.GetFormValues("txtseatStandTrain");
            string[] payNums = Utils.GetFormValues("txtpayNumTrain");
            //string[] freenums = Utils.GetFormValues("txtFreeNumTrain");
            string[] txtHuoCheBeiZhu = Utils.GetFormValues("txtHuoCheBeiZhu");
            string[] txtHuoCheDanJia = Utils.GetFormValues("txtHuoCheDanJia");
            string[] txtHuoCheXiaoJi = Utils.GetFormValues("txtHuoCheXiaoJi");
            //结算费用
            decimal totalMoney = Utils.GetDecimal(Utils.GetFormValue(this.txtTotalPrices1.UniqueID));
            //导游需知  其它备注
            string guidNotes = Utils.GetFormValue("txtGuidNotes1");
            string otherRemrk = Utils.GetFormValue("txtOtherRemark1");
            #endregion

            #region 后台验证
            if (string.IsNullOrEmpty(trainID) && string.IsNullOrEmpty(trainName))
            {
                msg += "请选择出票点!<br/>";
            }
            if (string.IsNullOrEmpty(costDetailB))
            {
                msg += "请输入费用明细!<br/>";
            }
            if (startDate.Length > 0)
            {
                for (int i = 0; i < startDate.Length; i++)
                {
                    if (string.IsNullOrEmpty(startDate[i]))
                    {
                        msg += "请输入出发日期!<br/>";
                    }
                }
            }
            /*if (startTime.Length > 0)
            {
                for (int i = 0; i < startTime.Length; i++)
                {
                    if (string.IsNullOrEmpty(startTime[i]))
                    {
                        msg += "请输入出发时间！<br/>";
                    }
                }
            }
            if (startPlace.Length > 0)
            {
                for (int i = 0; i < startPlace.Length; i++)
                {
                    if (string.IsNullOrEmpty(startPlace[i]))
                    {
                        msg += "请输入出发地!<br/>";
                    }
                }
            }
            if (endPlace.Length > 0)
            {
                for (int i = 0; i < endPlace.Length; i++)
                {
                    if (string.IsNullOrEmpty(endPlace[i]))
                    {
                        msg += "请输入目的地！<br/>";
                    }
                }
            }
            if (coachNums.Length > 0)
            {
                for (int i = 0; i < coachNums.Length; i++)
                {
                    if (string.IsNullOrEmpty(coachNums[i]))
                    {
                        msg += "请输入车次!<br/>";
                    }
                }
            }
            if (seatStandTrain.Length > 0)
            {
                for (int i = 0; i < seatStandTrain.Length; i++)
                {
                    if (string.IsNullOrEmpty(seatStandTrain[i]))
                    {
                        msg += "请输入座位标准!<br/>";
                    }
                }
            }
            if (payNums.Length > 0)
            {
                for (int i = 0; i < payNums.Length; i++)
                {
                    if (string.IsNullOrEmpty(payNums[i]) || Utils.GetDecimal(payNums[i]) <= 0)
                    {
                        msg += "请输入付费数量!<br/>";
                    }
                }
            }
            if (freenums.Length > 0)
            {
                for (int i = 0; i < freenums.Length; i++)
                {
                    if (!string.IsNullOrEmpty(freenums[i]) && Utils.GetInt(freenums[i]) < 0)
                    {
                        msg += "请输入免费数量!<br/>";
                    }
                }
            }
            if (totalMoney <= 0)
            {
                msg += "请输入结算费用！<br/>";
            }*/
            if (string.IsNullOrEmpty(Utils.GetFormValue("SelPanyMentT")))
            {
                msg += "请选择支付方式!<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("SelStatusT")))
            {
                msg += "请选择状态！<br/>";
            }
            if (msg != "")
            {
                seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                RCWE(seterrorMsg);
            }
            #endregion

            #region 实体赋值
            EyouSoft.Model.PlanStructure.MPlanBaseInfo trainBase = new EyouSoft.Model.PlanStructure.MPlanBaseInfo();
            trainBase.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            trainBase.CompanyId = this.SiteUserInfo.CompanyId;
            trainBase.Confirmation = totalMoney;
            trainBase.ContactFax = contectFax;
            trainBase.ContactName = contectName;
            trainBase.ContactPhone = contectPhone;
            trainBase.GuideNotes = guidNotes;
            trainBase.IssueTime = System.DateTime.Now;
            int nums = 0;
            trainBase.PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)Utils.GetInt(Utils.GetFormValue("SelPanyMentT"));
            trainBase.PlanLargeTime = new List<EyouSoft.Model.PlanStructure.MPlanLargeTime>();
            for (int i = 0; i < startDate.Length; i++)
            {
                EyouSoft.Model.PlanStructure.MPlanLargeTime timeModel = new EyouSoft.Model.PlanStructure.MPlanLargeTime();
                nums += EyouSoft.Common.Utils.GetInt(payNums[i]);
                timeModel.Departure = startPlace[i];
                timeModel.DepartureTime = Utils.GetDateTimeNullable(startDate[i]);
                timeModel.Destination = endPlace[i];
                //timeModel.FreeNumber = EyouSoft.Common.Utils.GetInt(freenums[i]);
                timeModel.Numbers = coachNums[i];
                timeModel.PayNumber = EyouSoft.Common.Utils.GetInt(payNums[i]);
                timeModel.SeatStandard = seatStandTrain[i];
                timeModel.Time = startTime[i];
                timeModel.BeiZhu = txtHuoCheBeiZhu[i];
                timeModel.SumPrice = Utils.GetDecimal(txtHuoCheXiaoJi[i]);
                timeModel.FarePrice = Utils.GetDecimal(txtHuoCheDanJia[i]);

                trainBase.PlanLargeTime.Add(timeModel);                
            }
            trainBase.Num = nums;
            trainBase.Remarks = otherRemrk;
            trainBase.SourceId = trainID;
            trainBase.SourceName = trainName;
            trainBase.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("SelStatusT"));
            trainBase.SueId = "";
            trainBase.TourId = Utils.GetQueryStringValue("tourId");
            trainBase.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车;
            trainBase.PlanCost = totalMoney;
            trainBase.CostDetail = costDetailB;
            trainBase.OperatorId = this.SiteUserInfo.UserId;
            trainBase.OperatorName = this.SiteUserInfo.Name;
            #endregion

            #region 提交操作
            if (Utils.GetQueryStringValue("action") == "saveTrain")
            {
                string planId = Utils.GetQueryStringValue("planIdTrainId");
                if (!string.IsNullOrEmpty(planId))
                {
                    trainBase.PlanId = planId;
                    if (new EyouSoft.BLL.PlanStructure.BPlan().UpdPlan(trainBase) > 0)
                    {
                        msg += "修改成功！";
                        seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                    }
                    else
                    {
                        msg += "修改失败！";
                        seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                    }
                }
                else
                {
                    if (new EyouSoft.BLL.PlanStructure.BPlan().AddPlan(trainBase) > 0)
                    {
                        msg += "添加成功！";
                        seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                    }
                    else
                    {
                        msg += "添加失败！";
                        seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                    }
                }
            }
            RCWE(seterrorMsg);
            #endregion

        }
        #endregion

        #endregion

        #region 汽车

        #region 已安排的汽车
        /// <summary>
        /// 绑定已安排的汽车列表
        /// </summary>
        /// <param name="tourID">团号</param>
        protected void DataInitBus()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            if (!string.IsNullOrEmpty(tourId))
            {
                ListPower = this.panView2.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(tourId, SiteUserInfo.UserId);
                if (ListPower) ListPower = panView2.Visible = Privs_AnPai;

                IList<EyouSoft.Model.PlanStructure.MPlan> listBus = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourId);
                if (listBus != null && listBus.Count > 0)
                {
                    this.repBuslist.DataSource = listBus;
                    this.repBuslist.DataBind();
                }
                else
                {
                    this.panShowListThird.Visible = false;
                }
            }
        }
        #endregion

        #region 删除汽车
        /// <summary>
        /// 删除汽车
        /// </summary>
        /// <returns></returns>
        void deleteBus()
        {
            string planId = Utils.GetQueryStringValue("planIdBus");
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(planId))
            {
                if (new EyouSoft.BLL.PlanStructure.BPlan().DelPlan(planId))
                {
                    msg = UtilsCommons.AjaxReturnJson("1", "删除成功!");
                }
                else
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "删除失败!");
                }
            }
            RCWE(msg);
        }
        #endregion

        #region 获取汽车实体
        /// <summary>
        /// 获取汽车实体
        /// </summary>
        /// <param name="PlanID">计调id</param>
        protected void GetModelBus()
        {
            string planId = Utils.GetQueryStringValue("palnIdBus");
            if (!string.IsNullOrEmpty(planId))
            {
                EyouSoft.Model.PlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.BLL.PlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车, planId);
                if (baseinfo != null)
                {
                    this.supplierControl3.HideID = baseinfo.SourceId;
                    this.supplierControl3.Name = baseinfo.SourceName;
                    this.txtContentName2.Text = baseinfo.ContactName;
                    this.txtContentFax2.Text = baseinfo.ContactFax;
                    this.txtContentPhone2.Text = baseinfo.ContactPhone;
                    //班次 出发时间 点时间 车牌号 出发地 目的地 价格 张数
                    if (baseinfo.PlanLargeTime != null && baseinfo.PlanLargeTime.Count > 0)
                    {
                        this.tabHolderView2.Visible = false;
                        this.repcoachlist1.DataSource = baseinfo.PlanLargeTime;
                        this.repcoachlist1.DataBind();
                    }
                    else
                    {
                        this.tabHolderView2.Visible = true;
                    }
                    //结算费用
                    this.txtTotalPricesBus.Text = Utils.FilterEndOfTheZeroDecimal(baseinfo.Confirmation);
                    //费用明细
                    this.txtcostDetailbus.Text = baseinfo.CostDetail;
                    //导游需知
                    this.txtGuidNotes2.Text = baseinfo.GuideNotes;
                    //其它备注
                    this.txtOtherRemark2.Text = baseinfo.Remarks;
                    //支付方式 状态                    
                    panyMentB = ((int)baseinfo.PaymentType).ToString();
                    StatusB = ((int)baseinfo.Status).ToString();

                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 汽车表单保存
        /// </summary>
        /// <returns></returns>
        void PageSaveBus()
        {
            #region 表单赋值
            string msg = string.Empty;
            string seterrorMsg = string.Empty;
            //出票点
            string busVotesID = Utils.GetFormValue(this.supplierControl3.ClientValue);
            string busVotesName = Utils.GetFormValue(this.supplierControl3.ClientText);
            //联系人 电话 传真
            string contectName = Utils.GetFormValue(this.txtContentName2.UniqueID);
            string contectPhone = Utils.GetFormValue(this.txtContentPhone2.UniqueID);
            string contectFax = Utils.GetFormValue(this.txtContentFax2.UniqueID);
            //班次 出发时间 点时间 车牌号 出发地 目的地 价格 张数
            string[] startDate = Utils.GetFormValues("txtStartTimeBus");
            //string[] startTime = Utils.GetFormValues("txtStartHoursBus");
            //string[] carNums = Utils.GetFormValues("txtCarNumBus");
            string[] startPlace = Utils.GetFormValues("txtStartPlaceBus");
            string[] endplace = Utils.GetFormValues("txtUnderPlaceBus");
            string[] priceBus = Utils.GetFormValues("txtPricesBus");
            string[] busNums = Utils.GetFormValues("txtTicketNumBus");
            string[] txtQiCheXiaoJi = Utils.GetFormValues("txtQiCheXiaoJi");
            string[] txtQiCheBeiZhu = Utils.GetFormValues("txtQiCheBeiZhu");
            //结算费用 费用明细 
            decimal totalPrices = Utils.GetDecimal(Utils.GetFormValue(this.txtTotalPricesBus.UniqueID));
            string costDetail = Utils.GetFormValue(this.txtcostDetailbus.UniqueID);
            //导游需知 其它备注
            string guidNotes = Utils.GetFormValue(this.txtGuidNotes2.UniqueID);
            string otherRemark = Utils.GetFormValue(this.txtOtherRemark2.UniqueID);

            #endregion

            #region 后台验证
            if (string.IsNullOrEmpty(busVotesID) && string.IsNullOrEmpty(busVotesName))
            {
                msg += "请选择出票点!<br/>";
            }
            if (startDate.Length > 0)
            {
                for (int i = 0; i < startDate.Length; i++)
                {
                    if (string.IsNullOrEmpty(startDate[i]))
                    {
                        msg += "请输入出发日期!<br/>";
                    }
                }
            }
            /*if (startTime.Length > 0)
            {
                for (int i = 0; i < startTime.Length; i++)
                {
                    if (string.IsNullOrEmpty(startTime[i]))
                    {
                        msg += "请输入出发时间!<br/>";
                    }
                }
            }
            if (carNums.Length > 0)
            {
                for (int i = 0; i < carNums.Length; i++)
                {
                    if (string.IsNullOrEmpty(carNums[i]))
                    {
                        msg += "请输入车牌号!<br/>";
                    }
                }
            }
            if (startPlace.Length > 0)
            {
                for (int i = 0; i < startPlace.Length; i++)
                {
                    if (string.IsNullOrEmpty(startPlace[i]))
                    {
                        msg += "请输入出发地!<br/>";
                    }
                }
            }
            if (endplace.Length > 0)
            {
                for (int i = 0; i < endplace.Length; i++)
                {
                    if (string.IsNullOrEmpty(endplace[i]))
                    {
                        msg += "请输入目的地!<br/>";
                    }
                }
            }
            if (priceBus.Length > 0)
            {
                for (int i = 0; i < priceBus.Length; i++)
                {
                    if (Utils.GetDecimal(priceBus[i]) <= 0 || string.IsNullOrEmpty(priceBus[i]))
                    {
                        msg += "请输入车票价格！<br/>";
                    }
                }
            }
            if (busNums.Length > 0)
            {
                for (int i = 0; i < busNums.Length; i++)
                {
                    if (string.IsNullOrEmpty(busNums[i]) || Utils.GetDecimal(busNums[i]) <= 0)
                    {
                        msg += "请输入票数！<br/>";
                    }
                }
            }
            //结算费用
            if (totalPrices <= 0)
            {
                msg += "请输入结算费用!<br/>";
            }*/
            //支付方式 状态
            if (string.IsNullOrEmpty(Utils.GetFormValue("SelPanyMentB")))
            {
                msg += "请选择支付方式!<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("selStatusB")))
            {
                msg += "请选择状态!<br/>";
            }
            if (msg != "")
            {
                seterrorMsg = "{\"result\":\"0\",\"msg\":\"" + msg + "\"}";
                RCWE(seterrorMsg);
            }
            #endregion

            #region 实体赋值
            EyouSoft.Model.PlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.Model.PlanStructure.MPlanBaseInfo();
            baseinfo.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            baseinfo.CompanyId = this.SiteUserInfo.CompanyId;
            baseinfo.Confirmation = totalPrices;
            baseinfo.PlanCost = totalPrices;
            baseinfo.ContactFax = contectFax;
            baseinfo.ContactName = contectName;
            baseinfo.ContactPhone = contectPhone;
            baseinfo.GuideNotes = guidNotes;
            baseinfo.IssueTime = System.DateTime.Now;
            baseinfo.PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)Utils.GetInt(Utils.GetFormValue("SelPanyMentB"));
            int nums = 0;
            baseinfo.PlanLargeTime = new List<EyouSoft.Model.PlanStructure.MPlanLargeTime>();
            for (int i = 0; i < startDate.Length; i++)
            {
                EyouSoft.Model.PlanStructure.MPlanLargeTime time = new EyouSoft.Model.PlanStructure.MPlanLargeTime();
                time.DepartureTime = Utils.GetDateTimeNullable(startDate[i]);
                //time.Time = startTime[i];
                //time.Numbers = carNums[i];
                time.Departure = startPlace[i];
                time.Destination = endplace[i];
                time.PayNumber = EyouSoft.Common.Utils.GetInt(busNums[i]);
                nums += EyouSoft.Common.Utils.GetInt(busNums[i]);
                time.FarePrice = EyouSoft.Common.Utils.GetDecimal(priceBus[i]);
                time.SumPrice = Utils.GetDecimal(txtQiCheXiaoJi[i]);
                time.BeiZhu = txtQiCheBeiZhu[i];
                baseinfo.PlanLargeTime.Add(time);
            }
            baseinfo.Num = nums;
            baseinfo.Remarks = otherRemark;
            baseinfo.SourceId = busVotesID;
            baseinfo.SourceName = busVotesName;
            baseinfo.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("selStatusB"));
            baseinfo.SueId = "";
            baseinfo.TourId = Utils.GetQueryStringValue("tourId");
            baseinfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车;
            baseinfo.OperatorId = this.SiteUserInfo.UserId;
            baseinfo.OperatorName = this.SiteUserInfo.Name;
            baseinfo.CostDetail = costDetail;
            #endregion

            #region 提交操作
            if (Utils.GetQueryStringValue("action") == "saveBus")
            {
                string planID = Utils.GetQueryStringValue("palnIdBus");
                if (!string.IsNullOrEmpty(planID))
                {
                    baseinfo.PlanId = planID;
                    if (new EyouSoft.BLL.PlanStructure.BPlan().UpdPlan(baseinfo) > 0)
                    {
                        msg += "修改成功!<br/>";
                        seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                    }
                    else
                    {
                        msg += "修改失败!<br/>";
                        seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                    }
                }
                else
                {
                    if (new EyouSoft.BLL.PlanStructure.BPlan().AddPlan(baseinfo) > 0)
                    {
                        msg += "添加成功!<br/>";
                        seterrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                    }
                    else
                    {
                        msg += "添加失败!<br/>";
                        seterrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                    }
                }
            #endregion
            }
            RCWE(seterrorMsg);
        }
        #endregion

        #endregion

        #region  权限判断
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
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排大交通))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排大交通, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排大交通);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排大交通))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排大交通, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排大交通);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排大交通))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排大交通, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排大交通);
                    break;
            }
        }
        #endregion
    }
}
