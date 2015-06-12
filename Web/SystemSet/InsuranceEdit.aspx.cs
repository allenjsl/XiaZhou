using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.SystemSet
{
    /// <summary>
    /// 保险修改页
    /// 修改记录:
    /// 1、2012-04-23 曹胡生 创建
    /// </summary>
    public partial class InsuranceEdit : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (Utils.GetQueryStringValue("dotype") == "save")
            {
                Save();
            }
            PageInit();
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var item = new EyouSoft.BLL.ComStructure.BComInsurance().GetModel(Id);
                if (item != null)
                {
                    txtInsName.Text = item.InsuranceName;
                    txtInsPrice.Text = Utils.FilterEndOfTheZeroDecimal(item.UnitPrice);
                }
            }
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_保险类型栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_保险类型栏目, false);
                return;
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
        /// 保险编号
        /// </summary>
        public string Id { get { return Utils.GetQueryStringValue("id"); } }

        protected void Save()
        {
            string msg = string.Empty;
            string txtinsname = Utils.GetFormValue(txtInsName.UniqueID);
            string txtinsprice = Utils.GetFormValue(txtInsPrice.UniqueID);
            if (string.IsNullOrEmpty(txtinsname))
            {
                msg += "保险名称不能为空!<br/>";
            }
            if (string.IsNullOrEmpty(txtinsprice))
            {
                msg += "单价不能为空!<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }

            EyouSoft.Model.ComStructure.MComInsurance model = new EyouSoft.Model.ComStructure.MComInsurance();
            model.InsuranceName = txtinsname;
            model.CompanyId = CurrentUserCompanyID;
            model.InsuranceId = Id;
            model.UnitPrice = Utils.GetDecimal(txtinsprice);


            if (string.IsNullOrEmpty(Id))
            {
                if (new EyouSoft.BLL.ComStructure.BComInsurance().Add(model))
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("1", "添加成功"));
                }
                else
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("0", "添加失败"));
                }
            }
            else
            {
                if (new EyouSoft.BLL.ComStructure.BComInsurance().Update(model))
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("1", "修改成功"));
                }
                else
                {
                    AjaxResponse(UtilsCommons.AjaxReturnJson("0", "修改失败"));
                }
            }
        }
    }
}
