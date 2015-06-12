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
    public partial class HangQiJiaGeBox : BackPage
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
            string[] guojis = Utils.GetFormValues("ddlguoji");
            string[] leixings = Utils.GetFormValues("ddlleixing");
            string[] jiages = Utils.GetFormValues("jiage");
            string[] zhanchuangs = Utils.GetFormValues("zhanchuang");
            string[] jiachuangs = Utils.GetFormValues("jiachuang");
            string[] bujiachuangs = Utils.GetFormValues("bujiachuang");
            string[] shuomings = Utils.GetFormValues("shuoming");
            string[] txtjiageid = Utils.GetFormValues("txtjiageid");
            List<MHangQiJiaGeInfo> list = new List<MHangQiJiaGeInfo>();

            if (fangxings != null && fangxings.Length > 0)
            {
                for (int i = 0; i < fangxings.Length; i++)
                {

                    MHangQiJiaGeInfo model = new MHangQiJiaGeInfo();
                    model.FangXingId = Utils.GetInt(fangxings[i]);

                    model.GuoJiId = Utils.GetInt(guojis[i]);

                    model.BinKeLeiXingId = Utils.GetInt(leixings[i]);

                    //model.JiaGe1 = Utils.GetDecimal(jiages[i]);

                    model.JiaGe2 = Utils.GetDecimal(zhanchuangs[i]);

                    model.JiaGe3 = Utils.GetDecimal(jiachuangs[i]);

                    model.JiaGe4 = Utils.GetDecimal(bujiachuangs[i]);                    

                    model.ShuoMing = shuomings[i];
                    model.JiaGeId = txtjiageid[i];

                    if (model.JiaGe1 == 0 && model.JiaGe2 == 0 && model.JiaGe3 == 0 && model.JiaGe4 == 0) continue;
                    var item1 = list.FindLast(tmp =>
                    {
                        if (tmp.FangXingId == model.FangXingId
                            && tmp.GuoJiId == model.GuoJiId
                            &&tmp.BinKeLeiXingId==model.BinKeLeiXingId) return true;
                        return false;
                    });

                    if (item1 != null) continue;

                    list.Add(model);
                }
            }

            if (list == null || list.Count == 0) RCWE(UtilsCommons.AjaxReturnJson("0", "至少要填写一个价格信息"));

            /*
            if (new EyouSoft.BLL.YlStructure.BHangQi().SheZhiHangQiJiaGe(SiteUserInfo.UserId, hangqi, riqi, list) == 1)
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
                if (string.IsNullOrEmpty(riqi))
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
            var riqiids = Utils.GetQueryStringValue("riqiids").Split(',');
            if (string.IsNullOrEmpty(riqi) && riqiids != null && riqiids.Length > 0) riqi = riqiids[0];

            if (Utils.GetQueryStringValue("piliang") == "1")
            {
                IList<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo> list = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQiJiaGes(hangqi,Utils.GetInt(Utils.GetQueryStringValue("hangxianid")));
                if (list != null && list.Count > 0)
                {
                    rptlist.DataSource = list;
                    rptlist.DataBind();
                    PlaceHolder1.Visible = false;
                }
            }
            else
            {
                IList<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo> list = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQiJiaGes(hangqi, riqi);
                if (list != null && list.Count > 0)
                {
                    rptlist.DataSource = list;
                    rptlist.DataBind();
                    PlaceHolder1.Visible = false;
                }
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
                search.YouLunLeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮;
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
