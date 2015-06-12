using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.IndStructure;

namespace EyouSoft.Web.UserCenter.WorkAwake.Preview
{
    public partial class PreControlOther : BackPage
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
        private int pageIndex = 1;
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
            //设置选择
            this.UserCenterNavi1.NavIndex = 8;
            this.UserCenterNavi1.PrivsList = SiteUserInfo.Privs;
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
            BIndividual bllBIndividual = new BIndividual();
            var list = bllBIndividual.GetPreControlOtherUsedLst(pageSize, pageIndex, ref recordCount, SiteUserInfo.CompanyId, SiteUserInfo.UserId);
            if (list != null && list.Count > 0)
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
                this.ExporPageInfoSelect2.Visible = false;
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

            this.ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect2.intPageSize = pageSize;
            this.ExporPageInfoSelect2.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect2.intRecordCount = recordCount;
        }
        /// <summary>
        /// ajax操作
        /// </summary>
        private void AJAX(string doType)
        {
            string msg = string.Empty;
            string argument = Utils.GetQueryStringValue("argument");
            //对应执行操作
            switch (doType)
            {
                case "SetLastTime":
                    //执行并获取结果
                    msg = SetLastTime(argument);
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
        /// 设置几天前提醒
        /// </summary>
        /// <param name="id">天数</param>
        /// <returns></returns>
        private string SetLastTime(string day)
        {
            string msg = string.Empty;
            if (new EyouSoft.BLL.ComStructure.BComSetting().SetOtherPreControlRemind(SiteUserInfo.CompanyId, Utils.GetInt(day)))
            {
                msg = UtilsCommons.AjaxReturnJson("true", "您设置其他预控到期前" + day + "提醒成功");
            }
            else
                msg = UtilsCommons.AjaxReturnJson("false", "您设置其他预控到期前" + day + "提醒失败");

            return msg;
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_栏目, true);
                return;
            }

            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_预控到期提醒栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_预控到期提醒栏目, true);
                return;
            }
        }
        #endregion
        #region 前台调用方法
        /// <summary>
        /// 获取景点预控提前提醒天数
        /// </summary>
        /// <returns></returns>
        protected string GetLastTime()
        {
            var setting = new EyouSoft.BLL.ComStructure.BComSetting().GetModel(SiteUserInfo.CompanyId);
            if (setting != null) return setting.OtherControlRemind.ToString();

            return string.Empty;
        }
        #endregion
    }
}
