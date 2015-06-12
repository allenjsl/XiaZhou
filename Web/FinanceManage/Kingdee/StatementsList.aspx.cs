using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Model.FinStructure;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.InteropServices;


namespace EyouSoft.Web.FinanceManage.Kingdee
{
    /// <summary>
    /// 导出金蝶报表
    /// </summary>
    /// 创建人:
    /// 创建时间:
    public partial class StatementsList : BackPage
    {


        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out   int ID);


        #region 分页参数
        /// <summary>
        /// 每页数量
        /// </summary>
        int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        int pageIndex = 0;
        /// <summary>
        /// 一共多少页
        /// </summary>
        int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            PowerControl();
            if (!IsPostBack)
            {
                BingCom();
            }



            if (UtilsCommons.IsToXls())
            {
                ToXls();
            }

        }
        #region 私有方法

        #region 绑定分页
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            ExporPageInfoSelect2.PageLinkURL = ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            ExporPageInfoSelect2.UrlParams = Request.QueryString;
            ExporPageInfoSelect2.intPageSize = pageSize;
            ExporPageInfoSelect2.CurrencyPage = pageIndex;
            ExporPageInfoSelect2.intRecordCount = recordCount;

        }
        #endregion

        #region 查询报表数据
        /// <summary>
        /// 查询报表数据
        /// </summary>
        private void BingCom()
        {
            pageIndex = EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("Page"));


            MKingDeeProofBase searchModel = new MKingDeeProofBase();
            searchModel.FDateS = Utils.GetQueryStringValue("txtDateStart");
            searchModel.FDateE = Utils.GetQueryStringValue("txtDateEnd");
            if (Utils.GetQueryStringValue("radImportYes") != "")
            {
                searchModel.FIsExport = Utils.GetQueryStringValue("radImportYes") == "true";
            }
            searchModel.FTourCode = Utils.GetQueryStringValue("txtTourNo");

            IList<MKingDeeProofDetail> list = new BFinance().GetKingDeeProofLst(pageSize, pageIndex, ref recordCount, SiteUserInfo.CompanyId, searchModel);


            if (list != null && list.Count > 0)
            {
                rpt_List.DataSource = list;
                rpt_List.DataBind();
                BindPage();
            }
            else
            {
                litMsg.Text = "<tr class='old'><td colspan='11' align='center'>没有相关数据</td></tr>";
                ExporPageInfoSelect2.Visible = false;
            }

        }
        #endregion


        #region 导出excel、修改导出状态
        /// <summary>
        /// 导出所有未导出数据到EXCEL
        /// </summary>
        protected void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1)
            {
                ResponseToXls(string.Empty);
            }
            MKingDeeProofBase searchModel = new MKingDeeProofBase();
            IList<MKingDeeProofDetail> list = new BFinance().GetKingDeeProofLst(SiteUserInfo.CompanyId, searchModel);

            ApplicationClass xlsApp = new ApplicationClass();
            if (xlsApp == null)
            {
                throw new Exception("未安装Excel");
            }

            Workbook workbook = xlsApp.Workbooks.Open(EyouSoft.Toolkit.Utils.GetMapPath("/ExcelDownTemp/KingDee_Report.xls"), Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            Worksheet worksheet = workbook.Sheets[2] as Worksheet;

            for (int i = 0; i < list.Count; i++)
            {
                worksheet.Cells[i + 2, 1] = list[i].FDate;
                worksheet.Cells[i + 2, 2] = list[i].FYear;
                worksheet.Cells[i + 2, 3] = list[i].FPeriod;
                worksheet.Cells[i + 2, 4] = list[i].FGroupId;
                worksheet.Cells[i + 2, 5] = list[i].FNumber;
                worksheet.Cells[i + 2, 6] = list[i].FAccountNum;
                worksheet.Cells[i + 2, 7] = list[i].FAccountName;
                worksheet.Cells[i + 2, 8] = list[i].FCurrencyNum;
                worksheet.Cells[i + 2, 9] = list[i].FCurrencyName;
                worksheet.Cells[i + 2, 10] = list[i].FAmountFor;
                worksheet.Cells[i + 2, 11] = list[i].FDebit;
                worksheet.Cells[i + 2, 12] = list[i].FCredit;
                worksheet.Cells[i + 2, 13] = list[i].FPreparerId;
                worksheet.Cells[i + 2, 14] = "";
                worksheet.Cells[i + 2, 15] = "";
                worksheet.Cells[i + 2, 16] = "";
                worksheet.Cells[i + 2, 17] = "";
                worksheet.Cells[i + 2, 18] = "";
                worksheet.Cells[i + 2, 19] = "";
                worksheet.Cells[i + 2, 20] = list[i].FExplanation;
                worksheet.Cells[i + 2, 21] = "";
                worksheet.Cells[i + 2, 22] = "";
                worksheet.Cells[i + 2, 23] = "";
                worksheet.Cells[i + 2, 24] = "";
                worksheet.Cells[i + 2, 25] = list[i].FTransDate;
                worksheet.Cells[i + 2, 26] = "";
                worksheet.Cells[i + 2, 27] = "";
                worksheet.Cells[i + 2, 28] = "";
                worksheet.Cells[i + 2, 29] = "";
                worksheet.Cells[i + 2, 30] = "";
                worksheet.Cells[i + 2, 31] = "";
                worksheet.Cells[i + 2, 32] = list[i].FEntryId;
                worksheet.Cells[i + 2, 33] = list[i].FItem;
                worksheet.Cells[i + 2, 34] = "";
                worksheet.Cells[i + 2, 35] = "";
                worksheet.Cells[i + 2, 36] = "";
            }

            workbook.Saved = true;
            string FileName = DateTime.Now.ToString("yyyyMMddhhmmss");

            string dir = Server.MapPath("/Temp/Kis/" + DateTime.Now.ToString("yyyyMM"));
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string filePath = "/Temp/Kis/" + DateTime.Now.ToString("yyyyMM") + "/" + FileName + ".xls";

            workbook.SaveCopyAs(EyouSoft.Toolkit.Utils.GetMapPath(filePath));

            //修改导出状态状态
            BFinance bf = new BFinance();
            bf.SetProofExport(SiteUserInfo.CompanyId);

            workbook.Close(true, Type.Missing, Type.Missing);
            xlsApp.Quit();


            ////结束当前excel进程
            //if (xlsApp != null)
            //{
            //    int progre;
            //    GetWindowThreadProcessId(new IntPtr(xlsApp.Hwnd), out progre);
            //    System.Diagnostics.Process.GetProcessById(progre).Kill();
            //}



            worksheet = null;
            workbook = null;
            xlsApp = null;

            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson("1", filePath));
            Response.End();

        }

        #endregion
        #endregion

        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_金蝶报表_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_金蝶报表_栏目, false);
            }
        }
    }
}
