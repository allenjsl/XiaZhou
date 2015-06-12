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
using EyouSoft.Model.EnumType.IndStructure;

namespace Web.UserCenter.WorkExchange
{

    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 修改人：蔡永辉
    /// 创建时间：2012-4-1
    /// 说明：个人中心：工作计划 新增，修改
    public partial class WorkPlanEdit : BackPage
    {
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
                //获得操作ID
                string id = Utils.GetQueryStringValue("id");
                //部门选用设置父级iframeid
                SelectSection1.ParentIframeID = Utils.GetQueryStringValue("iframeId");
                //审核人选用设置父级iframeid
                SelectSection2.ParentIframeID = Utils.GetQueryStringValue("iframeId");
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
                MWorkPlan modelMWorkPlan = new MWorkPlan();
                //实例化业务层
                BIndividual bllBIndividual = new BIndividual();
                modelMWorkPlan = bllBIndividual.GetWorkPlan(id);
                if (modelMWorkPlan != null)
                {
                    //计划标题
                    this.txtTitle.Text = modelMWorkPlan.Title;
                    //计划人
                    this.txtUserName.Text = modelMWorkPlan.OperatorName;
                    //计划部门id
                    this.SelectSection1.SectionID = modelMWorkPlan.DeptId.ToString();
                    //计划部门名称
                    this.SelectSection1.SectionName = modelMWorkPlan.Department;
                    //计划时间
                    this.txtDateTime.Text = modelMWorkPlan.ScheduledTime.ToShortDateString();
                    //计划内容
                    this.txtContent.Text = modelMWorkPlan.Remark;
                    //附件
                    if (!string.IsNullOrEmpty(modelMWorkPlan.UploadUrl))
                    {
                        string[] strlist = modelMWorkPlan.UploadUrl.Split('|');
                        if (strlist.Length == 2)
                            this.lclFile.Text = "<span class='upload_filename'>&nbsp;<a href='" + strlist[1] + "' title='" + strlist[0] + "' target='_blank'>查看附件</a><a href='javascript:void(0);' onclick='WorkPlanEditPage.RemoveVisaFile(this);return false;'> <img style='vertical-align:middle' src='/images/cha.gif'></a><input type='hidden' name='hideOtherFile' value='" + modelMWorkPlan.UploadUrl + "'></span>";
                    }

                    #region 审核人and ID
                    //审核人
                    string approver = "";
                    //审核人id
                    string approverid = "";
                    if (modelMWorkPlan.list != null && modelMWorkPlan.list.Count > 0)
                    {
                        foreach (MWorkPlanCheck item in modelMWorkPlan.list)
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
                        if (modelMWorkPlan.list != null && modelMWorkPlan.list.Count > 0)
                        {
                            foreach (MWorkPlanCheck model in modelMWorkPlan.list)
                            {
                                if (model != null)
                                {
                                    if (model.Status == Status.已结束 || model.Status == Status.已审批)
                                        IsShenpi = true;
                                }
                            }
                            if (IsShenpi || modelMWorkPlan.OperatorId != null && modelMWorkPlan.OperatorId != SiteUserInfo.UserId)
                            {
                                this.phdSave.Visible = false;
                            }
                        }
                    }
                }
                else
                    txtDateTime.Text = DateTime.Now.ToShortDateString();
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
            //实例化工作计划实体
            MWorkPlan modelMWorkPlan = new MWorkPlan();
            //实例化业务层
            BIndividual bllBIndividual = new BIndividual();
            if (!string.IsNullOrEmpty(Utils.GetFormValue(hidId.UniqueID)))
            {
                //修改状态根据id获取计划实体
                modelMWorkPlan = bllBIndividual.GetWorkPlan(Utils.GetFormValue(hidId.UniqueID));
            }
            else
            {
                //计划时间
                modelMWorkPlan.IssueTime = DateTime.Now;
                //最后修改时间
                modelMWorkPlan.UpdateTime = DateTime.Now;
            }
            #region 显性属性（页面直接和用户交互）
            //计划标题
            string title = Utils.GetFormValue(txtTitle.UniqueID);
            //计划人
            string userName = Utils.GetFormValue(txtUserName.UniqueID);
            //部门id
            string departID = Utils.GetFormValue(this.SelectSection1.SelectIDClient);
            //部门名称
            string departName = Utils.GetFormValue(this.SelectSection1.SelectNameClient);
            //计划预计完成时间
            string dateTime = Utils.GetFormValue(txtDateTime.UniqueID);
            //计划内容
            string content = Utils.GetFormValue(txtContent.UniqueID);
            //附件上传
            string strUploadUrl = Utils.GetFormValue(UploadControl1.ClientHideID);

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
            List<MWorkPlanCheck> list = new List<MWorkPlanCheck>();
            if (approver.Length > 0)
            {
                for (int i = 0; i < approver.Length; i++)
                {
                    MWorkPlanCheck modelMWorkPlanCheck = new MWorkPlanCheck();
                    if (approverId[i] != null)
                        //审批人id
                        modelMWorkPlanCheck.ApproverId = approverId[i];
                    //审批人姓名
                    modelMWorkPlanCheck.Approver = approver[i];
                    list.Add(modelMWorkPlanCheck);
                }
            }
            #endregion

            #region 实体赋值
            modelMWorkPlan.Title = title;
            modelMWorkPlan.OperatorName = userName;
            modelMWorkPlan.OperatorId = "";
            modelMWorkPlan.DeptId = Utils.GetInt(departID);
            modelMWorkPlan.Department = departName;
            modelMWorkPlan.ScheduledTime = Utils.GetDateTime(dateTime);
            modelMWorkPlan.Remark = content;

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

            modelMWorkPlan.UploadUrl = strUploadUrl;
            modelMWorkPlan.list = list;
            #endregion
            #endregion
            #endregion

            #region 隐性属性（id之类）
            //系统公司id
            string companyid = SiteUserInfo.CompanyId;
            modelMWorkPlan.CompanyId = companyid;
            //计划人编号
            modelMWorkPlan.OperatorId = SiteUserInfo.UserId;
            #endregion
            //操作之后返回结果
            bool result = false;
            if (!string.IsNullOrEmpty(hidId.Value))
            {
                result = bllBIndividual.UpdWorkPlan(modelMWorkPlan);
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
                result = bllBIndividual.AddWorkPlan(modelMWorkPlan);
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
        /// 权限判断
        /// </summary>
        protected void PowerControl()
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
