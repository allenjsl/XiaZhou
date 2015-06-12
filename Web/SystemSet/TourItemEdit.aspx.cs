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
    /// 团队项目编辑
    /// </summary>
    /// 修改记录：
    /// 1、2011-10-9 曹胡生 创建
    public partial class TourItemEdit :BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PowerControl();
                PageInit();
            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            EyouSoft.Model.ComStructure.MComProject model = new EyouSoft.BLL.ComStructure.BComProject().GetModel(Id,SiteUserInfo.CompanyId);
            if (model != null)
            {
                txtContent.Text = model.Content;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtContent.Text == "")
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "parent.tableToolbar._showMsg('内容不能为空');");
                return;
            }
            EyouSoft.Model.ComStructure.MComProject model = new EyouSoft.Model.ComStructure.MComProject();
            model.CompanyId = SiteUserInfo.CompanyId;
            model.Id = Id;
            model.Content = txtContent.Text;
            model.ItemType = GetContainProjectType();
            model.Type = GetProjectType();
            model.OperatorId = SiteUserInfo.UserId;
            if (new EyouSoft.BLL.ComStructure.BComProject().Update(model))
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, string.Format("parent.tableToolbar._showMsg('修改成功');parent.Boxy.getIframeDialog('{0}').hide();window.parent.location.href='TourItemList.aspx?sl={1}&type={2}&itemtype={3}&memuid={4}';", Utils.GetQueryStringValue("iframeId"), SL, Type, ItemType,memuid));
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, string.Format("parent.tableToolbar._showMsg('修改失败')"));
            }
        }

        /// <summary>
        /// 获得小类型枚举
        /// </summary>
        /// <returns></returns>
        private EyouSoft.Model.EnumType.ComStructure.ContainProjectType GetContainProjectType()
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

            }
            return ContainProjectType;
        }

        /// <summary>
        /// 获得大类型枚举
        /// </summary>
        /// <returns></returns>
        private EyouSoft.Model.EnumType.ComStructure.ProjectType GetProjectType()
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
            this.PageType = PageType.boxyPage;
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
    }
}
