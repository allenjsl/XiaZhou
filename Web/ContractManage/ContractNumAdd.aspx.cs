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
    /// 合同管理-合同号段-添加
    /// </summary>
    /// 创建人：邵权江
    /// 创建时间：2011-10-13
    public partial class ContractNumAdd : BackPage
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
            //合同号类型
            //UtilsCommons.bindDDL(ddlConType, true, EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.ConStructure.ContractType)));
        }

        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave()
        {
            //获取form参数
            string Code = Utils.GetFormValue("txtCode");//合同编码
            int ConType = Utils.GetInt(Utils.GetFormValue("ddlConType"), -1);//合同类型
            string errorMsg = "";
            //数据验证开始
            if (string.IsNullOrEmpty(Code.Trim()))
            {
                errorMsg = "合同编码不能为空!";
            }
            if (ConType<0)
            {
                errorMsg = "合同类型必须选择!";
            }
            //数据验证结束
            
            EyouSoft.BLL.ConStructure.BContractNum bll = new EyouSoft.BLL.ConStructure.BContractNum();
            int result = 0;
            //string errorMsg = "";
            
            //开始号
            string[] StartNum = Utils.GetFormValues("txtConStart");
            //结束号
            string[] EndNum = Utils.GetFormValues("txtConEnd");
            if (StartNum.Length > 0 && EndNum.Length > 0 && StartNum.Length == EndNum.Length)
            {
                List<EyouSoft.Model.ConStructure.MContractNum> list = new List<EyouSoft.Model.ConStructure.MContractNum>();
                EyouSoft.Model.ConStructure.MContractNum ConModel = null;
                for (int i = 0; i < StartNum.Length; i++)
                {
                    if (StartNum[i].Trim() == "" || EndNum[i].Trim() == "")
                    {
                        errorMsg += "第" + (i+1).ToString() + "行开始号或结束号不能为空! &nbsp;";
                    }
                    else
                    {
                        if (Utils.GetInt(StartNum[i].Trim()) >= Utils.GetInt(EndNum[i].Trim()))
                        {
                            errorMsg += "第" + (i + 1).ToString() + "行开始号大于或等于结束号! &nbsp;";
                        }
                        else
                        {
                            if (i > 0 && Utils.GetInt(EndNum[i - 1].Trim()) >= Utils.GetInt(EndNum[i].Trim()))
                            {
                                errorMsg += "第" + (i + 1).ToString() + "行号码段与第" + (i).ToString() + "行号码段有重合或小于! &nbsp;";
                            }
                            //ConModel = new EyouSoft.Model.ConStructure.MContractNum();
                            //ConModel.Code = Code;
                            //ConModel.CompanyId = this.SiteUserInfo.CompanyId;
                            //ConModel.ContractType = (EyouSoft.Model.EnumType.ConStructure.ContractType)ConType;
                            //ConModel.StartNum = Utils.GetInt(StartNum[i].Trim());
                            //ConModel.EndNum = Utils.GetInt(EndNum[i].Trim());
                            //ConModel.IssueTime = DateTime.Now;
                            //ConModel.OperatorId = this.SiteUserInfo.UserId;
                            list.Add(ConModel);
                        }
                    }
                    ConModel = null;
                }
                if (list.Count > 0 && errorMsg == "")
                {
                    result = bll.AddContractNum(list);
                    if (result == 1)
                    {
                        Utils.ShowMsgAndCloseBoxy("登记合同号成功！", Utils.GetQueryStringValue("IframeId"), true);
                        //EyouSoft.Common.Function.MessageBox.ResponseScript(this, "tableToolbar._showMsg('登记合同号成功！');");
                    }
                    else if (result > 1)
                    {
                        EyouSoft.Common.Function.MessageBox.ResponseScript(this, "tableToolbar._showMsg('登记合同号段失败!合同号段有重复！');");
                    }
                    else
                    {
                        EyouSoft.Common.Function.MessageBox.ResponseScript(this, "tableToolbar._showMsg('登记合同号段失败!请检查输入的合同号段');");
                    }
                }
                else
                {
                    EyouSoft.Common.Function.MessageBox.ResponseScript(this, "tableToolbar._showMsg('登记合同号段失败!" + errorMsg + "');");
                }
                
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, "tableToolbar._showMsg('登记合同号段失败!请检查输入的合同号段');");
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
            //if (!this.CheckGrant(Common.Enum.TravelPermission.合同管理_合同管理_登记))
            //{
            //    Utils.ResponseNoPermit(Common.Enum.TravelPermission.合同管理_合同管理_登记, false);
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
