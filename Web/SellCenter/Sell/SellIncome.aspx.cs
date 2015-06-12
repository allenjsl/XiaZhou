using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace Web.SellCenter.Sell
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：戴银柱
    /// 创建时间：2011-9-13
    /// 说明：同业分销 中 销售收款列表
    public partial class SellIncome : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 10;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Request.QueryString["doType"];
            //存在ajax请求
            if (doType != null && doType.Length > 0)
            {
                AJAX(doType);
            }
            #endregion
            if (!IsPostBack)
            {
                //权限判断
                PowerControl();

                //团号
                string tourNum = Utils.GetQueryStringValue("tourNum");
                //订单号
                string orderNum = Utils.GetQueryStringValue("orderNum");
                //客户单位
                string customerName = Utils.GetQueryStringValue("customerName");
                //销售员姓名
                string salesMan = Utils.GetQueryStringValue("salesMan");

                //初始化
                DataInit(tourNum,orderNum,customerName,salesMan);
            }

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit(string tourNum, string orderCode, string customerName, string salesMan)
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));

            EyouSoft.Model.FinStructure.MReceivableBase seachModel = new EyouSoft.Model.FinStructure.MReceivableBase();
            seachModel.CompanyId = SiteUserInfo.CompanyId;
            seachModel.Customer = customerName;
            //seachModel.TourCode = tourNum;
            //seachModel.OrderCode = orderCode;
            //seachModel.Salesman = salesMan;
            //IList<EyouSoft.Model.FinStructure.MReceivableInfo> list = new EyouSoft.BLL.FinStructure.BFinance().GetReceivableInfoLst(pageSize, pageIndex, ref recordCount, false, false, seachModel);
            //if (list != null && list.Count > 0)
            //{
            //    this.rptList.DataSource = list;
            //    this.rptList.DataBind();
            //}
            //else {
            //    this.lblMsg.Text = "没有相关数据!";
            //    this.ExporPageInfoSelect1.Visible = false;
            //    this.ExporPageInfoSelect2.Visible = false;
            //}

            //绑定分页
            BindPage();
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
        }
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
                    string id = Utils.GetQueryStringValue("id");
                    //执行并获取结果
                    msg = DeleteData(id);
                    break;
                default:
                    break;
            }
            //返回ajax操作结果
            Response.Clear();
            Response.Write(msg);
            Response.End();
        }
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id">删除ID</param>
        /// <returns></returns>
        private string DeleteData(string id)
        {
            string msg = string.Empty;
            //删除操作
            return msg;
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            //if (!this.CheckGrant(Common.Enum.TravelPermission.同业分销_销售收款_栏目))
            //{
            //    Utils.ResponseNoPermit(Common.Enum.TravelPermission.同业分销_销售收款_栏目, false);
            //    return;
            //}
        }
        #endregion
        #region 前台调用方法
        /// <summary>
        /// 某某方法
        /// </summary>
        /// <param name="i">参数1</param>
        /// <param name="s">参数2</param>
        protected void XXX(int i, string s)
        {

        }
        #endregion
    }
}
