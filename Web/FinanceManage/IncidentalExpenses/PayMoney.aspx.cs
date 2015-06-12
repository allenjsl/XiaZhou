using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Model.FinStructure;
using EyouSoft.Model.EnumType.ComStructure;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.FinStructure;

namespace Web.FinanceManage.IncidentalExpenses
{
    /// <summary>
    /// 杂费支出
    /// 支付 批量支付 以支付
    /// 公共
    /// </summary>
    /// 创建人:柴逸宁
    /// 创建时间:2012-3-27
    public partial class PayMoney : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetFormValue("doType").Length > 0)
            {
                Save();
            }
            DataInit();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
           
            string OtherFeeID = Utils.GetQueryStringValue("OtherFeeID");
            IList<MOtherFeeInOut> sl = new BFinance().GetOtherFeeInOutLst(ItemType.支出, Utils.ConvertToIntArray(OtherFeeID.Split(',')));
            txt_PMDate.Text = UtilsCommons.GetDateString(DateTime.Now, ProviderToDate);
            lbl_PPerson.Text = SiteUserInfo.Name;
            if (sl != null && sl.Count > 0)
            {
                rpt_list.DataSource = sl;
                rpt_list.DataBind();
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            IList<MBatchPay> registers = new List<MBatchPay>();
            string[] data = Utils.GetFormValue("data").Split('|');
            int i = data.Length;
            while (i-- > 0)
            {
                string[] datas = data[i].Split(',');
                if (datas.Length == 3)
                {
                    registers.Add(new MBatchPay
                    {
                        RegisterId = Utils.GetInt(datas[0]),
                        PaymentType = Utils.GetInt(datas[1]),
                        PaymentTypeName = datas[2]

                    });
                }
            }
            string msg = string.Empty;

            bool retBool = new BFinance().SetOtherFeeOutPay(
                        SiteUserInfo.DeptId, //部门ID
                        SiteUserInfo.UserId,//出纳Id
                        SiteUserInfo.Name,//出纳
                        Utils.GetDateTime(Utils.GetFormValue("PMDate")), //支付时间(当前时间)
                        FinStatus.账务已支付,
                        registers//支付列表
                        ) > 0;
            if (retBool)
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("1"));
            }
            else
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "支付失败!"));
            }

        }
    }
}
