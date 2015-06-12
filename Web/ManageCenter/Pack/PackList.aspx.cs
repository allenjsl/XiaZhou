using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Model.ComStructure;
namespace Web.ManageCenter.Pack
{
    /// <summary>
    /// 行政中心-公司合同-列表
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-04-05
    public partial class PackList : BackPage
    {
        #region 页面参数
        /// <summary>
        /// 页大小
        /// </summary>
        private int pageSize = 20;
        /// <summary>
        /// 页码
        /// </summary>
        private int pageIndex = 1;
        /// <summary>
        /// 总记录数
        /// </summary>
        private int recordCount = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Request.QueryString["doType"];
            //存在ajax请求
            if (doType == "delete")
            {
                AJAX();
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
            //获取查询参数
            string txtType = Utils.GetQueryStringValue("txtType"); //合同编号
            DateTime? txtStartTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtStartTime"));
            DateTime? txtEndTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndTime"));
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            EyouSoft.BLL.GovStructure.BContract BLL = new EyouSoft.BLL.GovStructure.BContract();
            IList<EyouSoft.Model.GovStructure.MGovContract> lst = BLL.GetGovContractList(this.SiteUserInfo.CompanyId, txtType, txtStartTime, txtEndTime, this.pageSize, this.pageIndex, ref this.recordCount);
            if (null != lst && lst.Count > 0)
            {
                this.RepList.DataSource = lst;
                this.RepList.DataBind();
                if (recordCount <= pageSize)
                {
                    this.ExporPageInfoSelect1.Visible = false;
                }
                else
                {
                    BindPage();
                }

            }
            else
            {
                this.RepList.Controls.Add(new Label() { Text = "<tr><td colspan='5' align='center'>对不起，没有相关数据！</td></tr>" });
                this.ExporPageInfoSelect1.Visible = false;
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
        }
        /// <summary>
        /// ajax操作
        /// </summary>
        private void AJAX()
        {
            string msg = string.Empty;
            bool result = false;
            //对应执行操作
            string id = Utils.GetQueryStringValue("id");
            if (!string.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.GovStructure.BContract BLL = new EyouSoft.BLL.GovStructure.BContract();
                result = BLL.DeleteGovContract(id.Split(','));
                msg = result ? "删除成功！" : "删除失败！";
            }
            //返回ajax操作结果
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
            Response.End();
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_公司合同_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_公司合同_栏目, false);
            }
            else
            {
                ph_Add.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_公司合同_新增);
                ph_Update.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_公司合同_修改);
                ph_Del.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_公司合同_删除);
            }
        }
        #endregion

        #region 前台调用方法
        /// <summary>
        /// 返回文件下载
        /// </summary>
        /// <param name="o"></param>
        protected string getFile(object o)
        {
            StringBuilder str = new StringBuilder();
            if (null != o)
            {
                IList<EyouSoft.Model.ComStructure.MComAttach> lst = (IList<EyouSoft.Model.ComStructure.MComAttach>)o;
                if (null != lst && lst.Count > 0)
                {
                    foreach (MComAttach m in lst)
                    {
                        str.AppendFormat("<p align=\"left\"><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank' >{1}</a></p>", m.FilePath, m.Name);
                    }
                }
            }
            return str.Length == 0 ? "<span class=\"fontgray\">无</span>" : str.ToString();
        }
        #endregion
    }
}