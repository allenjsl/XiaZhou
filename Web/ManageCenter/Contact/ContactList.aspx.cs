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
namespace Web.ManageCenter.Contact
{
    /// <summary>
    /// 行政中心-内部通讯录-列表
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-03-26
    public partial class ContactList : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 页大小
        /// </summary>
        private int pageSize = 20;
        /// <summary>
        /// 页码
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        private int recordCount = 0;
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
            if (UtilsCommons.IsToXls()) ToXls();
        }

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            string sectionName = Utils.GetQueryStringValue(this.SelectSection1.SelectNameClient);
            string sectionID = Utils.GetQueryStringValue(this.SelectSection1.SelectIDClient);
            this.SelectSection1.SectionName = sectionName;
            this.SelectSection1.SectionID = sectionID;
            string txtName = Utils.GetQueryStringValue("txtName");//姓名
            EyouSoft.BLL.GovStructure.BArchives BLL = new EyouSoft.BLL.GovStructure.BArchives();
            IList<EyouSoft.Model.GovStructure.MGovFile> lst = BLL.GetSearchArchivesList(txtName, sectionID, sectionName, this.SiteUserInfo.CompanyId, this.pageSize, this.pageIndex, ref this.recordCount);
            if (null != lst && lst.Count > 0)
            {
                this.RepList.DataSource = lst;
                this.RepList.DataBind();
                if (recordCount <= pageSize)
                {
                    this.ExporPageInfoSelect1.Visible = false;
                    this.ExporPageInfoSelect1.intRecordCount = recordCount;
                }
                else
                {
                    BindPage();
                }
            }
            else
            {
                this.RepList.Controls.Add(new Label() { Text = "<tr><td colspan='8' align='center'>对不起，没有相关数据！</td></tr>" });
                this.ExporPageInfoSelect1.Visible = false;

            }
        }
        #endregion

        #region 绑定分页
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
        #endregion

        #region 获取部门
        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string getSectionInfo(object o)
        {
            if (null == o)
                return "";
            string[] strArr = { };
            List<string> lstStr = new List<string>();
            IList<MGovFilePosition> lst = (IList<MGovFilePosition>)o;
            if (null != lst && lst.Count > 0)
            {
                foreach (MGovFilePosition m in lst)
                {
                    lstStr.Add(m.Title);
                }
            }
            strArr = lstStr.ToArray();
            return strArr == null ? "" : String.Join(",", strArr);
        }
        #endregion

        /// <summary>
        /// 导出
        /// </summary>
        void ToXls()
        {
            string browser = this.Context.Request.UserAgent.ToUpper();
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);
            int recordCount = 0;
            StringBuilder s = new StringBuilder();
            EyouSoft.BLL.GovStructure.BArchives BLL = new EyouSoft.BLL.GovStructure.BArchives();
            var data = BLL.GetSearchArchivesList(Utils.GetQueryStringValue("txtName"), Utils.GetQueryStringValue(this.SelectSection1.SelectIDClient), Utils.GetQueryStringValue(this.SelectSection1.SelectNameClient), this.SiteUserInfo.CompanyId, toXlsRecordCount, 1, ref recordCount);
            if (data != null && data.Count > 0)
            {
                s.AppendFormat("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset=UTF-8\"/><table border='1' style='border-collapse:collapse;'><tr><th width='105'>档案编号</th><th width='105'>姓名</th><th width='107'>部门</th><th width='168'>职务</th><th width='168'>电话</th><th width='158'>手机</th><th width='164'>QQ</th>");
                foreach (var item in data)
                {
                    s.AppendFormat("<tr><td align='center'>{0}</td><td align='center'>{1}</td><td align='center'>{2}</td><td align='center'>{3}</td><td align='center'>{4}</td><td align='center'>{5}</td><td align='center'>{6}</td></tr>", item.FileNumber, item.Name, item.DepartName, getSectionInfo(item.GovFilePositionList), item.Contact, item.Mobile, item.qq);
                }
                s.AppendFormat("</table>");
            }
            string fileName = "内部通讯录";
            if (browser.Contains("MS") && browser.Contains("IE"))
            {
                fileName = System.Web.HttpUtility.UrlEncode(fileName, Encoding.UTF8);
            }
            ResponseToXls(s.ToString(), Encoding.UTF8, fileName);
        }


        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_内部通讯录_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_内部通讯录_栏目, false);
            }
        }
        #endregion

    }
}