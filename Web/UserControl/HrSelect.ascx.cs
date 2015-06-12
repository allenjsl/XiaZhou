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
    /// 人事选择
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-03-26
    public partial class HrSelect : System.Web.UI.UserControl
    {
        /// <summary>
        /// 人事编号
        /// </summary>
        public string HrSelectID { get; set; }
        /// <summary>
        /// 人事名称
        /// </summary>
        public string HrSelectName { get; set; }
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
        public string HrSelectIDClient
        {
            get { return SetPriv + "_hideSelectID"; }
        }

        /// <summary>
        /// 显示文本框客户端ID和Name
        /// </summary>
        public string HrSelectNameClient
        {
            get { return SetPriv + "_txtSelectName"; }
        }
        /// <summary>
        /// 人员部门编号
        /// </summary>
        public string ClientDeptID { get; set; }
        /// <summary>
        /// 人员部门名称
        /// </summary>
        public string ClientDeptName { get; set; }
        /// <summary>
        /// 设置回调方法
        /// </summary>
        public string CallBackFun { get; set; }
        /// <summary>
        /// 所选人部门编号
        /// </summary>
        public string HrSelectDeptID { get; set; }
        /// <summary>
        /// 所选人部门名称
        /// </summary>
        public string HrSelectDeptName { get; set; }

        /// <summary>
        /// 设置控件只读,默认为可以修改
        /// </summary>
        private bool _readOnly = false;
        public bool ReadOnly
        {
            get { return _readOnly; }
            set { _readOnly = value; }
        }

        /// <summary>
        /// 是否为必填
        /// </summary>
        private bool _isValid = true;
        [Bindable(true)]
        public bool IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }

        /// <summary>
        /// 设置a标签是否可见
        /// </summary>
        private bool _isDisplay = false;
        public bool IsDisplay
        {
            get { return _isDisplay; }
            set { _isDisplay = value; }
        }

        /// <summary>
        /// 设置是否显示已分配帐号的人员信息
        /// </summary>
        private bool _isshow = true;
        [Bindable(true)]
        public bool Isshow
        {
            get { return _isshow; }
            set { _isshow = value; }
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