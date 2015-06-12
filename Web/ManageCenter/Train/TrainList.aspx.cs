using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.GovStructure;
using System.Text;
using EyouSoft.Model.ComStructure;
using EyouSoft.BLL.GovStructure;
namespace Web.ManageCenter.Train
{
    /// <summary>
    /// 行政中心-培训管理-列表
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-04-05
    public partial class TrainList : BackPage
    {
        private int pageSize = 20;
        private int pageIndex = 0;
        private int recordCount = 0;
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
            DateTime? txtSTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtSTime"));//培训时间
            DateTime? txtETime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtETime"));
            string title = Utils.GetQueryStringValue("txtTheme");
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            EyouSoft.BLL.GovStructure.BTrain BLL = new EyouSoft.BLL.GovStructure.BTrain();
            IList<EyouSoft.Model.GovStructure.MGovTrain> lst = BLL.GetGovTrainList(this.SiteUserInfo.CompanyId, txtSTime, txtETime, title, this.pageSize, this.pageIndex, ref this.recordCount);
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
                this.RepList.Controls.Add(new Label() { Text = "<tr><td colspan='6' align='center'>对不起，没有相关数据！</td></tr>" });
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
        private void AJAX(string doType)
        {
            string msg = string.Empty;
            bool result = false;
            string id = Utils.GetQueryStringValue("id");
            //执行并获取结果
            BTrain BLL = new BTrain();
            result = BLL.DeleteGovTrain(id.Split(','));
            msg = result ? "删除成功！" : "删除失败！";
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
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_培训管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_培训管理_栏目, false);
            }
            else
            {
                this.ph_Add.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_培训管理_新增);
                this.ph_Update.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_培训管理_修改);
                this.ph_Del.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_培训管理_删除);
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
                IList<MComAttach> lst = (IList<MComAttach>)o;
                if (null != lst && lst.Count > 0)
                {
                    foreach (MComAttach m in lst)
                    {
                        str.AppendFormat("<p align=\"left\"><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank' >{1}</a></p>", m.FilePath, m.Name.Length > 3 ? m.Name.Substring(3, m.Name.Length - 3) : "");
                    }
                }
            }
            return str.Length == 0 ? "<span class=\"fontgray\">无</span>" : str.ToString();
        }
        protected string getUserInfo(object o)
        {
            List<string> lstStr = new List<string>();
            if (null != o)
            {
                IList<MGovTrainStaff> lst = (IList<MGovTrainStaff>)o;
                if (null != lst && lst.Count > 0)
                {
                    foreach (MGovTrainStaff m in lst)
                    {
                        lstStr.Add(m.User);
                    }
                }
            }
            return String.Join(",", lstStr.ToArray());
        }
        #endregion
    }
}