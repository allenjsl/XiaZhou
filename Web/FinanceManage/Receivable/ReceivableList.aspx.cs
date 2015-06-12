using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eyousoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.FinStructure;
using EyouSoft.BLL.FinStructure;
using Common.Enum;

namespace Web.FinanceManage.Receivable
{
    /// <summary>
    /// 应收账款-列表-
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-8
    public partial class ReceivableList : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(只读)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        /// 需要在程序中改变则去掉readonly修饰
        private readonly int pageSize = 20;
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
                object a1= true;
                object a2 = null;
                bool? b = (bool?)a2;
                if (b == true)
                {

                }
                //权限判断
                PowerControl();
                //初始化
                DataInit();
            }
        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            #region 查询实体
            MReceivableBase queryModel = new MReceivableBase();
            queryModel.CompanyId = CurrentUserCompanyID;
            queryModel.Customer = Utils.GetQueryStringValue("txt_customerUnitName");
            queryModel.IsClean = false;
            queryModel.OrderCode = Utils.GetQueryStringValue("");
            queryModel.Salesman = Utils.GetQueryStringValue(this.SellsSelect1.UniqueID.Replace('$', '_') + "_txtSellName");
            queryModel.TourCode = Utils.GetQueryStringValue("");
            #endregion

            IList<MReceivableInfo> ls = new BFinance().GetReceivableInfoLst(
                pageSize,
                pageIndex,
                ref  recordCount,
                CheckGrant(TravelPermission.账务管理_应收管理_查看全部),
                false,
                queryModel);
            if (ls != null && ls.Count > 0)
            {
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                BindPage();
            }
            else
            {
                lbl_msg.Text = "没有相关数据！";
                ExporPageInfoSelect1.Visible = false;
                ExporPageInfoSelect2.Visible = false;

            }
            CustomerUnitSelect1.ThisTitle = "客户单位";
            CustomerUnitSelect1.CustomerUnitName = Utils.GetQueryStringValue("txt_customerUnitName");
            SellsSelect1.SetTitle = "销售员";
            SellsSelect1.SellsName = Utils.GetQueryStringValue(this.SellsSelect1.UniqueID.Replace('$', '_') + "_txtSellName");
            //绑定分页
            BindPage();

        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            //this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            //this.ExportPageInfo1.UrlParams = Request.QueryString;
            //this.ExportPageInfo1.intPageSize = pageSize;
            //this.ExportPageInfo1.CurrencyPage = pageIndex;
            //this.ExportPageInfo1.intRecordCount = recordCount;
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

        }

        #endregion
    }
}
