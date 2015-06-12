using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.ComponentModel;

namespace EyouSoft.Web.UserControl
{
    public partial class YLCompanySelect : System.Web.UI.UserControl
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 说明：选择销售员

        /// <summary>
        /// 游轮公司ID
        /// </summary>
        private string _compID;
        [Bindable(true)]
        public string ComPid
        {
            get { return _compID; }
            set { _compID = value; }
        }
        /// <summary>
        /// 隐藏域客户端ID和Name
        /// </summary>
        public string CompIDClient
        {
            get { return SetPriv + "_hideCompID"; }
        }
        /// <summary>
        /// 显示文本框客户端ID和Name
        /// </summary>
        public string CompNameClient
        {
            get { return SetPriv + "_txtCompName"; }
        }
        /// <summary>
        /// 邮轮公司名称
        /// </summary>
        private string _compName;
        [Bindable(true)]
        public string CompName
        {
            get { return _compName; }
            set { _compName = value; }
        }
        /// <summary>
        /// 设置标题
        /// </summary>
        private string _setTitle = "游轮公司";
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
            get { return _sMode ? true : _readOnly; }
            set { _readOnly = value; }
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

        /// <summary>
        /// 是否强制选中和失去焦点选择
        /// </summary>
        private bool _selectFirst = true;
        [Bindable(true)]
        public bool SelectFrist
        {
            get { return _selectFirst; }
            set { _selectFirst = value; }
        }

        private bool _isShowSelect = true;
        /// <summary>
        /// 是否显示选用按钮
        /// </summary>
        [Bindable(true)]
        public bool IsShowSelect
        {
            get { return _isShowSelect; }
            set { _isShowSelect = value; }
        }

        public int T1
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }


    }
}