using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.FinStructure;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.FinStructure;
using EyouSoft.Model.EnumType.PrivsStructure;
using System.Text;

namespace Web.FinanceManage.FixedAssets
{
    /// <summary>
    /// 固定资产-列表-
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-13
    public partial class List : BackPage
    {

        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (doType.Length > 0)
            {
                AJAX(doType);
            }
            #endregion

            //权限判断
            PowerControl();
            if (UtilsCommons.IsToXls())
            {
                ToXls();
            }
            //初始化
            DataInit();


        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            SellsSelect1.SetTitle = "选择 管理责任人";
            SellsSelect1.ParentIframeID = Utils.GetQueryStringValue("iframeId");
            SelectSection1.SetTitle = "选择 使用部门";
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            #region 查询实体
            MAssetBase queryModel = new MAssetBase();
            //编号
            queryModel.AssetCode = Utils.GetQueryStringValue("txt_Id");
            //资产名称
            queryModel.AssetName = Utils.GetQueryStringValue("txt_propertyName");
            queryModel.CompanyId = CurrentUserCompanyID;
            queryModel.Department = SelectSection1.SectionName = Utils.GetQueryStringValue(SelectSection1.SelectNameClient);
            queryModel.DepartmentId = Utils.GetInt(SelectSection1.SectionID = Utils.GetQueryStringValue(SelectSection1.SelectIDClient));
            queryModel.Admin = SellsSelect1.SellsName = Utils.GetQueryStringValue(SellsSelect1.SellsNameClient);
            queryModel.AdminId = SellsSelect1.SellsID = Utils.GetQueryStringValue(SellsSelect1.SellsIDClient);
            #endregion


            IList<MAsset> ls = new BFinance().GetAssetLst(pageSize, pageIndex, ref recordCount, queryModel);
            if (ls != null && ls.Count > 0)
            {
                pan_Msg.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
                BindPage();
            }
            ExporPageInfoSelect1.Visible = ls != null && ls.Count > 0 && recordCount > pageSize;
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;
        }
        /// <summary>
        /// ajax操作
        /// </summary>
        private void AJAX(string doType)
        {
            string msg = string.Empty;

            //对应执行操作
            switch (doType)
            {
                case "del":
                    //判断权限
                    int[] ids = Utils.GetIntArray(Utils.GetQueryStringValue("Ids"), ",");
                    if (new BFinance().DelAsset(CurrentUserCompanyID, ids) > 0)
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
                    }
                    else
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "删除失败!"));
                    }

                    break;
                default:
                    break;
            }

        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(Privs.财务管理_固定资产_栏目))
            {
                Utils.ResponseNoPermit(Privs.财务管理_固定资产_栏目, true);
                return;
            }
            else
            {
                pan_Add.Visible = CheckGrant(Privs.财务管理_固定资产_新增);

                pan_updata.Visible = CheckGrant(Privs.财务管理_固定资产_修改);

                pan_delete.Visible = CheckGrant(Privs.财务管理_固定资产_删除);
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        private void ToXls()
        {
            int recordCount = 0;

            IList<MAsset> ls = new BFinance().GetAssetLst(UtilsCommons.GetToXlsRecordCount(), 1, ref recordCount, new MAssetBase { CompanyId = CurrentUserCompanyID });
            if (ls != null && ls.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\n",
                    "编号",
                    "资产名称",
                    "购买时间",
                    "原始价值",
                    "折旧年限",
                    "使用部门",
                    "管理责任人");
                foreach (MAsset item in ls)
                {
                    sb.Append(item.AssetCode + "\t");
                    sb.Append(item.AssetName + "\t");
                    sb.Append(UtilsCommons.GetDateString(item.BuyTime, ProviderToDate) + "\t");
                    sb.Append(UtilsCommons.GetMoneyString(item.BuyPrice, ProviderToMoney) + "\t");
                    sb.Append(item.DepreciableLife + "\t");
                    sb.Append(item.Department + "\t");
                    sb.Append(item.Admin + "\n");
                }
                ResponseToXls(sb.ToString());
            }
            ResponseToXls(string.Empty);

        }
        #endregion
    }
}
