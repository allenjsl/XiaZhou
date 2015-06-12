using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.YlStructure;
namespace EyouSoft.Web.YouLun
{
    public partial class HangQiJiaGeBoxHY : BackPage
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
            string hangqi = Utils.GetQueryStringValue("id");
            string riqi = Utils.GetQueryStringValue("aid");

            string[] fangxings = Utils.GetFormValues("ddlfangxing");
            string[] renshus = Utils.GetFormValues("renshu");
            string[] loucengs = Utils.GetFormValues("louceng");
            string[] leixings = Utils.GetFormValues("ddlleixing");
            string[] shuomings = Utils.GetFormValues("shuoming");
            string[] jiage = Utils.GetFormValues("jiage");
            string[] fangcha = Utils.GetFormValues("fangcha");
            string[] txtjiageid = Utils.GetFormValues("txtjiageid");
            List<MHangQiJiaGeInfo> list = new List<MHangQiJiaGeInfo>();

            if (fangxings != null && fangxings.Length > 0)
            {
                for (int i = 0; i < fangxings.Length; i++)
                {

                    MHangQiJiaGeInfo model = new MHangQiJiaGeInfo();
                    model.FangXingId = Utils.GetInt(fangxings[i]);

                    model.RongNaRenShu = Utils.GetInt(renshus[i]);

                    model.LouCeng = loucengs[i];

                    model.BinKeLeiXingId = Utils.GetInt(leixings[i]);

                    model.ShuoMing = shuomings[i];

                    model.JiaGe1 = Utils.GetDecimal(jiage[i]);
                    model.FangCha = Utils.GetDecimal(fangcha[i]);

                    model.JiaGeId = txtjiageid[i];

                    if (model.JiaGe1 == 0) continue;
                    var item1 = list.FindLast(tmp =>
                    {
                        if (tmp.FangXingId == model.FangXingId
                            && tmp.LouCeng == model.LouCeng
                            && tmp.BinKeLeiXingId == model.BinKeLeiXingId) return true;
                        return false;
                    });

                    if (item1 != null) continue;

                    list.Add(model);
                }
            }

            if (list == null || list.Count == 0) RCWE(UtilsCommons.AjaxReturnJson("0", "至少要填写一个价格信息"));

            /*if (new EyouSoft.BLL.YlStructure.BHangQi().SheZhiHangQiJiaGe(SiteUserInfo.UserId, hangqi, riqi, list) == 1)
            {

                RCWE(UtilsCommons.AjaxReturnJson("1", "保存成功"));
            }
            else
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "保存失败"));

            }*/

            var riqiids = Utils.GetFormValue("txtriqiid");
            var riqiiditems = riqiids.Split(',');

            foreach (var riqiiditem in riqiiditems)
            {
                if (Utils.GetQueryStringValue("piliang") == "1")
                {
                    foreach (var item in list)
                    {
                        item.JiaGeId = string.Empty;
                    }
                }

                int bllretcode = new EyouSoft.BLL.YlStructure.BHangQi().SheZhiHangQiJiaGe(SiteUserInfo.UserId, hangqi, riqiiditem, list);
            }

            RCWE(UtilsCommons.AjaxReturnJson("1", "保存成功"));
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        void initList()
        {
            string hangqi = Utils.GetQueryStringValue("id");
            string riqi = Utils.GetQueryStringValue("aid");

            if (Utils.GetQueryStringValue("piliang") == "1") return;

            IList<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo> list = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQiJiaGes(hangqi, riqi);
            if (list != null && list.Count > 0)
            {
                rptlist.DataSource = list;
                rptlist.DataBind();
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
                search.YouLunLeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮;
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
