using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using EyouSoft.BLL.GovStructure;
using EyouSoft.Model.GovStructure;
using System.Text;
using EyouSoft.Model.EnumType.GovStructure;

namespace Web.ManageCenter.Meet
{
    /// <summary>
    /// 行政中心-会议管理-添加
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-03-26
    public partial class meetAdd : BackPage
    {
        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            //PowerControl();
            string save = Utils.GetQueryStringValue("save");
            string doType = Request.QueryString["doType"];
            #region ajax请求
            if (save == "save")
            {
                PageSave(doType);
            }
            #endregion
            if (!IsPostBack)
            {
                string id = Utils.GetQueryStringValue("id");
                PageInit(id);
                if (String.Equals(doType, "checkNum"))
                {
                    this.CheckNum();
                }
            }
        }
        #endregion

        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {
            #region 参会人员选用控件初始化
            this.HrSelect1.ParentIframeID = Utils.GetQueryStringValue("iframeId");
            this.HrSelect1.SetTitle = "参会人员";
            this.HrSelect1.SModel = "2";
            IList<EnumObj> list = EnumObj.GetList(typeof(Category));
            this.selType.DataSource = list;
            this.selType.DataTextField = "Text";
            this.selType.DataValueField = "Value";

            this.selType.AppendDataBoundItems = true;
            this.selType.DataBind();
            #endregion

            //编辑初始化
            if (!string.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.GovStructure.BMeeting BLL = new EyouSoft.BLL.GovStructure.BMeeting();
                EyouSoft.Model.GovStructure.MGovMeeting Model = BLL.GetGovMeetingModel(id);
                if (null != Model)
                {
                    this.hidId.Value = Model.MeetingId;
                    //会议编号
                    this.txtNum.Text = Model.Number;
                    //会议主题
                    this.txtTitle.Text = Model.Theme;
                    //会议类型
                    this.selType.SelectedValue = ((int)Model.Category).ToString();
                    IList<EyouSoft.Model.GovStructure.MGovMeetingStaff> lst = Model.MGovMeetingStaff;
                    StringBuilder strNames = new StringBuilder();
                    StringBuilder strIDs = new StringBuilder();
                    if (null != lst && lst.Count > 0)
                    {
                        foreach (MGovMeetingStaff m in lst)
                        {
                            strNames.Append(m.AcceptType + ",");
                            strIDs.Append(m.AcceptTypeID + ",");
                        }
                        //参会人员名
                        this.HrSelect1.HrSelectName = Model.MeetingStaff; //strNames.Length > 0 ? strNames.ToString().Substring(0, strNames.Length - 1) : "";
                        //参会人员ID
                        this.HrSelect1.HrSelectID = strIDs.Length > 0 ? strIDs.ToString().Substring(0, strIDs.Length - 1) : "";
                    }
                    //会议时间
                    this.HrSelect1.HrSelectName = Model.MeetingStaff;
                    this.txtStartTime.Text = string.Format("{0:yyyy-MM-dd HH:mm}", Model.StartTime);
                    this.txtEndTime.Text = string.Format("{0:yyyy-MM-dd HH:mm}", Model.EndTime);
                    //会议地点
                    this.txtPlace.Text = Model.Venue;
                    //会议纪要 
                    this.txtContent.Text = Model.Minutes;
                    if (!this.SiteUserInfo.IsHandleElse && this.SiteUserInfo.UserId != Model.OperatorId)
                    {
                        this.ph_Save.Visible = false;
                    }
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        protected void PageSave(string doType)
        {
            #region 表单取值
            //会议编号
            string num = Utils.GetFormValue(txtNum.UniqueID);
            //会议类型
            string type = Utils.GetFormValue(selType.UniqueID);
            //会议主题
            string title = Utils.GetFormValue(txtTitle.UniqueID);
            //参会人员
            string people = Utils.GetFormValue(HrSelect1.HrSelectNameClient);
            //参会人员编号
            string peopleid = Utils.GetFormValue(HrSelect1.HrSelectIDClient);
            //开始时间
            string starttime = Utils.GetFormValue(txtStartTime.UniqueID);
            //结束时间
            string endtime = Utils.GetFormValue(txtEndTime.UniqueID);
            //会议地点
            string place = Utils.GetFormValue(txtPlace.UniqueID);
            //会议纪要
            string content = Utils.GetFormValue(txtContent.UniqueID);
            //主键编号
            string hidid = Utils.GetFormValue(hidId.UniqueID);
            #endregion

            #region 表单验证
            string msg = "";
            bool result = false;
            Response.Clear();
            if (string.IsNullOrEmpty(num))
            {
                msg += "-请输入会议编号！<br/>";
            }
            if (string.IsNullOrEmpty(title))
            {
                msg += "-请输入会议主题！<br/>";
            }
            if (string.IsNullOrEmpty(type))
            {
                msg += "-请选择会议类型！<br/>";
            }
            if (string.IsNullOrEmpty(place))
            {
                msg += "-请输入会议地点！<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                result = false;
                Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
                Response.End();
                return;
            }
            #endregion

            #region 实体赋值
            MGovMeeting Model = new MGovMeeting();
            Model.Category = (Category)Utils.GetInt(type);
            Model.CompanyID = this.SiteUserInfo.CompanyId;
            Model.EndTime = Utils.GetDateTime(endtime);
            Model.IssueTime = DateTime.Now;
            Model.MeetingStaff = people;
            Model.Minutes = content;
            Model.Number = num;
            Model.OperatorId = this.SiteUserInfo.UserId;
            Model.StartTime = Utils.GetDateTime(starttime);
            Model.Theme = title;
            Model.Venue = place;
            Model.MGovMeetingStaff = GetMeetingStaff(people, peopleid, hidid);
            Model.MeetingId = hidid;
            #endregion

            #region 提交回应
            BMeeting BLL = new BMeeting();
            if (doType == "add")
            {
                result = BLL.AddGovMeeting(Model);
                msg = result ? "添加成功！" : "添加失败！";
            }
            if (doType == "update")
            {
                result = BLL.UpdateGovMeeting(Model);
                msg = result ? "修改成功！" : "修改失败！";
            }
            Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
            Response.End();
            #endregion
        }
        #endregion

        #region 获取集合列表
        /// <summary>
        /// 获取集合列表
        /// </summary>
        /// <param name="name">参会人员名单</param>
        /// <param name="id">参会人员编号</param>
        /// <param name="hidid">会议编号</param>
        /// <returns></returns>
        protected IList<MGovMeetingStaff> GetMeetingStaff(string name, string id, string hidid)
        {

            IList<MGovMeetingStaff> list = new List<MGovMeetingStaff>();
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(id))
            {
                string[] ids = id.Split(',');
                string[] names = name.Split(',');
                for (int i = 0; i < ids.Length; i++)
                {
                    MGovMeetingStaff model = new MGovMeetingStaff();
                    model.AcceptType = names[i];
                    model.AcceptTypeID = ids[i];
                    model.MeetingId = hidid;
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_会议管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_会议管理_栏目, false);
            }
            else
            {
                string doType = Utils.GetQueryStringValue("doType");
                if (doType == "add")
                {
                    if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_会议管理_新增))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_会议管理_新增, false);
                    }
                }
                else
                {
                    if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_会议管理_修改))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_会议管理_修改, false);
                    }
                }
            }
        }
        #endregion

        #region 检查会议编号是否重复
        /// <summary>
        /// 检查会议编号是否重复 
        /// </summary>
        protected void CheckNum()
        {
            String str = String.Empty;
            String id = Request.QueryString["id"].Trim();
            String num = Request.QueryString["num"].Trim();
            if (!String.IsNullOrEmpty(num))
            {
                BMeeting BLL = new BMeeting();
                if (String.IsNullOrEmpty(id))
                {//新增
                    if (BLL.ExistsNumber(num, "", this.SiteUserInfo.CompanyId))
                    {
                        str = "1";
                    }
                }
                else
                { //编辑
                    MGovMeeting Model = BLL.GetGovMeetingModel(id);
                    if (null != Model && !String.Equals(num, Model.Number))
                    {
                        if (BLL.ExistsNumber(num, "", this.SiteUserInfo.CompanyId))
                        {
                            str = "1";
                        }
                    }
                }
            }
            Response.Clear();
            Response.Write(str);
            Response.End();
        }
        #endregion

        #region 重写OnPreInit
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
        #endregion

    }
}