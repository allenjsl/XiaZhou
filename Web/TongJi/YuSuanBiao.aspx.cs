//统计分析-预算表 汪奇志 2014-02-24
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Model.EnumType.PrivsStructure;

namespace EyouSoft.Web.TongJi
{
    /// <summary>
    /// 统计分析-预算表
    /// </summary>
    public partial class YuSuanBiao : EyouSoft.Common.Page.BackPage
    {
        #region attributes
        /// <summary>
        /// 每页显示条数
        /// </summary>
        protected int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        protected int pageIndex = 1;        
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (UtilsCommons.IsToXls()) ToXls();
            InitPrivs();
            InitRpt();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!this.CheckGrant(Privs.统计分析_预算表_栏目))
            {
                Utils.ResponseNoPermit(Privs.统计分析_预算表_栏目, false);
                return;
            }
        }

        /// <summary>
        /// init rpt
        /// </summary>
        void InitRpt()
        {
            int recordCount = 0;
            pageIndex = UtilsCommons.GetPadingIndex();
            var chaXun = GetChaXunInfo();
            object[] heJi;

            var items = new EyouSoft.BLL.StatStructure.BYuSuanBiao().GetYuSuanBiaos(CurrentUserCompanyID, pageSize, pageIndex, ref recordCount, chaXun, out heJi);

            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();

                paging.UrlParams = Request.QueryString;
                paging.intPageSize = pageSize;
                paging.CurrencyPage = pageIndex;
                paging.intRecordCount = recordCount;

                ltrShiShouRenShu.Text = heJi[0].ToString();
                ltrShouRuJinE.Text = ((decimal)heJi[1]).ToString("F2");
                ltrZhiChuJinE.Text = ((decimal)heJi[2]).ToString("F2");
                ltrMaoLi.Text = ((decimal)heJi[1] - (decimal)heJi[2]).ToString("F2");
            }
            else
            {
                phEmpty.Visible = true;
                phHeJi.Visible = false;
            }
        }

        /// <summary>
        /// get chaxun info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.StatStructure.MYuSuanBiaoChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.StatStructure.MYuSuanBiaoChaXunInfo();

            info.ChuTuanERiQi = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtChuTuanERiQi"));
            info.ChuTuanSRiQi= Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtChuTuanSRiQi"));
            info.TourCode = Utils.GetQueryStringValue("txtTourCode");
            info.TourSellerDeptIds = null;
            info.TourStatus = null;
            info.XianLuName = Utils.GetQueryStringValue("txtXianLuName");
            txtXiaoShouYuan.SellsID = info.XiaoShouYuanId = Utils.GetQueryStringValue(txtXiaoShouYuan.SellsIDClient); ;
            txtXiaoShouYuan.SellsName = info.XiaoShouYuanName = Utils.GetQueryStringValue(txtXiaoShouYuan.SellsNameClient);

            string status = Utils.GetQueryStringValue(txtTourStatus.ValueClientID);
            if (!string.IsNullOrEmpty(status))
            {
                string[] items = status.Split(',');
                if (items != null && items.Length > 0)
                {
                    info.TourStatus = new EyouSoft.Model.EnumType.TourStructure.TourStatus[items.Length];
                    for (int i = 0; i < items.Length; i++)
                    {
                        info.TourStatus[i] = Utils.GetEnumValue<EyouSoft.Model.EnumType.TourStructure.TourStatus>(items[i], EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划);
                    }
                }
            }

            string tourSellerDepts = txtXiaoShouYuanBuMen.SectionID = Utils.GetQueryStringValue(txtXiaoShouYuanBuMen.SelectIDClient);
            txtXiaoShouYuanBuMen.SectionName = Utils.GetQueryStringValue(txtXiaoShouYuanBuMen.SelectNameClient);

            if (!string.IsNullOrEmpty(tourSellerDepts))
            {
                string[] items = tourSellerDepts.Split(',');
                if (items != null && items.Length > 0)
                {
                    info.TourSellerDeptIds = new int[items.Length];
                    for (int i = 0; i < items.Length; i++)
                    {
                        info.TourSellerDeptIds[i] = Utils.GetInt(items[i]);
                    }
                }
            }

            return info;
        }

        /// <summary>
        /// to xls
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            int _recordCount = 0;
            StringBuilder s = new StringBuilder();

            var chaXun = GetChaXunInfo();
            object[] heJi;

            var items = new EyouSoft.BLL.StatStructure.BYuSuanBiao().GetYuSuanBiaos(CurrentUserCompanyID, toXlsRecordCount, 1, ref _recordCount, chaXun, out heJi);

            s.AppendFormat("序号\t团号\t线路名称\t出团日期\t回团日期\t销售员\t计调员\t导游\t团队状态\t人数\t收入\t支出\t毛利\n");

            if (items == null && items.Count == 0) ResponseToXls(s.ToString());

            int i = 1;
            foreach (var item in items)
            {
                s.AppendFormat("{0}\t", i);
                s.AppendFormat("{0}\t", item.TourCode);
                s.AppendFormat("{0}\t", item.XianLuName);
                s.AppendFormat("{0}\t", item.ChuTuanRiQi);
                s.AppendFormat("{0}\t", item.HuiTuanRiQi);
                s.AppendFormat("{0}\t", item.XiaoShouYuanName);
                s.AppendFormat("{0}\t", item.JiDiaoYuanName);
                s.AppendFormat("{0}\t", item.DaoYouName);
                s.AppendFormat("{0}\t", GetTourStatus(item.TourType, item.TourStatus));
                s.AppendFormat("{0}\t", item.ShiShouRenShu);
                s.AppendFormat("{0}\t", item.ShouRuJinE);
                s.AppendFormat("{0}\t", item.ZhiChuJinE);
                s.AppendFormat("{0}\n", item.MaoLi);
                i++;
            }

            ResponseToXls(s.ToString());
        }
        #endregion

        #region protected members
        /// <summary>
        /// 获取计划状态
        /// </summary>
        /// <param name="tourType">团队类型</param>
        /// <param name="tourStatus">状态</param>
        /// <returns></returns>
        protected string GetTourStatus(object tourType, object tourStatus)
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
