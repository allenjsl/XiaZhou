using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using System.Text;

namespace EyouSoft.Web.YouLun
{
    public partial class HangQiSheDing : BackPage
    {
        protected string OldDate = string.Empty;
        protected string CurrentDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today);
        protected string NextDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today.AddMonths(1));
        protected StringBuilder stroldHangqis = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("save") == "save") BaoCun();
            initPage();
        }
        void BaoCun()
        {
            string hangqiid = Utils.GetQueryStringValue("hangqiid");
            string riqiid = Utils.GetQueryStringValue("riqiid");
            string riqi = Utils.GetQueryStringValue("riqi");

            int bllRetCode = new EyouSoft.BLL.YlStructure.BHangQi().QuXiaoHangQiRiQi(hangqiid, riqiid, SiteUserInfo.UserId, Utils.GetDateTime(riqi));

            if (bllRetCode == 1)
            {
                RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            }
            else if (bllRetCode == -97)
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败:已经存在预订订单，不可取消"));
            }
            else if (bllRetCode == -96)
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败:已经发布团购信息，不可取消"));
            }
            else
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
            }
        }
        /// <summary>
        /// 初始化已保存的数据
        /// </summary>
        void initPage()
        {
            string hangqiid = Utils.GetQueryStringValue("id");
            var list = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQiRiQis(hangqiid, null, null, null);
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    stroldHangqis.AppendFormat("<div  id=\"{0}\" data-id={2}><input type=\"hidden\" name=\"YuKongRi\" value=\"{0}\"><input type=\"hidden\" name=\"YuKongShu\" value=\"{0}\"></div>", list[i].RiQi.ToString("yyyy-M-d"), list[i].RenShu, list[i].RiQiId);
                }
            }
        }
    }
}
