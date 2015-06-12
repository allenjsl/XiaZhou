using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.SystemSet
{
    /// <summary>
    /// 线路区域编辑
    /// </summary>
    /// 修改记录：
    /// 1、2011-10-9 曹胡生 创建
    public partial class RouterAreaEdit : BackPage
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

        protected void Save()
        {
            string msg = string.Empty;
            string txtareaname = Utils.GetFormValue(txtAreaName.UniqueID);
            string ddlareatype = Utils.GetFormValue(ddlAreaType.UniqueID);
            if (string.IsNullOrEmpty(txtareaname))
            {
                msg += "区域名称不能为空!<br/>";
            }
            if (string.IsNullOrEmpty(ddlareatype))
            {
                msg += "区域类别不能为空!<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }
            EyouSoft.Model.ComStructure.MComArea model = new EyouSoft.Model.ComStructure.MComArea();
            model.AreaName = txtAreaName.Text;
            model.CompanyId = SiteUserInfo.CompanyId;
            model.OperatorId = SiteUserInfo.UserId;
            model.Type = (EyouSoft.Model.EnumType.ComStructure.AreaType)Utils.GetInt(ddlAreaType.SelectedValue);
            string[] sellsArray = Utils.GetFormValue(this.SellsSelect1.SellsIDClient).Split(',');
            IList<EyouSoft.Model.ComStructure.MComAreaPlan> Planer = new List<EyouSoft.Model.ComStructure.MComAreaPlan>();
            for (int i = 0; i < sellsArray.Length; i++)
            {
                if (!string.IsNullOrEmpty(sellsArray[i]))
                {
                    EyouSoft.Model.ComStructure.MComAreaPlan planerModel = new EyouSoft.Model.ComStructure.MComAreaPlan();
                    planerModel.AreaId = AreaId;
                    planerModel.OperatorId = sellsArray[i];
                    Planer.Add(planerModel);
                }
            }
            model.Plan = Planer.Count > 0 ? Planer : null;
            Response.Clear();
            if (AreaId == 0)
            {
                if (new EyouSoft.BLL.ComStructure.BComArea().Add(model)>0)
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("1", "添加成功"));
                }
                else
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("0", "添加失败"));
                }
            }
            else
            {
                model.AreaId = AreaId;
                if (new EyouSoft.BLL.ComStructure.BComArea().Update(model))
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("1", "修改成功"));
                }
                else
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("0", "修改失败"));
                }
            }
            Response.End();
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            System.Collections.Generic.List<EnumObj> list = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.ComStructure.AreaType));
            this.ddlAreaType.DataTextField = "Text";
            this.ddlAreaType.DataValueField = "Value";
            this.ddlAreaType.DataSource = list;
            this.ddlAreaType.DataBind();
            if (AreaId != 0)
            {
                EyouSoft.Model.ComStructure.MComArea model = new EyouSoft.BLL.ComStructure.BComArea().GetModel(AreaId, SiteUserInfo.CompanyId);
                txtAreaName.Text = model.AreaName;
                if (model.Plan != null)
                {
                    foreach (var item in model.Plan)
                    {
                        SellsSelect1.SellsID += item.OperatorId + ",";
                        SellsSelect1.SellsName += item.Planer + ",";
                    }
                    SellsSelect1.SellsID = SellsSelect1.SellsID.Trim(',');
                    SellsSelect1.SellsName = SellsSelect1.SellsName.Trim(',');
                }
                if (ddlAreaType.Items.FindByValue(((int)model.Type).ToString()) != null)
                {
                    ddlAreaType.SelectedValue = ((int)model.Type).ToString();
                }
            }
        }

        /// <summary>
        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_线路区域栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_线路区域栏目, false);
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
        /// 线路区域编号
        /// </summary>
        public int AreaId { get { return Utils.GetInt(Utils.GetQueryStringValue("id")); } }
    }
}
