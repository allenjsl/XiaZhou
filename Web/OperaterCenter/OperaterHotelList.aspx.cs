using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.IO;

namespace Web.OperaterCenter
{
    /// <summary>
    /// 计调中心-出境,地接- 酒店安排
    /// 创建人：李晓欢
    /// 创建时间：2011-09-08
    /// </summary>
    public partial class OperaterHotelList : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        //支付方式 状态
        protected string PanyMent = string.Empty;
        protected string Status = string.Empty;
        protected string SourceID = string.Empty;
        //预控编号
        protected string SueID = string.Empty;
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
            InitDropDownList();
            EyouSoft.BLL.SysStructure.BSys bsys = new EyouSoft.BLL.SysStructure.BSys();
            if (bsys.IsExistsMenu2(this.SiteUserInfo.SysId, EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源预控_酒店预控))
            {
                this.supplierControl1.Flag = 1;
            }
            querenUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.酒店确认单);

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
                        Response.Write(DelHotel());
                        Response.End();
                        break;
                    case "update":
                        GetHotelModel();
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
        /// <param name="tourID"></param>
        protected void DataInit()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            //绑定已安排酒店列表数据 
            if (!string.IsNullOrEmpty(tourId))
            {
                ListPower = this.panView.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(tourId, SiteUserInfo.UserId);
                if (ListPower) ListPower = panView.Visible = Privs_AnPai;

                this.supplierControl1.TourID = tourId;
                IList<EyouSoft.Model.PlanStructure.MPlan> hotelList = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourId);
                if (hotelList != null && hotelList.Count > 0)
                {
                    this.repHotellist.DataSource = hotelList;
                    this.repHotellist.DataBind();
                }
                else
                {
                    this.phdShowList.Visible = false;
                }
            }

        }
        #endregion

        #region 绑定下拉框
        protected void InitDropDownList()
        {
            //是否含早
            this.ddlContainsEarly.Items.Clear();
            List<EnumObj> listIsMeal = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanHotelIsMeal));
            if (listIsMeal != null && listIsMeal.Count > 0)
            {
                for (int k = 0; k < listIsMeal.Count; k++)
                {
                    ListItem ismeal = new ListItem();
                    ismeal.Text = listIsMeal[k].Text;
                    ismeal.Value = listIsMeal[k].Value;
                    this.ddlContainsEarly.Items.Add(ismeal);
                }
            }

            //酒店星级
            this.ddlHotelStart.Items.Clear();
            List<EnumObj> start = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SourceStructure.HotelStar));
            if (start != null && start.Count > 0)
            {
                for (int i = 0; i < start.Count; i++)
                {
                    ListItem st = new ListItem();
                    st.Text = start[i].Text;
                    st.Value = start[i].Value;
                    if (start[i].Value == Utils.GetFormValue(this.hotelStar.UniqueID))
                    {
                        st.Selected = true;
                    }
                    this.ddlHotelStart.Items.Add(st);
                }
            }
        }
        #endregion

        #region 酒店房型计算单位
        /// <summary>
        /// 酒店房型计算单位
        /// </summary>
        /// <param name="selectindex"></param>
        /// <returns></returns>
        protected string Getcalculate(string selectindex)
        {
            System.Text.StringBuilder calculateHtml = new System.Text.StringBuilder();
            List<EnumObj> roomUnitprices = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanHotelPriceType));
            if (roomUnitprices.Count > 0 && roomUnitprices != null)
            {
                for (int i = 0; i < roomUnitprices.Count; i++)
                {
                    if (selectindex == roomUnitprices[i].Value)
                    {
                        calculateHtml.Append("<option value='" + roomUnitprices[i].Value + "' selected='selected'>元/" + roomUnitprices[i].Text + "</option>");
                    }
                    else
                    {
                        calculateHtml.Append("<option value='" + roomUnitprices[i].Value + "'>元/" + roomUnitprices[i].Text + "</option>");
                    }
                }
            }
            return calculateHtml.ToString();
        }
        #endregion

        #region 删除酒店方法
        protected string DelHotel()
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

        #region 获取酒店实体
        /// <summary>
        /// 获取酒店实体
        /// </summary>
        protected void GetHotelModel()
        {
            string planId = Utils.GetQueryStringValue("planId");
            if (!string.IsNullOrEmpty(planId))
            {
                EyouSoft.Model.PlanStructure.MPlanBaseInfo baseInfo = new EyouSoft.BLL.PlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店, planId);
                if (baseInfo != null)
                {
                    SourceID = baseInfo.SourceId;
                    this.supplierControl1.HideID = baseInfo.SourceId;
                    this.supplierControl1.Name = baseInfo.SourceName;
                    if (!string.IsNullOrEmpty(baseInfo.SueId.Trim()))
                    {
                        this.supplierControl1.HideID_zyyk = baseInfo.SueId;
                        this.supplierControl1.isyukong = "1";
                        SueID = baseInfo.SueId;
                        EyouSoft.Model.SourceStructure.MSourceSueHotel sueHotel = new EyouSoft.BLL.SourceStructure.BSourceControl().GetModelByHotelId(baseInfo.SueId, this.SiteUserInfo.CompanyId);
                        if (sueHotel != null)
                        {
                            this.hidUserNum.Value = (sueHotel.ControlNum - sueHotel.AlreadyNum + baseInfo.Num).ToString();
                        }
                    }
                    else
                    {
                        this.supplierControl1.isyukong = "0";
                    }

                    if (!string.IsNullOrEmpty(baseInfo.SourceId))
                    {
                        EyouSoft.Model.SourceStructure.MSourceHotel HotelModel = new EyouSoft.BLL.SourceStructure.BSource().GetOneHotelModel(baseInfo.SourceId);
                        if (HotelModel != null)
                        {
                            if (HotelModel.HotelRoomList != null && HotelModel.HotelRoomList.Count > 0)
                            {
                                for (int i = 0; i < HotelModel.HotelRoomList.Count; i++)
                                {
                                    this.hidroomTypePrices.Value += "" + HotelModel.HotelRoomList[i].RoomId + "," + EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(HotelModel.HotelRoomList[i].PricePJ) + "|";
                                }
                            }
                        }
                    }

                    if (baseInfo.PlanHotel != null)
                    {
                        this.ddlHotelStart.Items.FindByValue(((int)baseInfo.PlanHotel.Star).ToString()).Selected = true;
                        this.txtroomDays.Text = baseInfo.PlanHotel.Days.ToString();

                        if (baseInfo.PlanHotel.PlanHotelRoomList != null && baseInfo.PlanHotel.PlanHotelRoomList.Count > 0)
                        {
                            this.holderView.Visible = false;
                            IList<EyouSoft.Model.PlanStructure.MPlanHotelRoom> hotelRoom = baseInfo.PlanHotel.PlanHotelRoomList;
                            if (hotelRoom != null && hotelRoom.Count > 0)
                            {
                                this.reproomtypelist.DataSource = hotelRoom;
                                this.reproomtypelist.DataBind();
                            }
                        }
                        else
                        {
                            this.holderView.Visible = true;
                        }
                        this.txtFreRoomNumber.Text = baseInfo.PlanHotel.FreeNumber.ToString();
                        this.txtunitPricesEarly.Text = Utils.FilterEndOfTheZeroDecimal(baseInfo.PlanHotel.MealPrice);
                        this.txtPeopleNumEarly.Text = baseInfo.PlanHotel.MealNumber.ToString();
                        this.txtsequenceNumEarly.Text = baseInfo.PlanHotel.MealFrequency.ToString();
                        this.ddlContainsEarly.Items.FindByValue(((int)baseInfo.PlanHotel.IsMeal).ToString()).Selected = true;

                        txtQianTaiTelephone.Text = baseInfo.PlanHotel.QianTaiTelephone;
                    }

                    this.txtContectName.Text = baseInfo.ContactName;
                    this.txtContectPhone.Text = baseInfo.ContactPhone;
                    this.txtContectFax.Text = baseInfo.ContactFax;
                    this.txtStartTime.Text = UtilsCommons.GetDateString(baseInfo.StartDate, ProviderToDate);
                    this.txtEndTime.Text = UtilsCommons.GetDateString(baseInfo.EndDate, ProviderToDate);
                    this.txtPayRoomNumbers.Text = baseInfo.Num.ToString();
                    this.txtTotalPrices.Text = Utils.FilterEndOfTheZeroDecimal(baseInfo.Confirmation);
                    PanyMent = ((int)baseInfo.PaymentType).ToString();
                    this.txtCostParticu.Text = baseInfo.CostDetail;
                    this.txtGuidNotes.Text = baseInfo.GuideNotes;
                    this.txtOtherRemark.Text = baseInfo.Remarks;
                    this.Selprofit1.Items.FindByValue(baseInfo.IsRebate == true ? "0" : "1").Selected = true;
                    Status = ((int)baseInfo.Status).ToString();
                }
            }
        }

        /// <summary>
        /// 根据供应商商id获取所有的酒店房型
        /// </summary>
        /// <param name="sourceID"></param>
        /// <returns></returns>
        protected string GetRoomType(string sourceID, string roomId, string sueID)
        {
            System.Text.StringBuilder sbRoomType = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(sueID))
            {
                EyouSoft.Model.SourceStructure.MSourceSueHotel hotelSue = new EyouSoft.BLL.SourceStructure.BSourceControl().GetModelByHotelId(sueID, this.SiteUserInfo.CompanyId);
                if (hotelSue != null)
                {
                    sbRoomType.Append("<option value='" + hotelSue.RoomId + "," + hotelSue.RoomType + "'>" + hotelSue.RoomType + "</option>");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(sourceID))
                {
                    EyouSoft.Model.SourceStructure.MSourceHotel HotelModel = new EyouSoft.BLL.SourceStructure.BSource().GetOneHotelModel(sourceID);
                    if (HotelModel != null)
                    {
                        if (HotelModel.HotelRoomList != null && HotelModel.HotelRoomList.Count > 0)
                        {
                            for (int i = 0; i < HotelModel.HotelRoomList.Count; i++)
                            {
                                if (roomId == HotelModel.HotelRoomList[i].RoomId)
                                {
                                    sbRoomType.Append("<option selected='selected' value='" + HotelModel.HotelRoomList[i].RoomId + "," + HotelModel.HotelRoomList[i].TypeName + "'>" + HotelModel.HotelRoomList[i].TypeName + "</option>");
                                }
                                else
                                {
                                    sbRoomType.Append("<option value='" + HotelModel.HotelRoomList[i].RoomId + "," + HotelModel.HotelRoomList[i].TypeName + "'>" + HotelModel.HotelRoomList[i].TypeName + "</option>");
                                }
                            }
                        }
                    }
                }
            }
            return sbRoomType.ToString();
        }
        #endregion

        #region 权限
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
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排酒店))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排酒店, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排酒店);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排酒店))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排酒店, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排酒店);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排酒店))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排酒店, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排酒店);
                    break;
            }
        }
        #endregion

        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected string PageSave()
        {
            #region 表单赋值
            string setsrrorMsg = string.Empty;
            string msg = string.Empty;
            //酒店名称
            string hotelName = Utils.GetFormValue(this.supplierControl1.ClientText);
            //酒店id
            string hotelId = Utils.GetFormValue(this.supplierControl1.ClientValue);
            //星级
            string hotelStart = Utils.GetFormValue(this.ddlHotelStart.UniqueID);
            //联系人 联系电话 联系传真
            string contectName = Utils.GetFormValue(this.txtContectName.UniqueID);
            string contectPhone = Utils.GetFormValue(this.txtContectPhone.UniqueID);
            string contectFax = Utils.GetFormValue(this.txtContectFax.UniqueID);
            //入住时间 离店时间 天数
            DateTime? startTime = Utils.GetDateTimeNullable(Utils.GetFormValue(this.txtStartTime.UniqueID));
            DateTime? endTime = Utils.GetDateTimeNullable(Utils.GetFormValue(this.txtEndTime.UniqueID));
            string days = Utils.GetFormValue(this.txtroomDays.UniqueID);
            //房型 单价 计算方式 数量 小计 
            string[] roomType = Utils.GetFormValues("ddlRoomType");
            string[] unitPirces = Utils.GetFormValues("txtunitPrice");
            string[] selectType = Utils.GetFormValues("select");
            string[] numbers = Utils.GetFormValues("txtRoomNumber");
            string[] roomItemDays = Utils.GetFormValues("txtRoomItemDays");
            string[] TotalMoney = Utils.GetFormValues("txtTotalMoney");
            string[] txtCheckInDate = Utils.GetFormValues("txtCheckInDate");
            string[] txtCheckOutDate = Utils.GetFormValues("txtCheckOutDate");

            //付房数量 免房数量
            string payRoomNum = Utils.GetFormValue(this.txtPayRoomNumbers.UniqueID);
            string FreRoomNum = Utils.GetFormValue(this.txtFreRoomNumber.UniqueID);
            //是否含早
            string ContainsEarly = Utils.GetFormValue(this.ddlContainsEarly.UniqueID);
            //早餐费用 单价 人数 次数
            decimal unitPricesEarly = Utils.GetDecimal(Utils.GetFormValue(this.txtunitPricesEarly.UniqueID));
            string earlyPeopleNum = Utils.GetFormValue(this.txtPeopleNumEarly.UniqueID);
            string sequenceNumEarly = Utils.GetFormValue(this.txtsequenceNumEarly.UniqueID);
            //结算费用 费用明细
            decimal totalMoney = Utils.GetDecimal(Utils.GetFormValue(this.txtTotalPrices.UniqueID));
            string CostParticu = Utils.GetFormValue(this.txtCostParticu.UniqueID);

            //导游须知 其它备注
            string guidNotes = Utils.GetFormValue(this.txtGuidNotes.UniqueID);
            string otherMarks = Utils.GetFormValue(this.txtOtherRemark.UniqueID);
            //返利 状态
            bool profit = Utils.GetFormValue(this.Selprofit1.UniqueID) == "0" ? true : false;
            #endregion

            #region 后台验证
            if (string.IsNullOrEmpty(hotelName) && string.IsNullOrEmpty(hotelId))
            {
                msg += "请选择酒店名称!<br/>";
            }
            if (string.IsNullOrEmpty(startTime.ToString()))
            {
                msg += "请填写入住时间!<br/>";
            }
            if (string.IsNullOrEmpty(endTime.ToString()))
            {
                msg += "请填写离店时间!<br/>";
            }
            if (string.IsNullOrEmpty(days))
            {
                msg += "请填写入住天数!<br/>";
            }

            if (roomType.Length > 0)
            {
                for (int i = 0; i < roomType.Length; i++)
                {
                    if (string.IsNullOrEmpty(roomType[i]))
                    {
                        msg += "第" + (i + 1) + "行请选择房型!<br/>";
                    }
                }
            }

            if (unitPirces.Length > 0)
            {
                for (int i = 0; i < unitPirces.Length; i++)
                {
                    if (string.IsNullOrEmpty(unitPirces[i]))
                    {
                        msg += "第" + (i + 1) + "行请输入单价！<br/>";
                    }
                }
            }
            if (numbers.Length > 0)
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    if (string.IsNullOrEmpty(numbers[i]))
                    {
                        msg += "第" + (i + 1) + "行请输入数量!<br/>";
                    }
                }
            }
            string[] totalPrices = Utils.GetFormValues("txtTotalMoney");
            if (totalPrices.Length > 0)
            {
                for (int i = 0; i < totalPrices.Length; i++)
                {
                    if (string.IsNullOrEmpty(totalPrices[i]) && Utils.GetDecimal(totalPrices[i]) <= 0)
                    {
                        msg += "第" + totalPrices[i] + "行请输入小计费用!<br/>";
                    }
                }
            }

            if (string.IsNullOrEmpty(ContainsEarly))
            {
                msg += "请选择是否含早餐!<br/>";
            }

            if (totalMoney <= 0)
            {
                msg += "请填写结算费用!<br/>";
            }

            if (profit.ToString() == "-1")
            {
                msg += "请选择是否返利!<br/>";
            }
            string status = Utils.GetFormValue("SelStatus");
            if (string.IsNullOrEmpty(status))
            {
                msg += "请选择状态！<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("SelPanyMent")))
            {
                msg += "请选择支付方式！<br/>";
            }
            if (msg != "")
            {
                setsrrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                return setsrrorMsg;
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
            baseinfo.EndDate = endTime;
            baseinfo.GuideNotes = guidNotes;
            baseinfo.IsRebate = profit;
            baseinfo.IssueTime = System.DateTime.Now;
            baseinfo.Num = Utils.GetInt(payRoomNum);
            baseinfo.PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)Utils.GetInt(Utils.GetFormValue("SelPanyMent"));
            baseinfo.PlanHotel = new EyouSoft.Model.PlanStructure.MPlanHotel();
            baseinfo.PlanHotel.Days = Utils.GetInt(days);
            baseinfo.PlanHotel.FreeNumber = Utils.GetInt(FreRoomNum);
            baseinfo.PlanHotel.IsMeal = (EyouSoft.Model.EnumType.PlanStructure.PlanHotelIsMeal)Enum.Parse(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanHotelIsMeal), ContainsEarly);
            baseinfo.PlanHotel.MealFrequency = Utils.GetInt(sequenceNumEarly);
            baseinfo.PlanHotel.MealNumber = Utils.GetInt(earlyPeopleNum);
            baseinfo.PlanHotel.MealPrice = unitPricesEarly;
            baseinfo.PlanHotel.Star = (EyouSoft.Model.EnumType.SourceStructure.HotelStar)Enum.Parse(typeof(EyouSoft.Model.EnumType.SourceStructure.HotelStar), hotelStart);
            baseinfo.PlanHotel.QianTaiTelephone = Utils.GetFormValue(txtQianTaiTelephone.UniqueID);
            baseinfo.PlanHotel.PlanHotelRoomList = new List<EyouSoft.Model.PlanStructure.MPlanHotelRoom>();
            for (int i = 0; i < roomType.Length; i++)
            {
                var roomHotel = new EyouSoft.Model.PlanStructure.MPlanHotelRoom();

                roomHotel.RoomType = roomType[i].Split(',')[1];
                roomHotel.RoomId = roomType[i].Split(',')[0];
                roomHotel.UnitPrice = Utils.GetDecimal(unitPirces[i]);
                roomHotel.Days = Utils.GetInt(roomItemDays[i]);
                roomHotel.PriceType = (EyouSoft.Model.EnumType.PlanStructure.PlanHotelPriceType)Enum.Parse(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanHotelPriceType), selectType[i]);
                roomHotel.Quantity = Utils.GetInt(numbers[i]);
                roomHotel.Total = Utils.GetDecimal(TotalMoney[i]);
                roomHotel.CheckInDate = Utils.GetDateTimeNullable(txtCheckInDate[i]);
                roomHotel.CheckOutDate = Utils.GetDateTimeNullable(txtCheckOutDate[i]);

                baseinfo.PlanHotel.PlanHotelRoomList.Add(roomHotel);
            }
            baseinfo.Remarks = otherMarks;
            baseinfo.SourceId = hotelId;
            baseinfo.SourceName = hotelName;
            baseinfo.StartDate = startTime;
            baseinfo.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("SelStatus"));
            if (Utils.GetFormValue(this.supplierControl1.ClientIsyukong) == "1")
            {
                baseinfo.SueId = Utils.GetFormValue(this.supplierControl1.ClientzyykValue);
            }
            baseinfo.TourId = Utils.GetQueryStringValue("tourId");
            baseinfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店;
            baseinfo.OperatorId = this.SiteUserInfo.UserId;
            baseinfo.OperatorName = this.SiteUserInfo.Name;
            baseinfo.CostDetail = CostParticu;
            #endregion

            #region 提交操作
            //酒店id
            string planID = Utils.GetQueryStringValue("planId");
            int result = 0;
            EyouSoft.BLL.PlanStructure.BPlan bll = new EyouSoft.BLL.PlanStructure.BPlan();
            if (planID != null && planID != "")
            {
                baseinfo.PlanId = planID;
                baseinfo.PlanHotel.PlanId = planID;
                result = bll.UpdPlan(baseinfo);
                if (result == 1)
                {
                    msg += "修改成功！";
                    setsrrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else if (result == 0)
                {
                    msg += "修改失败!";
                    setsrrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
                else if (result == -2)
                {
                    msg += "预控数量不足,修改失败!";
                    setsrrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }
            else
            {
                result = bll.AddPlan(baseinfo);
                if (result == 1)
                {
                    msg += "添加成功!";
                    setsrrorMsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                }
                else if (result == 0)
                {
                    msg += "添加失败!";
                    setsrrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
                else if (result == -2)
                {
                    msg += "预控数量不足,添加失败!";
                    setsrrorMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                }
            }

            #endregion

            return setsrrorMsg;
        }
    }
}
