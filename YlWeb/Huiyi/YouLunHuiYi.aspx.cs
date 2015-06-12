using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EyouSoft.YlWeb.Huiyi
{
    public partial class YouLunHuiYi : EyouSoft.YlWeb.WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.游轮会议横幅;
            if (!IsPostBack)
            {
                GetAdv(EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.游轮会议长江游轮图片);
                GetAdv(EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.游轮会议海洋游轮图片);
                GetInfo(EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮会议简介);
                GetInfo(EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮会议简介);
            }
        }
        private void GetAdv(EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi type)
        {
            EyouSoft.BLL.YlStructure.BWz bll = new EyouSoft.BLL.YlStructure.BWz();
            int recordcount = 0;
            var list = bll.GetGuangGaos(YuMingInfo.CompanyId, 1, 1, ref recordcount, new EyouSoft.Model.YlStructure.MWzGuangGaoChaXunInfo { WeiZhi = type });
            if (list != null && list.Count > 0)
            {
                if (type == EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.游轮会议长江游轮图片)
                    ImgChangJiang.Src = ErpFilepath + list[0].Filepath;
                else
                    ImgHaiYang.Src = ErpFilepath + list[0].Filepath;
            }
        }
        private void GetInfo(EyouSoft.Model.EnumType.YlStructure.WzKvKey type)
        {
            EyouSoft.BLL.YlStructure.BWz bll = new EyouSoft.BLL.YlStructure.BWz();

            var KyInfo = bll.GetKvInfo(YuMingInfo.CompanyId, type);
            if (KyInfo != null)
            {
                if (type == EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮会议简介)
                {
                    this.Content_HY.InnerHtml = KyInfo.V;
                }
                else
                {
                    this.Content_CJ.InnerHtml = KyInfo.V;
                }
            }
        }
    }
}
