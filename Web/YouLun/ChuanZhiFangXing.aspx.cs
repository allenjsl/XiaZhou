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
namespace EyouSoft.Web.YouLun
{
    public partial class ChuanZhiFangXing : BackPage
    {
        protected string newfangxingid = Guid.NewGuid().ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initList();
            }
            if (Utils.GetQueryStringValue("save") == "save") BaoCun();
            if (Utils.GetQueryStringValue("doajax") == "getguid") GetGuid();
        }
        #region 私有方法
        void GetGuid()
        {
            Response.Clear();
            Response.Write(Guid.NewGuid().ToString());
            Response.End();
        }
        /// <summary>
        /// 保存操作
        /// </summary>
        void BaoCun()
        {
            var model = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetChuanZhiInfo(Utils.GetQueryStringValue("id"));
            model.FangXings = GetDataList();
            model.BeiShus = GetBeiShus();
            if (new EyouSoft.BLL.YlStructure.BJiChuXinXi().UpdateChuanZhi(model) == 1)
            {
                RCWE(UtilsCommons.AjaxReturnJson("1", "修改成功"));
            }
            else
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "修改失败"));
            }

        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        void initList()
        {
            var model = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetChuanZhiInfo(Utils.GetQueryStringValue("id"));
            if (model != null && model.FangXings.Count > 0)
            {
                phrPanel.Visible = false;
                rptlist.DataSource = model.FangXings;
                rptlist.DataBind();
            }
        }

        /// <summary>
        /// get beishu xinxi
        /// </summary>
        /// <returns></returns>
        IList<MChuanZhiFangXingBeiShu> GetBeiShus()
        {
            var ids = Utils.GetFormValues("hidid");
            var fangxingids = Utils.GetFormValues("hidfangxingid");
            var leixings = Utils.GetFormValues("ddlleixing");
            var beishus = Utils.GetFormValues("beishu");
            var beishu1s = Utils.GetFormValues("beishu1");
            var beishu2s = Utils.GetFormValues("beishu2");
            var l = new List<MChuanZhiFangXingBeiShu>();

            if (ids.Length > 0)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    if (!string.IsNullOrEmpty(leixings[i] + beishus[i])&&!string.IsNullOrEmpty(fangxingids[i]))
                    {
                        l.Add(new MChuanZhiFangXingBeiShu() { Id = !string.IsNullOrEmpty(ids[i]) ? ids[i] : Guid.NewGuid().ToString(), FangXingId = fangxingids[i], RenYuanLeiXing = Utils.GetInt(leixings[i]), BeiShu = Utils.GetDecimal(beishus[i]), BeiShu1 = Utils.GetDecimal(beishu1s[i]), BeiShu2 = Utils.GetDecimal(beishu2s[i]) });
                    }
                }
            }

            return l;
        }

        /// <summary>
        /// 获取船只美食列表
        /// </summary>
        IList<MChuanZhiFangXingInfo> GetDataList()
        {

            string[] MingChengs = Utils.GetFormValues("ddlfangxing");
            string[] ShuLiangs = Utils.GetFormValues("ShuLiang");
            string[] MianJis = Utils.GetFormValues("MianJi");
            string[] LouCengs = Utils.GetFormValues("LouCeng");
            string[] JieGous = Utils.GetFormValues("JieGou");
            string[] ChuangWeiPeiZhis = Utils.GetFormValues("ChuangWeiPeiZhi");
            string[] SheShis = Utils.GetYlEditorValues("SheShi");
            string[] YongPins = Utils.GetYlEditorValues("YongPin");
            string[] JieShaos = Utils.GetYlEditorValues("JieShao");
            string[] Filepaths = Utils.GetFormValues("hide_Cruiseimg_file");
            var pfangxingids = Utils.GetFormValues("phidfangxingid");
            string[] txtmingcheng = Utils.GetFormEditorValues("txtmingcheng");

            if (MingChengs.Length > 0)
            {
                IList<MChuanZhiFangXingInfo> list = new List<MChuanZhiFangXingInfo>();
                for (int i = 0; i < MingChengs.Length; i++)
                {
                    if (!String.IsNullOrEmpty(MingChengs[i].ToString()))
                    {
                        MChuanZhiFangXingInfo model = new MChuanZhiFangXingInfo();

                        model.FangXingId = pfangxingids[i];
                        model.MingChengId = MingChengs[i];
                        model.ShuLiang = ShuLiangs[i];
                        model.MianJi = MianJis[i];
                        model.LouCeng = LouCengs[i];
                        model.JieGou = JieGous[i];
                        model.ChuangWeiPeiZhi = ChuangWeiPeiZhis[i];
                        model.SheShi = SheShis[i];
                        model.YongPin = YongPins[i];
                        model.JieShao = JieShaos[i];

                        //var jcxxinfo = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetJiChuXinXiInfo(Utils.GetInt(model.MingChengId));
                        //if (jcxxinfo != null) model.MingCheng = jcxxinfo.MingCheng;

                        model.MingCheng = txtmingcheng[i];

                        if (!string.IsNullOrEmpty(Filepaths[i]))
                            model.Filepath = Filepaths[i].Split('|')[1];

                        list.Add(model);
                    }
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        #endregion

        protected string getFilePath(string filepath)
        {
            if (string.IsNullOrEmpty(filepath))
            {
                return "";
            }
            else
            {
                return string.Format("<div data-class='span_Cruiseimg_file' class='upload_filename'><a target='_blank' href='{0}'>查看附件</a><a href='javascript:void(0);' title='删除附件' onclick='pageDataJs.RemoveFile(this);'><img src='/images/cha.gif' border='0'></a> </div>", filepath);
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

        private void rptlist_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                var PlaceHolder1 = (System.Web.UI.WebControls.PlaceHolder)e.Item.FindControl("PlaceHolder1");
                var rpt = (System.Web.UI.WebControls.Repeater)e.Item.FindControl("rpt");
                var beishus = (IList<MChuanZhiFangXingBeiShu>)DataBinder.Eval(e.Item.DataItem, "beishus");

                if (beishus != null && beishus.Count > 0)
                {
                    rpt.DataSource = beishus;
                    rpt.DataBind();
                    PlaceHolder1.Visible = false;
                }
            }
        }

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.rptlist.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.rptlist_ItemDataBound);

        }
        #endregion
    }
}
