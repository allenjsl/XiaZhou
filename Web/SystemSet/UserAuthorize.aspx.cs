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
    public partial class UserAuthorize : BackPage
    {
        //权限html
        protected string PowerStr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();

            if (!IsPostBack)
            {
                PageInit();
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
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit()
        {
            System.Text.StringBuilder RolePower = new System.Text.StringBuilder();
            IList<EyouSoft.Model.ComStructure.MComRole> RoleList = new EyouSoft.BLL.ComStructure.BComRole().GetList(SiteUserInfo.CompanyId);
            if (RoleList != null && RoleList.Count > 0)
            {
                RolePower.Append("[");
                foreach (var item in RoleList)
                {
                    RolePower.Append("{");
                    RolePower.AppendFormat("RoleId: '{0}',PowerIds: '{1}'", item.Id, item.RoleChilds);
                    RolePower.Append("},");
                }
                RolePower.Append("]");
            }
            hidPower.Value = RolePower.ToString();
            this.ddlRole.DataTextField = "RoleName";
            this.ddlRole.DataValueField = "Id";
            this.ddlRole.DataSource = RoleList;
            this.ddlRole.DataBind();
            this.ddlRole.Items.Insert(0, new ListItem("请选择", "0"));
            if (ddlRole.Items.FindByValue(RoleID) != null)
            {
                ddlRole.Items.FindByValue(RoleID).Selected = true;
            }
            string[] PowerIds = new EyouSoft.BLL.ComStructure.BComUser().GetPrivs(UserID, SiteUserInfo.CompanyId).Split(',');
            if (PowerIds == null || PowerIds[0] == "")
            {
                if (!string.IsNullOrEmpty(RoleID))
                {
                    EyouSoft.Model.ComStructure.MComRole model = new EyouSoft.BLL.ComStructure.BComRole().GetModel(Utils.GetInt(RoleID), SiteUserInfo.CompanyId);
                    if (model != null)
                    {
                        PowerIds = model.RoleChilds.Split(',');
                    }
                }
            }
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            System.Text.StringBuilder Privs = null;
            EyouSoft.BLL.SysStructure.BSysMenu BSysMenuBll = new EyouSoft.BLL.SysStructure.BSysMenu();
            IList<EyouSoft.Model.SysStructure.MComMenu1Info> list = BSysMenuBll.GetComMenus(SiteUserInfo.SysId);
            if (list != null)
            {
                //一级栏目
                for (int i = 0; i < list.Count; i++)
                {
                    str.Append("<table width=\"99%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" align=\"center\" class=\"quanxian_add_bottom\"> <tbody><tr>");
                    str.AppendFormat(" <td width=\"92\" height=\"25\" align=\"center\" class=\"quanxian_add\">{0}</td>", list[i].Name);
                    str.AppendFormat("<td><span>&nbsp;&nbsp;<input type=\"checkbox\" name=\"chkAll\" value=\"{0}\"/></span>全选</td>", list[i].MenuId);
                    str.Append(" </tr></tbody></table>");
                    str.Append("<div class=\"hr_5\"></div>");
                    str.Append(" <table width=\"99%\" cellspacing=\"1\" cellpadding=\"0\" border=\"0\" bgcolor=\"#85c1dd\" align=\"center\"><tbody>");
                    if (list[i].Menu2s != null)
                    {
                        //每行显示4列,共循环多少次
                        for (int k = 0; k <= list[i].Menu2s.Count / 4; k++)
                        {
                            Privs = new System.Text.StringBuilder();
                            str.Append("<tr>");
                            Privs.Append("<tr>");
                            for (int j = k * 4; j < list[i].Menu2s.Count; j++)
                            {
                                //当前权限是否选中
                                bool check = false;
                                str.AppendFormat("<th width=\"25%\" height=\"26\" bgcolor=\"#BDDCF4\" align=\"left\">&nbsp; <input type=\"checkbox\" MenuId =\"{0}\" Menu2Id=\"{1}\" name=\"chkMenu\" />{2}</th>", list[i].MenuId, list[i].Menu2s[j].MenuId, list[i].Menu2s[j].Name);
                                Privs.Append("<td style=\" vertical-align:top;\"><table cellspacing=\"0\" cellpadding=\"0\" border=\"0\">");
                                for (int t = 0; t < list[i].Menu2s[j].Privs.Count; t++)
                                {
                                    for (int p = 0; p < PowerIds.Length; p++)
                                    {
                                        check = false;
                                        if (Utils.GetInt(PowerIds[p]) == list[i].Menu2s[j].Privs[t].PrivsId)
                                        {
                                            check = true;
                                            break;
                                        }
                                    }
                                    Privs.AppendFormat("<tr><td width=\"25%\" height=\"26\" bgcolor=\"#FFFFFF\" align=\"left\">&nbsp;<input type=\"checkbox\"  name=\"chkPower\" Menu2Id=\"{0}\" value=\"{1}\" {2}/>{3}</td></tr>", list[i].Menu2s[j].MenuId, list[i].Menu2s[j].Privs[t].PrivsId, check ? "checked=\"checked\"" : "", list[i].Menu2s[j].Privs[t].Name);
                                }
                                Privs.Append("</table></td>");
                                if ((j != 0 && (j + 1) % 4 == 0) || j + 1 == list[i].Menu2s.Count)
                                {
                                    str.Append("</tr>");
                                    Privs.Append("<tr>");
                                    str.Append(Privs.ToString());
                                    break;
                                }
                            }
                        }
                    }
                    str.Append("</tbody></table><div class=\"hr_5\"></div>");
                    PowerStr = str.ToString();
                }
            }
        }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserID { get { return Utils.GetQueryStringValue("id"); } }

        /// <summary>
        /// 角色编号
        /// </summary>
        public string RoleID { get { return Utils.GetQueryStringValue("roleId"); } }

        /// <summary>
        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            //if (!this.CheckGrant(Common.Enum.TravelPermission.系统设置_用户管理_栏目))
            //{
            //    Utils.ResponseNoPermit(Common.Enum.TravelPermission.系统设置_用户管理_栏目, false);
            //    return;
            //}
            //if (!this.CheckGrant(Common.Enum.TravelPermission.系统设置_用户管理_授权))
            //{
            //    Utils.ResponseNoPermit(Common.Enum.TravelPermission.系统设置_用户管理_授权, false);
            //    return;
            //}
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string powerIds = string.Empty;
            int roleId = Utils.GetInt(this.ddlRole.SelectedValue);
            EyouSoft.BLL.ComStructure.BComUser BComUserBll = new EyouSoft.BLL.ComStructure.BComUser();
            int length = Utils.GetFormValues("chkPower").Length;
            for (int i = 0; i < length; i++)
            {
                powerIds += Utils.GetFormValues("chkPower")[i] + ",";
            }
            if (powerIds != "")
            {
                powerIds = powerIds.Trim(',');
            }

            if (BComUserBll.UpdatePrivs(UserID, SiteUserInfo.CompanyId, roleId, powerIds))
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, string.Format("tableToolbar._showMsg('修改成功');parent.Boxy.getIframeDialog('{0}').hide();window.parent.location.reload();", Utils.GetQueryStringValue("iframeId"), Utils.GetQueryStringValue("sl")));
            }
            else
            {
                EyouSoft.Common.Function.MessageBox.ResponseScript(this, string.Format("tableToolbar._showMsg('修改失败');parent.Boxy.getIframeDialog('{0}').hide();window.parent.location.reload();", Utils.GetQueryStringValue("iframeId"), Utils.GetQueryStringValue("sl")));
            }
        }
    }
}

