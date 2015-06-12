using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.ConStructure;
using EyouSoft.BLL.ConStructure;
using EyouSoft.Common.Function;
namespace Web.ContractManage
{
    /// <summary>
    /// 合同管理-合同号段-领用列表
    /// </summary>
    /// 创建人：邵权江
    /// 创建时间：2011-10-14
    public partial class ContractNumCollarList : BackPage
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
        /// <summary>
        /// 总页数
        /// </summary>
        private int pageCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
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
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            EyouSoft.BLL.ConStructure.BContractNum BLL = new EyouSoft.BLL.ConStructure.BContractNum();
            //IList<EyouSoft.Model.ConStructure.MContractNumCollar> list = BLL.GetContractNumCollarList(Utils.GetQueryStringValue("id"), this.pageSize, this.pageIndex, ref recordCount, ref pageCount);
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
