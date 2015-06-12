using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.YlStructure;
using System.Text;
using System.Collections;
using Web.YouLun.WUC;

namespace EyouSoft.Web.YouLun
{
    public partial class HangQiAdd : BackPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
            YLCompanySelect1.T1 = Utils.GetInt(Utils.GetQueryStringValue("t"));
            if (!IsPostBack)
            {
                initPage();
            }
            if (Utils.GetQueryStringValue("save") == "save") BaoCun();

            #region 获取页面所需数据选项
            if (Utils.GetQueryStringValue("getXL") == "1") getXL();
            if (Utils.GetQueryStringValue("getCZ") == "1") getCZ();

            #endregion
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        void BaoCun()
        {
            string msg = string.Empty;
            string Id = Utils.GetQueryStringValue("id");
            EyouSoft.Model.YlStructure.MHangQiInfo model = new EyouSoft.Model.YlStructure.MHangQiInfo();
            model.SeoTitle = Utils.GetFormValue(txtSeoTitle.UniqueID);
            model.SeoKeyword = Utils.GetFormValue(txtSeoKeyword.UniqueID);
            model.SeoDescription = Utils.GetFormValue(txtSeoDescription.UniqueID);

            model.CompanyId = SiteUserInfo.CompanyId;
            model.IssueTime = DateTime.Now;
            model.OperatorId = SiteUserInfo.UserId;

            model.MingCheng = Utils.GetFormValue(txtchanpinmingcheng.UniqueID);
            model.BianHao = Utils.GetFormValue(txtchanpinbainhao.UniqueID);
            model.QiShiJiaGe = Utils.GetDecimal(Utils.GetFormValue(txtqishijiage.UniqueID));
            model.GysId = Utils.GetFormValue(SupplierControl1.ClientValue);
            model.GongSiId = Utils.GetFormValue(YLCompanySelect1.CompIDClient);
            model.XiLieId = Utils.GetFormValue(ddlxilie.UniqueID);
            model.ChuanZhiId = Utils.GetFormValue(ddlchuanzhi.UniqueID);
            model.HangXianId = Utils.GetInt(Utils.GetFormValue(ddlhangxian.UniqueID));
            model.ChuFaGangKouId = Utils.GetInt(Utils.GetFormValue(ddlchufagangkou.UniqueID));
            model.DiDaGangKouId = Utils.GetInt(Utils.GetFormValue(ddldidagangkou.UniqueID));
            model.TianShu1 = Utils.GetInt(Utils.GetFormValue(txtxingchengtian.UniqueID));
            model.TianShu2 = Utils.GetInt(Utils.GetFormValue(txtxingchengwan.UniqueID));
            IList<MHangQiTuJingChengShiInfo> tujings = new List<MHangQiTuJingChengShiInfo>();

            model.TuJingChengShi = Utils.GetFormValue(txttujingchengshi.UniqueID); ;

            model.GongLue = Utils.GetYlEditorText(Request.Form[txtlvyougonglve.UniqueID]);
            model.QiShiJiaGeShuoMing = Utils.GetYlEditorText(Request.Form[txtqishijiageshuoming.UniqueID]);
            model.ChanPinTeSe = Utils.GetYlEditorText(Request.Form[txtchanpintese.UniqueID]);
            model.YouHuiXinXi = Utils.GetYlEditorText(Request.Form[txtyouhuixinxi.UniqueID]);
            //  model.youhuig = Utils.GetFormValue(txtchanpinmingcheng.UniqueID);优惠规则
            model.FeiYongShuoMing = Utils.GetYlEditorText(Request.Form[txtfeiyongshuoming.UniqueID]);
            model.YuDingXuZhi = Utils.GetYlEditorText(Request.Form[txtyudingxuzhi.UniqueID]);
            model.YouQingTiShi = Utils.GetYlEditorText(Request.Form[txtyouqingtishi.UniqueID]);
            model.KeDiKouJinFen = Utils.GetInt(Utils.GetFormValue(txtzuiduojifen.UniqueID));
            decimal jifenduihuan = Utils.GetDecimal(Utils.GetFormValue(txtjifenbili.UniqueID));
            model.JiFenDuiHuanBiLi = jifenduihuan != 0 ? jifenduihuan / 100M : jifenduihuan;
            decimal jifenbili = Utils.GetDecimal(Utils.GetFormValue(txtleijibili.UniqueID));
            model.JiFenLeiJiBiLi = jifenbili != 0 ? jifenbili / 100M : jifenbili;
            model.QianZhengQianZhu = Utils.GetYlEditorText(Request.Form[qianzhengqianzhu.UniqueID]);
            model.HangXianXingZhi = Utils.GetFormValue(hangxianxingzhi.UniqueID);
            model.FaPiaoKuaiDiJinE = Utils.GetDecimal(Utils.GetFormValue(kuaidifei.UniqueID));
            model.XiaoLiang1 = Utils.GetInt(Utils.GetFormValue(txtXiaoLiang.UniqueID));
            model.HaoPing = Utils.GetDecimal(Utils.GetFormValue(txtHaoPing.UniqueID));
            model.PaiXuId = Utils.GetInt(Utils.GetFormValue(txtpaixuid.UniqueID));

            model.LeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)Utils.GetInt(Utils.GetQueryStringValue("t"));

