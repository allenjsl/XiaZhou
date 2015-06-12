using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.UserControl
{
    /// <summary>
    ///选择地接社
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2011-9-28
    public partial class SupplierControl : System.Web.UI.UserControl
    {
        protected string NoticeHTML = "valid=\"required\"errmsg=\"选用不能为空！\"";
        /// <summary>
        /// 选用类型
        /// </summary>
        private EyouSoft.Model.EnumType.PlanStructure.PlanProject _supplierType;
        public EyouSoft.Model.EnumType.PlanStructure.PlanProject SupplierType
        {
            get { return _supplierType; }
            set { _supplierType = value; }
        }
        /// <summary>
        /// 单选设置 1，多选设置为2;默认单选
        /// </summary>
        private int _sMode;
        /// <summary>
        /// 单选设置 1，多选设置为2;默认单选
        /// </summary>
        public int SMode
        {
            get { return _sMode; }
            set { _sMode = value; }
        }
        private int _flag = 0;
        /// <summary>
        /// 标识供应商或者资源预控(0:供应商 1：资源预控 默认为0)
        /// </summary>
        public int Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }
        private string _hideID_zyyk;
        /// <summary>
        /// 资源预控ID
        /// </summary>
        public string HideID_zyyk
        {
            get { return _hideID_zyyk; }
            set { _hideID_zyyk = value; }
        }
        /// <summary>
        /// ID
        /// </summary>
        private string _hideID;
        /// <summary>
        /// 供应商ID
        /// </summary>
        public string HideID
        {
            get { return _hideID; }
            set { _hideID = value; }
        }
        /// <summary>
        /// 选用名称
        /// </summary>
        private string _name;
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 是否必选默认值
        /// </summary>
        private bool _ismust = true;
        /// <summary>
        /// 获取或设置是否必选（默认：true）
        /// </summary>
        public bool IsMust
        {
            get { return _ismust; }
            set { _ismust = value; }
        }
        /// <summary>
        /// 获取或设置回调函数方法名
        /// </summary>
        public string CallBack { get; set; }
        /// <summary>
        /// 是否从预控中选用(资源预控下flag为1，如果在有资源预控（酒店，游轮，车队）下选择的是从供应商选用的话就给此属性0。)
        /// </summary>
        private string _isyukong = "0";
        public string isyukong
        {
            get { return _isyukong; }
            set { _isyukong = value; }
        }
        /// <summary>
        /// 获取IframeID
        /// </summary>
        protected string IframeID
        {
            get { return Utils.GetQueryStringValue("iframeId"); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (IsPostBack)
            {
                //this.Name = Utils.GetFormValue("txtShowID_djs");
                //this.HideID = Utils.GetFormValue("hideID_djs");
                //this.Flag = Utils.GetInt(Utils.GetFormValue("flag"));
                //this.HideID_zyyk = Utils.GetInt(Utils.GetFormValue("hideID_zyyk"));
                base.OnLoad(e);
            }
        }
        private string _clienttext;
        /// <summary>
        /// 获取供应商（资源预控）名称input name
        /// </summary>
        public string ClientText { get { return "txt_" + this.ClientID + "_Name"; } set { _clienttext = value; } }

        private string _clientvalue;
        /// <summary>
        /// 获取供应商input value
        /// </summary>
        public string ClientValue { get { return "hd_" + this.ClientID + "_ID"; } set { _clientvalue = value; } }

        private string _zyykvalue;
        /// <summary>
        /// 获取资源预控编号
        /// </summary>
        public string ClientzyykValue { get { return "hd_" + this.ClientID + "_zyykID"; } set { _zyykvalue = value; } }
        private string _clienttype;
        /// <summary>
        /// 获取供应商类型input type
        /// </summary>
        public string ClientType { get { return "hd_" + this.ClientID + "_Type"; } set { _clienttype = value; } }
        private string _clientflag;
        /// <summary>
        /// 获取供应商类型input flag
        /// </summary>
        public string ClientFlag { get { return "hd_" + this.ClientID + "_Flag"; } set { _clientflag = value; } }
        private string _clientisyukong;
        /// <summary>
        /// 用来判断是资源预控还是供应商
        /// </summary>
        public string ClientIsyukong { get { return "hd_" + this.ClientID + "yukong"; } set { _clientisyukong = value; } }

        /// <summary>
        /// 获取选用按钮ID
        /// </summary>
        public string btnID { get { return "btn_" + this.ClientID + "_ID"; } }

        private string _tourid;
        /// <summary>
        /// 团队编号(预控资源选用使用)
        /// </summary>
        public string TourID { get { return _tourid; } set { _tourid = value; } }
    }
}