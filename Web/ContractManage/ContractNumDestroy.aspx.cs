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
    /// 合同管理-合同号段-销号
    /// </summary>
    /// 创建人：邵权江
    /// 创建时间：2011-10-14
    public partial class ContractNumDestroy : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            if (!IsPostBack)
            {
                //根据ID初始化页面
                PageInit();
            }
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            //UtilsCommons.bindDDL(ddlConType, true, EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.ConStructure.ContractType)));
        }

        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave()
        {
            //获取form参数
            string Code = Utils.GetFormValue("txtCode");//合同编码
            int ConType = Utils.GetInt(Utils.GetFormValue("ddlConType"),-1);//合同类型
            int txtNumber = Utils.GetInt(Utils.GetFormValue("txtNumber"));//合同序号
            string errorMsg = "";
            //数据验证开始
            if (string.IsNullOrEmpty(Code.Trim()))
            {
                errorMsg = "合同编码不能为空!";
            }
            if (ConType < 0)
            {
                errorMsg = "合同类型必须选择!";
            }
            if (txtNumber <= 0)
            {
                errorMsg = "合同序号必须大于零!";
            }
            //数据验证结束

            EyouSoft.BLL.ConStructure.BContractNum bll = new EyouSoft.BLL.ConStructure.BContractNum();
            int result = 0;
            if (errorMsg == "")
            {
                //MContractDestroy model = new MContractDestroy();
                //model.Code = Code;
                //model.Index = txtNumber;
                //model.IssueTime = DateTime.Now;
                //model.OperatorId = this.SiteUserInfo.UserId;
                //result = bll.AddContractDestroy(model, ConType);
                if (result == 1)
                {
                    Utils.ShowMsgAndCloseBoxy("销毁合同号成功！", Utils.GetQueryStringValue("IframeId"), true);
                }
                else if (result == 2)
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('销毁合同号失败!合同号已经被销毁！');");
                }
                else if (result == 3)
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('销毁合同号失败!编码或序号不存在！');");
                }
                else
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('销毁合同号失败!请检查输入的合同号');");
                }
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "alert('销毁合同号失败!" + errorMsg + "');");
            }

        }

        //保存
        protected void btnClick(object sender, EventArgs e)
        {
            this.PageSave();
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            //if (!this.CheckGrant(Common.Enum.TravelPermission.合同管理_合同管理_销号))
            //{
            //    Utils.ResponseNoPermit(Common.Enum.TravelPermission.合同管理_合同管理_销号, false);
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
