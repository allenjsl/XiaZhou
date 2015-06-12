using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;

namespace Web.CommonPage
{
    /// <summary>
    /// 导游选择
    /// 创建人：lixh 创建时间：2012-03-19
    /// </summary>
    public partial class SelectGuid : EyouSoft.Common.Page.BackPage
    {
        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 8;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            //权限判断
            PowerControl();

            if (!IsPostBack)
            {
                //初始化
                DataInit();
            }
        }

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
            string gener = Utils.GetQueryStringValue("guidsex");
            if (!string.IsNullOrEmpty(gener) && gener != "-1")
            {
                search.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)Utils.GetInt(gener);
            }
            //类别
            string type = Utils.GetQueryStringValue("guidType");
            if (!string.IsNullOrEmpty(type) && type != "-1")
            {
                search.GuideCategory = (EyouSoft.Model.EnumType.SourceStructure.GuideCategory)Utils.GetInt(type);
            }
            //级别
            string leavel = Utils.GetQueryStringValue("leavel");
            if (!string.IsNullOrEmpty(leavel) && leavel != "-1")
            {
                search.Level = (EyouSoft.Model.EnumType.SourceStructure.GuideLevel)Utils.GetInt(leavel);
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
                this.litdatamsg.Text = "没有相关数据！";
                this.ExporPageInfoSelect1.Visible = false;
            }
        }

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
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {

        }
        #endregion
    }
}