            #region fujians
            var items1 = upload1.Files;
            var items2 = upload1.YuanFiles;
            var items3 = new List<EyouSoft.Model.YlStructure.MFuJianInfo>();

            if (items1 != null && items1.Count > 0)
            {
                foreach (var item in items1)
                {
                    var item1 = new EyouSoft.Model.YlStructure.MFuJianInfo();
                    item1.Filepath = item.FilePath;
                    items3.Add(item1);
                }
            }

            if (items2 != null && items2.Count > 0)
            {
                foreach (var item in items2)
                {
                    var item1 = new EyouSoft.Model.YlStructure.MFuJianInfo();
                    item1.Filepath = item.FilePath;
                    items3.Add(item1);
                }
            }

            model.FuJians = items3;
            #endregion



            #region 行程安排
            //string[] qujians1 = Utils.GetFormValues("qujians");
            //string[] qujians2 = Utils.GetFormValues("qujiane");
            string[] qujian=Utils.GetFormValues("qujian");
            string[] jiaotonggongjus = Utils.GetFormValues("jiaotong");
            string[] bancis = Utils.GetFormValues("banci");
            string[] zaos = Utils.GetFormValues("zaocan");
            string[] zhongs = Utils.GetFormValues("wucan");
            string[] wans = Utils.GetFormValues("wancan");
            string[] zhusus = Utils.GetFormValues("zhusu");
            string[] neirongs = Utils.GetYlEditorValues("neirong");
            string[] files = Utils.GetFormValues("hide_Cruiseimg_file");
            IList<MHangQiXingChengInfo> xingchengs = new List<MHangQiXingChengInfo>();
            if (qujian != null && qujian.Length > 0)
            {
                for (int i = 0; i < qujian.Length; i++)
                {
                    MHangQiXingChengInfo xingcheng = new MHangQiXingChengInfo();
                    xingcheng.QuJian1 = qujian[i];
                    //xingcheng.QuJian2 = qujians2[i];
                    xingcheng.JiaoTongGongJu = jiaotonggongjus[i];
                    //xingcheng.BanCi = bancis[i];
                    xingcheng.BanCi = string.Empty;
                    xingcheng.Zao = zaos[i];
                    xingcheng.Zhong = zhongs[i];
                    xingcheng.Wan = wans[i];
                    xingcheng.ZhuSu = zhusus[i];
                    xingcheng.NeiRong = neirongs[i];
                    if (files[i] != null && files[i].Split('|').Length > 0)
                    {
                        xingcheng.Filepath = files[i].Split('|')[1];
                    }
                    xingchengs.Add(xingcheng);
                }
            }

            model.XingChengs = xingchengs;

