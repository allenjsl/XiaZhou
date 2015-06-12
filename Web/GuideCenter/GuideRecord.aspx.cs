using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;

namespace Web.GuideCenter
{
    /// <summary>
    /// 导游中心-导游档案
    /// 创建时间：2011-09-14
    /// 创建人：李晓欢
    /// </summary>
    public partial class GuideRecord : EyouSoft.Common.Page.BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        protected int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        protected int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        protected int recordCount = 0;
        protected int pageCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();

            #region ajax请求
            //删除
            string doType = Utils.GetQueryStringValue("type");
            if (!string.IsNullOrEmpty(doType))
            {
                switch (doType)
                {
                    case "delguid":
                        Response.Clear();
                        Response.Write(DeleteGuid());
                        Response.End();
                        break;
                    default: break;
                }
            }
            #endregion

            //导出
            if (UtilsCommons.IsToXls())
            { ToXls(); }

            DataInit();
        }

        /// <summary>
        /// 导出
        /// </summary>
        protected void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            StringBuilder s = new StringBuilder();

            EyouSoft.Model.SourceStructure.MGuideSearchModel search = new EyouSoft.Model.SourceStructure.MGuideSearchModel();
            //导游姓名
            string guidName = Utils.GetQueryStringValue("txtguidName");
            if (!string.IsNullOrEmpty(guidName))
            {
                search.Name = guidName;
            }
            //性别
            string gener = Utils.GetQueryStringValue("seGender");
            if (!string.IsNullOrEmpty(gener) && gener != "-1")
            {
                search.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.Gender), gener);
            }
            //类别
            string type = Utils.GetQueryStringValue("seType");
            if (!string.IsNullOrEmpty(type) && type != "-1")
            {
                search.GuideCategory = (EyouSoft.Model.EnumType.SourceStructure.GuideCategory)Enum.Parse(typeof(EyouSoft.Model.EnumType.SourceStructure.GuideCategory), type);
            }
            //级别
            string leavel = Utils.GetQueryStringValue("seLevel");
            if (!string.IsNullOrEmpty(leavel) && leavel != "-1")
            {
                search.Level = (EyouSoft.Model.EnumType.SourceStructure.GuideLevel)Enum.Parse(typeof(EyouSoft.Model.EnumType.SourceStructure.GuideLevel), leavel);
            }
            //语种
            string language = Utils.GetQueryStringValue("txtguidlanguage");
            if (!string.IsNullOrEmpty(language))
            {
                search.Language = language;
            }

            IList<EyouSoft.Model.SourceStructure.MGuideListModel> userList = new EyouSoft.BLL.SourceStructure.BSource().GetGuideShowModel(search, this.SiteUserInfo.CompanyId, pageIndex, pageSize, ref recordCount);

            if (userList != null && userList.Count > 0)
            {
                s.Append("<table id='liststyle' width='100%'>");
                s.Append("<tr><td align='center' class='th-line'>姓名</td>");
                s.Append("<td align='center'  class='th-line'>性别</td>");
                s.Append("<td align='center'  class='th-line'>类别</td>");
                s.Append("<td align='center'  class='th-line'>级别</td>");
                s.Append("<td align='center'  class='th-line'>语种</td>");
                s.Append("<td align='center'  class='th-line'>手机</td>");
                s.Append("<td align='center'  class='th-line'>带团次数</td>");
                s.Append("<td align='center'  class='th-line'>带团天数</td>");
                s.Append("<td align='center'  class='th-line'>挂靠单位</td>");
                s.Append("<td align='center'  class='th-line'>擅长路线</td>");
                s.Append("<td align='center'  class='th-line'>年审状态</td></tr>");
                foreach (var item in userList)
                {
                    s.Append("<tr><td align='center' >" + item.Name + "</td>");
                    s.Append("<td align='center'>" + item.Gender.ToString() + "</td>");
                    string str = string.Empty;
                    if (item.SourceGuideCategoryList != null && item.SourceGuideCategoryList.Count > 0)
                    {
                        for (int i = 0; i < item.SourceGuideCategoryList.Count; i++)
                        {
                            if (i == item.SourceGuideCategoryList.Count - 1)
                            {
                                str += item.SourceGuideCategoryList[i].GuideCategory;
                            }
                            else
                            {
                                str += item.SourceGuideCategoryList[i].GuideCategory + ",";
                            }
                        }
                    }
                    s.Append("<td align='center'>" + str + "</td>");
                    s.Append("<td align='center'>" + item.Level + "</td>");
                    s.Append("<td align='center'>" + item.Language + "</td>");
                    s.Append("<td align='center'>" + item.Mobile + "</td>");
                    s.Append("<td align='center'>" + item.Counts + "</td>");
                    s.Append("<td align='center'>" + item.Days + "</td>");
                    s.Append("<td align='center'>" + item.AnchoredCom.ToString() + "</td>");
                    s.Append("<td align='center'>" + item.TourExperience + "</td>");
                    s.Append("<td align='center'>" + (item.AnnualReview == true ? "已审" : "未审") + "</td>");
                    s.Append("</tr>");
                }
                s.Append("</table>");
            }
            ResponseToXls(s.ToString());
        }

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="guidID">导游编号</param>
        /// <returns></returns>
        protected string DeleteGuid()
        {
            string msg = string.Empty;
            string[] stridList = Utils.GetQueryStringValue("idlist").Split(',');
            if (stridList.Length > 0)
            {
                int r = 0;
                r = new EyouSoft.BLL.SourceStructure.BSource().DeleteGuideModel(stridList);

                if (r == 1)
                {

                    msg = UtilsCommons.AjaxReturnJson("1", "删除成功！");
                }
                else if (r == 2)
                {
                    msg = UtilsCommons.AjaxReturnJson("0", "导游有安排，不能删除!");
                }
                else
                {
                    msg = UtilsCommons.AjaxReturnJson("0", " 删除失败!");
                }
            }
            return msg;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            EyouSoft.Model.SourceStructure.MGuideSearchModel search = new EyouSoft.Model.SourceStructure.MGuideSearchModel();
            //导游姓名
            string guidName = Utils.GetQueryStringValue("txtguidName");
            if (!string.IsNullOrEmpty(guidName))
            {
                search.Name = guidName;
            }
            //性别
            string gener = Utils.GetQueryStringValue("seGender");
            if (!string.IsNullOrEmpty(gener) && gener != "-1")
            {
                search.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.Gender), gener);
            }
            //类别
            string type = Utils.GetQueryStringValue("seType");
            if (!string.IsNullOrEmpty(type) && type != "-1")
            {
                search.GuideCategory = (EyouSoft.Model.EnumType.SourceStructure.GuideCategory)Enum.Parse(typeof(EyouSoft.Model.EnumType.SourceStructure.GuideCategory), type);
            }
            //级别
            string leavel = Utils.GetQueryStringValue("seLevel");
            if (!string.IsNullOrEmpty(leavel) && leavel != "-1")
            {
                search.Level = (EyouSoft.Model.EnumType.SourceStructure.GuideLevel)Enum.Parse(typeof(EyouSoft.Model.EnumType.SourceStructure.GuideLevel), leavel);
            }
            //语种
            string language = Utils.GetQueryStringValue("txtguidlanguage");
            if (!string.IsNullOrEmpty(language))
            {
                search.Language = language;
            }

            IList<EyouSoft.Model.SourceStructure.MGuideListModel> userList = new EyouSoft.BLL.SourceStructure.BSource().GetGuideShowModel(search, this.SiteUserInfo.CompanyId, pageIndex, pageSize, ref recordCount);
            if (userList != null && userList.Count > 0)
            {
                this.replist.DataSource = userList;
                this.replist.DataBind();
                BindPage();
            }
            else
            {
                this.litdatamsg.Text = "<tr><td align='center' colspan='12'>没有相关数据!</td></tr>";
                this.ExporPageInfoSelect1.Visible = false;
                this.ExporPageInfoSelect2.Visible = false;
            }
        }

        /// <summary>
        /// 导游类型
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetGuidType(object o)
        {
            System.Text.StringBuilder typeHtml = new StringBuilder();
            IList<EyouSoft.Model.SourceStructure.MSourceGuideCategory> GuideCategory = (IList<EyouSoft.Model.SourceStructure.MSourceGuideCategory>)o;
            if (GuideCategory != null)
            {
                for (int i = 0; i < GuideCategory.Count; i++)
                {
                    typeHtml.Append(GuideCategory[i].GuideCategory + ",");
                }
            }
            return typeHtml.ToString().Length > 0 ? typeHtml.ToString().Substring(0, typeHtml.Length - 1) : "";
        }

        #region 性别
        /// <summary>
        /// 性别
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected string GetGender(string index)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append("<option value='-1'>--请选择--</option>");
            List<EnumObj> Gender = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender));
            if (string.IsNullOrEmpty(index))
            {
                index = "3";
            }
            if (Gender != null && Gender.Count > 0)
            {
                for (int i = 0; i < Gender.Count; i++)
                {
                    if (Gender[i].Value == index)
                    {
                        sb.Append("<option selected='selected' value='" + Gender[i].Value + "'>" + Gender[i].Text + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value='" + Gender[i].Value + "'>" + Gender[i].Text + "</option>");
                    }
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 等级
        protected string GetGuidLeavel(string index)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append("<option value='-1'>--请选择--</option>");
            List<EnumObj> leavel = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SourceStructure.GuideLevel));
            if (leavel != null && leavel.Count > 0)
            {
                for (int i = 0; i < leavel.Count; i++)
                {
                    if (leavel[i].Value == index)
                    {
                        sb.Append("<option selected='selected' value='" + leavel[i].Value + "'>" + leavel[i].Text + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value='" + leavel[i].Value + "'>" + leavel[i].Text + "</option>");
                    }
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 导游类型
        protected string GetGuidType(string index)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append("<option value='-1'>--请选择--</option>");
            List<EnumObj> typelist = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SourceStructure.GuideCategory));
            if (typelist != null && typelist.Count > 0)
            {
                for (int i = 0; i < typelist.Count; i++)
                {
                    if (typelist[i].Value == index)
                    {
                        sb.Append("<option selected='selected' value='" + typelist[i].Value + "'>" + typelist[i].Text + "</option>");
                    }
                    else
                    {
                        sb.Append("<option  value='" + typelist[i].Value + "'>" + typelist[i].Text + "</option>");
                    }
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 分页
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
            this.ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect2.intPageSize = pageSize;
            this.ExporPageInfoSelect2.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect2.intRecordCount = recordCount;
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游档案_栏目))
            {
                if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游档案_新增))
                {
                    this.add.Visible = false;
                }
                if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游档案_修改))
                {
                    this.update.Visible = false;
                }
                if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游档案_删除))
                {
                    this.delete.Visible = false;
                }
            }
            else
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游档案_栏目, false);
                return;
            }
        }
        #endregion

        #endregion
    }
}
