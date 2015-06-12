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
    /// 计调安排—备用金申请
    /// 创建人:李晓欢
    /// 创建时间：2011-09-22
    /// </summary>
    public partial class PettyCashList : EyouSoft.Common.Page.BackPage
    {
        protected string tourCode = string.Empty;
        protected bool ret = false;

        protected EyouSoft.Model.EnumType.TourStructure.TourStatus tourStatus;

        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();

            //菜单控件 样式初始化
            this.menu1.IndexClass = "3";
            menu1.CompanyId = SiteUserInfo.CompanyId;

            string tourID = Utils.GetQueryStringValue("tourId");
            DataInitByTourId(tourID);
            GetSignNums(tourID);
            GetOrderListBytourID(tourID);
            GetDebitListByTourId(tourID);

            string doType = Utils.GetQueryStringValue("action");
            string id = Utils.GetQueryStringValue("ID");
            switch (doType)
            {
                case "delete":
                    Response.Clear();
                    Response.Write(DeleteDebitByID(id));
                    Response.End();
                    break;
                case "update":
                    Response.Clear();
                    Response.Write(debitAddOrUpdate());
                    Response.End();
                    break;
            }
        }

        #region 根据团号获取团队信息
        /// <summary>
        /// 根据团号获取团队信息
        /// </summary>
        /// <param name="toutID">团号</param>
        protected void DataInitByTourId(string tourID)
        {
            if (!string.IsNullOrEmpty(tourID))
            {
                EyouSoft.Model.TourStructure.MTourBaseInfo tourInfo = new EyouSoft.BLL.TourStructure.BTour().GetTourInfo(tourID);                
                if (tourInfo != null)
                {
                    //团号
                    tourStatus = tourInfo.TourStatus;
                    this.litTourCode.Text = tourInfo.TourCode;
                    tourCode = tourInfo.TourCode;
                    //线路名称
                    this.litRouteName.Text = tourInfo.RouteName;
                    //天数
                    this.litDays.Text = tourInfo.TourDays.ToString();
                    //人数
                    this.litNums.Text = tourInfo.PlanPeopleNumber.ToString();
                    //出团时间
                    this.litStartDate.Text = EyouSoft.Common.UtilsCommons.GetDateString(tourInfo.LDate, ProviderToDate);
                    //销售员
                    if (tourInfo.SaleInfo != null)
                    {
                        this.litSellers.Text = tourInfo.SaleInfo.Name;
                    }
                }

                //总金额统计
                decimal totalPrices = 0;
                int indexCount = 0;

                //地接
                IList<EyouSoft.Model.PlanStructure.MPlan> ayencylist = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接, EyouSoft.Model.EnumType.PlanStructure.Payment.导游现付, null, false, null, tourID);
                if (ayencylist != null && ayencylist.Count > 0)
                {
                    this.tabAyencyView.Visible = true;
                    indexCount += 1;
                    totalPrices += ayencylist.Sum(p => p.Confirmation);
                    this.repayencyList.DataSource = ayencylist;
                    this.repayencyList.DataBind();
                }


                IList<EyouSoft.Model.PlanStructure.MPlan> guidlist = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游, EyouSoft.Model.EnumType.PlanStructure.Payment.导游现付, null, false, null, tourID);
                if (guidlist != null && guidlist.Count > 0)
                {
                    this.tabGuidView.Visible = true;
                    indexCount += 1;
                    totalPrices += guidlist.Sum(p => p.Confirmation);
                    this.repGuidList.DataSource = guidlist;
                    this.repGuidList.DataBind();
                }

                IList<EyouSoft.Model.PlanStructure.MPlan> hotellist = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店, EyouSoft.Model.EnumType.PlanStructure.Payment.导游现付, null, false, null, tourID);
                if (hotellist != null && hotellist.Count > 0)
                {
                    indexCount += 1;
                    this.tabHotelView.Visible = true;
                    totalPrices += hotellist.Sum(p => p.Confirmation);
                    this.rephotellist.DataSource = hotellist;
                    this.rephotellist.DataBind();
                }

                IList<EyouSoft.Model.PlanStructure.MPlan> carslist = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车, EyouSoft.Model.EnumType.PlanStructure.Payment.导游现付, null, false, null, tourID);
                if (carslist != null && carslist.Count > 0)
                {
                    indexCount += 1;
                    this.tabCarsView.Visible = true;
                    totalPrices += carslist.Sum(p => p.Confirmation);
                    this.repcarslist.DataSource = carslist;
                    this.repcarslist.DataBind();
                }

                IList<EyouSoft.Model.PlanStructure.MPlan> airlist = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机, EyouSoft.Model.EnumType.PlanStructure.Payment.导游现付, null, false, null, tourID);
                if (airlist != null && airlist.Count > 0)
                {
                    indexCount += 1;
                    this.tabAirView.Visible = true;
                    totalPrices += airlist.Sum(p => p.Confirmation);
                    this.repairlist.DataSource = airlist;
                    this.repairlist.DataBind();
                }

                IList<EyouSoft.Model.PlanStructure.MPlan> trainlist = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车, EyouSoft.Model.EnumType.PlanStructure.Payment.导游现付, null, false, null, tourID);
                if (trainlist != null && trainlist.Count > 0)
                {
                    indexCount += 1;
                    this.tabtrainView.Visible = true;
                    totalPrices += trainlist.Sum(p => p.Confirmation);
                    this.reptrainlist.DataSource = trainlist;
                    this.reptrainlist.DataBind();
                }

                IList<EyouSoft.Model.PlanStructure.MPlan> buslist = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车, EyouSoft.Model.EnumType.PlanStructure.Payment.导游现付, null, false, null, tourID);
                if (buslist != null && buslist.Count > 0)
                {
                    indexCount += 1;
                    this.tabbusView.Visible = true;
                    totalPrices += buslist.Sum(p => p.Confirmation);
                    this.repbuslist.DataSource = buslist;
                    this.repbuslist.DataBind();
                }

                IList<EyouSoft.Model.PlanStructure.MPlan> attrlist = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点, EyouSoft.Model.EnumType.PlanStructure.Payment.导游现付, null, false, null, tourID);
                if (attrlist != null && attrlist.Count > 0)
                {
                    indexCount += 1;
                    this.tabAttrView.Visible = true;
                    totalPrices += attrlist.Sum(p => p.Confirmation);
                    this.repattrlist.DataSource = attrlist;
                    this.repattrlist.DataBind();
                }

                IList<EyouSoft.Model.PlanStructure.MPlan> foreignshiplist = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.涉外游轮, EyouSoft.Model.EnumType.PlanStructure.Payment.导游现付, null, false, null, tourID);
                if (foreignshiplist != null && foreignshiplist.Count > 0)
                {
                    indexCount += 1;
                    this.tabForeignShipView.Visible = true;
                    totalPrices += foreignshiplist.Sum(p => p.Confirmation);
                    this.repforeignshiplist.DataSource = foreignshiplist;
                    this.repforeignshiplist.DataBind();
                }

                IList<EyouSoft.Model.PlanStructure.MPlan> chinashiplist = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.国内游轮, EyouSoft.Model.EnumType.PlanStructure.Payment.导游现付, null, false, null, tourID);
                if (chinashiplist != null && chinashiplist.Count > 0)
                {
                    indexCount += 1;
                    this.tabchinashipView.Visible = true;
                    totalPrices += chinashiplist.Sum(p => p.Confirmation);
                    this.repchinashiplist.DataSource = chinashiplist;
                    this.repchinashiplist.DataBind();
                }

                IList<EyouSoft.Model.PlanStructure.MPlan> dinlist = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐, EyouSoft.Model.EnumType.PlanStructure.Payment.导游现付, null, false, null, tourID);
                if (dinlist != null && dinlist.Count > 0)
                {
                    indexCount += 1;
                    this.tabDinView.Visible = true;
                    totalPrices += dinlist.Sum(p => p.Confirmation);
                    this.repDinlist.DataSource = dinlist;
                    this.repDinlist.DataBind();
                }

                IList<EyouSoft.Model.PlanStructure.MPlan> shoplist = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物, EyouSoft.Model.EnumType.PlanStructure.Payment.导游现付, null, false, null, tourID);
                if (shoplist != null && shoplist.Count > 0)
                {
                    indexCount += 1;
                    this.tabshopView.Visible = true;
                    totalPrices += shoplist.Sum(p => p.Confirmation);
                    this.repshoplist.DataSource = shoplist;
                    this.repshoplist.DataBind();
                }

                IList<EyouSoft.Model.PlanStructure.MPlan> picklist = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.领料, EyouSoft.Model.EnumType.PlanStructure.Payment.导游现付, null, false, null, tourID);
                if (picklist != null && picklist.Count > 0)
                {
                    indexCount += 1;
                    this.tabpickView.Visible = true;
                    totalPrices += picklist.Sum(p => p.Confirmation);
                    this.reppicklist.DataSource = picklist;
                    this.reppicklist.DataBind();
                }

                IList<EyouSoft.Model.PlanStructure.MPlan> otherlist = new EyouSoft.BLL.PlanStructure.BPlan().GetList(EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它, EyouSoft.Model.EnumType.PlanStructure.Payment.导游现付, null, false, null, tourID);
                if (otherlist != null && otherlist.Count > 0)
                {
                    indexCount += 1;
                    this.tabotherView.Visible = true;
                    totalPrices += otherlist.Sum(p => p.Confirmation);
                    this.repotherlist.DataSource = otherlist;
                    this.repotherlist.DataBind();
                }
                //总金额统计
                this.littotalPrices.Text = EyouSoft.Common.UtilsCommons.GetMoneyString(totalPrices, ProviderToMoney);
            }
        }
        #endregion

        #region 支付方式为导游签单的计调项统计
        /// <summary>
        /// 支付方式为导游签单的计调项统计
        /// </summary>
        /// <param name="tourID">团号</param>
        protected void GetSignNums(string tourID)
        {
            if (!string.IsNullOrEmpty(tourID))
            {
                int nums = new EyouSoft.BLL.PlanStructure.BPlan().GetGuideSignNum(tourID);
                this.litSignNums.Text = nums.ToString();
            }
        }
        #endregion

        #region 导游收款 订单列表
        /// <summary>
        /// 导游收款 订单列表
        /// </summary>
        /// <param name="tourID">团号</param>
        protected void GetOrderListBytourID(string tourID)
        {
            if (!string.IsNullOrEmpty(tourID))
            {
                EyouSoft.Model.TourStructure.MOrderSum sum = new EyouSoft.Model.TourStructure.MOrderSum();
                IList<EyouSoft.Model.TourStructure.MTourOrder> orders = new EyouSoft.BLL.TourStructure.BTourOrder().GetTourOrderListById(ref  sum, tourID);
                if (orders != null && orders.Count > 0)
                {
                    this.litsumPrices.Text = UtilsCommons.GetMoneyString(orders.Sum(p => p.GuideIncome), ProviderToMoney);
                    this.repGuidPayment.DataSource = orders;
                    this.repGuidPayment.DataBind();
                }
                else
                {
                    this.tabGuidPaymentView.Visible = false;
                }
            }
        }
        #endregion

        #region 绑定导游借款列表
        /// <summary>
        /// 绑定导游借款列表
        /// </summary>
        /// <param name="tourID">团号</param>
        protected void GetDebitListByTourId(string tourID)
        {
            if (!string.IsNullOrEmpty(tourID))
            {
                IList<EyouSoft.Model.FinStructure.MDebit> detitlist = new EyouSoft.BLL.FinStructure.BFinance().GetDebitLstByTourId(tourID, false);
                if (detitlist != null && detitlist.Count > 0)
                {
                    this.repGuidPayList.DataSource = detitlist;
                    this.repGuidPayList.DataBind();
                }
            }
        }
        #endregion

        #region 删除导游借款
        /// <summary>
        /// 删除导游借款
        /// </summary>
        /// <param name="Id">借款id</param>
        /// <returns></returns>
        protected string DeleteDebitByID(string Id)
        {
            string setMsg = string.Empty;
            if (!string.IsNullOrEmpty(Id))
            {
                if (new EyouSoft.BLL.FinStructure.BFinance().DeleteDebit(this.SiteUserInfo.CompanyId, Convert.ToInt32(Id)))
                {
                    setMsg = UtilsCommons.AjaxReturnJson("1", "删除成功！");
                }
                else
                {
                    setMsg = UtilsCommons.AjaxReturnJson("0", "删除失败！");
                }
            }
            return setMsg;
        }
        #endregion

        #region 财务状态
        /// <summary>
        /// 财务状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        protected string GetFinStatus(EyouSoft.Model.EnumType.FinStructure.FinStatus? status, EyouSoft.Model.EnumType.TourStructure.TourStatus tStatus, string id)
        {
            string str = "";

            if ((status != EyouSoft.Model.EnumType.FinStructure.FinStatus.财务待审批 || tStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.封团) && id != "")
            {
                return str;
            }
            if (id != "")
            {
                str = "<a href='javascript:' data-class='savePreApp'><img src='/images/y-delupdateicon.gif' border='0' data-id='" + id + "' />修改</a> <a href='javascript:' data-class='deletePreApp'><img src='/images/y-delicon.gif' alt='' data-id='" + id + "' />删除</a>";
            }
            else
            {
                if (tStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未结算 || tStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.销售待审 || tStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.计调待审 || tStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.待终审 || tStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.财务核算 || tStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.封团 || tStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.已取消 || tStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.垫付申请 || tStatus == EyouSoft.Model.EnumType.TourStructure.TourStatus.审核失败)
                {
                    ret = true;
                    str = "";
                }
                else
                {
                    str = "<a data-class=\"savePreApp\" href=\"javascript:void(0);\"><img border=\"0\"  src=\"/images/addicon.gif\">申请</a>";
                }
            }
            return str;
        }

        #endregion

        #region 添加 修改 借款
        /// <summary>
        /// 导游借款 添加 修改
        /// </summary>
        /// <returns></returns>
        protected string debitAddOrUpdate()
        {
            string setMsg = string.Empty;
            string msgArr = string.Empty;
            EyouSoft.Model.FinStructure.MDebit debit = new EyouSoft.Model.FinStructure.MDebit();
            debit.CompanyId = this.SiteUserInfo.CompanyId;
            debit.DeptId = this.SiteUserInfo.DeptId;
            debit.IssueTime = System.DateTime.Now;
            debit.Operator = this.SiteUserInfo.Name;
            debit.OperatorId = this.SiteUserInfo.UserId;
            //借款编号
            debit.Id = Utils.GetInt(Utils.GetQueryStringValue("ID"));
            //团号
            debit.TourId = Utils.GetQueryStringValue("TourId");
            debit.TourCode = tourCode;
            //借款用途
            debit.UseFor = Utils.GetQueryStringValue("txtUseFor");
            //借款人
            string Borrower = Utils.GetQueryStringValue("guidName");
            string BorrowerId = Utils.GetQueryStringValue("guid");
            if (string.IsNullOrEmpty(Borrower) && string.IsNullOrEmpty(BorrowerId))
            {
                msgArr += "请选择借款人!<br/>";
            }
            else
            {
                debit.Borrower = Borrower;
                debit.BorrowerId = BorrowerId;
            }
            //借款时间
            DateTime BorrowTime = Utils.GetDateTime(Utils.GetQueryStringValue("txtBorrowTime"));
            if (string.IsNullOrEmpty(BorrowTime.ToString()))
            {
                msgArr += "请选择借款时间！<br/>";
            }
            else
            {
                debit.BorrowTime = BorrowTime;
            }
            //预借金额 实借金额
            decimal RealAmount = Utils.GetDecimal(Utils.GetQueryStringValue("txtRealAmount"));
            decimal BorrowAmount = Utils.GetDecimal(Utils.GetQueryStringValue("txtBorrowAmount"));
            if (BorrowAmount <= 0)
            {
                msgArr += "请输入预借金额！<br/>";
            }
            else
            {
                debit.RealAmount = RealAmount;
                debit.BorrowAmount = BorrowAmount;
            }
            //欲领签单数 实领签单数
            int PreSignNum = Utils.GetInt(Utils.GetQueryStringValue("txtPreSignNum"));
            int RelSignNum = Utils.GetInt(Utils.GetQueryStringValue("txtRelSignNum"));
            if (PreSignNum <= 0 && !Utils.GetQueryStringValue("txtPreSignNum").Equals("0"))
            {
                msgArr += "请输入预领签单数！<br/>";
            }
            else
            {
                debit.PreSignNum = PreSignNum;
                debit.RelSignNum = RelSignNum;
            }
            if (!string.IsNullOrEmpty(msgArr))
            {
                setMsg = UtilsCommons.AjaxReturnJson("0", "" + msgArr + "");
                return setMsg;
            }

            if (new EyouSoft.BLL.FinStructure.BFinance().AddOrUpdDebit(debit))
            {
                setMsg = UtilsCommons.AjaxReturnJson("1", "操作成功！");
            }
            else
            {
                setMsg = UtilsCommons.AjaxReturnJson("0", "操作失败！");
            }

            return setMsg;
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
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_备用金申请))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_出境计调_备用金申请, false);
                        return;
                    }
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.地接团队:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_备用金申请))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_地接计调_备用金申请, false);
                        return;
                    }
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团散拼短线:
                case EyouSoft.Model.EnumType.TourStructure.TourType.组团团队:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_备用金申请))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.计调中心_组团计调_备用金申请, false);
                        return;
                    }
                    break;
            }
        }
        #endregion
    }
}
