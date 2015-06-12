using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.TourStructure;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.EnumType.ComStructure;
using System.Text;
using EyouSoft.Model.EnumType.TourStructure;
using EyouSoft.BLL.ComStructure;

namespace Web.SellCenter
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 修改人：蔡永辉
    /// 创建时间：2012-4-25
    /// 说明：同业分销 中 收客计划 列表

    public partial class CustomerPlanList : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int pageIndex = 1;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;
        #endregion

        /// <summary>
        /// 二级栏目编号
        /// </summary>
        protected int sl = 0;

        /// <summary>
        /// 页面类型1.组团2.地接团队3.出境团队
        /// </summary>
        protected int type = 0;

        /// <summary>
        /// 散拼行程单
        /// </summary>
        protected string PrintPageSp = string.Empty;

        /// <summary>
        /// 游客信息
        /// </summary>
        protected string PrintPageYk = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region ajax 操作
            //ajax操作类型
            string ajaxtype = "";
            ajaxtype = Utils.GetQueryStringValue("ajaxtype");
            if (!string.IsNullOrEmpty(ajaxtype))
                Ajax(ajaxtype);
            #endregion
            sl = Utils.GetInt(Utils.GetQueryStringValue("sl"));
            PrintPageSp = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, PrintTemplateType.散拼行程单);
            PrintPageYk = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, PrintTemplateType.订单信息汇总表);
            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
            }

            ////初始化
            DataInit();
        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            EyouSoft.Model.TourStructure.MTourSanPinSearch searchModel = new EyouSoft.Model.TourStructure.MTourSanPinSearch();
            #region 搜索条件
            //团号
            string teamNum = Utils.GetQueryStringValue("txtTourID");
            //线路名称
            string lineName = Utils.GetQueryStringValue("txtAreaName");
            //线路区域
            string areaLine = Utils.GetQueryStringValue("dropAreaID");
            //天数
            string txtday = Utils.GetQueryStringValue("txtday");
            //销售员
            string salesMan = Utils.GetQueryStringValue(this.SellsSelect1.SellsNameClient);
            this.SellsSelect1.SellsName = salesMan;
            //出团时间
            DateTime? leaveTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtstardate"));
            //回团时间
            DateTime? backTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtenddate"));
            //线路区域ID
            int areaID = Utils.GetInt(Utils.GetQueryStringValue("dropAreaID"));
            ///关键字编号
            int keyid = Utils.GetInt(Utils.GetQueryStringValue("KeyId"));
            #endregion
            searchModel.AreaId = areaID;
            searchModel.LLDate = backTime;
            searchModel.SLDate = leaveTime;
            searchModel.TourCode = teamNum;
            searchModel.RouteName = lineName;
            searchModel.TourDays = Utils.GetInt(txtday);
            searchModel.SellerName = salesMan;
            searchModel.KeyId = keyid;
            //声明list对象保存收客列表数据
            IList<MTourTongHanInfo> list = new EyouSoft.BLL.TourStructure.BTour().GetTYFXTourSanPinList(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, searchModel);
            if (list != null && list.Count > 0)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                //绑定分页
                BindPage();
                this.litMsg.Visible = false;
            }
            else
            {
                this.litMsg.Visible = true;
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;
            }

            //IList<EyouSoft.Model.ComStructure.MComArea> keyList = new EyouSoft.BLL.ComStructure.BComArea().GetAreaByCID(SiteUserInfo.CompanyId);

            IList<MKeyTour> keyList = new EyouSoft.BLL.TourStructure.BTour().GetKeyTour(SiteUserInfo.CompanyId);
            if (keyList != null && keyList.Count > 0)
            {
                this.rptKsy.DataSource = keyList;
                this.rptKsy.DataBind();

            }
            else
            {
                this.licMsg.Text = "";
            }

        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;

            this.ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect2.intPageSize = pageSize;
            this.ExporPageInfoSelect2.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect2.intRecordCount = recordCount;
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_收客计划_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.同业分销_收客计划_栏目, false);
                return;
            }
        }

        #region ajax操作
        private void Ajax(string type)
        {
            string result = "";
            switch (type)
            {
                case "OperatorInfo":
                    result = GetOperatorInfo();
                    break;
                case "MTourPriceStandard":
                    result = GetMTourPriceStandard();
                    break;
                case "GetOperatorInfo":
                    result = GetOperatorInfo();
                    break;
                case "AjaxLoadData":
                    DataInit();
                    break;
            }
            Response.Clear();
            Response.Write(result);
            Response.End();
        }



        /// <summary>
        /// 获取发布人信息
        /// </summary>
        /// <returns></returns>
        protected string GetOperatorInfo()
        {
            string result = "";
            string arguments = Utils.GetQueryStringValue("argument");
            /**/
            string[] listarg = arguments.Split('|');
            BTour bllBTour = new BTour();
            MPersonInfo modelMPersonInfo = new MPersonInfo();
            if (!string.IsNullOrEmpty(listarg[0]) || !string.IsNullOrEmpty(listarg[1]))
            {
                modelMPersonInfo = bllBTour.GetPersonInfo(listarg[1], listarg[0]);
                if (modelMPersonInfo != null)
                {
                    result = "联系电话:" + modelMPersonInfo.Phone + "<br/>联系手机:" + modelMPersonInfo.Mobile;
                    result = UtilsCommons.AjaxReturnJson("true", result);
                }
                else
                {
                    result = UtilsCommons.AjaxReturnJson("false", "数据为空");
                }
            }
            else
                result = UtilsCommons.AjaxReturnJson("false", "参数为空");


            return result;
        }

        #endregion

        #endregion

        #region 前台调用方法

        /// <summary>
        /// 获取计调员
        /// </summary>
        /// <param name="TourPlanerlist"></param>
        /// <returns></returns>
        protected string GetTourPlaner(object TourPlanerlist)
        {
            //返回的字符串
            string reslut = "";
            if (TourPlanerlist != null)
            {
                //转换为计调员列表
                IList<MTourPlaner> list = TourPlanerlist as IList<MTourPlaner>;
                if (list != null && list.Count > 0)
                {
                    foreach (MTourPlaner model in list)
                    {
                        if (model != null)
                        {
                            reslut += model.Planer + " ";
                        }
                    }
                }
            }
            return reslut;
        }

        /// <summary>
        /// 获取散拼计划报价标准
        /// </summary>
        /// <returns></returns>
        private string GetMTourPriceStandard()
        {
            //返回的信息
            string result = "";
            //计划编号
            string TourId = Utils.GetQueryStringValue("argument");
            //拼接数据
            string strb = string.Empty;
            IList<MTourPriceStandard> list = new EyouSoft.BLL.TourStructure.BTour().GetTourSanPinPrice(TourId);
            if (list != null && list.Count > 0)
            {
                strb = UtilsCommons.GetPriceStandardTable(list, ProviderToMoney);
                result = UtilsCommons.AjaxReturnJson("true", strb.ToString());
            }
            else
                result = UtilsCommons.AjaxReturnJson("false", "数据为空");
            return result;
        }



        /// <summary>
        /// 通过收客状态返回html
        /// </summary>
        /// <param name="tourId"></param>
        /// <param name="state"></param>
        /// <param name="sourceId"></param>
        /// <param name="isCheck"></param>
        /// <returns></returns>
        protected string GetHtmlByShouKeState(object tourId, object state, object sourceId, object isCheck, string tourType)
        {
            StringBuilder sb = new StringBuilder();
            bool c = isCheck == null ? false : (bool)isCheck;
            int isBaoMing = -1;
            EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus skState = (EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus)state;
            string strtype = "";
            if (tourType == TourType.出境散拼.ToString() || tourType == TourType.出境团队.ToString())
                strtype = "3";
            if (tourType == TourType.地接散拼.ToString() || tourType == TourType.地接团队.ToString())
                strtype = "2";
            if (tourType == TourType.组团散拼.ToString() || tourType == TourType.组团团队.ToString())
                strtype = "1";
            switch (skState)
            {
                case EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus.报名中:
                case EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus.手动客满:
                case EyouSoft.Model.EnumType.TourStructure.TourShouKeStatus.手动停收:
                    if (sourceId == null || sourceId.ToString().Trim() == "")
                    {
                        sb.Append("<a onclick='CustomerPlanList.OpenBaoMing(this);return false;' href='/TeamCenter/SanKeBaoMing.aspx?tourID=" + tourId.ToString() + "&sl=" + EyouSoft.Common.Utils.GetQueryStringValue("sl") + "&type=" + strtype + "'>" + skState.ToString() + "</a>");
                    }
                    if (sourceId != null && sourceId.ToString().Trim() != "" && c)
                    {
                        sb.Append("<a onclick='CustomerPlanList.OpenBaoMing(this);return false;' href='/TeamCenter/SanKeBaoMing.aspx?tourID=" + tourId.ToString() + "&sl=" + EyouSoft.Common.Utils.GetQueryStringValue("sl") + "&type=" + strtype + "'><b class='fontred' title='该计划来源于供应商' >" + skState.ToString() + "</b></a>");
                    }
                    break;
                default:
                    sb.Append("<b class='fontred'>" + skState.ToString() + "</b>");
                    break;
            }
            isBaoMing = Utils.GetInt(strtype);
            if (GetControl(isBaoMing))
                return sb.ToString();
            else
                return "无权报名";
        }


        /// <summary>
        /// 获取报名权限
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool GetControl(int type)
        {
            bool IsBaoMing = false;
            switch (type)
            {
                case 1:
                    if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.组团团队_组团散拼_订单报名))
                    {
                        IsBaoMing = true;
                    }
                    break;
                case 2:
                    if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.地接团队_组团散拼_订单报名))
                    {
                        IsBaoMing = true;
                    }
                    break;
                case 3:
                    if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.出境团队_组团散拼_订单报名))
                    {
                        IsBaoMing = true;
                    }
                    break;
                default:
                    IsBaoMing = false;
                    break;
            }
            return IsBaoMing;
        }





        /// <summary>
        /// 根据计划来源显示发布人信息
        /// </summary>
        /// <param name="sourceId"></param>
        /// <param name="operatorInfo"></param>
        /// <param name="ShowPublisher"></param>
        /// <returns></returns>
        protected string GetOperatorInfo(object sourceId, object operatorInfo, object sourceCompanyName)
        {
            string str = "<a href=\"javascript:void(0);\" data-comandsouid=\"{2}|{1}\" data-ispostback=\"false\">{0}</a>";
            if (sourceId == null || sourceId.ToString().Trim() == "")
            {
                if (operatorInfo != null)
                {
                    EyouSoft.Model.TourStructure.MOperatorInfo info = (EyouSoft.Model.TourStructure.MOperatorInfo)operatorInfo;
                    str = string.Format(str, info.Name, "", info.OperatorId);
                }
            }
            else
            {
                if (sourceCompanyName != null)
                {
                    str = string.Format(str, sourceCompanyName, sourceId, "");
                }
            }
            return string.Format(str, "", "", "");
        }

        /// <summary>
        /// 获取打印单url
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        protected string GetPrintUrl(string tourId)
        {
            if (!string.IsNullOrEmpty(tourId))
            {
                return PrintPageSp + "?tourid=" + tourId;
            }
            else
                return "#";
        }

        /// <summary>
        /// 获取打印单url
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        protected string GetPrintUrlYk(string tourId)
        {
            if (!string.IsNullOrEmpty(tourId))
            {
                return PrintPageYk + "?tourid=" + tourId;
            }
            else
                return "#";
        }

        #endregion
    }
}
