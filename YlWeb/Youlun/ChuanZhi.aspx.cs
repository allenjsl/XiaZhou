using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Model.YlStructure;

namespace EyouSoft.YlWeb.Youlun
{
    public partial class ChuanZhi : WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.LeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮;

            if (!IsPostBack)
            {
                initPage();
            }

        }
        /// <summary>
        /// 初始化
        /// </summary>
        void initPage()
        {
            int recordCount = 0;
            EyouSoft.BLL.YlStructure.BHangQi bll = new EyouSoft.BLL.YlStructure.BHangQi();
            MHangQiChaXunInfo chaxun = new MHangQiChaXunInfo()
            {
                IsYouXiao = true,
                IsReXiao = true,
                LeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮
            };
            var lists = bll.GetHangQis(YuMingInfo.CompanyId, 12, 1, ref recordCount, chaxun);
            if (lists != null)
            {
                rpt_Hot.DataSource = lists;
                rpt_Hot.DataBind();
            }

            var model = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetChuanZhiInfo(Utils.GetQueryStringValue("id"));
            if (model != null)
            {
                if (model.GongSiLeiXing == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮) Response.Redirect(string.Format("chuanzhicj.aspx?id={0}", model.ChuanZhiId));
                lblCZ.Text = model.MingCheng;
                lblName.Text = model.MingCheng;
                litInfo.Text = model.XiangXiJieShao;

                lblzongdunwei.Text = string.Format("{0}吨", model.DunWei);
                lblzaikeliang.Text = string.Format("{0}人", model.ZaiKeLiang);
                lblshouhang.Text = string.Format("{0}", model.XiaShuiRiQi);
                lblchangdu.Text = string.Format("{0}米", model.ChangDu);
                lblkuandu.Text = string.Format("{0}米", model.KuangDu);
                lbllouceng.Text = string.Format("{0}层", model.JiaBanLouCeng);
                lblfangjian.Text = string.Format("{0}间", model.KeFangShuLiang);
                lblchishui.Text = string.Format("{0}米", model.ChiShui);
                lblhangsu.Text = string.Format("{0}节", model.ZuiGaoHangSu);

                lblimg.Text = "<img src=\"\" />";
                if (model.FuJians != null && model.FuJians.Count > 0)
                {
                    lblimg.Text = string.Format("<img src=\"{0}\" />", TuPian.F1(ErpFilepath + model.FuJians[0].Filepath, 648, 214));
                }

                var shipins = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetShiPins(model.CompanyId, 1000, 1, ref recordCount, new EyouSoft.Model.YlStructure.MShiPinChaXunInfo() { ChuanZhiId = model.ChuanZhiId });
                if (shipins != null && shipins.Count > 0)
                {
                    litShiPin.Text += string.Format("<div class=\"video_big\"><a target=\"_blank\" href=\"{0}\"><img src=\"{1}\"></a></div>", "javascript:void(0)", ErpFilepath + shipins[0].ShiPinIMG);
                    litShiPin.Text += " <div class=\"video_small\"><ul>";
                    for (int i = 1; i < shipins.Count; i++)
                    {
                        litShiPin.Text += string.Format("<li><a href=\"{0}\"><img src=\"{1}\"></a></li>", "javascript:void(0)", ErpFilepath + shipins[i].ShiPinIMG);
                    }

                    litShiPin.Text += " </ul><div class=\"clear\"></div></div>";
                }
                var list = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetXiLies(model.CompanyId, 1000, 1, ref recordCount, new EyouSoft.Model.YlStructure.MXiLieChaXunInfo() { GongSiId = model.GongSiId });
                if (list != null && list.Count > 0)
                {
                    rptxilies.DataSource = list;
                    rptxilies.DataBind();
                }

                if (model.FangXings != null && model.FangXings.Count > 0)
                {
                    rptfangxings.DataSource = model.FangXings;
                    rptfangxings.DataBind();
                }
                if (model.MeiShis != null && model.MeiShis.Count > 0)
                {
                    rptmeishis.DataSource = model.MeiShis;
                    rptmeishis.DataBind();
                }
                if (model.SheShis != null && model.SheShis.Count > 0)
                {
                    rptsheshis.DataSource = model.SheShis;
                    rptsheshis.DataBind();
                }

                if (model.PingMianTus != null && model.PingMianTus.Count > 0)
                {
                    rptpingmians.DataSource = model.PingMianTus;
                    rptpingmians.DataBind();
                }


                var items = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQis(model.CompanyId, 3, 1, ref recordCount, new EyouSoft.Model.YlStructure.MHangQiChaXunInfo() { ChuanZhiId = model.ChuanZhiId, IsYouXiao = true });
                if (items != null && items.Count > 0)
                {
                    rpthangxian.DataSource = items;
                    rpthangxian.DataBind();
                }

                var gongSiInfo = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetGongSiInfo(model.GongSiId);
                if (gongSiInfo != null) lblName.Text = gongSiInfo.MingCheng;
            }

        }

        /// <summary>
        /// 处理图片链接
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected string getImgPath(string path)
        {
            if (string.IsNullOrEmpty(path)) return "";
            return ErpFilepath + path;
        }
        /// <summary>
        /// 获取系列下面的船只
        /// </summary>
        /// <param name="company"></param>
        /// <param name="xilie"></param>
        /// <returns></returns>
        protected string getChuanzhiByXilie(string company, string xilie)
        {
            int recordCount = 0;
            StringBuilder strbu = new StringBuilder();
            strbu.Append("<div class=\"menubox\">");
            var items = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetChuanZhis(company, 1000, 1, ref recordCount, new EyouSoft.Model.YlStructure.MChuanZhiChaXunInfo() { XiLieId = xilie });
            if (items != null && items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    strbu.AppendFormat("<a href=\"/youlun/ChuanZhi.aspx?id={1}\">{0}</a> ", items[i].MingCheng, items[i].ChuanZhiId);
                }
            }

            strbu.Append(" </div>");
            return strbu.ToString();
        }
        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="fujians"></param>
        /// <returns></returns>
        protected string GetReXiaoImg(object fujians)
        {
            if (fujians == null) return string.Empty;

            var items = (IList<EyouSoft.Model.YlStructure.MFuJianInfo>)fujians;

            if (items != null && items.Count > 0)
            {
                return string.Format("<img src=\"{0}\" />", TuPian.F1(ErpFilepath + items[0].Filepath,97,78));
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="fujians"></param>
        /// <returns></returns>
        protected string GetReXiaoImg1(object fujians)
        {
            if (fujians == null) return string.Empty;

            var items = (IList<EyouSoft.Model.YlStructure.MFuJianInfo>)fujians;

            if (items != null && items.Count > 0)
            {
                return string.Format("<img src=\"{0}\" />", TuPian.F1(ErpFilepath + items[0].Filepath, 232,167));
            }

            return string.Empty;
        }




    }
}
