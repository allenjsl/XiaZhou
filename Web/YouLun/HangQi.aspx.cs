using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.YlStructure;

namespace EyouSoft.Web.YouLun
{
    public partial class HangQi : BackPage
    {
        #region 分页参数
        int pageSize = 20, pageIndex = 1, recordCount = 0;
        protected string sl = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            sl = Utils.GetQueryStringValue("sl");
            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                //初始化
                initList();

                InitXiLie();
            }
            if (Utils.GetQueryStringValue("del") == "1") Del();
            if (Utils.GetQueryStringValue("biaoqian") == "1") setBiaoQian();
        }
        /// <summary>
        /// 删除
        /// </summary>
        void Del()
        {
            string id = Utils.GetQueryStringValue("id");
            int result = new EyouSoft.BLL.YlStructure.BHangQi().DeleteHangQi(SiteUserInfo.CompanyId, SiteUserInfo.UserId, id);
            if (result == 1)
            {
                RCWE(UtilsCommons.AjaxReturnJson("1", "删除成功"));
            }
            else
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "删除失败"));
            }
        }
        /// <summary>
        /// 初始化列表
        /// </summary>
        void initList()
        {

            EyouSoft.Model.YlStructure.MHangQiChaXunInfo searchModel = new EyouSoft.Model.YlStructure.MHangQiChaXunInfo();
            searchModel.GysName = Utils.GetQueryStringValue("gongyingshang");
            searchModel.GongSiName = Utils.GetQueryStringValue("youlun");
            searchModel.XiLieName = Utils.GetQueryStringValue("xilie");
            searchModel.ChuanZhiName = Utils.GetQueryStringValue("chuanzhi");
            searchModel.RiQi1 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("riqi1"));
            searchModel.RiQi2 = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("riqi2"));
            searchModel.BianHao = Utils.GetQueryStringValue("bianhao");
            searchModel.MingCheng = Utils.GetQueryStringValue("mingcheng");
            searchModel.LeiXing = Utils.GetEnumValue<EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing>(Utils.GetQueryStringValue("t"), EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮);
            searchModel.XiLieId = Utils.GetQueryStringValue("txtxilieid");
            pageIndex = UtilsCommons.GetPadingIndex();

            IList<EyouSoft.Model.YlStructure.MHangQiInfo> list = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQis(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, searchModel); ;
            if (list != null && list.Count > 0)
            {
                this.repList.DataSource = list;
                this.repList.DataBind();
                BindPage();
            }
            else
            {
                this.repList.EmptyText = "<tr><td  align=\"center\" colspan=\"15\">暂无数据!</td></tr>";
                this.ExporPageInfoSelect1.Visible = false;
            }
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        void BindPage()
        {
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;

        }
        /// <summary>
        /// 权限控制
        /// </summary>
        void PowerControl()
        {

        }
        /// <summary>
        /// 设置标签
        /// </summary>
        void setBiaoQian()
        {
            int isSet = Utils.GetInt(Utils.GetQueryStringValue("dotype"));
            int result = 0;
            MHangQiBiaoQianInfo biaoqian = new MHangQiBiaoQianInfo();
            biaoqian.HangQiId = Utils.GetQueryStringValue("hangqi");
            biaoqian.IssueTime = DateTime.Now;
            biaoqian.OperatorId = SiteUserInfo.UserId;
            biaoqian.BiaoQian = (EyouSoft.Model.EnumType.YlStructure.HangQiBiaoQian)Utils.GetInt(Utils.GetQueryStringValue("b"));

            result = new EyouSoft.BLL.YlStructure.BHangQi().SheZhiHangQiBiaoQian(biaoqian, isSet);

            if (result == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));

        }

        protected string BiaoQianInfo(IList<MHangQiBiaoQianInfo> BiaoQians)
        {
            System.Text.StringBuilder strbu = new System.Text.StringBuilder();
            if (BiaoQians != null && BiaoQians.Count > 0)
            {
                for (int i = 0; i < BiaoQians.Count; i++)
                {
                    if (BiaoQians[i].BiaoQian == EyouSoft.Model.EnumType.YlStructure.HangQiBiaoQian.热门) strbu.AppendFormat("<a class=\"remen\">【热门】</a>");
                    if (BiaoQians[i].BiaoQian == EyouSoft.Model.EnumType.YlStructure.HangQiBiaoQian.热门推荐) strbu.AppendFormat("<a class=\"rementuijian\">【热门推荐】</a>");
                    if (BiaoQians[i].BiaoQian == EyouSoft.Model.EnumType.YlStructure.HangQiBiaoQian.特价) strbu.AppendFormat("<a class=\"tejia\">【特价】</a>");
                    if (BiaoQians[i].BiaoQian == EyouSoft.Model.EnumType.YlStructure.HangQiBiaoQian.特价推荐) strbu.AppendFormat("<a class=\"tejiatuijian\">【特价推荐】</a>");
                }
            }

            return strbu.ToString();
        }

        void InitXiLie()
        {
            var chaXun = new EyouSoft.Model.YlStructure.MXiLieChaXunInfo();
            chaXun.GongSiLeiXing = Utils.GetEnumValue<EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing>(Utils.GetQueryStringValue("t"), EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮);
            int _recordcount = 0;
            var items = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetXiLies(CurrentUserCompanyID, 10000, 1, ref _recordcount, chaXun);

            System.Text.StringBuilder s = new System.Text.StringBuilder();

            string xilieid = Utils.GetQueryStringValue("txtxilieid");

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    if (item.XiLieId == xilieid)
                    {
                        s.AppendFormat("<option value='{0}' selected='selected'>{1}</option>", item.XiLieId, item.MingCheng);
                    }
                    else
                    {
                        s.AppendFormat("<option value='{0}'>{1}</option>", item.XiLieId, item.MingCheng);
                    }
                }
            }

            ltrXiLie.Text = s.ToString();
        }
    }
}
