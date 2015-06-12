using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace EyouSoft.YlWeb.Lipinka
{
    /// <summary>
    /// 礼品卡首页
    /// </summary>
    public partial class Default : WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitGunDongTuPian();
            InitShiTiKa();
            InitDianZiKa();
        }

        #region private members
        /// <summary>
        /// init gundongtupin
        /// </summary>
        void InitGunDongTuPian()
        {
            int recordCount = 0;
            var chaXun = new EyouSoft.Model.YlStructure.MWzGuangGaoChaXunInfo();
            //chaXun.WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.礼品卡首页轮换图片;
            var items = new EyouSoft.BLL.YlStructure.BWz().GetGuangGaos(YuMingInfo.CompanyId, 5, 1, ref recordCount, chaXun);

            StringBuilder s1 = new StringBuilder();
            StringBuilder s2 = new StringBuilder();

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    string url = item.Url;
                    string target = "target=\"_blank\"";

                    if (string.IsNullOrEmpty(url))
                    {
                        url = "javascript:void(0)";
                        target = string.Empty;
                    }

                    s1.AppendFormat("<li><a href=\"{0}\" {1}><img src=\"{2}\" /></a></li>", url, target, ErpFilepath+item.Filepath);

                    s2.AppendFormat("<li><a href=\"javascript:void(0)\"></a></li>");
                }
            }

            ltr1.Text = s1.ToString();
            ltr2.Text = s2.ToString();
        }

        /// <summary>
        /// init shitika
        /// </summary>
        void InitShiTiKa()
        {
            int recordCount = 0;
            var chaXun = new EyouSoft.Model.YlStructure.MLiPinKaChaXunInfo();
            chaXun.LeiXing = EyouSoft.Model.EnumType.YlStructure.LiPinKaLeiXing.实体卡;
            var items = new EyouSoft.BLL.YlStructure.BLiPinKa().GetLiPinKas(YuMingInfo.CompanyId, 5, 1, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                rpt1.DataSource = items;
                rpt1.DataBind();
            }
        }

        /// <summary>
        /// init dainzika
        /// </summary>
        void InitDianZiKa()
        {
            int recordCount = 0;
            var chaXun = new EyouSoft.Model.YlStructure.MLiPinKaChaXunInfo();
            chaXun.LeiXing = EyouSoft.Model.EnumType.YlStructure.LiPinKaLeiXing.电子卡;
            var items = new EyouSoft.BLL.YlStructure.BLiPinKa().GetLiPinKas(YuMingInfo.CompanyId, 5, 1, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                rpt2.DataSource = items;
                rpt2.DataBind();
            }
        }
        #endregion
    }
}
