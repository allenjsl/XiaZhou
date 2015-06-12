using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.SysCenter
{
    /// <summary>
    /// 短信配置页
    /// 修改记录:
    /// 1、2012-04-17 曹胡生 创建
    /// </summary>
    public partial class SmsSetting : BackPage
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

        protected void PageInit()
        {
            var list = new EyouSoft.BLL.SmsStructure.BSmsSetting().GetSmsSetting(SiteUserInfo.CompanyId);
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    switch (item.Type)
                    {
                        case EyouSoft.Model.EnumType.SmsStructure.SettingType.出团通知:
                            {
                                txtCTContent.Text = item.Message;
                                txtCTDay.Text = item.BeforeDay.ToString();
                                txtCTHour.Text = item.Hour.ToString();
                                chkCTState.Checked = item.IsEnabled;
                                break;
                            }
                        case EyouSoft.Model.EnumType.SmsStructure.SettingType.回团通知:
                            {
                                txtHTContent.Text = item.Message;
                                txtHTDay.Text = item.BeforeDay.ToString();
                                txtHTHour.Text = item.Hour.ToString();
                                chkHTState.Checked = item.IsEnabled;
                                break;
                            }
                        case EyouSoft.Model.EnumType.SmsStructure.SettingType.生日提醒:
                            {
                                txtSRContent.Text = item.Message;
                                txtSRDay.Text = item.BeforeDay.ToString();
                                txtSRHour.Text = item.Hour.ToString();
                                chkSRState.Checked = item.IsEnabled;
                                break;
                            }
                    }
                }
            }
            ddlSelCTLabel.DataTextField = "Text";
            ddlSelCTLabel.DataValueField = "Value";
            ddlSelCTLabel.DataSource = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SmsStructure.SettingTypeCTLabel));
            ddlSelCTLabel.DataBind();

            ddlSelHTLabel.DataTextField = "Text";
            ddlSelHTLabel.DataValueField = "Value";
            ddlSelHTLabel.DataSource = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SmsStructure.SettingTypeHTLabel));
            ddlSelHTLabel.DataBind();

            ddlSelSRLabel.DataTextField = "Text";
            ddlSelSRLabel.DataValueField = "Value";
            ddlSelSRLabel.DataSource = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SmsStructure.SettingTypeSRLabel));
            ddlSelSRLabel.DataBind();
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_短信设置栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.常用工具_短信中心_短信设置栏目, false);
                return;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            string Msg = string.Empty;
            EyouSoft.Model.SmsStructure.MSmsSetting model = new EyouSoft.Model.SmsStructure.MSmsSetting();
            model.CompanyId = CurrentUserCompanyID;
            model.OperatorId = SiteUserInfo.UserId;
            model.IssueTime = DateTime.Now;
            string SaveType = Utils.GetQueryStringValue("SaveType");
            if (SaveType == "1")
            {
                model.Message = Utils.GetFormValue(txtCTContent.UniqueID);
                model.BeforeDay = Utils.GetInt(Utils.GetFormValue(txtCTDay.UniqueID));
                model.Hour = Utils.GetInt(Utils.GetFormValue(txtCTHour.UniqueID));
                model.IsEnabled = chkCTState.Checked;
                model.Type = EyouSoft.Model.EnumType.SmsStructure.SettingType.出团通知;
                if (!string.IsNullOrEmpty(model.Message))
                {
                    Msg += "发送内容不能为空!<br/>";
                }
                if (model.BeforeDay == 0)
                {
                    Msg += "发送时间天数不能为空!<br/>";
                }
                if (model.Hour == 0)
                {
                    Msg += "发送时间小时不能为空!<br/>";
                }
                if (!string.IsNullOrEmpty(Msg))
                {
                    int Result = new EyouSoft.BLL.SmsStructure.BSmsSetting().SetSmsSetting(model);
                    if (Result == 1)
                    {
                        Msg = UtilsCommons.AjaxReturnJson("1", "保存成功！");
                    }
                    else
                    {
                        Msg = UtilsCommons.AjaxReturnJson("0", "保存失败！");
                    }
                }
            }
            else if (SaveType == "2")
            {
                model.Message = Utils.GetFormValue(txtHTContent.UniqueID);
                model.BeforeDay = Utils.GetInt(Utils.GetFormValue(txtHTDay.UniqueID));
                model.Hour = Utils.GetInt(Utils.GetFormValue(txtHTHour.UniqueID));
                model.IsEnabled = chkHTState.Checked;
                model.Type = EyouSoft.Model.EnumType.SmsStructure.SettingType.回团通知;
                if (!string.IsNullOrEmpty(model.Message))
                {
                    Msg += "发送内容不能为空!<br/>";
                }
                if (model.BeforeDay == 0)
                {
                    Msg += "发送时间天数不能为空!<br/>";
                }
                if (model.Hour == 0)
                {
                    Msg += "发送时间小时不能为空!<br/>";
                }
                if (!string.IsNullOrEmpty(Msg))
                {
                    int Result = new EyouSoft.BLL.SmsStructure.BSmsSetting().SetSmsSetting(model);
                    if (Result == 1)
                    {
                        Msg = UtilsCommons.AjaxReturnJson("1", "保存成功！");
                    }
                    else
                    {
                        Msg = UtilsCommons.AjaxReturnJson("0", "保存失败！");
                    }
                }
            }
            else if (SaveType == "3")
            {
                model.Message = Utils.GetFormValue(txtSRContent.UniqueID);
                model.BeforeDay = Utils.GetInt(Utils.GetFormValue(txtSRDay.UniqueID));
                model.Hour = Utils.GetInt(Utils.GetFormValue(txtSRHour.UniqueID));
                model.IsEnabled = chkSRState.Checked;
                model.Type = EyouSoft.Model.EnumType.SmsStructure.SettingType.生日提醒;
                if (!string.IsNullOrEmpty(model.Message))
                {
                    Msg += "发送内容不能为空!<br/>";
                }
                if (model.BeforeDay == 0)
                {
                    Msg += "发送时间天数不能为空!<br/>";
                }
                if (model.Hour == 0)
                {
                    Msg += "发送时间小时不能为空!<br/>";
                }
                if (!string.IsNullOrEmpty(Msg))
                {
                    int Result = new EyouSoft.BLL.SmsStructure.BSmsSetting().SetSmsSetting(model);
                    if (Result == 1)
                    {
                        Msg = UtilsCommons.AjaxReturnJson("1", "保存成功！");
                    }
                    else
                    {
                        Msg = UtilsCommons.AjaxReturnJson("0", "保存失败！");
                    }
                }
            }
            Response.Clear();
            Response.Write(Msg);
            Response.End();
        }
    }
}
