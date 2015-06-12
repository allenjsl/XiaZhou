using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Model.TourStructure;
using System.Text;

namespace Web.UserControl
{
    /// <summary>
    /// 对外报价
    /// 编辑人：DYZ
    /// </summary>
    public partial class ForeignQuote : System.Web.UI.UserControl
    {
        /// <summary>
        /// 是否为整团
        /// </summary>
        public bool IsTourOrSubentry = false;

        /// <summary>
        /// 整团报价 服务标准
        /// </summary>
        public string GroupService = string.Empty;

        /// <summary>
        /// 设置
        /// </summary>
        public bool IsFromTourOrRoute = true;

        public IList<EyouSoft.Model.SourceStructure.MRouteStandard> StandardRouteList;

        public IList<EyouSoft.Model.TourStructure.MTourTeamPrice> StandardTourList;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (StandardRouteList != null && StandardRouteList.Count > 0)
                {
                    this.rptListRoute.DataSource = this.StandardRouteList;
                    this.rptListRoute.DataBind();
                    this.phrPanel.Visible = false;
                }
                if (StandardTourList != null && StandardTourList.Count > 0)
                {
                    this.rptListTour.DataSource = this.StandardTourList;
                    this.rptListTour.DataBind();
                    this.phrPanel.Visible = false;
                }
            }
        }


        /// <summary>
        /// 生成带option的计调项目
        /// </summary>
        /// <param name="selectValue"></param>
        /// <returns></returns>
        protected string GetOptionByPlanProject(string selectValue)
        {
            StringBuilder sb = new StringBuilder();
            IList<EnumObj> list = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.ComStructure.ContainProjectType));
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    EyouSoft.Model.EnumType.ComStructure.ContainProjectType model = (EyouSoft.Model.EnumType.ComStructure.ContainProjectType)Utils.GetInt(list[i].Value);
                    //if (model == EyouSoft.Model.EnumType.ComStructure.ContainProjectType.用车 || model == EyouSoft.Model.EnumType.ComStructure.ContainProjectType.导服)
                    //{
                    //    sb.AppendFormat("<option {0} value='" + list[i].Value + "|2'>" + list[i].Text + "</option>", list[i].Value == selectValue ? "selected=selected" : "");
                    //}
                    //else if (model == EyouSoft.Model.EnumType.ComStructure.ContainProjectType.其它)
                    //{
                    //    sb.AppendFormat("<option {0} value='" + list[i].Value + "|1'>" + list[i].Text + "</option>", list[i].Value == selectValue ? "selected=selected" : "");
                    //}
                    //else
                    //{
                    //    sb.AppendFormat("<option {0} value='" + list[i].Value + "|0'>" + list[i].Text + "</option>", list[i].Value == selectValue ? "selected=selected" : "");
                    //}

                    sb.AppendFormat("<option {0} value='" + list[i].Value + "|2'>" + list[i].Text + "</option>", list[i].Value == selectValue ? "selected=selected" : "");
                }
            }
            return sb.ToString();
        }

       

    }
}