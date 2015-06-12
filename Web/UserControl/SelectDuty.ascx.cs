using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Web.UserControl
{
    /// <summary>
    /// 选择职务
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-03-26
    public partial class SelectDuty : System.Web.UI.UserControl
    {
        /// <summary>
        /// 职务ID
        /// </summary>
        public string DutyID { get; set; }
        /// <summary>
        /// 职务名
        /// </summary>
        public string DutyName { get; set; }
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
        /// <summary>
        /// 弹出模式，"1":单选 "2":多选
        /// </summary>
        public string SModel { get; set; }
        /// <summary>
        /// 隐藏域客户端ID和Name
        /// </summary>
        public string DutyIDClient
        {
            get { return SetPriv + "_hideSellID"; }
        }

        /// <summary>
        /// 显示文本框客户端ID和Name
        /// </summary>
        public string DutyNameClient
        {
            get { return SetPriv + "_txtSellName"; }
        }

        /// <summary>
        /// 设置控件只读,默认为可以修改
        /// </summary>
        private bool _readOnly = false;
        [Bindable(true)]
        public bool ReadOnly
        {
            get { return _readOnly; }
            set { _readOnly = value; }
        }

        private string _setPriv = string.Empty;
        /// <summary>
        /// 指定控件Name前缀，默认为控件ClientID
        /// </summary>
        public string SetPriv
        {
            get { return string.IsNullOrEmpty(_setPriv) ? this.ClientID : _setPriv; }
            set { _setPriv = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}