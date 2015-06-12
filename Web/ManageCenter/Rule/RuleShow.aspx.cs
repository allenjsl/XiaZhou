using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.GovStructure;

namespace Web.ManageCenter.Rule
{
    /// <summary>
    /// 行政中心-规章制度-展示
    /// </summary>
    /// 创建人：徐从栎
    /// 创建时间：2011-9-16
    public partial class RuleShow : BackPage
    {
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
            EyouSoft.BLL.GovStructure.BRegulation BLL = new EyouSoft.BLL.GovStructure.BRegulation();
            EyouSoft.Model.GovStructure.MGovRegulation Model = BLL.GetGovRegulationModel(id);
            if (null != Model)
            {
                this.lbNum.Text = Model.Code;
                this.lbTitle.Text = Model.Title;
                this.lbContent.Text = Model.Content;
                this.lbApplydept.Text = GetSectionID(Model.ApplyDeptList);
                this.lbIssuedName.Text = Model.IssuedName;
                this.lbIssurdTime.Text = Model.IssueTime.ToString("yyyy-MM-dd");
                this.lbDept.Text = Model.IssuedDepartName;
                IList<EyouSoft.Model.ComStructure.MComAttach> lstFiles = Model.ComAttachList;
                if (null != lstFiles && lstFiles.Count > 0)
                {
                    for (int i = 0; i < lstFiles.Count; i++)
                    {
                        this.lbFile.Text = string.Format("<span><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a></span>", lstFiles[i].FilePath, lstFiles[i].Name);
                    }
                }
                else
                {
                    this.lbFile.Text = "无";
                }
            }
        }

        /// <summary>
        /// 获取适用部门名称
        /// </summary>
        /// <param name="ApplyDeptList"></param>
        /// <returns></returns>
        protected string GetSectionID(IList<MGovRegApplyDept> ApplyDeptList)
        {
            string str = string.Empty;
            if (ApplyDeptList != null && ApplyDeptList.Count > 0)
            {
                foreach (var item in ApplyDeptList)
                {
                    str += item.DepartName + ",";
                }
                return str.Length > 0 ? str.Substring(0, str.Length - 1) : "";
            }
            return string.Empty;
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_规章制度_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_规章制度_栏目, false);
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