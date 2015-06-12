using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace EyouSoft.Web.YouLun
{
    public partial class HangQiYuKong : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageInit();
            if (Utils.GetQueryStringValue("dotype") == "" && Utils.GetQueryStringValue("save") == "save") BaoCun();
            if (Utils.GetQueryStringValue("dotype") == "xg" && Utils.GetQueryStringValue("save") == "save") BaoCunXG();

        }
        /// <summary>
        /// 保存
        /// </summary>
        void BaoCun()
        {
            string id = Utils.GetQueryStringValue("id");
            //string date = Utils.GetQueryStringValue("aid");
            string date = Utils.GetFormValue("txtriqi");
            string[] dates = date.Split(',');
            string renshu = Utils.GetFormValue(txtRS.UniqueID);
            IList<EyouSoft.Model.YlStructure.MHangQiRiQiInfo> items = new List<EyouSoft.Model.YlStructure.MHangQiRiQiInfo>();

            foreach (var s in dates)
            {
                EyouSoft.Model.YlStructure.MHangQiRiQiInfo item = new EyouSoft.Model.YlStructure.MHangQiRiQiInfo();
                item.HangQiId = id;
                item.RiQi = Utils.GetDateTime(s);
                item.RenShu = Utils.GetInt(renshu);
                items.Add(item);
            }

            if (new EyouSoft.BLL.YlStructure.BHangQi().SheZhiHangQiRiQi(SiteUserInfo.UserId, id, items) == 1)
            {
                RCWE(UtilsCommons.AjaxReturnJson("1", "保存成功"));
            }
            else
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "保存失败"));
            }

        }
        /// <summary>
        /// 初始化
        /// </summary>
        void PageInit()
        {
            string rid = Utils.GetQueryStringValue("riqiid");
            if (Utils.GetQueryStringValue("piliang") == "1") return;
            var model = new EyouSoft.BLL.YlStructure.BHangQi().GetRiQiInfo(rid);
            if (model == null) return;
            txtRS.Value = model.RenShu.ToString();
        }
        /// <summary>
        /// 修改人数
        /// </summary>
        void BaoCunXG()
        {
            string rid = Utils.GetQueryStringValue("riqiid");
            int renshu = Utils.GetInt(Utils.GetFormValue(txtRS.UniqueID));
            if (new EyouSoft.BLL.YlStructure.BHangQi().ShenZhiHangQiRiQiRenShu(rid, renshu, SiteUserInfo.UserId) == 1)
            {
                RCWE(UtilsCommons.AjaxReturnJson("1", "保存成功"));
            }
            else
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "保存失败"));
            }

        }
    }
}
