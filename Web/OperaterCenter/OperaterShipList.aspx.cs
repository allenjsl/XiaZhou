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
    /// 计调中心-地接,出境-涉外游船
    /// 创建人：李晓欢
    /// 创建时间:2011-09-15
    /// </summary>
    public partial class OperaterShipList : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        //支付方式 状态
        protected string PanyMent = string.Empty;
        protected string Status = string.Empty;
        protected string PanyMentC = string.Empty;
        protected string StatusC = string.Empty;
        //游船名称 国内
        protected System.Text.StringBuilder ShipNameListC = new System.Text.StringBuilder();
        protected System.Text.StringBuilder ShipNameListF = new System.Text.StringBuilder();
        //游轮确认单
        protected string querenforeirnUrl = string.Empty;
        protected string querenchinaUrl = string.Empty;
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
            PowerControl();
            UserId = this.SiteUserInfo.UserId;
            EyouSoft.BLL.SysStructure.BSys bsys = new EyouSoft.BLL.SysStructure.BSys();
            if (bsys.IsExistsMenu2(this.SiteUserInfo.SysId, EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源预控_游船预控))
            {
                this.SupplierControl1.Flag = 1;
                this.SupplierControl2.Flag = 1;
            }

            querenforeirnUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.涉外游轮确认单);
            querenchinaUrl = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(this.SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.国内游轮确认单);

            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("action");
            string sourceId = Utils.GetQueryStringValue("suppId");
            if (doType != "")
            {
                //存在ajax请求
                switch (doType)
                {
                    case "delete":
                        Response.Clear();
                        Response.Write(DeleteForeignShip());
                        Response.End();
                        break;
                    case "update":
                        GetShipModelForeign();
                        break;
                    case "save":
                        Response.Clear();
                        Response.Write(PageSaveForeign());
                        Response.End();
                        break;
                    case "deleteC":
                        Response.Clear();
                        Response.Write(DeleteShipC());
                        Response.End();
                        break;
                    case "updateC":
                        GetShipModelC();
                        break;
                    case "saveC":
                        Response.Clear();
                        Response.Write(PageSaveC());
                        Response.End();
                        break;
                    default: break;
                }
            }
            #endregion

            DataInit();
            DataInitC();
        }

        #region 游轮涉外

        #region 页面初始化
        /// <summary>
        /// 绑定安排的游轮 涉外
        /// </summary>
        /// <param name="tourID">团号</param>
        protected void DataInit()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            if (!string.IsNullOrEmpty(tourId))
            {
                ListPower = this.panView2.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(tourId, SiteUserInfo.UserId);
                if (ListPower) ListPower = panView2.Visible = Privs_AnPai;

                this.SupplierControl1.TourID = tourId;
                IList<EyouSoft.Model.PlanStructure.MPlan> ShipForeignList = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.涉外游轮, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourId);
                if (ShipForeignList != null && ShipForeignList.Count > 0)
                {
                    this.repShipAboradList.DataSource = ShipForeignList;
                    this.repShipAboradList.DataBind();
                }
                else
                {
                    this.phdShowListFrist.Visible = false;
                }
            }
            //this.txtCustomerInfo.Text = GetCustomerList();
        }
        #endregion

        #region 删除游轮 涉外
        /// <summary>
        /// 删除游轮 涉外
        /// </summary>
        /// <param name="planID">游轮id 涉外</param>
        /// <returns></returns>
        protected string DeleteForeignShip()
        {
            string planId = Utils.GetQueryStringValue("planId");
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
            return msg;
        }
        #endregion

        #region 获取安排的游轮实体
        /// <summary>
        /// 获取安排的游轮实体 涉外
        /// </summary>
        /// <param name="planID">游轮id</param>
        protected void GetShipModelForeign()
        {
            string planID = Utils.GetQueryStringValue("planId");
            if (!string.IsNullOrEmpty(planID))
            {
                EyouSoft.Model.PlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.BLL.PlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.涉外游轮, planID);
                if (baseinfo != null)
                {
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
                    this.txtContectFax.Text = baseinfo.ContactFax;
                    this.txtContectName.Text = baseinfo.ContactName;
                    this.txtContectPhone.Text = baseinfo.ContactPhone;
                    if (baseinfo.PlanShip != null)
                    {
                        if (!string.IsNullOrEmpty(baseinfo.SueId.Trim())) //预控
                        {
                            ShipNameListF.Append("<option value='" + baseinfo.PlanShip.SubId + "," + baseinfo.PlanShip.ShipName + "'>" + baseinfo.PlanShip.ShipName + "</option>");
                            EyouSoft.Model.SourceStructure.MSourceSueShip shipmodel = new EyouSoft.BLL.SourceStructure.BSourceControl().GetModelByShipId(baseinfo.SueId, this.SiteUserInfo.CompanyId);
                            if (shipmodel != null)
                            {
                                this.hidSurpluNum.Value = (shipmodel.ControlNum - shipmodel.AlreadyNum).ToString();
                            }
                        }
                        else  //供应商
                        {
                            EyouSoft.Model.SourceStructure.MSourceShip shipModel = new EyouSoft.BLL.SourceStructure.BSource().GetShipModel(baseinfo.SourceId);
                            if (shipModel != null)
                            {
                                if (shipModel.SubShipList != null && shipModel.SubShipList.Count > 0)
                                {
                                    for (int i = 0; i < shipModel.SubShipList.Count; i++)
                                    {
                                        if (shipModel.SubShipList[i].SubId == baseinfo.PlanShip.SubId)
                                        {
                                            ShipNameListF.Append("<option selected='selected' value='" + shipModel.SubShipList[i].SubId + "," + shipModel.SubShipList[i].ShipName + "'>" + shipModel.SubShipList[i].ShipName + "</option>");
                                        }
                                        else
                                        {
                                            ShipNameListF.Append("<option value='" + shipModel.SubShipList[i].SubId + "," + shipModel.SubShipList[i].ShipName + "'>" + shipModel.SubShipList[i].ShipName + "</option>");
                                        }
                                    }
                                }
                            }
                        }
                        this.txtShipPhone.Text = baseinfo.PlanShip.ShipCalls;
                        this.txtBoardWharif.Text = baseinfo.PlanShip.LoadDock;
                        this.txtBoardCode.Text = baseinfo.PlanShip.LoadCode;
                        this.txtRoute.Text = baseinfo.PlanShip.Line;
                        this.txtStopAttr.Text = baseinfo.PlanShip.Sight;

                        if (baseinfo.PlanShip.PlanShipPriceList != null && baseinfo.PlanShip.PlanShipPriceList.Count > 0)
                        {
                            this.tabViewPrices.Visible = false;
                            this.repPricesList.DataSource = baseinfo.PlanShip.PlanShipPriceList;
                            this.repPricesList.DataBind();
                        }
                        else
                        {
                            this.tabViewPrices.Visible = true;
                        }
                        
                    }
                    this.txtBoardDate.Text = UtilsCommons.GetDateString(baseinfo.StartDate, ProviderToDate);
                    this.txtBoardTime.Text = baseinfo.StartTime;
                    this.txtTotalPrices.Text = Utils.FilterEndOfTheZeroDecimal(baseinfo.Confirmation);
                    if (!string.IsNullOrEmpty(baseinfo.CustomerInfo))
                    {
                        this.txtCustomerInfo.Text = baseinfo.CustomerInfo;
                    }
                    else
                    {
                        this.txtCustomerInfo.Text = GetCustomerList();
                    }
                    this.txtCostDetail.Text = baseinfo.CostDetail;
                    this.txtGuidNOtes.Text = baseinfo.GuideNotes;
                    this.txtOtherRemarks.Text = baseinfo.Remarks;
                    this.ddlprofit1.Items.FindByValue(baseinfo.IsRebate == true ? "0" : "1").Selected = true;
                    PanyMent = ((int)baseinfo.PaymentType).ToString();
                    Status = ((int)baseinfo.Status).ToString();
                }
            }
        }
        #endregion

        #region 游轮房型 人群类型
        /// <summary>
        /// 游轮房型
        /// </summary>
        protected string GetShipRoomType(string selectedID)
        {
            System.Text.StringBuilder sbroomType = new System.Text.StringBuilder();
            List<EnumObj> roomlist = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanShipRoomType));
            for (int i = 0; i < 6; i++)
            {
                if (roomlist[i].Value == selectedID)
                {
                    sbroomType.Append("<option selected='selected' value='" + roomlist[i].Value + "'>" + roomlist[i].Text + "</option>");
                }
                else
                {
                    sbroomType.Append("<option value='" + roomlist[i].Value + "'>" + roomlist[i].Text + "</option>");
                }
            }
            return sbroomType.ToString();
        }
        /// <summary>
        /// 游轮人群类型
        /// </summary>
        /// <param name="selectedID">类型id</param>
        /// <returns></returns>
        protected string GetShipCrowdType(string selectedID)
        {
            System.Text.StringBuilder sbCrowdType = new System.Text.StringBuilder();
            List<EnumObj> crowdlist = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanShipCrowdType));
            for (int i = 0; i < crowdlist.Count; i++)
            {
                if (crowdlist[i].Value == selectedID)
                {
                    sbCrowdType.Append("<option selected='selected' value='" + crowdlist[i].Value + "'>" + crowdlist[i].Text + "</option>");
                }
                else
                {
                    sbCrowdType.Append("<option value='" + crowdlist[i].Value + "'>" + crowdlist[i].Text + "</option>");
                }
            }
            return sbCrowdType.ToString();
        }
        #endregion

        #region 获取游客资料
        /// <summary>
        /// 获取游客资料
        /// </summary>
        /// <param name="tourID">团号</param>
        /// <returns></returns>
        protected string GetCustomerList()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            System.Text.StringBuilder sbCustomerlist = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(tourId))
            {
                IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> OrderCustomerlist = new EyouSoft.BLL.TourStructure.BTour().GetTourTravellerList(tourId);
                if (OrderCustomerlist != null && OrderCustomerlist.Count > 0)
                {
                    sbCustomerlist.Append("姓名：" + OrderCustomerlist[0].CnName + "， 性别：" + OrderCustomerlist[0].Gender.ToString() + "， 证件号码：" + OrderCustomerlist[0].CardNumber + "");
                }
            }
            return sbCustomerlist.ToString();
        }
        #endregion

        #region 获取楼层
        /// <summary>
        /// 获取楼层
        /// </summary>
        /// <param name="Floorid"></param>
        /// <returns></returns>
        protected string GetFloors(string Floorid)
        {
            System.Text.StringBuilder _S = new System.Text.StringBuilder();
            if (!string.IsNullOrEmpty(Floorid))
            {
                if (Floorid == "0")
                {
                    _S.Append("<option value='-1'>--请选择--</option>");
                    _S.Append("<option value='0' selected='selected'>一层</option>");
                    _S.Append("<option value='1'>两层</option>");
                }
                if (Floorid == "1")
                {
                    _S.Append("<option value='-1'>--请选择--</option>");
                    _S.Append("<option value='0'>一层</option>");
                    _S.Append("<option value='1' selected='selected'>两层</option>");
                }
            }
            else
            {
                _S.Append("<option value='-1'>--请选择--</option>");
                _S.Append("<option value='0'>一层</option>");
                _S.Append("<option value='1'>两层</option>");
            }
            return _S.ToString();
        }
        #endregion

        #region 提交
        protected string PageSaveForeign()
        {
            #region 表单取值
            string msg = string.Empty;
            string seterrormsg = string.Empty;
            //游船公司
            string shipComName = Utils.GetFormValue(this.SupplierControl1.ClientText);
            string shipComID = Utils.GetFormValue(this.SupplierControl1.ClientValue);
            //联系人 电话 传真
            string contectName = Utils.GetFormValue(this.txtContectName.UniqueID);
            string contectPhone = Utils.GetFormValue(this.txtContectPhone.UniqueID);
            string contectFax = Utils.GetFormValue(this.txtContectFax.UniqueID);
            //船名 船载电话           
            string shipName = Utils.GetFormValue("ddlShipNameList").Split(',')[1];
            string shipPhone = Utils.GetFormValue(this.txtShipPhone.UniqueID);
            //登船日期 时间 船载码头
            DateTime? boardDate = Utils.GetDateTimeNullable(Utils.GetFormValue(this.txtBoardDate.UniqueID));
            string boardTime = Utils.GetFormValue(this.txtBoardTime.UniqueID);
            string boardWharif = Utils.GetFormValue(this.txtBoardWharif.UniqueID);
            //登船号 航线
            string boardCode = Utils.GetFormValue(this.txtBoardCode.UniqueID);
            string route = Utils.GetFormValue(this.txtRoute.UniqueID);
            //停靠景点
            string stopAttr = Utils.GetFormValue(this.txtStopAttr.UniqueID);
            //价格组成
            string[] roomTypes = Utils.GetFormValues("selRoomTypeList");
            string[] CrowdTypes = Utils.GetFormValues("selCrowdType");
            string[] adultNums = Utils.GetFormValues("txtAdultNum");
            string[] adultPrices = Utils.GetFormValues("txtAdultPrice");
            //string[] childNums = Utils.GetFormValues("txtChildNum");
            //string[] childPrices = Utils.GetFormValues("txtChildPrice");
            //string[] childNoNums = Utils.GetFormValues("txtChildNoNum");
            //string[] childNoPrices = Utils.GetFormValues("txtChildNoPrice");
            //string[] bobyNums = Utils.GetFormValues("txtBobyNum");
            //string[] bobyPrices = Utils.GetFormValues("txtBobyPrice");
            string[] totalPrices = Utils.GetFormValues("txtTotalPrice");
            //楼层
            string[] floors = Utils.GetFormValues("selFloorList");
            string[] peopleNums = Utils.GetFormValues("txtPeopleNum");
            string[] unitPrices = Utils.GetFormValues("txtUnitPrice");
            //自费项目
            //string ownCost = Utils.GetFormValue(this.txtOwnCost.UniqueID);
            //string ownPrices = Utils.GetFormValue(this.txtOwnCostPrice.UniqueID);
            //string ownNums = Utils.GetFormValue(this.txtOwnCostNums.UniqueID);
            //结算费用
            decimal totalCost = Utils.GetDecimal(Utils.GetFormValue(this.txtTotalPrices.UniqueID));
            //游客资料
            string customerInfo = Utils.GetFormValue(this.txtCustomerInfo.UniqueID);
            //费用明细
            string costDetail = Utils.GetFormValue(this.txtCostDetail.UniqueID);
            //导游需知 其它备注
            string guidNotes = Utils.GetFormValue(this.txtGuidNOtes.UniqueID);
            string otherMarks = Utils.GetFormValue(this.txtOtherRemarks.UniqueID);
            string[] txtSheWaiBeiZhu = Utils.GetFormValues("txtSheWaiBeiZhu");
            #endregion

            #region 后台验证
            if (string.IsNullOrEmpty(shipComID) && string.IsNullOrEmpty(shipComName))
            {
                msg += "请选择游船公司!<br/>";
            }
            if (string.IsNullOrEmpty(shipName))
            {
                msg += "请输入船名!<br/>";
            }
            if (string.IsNullOrEmpty(boardWharif))
            {
                msg += "请输入登船码头!<br/>";
            }
            if (roomTypes.Length > 0)
            {
                for (int i = 0; i < roomTypes.Length; i++)
                {
                    if (string.IsNullOrEmpty(roomTypes[i]))
                    {
                        msg += "请选择房型!<br/>";
                    }
                }
            }
            if (CrowdTypes.Length > 0)
            {
                for (int i = 0; i < CrowdTypes.Length; i++)
                {
                    if (string.IsNullOrEmpty(CrowdTypes[i]))
                    {
                        msg += "请选择人群类型!<br/>";
                    }
                }
            }
            if (adultNums.Length > 0)
            {
                for (int i = 0; i < adultNums.Length; i++)
                {
                    if (string.IsNullOrEmpty(adultNums[i]) || Utils.GetInt(adultNums[i]) < 0)
                    {
                        msg += "请输入人数！<br/>";
                    }
                }
            }
            if (adultPrices.Length > 0)
            {
                for (int i = 0; i < adultPrices.Length; i++)
                {
                    if (Utils.GetDecimal(adultPrices[i]) < 0 || string.IsNullOrEmpty(adultPrices[i]))
                    {
                        msg += "请输入单价！<br/>";
                    }
                }
            }
            if (totalCost < 0)
            {
                msg += "请输入结算费用!<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("SelPanyMent")))
            {
                msg += "请选择支付方式!<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("SelStatus")))
            {
                msg += "请选择状态!<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue(this.ddlprofit1.UniqueID)))
            {
                msg += "请选择是否返利!<br/>";
            }
            if (msg != "")
            {
                seterrormsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                return seterrormsg;
            }
            #endregion

            #region 实体赋值
            EyouSoft.Model.PlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.Model.PlanStructure.MPlanBaseInfo();
            baseinfo.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            baseinfo.CompanyId = this.SiteUserInfo.CompanyId;
            baseinfo.Confirmation = totalCost;
            baseinfo.PlanCost = totalCost;
            baseinfo.ContactFax = contectFax;
            baseinfo.ContactName = contectName;
            baseinfo.ContactPhone = contectPhone;
            baseinfo.CostDetail = costDetail;
            baseinfo.CustomerInfo = customerInfo;
            baseinfo.GuideNotes = guidNotes;
            baseinfo.IsRebate = Utils.GetFormValue(this.ddlprofit1.UniqueID) == "0" ? true : false;
            baseinfo.IssueTime = System.DateTime.Now;
            baseinfo.PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)Utils.GetInt(Utils.GetFormValue("SelPanyMent"));
            baseinfo.PlanShip = new EyouSoft.Model.PlanStructure.MPlanShip();
            baseinfo.PlanShip.Line = route;
            baseinfo.PlanShip.LoadCode = boardCode;
            baseinfo.PlanShip.LoadDock = boardWharif;
            baseinfo.PlanShip.ShipCalls = shipPhone;
            baseinfo.PlanShip.ShipName = shipName;
            baseinfo.PlanShip.SubId = Utils.GetFormValue("ddlShipNameList").Split(',')[0];
            baseinfo.PlanShip.Sight = stopAttr;
            //自费项目
            //baseinfo.PlanShip.PlanShipOwnCostList = new List<EyouSoft.Model.PlanStructure.MPlanShipOwnCost>();
            //EyouSoft.Model.PlanStructure.MPlanShipOwnCost OwnCostM = new EyouSoft.Model.PlanStructure.MPlanShipOwnCost();
            //OwnCostM.OwnItem = ownCost;
            //OwnCostM.PeopleNum = Utils.GetInt(ownNums);
            //OwnCostM.Price = Utils.GetDecimal(ownPrices);
            //OwnCostM.IsFloor = false;
            //baseinfo.PlanShip.PlanShipOwnCostList.Add(OwnCostM);
            //楼层
            //for (int i = 0; i < floors.Length; i++)
            //{
            //    EyouSoft.Model.PlanStructure.MPlanShipOwnCost OwnCostF = new EyouSoft.Model.PlanStructure.MPlanShipOwnCost();
            //    OwnCostF.IsFloor = true;
            //    OwnCostF.OwnItem = floors[i];
            //    OwnCostF.PeopleNum = Utils.GetInt(peopleNums[i]);
            //    OwnCostF.Price = Utils.GetDecimal(unitPrices[i]);
            //    baseinfo.PlanShip.PlanShipOwnCostList.Add(OwnCostF);
            //}
            decimal nums = 0;
            //价格组成
            baseinfo.PlanShip.PlanShipPriceList = new List<EyouSoft.Model.PlanStructure.MPlanShipPrice>();
            for (int i = 0; i < roomTypes.Length; i++)
            {
                EyouSoft.Model.PlanStructure.MPlanShipPrice pricesM = new EyouSoft.Model.PlanStructure.MPlanShipPrice();
                pricesM.DNum = Utils.GetDecimal(adultNums[i]);
                pricesM.AdultNumber = Utils.GetInt(adultNums[i]);
                pricesM.AdultNumber = Convert.ToInt32(pricesM.DNum);
                pricesM.AdultPrice = Utils.GetDecimal(adultPrices[i]);
                //pricesM.BabyNumber = Utils.GetInt(bobyNums[i]);
                //pricesM.BabyNumberPrice = Utils.GetDecimal(bobyPrices[i]);
                //pricesM.ChildNoOccupancy = Utils.GetInt(childNoNums[i]);
                //pricesM.ChildNoOccupancyPrice = Utils.GetDecimal(childNoPrices[i]);
                //pricesM.ChildNumber = Utils.GetInt(childNums[i]);
                //pricesM.ChildPrice = Utils.GetDecimal(childPrices[i]);
                pricesM.SumPrice = Utils.GetDecimal(totalPrices[i]); ;
                //nums += Utils.GetInt(adultNums[i]) + Utils.GetInt(bobyNums[i]) + Utils.GetInt(childNoNums[i]) + Utils.GetInt(childNums[i]);
                nums += Utils.GetDecimal(adultNums[i]);
                pricesM.CrowdType = (EyouSoft.Model.EnumType.PlanStructure.PlanShipCrowdType)Enum.Parse(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanShipCrowdType), CrowdTypes[i]);
                pricesM.RoomType = (EyouSoft.Model.EnumType.PlanStructure.PlanShipRoomType)Enum.Parse(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanShipRoomType), roomTypes[i]);
                pricesM.BeiZhu = txtSheWaiBeiZhu[i];
                //小计
                baseinfo.PlanShip.PlanShipPriceList.Add(pricesM);
            }
            baseinfo.Num = Convert.ToInt32(nums);
            baseinfo.DNum = nums;
            baseinfo.Remarks = otherMarks;
            baseinfo.SourceId = shipComID;
            baseinfo.SourceName = shipComName;
            baseinfo.StartDate = boardDate;
            baseinfo.StartTime = boardTime;
            baseinfo.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("SelStatus"));
            if (Utils.GetFormValue(this.SupplierControl1.ClientIsyukong) == "1")
            {
                baseinfo.SueId = Utils.GetFormValue(this.SupplierControl1.ClientzyykValue);
            }
            baseinfo.TourId = Utils.GetQueryStringValue("tourId");
            baseinfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.涉外游轮;
            baseinfo.OperatorId = this.SiteUserInfo.UserId;
            baseinfo.OperatorName = this.SiteUserInfo.Name;
            #endregion

            #region 提交操作
            int result = 0;
            EyouSoft.BLL.PlanStructure.BPlan bll = new EyouSoft.BLL.PlanStructure.BPlan();
            if (Utils.GetQueryStringValue("action") == "save")
            {
                string editID = Utils.GetQueryStringValue("planId");
                if (!string.IsNullOrEmpty(editID))
                {
                    baseinfo.PlanShip.PlanId = editID;
                    baseinfo.PlanId = editID;
                    result = bll.UpdPlan(baseinfo);
                    if (result == 1)
                    {
                        msg += "修改成功！";
                        seterrormsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                    }
                    else if (result == 0)
                    {
                        msg += "修改失败!";
                        seterrormsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                    }
                    else if (result == -2)
                    {
                        msg += "预控数量不足,修改失败!";
                        seterrormsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                    }
                }
                else
                {
                    result = bll.AddPlan(baseinfo);
                    if (result == 1)
                    {
                        msg += "添加成功!";
                        seterrormsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                    }
                    else if (result == 0)
                    {
                        msg += "添加失败!";
                        seterrormsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                    }
                    else if (result == -2)
                    {
                        msg += "预控数量不足,添加失败!";
                        seterrormsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                    }
                }
            }
            #endregion

            return seterrormsg;
        }
        #endregion
        #endregion

        #region 国内游轮
        #region 国内游轮舱位
        /// <summary>
        ///国内游轮舱位
        /// </summary>
        /// <param name="selectedID">舱位id</param>
        /// <returns></returns>
        protected string GetShipSeat(string selectedID)
        {
            System.Text.StringBuilder sbSeatList = new System.Text.StringBuilder();
            List<EnumObj> seatlist = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanShipRoomType));
            if (seatlist != null)
            {
                for (int i = 6; i < seatlist.Count; i++)
                {
                    if (seatlist[i].Value == selectedID)
                    {
                        sbSeatList.Append("<option selected='selected' value='" + seatlist[i].Value + "'>" + seatlist[i].Text + "</option>");
                    }
                    else
                    {
                        sbSeatList.Append("<option value='" + seatlist[i].Value + "'>" + seatlist[i].Text + "</option>");
                    }
                }
            }
            return sbSeatList.ToString();
        }
        #endregion

        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="tourID"></param>
        protected void DataInitC()
        {
            string tourId = Utils.GetQueryStringValue("tourId");
            if (!string.IsNullOrEmpty(tourId))
            {
                ListPower = this.panView1.Visible = EyouSoft.Common.UtilsCommons.GetUpdateAndDeleteByStatus(tourId, SiteUserInfo.UserId);
                if (ListPower) ListPower = panView1.Visible = Privs_AnPai;

                this.SupplierControl2.TourID = tourId;
                IList<EyouSoft.Model.PlanStructure.MPlan> ShipC = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.国内游轮, null, EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加, false, null, tourId);
                if (ShipC != null && ShipC.Count > 0)
                {
                    this.repShipListC.DataSource = ShipC;
                    this.repShipListC.DataBind();
                }
                else
                {
                    this.phdShowListSecond.Visible = false;
                }
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="planID">游轮id</param>
        /// <returns></returns>
        protected string DeleteShipC()
        {
            string planId = Utils.GetQueryStringValue("planId");
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
            return msg;
        }
        #endregion

        #region 获取国内游轮实体
        /// <summary>
        /// 获取国内游轮实体
        /// </summary>
        /// <param name="planID">游轮id</param>
        protected void GetShipModelC()
        {
            string planId = Utils.GetQueryStringValue("planId");
            if (!string.IsNullOrEmpty(planId))
            {
                EyouSoft.Model.PlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.BLL.PlanStructure.BPlan().GetModel(EyouSoft.Model.EnumType.PlanStructure.PlanProject.国内游轮, planId);
                if (baseinfo != null)
                {
                    this.SupplierControl2.HideID = baseinfo.SourceId;
                    this.SupplierControl2.Name = baseinfo.SourceName;
                    if (!string.IsNullOrEmpty(baseinfo.SueId.Trim()))
                    {
                        this.SupplierControl2.HideID_zyyk = baseinfo.SueId;
                        this.SupplierControl2.isyukong = "1";
                    }
                    else
                    {
                        this.SupplierControl2.isyukong = "0";
                    }
                    this.txtContectFaxC.Text = baseinfo.ContactFax;
                    this.txtContectNameC.Text = baseinfo.ContactName;
                    this.txtContectPhoneC.Text = baseinfo.ContactPhone;
                    if (baseinfo.PlanShip != null)
                    {
                        if (!string.IsNullOrEmpty(baseinfo.SueId.Trim())) //预控
                        {
                            ShipNameListC.Append("<option value='" + baseinfo.PlanShip.SubId + "," + baseinfo.PlanShip.ShipName + "'>" + baseinfo.PlanShip.ShipName + "</option>");
                            EyouSoft.Model.SourceStructure.MSourceSueShip shipmodel = new EyouSoft.BLL.SourceStructure.BSourceControl().GetModelByShipId(baseinfo.SueId, this.SiteUserInfo.CompanyId);
                            if (shipmodel != null)
                            {
                                this.hidSurpluNumC.Value = (shipmodel.ControlNum - shipmodel.AlreadyNum).ToString();
                            }
                        }
                        else  //供应商
                        {
                            EyouSoft.Model.SourceStructure.MSourceShip shipModel = new EyouSoft.BLL.SourceStructure.BSource().GetShipModel(baseinfo.SourceId);
                            if (shipModel != null)
                            {
                                if (shipModel.SubShipList != null && shipModel.SubShipList.Count > 0)
                                {
                                    for (int i = 0; i < shipModel.SubShipList.Count; i++)
                                    {
                                        if (shipModel.SubShipList[i].SubId == baseinfo.PlanShip.SubId)
                                        {
                                            ShipNameListC.Append("<option selected='selected' value='" + shipModel.SubShipList[i].SubId + "," + shipModel.SubShipList[i].ShipName + "'>" + shipModel.SubShipList[i].ShipName + "</option>");
                                        }
                                        else
                                        {
                                            ShipNameListC.Append("<option value='" + shipModel.SubShipList[i].SubId + "," + shipModel.SubShipList[i].ShipName + "'>" + shipModel.SubShipList[i].ShipName + "</option>");
                                        }
                                    }
                                }
                            }
                        }
                        this.txtRouteC.Text = baseinfo.PlanShip.Line;
                        this.txtBoardWhilfC.Text = baseinfo.PlanShip.LoadDock;
                        this.txtStopAttrC.Text = baseinfo.PlanShip.Sight;
                        if (baseinfo.PlanShip.PlanShipPriceList != null && baseinfo.PlanShip.PlanShipPriceList.Count > 0)
                        {
                            this.tabView.Visible = false;
                            this.repPriceListC.DataSource = baseinfo.PlanShip.PlanShipPriceList;
                            this.repPriceListC.DataBind();
                            //if (baseinfo.PlanShip.PlanShipOwnCostList != null && baseinfo.PlanShip.PlanShipOwnCostList.Count > 0)
                            //{
                            //    EyouSoft.Model.PlanStructure.MPlanShipOwnCost ownCostC = baseinfo.PlanShip.PlanShipOwnCostList.SingleOrDefault(p => p.IsFloor == false);
                            //    if (ownCostC != null)
                            //    {
                            //        this.txtOwnCostC.Text = ownCostC.OwnItem;
                            //        this.txtOwnCostNumsC.Text = ownCostC.PeopleNum.ToString();
                            //        this.txtOwnUnitPricesC.Text = Utils.FilterEndOfTheZeroDecimal(ownCostC.Price);
                            //    }
                            //}
                        }
                        else
                        {
                            this.tabView.Visible = true;
                        }
                    }
                    this.txtBoardDateC.Text = UtilsCommons.GetDateString(baseinfo.StartDate, ProviderToDate);
                    this.txtBoardTimeC.Text = baseinfo.StartTime;
                    this.txttotalPricesC.Text = Utils.FilterEndOfTheZeroDecimal(baseinfo.Confirmation);
                    this.txtCostDetailC.Text = baseinfo.CostDetail;
                    this.txtGuidNotesC.Text = baseinfo.GuideNotes;
                    this.txtOtherMarkC.Text = baseinfo.Remarks;
                    PanyMentC = ((int)baseinfo.PaymentType).ToString();
                    StatusC = ((int)baseinfo.Status).ToString();
                    this.ddlprofit2.Items.FindByValue(baseinfo.IsRebate == true ? "0" : "1").Selected = true;
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        protected string PageSaveC()
        {
            #region 表单取值
            string msg = string.Empty;
            string seterrormsg = string.Empty;
            //游船公司id name
            string shipIdC = Utils.GetFormValue(this.SupplierControl2.ClientValue);
            string shipNameC = Utils.GetFormValue(this.SupplierControl2.ClientText);
            //联系人  电话 传真
            string contectName = Utils.GetFormValue(this.txtContectNameC.UniqueID);
            string contectPhone = Utils.GetFormValue(this.txtContectPhoneC.UniqueID);
            string contectFax = Utils.GetFormValue(this.txtContectFaxC.UniqueID);
            //船名 线路
            string shipId = string.Empty;
            string shipname = string.Empty;
            if (!string.IsNullOrEmpty(Utils.GetFormValue("SelShipNameC")))
            {
                shipId = Utils.GetFormValue("SelShipNameC").Split(',')[0];
                shipname = Utils.GetFormValue("SelShipNameC").Split(',')[1];
            }
            string route = Utils.GetFormValue(this.txtRouteC.UniqueID);
            //登船日期 时间 登船码头 停靠景点
            DateTime? boardDate = Utils.GetDateTimeNullable(Utils.GetFormValue(this.txtBoardDateC.UniqueID));
            string boardtime = Utils.GetFormValue(this.txtBoardTimeC.UniqueID);
            string boardWhilfC = Utils.GetFormValue(this.txtBoardWhilfC.UniqueID);
            string stopAttr = Utils.GetFormValue(this.txtStopAttrC.UniqueID);
            //价格组成
            string[] AircraftType = Utils.GetFormValues("selAircraft");
            string[] adultNums = Utils.GetFormValues("AdultNumsC");
            string[] adultPrices = Utils.GetFormValues("adultPricesC");
            //string[] childNums = Utils.GetFormValues("childrenNumC");
            //string[] childPrices = Utils.GetFormValues("childrenpricesC");
            //自费项目
            //string ownCost = Utils.GetFormValue(this.txtOwnCostC.UniqueID);
            //string ownPeopleNums = Utils.GetFormValue(this.txtOwnCostNumsC.UniqueID);
            //decimal ownUnitPrices = Utils.GetDecimal(Utils.GetFormValue(this.txtOwnUnitPricesC.UniqueID));
            //结算费用
            decimal totalPrices = Utils.GetDecimal(Utils.GetFormValue(this.txttotalPricesC.UniqueID));
            //费用明细
            string costDetail = Utils.GetFormValue(this.txtCostDetailC.UniqueID);
            //导游需知 其它备注
            string guidNotes = Utils.GetFormValue(this.txtGuidNotesC.UniqueID);
            string otherRemrk = Utils.GetFormValue(this.txtOtherMarkC.UniqueID);

            string[] txtGuoNeiXiaoJi = Utils.GetFormValues("txtGuoNeiXiaoJi");
            string[] txtGuoNeiBeiZhu = Utils.GetFormValues("txtGuoNeiBeiZhu");
            #endregion

            #region 后台验证
            if (string.IsNullOrEmpty(shipIdC) && string.IsNullOrEmpty(shipNameC))
            {
                msg += "请选择游轮公司!<br/>";
            }
            if (string.IsNullOrEmpty(route))
            {
                msg += "请输入线路！<br/>";
            }
            if (string.IsNullOrEmpty(boardWhilfC))
            {
                msg += "请输入登船码头!<br/>";
            }
            if (totalPrices <= 0)
            {
                msg += "请输入结算价格！<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("SelPanyMentC")))
            {
                msg += "请选择支付方式!<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue("SelStateC")))
            {
                msg += "请选择状态!<br/>";
            }
            if (string.IsNullOrEmpty(Utils.GetFormValue(this.ddlprofit2.UniqueID)))
            {
                msg += "请选择是否返利!<br/>";
            }
            if (AircraftType.Length > 0)
            {
                for (int i = 0; i < AircraftType.Length; i++)
                {
                    if (string.IsNullOrEmpty(AircraftType[i]))
                    { msg += "请选择舱位类型!<br/>"; }
                }
            }
            if (adultNums.Length > 0)
            {
                for (int i = 0; i < adultNums.Length; i++)
                {
                    if (string.IsNullOrEmpty(adultNums[i]) || Utils.GetInt(adultNums[i]) < 0) { msg += "请输入人数!<br/>"; }
                }
            }
            if (adultPrices.Length > 0)
            {
                for (int i = 0; i < adultPrices.Length; i++)
                {
                    if (Utils.GetDecimal(adultPrices[i]) < 0) { msg += "请输入单价格!<br/>"; }
                }
            }

            if (msg != "")
            {
                seterrormsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                return seterrormsg;
            }
            #endregion

            #region 实体赋值
            EyouSoft.Model.PlanStructure.MPlanBaseInfo baseinfo = new EyouSoft.Model.PlanStructure.MPlanBaseInfo();
            baseinfo.AddStatus = EyouSoft.Model.EnumType.PlanStructure.PlanAddStatus.计调安排时添加;
            baseinfo.CompanyId = this.SiteUserInfo.CompanyId;
            baseinfo.Confirmation = totalPrices;
            baseinfo.PlanCost = totalPrices;
            baseinfo.ContactFax = contectFax;
            baseinfo.ContactName = contectName; baseinfo.ContactPhone = contectPhone;
            baseinfo.CostDetail = costDetail;
            baseinfo.GuideNotes = guidNotes;
            baseinfo.IsRebate = Utils.GetFormValue(this.ddlprofit2.UniqueID) == "0" ? true : false;
            baseinfo.IssueTime = System.DateTime.Now;
            baseinfo.PaymentType = (EyouSoft.Model.EnumType.PlanStructure.Payment)Utils.GetInt(Utils.GetFormValue("SelPanyMentC"));
            baseinfo.PlanShip = new EyouSoft.Model.PlanStructure.MPlanShip();
            baseinfo.PlanShip.Line = route;
            baseinfo.PlanShip.LoadDock = boardWhilfC;
            baseinfo.PlanShip.Sight = stopAttr;
            baseinfo.PlanShip.SubId = shipId;
            baseinfo.PlanShip.ShipName = shipname;
            decimal nums = 0;
            baseinfo.PlanShip.PlanShipPriceList = new List<EyouSoft.Model.PlanStructure.MPlanShipPrice>();
            for (int i = 0; i < AircraftType.Length; i++)
            {
                EyouSoft.Model.PlanStructure.MPlanShipPrice prices = new EyouSoft.Model.PlanStructure.MPlanShipPrice();
                prices.RoomType = (EyouSoft.Model.EnumType.PlanStructure.PlanShipRoomType)Enum.Parse(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanShipRoomType), AircraftType[i]);
                prices.DNum = Utils.GetDecimal(adultNums[i]);
                prices.AdultNumber = Utils.GetInt(adultNums[i]);
                prices.AdultNumber = Convert.ToInt32(prices.DNum);
                prices.AdultPrice = Utils.GetDecimal(adultPrices[i]);
                //prices.ChildNumber = Utils.GetInt(childNums[i]);
                //prices.ChildPrice = Utils.GetDecimal(childPrices[i]);
                prices.SumPrice = Utils.GetDecimal(txtGuoNeiXiaoJi[i]);
                prices.BeiZhu = txtGuoNeiBeiZhu[i];
                baseinfo.PlanShip.PlanShipPriceList.Add(prices);
                //nums += Utils.GetInt(adultNums[i]) + Utils.GetInt(childNums[i]);
                nums += Utils.GetDecimal(adultNums[i]);
            }
            baseinfo.PlanShip.PlanShipOwnCostList = new List<EyouSoft.Model.PlanStructure.MPlanShipOwnCost>();
            //EyouSoft.Model.PlanStructure.MPlanShipOwnCost ownCostObj = new EyouSoft.Model.PlanStructure.MPlanShipOwnCost();
            //ownCostObj.IsFloor = false;
            //ownCostObj.OwnItem = ownCost;
            //ownCostObj.PeopleNum = Utils.GetInt(ownPeopleNums);
            //ownCostObj.Price = ownUnitPrices;
            //baseinfo.PlanShip.PlanShipOwnCostList.Add(ownCostObj);
            baseinfo.Num = Convert.ToInt32(nums);
            baseinfo.DNum = nums;
            baseinfo.Remarks = otherRemrk;
            baseinfo.SourceId = shipIdC;
            baseinfo.SourceName = shipNameC;
            baseinfo.StartDate = boardDate;
            baseinfo.StartTime = boardtime;
            baseinfo.Status = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue("SelStateC"));
            if (Utils.GetFormValue(this.SupplierControl2.ClientIsyukong) == "1")
            {
                baseinfo.SueId = Utils.GetFormValue(this.SupplierControl2.ClientzyykValue);
            }
            baseinfo.TourId = Utils.GetQueryStringValue("tourId");
            baseinfo.Type = EyouSoft.Model.EnumType.PlanStructure.PlanProject.国内游轮;
            baseinfo.OperatorId = this.SiteUserInfo.UserId;
            baseinfo.OperatorName = this.SiteUserInfo.Name;
            #endregion

            #region 表单提交
            if (Utils.GetQueryStringValue("action") == "saveC")
            {
                string planID = Utils.GetQueryStringValue("planId");
                //修改
                if (!string.IsNullOrEmpty(planID))
                {
                    baseinfo.PlanId = planID;
                    baseinfo.PlanShip.PlanId = planID;
                    if (new EyouSoft.BLL.PlanStructure.BPlan().UpdPlan(baseinfo) > 0)
                    {
                        msg += "修改成功！";
                        seterrormsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                    }
                    else
                    {
                        msg += "修改失败！";
                        seterrormsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                    }
                }
                else //添加
                {
                    if (new EyouSoft.BLL.PlanStructure.BPlan().AddPlan(baseinfo) > 0)
                    {
                        msg += "添加成功！";
                        seterrormsg = UtilsCommons.AjaxReturnJson("1", "" + msg + "");
                    }
                    else
                    {
                        msg += "添加失败！";
                        seterrormsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                    }
                }
            }
            #endregion

            return seterrormsg;
        }
        #endregion


        #endregion

        #region 权限判断
        protected void PowerControl()
        {
            var tourType = new EyouSoft.BLL.TourStructure.BTour().GetTourType(Utils.GetQueryStringValue("tourid"));

            switch (tourType)
            {
                case EyouSoft.Model.EnumType.TourStructure.TourType.出境散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.出境团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排游轮))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排游轮, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_安排游轮);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排游轮))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排游轮, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_安排游轮);
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                    //if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排游轮))
                    //{
                    //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排游轮, false);
                    //    return;
                    //}
                    Privs_AnPai = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_安排游轮);
                    break;
            }
        }
        #endregion
    }
}
