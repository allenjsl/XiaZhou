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
    /// <summary>
    ///选择物品
    /// </summary>
    /// 创建人：徐从栎
    /// 创建时间：2011-10-11
    public partial class SelectObject : System.Web.UI.UserControl
    {
        /// <summary>
        /// 物品ID
        /// </summary>
        public string ObjectID { get; set; }
        /// <summary>
        /// 物品名
        /// </summary>
        public string ObjectName { get; set; }
        /// <summary>
        /// 设置标题
        /// </summary>
        public string SetTitle { get; set; }
        /// <summary>
        /// 弹出模式，"1":单选 "2":多选
        /// </summary>
        public string SModel { get; set; }

        /// <summary>
        /// 隐藏域客户端ID和Name
        /// </summary>
        public string SelectIDClient
        {
            get { return SetPriv + "_hideObjectID"; }
        }

        /// <summary>
        /// 显示文本框客户端ID和Name
        /// </summary>
        public string SelectNameClient
        {
            get { return SetPriv + "_txtObjectName"; }
        }
        /// <summary>
        /// 设置回调方法
        /// </summary>
        public string CallBackFun { get; set; }

        private string _parentIframeID;
        public string ParentIframeID
        {
            get { return _parentIframeID ?? Utils.GetQueryStringValue("iframeId"); }
            set { _parentIframeID = value; }
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
        /// 是否只读
        /// </summary>
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
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}