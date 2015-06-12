using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.YlWeb.MasterPage
{
    public partial class Boxy : System.Web.UI.MasterPage
    {
        protected string ITitle = string.Empty;
        EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi _WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.长江游轮横幅;
        protected string Keywords = string.Empty;
        protected string Description = string.Empty;
        #region 外部seo
        /// <summary>
        /// 网页标题
        /// </summary>
        public string SEOWebTitle { get; set; }
        /// <summary>
        /// 网页关键字
        /// </summary>
        public string SEOKeywords { get; set; }
        /// <summary>
        /// 网页描述
        /// </summary>
        public string SEODescription { get; set; }
        #endregion

        public EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi WeiZhi
        {
            get { return _WeiZhi; }
            set { this._WeiZhi = value; }
        }

        bool _IsXianShiHengFu = true;
        public bool IsXianShiHengFu
        {
            get { return _IsXianShiHengFu; }
            set { this._IsXianShiHengFu = value; }
        }

        EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? _LeiXing = null;
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? LeiXing
        {
            get { return _LeiXing; }
            set { _LeiXing = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ITitle = Page.Title;

            var yuMingInfo = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();
            var biaoTiInfo = new EyouSoft.BLL.YlStructure.BWz().GetKvInfo(yuMingInfo.CompanyId, EyouSoft.Model.EnumType.YlStructure.WzKvKey.网站标题);
            var keywordsInfo = new EyouSoft.BLL.YlStructure.BWz().GetKvInfo(yuMingInfo.CompanyId, EyouSoft.Model.EnumType.YlStructure.WzKvKey.网站关键字);
            var descriptionInfo = new EyouSoft.BLL.YlStructure.BWz().GetKvInfo(yuMingInfo.CompanyId, EyouSoft.Model.EnumType.YlStructure.WzKvKey.网站描述);
            if (!String.IsNullOrEmpty(SEOWebTitle))
                ITitle = SEOWebTitle;
            else
                ITitle += "-" + biaoTiInfo.V;
            if (!String.IsNullOrEmpty(SEOKeywords))
                Keywords = string.Format("<meta name=\"keywords\" content=\"{0}\" />", SEOKeywords);
            else
                Keywords = string.Format("<meta name=\"keywords\" content=\"{0}\" />", keywordsInfo.V);
            if (!String.IsNullOrEmpty(SEODescription))
                Description = string.Format("<meta name=\"description\" content=\"{0}\" />", SEODescription);
            else
                Description = string.Format("<meta name=\"description\" content=\"{0}\" />", descriptionInfo.V);

            TopGuangGao1.WeiZhi = WeiZhi;
            TopGuangGao1.Visible = IsXianShiHengFu;

            Navhead1.LeiXing = LeiXing;
        }
    }
}