            #endregion

            #region 附加产品
            string[] fuwuleixings = Utils.GetFormValues("selectfwlx");
            string[] fuwuxiangmus = Utils.GetFormValues("txtfwxm");
            string[] danjias = Utils.GetFormValues("txtdanjia");
            string[] jijiadanweis = Utils.GetFormValues("txtjijiadanwei");
            string[] chanpinjieshaos = Utils.GetYlEditorValues("txtchanpinjieshao");
            string[] fujiacpids = Utils.GetFormValues("fujiacpid");

            IList<MHangQiFuJiaChanPinInfo> fujias = new List<MHangQiFuJiaChanPinInfo>();
            if (fuwuleixings != null && fuwuleixings.Length > 0)
            {
                for (int i = 0; i < fuwuleixings.Length; i++)
                {
                    MHangQiFuJiaChanPinInfo fujia = new MHangQiFuJiaChanPinInfo();
                    fujia.LeiXingId = Utils.GetInt(fuwuleixings[i]);
                    fujia.XiangMu = fuwuxiangmus[i];
                    fujia.DanJia = Utils.GetDecimal(danjias[i]);
                    fujia.DanWei = jijiadanweis[i];
                    fujia.JieShao = chanpinjieshaos[i];
                    fujia.FuJiaChanPinId = fujiacpids[i];

                    if (fujia.LeiXingId == 0) continue;

                    fujias.Add(fujia);
                }
            }

            model.FuJiaChanPins = fujias;

            #endregion

