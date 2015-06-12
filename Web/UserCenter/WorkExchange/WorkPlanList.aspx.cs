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

namespace Web.UserCenter.WorkExchange
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 修改人：蔡永辉
    /// 创建时间：2012-4-1
    /// 说明：个人中心：工作计划  列表
    public partial class WorkPlanList : BackPage
    {
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
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            if (!string.IsNullOrEmpty(doType))
                Ajax(doType);
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
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            #region 搜索实体
            //搜索实体
            MWorkPlanSearch modelsearch = new MWorkPlanSearch();
            //标题
            string txtTitle = Utils.GetQueryStringValue("txtTitle");
            if (!string.IsNullOrEmpty(txtTitle))
                modelsearch.Title = txtTitle;
            //计划人
            string txtName = Utils.GetQueryStringValue("txtName");
            if (!string.IsNullOrEmpty(txtName))
                modelsearch.OperatorName = txtName;
            //时间
            DateTime? txtBegin = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtBegin"));
            modelsearch.IssueTimeS = txtBegin;
            //结束时间
            DateTime? txtEnd = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEnd"));
            modelsearch.IssueTimeE = txtEnd;
            #endregion
            //实例化业务层
            BIndividual bllBIndividual = new BIndividual();
            IList<MWorkPlan> list = bllBIndividual.GetWorkPlanLst(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, modelsearch);
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
                rep.DataSource = ((MWorkPlan)e.Item.DataItem).list;
                rep.DataBind();
            }
        }
        #endregion

        /// <summary>
        /// ajax操作
        /// </summary>
        private void Ajax(string type)
        {
            //ajax操作之后的返回信息
            string resultmsg = "";
            //获取ajax请求操作id
            string argument = Utils.GetQueryStringValue("argument");
            if (!string.IsNullOrEmpty(type))
            {
                //存在ajax请求
                switch (type)
                {
                    case "delete":
                        if (!string.IsNullOrEmpty(argument))
                            resultmsg = DeleteData(argument);
                        else
                            resultmsg = UtilsCommons.AjaxReturnJson("false", "参数id为空");
                        break;
                }
            }
            else
                resultmsg = UtilsCommons.AjaxReturnJson("false", "参数类型为空");
            Response.Clear();
            Response.Write(resultmsg);
            Response.End();
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id">删除ID</param>
        /// <returns></returns>
        private string DeleteData(string id)
        {
            //返回信息
            string msg = string.Empty;
            //实例化业务层
            BIndividual bllBIndividual = new BIndividual();
            string[] workPlanIds = { "" };
            //判断删除是批量还是单个删除
            if (id.Contains(','))
                workPlanIds = id.Split(',');
            else
                workPlanIds[0] = id;
            //删除操作
            if (bllBIndividual.DelWorkPlan(workPlanIds))
                msg = UtilsCommons.AjaxReturnJson("true", "删除成功");
            else
                msg = UtilsCommons.AjaxReturnJson("false", "删除失败");
            //删除操作
            return msg;
        }
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
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_工作交流_工作计划栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_工作交流_工作计划栏目, false);
                return;
            }
        }
        #endregion
        #region 前台调用方法
        #endregion


    }
}
