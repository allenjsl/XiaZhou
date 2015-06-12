using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.YlStructure;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.EnumType.YlStructure;
using System.Xml;

namespace EyouSoft.Web.YouLun
{
    public partial class YouHuiGuiZe : BackPage
    {
        protected string xiangyong = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetQueryStringValue("save") == "save") BaoCun();
            InitPage();
        }
        void InitPage()
        {
            string hangqi = Utils.GetQueryStringValue("id");
            YouHuiGongXiangFangShi fangshi;
            var items = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQiyouHuiGuiZes(hangqi, out fangshi);
            if (items == null || items.Count == 0) return;
            xiangyong = ((int)fangshi).ToString();
            rptlist.DataSource = items;
            rptlist.DataBind();
            PlaceHolder1.Visible = false;


        }
        /// <summary>
        /// 保存
        /// </summary>
        void BaoCun()
        {
            string hangqi = Utils.GetQueryStringValue("id");
            EyouSoft.Model.EnumType.YlStructure.YouHuiGongXiangFangShi fangshi = (EyouSoft.Model.EnumType.YlStructure.YouHuiGongXiangFangShi)Utils.GetInt(Utils.GetFormValue("ddlyouhuifangshi"));
            IList<EyouSoft.Model.YlStructure.MHangQiYouHuiGuiZeInfo> items = new List<EyouSoft.Model.YlStructure.MHangQiYouHuiGuiZeInfo>();
            string[] youhuimingchengs = Utils.GetFormValues("youhuimingcheng");
            string[] youhuimiaoshus = Utils.GetFormValues("youhuimiaoshu");
            string[] youhuijiages = Utils.GetFormValues("youhuijiage");
            string[] youhuifangshis = Utils.GetFormValues("ddlfangshi");

            string[] XiaDanShiJianTiaoJians = Utils.GetFormValues("ddlxiadanshijian");
            string[] XiaDanShiJianTianShus = Utils.GetFormValues("txtxiadanshijiant");
            string[] XiaDanShiJianSs = Utils.GetFormValues("txtxiadanshijians");
            string[] XiaDanShiJianEs = Utils.GetFormValues("txtxiadanshijiane");
            string[] ChuGangShiJianSs = Utils.GetFormValues("txtchugangshijians");
            string[] ChuGangShiJianEs = Utils.GetFormValues("txtchugangshijiane");
            string[] RenShuTiaoJians = Utils.GetFormValues("ddlrenshu");
            string[] RenShus = Utils.GetFormValues("txtrenshu");
            string[] YouKeNianLingTiaoJians = Utils.GetFormValues("ddlyoukenianling");
            string[] YouKeNianLings = Utils.GetFormValues("txtyoukenianling");
            string[] YouKeQuYus = Utils.GetFormValues("txtquyu");
            string[] ShiFouHuiYuans = Utils.GetFormValues("ddlchugangshijian");
            string[] DingDanJinETiaoJians = Utils.GetFormValues("ddldingdanjine");
            string[] DingDanJinEs = Utils.GetFormValues("txtdingdanjine");
            string[] XingBies = Utils.GetFormValues("ddlxingbie");

            if (youhuimingchengs != null && youhuimingchengs.Length > 0)
            {
                for (int i = 0; i < youhuimingchengs.Length; i++)
                {
                    MHangQiYouHuiGuiZeInfoJSON JSModel = new MHangQiYouHuiGuiZeInfoJSON();
                    JSModel.XiaDanShiJianTiaoJian = XiaDanShiJianTiaoJians[i];
                    JSModel.XiaDanShiJianTianShu = XiaDanShiJianTianShus[i];
                    JSModel.XiaDanShiJianS = XiaDanShiJianSs[i];
                    JSModel.XiaDanShiJianE = XiaDanShiJianEs[i];
                    JSModel.ChuGangShiJianS = ChuGangShiJianSs[i];
                    JSModel.ChuGangShiJianE = ChuGangShiJianEs[i];
                    JSModel.RenShuTiaoJian = RenShuTiaoJians[i];
                    JSModel.RenShu = RenShus[i];
                    JSModel.YouKeNianLingTiaoJian = YouKeNianLingTiaoJians[i];
                    JSModel.YouKeNianLing = YouKeNianLings[i];
                    JSModel.YouKeQuYu = YouKeQuYus[i];
                    JSModel.ShiFouHuiYuan = ShiFouHuiYuans[i];
                    JSModel.DingDanJinETiaoJian = DingDanJinETiaoJians[i];
                    JSModel.DingDanJinE = DingDanJinEs[i];
                    JSModel.XingBie = XingBies[i];


                    MHangQiYouHuiGuiZeInfo item = new MHangQiYouHuiGuiZeInfo();
                    item.MingCheng = youhuimingchengs[i];
                    item.MiaoShu = youhuimiaoshus[i];
                    item.JinE = Utils.GetDecimal(youhuijiages[i]);
                    item.FangShi = Utils.GetInt(youhuifangshis[i]);
                    if (JSModel != null)
                    {
                        item.GuiZe = Newtonsoft.Json.JsonConvert.SerializeObject(JSModel);
                    }
                    items.Add(item);
                }
            }

            if (new EyouSoft.BLL.YlStructure.BHangQi().SheZhiHangQiYouHuiGuiZe(SiteUserInfo.UserId, hangqi, fangshi, items) == 1)
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
            }
        }

        #region 前台调用方法
        protected string getSelectOpt(object obj)
        {
            System.Text.StringBuilder strbu = new System.Text.StringBuilder();
            strbu.Append("<option value=\"\">请选择</option>");
            if (obj != null && obj.ToString() == "1") { strbu.Append("<option value=\"1\"  selected=\"selected\">></option>"); }
            else { strbu.Append("<option value=\"1\" >></option>"); }
            if (obj != null && obj.ToString() == "2") { strbu.Append("<option value=\"2\"  selected=\"selected\">=</option>"); }
            else { strbu.Append("<option value=\"2\" >=</option>"); }
            if (obj != null && obj.ToString() == "3") { strbu.Append("<option value=\"3\"  selected=\"selected\"><</option>"); }
            else { strbu.Append("<option value=\"3\" ><</option>"); }
            return strbu.ToString();
        }
        /// <summary>
        /// 获取性别
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string getSelectXingBie(object obj)
        {
            System.Text.StringBuilder strbu = new System.Text.StringBuilder();
            strbu.Append("<option value=\"\">请选择</option>");
            if (obj != null && obj.ToString() == "0") { strbu.Append("<option value=\"0\"  selected=\"selected\">男</option>"); }
            else { strbu.Append("<option value=\"0\" >男</option>"); }
            if (obj != null && obj.ToString() == "1") { strbu.Append("<option value=\"1\"  selected=\"selected\">女</option>"); }
            else { strbu.Append("<option value=\"1\" >女</option>"); }

            return strbu.ToString();
        }

        /// <summary>
        /// 获取优惠方式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string getSelectFangShi(object obj)
        {
            System.Text.StringBuilder strbu = new System.Text.StringBuilder();
            if (obj != null && obj.ToString() == "0") { strbu.Append("<option value=\"0\"  selected=\"selected\">按订单</option>"); }
            else { strbu.Append("<option value=\"0\" >按订单</option>"); }
            if (obj != null && obj.ToString() == "1") { strbu.Append("<option value=\"1\"  selected=\"selected\">按人</option>"); }
            else { strbu.Append("<option value=\"1\" >按人</option>"); }

            return strbu.ToString();
        }
        /// <summary>
        /// 返回规则JSON实体
        /// </summary>
        /// <param name="GuiZe"></param>
        /// <returns></returns>
        protected MHangQiYouHuiGuiZeInfoJSON getJSModel(string GuiZe)
        {
            MHangQiYouHuiGuiZeInfoJSON model = new MHangQiYouHuiGuiZeInfoJSON();
            if (string.IsNullOrEmpty(GuiZe)) return model;

            //System.Runtime.Serialization.Json.DataContractJsonSerializer _Json = new System.Runtime.Serialization.Json.DataContractJsonSerializer(model.GetType());
            //byte[] _Using = System.Text.Encoding.UTF8.GetBytes(GuiZe);
            //System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream(_Using);
            //_MemoryStream.Position = 0;
            //model = (MHangQiYouHuiGuiZeInfoJSON)_Json.ReadObject(_MemoryStream);
            //return model;

            model = Newtonsoft.Json.JsonConvert.DeserializeObject<MHangQiYouHuiGuiZeInfoJSON>(GuiZe);

            return model;


        }

        protected string GetQuYu()
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();

            var items = new EyouSoft.BLL.ComStructure.BComCity().GetProvince(1, CurrentUserCompanyID);
            if (items == null || items.Count == 0) return string.Empty;
            int i = 0;

            foreach (var item in items)
            {
                s.AppendFormat("<label>&nbsp;<input type='checkbox' name='chkquyu' value='{0}'>{1}</label>&nbsp;&nbsp;&nbsp;&nbsp;", item.ProvinceId, item.Name);
                if (i > 0 &&( i+1) % 5 == 0) s.AppendFormat("<br>");
                i++;
            }

            return s.ToString();
        }
        #endregion

    }
}
