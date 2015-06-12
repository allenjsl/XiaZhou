using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.TourStructure;

namespace EyouSoft.Web.TongJi
{
    public partial class SetTourStatus : BackPage
    {
        #region attributes
        /// <summary>
        /// 计划编号
        /// </summary>
        string TourId = string.Empty;
        /// <summary>
        /// 计划状态变更权限
        /// </summary>
        bool Privs_SetTourStatus = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            TourId = Utils.GetQueryStringValue("tourid");
            if (string.IsNullOrEmpty(TourId)) RCWE(UtilsCommons.AjaxReturnJson("0", "请求异常"));

            InitPrivs();

            if (Utils.GetQueryStringValue("dotype") == "submit") BaoCun();

            InitInfo();
        }

        #region private members
        /// <summary>
        /// initprivs
        /// </summary>
        void InitPrivs()
        {
            Privs_SetTourStatus = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.统计分析_状态查询表_计划状态变更);

            if (Privs_SetTourStatus)
            {
                ltrOperatorHtml.Text = "<a id=\"i_a_submit\" href=\"javascript:void(0);\" style=\"text-indent:0px;\">保存</a>";
            }
            else
            {
                ltrOperatorHtml.Text = "你没有计划状态变更权限。";
            }
        }

        /// <summary>
        /// 初始化信息
        /// </summary>
        void InitInfo()
        {
            var info = new EyouSoft.BLL.TourStructure.BTour().GetTourInfo(TourId);
            if (info == null) RCWE(UtilsCommons.AjaxReturnJson("0", "请求异常"));

            ltrTourCode.Text = info.TourCode;
            ltrRouteName.Text = info.RouteName;

            //var tourType = new EyouSoft.BLL.TourStructure.BTour().GetTourType(TourId);
            //var tourStatus = new EyouSoft.BLL.TourStructure.BTour().GetTourStatus(SiteUserInfo.CompanyId, TourId);

            var tourType = info.TourType;
            var tourStatus = info.TourStatus;

            txtTourType.Value = ((int)tourType).ToString();

            string s = string.Empty;

            if (tourType == EyouSoft.Model.EnumType.TourStructure.TourType.单项服务)
            {
                switch (tourStatus)
                {
                    case TourStatus.销售未派计划:
                        ltrYuanStatus.Text = "操作中";
                        break;
                    case TourStatus.计调配置完毕:
                        ltrYuanStatus.Text = "已落实";
                        s = "<option value='0'>未落实</option>";
                        break;
                    case TourStatus.待终审:
                        ltrYuanStatus.Text = "待终审";
                        s = "<option value='0'>操作中</option><option value='3'>已落实</option>";
                        break;
                    case TourStatus.财务核算:
                        ltrYuanStatus.Text = "财务核算";
                        s = "<option value='0'>操作中</option><option value='3'>已落实</option><option value='9'>待终审</option>";
                        break;
                    case TourStatus.封团:
                        ltrYuanStatus.Text = "核算结束";
                        s = "<option value='10'>财务核算</option>";
                        break;
                    default: break;
                }
            }
            else
            {
                ltrYuanStatus.Text = tourStatus.ToString();

                switch (tourStatus)
                {
                    case EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划: break;
                    case EyouSoft.Model.EnumType.TourStructure.TourStatus.计调未接收: break;
                    case TourStatus.计调配置:
                        s = UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(TourStatus), new string[] { "0", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" }));
                        break;
                    case TourStatus.计调配置完毕:
                        s = UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(TourStatus), new string[] { "0", "1", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" }));
                        break;
                    case TourStatus.导游带团:
                        s = UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(TourStatus), new string[] { "0", "1", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" }));
                        break;
                    case TourStatus.导游报帐:
                        s = UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(TourStatus), new string[] { "0", "1", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" }));
                        break;
                    case TourStatus.销售未结算:
                        s = UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(TourStatus), new string[] { "0", "1", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" }));
                        break;
                    case TourStatus.销售待审:
                        s = UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(TourStatus), new string[] { "0", "1", "7", "8", "9", "10", "11", "12", "13", "14", "15" }));
                        break;
                    case TourStatus.计调待审:
                        s = UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(TourStatus), new string[] { "0", "1", "8", "9", "10", "11", "12", "13", "14", "15" }));
                        break;
                    case TourStatus.待终审:
                        s = UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(TourStatus), new string[] { "0", "1", "9", "10", "11", "12", "13", "14", "15" }));
                        break;
                    case TourStatus.财务核算:
                        s = UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(TourStatus), new string[] { "0", "1", "10", "11", "12", "13", "14", "15" }));
                        break;
                    case TourStatus.封团:
                        s = UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(TourStatus), new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "11", "12", "13", "14", "15" }));
                        break;
                    default: break;
                }
            }

            ltrStatusOption.Text = s;
        }

        /// <summary>
        /// 保存
        /// </summary>
        void BaoCun()
        {
            if (!Privs_SetTourStatus) RCWE(UtilsCommons.AjaxReturnJson("0", "没有操作权限"));

            var info = new EyouSoft.Model.TourStructure.MSetTourStatusInfo();
            info.CompanyId = SiteUserInfo.CompanyId;
            info.OperatorId = SiteUserInfo.UserId;
            info.Status = Utils.GetEnumValue<TourStatus>(Utils.GetFormValue("txtStatus"), TourStatus.封团);
            info.TourId = TourId;
            info.TourType = Utils.GetEnumValue<TourType>(Utils.GetFormValue(txtTourType.UniqueID), TourType.组团团队);
            info.YuanYin = Utils.GetFormValue("txtYuanYin");

            if (info.Status == TourStatus.封团) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：不允许的状态变更。"));

            int bllRetCode = new EyouSoft.BLL.TourStructure.BTour().SetTourStatus(info);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功。"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：不允许的状态变更。"));
        }
        #endregion
    }
}