            Response.Clear();
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Write(UtilsCommons.AjaxReturnJson("0", msg));
                Response.End();
            }
            if (string.IsNullOrEmpty(Id))
            {
                if (new EyouSoft.BLL.YlStructure.BHangQi().InsertHangQi(model) == 1)
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("1", "添加成功"));
                }
                else
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("0", "添加失败"));
                }
            }
            else
            {
                model.HangQiId = Id;
                if (new EyouSoft.BLL.YlStructure.BHangQi().UpdateHangQi(model) == 1)
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("1", "修改成功"));
                }
                else
                {
                    Response.Write(UtilsCommons.AjaxReturnJson("0", "修改失败"));
                }
            }
            Response.End();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        void initPage()
        {
            initDDL();
            var model = new EyouSoft.BLL.YlStructure.BHangQi().GetHangQiInfo(Utils.GetQueryStringValue("id"));
            if (model != null)
            {
                txtSeoTitle.Text = model.SeoTitle;
                txtSeoKeyword.Text = model.SeoKeyword;
                txtSeoDescription.Text = model.SeoDescription;

                txtchanpinmingcheng.Text = model.MingCheng;
                txtchanpinbainhao.Text = model.BianHao;
                txtqishijiage.Text = model.QiShiJiaGe1.ToString("F2");
                SupplierControl1.HideID = model.GysId;
                SupplierControl1.Name = model.GysName;
                YLCompanySelect1.ComPid = model.GongSiId;
                YLCompanySelect1.CompName = model.GongSiName;

                //if (ddlxilie.Items.FindByValue(model.XiLieId) != null)
                //    ddlxilie.Items.FindByValue(model.XiLieId).Selected = true;
                //if (ddlchuanzhi.Items.FindByValue(model.ChuanZhiId) != null)
                //    ddlchuanzhi.Items.FindByValue(model.ChuanZhiId).Selected = true;
                xlbh.Value = model.XiLieId;
                czbh.Value = model.ChuanZhiId;


                if (ddlhangxian.Items.FindByValue(model.HangXianId.ToString()) != null)
                    ddlhangxian.Items.FindByValue(model.HangXianId.ToString()).Selected = true;
                if (ddlchufagangkou.Items.FindByValue(model.ChuFaGangKouId.ToString()) != null)
                    ddlchufagangkou.Items.FindByValue(model.ChuFaGangKouId.ToString()).Selected = true;
                if (ddldidagangkou.Items.FindByValue(model.DiDaGangKouId.ToString()) != null)
                    ddldidagangkou.Items.FindByValue(model.DiDaGangKouId.ToString()).Selected = true;


                txtxingchengtian.Text = model.TianShu1.ToString();
                txtxingchengwan.Text = model.TianShu2.ToString();
                txttujingchengshi.Text = model.TuJingChengShi;

                txtlvyougonglve.Text = model.GongLue;
                txtqishijiageshuoming.Text = model.QiShiJiaGeShuoMing;
                txtchanpintese.Text = model.ChanPinTeSe;
                txtyouhuixinxi.Text = model.YouHuiXinXi;
                txtfeiyongshuoming.Text = model.FeiYongShuoMing;
                txtyudingxuzhi.Text = model.YuDingXuZhi;
                txtyouqingtishi.Text = model.YouQingTiShi;
                txtzuiduojifen.Text = model.KeDiKouJinFen.ToString();
                txtjifenbili.Text = (model.JiFenDuiHuanBiLi * 100).ToString("F2");
                txtleijibili.Text = (model.JiFenLeiJiBiLi * 100).ToString("F2");

                qianzhengqianzhu.Text = model.QianZhengQianZhu;
                hangxianxingzhi.Text = model.HangXianXingZhi;

                kuaidifei.Text = model.FaPiaoKuaiDiJinE.ToString("F2");

                txtXiaoLiang.Text = model.XiaoLiang1.ToString();
                txtHaoPing.Text = model.HaoPing.ToString("F2");
                txtpaixuid.Text = model.PaiXuId.ToString();

                #region fujians
                var items = new List<MFileInfo>();

                if (model.FuJians != null && model.FuJians.Count > 0)
                {
                    foreach (var item in model.FuJians)
                    {
                        MFileInfo file = new MFileInfo();
                        file.FilePath = item.Filepath;
                        items.Add(file);
                    }
                }
                upload1.YuanFiles = items;
                #endregion


                YLXingCheng1.SetPlanList = model.XingChengs;
                if (model.FuJiaChanPins != null && model.FuJiaChanPins.Count > 0)
                {
                    PlaceHolder1.Visible = false;
                    rptlist.DataSource = model.FuJiaChanPins;
                    rptlist.DataBind();
                }

            }
        }
        /// <summary>
        /// 初始化下拉框
        /// </summary>
        /// 
        void initDDL()
        {
            EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing leixing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)Utils.GetInt(Utils.GetQueryStringValue("t"));
            int recordCount = 0;
            //var xilies = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetXiLies(SiteUserInfo.CompanyId, 1000, 1, ref recordCount, null);
            //if (xilies != null && xilies.Count > 0)
            //{
            //    this.ddlxilie.DataTextField = "MingCheng";
            //    this.ddlxilie.DataValueField = "XiLieId";
            //    this.ddlxilie.DataSource = xilies;
            //    this.ddlxilie.DataBind();
            //}
            //var chuanzhis = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetChuanZhis(SiteUserInfo.CompanyId, 1000, 1, ref recordCount, null);
            //if (chuanzhis != null && chuanzhis.Count > 0)
            //{
            //    this.ddlchuanzhi.DataTextField = "MingCheng";
            //    this.ddlchuanzhi.DataValueField = "ChuanZhiId";
            //    this.ddlchuanzhi.DataSource = chuanzhis;
            //    this.ddlchuanzhi.DataBind();
            //}



            var hangxians = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetJiChuXinXis(SiteUserInfo.CompanyId, new EyouSoft.Model.YlStructure.MJiChuXinXiChaXunInfo { LeiXing = EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.航线, YouLunLeiXing = leixing });
            if (hangxians != null && hangxians.Count > 0)
            {
                this.ddlhangxian.DataTextField = "MingCheng";
                this.ddlhangxian.DataValueField = "XinXiId";
                this.ddlhangxian.DataSource = hangxians;
                this.ddlhangxian.DataBind();
            }
            var gangkous = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetJiChuXinXis(SiteUserInfo.CompanyId, new EyouSoft.Model.YlStructure.MJiChuXinXiChaXunInfo { LeiXing = EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.港口, YouLunLeiXing = leixing });
            if (gangkous != null && gangkous.Count > 0)
            {
                this.ddlchufagangkou.DataTextField = this.ddldidagangkou.DataTextField = "MingCheng";
                this.ddlchufagangkou.DataValueField = this.ddldidagangkou.DataValueField = "XinXiId";
                this.ddlchufagangkou.DataSource = this.ddldidagangkou.DataSource = gangkous;
                this.ddlchufagangkou.DataBind();
                this.ddldidagangkou.DataBind();
            }


        }


        /// <summary>
        /// 权限控制
        /// </summary>
        void PowerControl()
        {
            //if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_支付方式栏目))
            //{
            //    Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.系统设置_基础设置_支付方式栏目, false);
            //    return;
            //}
        }

        protected string getOpts(string val)
        {
            EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing leixing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing)Utils.GetInt(Utils.GetQueryStringValue("t"));
            StringBuilder strbu = new StringBuilder();
            strbu.Append("<option value=\"\">请选择</option>");
            var fujias = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetJiChuXinXis(SiteUserInfo.CompanyId, new EyouSoft.Model.YlStructure.MJiChuXinXiChaXunInfo { LeiXing = EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.附加产品类型, YouLunLeiXing = leixing });
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

        #region AJAX
        /// <summary>
        /// 获取系列下拉选项
        /// </summary>
        void getXL()
        {
            int recordCount = 0;
            string comPid = Utils.GetQueryStringValue("gongsi");
            string selectID = Utils.GetQueryStringValue("sid");
            StringBuilder xilieStr = new StringBuilder();
            xilieStr.Append("<option value=\"\">请选择</option>");
            var xilies = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetXiLies(SiteUserInfo.CompanyId, 1000, 1, ref recordCount, new MXiLieChaXunInfo() { GongSiId = comPid });
            if (xilies != null && xilies.Count > 0)
            {
                for (int i = 0; i < xilies.Count; i++)
                {
                    if (xilies[i].XiLieId == selectID)
                    {
                        xilieStr.AppendFormat("<option  value=\"{0}\"  selected=\"selected\">{1}</option> ", xilies[i].XiLieId, xilies[i].MingCheng);
                    }
                    else
                    {
                        xilieStr.AppendFormat("<option  value=\"{0}\" >{1}</option>", xilies[i].XiLieId, xilies[i].MingCheng);
                    }
                }

            }
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson("1", "", xilieStr.ToString()));
            Response.End();
            GC.Collect();
        }
        /// <summary>
        /// 获取船只
        /// </summary>
        void getCZ()
        {
            int recordCount = 0;
            string comPid = Utils.GetQueryStringValue("gongsi");
            string xilieid = Utils.GetQueryStringValue("xilie");
            string selectID = Utils.GetQueryStringValue("sid");
            StringBuilder xilieStr = new StringBuilder();
            xilieStr.Append("<option value=\"\">请选择</option>");
            var chuanzhis = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetChuanZhis(SiteUserInfo.CompanyId, 1000, 1, ref recordCount, new MChuanZhiChaXunInfo() { XiLieId = xilieid, });
            if (chuanzhis != null && chuanzhis.Count > 0)
            {
                for (int i = 0; i < chuanzhis.Count; i++)
                {
                    if (chuanzhis[i].ChuanZhiId == selectID)
                    {
                        xilieStr.AppendFormat("<option  value=\"{0}\"  selected=\"selected\">{1}</option> ", chuanzhis[i].ChuanZhiId, chuanzhis[i].MingCheng);
                    }
                    else
                    {
                        xilieStr.AppendFormat("<option  value=\"{0}\" >{1}</option>", chuanzhis[i].ChuanZhiId, chuanzhis[i].MingCheng);
                    }
                }

            }
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson("1", "", xilieStr.ToString()));
            Response.End();
        }
        #endregion





    }
}
