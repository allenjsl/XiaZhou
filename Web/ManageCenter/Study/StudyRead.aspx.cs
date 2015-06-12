#region 命名空间
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
#endregion

namespace EyouSoft.Web.ManageCenter.Study
{
    /// <summary>
    /// 交流学习查看
    /// </summary>
    public partial class StudyRead : BackPage
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
            //权限验证
            PowerControl();
            if (!IsPostBack)
            {
                //获得操作ID
                string id = Utils.GetQueryStringValue("id");
                //根据ID初始化页面
                PageInit(id);
            }
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {
            EyouSoft.BLL.GovStructure.BNotice BLL = new EyouSoft.BLL.GovStructure.BNotice();
            EyouSoft.Model.GovStructure.MGovNotice Model = BLL.GetGovNoticeModel(id);
            if (null != Model)
            {
                //浏览人数加1
                int views = 0;
                Int32.TryParse(Model.Views.ToString(), out views);
                Model.Views = views + 1;
                BLL.UpdateGovNotice(Model);
                //插入浏览人相关信息
                MGovNoticeBrowse viewModel = new MGovNoticeBrowse();
                viewModel.IssueTime = DateTime.Now;
                viewModel.NoticeId = id;
                viewModel.OperatorId = this.SiteUserInfo.UserId;
                viewModel.Operator = this.SiteUserInfo.Name;
                BLL.AddGovNoticeBrowse(viewModel);

                //通知公告标题标题
                this.lbTitle.Text = Model.Title;
                StringBuilder strSec = new StringBuilder();
                if (null != Model.MGovNoticeReceiverList && Model.MGovNoticeReceiverList.Count > 0)
                {
                    EyouSoft.BLL.ComStructure.BComDepartment secBLL = new EyouSoft.BLL.ComStructure.BComDepartment();
                    EyouSoft.Model.ComStructure.MComDepartment secModel;
                    foreach (MGovNoticeReceiver m in Model.MGovNoticeReceiverList)
                    {
                        if (m.ItemType == EyouSoft.Model.EnumType.GovStructure.ItemType.指定部门)
                        {
                            if (!string.IsNullOrEmpty(m.ItemId))
                            {
                                int[] intArry = Utils.GetIntArray(m.ItemId, ",");
                                for (int j = 0; j < intArry.Length; j++)
                                {

                                    secModel = secBLL.GetModel(intArry[j], this.SiteUserInfo.CompanyId);
                                    if (null != secModel)
                                    {
                                        strSec.Append(secModel.DepartName + ",");
                                    }

                                }
                            }
                        }
                        else
                        {
                            strSec.Append(m.ItemType.ToString() + ",");
                        }
                    }
                }
                //发布对象
                this.lbObj.Text = strSec.Length > 0 ? strSec.ToString().Substring(0, strSec.Length - 1) : "";
                //内容
                this.lbContent.Text = Model.Content;
                IList<EyouSoft.Model.ComStructure.MComAttach> lstFiles = Model.ComAttachList;
                StringBuilder strFile = new StringBuilder();
                if (null != lstFiles && lstFiles.Count > 0)
                {
                    for (int i = 0; i < lstFiles.Count; i++)
                    {
                        strFile.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a></span>", lstFiles[i].FilePath, lstFiles[i].Name);
                    }
                    this.lbFile.Text = strFile.ToString();
                }
                else
                {
                    this.lbFile.Text = "无";
                }
                this.lbTime.Text = string.Format("{0:yyyy-MM-dd HH:mm}", Model.IssueTime);
                this.lbSender.Text = Model.Operator;
                InitReplyList(id);
            }
        }
        /// <summary>
        /// 加载回复列表
        /// </summary>
        /// <param name="NoticeId">交流信息编号</param>
        private void InitReplyList(string NoticeId)
        {
            EyouSoft.BLL.GovStructure.BNotice BLL = new EyouSoft.BLL.GovStructure.BNotice();
            IList<EyouSoft.Model.GovStructure.MGovNoticeReply> lst = BLL.GetGovNoticeReplyList(NoticeId, this.pageSize, this.pageIndex, ref this.recordCount);
            if (null != lst && lst.Count > 0)
            {
                this.rptReply.DataSource = lst;
                this.rptReply.DataBind();
                if (recordCount <= pageSize)
                {
                    this.ExporPageInfoSelect1.Visible = false;

                }
                else
                {
                    BindPage();
                }
            }
            else { this.ExporPageInfoSelect1.Visible = false; }
        }
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
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_通知公告_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_通知公告_栏目, false);
            }
        }
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
        /// <summary>
        /// 保存回复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string id = Utils.GetQueryStringValue("id");
            string txtReply = Utils.GetFormValue("txtReply");
            if (!String.IsNullOrEmpty(id) && !String.IsNullOrEmpty(txtReply))
            {
                EyouSoft.Model.GovStructure.MGovNoticeReply model = new MGovNoticeReply();
                model.NoticeId = id;
                model.OperatorId = SiteUserInfo.UserId;
                model.OperatorName = SiteUserInfo.Name;
                model.ReplyInfo = txtReply;
                model.IssueTime = DateTime.Now;
                EyouSoft.BLL.GovStructure.BNotice BLL = new EyouSoft.BLL.GovStructure.BNotice();
                BLL.AddNoticeReply(model);
                EyouSoft.Common.Function.MessageBox.ShowAndRedirect(this, "回复成功", Request.ServerVariables["SCRIPT_NAME"].ToString() + "?" + Request.QueryString);
            }
            else { EyouSoft.Common.Function.MessageBox.ShowAndReturnBack(this, "回复内容不能为空！",1); }
            return;
        }
    }
}
