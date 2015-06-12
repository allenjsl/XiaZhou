using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using System.Collections;

namespace EyouSoft.Web.YouLun
{
    public partial class HangQiJiaGe : BackPage
    {
        protected string OldDate = string.Empty;
        protected string CurrentDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today);
        protected string NextDate = string.Format("new Date(Date.parse('{0:yyyy\\/MM\\/dd}'))", DateTime.Today.AddMonths(1));
        protected StringBuilder stroldHangqis = new StringBuilder();//已设定航期的
         protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("save") == "save") BaoCun();
            if (Utils.GetQueryStringValue("get") == "get") SheDingState();
            if (Utils.GetQueryStringValue("do") == "check") Check();
            initPage();
        }
         void Check()
         {
             var hangqiid = Utils.GetQueryStringValue("hangqiid");
             var riqiids = Utils.GetQueryStringValue("riqiids").Split(',');
             var b = new BLL.YlStructure.BHangQi();
             var l0 = b.GetHangQiJiaGes(hangqiid, riqiids[0]);
             var c = "1";

             foreach (var riqiid in riqiids.Skip(1))
             {
                 var l1 = b.GetHangQiJiaGes(hangqiid, riqiid);
                 var l2 = l0.Except(l1, new HangQiJiaGeListEquality()).ToList();
                 var l3 = l1.Except(l0, new HangQiJiaGeListEquality()).ToList();
                 if (l2.Count > 0 || l3.Count > 0)
                 {
                     c = "0";
                     break;
                 }
             }
             RCWE(UtilsCommons.AjaxReturnJson(c));
         }
        void BaoCun()
        {
            
            RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
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
                    stroldHangqis.AppendFormat("<div  id=\"{0}\" data-id=\"{2}\" data-Jiage=\"{3}\"><input type=\"hidden\" name=\"YuKongRi\" value=\"{0}\"><input type=\"hidden\" name=\"YuKongShu\" value=\"{0}\"></div>", list[i].RiQi.ToString("yyyy-M-d"), list[i].RenShu, list[i].RiQiId, list[i].IsJiaGe);
                }
            }

        }
        void SheDingState()
        {
            string hangqiid = Utils.GetQueryStringValue("id");
            string riqiID = Utils.GetQueryStringValue("aid");
            var jiages = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQiJiaGes(hangqiid, riqiID);
            if (jiages != null && jiages.Count > 0)
            {
                RCWE(UtilsCommons.AjaxReturnJson("1", "已设定"));
            }
            else
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "未设定"));
            }
        }
    }

    public class HangQiJiaGeListEquality : IEqualityComparer<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo>
    {
        public bool Equals(EyouSoft.Model.YlStructure.MHangQiJiaGeInfo x, EyouSoft.Model.YlStructure.MHangQiJiaGeInfo y)
        {
            return x.BinKeLeiXingId == y.BinKeLeiXingId && x.FangXingId == y.FangXingId && x.GuoJiId == y.GuoJiId && x.JiaGe2 == y.JiaGe2 && x.JiaGe3 == y.JiaGe3 && x.JiaGe4 == y.JiaGe4 && x.ShuoMing == y.ShuoMing;
        }

        public int GetHashCode(EyouSoft.Model.YlStructure.MHangQiJiaGeInfo obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return obj.ToString().GetHashCode();
            }
        }
    }
}
