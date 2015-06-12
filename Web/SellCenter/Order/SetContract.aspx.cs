using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.TourStructure;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.EnumType.ComStructure;
using System.Text;
using EyouSoft.Model.EnumType.TourStructure;
using EyouSoft.BLL.ComStructure;

namespace EyouSoft.Web.SellCenter.Order
{
    public partial class SetContract  : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (doType == "save")
            {
                PageSave();
            }
            if (!IsPostBack)
            {
                string orderID = Utils.GetQueryStringValue("orderID");
                if (orderID != "")
                {
                    PageInitForOrder(orderID);
                }

                if (orderID == "")
                {
                    Utils.ResponseGoBack();
                }
            }
        }
        private void PageInitForOrder(string orderID)
        {
            EyouSoft.BLL.TourStructure.BTourOrder bll = new EyouSoft.BLL.TourStructure.BTourOrder();
            EyouSoft.Model.TourStructure.MTourOrderExpand orderModel = bll.GetTourOrderExpandByOrderId(orderID);
            if (orderModel != null)
            {                
                this.lblOrderNum.Text = orderModel.OrderCode;
                this.ltrBuyCompanyName.Text = orderModel.BuyCompanyName;
                txtHeTongHao.HeTongId = orderModel.ContractId;
                txtHeTongHao.HeTongCode = orderModel.ContractCode;
            }
            else
            {
                Utils.ResponseGoBack();
            }
        }
        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        protected void PageSave()
        {
            /*
            #region 表单取值
            string msg = "";
            bool result = false;
            Response.Clear();
            string orderId = Utils.GetQueryStringValue("orderId");
            //合同号
            string contractCode = Utils.GetFormValue(txtHeTongHao.HeTongCodeClientID);
            string contractCodeId = Utils.GetFormValue(txtHeTongHao.HeTongIdClientID);
            #endregion

            #region 表单验证
            if (string.IsNullOrEmpty(orderId))
            {
                msg += "未找到您要设置的订单!";
            }
            if (string.IsNullOrEmpty(contractCode))
            {
                msg += "请输入合同号!";
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
            int i = new EyouSoft.BLL.TourStructure.BTourOrder().UpdateTourOrderContaract(orderId,contractCode,contractCodeId);
            if (i ==0)
            {
                result = false;
                msg = "登记失败！";
            }
            if (i == 1)
            {
                result = true;
                msg = "登记成功！";
            }
            Response.Clear();
            Response.Write("{\"result\":\"" + result + "\",\"msg\":\"" + msg + "\"}");
            Response.End();
            #endregion
            */

            string dingDanId = Utils.GetQueryStringValue("orderId");
            string heTongId = Utils.GetFormValue(txtHeTongHao.HeTongIdClientID);

            if (string.IsNullOrEmpty(dingDanId)) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败:异常请求"));
            if (string.IsNullOrEmpty(heTongId)) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败:请录入合同号"));

            int bllRetCode = new EyouSoft.BLL.TourStructure.BTourOrder().SheZhiDingDanHeTong(CurrentUserCompanyID, dingDanId, heTongId);
            if(bllRetCode==1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));

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
