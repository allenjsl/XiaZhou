using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.IndStructure;
using EyouSoft.BLL.IndStructure;
using EyouSoft.Model.EnumType.IndStructure;

namespace Web.UserCenter.WorkExchange
{
    public partial class WorkReportEdit : BackPage
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 修改人：蔡永辉
        /// 创建时间：2012-4-1
        /// 说明：个人中心：工作汇报 添加，修改
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();

            #region ajax操作

            string Isajax = Utils.GetQueryStringValue("doType");
            if (!string.IsNullOrEmpty(Isajax))
                Ajax(Isajax);

            #endregion
            if (!IsPostBack)
            {
                //部门选用设置父级iframeid
                SelectSection1.ParentIframeID = Utils.GetQueryStringValue("iframeId");
                //审核人选用设置父级iframeid
                SelectSection2.ParentIframeID = Utils.GetQueryStringValue("iframeId");
                //获得操作ID
                string id = Utils.GetQueryStringValue("id");
                //为上传控件赋值公司id
                this.UploadControl1.CompanyID = SiteUserInfo.CompanyId;
                //为上传控件赋值多文件
                this.UploadControl1.IsUploadMore = false;
                //为上传控件赋值自动上传
                this.UploadControl1.IsUploadSelf = true;
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
            if (!string.IsNullOrEmpty(id))
            {
                //把操作id方法在隐藏域之中供修改的时候用
                hidId.Value = id;
                //实例化工作汇报实体
                MWorkReport modelMWorkReport = new MWorkReport();
                //实例化业务层
                BIndividual bllBIndividual = new BIndividual();
                modelMWorkReport = bllBIndividual.GetWorkReport(Utils.GetInt(id));
                if (modelMWorkReport != null)
                {
                    //汇报标题
                    this.txtTitle.Text = modelMWorkReport.Title;
                    //汇报人
                    this.txtUserName.Text = modelMWorkReport.OperatorName;
                    //部门id
                    this.SelectSection1.SectionID = modelMWorkReport.DepartmentId.ToString();
                    //部门名称
                    this.SelectSection1.SectionName = modelMWorkReport.Department;
                    //汇报时间
                    this.txtDateTime.Text = modelMWorkReport.IssueTime.ToShortDateString();
                    //汇报内容
                    this.txtContent.Text = modelMWorkReport.Content;
                    //附件
                    if (!string.IsNullOrEmpty(modelMWorkReport.UploadUrl))
                    {
                        string[] strlist = modelMWorkReport.UploadUrl.Split('|');
                        if (strlist.Length == 2)
                            this.lclFile.Text = "<span class='upload_filename'>&nbsp;<a href='" + strlist[1] + "' title='" + strlist[0] + "' target='_blank'>查看附件</a><a href='javascript:void(0);' onclick='WorkReportEditPage.RemoveVisaFile(this);return false;'> <img style='vertical-align:middle' src='/images/cha.gif'></a><input type='hidden' name='hideOtherFile' value='" + modelMWorkReport.UploadUrl + "'></span>";
                    }

                    #region 审核人and ID
                    //审核人
                    string approver = "";
                    //审核人id
                    string approverid = "";
                    if (modelMWorkReport.list != null && modelMWorkReport.list.Count > 0)
                    {
                        foreach (MWorkReportCheck item in modelMWorkReport.list)
                        {
                            if (item != null)
                            {
                                //审核人
                                approver += item.Approver.Trim() + ",";
                                //审核人ID
                                approverid += item.ApproverId.Trim() + ",";
                            }
                        }
                    }
                    this.SelectSection2.SellsName = approver.TrimEnd(',');
                    this.SelectSection2.SellsID = approverid.TrimEnd(',');
                    #endregion

                    //是否可以操作该数据
                    if (!SiteUserInfo.IsHandleElse)
                    {
                        bool IsShenpi = false;
                        if (modelMWorkReport.list != null && modelMWorkReport.list.Count > 0)
                        {
                            foreach (MWorkReportCheck model in modelMWorkReport.list)
                            {
                                if (model != null)
                                {
                                    if (model.Status == Status.已结束 || model.Status == Status.已审批)
                                        IsShenpi = true;
                                }
                            }
                            if (IsShenpi || modelMWorkReport.Status == Status.已审批 || (modelMWorkReport.OperatorId != null && modelMWorkReport.OperatorId != SiteUserInfo.UserId))
                            {
                                this.phdSave.Visible = false;
                            }
                        }
                    }
                }
            }
            else
                txtDateTime.Text = DateTime.Now.ToShortDateString();
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
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

        #region Ajax操作
        private void Ajax(string type)
        {
            switch (type)
            {
                case "AjaxSave"://ajax操作保存
                    PageSave();
                    break;
            }
        }

        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave()
        {
            //ajax返回信息
            string resultmsg = "";
            //实例化工作汇报实体
            MWorkReport modelMWorkReport = new MWorkReport();
            //实例化业务层
            BIndividual bllBIndividual = new BIndividual();
            if (!string.IsNullOrEmpty(hidId.Value))
            {
                //修改状态根据id获取汇报实体
                modelMWorkReport = bllBIndividual.GetWorkReport(Utils.GetInt(hidId.Value));
            }
            else
            {
                //汇报时间
                modelMWorkReport.IssueTime = DateTime.Now;
            }
            #region 显性属性（页面直接和用户交互）
            //标题
            string title = this.txtTitle.Text;
            //汇报人
            string userName = this.txtUserName.Text;
            //部门id
            string departID = Utils.GetFormValue(this.SelectSection1.SelectIDClient);
            //部门名称
            string departName = Utils.GetFormValue(this.SelectSection1.SelectNameClient);
            //汇报时间
            string dateTime = this.txtDateTime.Text;
            //汇报内容
            string content = this.txtContent.Text;
            //附件上传
            string strUploadUrl = Utils.GetFormValue(this.UploadControl1.ClientHideID);

            #region  审批人列表
            //审批人
            string strapprover = Utils.GetFormValue(this.SelectSection2.SellsNameClient);
            string[] approver = { "" };
            if (strapprover != null && strapprover.Contains(','))
                approver = strapprover.Split(',');
            else
                approver[0] = strapprover;
            //审核人id
            string strapproverId = Utils.GetFormValue(this.SelectSection2.SellsIDClient);
            string[] approverId = { "" };
            if (strapproverId != null && strapproverId.Contains(','))
                approverId = strapproverId.Split(',');
            else
                approverId[0] = strapproverId;
            List<MWorkReportCheck> list = new List<MWorkReportCheck>();
            if (approver.Length > 0)
            {
                for (int i = 0; i < approver.Length; i++)
                {
                    MWorkReportCheck modelMWorkReportCheck = new MWorkReportCheck();
                    if (approverId[i] != null)
                        //审批人id
                        modelMWorkReportCheck.ApproverId = approverId[i];
                    //审批人姓名
                    modelMWorkReportCheck.Approver = approver[i];
                    list.Add(modelMWorkReportCheck);
                }
            }
            #endregion

            #region 实体赋值
            modelMWorkReport.Title = title;
            modelMWorkReport.OperatorName = userName;
            modelMWorkReport.OperatorId = "";
            modelMWorkReport.DepartmentId = Utils.GetInt(departID);
            modelMWorkReport.Department = departName;
            modelMWorkReport.IssueTime = Utils.GetDateTime(dateTime);
            modelMWorkReport.Content = content;
            modelMWorkReport.Status = Status.审批中;
            #region 附件
            if (!string.IsNullOrEmpty(strUploadUrl))
            {
                //验证数据
                string[] strUploadList = strUploadUrl.Split('|');
                if (strUploadList.Length == 2)
                    strUploadUrl = strUploadList[0] + "|" + strUploadList[1];
            }
            else
            {
                if (!string.IsNullOrEmpty(Utils.GetFormValue("hideOtherFile")))
                {
                    strUploadUrl = Utils.GetFormValue("hideOtherFile");
                }
            }
            modelMWorkReport.UploadUrl = strUploadUrl;
            #endregion
            modelMWorkReport.list = list;
            #endregion
            #endregion

            #region 隐性属性（id之类）
            //系统公司id
            string companyid = SiteUserInfo.CompanyId;
            modelMWorkReport.CompanyId = companyid;
            //汇报人编号
            modelMWorkReport.OperatorId = SiteUserInfo.UserId;
            #endregion
            //操作之后返回结果
            bool result = false;
            if (!string.IsNullOrEmpty(hidId.Value))
            {
                result = bllBIndividual.UpdWorkReport(modelMWorkReport);
                if (result)
                {
                    resultmsg = UtilsCommons.AjaxReturnJson("true", "修改成功");
                }
                else
                {
                    resultmsg = UtilsCommons.AjaxReturnJson("false", "修改失败,检查选用的数据是否正确");
                }
            }
            else
            {
                result = bllBIndividual.AddWorkReport(modelMWorkReport);
                if (result)
                {
                    resultmsg = UtilsCommons.AjaxReturnJson("true", "添加成功");
                }
                else
                {
                    resultmsg = UtilsCommons.AjaxReturnJson("false", "添加失败,检查选用的数据是否正确");
                }
            }
            Response.Clear();
            Response.Write(resultmsg);
            Response.End();
        }

        #endregion
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }

    }
}
