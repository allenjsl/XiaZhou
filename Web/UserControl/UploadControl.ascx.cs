using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.UserControl
{
    public partial class UploadControl : System.Web.UI.UserControl
    {

        public string ClientHideID
        {
            get { return this.ClientID + "hidFileName"; }
        }

        private string _companyID;
        /// <summary>
        /// 当前登录公司编号
        /// </summary>
        public string CompanyID
        {
            get { return _companyID; }
            set { _companyID = value; }
        }

        private bool _isUploadMore = false;
        /// <summary>
        /// 是否可以选择多个文件，默认1个
        /// </summary>
        public bool IsUploadMore
        {
            get { return _isUploadMore; }
            set { _isUploadMore = value; }
        }

        private bool _isUploadSelf = false;
        /// <summary>
        /// 是否自动上传
        /// </summary>
        public bool IsUploadSelf 
        {
            get { return _isUploadSelf; }
            set { _isUploadSelf = value; }
        }

        private string _fileTypes = "*.xls;*.rar;*.pdf;*.doc;*.swf;*.jpg;*.gif;*.jpeg;*.png";
        /// <summary>
        /// 设置可上传文件格式 默认: *.xls;*.rar;*.pdf;*.doc;*.swf;*.jpg;*.gif;*.jpeg;*.png;
        /// </summary>
        public string FileTypes
        {
            get { return _fileTypes; }
            set { _fileTypes = value; }
        }

        public string _P1 = string.Empty;
        /// <summary>
        /// 游轮设置成YL 其它默认
        /// </summary>
        public string P1
        {
            get { return _P1; }
            set { _P1 = value; }
        }

        protected string _ImgUrl = "/images/swfupload/XPButtonNoText_178_34.gif";

        public int YangShi
        {
            set
            {
                switch (value)
                {
                    case 1: _ImgUrl = "/images/swfupload/XPButtonNoText_178_34_01.gif"; break;
                    default: break;
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.CompanyID))
            {
                //throw new Exception("UploadControl控件的CompanyID未赋值");
                var domain = EyouSoft.Security.Membership.UserProvider.GetDomain();
                CompanyID = domain.CompanyId;
            }
        }
    }
}