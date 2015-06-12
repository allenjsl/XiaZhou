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
    /// 公司员工选择
    /// </summary>
    /// 创建人：徐从栎
    /// 创建时间：2011-9-19
    public partial class SelectPeople : System.Web.UI.UserControl
    {
        protected string getTxtShowID = string.Empty;
        /// <summary>
        /// 员工ID
        /// </summary>
        public string userID { get; set; }
        /// <summary>
        /// 员工名
        /// </summary>
        public string userName { get; set; }
        private bool isValid=true;
        private string valid = "required";
        private string errMsg = "*";
        /// <summary>
        /// 是否启用验证，true：启用 ;false：不启用
        /// </summary>
        public bool IsValid {
            get { return isValid; }
            set { isValid = value; }
        }
        /// <summary>
        /// 验证规则
        /// </summary>
        public string Valid {
            get { return valid; }
            set { valid = value; }
        }
        /// <summary>
        /// 验证消息提示
        /// </summary>
        public string ErrMsg {
            get { return errMsg; }
            set { errMsg = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.userID = Utils.GetFormValue("hideID");
                this.userName = Utils.GetFormValue("txtShowID");
                this.txtShowID.Value = this.userName;
            }
            this.getTxtShowID = this.txtShowID.ClientID;
            this.initData();
        }
        protected void initData()
        {
            if (this.isValid)
            {
                this.txtShowID.Attributes.Add("valid", this.valid);
                this.txtShowID.Attributes.Add("errmsg", this.errMsg);
            }
            else
            {
                this.txtShowID.Attributes.Remove("valid");
            }
        }
    }
}