using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;

namespace Web.CommonPage
{
    /// <summary>
    /// 地接社选取
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2011-9-27
    public partial class UseSupplier : BackPage
    {
        /// <summary>
        /// 计调枚举
        /// </summary>
        protected EyouSoft.Model.EnumType.PlanStructure.PlanProject type;
        /// <summary>
        /// 用来区分供应商和预控(1:包括供应商选用和预控选用，0：只供应商选用)
        /// </summary>
        protected string flag = string.Empty;
        /// <summary>
        /// 标识为预控(区分是预控选用（1）还是供应商（0）)
        /// </summary>
        protected string IsYuKong = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {            
            //省份
            string dplProvince = Utils.GetQueryStringValue("ddlProvice");
            //城市
            string dplCity = Utils.GetQueryStringValue("ddlCity");
            //县区
            string dplArea = Utils.GetQueryStringValue("ddlArea");
            //名称
            string Name = Utils.GetQueryStringValue("txtName");
            flag = Utils.GetQueryStringValue("flag");
            IsYuKong = Utils.GetQueryStringValue("isyukong");
            type = (EyouSoft.Model.EnumType.PlanStructure.PlanProject)Enum.Parse(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanProject), (Utils.GetQueryStringValue("suppliertype")));
        }
    }
}
