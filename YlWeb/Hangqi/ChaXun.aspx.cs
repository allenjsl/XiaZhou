using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Common;

namespace EyouSoft.YlWeb.Hangqi
{
    public partial class ChaXun : EyouSoft.YlWeb.WzPage
    {
        public EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing? LeiXing = null;
        protected string S1 = "游轮";
        protected string S2 = "登船地址";
        protected string LeiXing1 = "-1";
        protected int recordCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            LeiXing = (EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing?)Utils.GetEnumValueNull(typeof(EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing), Utils.GetQueryStringValue("lx"));
            //if (!LeiXing.HasValue) LeiXing = EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.长江游轮;

            //if (Utils.GetQueryStringValue("dotype") == "getrx") InitRX1();

            if(LeiXing.HasValue)
            Master.LeiXing = LeiXing.Value;
            Master.IsXianShiHengFu = false;

            if (LeiXing.HasValue)
            {
                if (LeiXing.Value == EyouSoft.Model.EnumType.YlStructure.YouLunLeiXing.海洋邮轮)
                {
                    S1 = "邮轮";
                    S2 = "出发港口";

                    phgs.Visible = true;
                    phxl.Visible = false;


                    Master.WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.海洋游轮横幅;
                }
                else
                {
                    phgs.Visible = false;
                    phxl.Visible = true;


                    Master.WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.长江游轮横幅;
                }
            }
            if (LeiXing.HasValue) LeiXing1 = ((int)LeiXing.Value).ToString();

            InitHX();
            InitXL();
            InitGS();
            InitGK();
            InitYF();

            //InitRX();
        }

        void InitHX()
        {
            var items = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetJiChuXinXis(YuMingInfo.CompanyId, new EyouSoft.Model.YlStructure.MJiChuXinXiChaXunInfo { LeiXing = EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.航线, YouLunLeiXing = LeiXing });
            rptHX.DataSource = items;
            rptHX.DataBind();
        }

        void InitXL()
        {
            var chaXun = new EyouSoft.Model.YlStructure.MXiLieChaXunInfo();
            chaXun.GongSiLeiXing = LeiXing;
            int recordCount=0;

            var items = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetXiLies(YuMingInfo.CompanyId, 1000, 1, ref recordCount, chaXun);
            rptXL.DataSource = items;
            rptXL.DataBind();
        }

        void InitGK()
        {
            var items = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetJiChuXinXis(YuMingInfo.CompanyId, new EyouSoft.Model.YlStructure.MJiChuXinXiChaXunInfo { LeiXing = EyouSoft.Model.EnumType.YlStructure.JiChuXinXiLeiXing.港口, YouLunLeiXing = LeiXing });
            rptGK.DataSource = items;
            rptGK.DataBind();
        }

        void InitYF()
        {
            StringBuilder s = new StringBuilder();
            DateTime time = DateTime.Now;
            for (int i = 0; i < 12; i++)
            {
                s.AppendFormat("<dd data-v='{1}'><a href=\"javascript:;\">{0}</a></dd>", time.AddMonths(i).ToString("yyyy年M月"), time.AddMonths(i).ToString("yyyy-MM"));
            }
            ltrYF.Text = s.ToString();
        }

        void InitGS()
        {
            int recordCount = 0;
            var chaXun = new EyouSoft.Model.YlStructure.MGongSiChaXunInfo();
            chaXun.GongSiLeiXing = LeiXing;

            var items = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetGongSis(YuMingInfo.CompanyId, 1000, 1, ref recordCount, chaXun);

            rptGS.DataSource = items;
            rptGS.DataBind();
        }
    }
}
