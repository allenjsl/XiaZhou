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

namespace Web.ManageCenter.Meet
{
    /// <summary>
    /// 行政中心-会议管理-列表
    /// </summary>
    /// 修改人：方琪
    /// 创建时间：2012-03-26
    public partial class MeetList : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 页大小
        /// </summary>
        private int pageSize = 10;
        /// <summary>
        /// 页码
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        private int recordCount = 0;
        #endregion

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Request.QueryString["doType"];
            //存在ajax请求
            if (doType == "delete")
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
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            string txtNum = Utils.GetQueryStringValue("txtNum");//会议编号
            string txtTitle = Utils.GetQueryStringValue("txtTitle");//会议主题
            DateTime? txtStartTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtStartTime"));//会议时间（始）
            DateTime? txtEndTime = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEndTime"));//会议时间（终）
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            EyouSoft.BLL.GovStructure.BMeeting BLL = new EyouSoft.BLL.GovStructure.BMeeting();
            EyouSoft.Model.GovStructure.MSearchMeeting Model = new EyouSoft.Model.GovStructure.MSearchMeeting();
            Model.EndTime = txtEndTime;
            Model.Number = txtNum;
            Model.StartTime = txtStartTime;
            Model.Theme = txtTitle;
            IList<EyouSoft.Model.GovStructure.MGovMeeting> lst = BLL.GetGovMeetingList(this.SiteUserInfo.CompanyId, Model, this.pageSize, this.pageIndex, ref this.recordCount);
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

        #region ajax操作
        /// <summary>
        /// ajax操作
        /// </summary>
        private void AJAX(string doType)
        {
            string msg = string.Empty;
            bool result = false;
            switch (doType)
            {
                case "delete":
                    //判断权限
                    //if(this.CheckGrant(Common.Enum.TravelPermission.行政中心_会议记录_删除))
                    {
                        string id = Utils.GetQueryStringValue("id");
                        result = DeleteData(id);
                        msg = result ? "删除成功！" : "删除失败!";
                    }
                    break;
                default:
                    break;
            }
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
            Response.End();
        }
        #endregion

        #region 删除操作
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id">删除ID</param>
        /// <returns></returns>
        private bool DeleteData(string id)
        {
            bool b = false;
            if (!String.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.GovStructure.BMeeting BLL = new EyouSoft.BLL.GovStructure.BMeeting();
                b = BLL.DeleteGovMeeting(id.Split(','));
            }
            return b;
        }
        protected string getUserInfo(object o)
        {
            List<string> lstStr = new List<string>();
            if (null != o)
            {
                IList<EyouSoft.Model.GovStructure.MGovMeetingStaff> lst = (IList<EyouSoft.Model.GovStructure.MGovMeetingStaff>)o;
                if (null != lst && lst.Count > 0)
                {
                    foreach (MGovMeetingStaff m in lst)
                    {
                        lstStr.Add(m.AcceptType);
                    }
                }
            }
            return String.Join(",", lstStr.ToArray());
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_会议管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_会议管理_栏目, false);
            }
            else
            {
                ph_Add.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_会议管理_新增);
                ph_Update.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_会议管理_修改);
                ph_Del.Visible = this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_会议管理_删除);
            }

        }
        #endregion


    }
}