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
    /// 合同号管理列表
    /// 创建者：邵权江 日期：2012.4.6
    /// </summary>
    public partial class ContractCodeList : BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        protected int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        protected int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;
        
        /// <summary>
        /// 二级栏目编号
        /// </summary>
        protected int sl = 0;
        /// <summary>
        /// 打印单链接
        /// </summary>
        protected string PrintPages = string.Empty;
        /// <summary>
        /// 合同类型
        /// </summary>
        protected EyouSoft.Model.EnumType.ConStructure.ContractType HeTongLeiXing = EyouSoft.Model.EnumType.ConStructure.ContractType.国内合同;
        /// <summary>
        /// 删除合同权限
        /// </summary>
        bool Privs_Delete = false;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            if (Utils.GetQueryStringValue("dotype") == "deletehetong") DeleteHeTong();

            if (Utils.GetQueryStringValue("dotype") == "shezhistatus") SheZhiStatus();

            PrintPages = new EyouSoft.BLL.ComStructure.BComSetting().GetPrintUri(SiteUserInfo.CompanyId, EyouSoft.Model.EnumType.ComStructure.PrintTemplateType.散拼行程单);
            
            HeTongLeiXing = Utils.GetEnumValue<EyouSoft.Model.EnumType.ConStructure.ContractType>(Utils.GetQueryStringValue("type"), EyouSoft.Model.EnumType.ConStructure.ContractType.国内合同);
            sl = Utils.GetInt(Utils.GetQueryStringValue("sl"));
            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                //初始化
                DataInit();
            }
        }

        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            #region 获取查询条件
            //合同号状态
            int status = Utils.GetInt(Utils.GetQueryStringValue("sltStatus"), -1);
            this.litStatue.Text = UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.ConStructure.ContractStatus)), status.ToString(), true);
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            //领用人
            string sellerId = Utils.GetQueryStringValue(this.HrSelect1.HrSelectIDClient);
            string sellerName = Utils.GetQueryStringValue(this.HrSelect1.HrSelectNameClient);
            this.HrSelect1.HrSelectID = sellerId;
            this.HrSelect1.HrSelectName = sellerName;
            //合同号
            string conCode = Utils.GetQueryStringValue("txt_ConCode");
            //领用时间
            DateTime? sDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txt_SDate"));
            DateTime? eDate = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txt_EDate"));
            EyouSoft.Model.ConStructure.MContractNumSearch searchModel = null;
            if (!string.IsNullOrEmpty(conCode) || !string.IsNullOrEmpty(sellerId) || !string.IsNullOrEmpty(sellerName) || status > -1 || sDate != null || eDate != null)
            {
                searchModel = new EyouSoft.Model.ConStructure.MContractNumSearch();
                searchModel.ContractCode = conCode;
                searchModel.UseId = sellerId;
                if (string.IsNullOrEmpty(sellerId) && !string.IsNullOrEmpty(sellerName))
                {
                    searchModel.UseName = sellerName;
                }
                searchModel.TimeStart = sDate;
                searchModel.TimeEnd = eDate;
                if (status > -1)
                {
                    searchModel.ContractStatus = (EyouSoft.Model.EnumType.ConStructure.ContractStatus)status;
                }
            }
            #endregion
            //获取列表
            IList<EyouSoft.Model.ConStructure.MContractNumList> list = new EyouSoft.BLL.ConStructure.BContractNum().GetContractNumList(SiteUserInfo.CompanyId, (int)HeTongLeiXing, searchModel, this.pageSize, this.pageIndex, ref this.recordCount);
            if (list != null && list.Count > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
            }
            else
            {
                this.rptList.Controls.Add(new Label() { Text = "<tr><td colspan='11' align='center'>对不起，没有相关数据！</td></tr>" });
            }
            //绑定分页
            BindPage();
            //释放
            list = null;
        }

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

            this.ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect2.intPageSize = pageSize;
            this.ExporPageInfoSelect2.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect2.intRecordCount = recordCount;
            if (recordCount <= pageSize)
            {
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;   
            }
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_栏目, true);
                return;
            }
            this.phForAdd.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_登记);
            this.phForCol.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_领用);
            this.phForDes.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_销号);
            this.phForDes.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_作废);

            phDelete.Visible = Privs_Delete;
        }

        /// <summary>
        /// 删除合同
        /// </summary>
        void DeleteHeTong()
        {
            if (!Privs_Delete) RCWE(UtilsCommons.AjaxReturnJson("0", "你没有操作权限"));

            var items = Utils.GetFormValues("txtHeTongId[]");
            if (items == null || items.Length == 0) RCWE(UtilsCommons.AjaxReturnJson("0", "删除失败：没有要删除的合同信息。"));

            var bll = new EyouSoft.BLL.ConStructure.BContractNum();
            foreach (var item in items)
            {
                bll.Delete(SiteUserInfo.CompanyId, item);
            }

            RCWE(UtilsCommons.AjaxReturnJson("1", "删除成功！"));
        }

        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            Privs_Delete = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_删除);
        }

        /// <summary>
        /// shezhi status
        /// </summary>
        void SheZhiStatus()
        {
            string txtHeTongId = Utils.GetFormValue("txtHeTongId");
            string txtFS = Utils.GetFormValue("txtFS");

            if (string.IsNullOrEmpty(txtHeTongId) || string.IsNullOrEmpty(txtFS)) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：异常请求"));

            EyouSoft.Model.EnumType.ConStructure.ContractStatus? status = null;

            if (txtFS == "quxiaoguanlian") status = EyouSoft.Model.EnumType.ConStructure.ContractStatus.领用;
            if (txtFS == "xiaohao") status = EyouSoft.Model.EnumType.ConStructure.ContractStatus.销号;
            if (txtFS == "zuofei") status = EyouSoft.Model.EnumType.ConStructure.ContractStatus.作废;
            if (txtFS == "quxiaoxiaohao") status = EyouSoft.Model.EnumType.ConStructure.ContractStatus.使用;
            if (txtFS == "quxiaozuofei") status = EyouSoft.Model.EnumType.ConStructure.ContractStatus.使用;

            if (!status.HasValue) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：异常请求"));

            bool privs = false;

            switch (status.Value)
            {
                case EyouSoft.Model.EnumType.ConStructure.ContractStatus.领用:
                    privs = true;
                    break;
                case EyouSoft.Model.EnumType.ConStructure.ContractStatus.使用:
                    privs = true;
                    break;
                case EyouSoft.Model.EnumType.ConStructure.ContractStatus.未领用:
                    privs = false;
                    break;
                case EyouSoft.Model.EnumType.ConStructure.ContractStatus.销号:
                    privs = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_销号); ;
                    break;
                case EyouSoft.Model.EnumType.ConStructure.ContractStatus.作废:
                    privs = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_作废); ;
                    break;
            }

            if (!privs) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：没有操作权限"));

            int bllRetCode = new EyouSoft.BLL.ConStructure.BContractNum().SheZhiHeTongStatus(CurrentUserCompanyID, txtHeTongId, status.Value);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else if (bllRetCode == -98) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未关联订单的合同不允许销号"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        #endregion

        #region protected members
        /// <summary>
        /// get caozuo
        /// </summary>
        /// <param name="status">status</param>
        /// <returns></returns>
        protected string GetCaoZuo(object status)
        {
            string s = string.Empty;
            var _status = (EyouSoft.Model.EnumType.ConStructure.ContractStatus)status;

            switch (_status)
            {
                case EyouSoft.Model.EnumType.ConStructure.ContractStatus.领用:
                    //s = "<a href=\"javascript:void(0)\" data-class=\"caozuo\" data-fs=\"guanliandingdan\">关联订单</a>";
                    break;
                case EyouSoft.Model.EnumType.ConStructure.ContractStatus.使用:
                    s = "<a href=\"javascript:void(0)\" data-class=\"caozuo\"  data-fs=\"quxiaoguanlian\">取消关联</a>&nbsp;" + "<a href=\"javascript:void(0)\" data-class=\"caozuo\"  data-fs=\"xiaohao\">销号</a>&nbsp;" + "<a href=\"javascript:void(0)\" data-class=\"caozuo\"  data-fs=\"zuofei\">作废</a>";
                    break;
                case EyouSoft.Model.EnumType.ConStructure.ContractStatus.未领用:
                    //s = "<a href=\"javascript:void(0)\" data-class=\"caozuo\"  data-fs=\"lingyong\">领用合同</a>";
                    break;
                case EyouSoft.Model.EnumType.ConStructure.ContractStatus.销号:
                    s = "<a href=\"javascript:void(0)\" data-class=\"caozuo\"  data-fs=\"quxiaoxiaohao\">取消销号</a>";
                    break;
                case EyouSoft.Model.EnumType.ConStructure.ContractStatus.作废:
                    s = "<a href=\"javascript:void(0)\" data-class=\"caozuo\"  data-fs=\"quxiaozuofei\">取消作废</a>";
                    break;
            }

            return s;
        }
        #endregion
    }
}
