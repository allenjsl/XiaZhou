using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Model.YlStructure;
using EyouSoft.Common;
using EyouSoft.BLL.YlStructure;
using System.Linq.Expressions;

namespace EyouSoft.YlWeb.TuanGou
{
    public partial class TuanGouPiao : WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.团购横幅;
            GetContent();
        }

        private void GetContent()
        {
            MTuanGouChaXunInfo search = new MTuanGouChaXunInfo();
            search.IsYouXiao = true;

            string jiage = Utils.GetQueryStringValue("jiage");
            if (!string.IsNullOrEmpty(jiage))
            {
                search.JiaGe1 = int.Parse(jiage.Split('-')[0]);
                if (jiage.Split('-').Length == 2)
                {
                    search.JiaGe2 = int.Parse(jiage.Split('-')[1]);
                }
            }
            string youlun_leixing = Utils.GetQueryStringValue("youlunleixing");
            if (!string.IsNullOrEmpty(youlun_leixing))
            {
                search.LeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)int.Parse(youlun_leixing);
            }
            string xingcheng = Utils.GetQueryStringValue("xingcheng");
            if (!string.IsNullOrEmpty(xingcheng))
            {
                search.TianShu1 = int.Parse(xingcheng.Split('-')[0]);
                if (xingcheng.Split('-').Length == 2)
                {
                    search.TianShu2 = int.Parse(xingcheng.Split('-')[1]);
                }
            }
            string chufadi = Utils.GetQueryStringValue("chufadi");
            if (!string.IsNullOrEmpty(chufadi))
            {
                search.ChuFaGangKouId = int.Parse(chufadi.Trim());
            }

            string hangxian = Utils.GetQueryStringValue("hangxian");
            if (!string.IsNullOrEmpty(hangxian))
            {
                search.HangXianId = int.Parse(hangxian);
            }
            var isasc = Utils.GetQueryStringValue("desc").ToLower() == "asc";
            switch (Utils.GetQueryStringValue("sort").ToLower())
            {
                case "xiaoliang":
                    search.PaiXu = isasc ? 9 : 8;
                    break;
                case "jiage":
                    search.PaiXu = isasc ? 3 : 2;
                    break;
                case "fabushijian":
                    search.PaiXu = isasc ? 1 : 0;
                    break;
                default:
                    search.PaiXu = 8;
                    break;
            }

            BHangQi bll = new BHangQi();

            int pageSize = 18;
            int pageIndex = UtilsCommons.GetPadingIndex();
            int recordCount = 0;
            IList<MTuanGouInfo> list = bll.GetTuanGous(YuMingInfo.CompanyId, pageSize, pageIndex, ref recordCount, search);
          
            if (list != null && list.Count() > 0)
            {
                Repeater1.DataSource = list;
                Repeater1.DataBind();

                RegisterScript(string.Format("pConfig.pageSize={0};pConfig.pageIndex={1};pConfig.recordCount={2};", pageSize, pageIndex, recordCount));
            }

            YouLunHangXiang();
            DengChuan();
        }

        /// <summary>
        /// 游轮航线
        /// </summary>
        private void YouLunHangXiang()
        {
            var list = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetJiChuXinXis(YuMingInfo.CompanyId, new EyouSoft.Model.YlStructure.MJiChuXinXiChaXunInfo { LeiXing = EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.航线, YouLunLeiXing = string.IsNullOrEmpty(Utils.GetQueryStringValue("youlunleixing"))?new Model.EnumType.YlStructure.YouLunLeiXing?():(Model.EnumType.YlStructure.YouLunLeiXing)Utils.GetInt(Utils.GetQueryStringValue("youlunleixing")) });
            if (list != null)
            {
                rptList_HangXian.DataSource = list;
                rptList_HangXian.DataBind();
            }
        }

        /// <summary>
        /// 出发地
        /// </summary>
        private void DengChuan()
        {
            var list = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetJiChuXinXis(YuMingInfo.CompanyId, new EyouSoft.Model.YlStructure.MJiChuXinXiChaXunInfo { LeiXing = EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.港口, YouLunLeiXing = string.IsNullOrEmpty(Utils.GetQueryStringValue("youlunleixing")) ? new Model.EnumType.YlStructure.YouLunLeiXing?() : (Model.EnumType.YlStructure.YouLunLeiXing)Utils.GetInt(Utils.GetQueryStringValue("youlunleixing")) });
            if (list != null)
            {
                rptList_DiZhi.DataSource = list;
                rptList_DiZhi.DataBind();
            }
        }
    }
}
