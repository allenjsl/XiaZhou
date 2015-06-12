using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.PlanStructure;
using EyouSoft.BLL.PlanStructure;
using EyouSoft.Model.EnumType.PlanStructure;

namespace EyouSoft.Web.m
{
    public partial class BianGeng : EyouSoft.Common.Page.MobilePage
    {

        string type = Utils.GetQueryStringValue("type");//变更类型（增减）    
        string planid = Utils.GetQueryStringValue("planid");//计调编号
        BPlan bll = new BPlan();
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (!IsPostBack)
            {
               DateInit();
            }
            else
            {
                Update();
            }

        }

        

        #region 数据初始化
        /// <summary>
        /// 数据初始化
        /// </summary>
        private void DateInit()
        {
            MPlanCostChange costchange = new MPlanCostChange();
            if (type == "add")
            {
                costchange = bll.GetPlanCostChanges(planid, true, PlanChangeChangeClass.导游报账);
            }
            else
            {
                costchange = bll.GetPlanCostChanges(planid, false, PlanChangeChangeClass.导游报账);
            }
            if (costchange != null)
            {
                txtfeiyong.Value=Utils.FilterEndOfTheZeroDecimal(costchange.ChangeCost);
                txtpeople.Value = Utils.GetString(costchange.PeopleNumber.ToString(), "");
                txtbeizhu.Value = Utils.GetString(costchange.Remark, "");
            }
        }
        #endregion


        /// <summary>
        /// 修改
        /// </summary>
        private void Update()
        {
            MPlanCostChange model = new MPlanCostChange();
            model.PeopleNumber = Utils.GetInt(txtpeople.Value, 0);
            model.DNum = Utils.GetDecimal(txtpeople.Value);
            model.ChangeCost = Utils.GetDecimal(txtfeiyong.Value, 0);
            model.Remark = Utils.GetString(txtbeizhu.Value, "");
            model.PlanId = planid;
            model.ChangeType = PlanChangeChangeClass.导游报账;
            model.IssueTime = DateTime.Now;
            if (type == "add")
            {
                model.Type = true;
            }
            else
            {
                model.Type = false;
            }
            if (bll.AddOrUpdPlanCostChange(model))
            {
                this.lblMsg.Text = "保存成功！";
            }
            else
            {
                this.lblMsg.Text = "保存失败！";
            }
            
        }

        /// <summary>
        /// 判断权限
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游报账_栏目))
            {
                Utils.MobileResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游报账_栏目);
                return;
            }
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游报账_导游报账操作))
            {
                Utils.MobileResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游报账_导游报账操作);
                return;
            }
        }
    }
}
