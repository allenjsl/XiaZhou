using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.YlStructure;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.Hangqi
{
    public partial class HangQiList : EyouSoft.YlWeb.WzPage
    {
        protected int PageIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitInfo();
            }
        }

        private void InitInfo()
        {
            EyouSoft.BLL.YlStructure.BHangQi bll = new EyouSoft.BLL.YlStructure.BHangQi();
            int recordCount = 0;
            MHangQiChaXunInfo chaxun = new MHangQiChaXunInfo();

            #region 查询|赋值
            //航线
            string hx = Utils.GetQueryStringValue("hx");
            //线路
            string xl = Utils.GetQueryStringValue("xl");
            //登船地址
            string dz = Utils.GetQueryStringValue("dz");
            //出发时间
            string cfy = Utils.GetQueryStringValue("cfy");
            //行程天数
            string xcs = Utils.GetQueryStringValue("xcs");
            string xce = Utils.GetQueryStringValue("xce");
            //价格区间
            string jgs = Utils.GetQueryStringValue("jgs");
            string jge = Utils.GetQueryStringValue("jge");
            string gs = Utils.GetQueryStringValue("gs");


            if (!string.IsNullOrEmpty(hx))
                chaxun.HangXianId = Utils.GetInt(hx);
            if (!string.IsNullOrEmpty(xl))
                chaxun.XiLieId = xl;
            if (!string.IsNullOrEmpty(dz))
                chaxun.ChuFaGangKouId = Utils.GetInt(dz);
            if (!string.IsNullOrEmpty(cfy))
            {
                DateTime t = Utils.GetDateTime(cfy, DateTime.Now) < DateTime.Now ? DateTime.Now : Utils.GetDateTime(cfy, DateTime.Now);
                chaxun.RiQi1 = t;
                chaxun.RiQi2 = t.AddDays(30);
            }
            if (!string.IsNullOrEmpty(xcs))
                chaxun.TianShu1 = Utils.GetInt(xcs);
            if (!string.IsNullOrEmpty(xce))
                chaxun.TianShu2 = Utils.GetInt(xce);
            if (!string.IsNullOrEmpty(jgs))
                chaxun.JiaGe1 = Utils.GetDecimal(jgs);
            if (!string.IsNullOrEmpty(jge))
                chaxun.JiaGe2 = Utils.GetDecimal(jge);

            #endregion

            chaxun.IsYouXiao = true;
            chaxun.GuanJianZi = Utils.GetQueryStringValue("gjz");
            chaxun.GongSiId = gs;

            PageIndex = UtilsCommons.GetPadingIndex();
            var list = bll.GetHangQis(YuMingInfo.CompanyId,14,PageIndex, ref recordCount, chaxun);
            if (list != null)
            {
                rpt_HangQi.DataSource = list;
                rpt_HangQi.DataBind();

                RegisterScript(string.Format("pConfig.pageSize={0};pConfig.pageIndex={1};pConfig.recordCount={2};", 14, PageIndex, recordCount));
            }
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
                if (model != null &&  model.Count> 0)
                {
                    for (int i = 0; i < model.Count; i++)
                    {
                        if (i > 2) break;
                        str += model[i].RiQi.ToString("M") + "/";
                    }
                    str = str.Substring(0, str.Length - 1);
                }
            }
            return str;
        }
        #endregion

        protected string GetHqUrl(object leiXing)
        {
            string url="/hangqi/hangqiinfo.aspx";
            var _leiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)leiXing;
            if (_leiXing == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮)
                url = "/hangqi/haiyanginfo.aspx";

            return url;
        }
    }
}
