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

namespace Web.ResourceManage.Path
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2011-10-09
    /// 说明: 资源管理： 路线库
    public partial class PathList : BackPage
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
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;
        #endregion
        #region 获取打印单
        protected string PrintPageXL = string.Empty;
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
            string PathType = string.IsNullOrEmpty(Utils.GetQueryStringValue("PathType")) ? null : Utils.GetQueryStringValue("PathType");
            PrintPageXL= new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.线路资源打印单);
            //路线名称
            string PathName = Utils.GetQueryStringValue("txtPathName");
            //天数
            string DataCount = Utils.GetQueryStringValue("txtData");
            //发布人
            string Auther = Utils.GetQueryStringValue("txtAuthor");
            //开始时间
            DateTime? StartTime = String.IsNullOrEmpty(Utils.GetQueryStringValue("txtStartTime")) ? null : (DateTime?)DateTime.Parse(Utils.GetQueryStringValue("txtStartTime"));
            //结束时间
            DateTime? EndTime = String.IsNullOrEmpty(Utils.GetQueryStringValue("txtEndTime")) ? null : (DateTime?)DateTime.Parse(Utils.GetQueryStringValue("txtEndTime"));
            //导出处理
            if (UtilsCommons.IsToXls()) ListToExcel(PathType, PathName, DataCount, Auther, StartTime, EndTime);
            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                //路线类型
               
                //初始化
                DataInit(PathType, PathName, DataCount, Auther, StartTime, EndTime);
            }

        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit(string pathtype, string pathname, string datacount, string auther, DateTime? starttime, DateTime? endtime)
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));

            EyouSoft.BLL.SourceStructure.BSource bsource = new EyouSoft.BLL.SourceStructure.BSource();
            EyouSoft.Model.SourceStructure.MRouteListModel listmodel = new EyouSoft.Model.SourceStructure.MRouteListModel();
            listmodel.DayCount = Utils.GetInt(datacount);
            listmodel.OperatorName = auther;
            listmodel.RouteName = pathname;
            listmodel.RouteAreaId = Utils.GetInt(pathtype);
            listmodel.CompanyId = this.SiteUserInfo.CompanyId;
            IList<EyouSoft.Model.SourceStructure.MRouteListModel> Pathlist = bsource.GetRouteShowModel(listmodel, starttime, endtime, this.pageIndex, this.pageSize, ref recordCount);
            if (Pathlist != null && Pathlist.Count > 0)
            {
                rptList.DataSource = Pathlist;
                rptList.DataBind();
            }
            else
            {
                this.rptList.Controls.Add(new Label() { Text = "<tr><td colspan='8' align='center'>对不起，没有相关数据！</td></tr>" });
                ExporPageInfoSelect1.Visible = false;
            }
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
                    if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_线路资源_删除))
                    {
                        string id = Utils.GetQueryStringValue("id");
                        msg = this.DeleteData(id);
                    }
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
            int b = 0;
            if (!String.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.SourceStructure.BSource bll = new EyouSoft.BLL.SourceStructure.BSource();
                b = bll.DeleteRouteModel(id.Split(','));
            }
            return b == 1 ? UtilsCommons.AjaxReturnJson("1", "删除成功!") : UtilsCommons.AjaxReturnJson("0", "删除失败!");
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_线路资源_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_线路资源_栏目, false);
                return;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_线路资源_新增))
            {
                this.path_add.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_线路资源_修改))
            {
                this.path_edit.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_线路资源_删除))
            {
                this.path_del.Visible = false;
            }

        }
        #endregion

        #region 导出Excel
        /// <summary>
        /// 导出Excel
        /// </summary>
        private void ListToExcel(string pathtype, string pathname, string datacount, string auther, DateTime? starttime, DateTime? endtime)
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);
            var s = new StringBuilder();
            //线路区域 	线路名称 	天数 	发布日期 	发布人 	上团数 	收客数 
            s.Append("线路区域\t线路名称\t天数\t发布日期\t发布人\t上团数\t收客数\n");
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            EyouSoft.BLL.SourceStructure.BSource bsource = new EyouSoft.BLL.SourceStructure.BSource();
            EyouSoft.Model.SourceStructure.MRouteListModel listmodel = new EyouSoft.Model.SourceStructure.MRouteListModel();
            listmodel.DayCount = Utils.GetInt(datacount);
            listmodel.OperatorName = auther;
            listmodel.RouteName = pathname;
            listmodel.RouteAreaId = Utils.GetInt(pathtype);
            listmodel.CompanyId = this.SiteUserInfo.CompanyId;
            IList<EyouSoft.Model.SourceStructure.MRouteListModel> list = bsource.GetRouteShowModel(listmodel, starttime, endtime, this.pageIndex, this.pageSize, ref recordCount);

            if (list != null && list.Count > 0)
            {
                foreach (var t in list)
                {
                    s.AppendFormat(
                        "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\n",
                        t.RouteTypeName,
                        t.RouteName,
                        t.DayCount.ToString(),
                        EyouSoft.Common.UtilsCommons.GetDateString(t.IssueTime,ProviderToDate),
                        t.OperatorName,
                        t.STCount.ToString(),
                        t.SKCount.ToString());
                }
            }

            ResponseToXls(s.ToString());
        }
        #endregion
        #region 前台调用方法
        /// <summary>
        /// 获取发布人信息
        /// </summary>
        /// <param name="id">SourceId</param>
        /// <returns></returns>
        protected string GetContactInfo(object List)
        {
            IList<EyouSoft.Model.ComStructure.MComUser> userlist = (IList<EyouSoft.Model.ComStructure.MComUser>)List;
            StringBuilder contactinfo = new StringBuilder();
            contactinfo.Append("<table cellspacing='0' cellpadding='0' border='0' width='100%' class='pp-tableclass'><tr class='pp-table-title'><th height='23' align='center'>电话</th><th align='center'>传真</th><th align='center'>手机</th><th align='center'>QQ</th><th align='center'>MSN</th><th align='center'>E-mail</th></tr>");
            if (userlist != null && userlist.Count > 0)
            {
                contactinfo.Append("<tr><td align='center'>" + userlist[0].ContactTel + "</td><td align='center'>" + userlist[0].ContactFax + "</td><td align='center' >" + userlist[0].ContactMobile + "</td><td align='center'>" + userlist[0].QQ + "</td><td align='center'>" + userlist[0].MSN + "</td><td align='center'>" + userlist[0].ContactEmail + "</td></tr>");

            }
            contactinfo.Append("</table>");
            return contactinfo.ToString();
        }
        #endregion
    }
}
