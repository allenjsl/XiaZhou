using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Model.YlStructure;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.Huiyuan
{
    public partial class ShouCang : EyouSoft.YlWeb.HuiYuanPage
    {
        protected int pageSize = 10;
        protected int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
        protected int recordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitRpt();
            if (Utils.GetQueryStringValue("del") == "1") Del();
        }

        void Del()
        {
            var ids = Utils.GetQueryStringValue("ids").Split(',');
            var msg = string.Empty;
            var result = 0;
            if (ids != null && ids.Length > 0)
            {
                foreach (var id in ids)
                {
                    result = new EyouSoft.BLL.YlStructure.BHuiYuan().DeleteShouCangJia(this.HuiYuanInfo.CompanyId, this.HuiYuanInfo.HuiYuanId, id);
                }
            }
            if (result == 1)
            {
                msg = UtilsCommons.AjaxReturnJson("1", "删除成功");
            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("0", "删除失败");
            }
            Response.Clear();
            Response.Write(msg);
            Response.End();
        }
        
        void InitRpt()
        {
            var l = new BLL.YlStructure.BHuiYuan().GetShouCangJias(this.HuiYuanInfo.HuiYuanId, pageSize, pageIndex, ref recordCount, this.GetChaXun());

            if (l != null && l.Count > 0)
            {
                rpt.DataSource = l;
                rpt.DataBind();

                RegisterScript(string.Format("pConfig.pageSize={0};pConfig.pageIndex={1};pConfig.recordCount={2};", pageSize, pageIndex, recordCount));
            }
            else
            {
                this.phdNoDat.Visible = true;
            }
        }

        MHuiYuanShouCangJiaChaXunInfo GetChaXun()
        {
            var m = new MHuiYuanShouCangJiaChaXunInfo()
            {
            };
            return m;
        }

        protected string GetLianJie(object cpid, object leixing)
        {
            var _leixing = (EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing)leixing;

            string s = "javascript:void(0)";
            switch (_leixing)
            {
                case EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing.海洋游轮:
                    s = "/hangqi/HY" + cpid+".html";
                    break;
                case EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing.积分兑换:
                    s = "/jifen/jifeninfo.aspx?id=" + cpid;
                    break;
                case EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing.团购产品:
                    s = "/tuangou/tuangouxiangqing.aspx?tuangouid=" + cpid;
                    break;
                case EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing.长江游轮:
                    s = "/hangqi/" + cpid+".html";
                    break;
            }

            return s;
        }

        protected string GetJinE(object jine, object jifen, object leixing)
        {
            var _leixing = (EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing)leixing;

            if (_leixing == EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing.海洋游轮
                || _leixing == EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing.团购产品
                || _leixing == EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing.长江游轮)
            {
                string s = string.Format("<font class='font_yellow font16'>{0:F2}</font><br/>", jine);
                s += string.Format("【可获积分{0:F2}】", jifen);

                return s;
            }

            if (_leixing == EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing.积分兑换)
            {
                string s = string.Format("<font class='font_yellow font16'>商品金额：{0:F2}</font><br/>", jine);
                s += string.Format("【兑换该商品需要{0:F2}积分】", jifen);

                return s;
            }

            return string.Empty;
        }

        protected string GetShuLiang(object isyouxiao, object leixing)
        {
            var _leixing = (EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing)leixing;

            if (_leixing == EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing.海洋游轮
                || _leixing == EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing.团购产品
                || _leixing == EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing.长江游轮)
            {
                if ((bool)isyouxiao) return "可预定";
                else return "已过期";
            }

            if (_leixing == EyouSoft.Model.EnumType.YlStructure.HuiYuanShouCangLeiXing.积分兑换)
            {
                if ((bool)isyouxiao) return "可兑换";
                else return "已下架";
            }

            return string.Empty;
        }
    }
}
