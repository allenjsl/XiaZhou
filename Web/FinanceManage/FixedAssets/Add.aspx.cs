using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.FinStructure;
using EyouSoft.BLL.FinStructure;


namespace Web.FinanceManage.FixedAssets
{
    /// <summary>
    /// 固定资产-添加or修改-
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-13
    public partial class Add : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Utils.GetFormValue("doType") == "Save")
            {
                Save();
            }

            //权限验证
            PowerControl();
            //根据ID初始化页面
            PageInit();
        }


        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit()
        {
            SellsSelect1.SetTitle = "管理责任人";
            SellsSelect1.CallBackFun = "FixedAssets.GetAdminVal";
            SelectSection1.SetTitle = "使用部门";
            int id = Utils.GetInt(Utils.GetQueryStringValue("id"));
            if (id > 0)
            {
                MAsset model = new BFinance().GetAsset(id);
                if (model != null)
                {
                    //编号
                    txt_Id.Text = model.AssetCode;
                    //资产名称
                    txt_Name.Text = model.AssetName;
                    //购买时间
                    txt_purchaseDate.Text = UtilsCommons.GetDateString(model.BuyTime, ProviderToDate);
                    SellsSelect1.SellsName = model.Admin;
                    SellsSelect1.SellsID = model.AdminId;
                    //使用部门名称
                    SelectSection1.SectionName = model.Department;
                    //使用部门Id
                    SelectSection1.SectionID = model.DepartmentId.ToString();
                    //原始价值
                    txt_cost.Text = Utils.FilterEndOfTheZeroDecimal(model.BuyPrice);
                    //折旧年限
                    txt_depreciationDateY.Text = model.DepreciableLife.ToString();
                    //备注
                    txt_Remark.Text = model.Remark;
                }
            }
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_固定资产_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.财务管理_固定资产_栏目, false);
            }
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
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            int id = Utils.GetInt(Utils.GetQueryStringValue("id"));
            BFinance bll = new BFinance();
            MAsset model = id > 0 ? bll.GetAsset(id) : new MAsset();
            string msg = string.Empty;
            if (GetPageVal(model, ref msg))
            {
                PageResponse(UtilsCommons.AjaxReturnJson((id > 0 ? bll.UpdAsset(model) : bll.AddAsset(model)) ? "1" : "-1", model.Id > 0 ? "修改失败!" : "保存失败!"));
            }
            else
            {
                PageResponse(UtilsCommons.AjaxReturnJson("-1", msg));
            }

        }
        /// <summary>
        /// 获取页面数据
        /// </summary>
        /// <param name="model">固定资产实体</param>
        /// <param name="msg">返回验证结果</param>
        /// <returns></returns>
        private bool GetPageVal(MAsset model, ref string msg)
        {
            if (model != null)
            {
                //资产ID
                model.AssetCode = Utils.InputText(Utils.GetFormValue(txt_Id.ClientID));
                msg += model.AssetCode.Length > 0 ? string.Empty : "编号不能为空！<br/>";
                //资产名称
                model.AssetName = Utils.InputText(Utils.GetFormValue(txt_Name.ClientID));
                msg += model.AssetName.Length > 0 ? string.Empty : "资产名称不能为空！<br/>";
                //购买时间
                model.BuyTime = Utils.GetDateTime(txt_purchaseDate.Text);
                msg += model.BuyTime != DateTime.MinValue ? string.Empty : "请填写正确的购买时间！<br/>";
                //部门名称
                model.Department = Utils.GetFormValue(SelectSection1.SelectNameClient);
                //使用部门
                model.DepartmentId = Utils.GetInt(Utils.GetFormValue(SelectSection1.SelectIDClient));
                msg += model.DepartmentId > 0 && model.Department.Length > 0 ? string.Empty : "请选择部门！<br/>";
                //原有价值
                model.BuyPrice = Utils.GetDecimal(Utils.GetFormValue(txt_cost.ClientID));
                msg += model.BuyPrice > 0 ? string.Empty : "原始价值不能为空！<br/>";
                //折旧年限
                model.DepreciableLife = Utils.GetDecimal(Utils.GetFormValue(txt_depreciationDateY.ClientID));
                msg += model.DepreciableLife > 0 ? string.Empty : "折旧年限不能为空！<br/>";
                model.Admin = Utils.GetFormValue(SellsSelect1.SellsNameClient);
                model.AdminId = Utils.GetFormValue(SellsSelect1.SellsIDClient);
                model.AdminDeptId = Utils.GetInt(Utils.GetFormValue("AdminDeptID"));
                //公司编号
                model.CompanyId = CurrentUserCompanyID;
                //操作时间
                model.IssueTime = DateTime.Now;
                //操作人
                model.OperatorId = SiteUserInfo.UserId;
                model.Operator = SiteUserInfo.Name;
                //备注
                model.Remark = Utils.InputText(Utils.GetFormValue(txt_Remark.ClientID));
                return msg.Length <= 0;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 页面返回
        /// </summary>
        /// <param name="ret"></param>
        private void PageResponse(string ret)
        {
            Response.Clear();
            Response.Write(ret);
            Response.End();
        }
    }
}
