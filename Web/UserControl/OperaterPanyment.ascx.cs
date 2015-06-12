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
    /// 计调中心-计调-支付方式用户控件
    /// 创建人：李晓欢
    /// 创建时间：2011-09-14
    /// </summary>
    public partial class OperaterPanyment : System.Web.UI.UserControl
    {
        //赋值
        private EyouSoft.Model.EnumType.PlanStructure.Payment _setValue;
        public EyouSoft.Model.EnumType.PlanStructure.Payment SetValue
        {
            get { return _setValue; }
            set { _setValue = value; }
        }

        //取值
        private EyouSoft.Model.EnumType.PlanStructure.Payment _getValue;
        public EyouSoft.Model.EnumType.PlanStructure.Payment GetValue
        {
            get { return _getValue; }
            set { _getValue = value; }
        }

        //非空验证 默认验证
        private bool _IsMust;
        public bool IsMust
        {
            get { return _IsMust; }
            set { _IsMust = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetpayMent();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (IsPostBack)
            {
                this.GetValue = (EyouSoft.Model.EnumType.PlanStructure.Payment)Utils.GetInt(this.ddlpayment.SelectedValue);
            }
            base.OnLoad(e);
        }

        protected void GetpayMent()
        {
            //支付方式
            this.ddlpayment.Items.Clear();
            List<EnumObj> panyMent = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment));
            if (panyMent.Count > 0 && panyMent != null)
            {
                for (int i = 0; i < panyMent.Count; i++)
                {
                    ListItem ltem = new ListItem();
                    ltem.Value = panyMent[i].Value;
                    ltem.Text = panyMent[i].Text;
                    this.ddlpayment.Items.Add(ltem);                    
                }
            }
            this.ddlpayment.SelectedValue = ((int)this.SetValue).ToString();
        }
    }

}