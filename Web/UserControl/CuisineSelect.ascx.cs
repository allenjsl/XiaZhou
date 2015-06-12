using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace EyouSoft.Web.UserControl
{
    public partial class CuisineSelect : System.Web.UI.UserControl
    {
        /// <summary>
        /// 菜系编号
        /// </summary>
        public string CuisineSelectID { get; set; }
        /// <summary>
        /// 菜系名称
        /// </summary>
        public string CuisineSelectName { get; set; }
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
        public string CuisineSelectIDClient
        {
            get { return SetPriv + "_hideCuisineID"; }
        }

        /// <summary>
        /// 显示文本框客户端ID和Name
        /// </summary>
        public string CuisineSelectNameClient
        {
            get { return SetPriv + "_txtCuisineName"; }
        }
        /// <summary>
        /// 设置回调方法
        /// </summary>
        public string CallBackFun { get; set; }


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