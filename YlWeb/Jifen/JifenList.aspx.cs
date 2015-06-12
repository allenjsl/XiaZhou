using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;
using EyouSoft.Model.YlStructure;
using EyouSoft.Model.EnumType.YlStructure;
using EyouSoft.BLL.YlStructure;
using EyouSoft.Model.SSOStructure;

namespace EyouSoft.YlWeb.Jifen
{
    public partial class JifenList : EyouSoft.YlWeb.WzPage
    {
        protected const int PageSize = 12;

        private int _pageIndex = 1;

        private int _recordCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.IsXianShiHengFu = false;
            if (!Page.IsPostBack)
            {
                initData();
                InitGunDongTuPian();
            }
        }

        protected void initData()
        {
            string uptype=Utils.GetQueryStringValue("uptype");
            string chatype=Utils.GetQueryStringValue("chatype");
            #region 会员积分
            MYlHuiYuanInfo userInfo = null;
            bool IsLogin = EyouSoft.Security.Membership.YlHuiYuanProvider.IsLogin(out userInfo);
            if (IsLogin)
            {
                var huiYuanInfo = new EyouSoft.BLL.YlStructure.BHuiYuan().GetHuiYuanInfo(userInfo.HuiYuanId);
                phlLoginY.Visible = false;
                phLoginIn.Visible = true;
                ltr_jfNumber.Text = huiYuanInfo.KeYongJiFen.ToString("0.00");
               // ltr_jfCar.Text = "";
            }
            #endregion
            MWzJiFenShangPinChaXunInfo Chaxun = new MWzJiFenShangPinChaXunInfo();
            if(uptype=="up")
            {
                switch (Utils.GetInt(chatype))
                {
                    case (int)JiFenDuiHuanFangShi.积分:
                        Chaxun.PaiXu = 0;
                        break;
                    case (int)JiFenDuiHuanFangShi.积分礼品卡:
                        Chaxun.PaiXu = 2;
                        break;
                    case (int)JiFenDuiHuanFangShi.积分现金:
                        Chaxun.PaiXu = 4;
                        break;
                    default:
                        Chaxun.PaiXu = 0;
                        break;
                }
            }
            else if(uptype=="down")
            {
                switch (Utils.GetInt(chatype))
                {
                    case (int)JiFenDuiHuanFangShi.积分:
                        Chaxun.PaiXu = 1;
                        break;
                    case (int)JiFenDuiHuanFangShi.积分礼品卡:
                        Chaxun.PaiXu = 3;
                        break;
                    case (int)JiFenDuiHuanFangShi.积分现金:
                        Chaxun.PaiXu = 5;
                        break;
                    default:
                        Chaxun.PaiXu = 1;
                        break;
                }
            }
            BDuiHuan bll=new BDuiHuan();
            _pageIndex = UtilsCommons.GetPadingIndex();
           var list= bll.GetJiFenShangPins(YuMingInfo.CompanyId, PageSize, _pageIndex, ref _recordCount, Chaxun);
           if (list != null && list.Count > 0)
           {
               rptList.DataSource = list;
               rptList.DataBind();

               RegisterScript(string.Format("pConfig.pageSize={0};pConfig.pageIndex={1};pConfig.recordCount={2};", PageSize, _pageIndex, _recordCount));
           }
        }

        #region 私有方法
        /// <summary>
        /// 图片路径
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected string ImageView(object list)
        {
            string sb = "";
            if (list != null)
            {
                var model = (List<EyouSoft.Model.YlStructure.MFuJianInfo>)list;
                if (model != null && model.Count > 0)
                {
                    sb = model[0].Filepath;
                }
            }
            return sb;
        }
        /// <summary>
        /// 兑换价格
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        protected string JifenHtml(object values)
        {
            StringBuilder sb = new StringBuilder();
            if (values != null)
            {
                var model = (List<EyouSoft.Model.YlStructure.MWzJiFenShangPinFangShiInfo>)values;
                if (model != null && model.Count > 0)
                {
                    for (int i = 0; i < model.Count; i++)
                    {
                        switch (model[i].FangShi)
                        {
                            case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分:
                                sb.AppendFormat("<span class=\"jf_score\">{0}</span>分</br>", model[i].JiFen.ToString("F0"));
                                break;
                            case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分礼品卡:
                                sb.AppendFormat("<span class=\"jf_score\">{0}</span>分 + <span class=\"jf_gprice\">{1}</span>元礼品卡</br>", model[i].JiFen.ToString("F0"), model[i].JinE.ToString("F2"));
                                break;
                            case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分现金:
                                sb.AppendFormat("<span class=\"jf_score\">{0}</span>分 + <span class=\"font_f00\">{1}</span>元</br>", model[i].JiFen.ToString("F0"), model[i].JinE.ToString("F2"));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// init gundongtupin
        /// </summary>
        void InitGunDongTuPian()
        {
            int recordCount = 0;
            var chaXun = new EyouSoft.Model.YlStructure.MWzGuangGaoChaXunInfo();
            chaXun.WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.积分商城首页轮换图片;
            var items = new EyouSoft.BLL.YlStructure.BWz().GetGuangGaos(YuMingInfo.CompanyId, 5, 1, ref recordCount, chaXun);

            StringBuilder s1 = new StringBuilder();
            StringBuilder s2 = new StringBuilder();

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    string url = item.Url;
                    string target = "target=\"_blank\"";

                    if (string.IsNullOrEmpty(url))
                    {
                        url = "javascript:void(0)";
                        target = string.Empty;
                    }

                    s1.AppendFormat("<li><a href=\"{0}\" {1}><img src=\"{2}\" /></a></li>", url, target, ErpFilepath + item.Filepath);

                    s2.AppendFormat("<li><a href=\"javascript:void(0)\"></a></li>");
                }
            }

            ltr1.Text = s1.ToString();
            ltr2.Text = s2.ToString();
        }
    }
}
