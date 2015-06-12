using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.ConStructure;
using EyouSoft.BLL.ConStructure;
using EyouSoft.Common.Function;
namespace Web.ContractManage
{
    /// <summary>
    /// 合同管理-合同号段-领用
    /// </summary>
    /// 创建人：邵权江
    /// 创建时间：2011-10-14
    public partial class ContractNumCollar : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            string doType = Request.QueryString["doType"];
            PowerControl();
            if (!IsPostBack)
            {
                //获得操作ID
                string id = Utils.GetQueryStringValue("id");
                //根据ID初始化页面
                PageInit(id, doType);
            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id, string dotype)
        {

        }
        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave(string id,string code)
        {
            //获取form参数
            int Dept = Utils.GetInt(this.SelectSection1.SectionID,-1);//部门
            string CollarId = this.SellsSelect1.SellsID;//领用人
            DateTime? txtCollarTime = Utils.GetDateTimeNullable(Utils.GetFormValue("txtCollarTime"));//领用时间
            int txtConStart = Utils.GetInt(Utils.GetFormValue("txtConStart"));//开始号
            int txtConEnd = Utils.GetInt(Utils.GetFormValue("txtConEnd"));//结束号
            string errorMsg = "";
            //数据验证开始
            if (Dept < 0)
            {
                errorMsg = "部门必须选择!";
            }
            if (string.IsNullOrEmpty(CollarId))
            {
                errorMsg = "领用人必须选择!";
            }
            if (txtCollarTime == null)
            {
                errorMsg = "领用时间必须选择!";
            }
            if (txtConStart <= 0 || txtConEnd <= 0 || txtConStart > txtConEnd)
            {
                errorMsg = "开始号和结束号必须输入大于0的值，并且结束号大于开始号!";
            }
            //数据验证结束
            if (errorMsg != "")
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('" + errorMsg + "');");
            }
            else
            {
                int result = 0;
                EyouSoft.BLL.ConStructure.BContractNum bll = new EyouSoft.BLL.ConStructure.BContractNum();
                EyouSoft.Model.ConStructure.MContractNumCollar Model = new MContractNumCollar();
                Model.ContractId = id;
                //Model.Code = code;
                //Model.StartNum = txtConStart;
                //Model.EndNum = txtConEnd;
                //Model.DepartId = Dept;
                //Model.UseOperatorId = CollarId;
                //Model.OperatorId = this.SiteUserInfo.UserId;
                //Model.IssueTime = txtCollarTime == null ? DateTime.Now : Convert.ToDateTime(txtCollarTime);
                //result = bll.AddContractNumCollar(Model);
                //0添加领取成功：1超出合同号码段范围；2已存在领用号码段部分;3添加失败
                if (result == 0)
                {
                    Utils.ShowMsgAndCloseBoxy("合同号领取成功成功！", Utils.GetQueryStringValue("IframeId"), true);
                }
                else if (result == 1)
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('超出合同号码段范围！');");
                }
                else if (result == 2)
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('已存在领用号码段部分！');");
                }
                else
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('领用号码段失败！');");
                }
            }
        }
        protected void btnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Utils.GetQueryStringValue("id")) && !string.IsNullOrEmpty(Utils.GetQueryStringValue("code")))
            {
                this.PageSave(Utils.GetQueryStringValue("id"), Utils.GetQueryStringValue("code"));
            }
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            //if (!this.CheckGrant(Common.Enum.TravelPermission.合同管理_合同管理_领用))
            //{
            //    Utils.ResponseNoPermit(Common.Enum.TravelPermission.合同管理_合同管理_领用, false);
            //    return;
            //}
        }
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
    }
}
