using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.SystemSet
{
    /// <summary>
    /// 团队项目列表
    /// </summary>
    /// 修改记录：
    /// 1、2011-10-9 曹胡生 创建
    public partial class TourItemList : BackPage
    {
        public string title = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PowerControl();
                string state = Utils.GetQueryStringValue("state");
                if (state == "del")
                {
                    DelItem();
                }
                PageInit();
            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            ReturnTitle();
            EyouSoft.Model.EnumType.ComStructure.ProjectType ProjectType = GetProjectType();
            EyouSoft.Model.EnumType.ComStructure.ContainProjectType ContainProjectType = GetContainProjectType();
            IList<EyouSoft.Model.ComStructure.MComProject> list = null;
            if (string.IsNullOrEmpty(ItemType))
            {
                list = new EyouSoft.BLL.ComStructure.BComProject().GetList(ProjectType, null, SiteUserInfo.CompanyId);
            }
            else
            {
                list = new EyouSoft.BLL.ComStructure.BComProject().GetList(ProjectType, ContainProjectType, SiteUserInfo.CompanyId);
            }
            this.repList.DataSource = list;
            this.repList.DataBind();
        }

        /// <summary>
        /// 项目删除
        /// </summary>
        protected void DelItem()
        {
            bool result = new EyouSoft.BLL.ComStructure.BComProject().Delete(Id, SiteUserInfo.CompanyId);
            if (result)
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, string.Format("tableToolbar._showMsg('删除成功');window.location.href='TourItemList.aspx?sl={0}&type={1}&itemtype={2}&memuid={3}';", SL, Type, ItemType, memuid));
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, string.Format("tableToolbar._showMsg('删除失败');window.location.href='TourItemList.aspx?sl={0}&type={1}&itemtype={2}&memuid={3}';", SL, Type, ItemType, memuid));
            }
        }

        /// <summary>
        /// 主键编号
        /// </summary>
        public int Id { get { return Utils.GetInt(Utils.GetQueryStringValue("id")); } }

        /// <summary>
        /// 大类型代号
        /// </summary>
        public string Type { get { return Utils.GetQueryStringValue("type"); } }

        /// <summary>
        /// 小类型代号
        /// </summary>
        public string ItemType { get { return Utils.GetQueryStringValue("itemtype"); } }

        /// <summary>
        /// 团队项目菜单编号
        /// </summary>
        public string memuid { get { return Utils.GetQueryStringValue("memuid"); } }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            EyouSoft.Model.ComStructure.MComProject model = new EyouSoft.Model.ComStructure.MComProject();
            model.CompanyId = SiteUserInfo.CompanyId;
            model.Content = txtContent.Text;
            model.IssueTime = DateTime.Now;
            model.ItemType = GetContainProjectType();
            model.Type = GetProjectType();
            model.OperatorId = SiteUserInfo.UserId;
            if (new EyouSoft.BLL.ComStructure.BComProject().Add(model))
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, string.Format("tableToolbar._showMsg('添加成功');window.location.href='TourItemList.aspx?sl={0}&type={1}&itemtype={2}&memuid={3}';", SL, Type, ItemType, memuid));
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, string.Format("tableToolbar._showMsg('添加失败');"));
            }
        }

        /// <summary>
        /// 获得小类型枚举
        /// </summary>
        /// <returns></returns>
        public EyouSoft.Model.EnumType.ComStructure.ContainProjectType GetContainProjectType()
        {
            EyouSoft.Model.EnumType.ComStructure.ContainProjectType ContainProjectType = EyouSoft.Model.EnumType.ComStructure.ContainProjectType.大交通;
            switch (ItemType)
            {
                case "1":
                    ContainProjectType = EyouSoft.Model.EnumType.ComStructure.ContainProjectType.大交通;
                    break;
                case "2":
                    ContainProjectType = EyouSoft.Model.EnumType.ComStructure.ContainProjectType.用车;
                    break;
                case "3":
                    ContainProjectType = EyouSoft.Model.EnumType.ComStructure.ContainProjectType.房;
                    break;
                case "4":
                    ContainProjectType = EyouSoft.Model.EnumType.ComStructure.ContainProjectType.餐;
                    break;
                case "5":
                    ContainProjectType = EyouSoft.Model.EnumType.ComStructure.ContainProjectType.导服;
                    break;
                case "6":
                    ContainProjectType = EyouSoft.Model.EnumType.ComStructure.ContainProjectType.景点;
                    break;
                case "7":
                    ContainProjectType = EyouSoft.Model.EnumType.ComStructure.ContainProjectType.保险;
                    break;
                case "8":
                    ContainProjectType = EyouSoft.Model.EnumType.ComStructure.ContainProjectType.小交通;
                    break;
                case "9":
                    ContainProjectType = EyouSoft.Model.EnumType.ComStructure.ContainProjectType.综费;
                    break;
                case "10":
                    ContainProjectType = EyouSoft.Model.EnumType.ComStructure.ContainProjectType.其它;
                    break;
                case "11":
                    ContainProjectType = EyouSoft.Model.EnumType.ComStructure.ContainProjectType.国内段;
                    break;
                case "12":
                    ContainProjectType = EyouSoft.Model.EnumType.ComStructure.ContainProjectType.签证;
                    break;
                case "13":
                    ContainProjectType = EyouSoft.Model.EnumType.ComStructure.ContainProjectType.游船;
                    break;

            }
            return ContainProjectType;
        }

        /// <summary>
        /// 获得大类型枚举
        /// </summary>
        /// <returns></returns>
        public EyouSoft.Model.EnumType.ComStructure.ProjectType GetProjectType()
        {
            EyouSoft.Model.EnumType.ComStructure.ProjectType ProjectType = EyouSoft.Model.EnumType.ComStructure.ProjectType.包含项目;
            switch (Type)
            {
                case "1":
                    ProjectType = EyouSoft.Model.EnumType.ComStructure.ProjectType.包含项目;
                    break;
                case "2":
                    ProjectType = EyouSoft.Model.EnumType.ComStructure.ProjectType.不含项目;
                    break;
                case "3":
                    ProjectType = EyouSoft.Model.EnumType.ComStructure.ProjectType.购物安排;
                    break;
                case "4":
                    ProjectType = EyouSoft.Model.EnumType.ComStructure.ProjectType.儿童安排;
                    break;
                case "5":
                    ProjectType = EyouSoft.Model.EnumType.ComStructure.ProjectType.自费项目;
                    break;
                case "6":
                    ProjectType = EyouSoft.Model.EnumType.ComStructure.ProjectType.注意事项;
                    break;
                case "7":
                    ProjectType = EyouSoft.Model.EnumType.ComStructure.ProjectType.温馨提醒;
                    break;
                case "8":
                    ProjectType = EyouSoft.Model.EnumType.ComStructure.ProjectType.服务标准;
                    break;
            }
            return ProjectType;
        }

        public void ReturnTitle()
        {
            string str = "";
            if (ItemType == "")
            {
                switch (Type)
                {
                    case "1":
                        str = "包含项目";
                        break;
                    case "2":
                        str = "不含项目";
                        break;
                    case "3":
                        str = "购物安排";
                        break;
                    case "4":
                        str = "儿童安排";
                        break;
                    case "5":
                        str = "自费项目";
                        break;
                    case "6":
                        str = "注意事项";
                        break;
                    case "7":
                        str = "温馨提醒";
                        break;
                    case "8":
                        str = "服务标准";
                        break;
                }
            }
            else
            {
                switch (ItemType)
                {
                    case "1":
                        str = "大交通";
                        break;
                    case "2":
                        str = "用车";
                        break;
                    case "3":
                        str = "住宿";
                        break;
                    case "4":
                        str = "用餐";
                        break;
                    case "5":
                        str = "导服";
                        break;
                    case "6":
                        str = "景点";
                        break;
                    case "7":
                        str = "保险";
                        break;
                    case "8":
                        str = "小交通";
                        break;
                    case "9":
                        str = "综费";
                        break;
                    case "10":
                        str = "其它";
                        break;
                    case "11":
                        str = "国内段";
                        break;
                    case "12":
                        str = "签证";
                        break;
                }
            }
            title = str;
        }

        /// <summary>
        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_模板管理栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_模板管理栏目, false);
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
            this.PageType = PageType.general;
        }
    }
}
