using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.ComponentModel;

namespace Web.UserControl
{
    public partial class selectGuid : System.Web.UI.UserControl
    {
        /// <summary>
        /// 导游ID
        /// </summary>
        private string _guidID;
        [Bindable(true)]
        public string GuidID
        {
            get { return _guidID; }
            set { _guidID = value; }
        }

        /// <summary>
        /// 隐藏域客户端ID和Name
        /// </summary>
        public string GuidIDClient
        {
            get { return SetPriv + "_hideGuidID"; }
        }
        /// <summary>
        /// 显示文本框客户端ID和Name
        /// </summary>
        public string GuidNameClient
        {
            get { return SetPriv + "_txtGuidName"; }
        }
        /// <summary>
        /// 导游姓名
        /// </summary>
        private string _guidName;
        [Bindable(true)]
        public string GuidName
        {
            get { return _guidName; }
            set { _guidName = value; }
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        private string _setTitle = "选择导游";
        [Bindable(true)]
        public string SetTitle
        {
            get { return _setTitle; }
            set { _setTitle = value; }
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

        /// <summary>
        /// 先赋值的控件的boxyID
        /// </summary>
        private string _parentIframeID;
        [Bindable(true)]
        public string ParentIframeID
        {
            get { return _parentIframeID ?? Utils.GetQueryStringValue("iframeId"); }
            set { _parentIframeID = value; }
        }
        /// <summary>
        /// 单选设置 true，多选设置为 fase ;默认单选 false
        /// </summary>
        private bool _sMode = false;
        [Bindable(true)]
        public bool SMode
        {
            get { return _sMode; }
            set { _sMode = value; }
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

        private string _companyID;
        /// <summary>
        /// 当前登录公司的ID
        /// </summary>
        public string CompanyID
        {
            get { return _companyID; }
            set { _companyID = value; }
        }

        private string _callBackFun;
        /// <summary>
        /// 设置回调
        /// </summary>
        public string CallBackFun
        {
            get { return _callBackFun; }
            set { _callBackFun = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}