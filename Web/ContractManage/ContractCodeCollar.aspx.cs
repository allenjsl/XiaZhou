using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.ContractManage
{
    /// <summary>
    /// 合同号管理-合同号领用
    /// </summary>
    /// 修改人：邵权江
    /// 创建时间：2012-04-10
    public partial class ContractCodeCollar : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            this.HrSelect1.ParentIframeID = Utils.GetQueryStringValue("iframeId");
            this.HrSelect1.SetTitle = "选择领用人";
            this.HrSelect1.CallBackFun = "ContractCodeCollar.CallBackFun";
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (doType == "save")
            {
                PageSave();
            }
            #endregion

            if (!IsPostBack)
            {
                //初始化页面
                PageInit();
            }
        }

        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit()
        {

        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        protected void PageSave()
        {
            #region 表单取值
            string msg = "";
            bool result = false;
            Response.Clear();
            //int type = Utils.GetQueryStringValue("type") != "1" ? 0 : 1;
            string[] ids = Utils.GetQueryStringValue("ids").Split(',');
            string[] codes = Utils.GetQueryStringValue("codes").Split(',');
            DateTime collarTime = Utils.GetDateTime(this.txtCollarTime.Text.Trim(), DateTime.Now);

            string useId = Utils.GetFormValue(this.HrSelect1.HrSelectIDClient); 
            int departId = Utils.GetInt(Utils.GetFormValue(this.hideDeptID.UniqueID));
            #endregion

            #region 表单验证
            if (ids.Length < 1 || ids.Length != codes.Length)
            {
                msg += "-领用的合同号有误！";
            }
            if (string.IsNullOrEmpty(useId))
            {
                msg += "-领用人必须填写！";
            }
            if (departId == 0)
            {
                msg += "-领用部门必须填写！";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                result = false;
                Response.Write("{\"result\":\"" + result + "\",\"msg\":\"" + msg + "\"}");
                Response.End();
                return;
            }
            #endregion

            #region 提交回应
            EyouSoft.BLL.ConStructure.BContractNum bll = new EyouSoft.BLL.ConStructure.BContractNum();
            int i = bll.AddContractNumCollar(this.SiteUserInfo.CompanyId,CreateListModel(ids, codes, useId, departId, collarTime));
            if (i < 0)
            {
                result = false;
                msg = "领用失败！";
            }
            if (i == 0)
            {
                result = true;
                msg = "领用成功！";
            }
            if (i > 0)
            {
                result = false;
                msg = "领用合同号失败，其中有 " + i + " 条合同号领用失败！";
            }
            Response.Clear();
            Response.Write("{\"result\":\"" + result + "\",\"msg\":\"" + msg + "\"}");
            Response.End();
            #endregion

        }
        #endregion

        #region 生成合同号领用集合
        /// <summary>
        /// 生成合同号领用集合
        /// </summary>
        /// <param name="ids">合同号id集合</param>
        /// <param name="codes">合同号集合</param>
        /// <param name="useId">领用人id</param>
        /// <param name="departId">领用部门id</param>
        /// <param name="collarTime">领用时间</param>
        /// <returns></returns>
        protected IList<EyouSoft.Model.ConStructure.MContractNumCollar> CreateListModel(string[] ids, string[] codes, string useId, int departId, DateTime collarTime)
        {
            IList<EyouSoft.Model.ConStructure.MContractNumCollar> listModel = new List<EyouSoft.Model.ConStructure.MContractNumCollar>();
            EyouSoft.Model.ConStructure.MContractNumCollar model = null;
            if (ids.Length == codes.Length)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    model = new EyouSoft.Model.ConStructure.MContractNumCollar();
                    model.ContractId = ids[i];
                    model.ContractCode = codes[i];
                    model.DepartId = departId;
                    model.UseId = useId;
                    model.OperatorId = this.SiteUserInfo.UserId;
                    model.IssueTime = collarTime;
                    listModel.Add(model);
                    model = null;
                }
            }
            return listModel;
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_栏目, false);
                return;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_领用))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_领用, false);
                return;
            }
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
