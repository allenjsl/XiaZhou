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
    /// 计调中心-计调-计调状态用户控件
    /// 创建人：李晓欢
    /// 创建时间：2011-09-14
    /// </summary>
    public partial class OperaterState : System.Web.UI.UserControl
    {
        //取值
        private EyouSoft.Model.EnumType.PlanStructure.PlanState _getValue;
        public EyouSoft.Model.EnumType.PlanStructure.PlanState GetValue
        {
            get { return _getValue; }
            set { _getValue = value; }
        }

        //赋值
        private EyouSoft.Model.EnumType.PlanStructure.PlanState _setValue;
        public EyouSoft.Model.EnumType.PlanStructure.PlanState SetValue
        {
            get { return _setValue; }
            set { _setValue = value; }
        }

        //验证必填 默认是true
        private bool _IsMust;
        public bool IsMust
        {
            get { return _IsMust; }
            set { _IsMust = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
            this.GetValue = (EyouSoft.Model.EnumType.PlanStructure.PlanState)Utils.GetInt(Utils.GetFormValue(this.ddlstate.UniqueID));
        }

        protected override void OnLoad(EventArgs e)
        {
            GetState();            
            base.OnLoad(e);
        }


        protected void GetState()
        {
            //状态
            this.ddlstate.Items.Clear();
            List<EnumObj> OperaterState = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState));
            if (OperaterState != null && OperaterState.Count > 0)
            {
                for (int i = 2; i < OperaterState.Count; i++)
                {
                    ListItem stateitem = new ListItem();
                    stateitem.Value = OperaterState[i].Value;
                    stateitem.Text = OperaterState[i].Text;
                    this.ddlstate.Items.Add(stateitem);                    
                }
            }
            this.ddlstate.SelectedValue = ((int)this.SetValue).ToString();
        }
    }
}