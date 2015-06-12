//预算表-毛利明细 汪奇志 2014-02-26
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.TongJi
{
    /// <summary>
    /// 预算表-毛利明细
    /// </summary>
    public partial class YuSuanBiaoMaoLi : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        string TourId = string.Empty;

        int ShiShouRenShu = 0;
        decimal ShouRuJinE = 0;
        decimal ZhiChuJinE = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourid");

            if (string.IsNullOrEmpty(TourId)) RCWE("异常请求");

            InitRptShouRu();
            InitRptZhiChu();

            InitInfo();
        }

        #region private members
        /// <summary>
        /// init rpt shouru
        /// </summary>
        void InitRptShouRu()
        {
            var items = new EyouSoft.BLL.StatStructure.BYuSuanBiao().GetShouRus(TourId);

            if (items != null && items.Count > 0)
            {
                rptShouRu.DataSource = items;
                rptShouRu.DataBind();

                foreach (var item in items)
                {
                    ShiShouRenShu += item.ShiShouRenShu;
                    ShouRuJinE += item.JinE;
                }

                if (items.Count > 1)
                {
                    ltrShiShouRenShu1.Text = ShiShouRenShu.ToString();
                    ltrShouRuJinE1.Text = ShouRuJinE.ToString("F2");
                }
                else
                {
                    phHeJiShuRu.Visible = false;
                }
            }
            else
            {
                phEmptyShouRu.Visible = true;
                phHeJiShuRu.Visible = false;
            }
        }

        /// <summary>
        /// init rpt zhichu
        /// </summary>
        void InitRptZhiChu()
        {
            var items = new EyouSoft.BLL.StatStructure.BYuSuanBiao().GetZhiChus(TourId);

            if (items != null && items.Count > 0)
            {
                rptZhiChu.DataSource = items;
                rptZhiChu.DataBind();

                foreach (var item in items)
                {
                    ZhiChuJinE += item.JinE;
                }

                if (items.Count > 1)
                {
                    ltrZhiChuJinE1.Text = ZhiChuJinE.ToString("F2");
                }
                else
                {
                    phHeJiZhiChu.Visible = false;
                }
            }
            else
            {
                phEmptyZhiChu.Visible = true;
                phHeJiZhiChu.Visible = false;
            }
        }

        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            var info = new EyouSoft.BLL.TourStructure.BTour().GetTourInfo(TourId);
            if (info == null) RCWE("异常请求");

            ltrTourCode.Text = info.TourCode;
            ltrRouteName.Text = info.RouteName;
            ltrTianShu.Text = info.TourDays.ToString();
            if (info.LDate.HasValue) ltrChuTuanRiQi.Text = info.LDate.Value.ToString("yyyy-MM-dd");
            if(info.RDate.HasValue)ltrHuiTuanRiQi.Text=info.RDate.Value.ToString("yyyy-MM-dd");
            ltrTourStatus.Text = GetTourStatus(info.TourType, info.TourStatus);
            ltrShiShouRenShu0.Text = ShiShouRenShu.ToString();
            ltrShouRuJinE0.Text = ShouRuJinE.ToString("F2");
            ltrZhiChuJinE0.Text = ZhiChuJinE.ToString("F2");
            ltrMaoLi.Text = (ShouRuJinE - ZhiChuJinE).ToString("F2");
            if(info.SaleInfo!=null)ltrXiaoShouYuan.Text = info.SaleInfo.Name;
            if (info.TourPlaner != null && info.TourPlaner.Count > 0)
            {
                string s = string.Empty;
                foreach (var item in info.TourPlaner)
                {
                    s += "," + item.Planer;
                }
                ltrJiDiaoYuan.Text = s.TrimStart(',');
            }
        }

        /// <summary>
        /// 获取计划状态
        /// </summary>
        /// <param name="tourType">团队类型</param>
        /// <param name="tourStatus">状态</param>
        /// <returns></returns>
        string GetTourStatus(object tourType, object tourStatus)
        {
            string s = string.Empty;
            if (tourType == null || tourStatus == null) return string.Empty;

            EyouSoft.Model.EnumType.TourStructure.TourType _tourType = (EyouSoft.Model.EnumType.TourStructure.TourType)tourType;
            EyouSoft.Model.EnumType.TourStructure.TourStatus _tourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)tourStatus;

            if (_tourType != EyouSoft.Model.EnumType.TourStructure.TourType.单项服务)
            {
                return _tourStatus.ToString();
            }

            switch (_tourStatus)
            {
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划:
                    s = "操作中";
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置完毕:
                    s = "已落实";
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.待终审:
                    s = "待终审";
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.财务核算:
                    s = "财务核算";
                    break;
                case EyouSoft.Model.EnumType.TourStructure.TourStatus.封团:
                    s = "核算结束";
                    break;
                default:
                    s = _tourStatus.ToString();
                    break;
            }

            return s;
        }
        #endregion
    }
}
