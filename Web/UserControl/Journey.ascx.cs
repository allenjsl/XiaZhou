using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.UserControl
{
    /// <summary>
    /// 行程安排
    /// 修改人：DYZ 创建日期:2011.9.13
    /// </summary>
    public partial class Journey : System.Web.UI.UserControl
    {
        private IList<EyouSoft.Model.TourStructure.MPlanBaseInfo> _setPlanList;
        public IList<EyouSoft.Model.TourStructure.MPlanBaseInfo> SetPlanList
        {
            get { return _setPlanList; }
            set { _setPlanList = value; }
        }

        #region 控件属性
        private bool _isSuppliers = false;
        /// <summary>
        /// 是否供应商平台
        /// </summary>
        public bool IsSuppliers
        {
            get { return _isSuppliers; }
            set { _isSuppliers = value; }
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SetPlanList != null && SetPlanList.Count > 0)
                {
                    this.rptJourney.DataSource = SetPlanList.OrderBy(p => p.Days);
                    this.rptJourney.DataBind();
                }
            }
        }

        /// <summary>
        /// 通过景区的数据返回html
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetSinceByList(object o)
        {
            if (o != null)
            {
                //获得景点集合
                IList<EyouSoft.Model.TourStructure.MTourPlanSpot> list = (IList<EyouSoft.Model.TourStructure.MTourPlanSpot>)o;
                if (list.Count > 0)
                {
                    string val = string.Empty;
                    string text = string.Empty;
                    string html = string.Empty;
                    for (int i = 0; i < list.Count; i++)
                    {
                        val += list[i].SpotId + ",";
                        text += Server.UrlEncode(list[i].SpotName) + ",";
                        html += "<span class='upload_filename'><a data-class='a_Journey_Since' data-for='" + list[i].SpotId + "'> " + list[i].SpotName + " </a> <a data-for='" + list[i].SpotId + "' href='javascript:void(0);' onclick='Journey.RemoveSince(this)'><img src='/images/cha.gif'></a></span>";
                    }
                    val = val.Substring(0, val.Length - 1);
                    text = text.Substring(0, text.Length - 1);
                    return "<input type='hidden' name='hd_scenery_spot' value='" + val + "' /><input type='hidden' name='show_scenery_spot' value='" + text + "' /><span data-class='fontblue' class='fontblue'>" + html + "</span>";
                }
            }
            return "<input type='hidden' name='hd_scenery_spot' value='' /><input type='hidden' name='show_scenery_spot' value='' /><span data-class='fontblue' class='fontblue'></span>";
        }
    }
}