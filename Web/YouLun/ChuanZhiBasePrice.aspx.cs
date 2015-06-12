using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Text;

namespace EyouSoft.Web.YouLun
{
    public partial class ChuanZhiBasePrice : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("save") == "save") BaoCun();
            initList();
        }

        /// <summary>
        ///保存
        /// </summary>
        void BaoCun()
        {
            var gj = Utils.GetFormValues("ddlguoji");
            var jg = Utils.GetFormValues("zhanchuang");
            var id = Utils.GetFormValues("hidid");
            var chuanzhiid = Utils.GetQueryStringValue("id");
            var hx = Utils.GetFormValues("ddlhangxian");
            var ls = new List<EyouSoft.Model.YlStructure.MChuanZhiBasePrice>();

            if (id != null && id.Length > 0)
            {
                for (int i = 0; i < id.Length; i++)
                {
                    ls.Add(new EyouSoft.Model.YlStructure.MChuanZhiBasePrice() { Id = !string.IsNullOrEmpty(id[i]) ? id[i] : Guid.NewGuid().ToString(), ChuanZhiId = chuanzhiid, CountryId = Utils.GetInt(gj[i]), BasePrice = Utils.GetDecimal(jg[i]), HangXianId = Utils.GetInt(hx[i]) });
                }
            }
            var r = new BLL.YlStructure.BJiChuXinXi().BasePrice(chuanzhiid,ls);

            if (r > 0)
                RCWE(UtilsCommons.AjaxReturnJson("1", "保存成功"));
            else
                RCWE(UtilsCommons.AjaxReturnJson("0", "保存失败"));
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        void initList()
        {
            string hangqi = Utils.GetQueryStringValue("id");

            var list = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetBasePrice(hangqi);
            if (list != null && list.Count > 0)
            {
                rpt.DataSource = list;
                rpt.DataBind();
                PlaceHolder1.Visible = false;
            }
        }

        /// <summary>
        /// 初始化列表选择项
        /// </summary>
        /// <param name="val"></param>
        /// <param name="mark"></param>
        /// <returns></returns>
        protected string getOpts(string val, int mark)
        {
            StringBuilder strbu = new StringBuilder();
            EyouSoft.Model.YlStructure.MJiChuXinXiChaXunInfo search = null;
            if (mark >= 0)
            {
                search = new EyouSoft.Model.YlStructure.MJiChuXinXiChaXunInfo();
                search.LeiXing = (EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing)mark;
                search.YouLunLeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)Utils.GetInt(Utils.GetQueryStringValue("leixing"));
            }

            strbu.Append("<option value=''>请选择</option>");
            var fujias = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetJiChuXinXis(SiteUserInfo.CompanyId, search);
            if (fujias != null && fujias.Count > 0)
            {
                for (int i = 0; i < fujias.Count; i++)
                {
                    if (fujias[i].XinXiId.ToString() == val)
                    {
                        strbu.AppendFormat("<option  value=\"{0}\" selected=\"selected\">{1}</option>", fujias[i].XinXiId, fujias[i].MingCheng);
                    }
                    else
                    {
                        strbu.AppendFormat("<option  value=\"{0}\" >{1}</option>", fujias[i].XinXiId, fujias[i].MingCheng);
                    }

                }
            }
            return strbu.ToString();
        }
    }
}
