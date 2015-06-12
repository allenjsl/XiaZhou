//团队状态明细信息 汪奇志 2013-09-05
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
    /// 团队状态明细信息
    /// </summary>
    public partial class TourStatusXX : EyouSoft.Common.Page.BackPage
    {
        string TourId = string.Empty;
        EyouSoft.Model.EnumType.TourStructure.TourType TourType = EyouSoft.Model.EnumType.TourStructure.TourType.单项服务; 


        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourid");
            if (string.IsNullOrEmpty(TourId)) RCWE("异常请求");
            InitInfo();
            InitRpt();
        }

        #region private members
        /// <summary>
        /// init info
        /// </summary>
        void InitInfo()
        {
            var info = new EyouSoft.BLL.TourStructure.BTour().GetTourInfo(TourId);

            if (info == null) RCWE("异常请求");

            ltrTourCode.Text = info.TourCode;
            TourType = info.TourType;
        }

        /// <summary>
        /// init repeater
        /// </summary>
        void InitRpt()
        {
            var items = new EyouSoft.BLL.TourStructure.BTour().GetTourStatusXXs(TourId);

            if (items != null && items.Count > 0)
            {
                rpt.DataSource = items;
                rpt.DataBind();
            }
            else
            {
                phEmpty.Visible = true;
            }

        }
        #endregion

        /// <summary>
        /// 获取团队状态
        /// </summary>
        /// <param name="tourStatus">团队状态</param>
        /// <returns></returns>
        protected string GetTourStatus(object tourStatus)
        {
            if (tourStatus == null) return string.Empty;

            string s = string.Empty;

            if (TourType == EyouSoft.Model.EnumType.TourStructure.TourType.单项服务)
            {
                var _tourStatus = (EyouSoft.Model.EnumType.TourStructure.TourStatus)tourStatus;
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
                    default: break;
                }

                return s;
            }

            return tourStatus.ToString();
        }
    }
}
