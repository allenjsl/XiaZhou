using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace EyouSoft.Web.UserControl
{
    /// <summary>
    /// 用户控件-上车地点选用
    /// </summary>
    /// 时间：2012-08-13
    public partial class PickUpPoint : System.Web.UI.UserControl
    {
        /// <summary>
        /// 设置标题
        /// </summary>
        public string SetTitle { get; set; }

        /// <summary>
        /// 弹窗父级iframeID
        /// </summary>
        private string parentIframeID;
        public string ParentIframeID
        {
            get { return parentIframeID ?? EyouSoft.Common.Utils.GetQueryStringValue("iframeId"); }
            set { parentIframeID = value; }
        }
        /// 设置回调方法
        /// </summary>
        public string CallBackFun { get; set; }

        private string _setPriv = string.Empty;
        /// <summary>
        /// 指定控件Name前缀，默认为控件ClientID
        /// </summary>
        public string SetPriv
        {
            get { return string.IsNullOrEmpty(_setPriv) ? this.ClientID : _setPriv; }
            set { _setPriv = value; }
        }

        /// <summary>
        /// 加载已选用上车地点
        /// </summary>
        public string PickUpOptions { get; set; }

        public string PickUpTourId { get; set; }

        public string PickUpTourIdClientID { get { return SetPriv + "_hidPickUpTourId"; } }

        /// <summary>
        /// 上车地点编号
        /// </summary>
        [Bindable(true)]
        public string PickUpID { get; set; }

        /// <summary>
        /// 上车地点编号客户端ID
        /// </summary>
        public string PickUpIDClientID
        {
            get { return SetPriv + "_hidPickUpID"; }
        }

        /// <summary>
        /// 上车地点名称
        /// </summary>
        [Bindable(true)]
        public string PickUpText { get; set; }

        /// <summary>
        /// 上车地点名称客户端ID
        /// </summary>
        public string PickUpTextClientID
        {
            get { return SetPriv + "_hidPickUpText"; }
        }

        /// <summary>
        /// 上车地点接价
        /// </summary>
        [Bindable(true)]
        public string PickUpPriceJ { get; set; }

        /// <summary>
        /// 上车地点接价客户端ID
        /// </summary>
        public string PickUpPriceJClientID
        {
            get { return SetPriv + "_hidPickUpPriceJ"; }
        }

        /// <summary>
        /// 上车地点送价
        /// </summary>
        [Bindable(true)]
        public string PickUpPriceS { get; set; }

        /// <summary>
        /// 上车地点送价客户端ID
        /// </summary>
        public string PickUpPriceSClientID
        {
            get { return SetPriv + "_hidPickUpPriceS"; }
        }
        /// <summary>
        /// 上车地点描述
        /// </summary>
        public string PickUpDesc { get; set; }
        /// <summary>
        /// 上车地点描述客户端ID
        /// </summary>
        public string PickUpDescClientID { get { return SetPriv + "_hidPickUpDes"; } }
        protected void Page_Load(object sender, EventArgs e)
        {

        }


    }
}