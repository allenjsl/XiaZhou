using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;


namespace EyouSoft.Web.SystemSet
{
    /// <summary>
    /// 编辑车型
    /// 创建人：赵晓慧
    /// 创建时间：2012-08-13
    /// </summary>
    public partial class CarTypeEdit : BackPage
    {
        /// <summary>
        /// 用来给页面传值的模板编号
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            if (Utils.GetQueryStringValue("dotype") == "save")
            {
                Save();
            }
            if (Utils.GetQueryStringValue("dotype") == "gettemplateid")
            {
                GetTemplateId();
            }
            PageInit();
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
                EyouSoft.Model.ComStructure.MComCarType model = new EyouSoft.Model.ComStructure.MComCarType();
                model.CarTypeId = Id;
                model.CarTypeName = Utils.GetFormValue(txtCarType.UniqueID);
                model.SeatNum = Utils.GetInt(Utils.GetQueryStringValue("ddlCarSeatNum"));
                model.TemplateId = Utils.GetFormValue(this.templateId.UniqueID);
                model.Desc = Utils.GetFormValue(txtDescribe.UniqueID);
                model.CompanyId = CurrentUserCompanyID;
                model.Operator = SiteUserInfo.Username;
                model.OperatorId = SiteUserInfo.UserId;

                if (string.IsNullOrEmpty(Id))
                {
                    if (new EyouSoft.BLL.ComStructure.BComCarType().AddComCarType(model))
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("1", "添加成功！"));
                    }
                    else
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("0", "添加失败！"));
                    }
                }
                else
                {
                    int updatestate = new EyouSoft.BLL.ComStructure.BComCarType().UpdateComCarType(model);
                    switch (updatestate)
                    {
                        case 1:
                            AjaxResponse(UtilsCommons.AjaxReturnJson("1", "修改成功！"));
                            break;
                        case 2:
                            AjaxResponse(UtilsCommons.AjaxReturnJson("0", "该车型已被使用,无法修改!"));
                            break;
                        default:
                            AjaxResponse(UtilsCommons.AjaxReturnJson("0", "修改失败！"));
                            break;
                    }
                }

        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            IList<EyouSoft.Model.SysStructure.MSysCarTypeSeatNum> seatnumlist = new EyouSoft.BLL.SysStructure.BSysCarType().GetCarTypeSeatNumList();
            if (seatnumlist != null && seatnumlist.Count > 0)
            {
                this.ddlCarSeatNum.DataTextField = "SeatNum";
                this.ddlCarSeatNum.DataValueField = "Id";
                this.ddlCarSeatNum.DataSource = seatnumlist;
                this.ddlCarSeatNum.DataBind();
            }
            if (!string.IsNullOrEmpty(Id))
            {
                EyouSoft.Model.ComStructure.MComCarType model = new EyouSoft.BLL.ComStructure.BComCarType().GetModel(Id);
                this.txtCarType.Text = model.CarTypeName;
                this.txtDescribe.Text = model.Desc;
                this.templateId.Value = model.TemplateId;
                this.oldseatnum.Value = model.SeatNum.ToString();

                IList<EyouSoft.Model.SysStructure.MSysCarTypeSeatNum> list = new EyouSoft.BLL.SysStructure.BSysCarType().GetCarTypeSeatNumList();
                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        if (item.SeatNum.ToString() == model.SeatNum.ToString())
                        {
                            this.ddlCarSeatNum.SelectedValue =item.Id.ToString();
                        }
                    }
                }
                
            }
        }
        /// <summary>
        /// 下拉框的联动事件 获取默认的模板编号
        /// </summary>
        private void GetTemplateId()
        {
            string seatnum = Utils.GetQueryStringValue("newseatnum");
            int id = 0;
            IList<EyouSoft.Model.SysStructure.MSysCarTypeSeatNum> seatnumlist = new EyouSoft.BLL.SysStructure.BSysCarType().GetCarTypeSeatNumList();
            if (seatnumlist != null && seatnumlist.Count > 0)
            {
                foreach (var item in seatnumlist)
                {
                    if (item.SeatNum.ToString() == seatnum)
                    {
                        id = item.Id;
                    }
                }
            }
            IList<EyouSoft.Model.SysStructure.MSysCarType> list = new EyouSoft.BLL.SysStructure.BSysCarType().GetCarTypeList(id);
            AjaxResponse(UtilsCommons.AjaxReturnJson("1","正确",list));
        }

        /// <summary>
        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_车型管理栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_车型管理栏目, false);
                return;
            }
        }

        /// <summary>
        /// 车型编号
        /// </summary>
        public string Id { get { return Utils.GetQueryStringValue("id"); } }
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
