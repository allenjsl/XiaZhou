using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.YlStructure;
using System.Text;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.UserControl
{

    public partial class ProductTeJia : System.Web.UI.UserControl
    {
        EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing _LeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮;
        protected string HQURL = "/hangqi/";

        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing LeiXing
        {
            get { return _LeiXing; }
            set { _LeiXing = value; if (_LeiXing == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮) HQURL = "/hangqi/HY"; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitTopInfo();
            }
        }

        private void InitTopInfo()
        {
            MWzYuMingInfo m = null;
            m = EyouSoft.Security.Membership.YlHuiYuanProvider.GetYuMingInfo();
            EyouSoft.BLL.YlStructure.BHangQi bll = new EyouSoft.BLL.YlStructure.BHangQi();
            int recordCount = 0;
            MHangQiChaXunInfo chaxun = new MHangQiChaXunInfo()
            {
                IsYouXiao = true,                
                LeiXing = LeiXing,
                BiaoQian = EyouSoft.Model.EnumType.YlStructure.HangQiBiaoQian.特价推荐,
                PaiXu=2
            };
            var list = bll.GetHangQis(m.CompanyId, 4, 1, ref recordCount, chaxun);
            if (list != null)
            {
                rptList_tj_top.DataSource = list;
                rptList_tj_top.DataBind();
                txtHangQiName.Text = HangQiNameHtml(list);
            }
            chaxun.BiaoQian = EyouSoft.Model.EnumType.YlStructure.HangQiBiaoQian.特价;
            var list2 = bll.GetHangQis(m.CompanyId, 7, 1, ref recordCount, chaxun);
            if (list2 != null)
            {
                rptList_tj_down.DataSource = list2;
                rptList_tj_down.DataBind();
            }
        }

        #region 私有方法
        /// <summary>
        /// 出发时间
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string ChuGangTimeHtml(object obj, object type)
        {
            string str = "";
            if (obj != null)
            {
                var model = (IList<MHangQiRiQiInfo>)obj;
                if (model != null && model.Count > 0)
                {
                    if (type.ToString() == "top")
                    {
                        str = model[0].RiQi.ToString("yyyy年MM月dd日");
                    }
                    else
                    {
                        /*for (int i = 0; i < 3; i++)
                        {
                            str += model[0].RiQi.ToString("MM月dd日") + "/";
                        }
                        str = str.Substring(0, str.Length - 1);*/

                        int i = 0;

                        foreach (var item in model)
                        {
                            if (i == 3) break;
                            str += item.RiQi.ToString("MM月dd日") + "/";
                            i++;
                        }

                        if (!string.IsNullOrEmpty(str)) str = str.Trim('/');
                    }
                }
            }
            return str;
        }
        /// <summary>
        /// 附件
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string FujianHtml(object obj, object type,object hangQiId)
        {
            StringBuilder sb = new StringBuilder();
            if (obj != null)
            {
                string view = "";
                string width = "width=\"106\" height=\"76\"";
                var list = (IList<EyouSoft.Model.YlStructure.MFuJianInfo>)obj;
                if (list != null && list.Count > 0)
                {
                    if (type.ToString() == "top")
                    {
                        view = "style=\"display: block;\"";
                        width = "width=\"450\" height=\"278\"";
                        sb.AppendFormat("<li {0}><img {2} src=\"{1}\"  id='img_tejia_{3}'></li>", view, EyouSoft.YlWeb.TuPian.F1(Utils.GetErpFilepath() + list[0].Filepath,450,278), width, hangQiId.ToString());
                    }
                    else
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            sb.AppendFormat("<li {0}><a href='javascript:void(0)'><img {2} class=\"i_tejia_img_2\" src=\"{1}\" data-src='{3}'></a></li>", view, EyouSoft.YlWeb.TuPian.F1(Utils.GetErpFilepath() + list[i].Filepath, 106, 76), width, EyouSoft.YlWeb.TuPian.F1(Utils.GetErpFilepath() + list[i].Filepath, 450, 278));
                        }
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string HangQiNameHtml(IList<EyouSoft.Model.YlStructure.MHangQiInfo> list)
        {
            StringBuilder sb = new StringBuilder();
            if (list != null && list.Count > 0)
            {                
                for (int i = 0; i < list.Count; i++)
                {
                    string className = "class=\"normal\"";
                    if (i == 0)
                    {
                        className = "class=\"current\"";
                    }
                    string name = list[i].ChuanZhiName + "&nbsp;" + list[i].TianShu1.ToString() + "日" + list[i].TianShu2.ToString() + "晚" +
                        JiChuXinXi(list[i].ChuFaGangKouId) + "出发";
                    sb.AppendFormat("<li {0}>{1}</li>", className, name);
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 港口基础信息
        /// </summary>
        /// <param name="jiChuXinXiId"></param>
        /// <returns></returns>
        private string JiChuXinXi(int jiChuXinXiId)
        {
            string str = "";
            EyouSoft.BLL.YlStructure.BJiChuXinXi bll = new EyouSoft.BLL.YlStructure.BJiChuXinXi();
            var list = bll.GetJiChuXinXiInfo(jiChuXinXiId);
            if (list != null)
            {
                str = list.MingCheng;
            }
            return str;
        }
        #endregion
    }
}