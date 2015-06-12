using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Text.RegularExpressions;

namespace EyouSoft.Web.SystemSet
{
    /// <summary>
    /// 编辑上车地点
    /// 创建人：赵晓慧
    /// 创建时间：2012-08-15
    /// </summary>
    public partial class CarPlaceEdit : BackPage
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
        /// 保存
        /// </summary>
        private void Save()
        {
            EyouSoft.Model.ComStructure.MComCarLocation model = new EyouSoft.Model.ComStructure.MComCarLocation();
            model.CarLocationId = Id;
            model.Location = Utils.GetFormValue(txtCarPlace.UniqueID);
            model.OnPrice = Utils.GetDecimal(Utils.GetFormValue(txtJiePrice.UniqueID));
            model.OffPrice = Utils.GetDecimal(Utils.GetFormValue(txtSongPrice.UniqueID));
            model.Desc = Utils.GetFormValue(txtDescribe.UniqueID);
            model.CompanyId = CurrentUserCompanyID;
            model.Operator = SiteUserInfo.Username;
            model.OperatorId = SiteUserInfo.UserId;
            if (this.rbtnStatus1.Checked == true)
            {
                model.Status = true;
            }
            else
            {
                model.Status = false;
            }
            if (string.IsNullOrEmpty(Id))
            {
                if (new EyouSoft.BLL.ComStructure.BComCarLocation().AddCarLocation(model))
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
                EyouSoft.Model.ComStructure.MComCarLocation m = new EyouSoft.BLL.ComStructure.BComCarLocation().GetModel(Id);
                //只修改状态
                if (IsSame(model.Location, m.Location) && IsSame(Utils.FilterEndOfTheZeroDecimal(model.OnPrice), Utils.FilterEndOfTheZeroDecimal(m.OnPrice)) && IsSame(Utils.FilterEndOfTheZeroDecimal(model.OffPrice), Utils.FilterEndOfTheZeroDecimal(m.OffPrice)) && IsSame(model.Desc, m.Desc) &&  !IsSame(model.Status.ToString(), m.Status.ToString()))
                {
                    if (new EyouSoft.BLL.ComStructure.BComCarLocation().UpdateCarLocation(Id, model.Status))
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("1", "修改成功"));
                    }
                    else
                    {
                        AjaxResponse(UtilsCommons.AjaxReturnJson("0", "修改失败"));
                    }
                }
                //修改其他信息
                else
                {
                    int updatestate = new EyouSoft.BLL.ComStructure.BComCarLocation().UpdateCarLocation(model);
                    switch (updatestate)
                    {
                        case 1:
                            AjaxResponse(UtilsCommons.AjaxReturnJson("1", "修改成功"));
                            break;
                        case 2:
                            AjaxResponse(UtilsCommons.AjaxReturnJson("0", "该地点已被使用，无法修改"));
                            break;
                        default:
                            AjaxResponse(UtilsCommons.AjaxReturnJson("0", "修改失败"));
                            break;
                    }
                }
                
            }
            
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void PageInit()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                EyouSoft.Model.ComStructure.MComCarLocation model = new EyouSoft.BLL.ComStructure.BComCarLocation().GetModel(Id);
                this.txtCarPlace.Text = model.Location;
                this.txtJiePrice.Text = Utils.FilterEndOfTheZeroDecimal(model.OnPrice);
                this.txtSongPrice.Text = Utils.FilterEndOfTheZeroDecimal(model.OffPrice);
                this.txtDescribe.Text = model.Desc;
                if (model.Status)
                {
                    this.rbtnStatus1.Checked = true;
                }
                else
                {
                    this.rbtnStatus0.Checked = true;
                }
            }
        }
        /// <summary>
        /// 判断两个字符串是否一样
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        private bool IsSame(string s1,string s2)
        {
            if (s1==s2)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_上车地点管理栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_上车地点管理栏目, false);
                return;
            }
        }

        /// <summary>
        /// 上车地点编号
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
