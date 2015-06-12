using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using System.Text;
using EyouSoft.Model.EnumType.TourStructure;

namespace Web.TeamCenter
{
    /// <summary>
    /// 报价列表
    /// 修改人：DYZ 
    /// 创建日期：2011.9.7
    /// </summary>
    public partial class PriceList : BackPage
    {

        #region 分页参数
        /// <summary>
        /// 每页显示条数(只读)
        /// </summary>
        private readonly int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 100;
        #endregion

        /// <summary>
        /// 页面类型1.组团2.地接团队3.出境团队
        /// </summary>
        protected int type = 0;
        /// <summary>
        /// 二级栏目编号
        /// </summary>
        protected int sl = 0;

        /// <summary>
        /// 组团打印单链接
        /// </summary>
        protected string PrintPageZt = string.Empty;

        /// <summary>
        /// 游客信息打印单
        /// </summary>
        protected string PrintPageYY = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 获得组团打印单链接
            PrintPageZt = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.团队行程单);
            #endregion

            PrintPageYY = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.游客名单);

            #region 获取页面参数
            type = Utils.GetInt(Utils.GetQueryStringValue("type"));
            sl = Utils.GetInt(Utils.GetQueryStringValue("sl"));
            #endregion

            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (doType.Length > 0)
            {
                AJAX(doType);
            }
            #endregion

            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                //初始化
                DataInit();
            }
        }

        #region 页面初始化
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {

            #region 获取查询条件

            //状态
            string status = Utils.GetQueryStringValue("sltStatus");

            string[] removeStatus = { ((int)QuoteState.垫付申请审核).ToString(), ((int)QuoteState.审核成功).ToString(), ((int)QuoteState.审核失败).ToString() };

            this.litStatue.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.QuoteState), removeStatus), status.ToString());
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));

            //线路区域ID
            int areaID = Utils.GetInt(Utils.GetQueryStringValue("ddlArea"));
            //线路ID、名称
            //string lineId = Utils.GetQueryStringValue("");
            string routeName = Utils.GetQueryStringValue("txtRouteName");
            //询价单位
            string comName = Utils.GetQueryStringValue(this.CustomerUnitSelect1.ClientNameKHMC);
            string comId = Utils.GetQueryStringValue(this.CustomerUnitSelect1.ClientNameKHBH);
            this.CustomerUnitSelect1.CustomerUnitId = comId;
            this.CustomerUnitSelect1.CustomerUnitName = comName;


            //销售员
            string sellerId = Utils.GetQueryStringValue(this.SellsSelect1.ClientID + "_hideSellID");
            string sellerName = Utils.GetQueryStringValue(this.SellsSelect1.ClientID + "_txtSellName");
            this.SellsSelect1.SellsID = sellerId;
            this.SellsSelect1.SellsName = sellerName;

            //报价员
            string operatorId = Utils.GetQueryStringValue(this.SellsSelect2.ClientID + "_hideSellID");
            string operatorName = Utils.GetQueryStringValue(this.SellsSelect2.ClientID + "_txtSellName");
            this.SellsSelect2.SellsID = operatorId;
            this.SellsSelect2.SellsName = operatorName;


            EyouSoft.Model.TourStructure.MTourQuoteSearch searchModel = new EyouSoft.Model.TourStructure.MTourQuoteSearch();
            searchModel.AreaId = areaID;
            searchModel.BuyCompanyID = comId;
            searchModel.BuyCompanyName = comName;
            searchModel.OperatorId = operatorId;
            searchModel.Operator = operatorName;
            if (status != "")
            {
                searchModel.QuoteState = (EyouSoft.Model.EnumType.TourStructure.QuoteState)Utils.GetInt(status);
            }
            searchModel.RouteName = routeName;
            searchModel.SellerId = sellerId;
            searchModel.SellerName = sellerName;
            #endregion

            EyouSoft.BLL.TourStructure.BQuote bll = new EyouSoft.BLL.TourStructure.BQuote();
            IList<EyouSoft.Model.TourStructure.MTourQuoteInfo> list = bll.GetTourQuoteList(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, searchModel, (EyouSoft.Model.EnumType.TourStructure.ModuleType)(type - 1));



            if (list != null && list.Count > 0)
            {
                //绑定分页
                rptList.DataSource = list;
                rptList.DataBind();
                BindPage();
                litMsg.Visible = false;
            }
            else
            {
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;
                litMsg.Visible = true;
            }
            //释放
            bll = null;
            list = null;
        }
        #endregion

        #region 分页
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;


            this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect2.intPageSize = pageSize;
            this.ExporPageInfoSelect2.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect2.intRecordCount = recordCount;
        }
        #endregion

        #region 处理AJAX请求
        /// <summary>
        /// ajax操作
        /// </summary>
        private void AJAX(string doType)
        {
            string msg = string.Empty;
            //对应执行操作
            switch (doType)
            {
                case "delete":
                    //判断权限
                    string[] ids = Utils.GetQueryStringValue("ids").Split(',');
                    //执行并获取结果
                    msg = DeleteData(ids);
                    break;
                default:
                    break;
            }
            //返回ajax操作结果
            Response.Clear();
            Response.Write(msg);
            Response.End();
        }
        #endregion

        #region 列表删除
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id">删除ID</param>
        /// <returns></returns>
        private string DeleteData(string[] ids)
        {
            string msg = string.Empty;
            //删除操作
            if (ids.Length > 0)
            {
                EyouSoft.BLL.TourStructure.BQuote bll = new EyouSoft.BLL.TourStructure.BQuote();
                foreach (string id in ids)
                {
                    bll.DeleteQuote(id);
                }
                msg = "{\"result\":\"1\",\"msg\":\"删除成功!\"}";
            }
            else
            {
                msg = "{\"result\":\"0\",\"msg\":\"请选择一行数据!\"}";
            }
            return msg;
        }
        #endregion

        #region 根据报价状态和次数显示信息
        /// <summary>
        /// 根据报价状态和次数显示信息
        /// </summary>
        /// <param name="count">报价次数</param>
        /// <param name="quoteState">报价状态</param>
        /// <returns></returns>
        protected string GetHtmlByState(string count, EyouSoft.Model.EnumType.TourStructure.QuoteState quoteState, string cancelReason)
        {
            StringBuilder sb = new StringBuilder();
            switch (quoteState)
            {
                case EyouSoft.Model.EnumType.TourStructure.QuoteState.报价成功:
                    sb.Append("<span class='fontgreen' data-class='QuoteState' data-state='" + (int)EyouSoft.Model.EnumType.TourStructure.QuoteState.报价成功 + "'>报价成功</span>"); break;
                case EyouSoft.Model.EnumType.TourStructure.QuoteState.取消报价:
                    sb.Append("<a data-class='cancelReason'><span class='fontgray' data-class='QuoteState' data-state='0'>取消报价</span></a><div style='display:none'><b>取消原因</b>:" + EyouSoft.Common.Function.StringValidate.TextToHtml(cancelReason) + "</div>"); break;
                case EyouSoft.Model.EnumType.TourStructure.QuoteState.垫付申请审核:
                    sb.Append("<span data-class='QuoteState' data-state='" + (int)EyouSoft.Model.EnumType.TourStructure.QuoteState.垫付申请审核 + "'>垫付申请审核</span>"); break;
                case EyouSoft.Model.EnumType.TourStructure.QuoteState.审核失败:
                    sb.Append("<span class='fontgray' data-class='QuoteState' data-state='0'>审核失败</span>"); break;
                case EyouSoft.Model.EnumType.TourStructure.QuoteState.审核成功:
                    sb.Append("<span data-class='QuoteState' data-state='0'>审核成功</span>"); break;
                case EyouSoft.Model.EnumType.TourStructure.QuoteState.未处理:
                    sb.Append("<span data-class='QuoteState' data-state='0'>第" + count + "次报价</span>"); break;
            }
            return sb.ToString();
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            switch (type)
            {
                case 1:
                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_团队报价_栏目))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_团队报价_栏目, true);
                        return;
                    }

                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_团队报价_新增))
                    {
                        this.phForAdd.Visible = false;
                        this.phForCopy.Visible = false;
                    }

                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_团队报价_删除))
                    {
                        this.phForDelete.Visible = false;
                    }
                    break;
                case 2:

                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_团队报价_栏目))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_团队报价_栏目, true);
                        return;
                    }

                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_团队报价_新增))
                    {
                        this.phForAdd.Visible = false;
                        this.phForCopy.Visible = false;
                    }

                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_团队报价_修改))
                    {
                        this.phForDelete.Visible = false;
                    }
                    break;
                case 3:

                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_团队报价_栏目))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_团队报价_栏目, true);
                        return;
                    }

                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_团队报价_新增))
                    {
                        this.phForAdd.Visible = false;
                        this.phForCopy.Visible = false;
                    }

                    if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_团队报价_删除))
                    {
                        this.phForDelete.Visible = false;
                    }
                    break;
            }
        }
        #endregion

        #region 获取询价状态
        /// <summary>
        /// 获取询价状态
        /// </summary>
        /// <returns></returns>
        protected string GetQuoteStateFormOper(string planerId, bool isPlanerQuote)
        {
            string str = string.Empty;
            if (planerId.Trim() == "")
            {
                str = "无询价";
            }
            if (planerId.Trim() != "" && isPlanerQuote)
            {
                str = "已回复";
            }

            if (planerId.Trim() != "" && isPlanerQuote == false)
            {
                str = "询价中";
            }
            return str;
        }
        #endregion


        /// <summary>
        /// 传递报价成功的团队编号给打印单
        /// </summary>
        /// <param name="tourModel">订单实体</param>
        /// <param name="qState">报价状态</param>
        /// <returns>返回报价成功的打印单链接</returns>
        protected string GetPrintUrl(object tourModel, object qState)
        {

            if (tourModel != null)
            {
                EyouSoft.Model.TourStructure.MTourQuoteTourInfo model = (EyouSoft.Model.TourStructure.MTourQuoteTourInfo)tourModel;
                if ((int)qState == (int)QuoteState.报价成功)
                {
                    if (model.TourId.Trim() != "")
                    {
                        return PrintPageYY + "?tourid=" + model.TourId;
                    }
                    else
                    {
                        return "javascript:void(0);";
                    }
                }
                else
                {
                    return "javascript:void(0);";
                }
            }
            else
            {
                return "javascript:void(0);";
            }
        }


    }
}
