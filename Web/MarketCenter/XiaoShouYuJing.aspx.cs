using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;

namespace Web.MarketCenter
{
    /// <summary>
    /// 销售中心-销售预警
    /// </summary>
    public partial class XiaoShouYuJing : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        /// <summary>
        /// 页记录数
        /// </summary>
        int pageSize = 20;
        /// <summary>
        /// 页索引
        /// </summary>
        int pageIndex = 1;
        /// <summary>
        /// 总记录数
        /// </summary>
        int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PowerControl();
                DataInit();
            }
        }

        #region private members
        /// <summary>
        /// 权限验证
        /// </summary>
        void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售预警_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.销售中心_销售预警_销售预警栏目, true);
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        void DataInit()
        {
            #region 获取查询条件
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            //合同号状态
            int dep = Utils.GetInt(Utils.GetQueryStringValue("sltDept"), 0);
            //销售员
            string sellerId = Utils.GetQueryStringValue(this.txtXiaoShouYuan.SellsIDClient);
            string sellerName = Utils.GetQueryStringValue(this.txtXiaoShouYuan.SellsNameClient);
            this.txtXiaoShouYuan.SellsID = sellerId;
            this.txtXiaoShouYuan.SellsName = sellerName;

            //查询实体
            EyouSoft.Model.FinStructure.MWarningBase searchModel = new EyouSoft.Model.FinStructure.MWarningBase();
            if (dep > 0)
            {
                searchModel.DeptId = dep;
            }
            searchModel.SellerId = sellerId;
            searchModel.SellerName = sellerName;    
            searchModel.SignArrear = (EyouSoft.Model.EnumType.FinStructure.EqualSign?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.FinStructure.EqualSign), Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperator));
            searchModel.Arrear= Utils.GetDecimalNull(Utils.GetQueryStringValue(CaiWuShaiXuan1.ClientUniqueIDOperatorNumber));
            searchModel.SignTransfinite = (EyouSoft.Model.EnumType.FinStructure.EqualSign?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.FinStructure.EqualSign), Utils.GetQueryStringValue(CaiWuShaiXuan2.ClientUniqueIDOperator));
            searchModel.Transfinite = Utils.GetDecimalNull(Utils.GetQueryStringValue(CaiWuShaiXuan2.ClientUniqueIDOperatorNumber));

            #endregion
            EyouSoft.BLL.FinStructure.BFinance bll = new EyouSoft.BLL.FinStructure.BFinance();
            IList<EyouSoft.Model.FinStructure.MSalesmanWarning> list = bll.GetSalesmanWarningLst(this.pageSize, this.pageIndex, ref this.recordCount,SiteUserInfo.CompanyId, searchModel);

            if (list != null && list.Count > 0)
            {
                rptList.DataSource = list;
                rptList.DataBind();
            }
            else
            {
                this.rptList.Controls.Add(new Label() { Text = "<tr><td colspan='8' align='center'>对不起，没有相关数据！</td></tr>" });
            }
            //绑定分页
            BindPage();
            //释放
            searchModel = null;
            bll = null;

        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        void BindPage()
        {
            if (recordCount <= pageSize)
            {
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;
            }

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
        }

        /// <summary>
        /// 获得部门Options
        /// </summary>
        /// <param name="selectIndex">设置选择项</param>
        /// <returns></returns>
        public static string GetDepartLineForSelect(int selectIndex, string companyID)
        {
            StringBuilder sb = new StringBuilder();
            IList<EyouSoft.Model.ComStructure.MComDepartment> list = new EyouSoft.BLL.ComStructure.BComDepartment().GetList(companyID);
            sb.Append("<option value=\"0\">-请选择-</option>");
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].DepartId != selectIndex)
                    {
                        sb.Append("<option value=\"" + list[i].DepartId + "\">" + list[i].DepartName + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value=\"" + list[i].DepartId + "\" selected=\"selected\">" + list[i].DepartName + "</option>");
                    }
                }
            }
            return sb.ToString();
        }
            #endregion
    }
}
