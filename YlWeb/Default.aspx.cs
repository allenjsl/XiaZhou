using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.YlStructure;
using System.Text;

namespace YlWeb
{
    public partial class _Default : EyouSoft.YlWeb.WzPage
    {
        protected string CompanyId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.长江游轮横幅;
            Master.LeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮;
            CompanyId = YuMingInfo.CompanyId;
            if (!Page.IsPostBack)
            {
                //HotList();
                GongSi();
            }
        }
        
        /// <summary>
        /// 合作游轮公司
        /// </summary>
        private void GongSi()
        {
            int recordCount = 0;
            EyouSoft.BLL.YlStructure.BJiChuXinXi bll = new EyouSoft.BLL.YlStructure.BJiChuXinXi();
            MGongSiChaXunInfo chaxun = new MGongSiChaXunInfo();
            chaxun.GongSiLeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮;
            var list = bll.GetGongSis(YuMingInfo.CompanyId, 7, 1, ref recordCount, chaxun);
            StringBuilder s = new StringBuilder();
            StringBuilder s1 = new StringBuilder();

            if (list != null && list.Count > 0)
            {
                s.AppendFormat("<div class=\"youlun_name\">{0}</div>", list[0].MingCheng);
                s.AppendFormat("<div class=\"youlun_img\"><a href=\"/youlun/YouLunGongSiCJ.aspx?id={1}\"><img width=\"400\" height=\"150\" src=\"{0}\"></a></div>", EyouSoft.YlWeb.TuPian.F1(ErpFilepath + list[0].Logo,400,150), list[0].GongSiId);

                for (int i = 1; i < list.Count; i++)
                {
                    s1.AppendFormat("<li title='{2}'><a href=\"/youlun/YouLunGongSiCJ.aspx?id={1}\"><img src=\"{0}\"></a></li>", EyouSoft.YlWeb.TuPian.F1(ErpFilepath + list[i].Logo,177,67), list[i].GongSiId, list[i].MingCheng);
                }
            }

            ltrGongSi1.Text = s.ToString();
            ltrGongSi2.Text = s1.ToString();
        }

        #region 私有方法
        /// <summary>
        /// 出发时间
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string ChuGangTimeHtml(object obj)
        {
            string str = "";
            if (obj != null)
            {
                var model = (IList<MHangQiRiQiInfo>)obj;
                if (model != null && model.Count > 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (model.Count > i)
                        {
                            str += model[0].RiQi.ToString("MM-dd") + "、";
                        }
                    }
                    str = str.Substring(0, str.Length - 1);

                    str += "...";
                }
            }
            return str;
        }
        /// <summary>
        /// 公司Logo
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GongSiLogo(object obj)
        {
            StringBuilder sb = new StringBuilder();
            if (obj != null)
            {
                var model = (IList<MFuJianInfo>)obj;
                if (model != null && model.Count > 0)
                {
                    sb.AppendFormat("<img src=\"{0}\">", model[0].Filepath);
                }
            }
            return sb.ToString();
        }

        protected string GetReXiaoImg(object fujians)
        {
            if (fujians == null) return string.Empty;

            var items = (IList<EyouSoft.Model.YlStructure.MFuJianInfo>)fujians;

            if (items != null && items.Count > 0)
            {
                return string.Format("<img src=\"{0}\" />", EyouSoft.YlWeb.TuPian.F1(ErpFilepath + items[0].Filepath, 193, 146));
            }

            return string.Empty;
        }
        #endregion
    }
}
