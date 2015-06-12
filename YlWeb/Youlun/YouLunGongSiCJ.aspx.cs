using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Model.YlStructure;

namespace EyouSoft.YlWeb.Youlun
{
    public partial class YouLunGongSiCJ : WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            initPage();
            GongSi();
        }
        void initPage()
        {
            var model = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetGongSiInfo(Utils.GetQueryStringValue("id"));
            int recordCount = 0;
            if (model != null)
            {
                if (model.LeiXing == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮) Response.Redirect(string.Format("/youlun/YouLunGongSi.aspx?id={0}", model.GongSiId));
                lblgongsi.Text = model.MingCheng;
                litInfo.Text = model.XiangXiJieShao;

                var items = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetXiLies(model.CompanyId, 1000, 1, ref recordCount, new MXiLieChaXunInfo() { GongSiId = model.GongSiId });
                if (items != null && items.Count > 0)
                {
                    rptxilies.DataSource = items;
                    rptxilies.DataBind();
                }
            }


            var YuMingInfo = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();
            var chaXun = new EyouSoft.Model.YlStructure.MWzZiXunChaXunInfo();
            chaXun.LeiXing = EyouSoft.Model.EnumType.YlStructure.WzZiXunLeiXing.长江游轮问题解答;
            var items1 = new EyouSoft.BLL.YlStructure.BWz().GetZiXuns(YuMingInfo.CompanyId, 8, 1, ref recordCount, chaXun);
            if (items1 != null && items1.Count > 0)
            {
                rptWT.DataSource = items1;
                rptWT.DataBind();
            }

        }

        protected string getYLbyXL(string gongsi, string xilie)
        {
            StringBuilder strbu = new StringBuilder();
            int recordCount = 0;
            var items = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetChuanZhis(gongsi, 50, 1, ref recordCount, new EyouSoft.Model.YlStructure.MChuanZhiChaXunInfo() { XiLieId = xilie });
            if (items != null && items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    strbu.AppendFormat("<li><div class=\"div_img\"><a href=\"/Youlun/ChuanZhiCJ.aspx?id={1}\"><img src=\"{0}\" style='width:161px;height:122px' /></a></div>", items[i].FuJians != null && items[i].FuJians.Count > 0 ? TuPian.F1(ErpFilepath + items[i].FuJians[0].Filepath,161,122) : "", items[i].ChuanZhiId);
                    strbu.AppendFormat("<dl><dt><a title='{0}' href=\"/Youlun/ChuanZhiCJ.aspx?id={1}\">{0}</a></dt>", items[i].MingCheng, items[i].ChuanZhiId);
                    strbu.AppendFormat("<dd title='{0}'>{0}</dd>", items[i].MingCheng1);
                    strbu.AppendFormat("<dd>邮轮吨位：{0}吨</dd>", Utils.GetInt(items[i].DunWei));
                    strbu.AppendFormat("<dd>载客人数：{0}人</dd>", Utils.GetInt(items[i].ZaiKeLiang));
                    strbu.AppendFormat("<dd>甲板层数：{0}层</dd>", Utils.GetInt(items[i].JiaBanLouCeng));
                    strbu.AppendFormat("<dd>首航时间：{0}</dd> </dl></li>", items[i].XiaShuiRiQi);
                }
            }
            return strbu.ToString();
        }
        /// <summary>
        /// 获取合作公司
        /// </summary>
        private void GongSi()
        {
            int recordCount = 0;
            EyouSoft.BLL.YlStructure.BJiChuXinXi bll = new EyouSoft.BLL.YlStructure.BJiChuXinXi();
            var list = bll.GetGongSis(YuMingInfo.CompanyId, 10, 1, ref recordCount, new MGongSiChaXunInfo() { GongSiLeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮 });
            if (list != null && list.Count > 0)
            {
                rpthezuos.DataSource = list;
                rpthezuos.DataBind();
            }
        }

    }
}
