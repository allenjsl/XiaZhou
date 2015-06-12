using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.BLL.IndStructure;
using EyouSoft.Model.IndStructure;
using EyouSoft.BLL.ComStructure;
using EyouSoft.Model.ComStructure;
using System.Data;

namespace Web.UserCenter.WorkExchange
{
    public partial class WorkReportList : BackPage
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 修改人：蔡永辉
        /// 创建时间：2012-4-1
        /// 说明：个人中心：工作汇报

        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 15;
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
            //权限判断
            PowerControl();
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (doType == "delete")
            {
                AJAX(doType);
            }
            #endregion
            if (!IsPostBack)
            {
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
            #region 搜索实体
            //搜索实体
            MWorkReportSearch modelsearch = new MWorkReportSearch();
            //标题
            string txtTitle = Utils.GetQueryStringValue("txtTitle");
            if (!string.IsNullOrEmpty(txtTitle))
                modelsearch.Title = txtTitle;
            //汇报人
            string txtName = Utils.GetQueryStringValue("txtName");
            if (!string.IsNullOrEmpty(txtName))
                modelsearch.OperatorName = txtName;
            //起始时间
            DateTime? txtBegin = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtBegin"));
            modelsearch.IssueTimeS = txtBegin;
            //结束时间
            DateTime? txtEnd = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEnd"));
            modelsearch.IssueTimeE = txtEnd;
            #endregion
            //实例化业务层
            BIndividual bllBIndividual = new BIndividual();
            IList<MWorkReport> list = bllBIndividual.GetWorkReportLst(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, modelsearch);
            if (list.Count > 0 && list != null)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
                //绑定分页
                BindPage();
            }
            else
            {
                this.lblMsg.Text = "没有相关数据!";
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

        #region ajax操作
        /// <summary>
        /// ajax操作
        /// </summary>
        private void AJAX(string doType)
        {
            //ajax操作参数
            string argument = Utils.GetQueryStringValue("argument");
            //ajax操作之后返回的信息
            string msg = DeleteData(argument);
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
            //返回删除之后的信息
            string msg = string.Empty;
            //实例化业务层
            BIndividual bllBIndividual = new BIndividual();
            string[] workReportIds = { "" };
            //判断删除是批量还是单个删除
            if (id.Contains(','))
                workReportIds = id.Split(',');
            else
                workReportIds[0] = id;
            //删除操作
            #region 转换string[]为int[]
            int[] workReportIdsint = { 0 };
            for(int i=0;i<workReportIds.Length;i++)
            {
                if(!string.IsNullOrEmpty(workReportIds[i]))
                {
                    workReportIdsint[i]=Utils.GetInt(workReportIds[i]);
                }
            }
            #endregion
            if (bllBIndividual.DelWorkReport(workReportIdsint))
                msg = UtilsCommons.AjaxReturnJson("true", "删除成功");
            else
                msg = UtilsCommons.AjaxReturnJson("false", "删除失败");
            //删除操作
            return msg;
        }
        #endregion

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_工作交流_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_工作交流_栏目, true);
                return;
            }

            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_工作交流_工作汇报栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_工作交流_工作汇报栏目, false);
                return;
            }
        }
        #endregion

        #region 前台调用方法
        /// <summary>
        /// 获取部门
        /// </summary>
        /// <param name="DepartmentID">部门ID</param>
        protected string GetDepartmentById(int DepartmentID, string companyid)
        {
            //返回信息
            string result = "";
            //实例化部门业务层
            BComDepartment BLL = new BComDepartment();
            MComDepartment model = BLL.GetModel(DepartmentID, companyid);
            if (model != null)
            {
                result = model.DepartName;
            }
            return result;
        }
        #endregion

        #region Repeater嵌套绑定
        /// <summary>
        /// Repeater嵌套绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            //判断里层repeater处于外层repeater的哪个位置（ AlternatingItemTemplate，FooterTemplate，
            //HeaderTemplate，，ItemTemplate，SeparatorTemplate）
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rep = e.Item.FindControl("rptCheckList") as Repeater;//找到里层的repeater对象
                //Repeater数据源
                rep.DataSource = ((MWorkReport)e.Item.DataItem).list;
                rep.DataBind();
            }
        }
        #endregion
    }
}
