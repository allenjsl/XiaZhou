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
using System.Text;

namespace EyouSoft.YlWeb.Huiyuan
{
    public partial class JiFen : EyouSoft.YlWeb.HuiYuanPage
    {
        protected int pageSize = 10;
        protected int pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
        protected int recordCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.InitRpt();
            InitInfo();
        }

        /*void InitRpt()
        {
            var l = new BLL.YlStructure.BHuiYuan().GetHuiYuanJiFenMingXis(this.HuiYuanInfo.HuiYuanId, pageSize, pageIndex, ref recordCount, this.GetChaXun());

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

        MHuiYuanJiFenMxChaXunInfo GetChaXun()
        {
            var m = new MHuiYuanJiFenMxChaXunInfo()
            {
                HuiYuanId=this.HuiYuanInfo.HuiYuanId
            };
            return m;
        }*/

        /// <summary>
        /// get chaxun info
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MHuiYuanJiFenMxChaXunInfo GetChaXunInfo()
        {
            return null;
        }

        void InitInfo()
        {
            var chaXun = GetChaXunInfo();
            var items = new EyouSoft.BLL.YlStructure.BHuiYuan().GetHuiYuanJiFenMingXis(HuiYuanInfo.HuiYuanId, pageSize, pageIndex, ref recordCount, chaXun);
            if (items != null && items.Count > 0)
            {
                StringBuilder s = new StringBuilder();

                foreach (var item in items)
                {
                    string s1 = string.Empty;//cp url
                    decimal jiFen = 0;
                    string s2 = string.Empty;

                    switch (item.LeiXing)
                    {
                        case EyouSoft.Model.EnumType.YlStructure.JiFenMxLeiXing.积分兑换商品:
                            s1 = "/jifen/jifeninfo.aspx?id=" + item.CPId;
                            jiFen = 0 - item.JiFen;
                            s2 = "积分兑换商品";
                            break;
                        case EyouSoft.Model.EnumType.YlStructure.JiFenMxLeiXing.下单抵扣积分:
                            jiFen = 0 - item.JiFen;
                            s2 = "下单抵扣积分";
                            if (item.IsTuanGou)
                            {
                                s1 = "/tuangou/tuangouxiangqing.aspx?tuangouid=" + item.TuanGouId;
                            }
                            else
                            {
                                if (item.HQLeiXing.Value == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮)
                                {
                                    s1 = "/hangqi/" + item.CPId+".html";
                                }
                                else if (item.HQLeiXing.Value == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮)
                                {
                                    s1 = "/hangqi/HY" + item.CPId+".html";
                                }
                            }
                            break;
                        case EyouSoft.Model.EnumType.YlStructure.JiFenMxLeiXing.下单累积积分:
                            jiFen = item.JiFen;
                            s2 = "下单累积积分";
                            if (item.IsTuanGou)
                            {
                                s1 = "/tuangou/tuangouxiangqing.aspx?tuangouid=" + item.TuanGouId;
                            }
                            else
                            {
                                if (item.HQLeiXing.Value == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮)
                                {
                                    s1 = "/hangqi/hangqiinfo.aspx?id=" + item.CPId;
                                }
                                else if (item.HQLeiXing.Value == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮)
                                {
                                    s1 = "/hangqi/haiyanginfo.aspx?id=" + item.CPId;
                                }
                            }

                            break;
                    }

                    s.AppendFormat("<tr>");
                    s.AppendFormat("<td align='left'>{0}<a class='blue' target='_blank' href='{1}'>【详情】</a></td>", item.CPName, s1);
                    s.AppendFormat("<td align='center'>{0:yyyy-MM-dd}</td>", item.IssueTime);
                    s.AppendFormat("<td align='center'><b class='font14'>{0:F2}</b></td>", item.JinE);
                    s.AppendFormat("<td align='center'><b class='font14' title='{1}'>{0:F2}</b></td>", jiFen, s2);
                    s.AppendFormat("</tr>");
                }

                ltr0.Text = s.ToString();
            }
            else
            {
                phdNoDat.Visible = true;
            }
        }
    }
}
