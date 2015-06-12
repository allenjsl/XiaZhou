using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.ContractManage
{
    /// <summary>
    /// 合同管理-合同列表
    /// </summary>
    /// 创建人：邵权江
    /// 创建时间：2011-10-12
    public partial class ContractNumList : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        protected int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        protected int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        private int pageCount = 0;
        #endregion
        /// <summary>
        /// 合同类型下拉列表
        /// </summary>
        protected string sel_conType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            switch (doType)
            {
                case "delete":
                    Response.Clear();
                    Response.Write(DeleteData("id"));
                    Response.End();
                    break;
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

        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            #region
            //合同类型
            int conType = Utils.GetInt(Utils.GetQueryStringValue("sel_conType"),-1);
            sel_conType = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.ConStructure.ContractType)), conType.ToString(),true);
            #endregion
            //开始号
            int ConStart = Utils.GetInt(Utils.GetQueryStringValue("txtConStart"), -1);
            //结束号
            int ConEnd = Utils.GetInt(Utils.GetQueryStringValue("txtConEnd"), -1);
            //领用人
            string SelerId = Utils.GetQueryStringValue("ctl00_ContentPlaceHolder1_Seller1_hideSellID");
            string SelerName = Utils.GetQueryStringValue("ctl00_ContentPlaceHolder1_Seller1_txtSellName");
            if (SelerId != "" && SelerName != "")
            {
                this.Seller1.SellsID = SelerId;
                this.Seller1.SellsName = SelerName;
            }

            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            EyouSoft.BLL.ConStructure.BContractNum BLL = new EyouSoft.BLL.ConStructure.BContractNum();
            EyouSoft.Model.ConStructure.MContractNum searchModel = null;
            if (conType >= 0 || ConStart > 0 || ConEnd > 0 || SelerId != "")
            {
                searchModel = new EyouSoft.Model.ConStructure.MContractNum();
                if (conType >= 0)
                {
                    searchModel.ContractType = (EyouSoft.Model.EnumType.ConStructure.ContractType)conType;
                }
                //if (ConStart > 0)
                //{
                //    searchModel.StartNum = ConStart;
                //}
                //if (ConEnd > 0)
                //{
                //    searchModel.EndNum = ConEnd;
                //}
                //if (SelerId != "")
                //{
                //    searchModel.UseOperatorId = SelerId;
                //}
            }
            //IList<EyouSoft.Model.ConStructure.MContractNum> list = BLL.GetContractNumList(this.SiteUserInfo.CompanyId, searchModel, this.pageSize, this.pageIndex, ref this.recordCount, ref this.pageCount);
            //if (null != list && list.Count > 0)
            //{
            //    this.RepList.DataSource = list;
            //    this.RepList.DataBind();
            //    BindPage();
            //}
            //else
            //{
            //    this.RepList.Controls.Add(new Label() { Text = "<tr><td colspan='8' align='center'>对不起，没有相关数据！</td></tr>" });
            //    this.ExporPageInfoSelect1.Visible = false;
            //    this.ExporPageInfoSelect2.Visible = false;
            //}
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
            //if (!this.CheckGrant(Common.Enum.TravelPermission.合同管理_合同管理_栏目))
            //{
            //    Utils.ResponseNoPermit(Common.Enum.TravelPermission.合同管理_合同管理_栏目, false);
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
