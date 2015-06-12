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
    /// 合同号管理-合同号销号
    /// </summary>
    /// 修改人：邵权江
    /// 创建时间：2012-04-10
    public partial class ContractCodeDestroy : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (doType == "save")
            {
                PageSave(Utils.GetQueryStringValue("cid").Split(','), Utils.GetQueryStringValue("sta").Split(','));
            }
            #endregion

            if (!IsPostBack)
            {
                //根据ID初始化页面
                PageInit(Utils.GetQueryStringValue("ids").Split(','));
            }
        }

        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string[] ids)
        {
            IList<EyouSoft.Model.ConStructure.MContractNumList> list = new EyouSoft.BLL.ConStructure.BContractNum().GetContractNumList(SiteUserInfo.CompanyId, ids);
            repList.DataSource = list;
            repList.DataBind();
            //释放
            list = null;
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        protected void PageSave(string[] ids, string[] sts)
        {
            #region 表单取值
            string msg = "";
            bool result = false;
            Response.Clear();
            #endregion

            #region 表单验证
            if (ids.Length < 1 || ids.Length != sts.Length)
            {
                msg += "-操作有误！";
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
            int i = bll.ChangeContractStatus(CreateListModel(ids,sts));
            if (i == 0)
            {
                result = false;
                msg = "操作失败！";
            }
            if (i == 1)
            {
                result = true;
                msg = "操作成功！";
            }
            Response.Clear();
            Response.Write("{\"result\":\"" + result + "\",\"msg\":\"" + msg + "\"}");
            Response.End();
            #endregion

        }
        #endregion

        #region 生成合同号状态集合
        /// <summary>
        /// 生成合同号状态集合
        /// </summary>
        /// <param name="ids">合同号编号集合</param>
        /// <param name="sts">合同号状态集合</param>
        /// <returns></returns>
        protected IList<EyouSoft.Model.ConStructure.MContractStatus> CreateListModel(string[] ids, string[] sts)
        {
            IList<EyouSoft.Model.ConStructure.MContractStatus> listModel = new List<EyouSoft.Model.ConStructure.MContractStatus>();
            EyouSoft.Model.ConStructure.MContractStatus model = null;
            if (ids.Length == sts.Length)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    model = new EyouSoft.Model.ConStructure.MContractStatus();
                    model.ContractId = ids[i];
                    model.ContractStatus = (EyouSoft.Model.EnumType.ConStructure.ContractStatus)Utils.GetInt(sts[i]);
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
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_销号))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_销号, false);
                return;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_作废))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_作废, false);
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
