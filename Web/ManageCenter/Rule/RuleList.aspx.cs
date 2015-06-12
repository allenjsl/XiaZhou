using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.GovStructure;
using EyouSoft.Model.EnumType.ComStructure;
using System.Text;
using EyouSoft.Model.GovStructure;

namespace Web.ManageCenter.Rule
{
    /// <summary>
    /// 行政中心-规章制度-列表
    /// </summary>
    /// 修改人：方琪
    /// 创建时间：2012-03-23
    public partial class RuleList : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 页大小
        /// </summary>
        private int pageSize = 20;
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
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (doType != null && doType.Length > 0)
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
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            BRegulation BLL = new BRegulation();
            EyouSoft.Model.GovStructure.MGovRegSearch resmodel = new EyouSoft.Model.GovStructure.MGovRegSearch();
            resmodel.Code = Utils.GetQueryStringValue("txtNum");
            resmodel.Title = Utils.GetQueryStringValue("txtTitle");
            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue(this.DeptSelect1.SelectIDClient)))
            {
                resmodel.DepIds = Utils.GetQueryStringValue(this.DeptSelect1.SelectIDClient).Split(',');
            }
            else
            {
                resmodel.DepName = Utils.GetQueryStringValue(this.DeptSelect1.SelectNameClient);
            }
            this.DeptSelect1.SectionName = Utils.GetQueryStringValue(this.DeptSelect1.SelectNameClient);
            this.DeptSelect1.SectionID = Utils.GetQueryStringValue(this.DeptSelect1.SelectIDClient);
            IList<EyouSoft.Model.GovStructure.MGovRegulation> lst = BLL.GetGovRegulationList(this.SiteUserInfo.CompanyId, resmodel, AttachItemType.规章制度, pageSize, pageIndex, ref recordCount);
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
                this.RepList.Controls.Add(new Label() { Text = "<tr><td colspan='7' align='center'>对不起，没有相关数据！</td></tr>" });
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
            //对应执行操作
            if (doType == "delete")
            {
                string id = Utils.GetQueryStringValue("id");
                //执行并获取结果
                DeleteData(id);
            }
            //返回ajax操作结果

        }
        #endregion

        #region 删除操作
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id">删除ID</param>
        /// <returns></returns>
        private void DeleteData(string id)
        {
            bool b = false;
            string msg = string.Empty;
            if (!String.IsNullOrEmpty(id))
            {
                BRegulation BLL = new BRegulation();
                b = BLL.DeleteGovRegulation(id.Split(','));
            }
            msg = b ? "删除成功！" : "删除失败！";
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(b ? "1" : "0", msg));
            Response.End();
        }
        #endregion

        #region 获取附件标签
        /// <summary>
        /// 获取附件标签
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string GetUrl(string id)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.GovStructure.BRegulation bll = new EyouSoft.BLL.GovStructure.BRegulation();
                EyouSoft.Model.ComStructure.MComAttach attachModel = new EyouSoft.Model.ComStructure.MComAttach();
                EyouSoft.Model.GovStructure.MGovRegulation ruleModel = new EyouSoft.Model.GovStructure.MGovRegulation();
                ruleModel = bll.GetGovRegulationModel(id);
                if (ruleModel != null)
                {
                    if (ruleModel.ComAttachList != null && ruleModel.ComAttachList.Count > 0)
                    {
                        attachModel = ruleModel.ComAttachList.First();
                        sb.AppendFormat("<a title=\"附件\" target=\"_blank\" href=\"/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}\" ><img src=\"/Images/fujian.gif\" /></a>", attachModel.FilePath, attachModel.Name);
                        return sb.ToString();
                    }
                }
            }
            return string.Empty;
        }
        #endregion

        #region 获取适用部门名称
        /// <summary>
        /// 获取适用部门名称
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetApplyDept(object o)
        {
            string applyGept = string.Empty;
            if (o != null)
            {
                IList<MGovRegApplyDept> ApplyDeptList = (IList<MGovRegApplyDept>)o;

                if (ApplyDeptList != null && ApplyDeptList.Count > 0)
                {
                    foreach (var item in ApplyDeptList)
                    {
                        applyGept += item.DepartName + ",";
                    }
                    applyGept = applyGept.Length > 0 ? applyGept.Substring(0, applyGept.Length - 1) : "";
                }
            }
            return applyGept;
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_规章制度_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_规章制度_栏目, false);
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_规章制度_新增))
            {
                ph_Add.Visible = false;

            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_规章制度_修改))
            {
                ph_Update.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_规章制度_删除))
            {
                ph_Del.Visible = false;
            }
        }
        #endregion


    }
}