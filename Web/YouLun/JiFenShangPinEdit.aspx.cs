using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using Web.YouLun.WUC;

namespace EyouSoft.Web.YouLun
{
    /// <summary>
    /// YL-积分兑换商品新增修改
    /// </summary>
    public partial class JiFenShangPinEdit : EyouSoft.Common.Page.BackPage
    {
        string ShangPinId = string.Empty;
        protected string Status = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();
            ShangPinId = Utils.GetQueryStringValue("editid");
            if (Utils.GetQueryStringValue("dotype") == "baocun") BaoCun();
            InitEditInfo();
        }

        #region private members
        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.游轮管理_积分兑换_栏目))
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "没有权限"));
            }
        }

        /// <summary>
        /// init edit info
        /// </summary>
        void InitEditInfo()
        {
            if (string.IsNullOrEmpty(ShangPinId)) return;

            var info = new EyouSoft.BLL.YlStructure.BDuiHuan().GetJiFenShangPinInfo(ShangPinId);
            if (info == null) return;

            txtMingCheng.Value = info.MingCheng;
            txtShuLiang.Value = info.ShuLiang.ToString();
            txtShuoMing.Value = info.ShuoMing;
            txtXuZhi.Value = info.XuZhi;
            txtPeiSongFangShi.Value = info.PeiSongFangShi;
            txtFaPiaoKuaiDiJinE.Value = info.FaPiaoKuaiDiJinE.ToString("F2");
            Status = ((int)info.Status).ToString();
            txtShangPinJinE.Value = info.ShangPinJinE.ToString("F2");

            var items = new List<MFileInfo>();

            if (info.FuJians != null && info.FuJians.Count > 0)
            {
                foreach (var item in info.FuJians)
                {
                    MFileInfo file = new MFileInfo();
                    file.FilePath = item.Filepath;
                    items.Add(file);
                }
            }
            
            upload1.YuanFiles = items;

            if (info.FangShis != null && info.FangShis.Count > 0)
            {
                foreach (var item in info.FangShis)
                {
                    switch (item.FangShi)
                    {
                        case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分:
                            txtJiFen1.Value = item.JiFen.ToString("F2");
                            break;
                        case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分礼品卡:
                            //txtJiFen2.Value = item.JiFen.ToString("F2");
                            //txtJinE2.Value = item.JinE.ToString("F2");
                            break;
                        case EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分现金:
                            txtJiFen3.Value = item.JiFen.ToString("F2");
                            txtJinE3.Value = item.JinE.ToString("F2");
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// baocun
        /// </summary>
        void BaoCun()
        {
            var info = new EyouSoft.Model.YlStructure.MWzJiFenShangPinInfo();

            info.CompanyId = CurrentUserCompanyID;
            info.FangShis = new List<EyouSoft.Model.YlStructure.MWzJiFenShangPinFangShiInfo>();
            info.FuJians = null;
            info.IssueTime = DateTime.Now;
            info.MingCheng = Utils.GetFormValue(txtMingCheng.UniqueID);
            info.OperatorId = SiteUserInfo.UserId;
            info.PeiSongFangShi = Utils.GetFormValue(txtPeiSongFangShi.UniqueID);
            info.ShangPinId = ShangPinId;
            info.ShuoMing = Utils.GetYlEditorText(Request.Form[txtShuoMing.UniqueID]);
            info.Status = Utils.GetEnumValue<EyouSoft.Model.EnumType.YlStructure.JiFenShangPinStatus>(Utils.GetFormValue("txtStatus"), EyouSoft.Model.EnumType.YlStructure.JiFenShangPinStatus.上架);
            info.XuZhi = Utils.GetYlEditorText(Request.Form[txtXuZhi.UniqueID]);
            info.ShuLiang = Utils.GetInt(Utils.GetFormValue(txtShuLiang.UniqueID));
            info.FaPiaoKuaiDiJinE = Utils.GetDecimal(Utils.GetFormValue(txtFaPiaoKuaiDiJinE.UniqueID));
            info.ShangPinJinE = Utils.GetDecimal(Utils.GetFormValue(txtShangPinJinE.UniqueID));

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

            info.FuJians = items3;

            var fangShi1 = new EyouSoft.Model.YlStructure.MWzJiFenShangPinFangShiInfo();
            var fangShi2 = new EyouSoft.Model.YlStructure.MWzJiFenShangPinFangShiInfo();
            var fangShi3 = new EyouSoft.Model.YlStructure.MWzJiFenShangPinFangShiInfo();
            decimal jiFen1 = Utils.GetDecimal(Utils.GetFormValue(txtJiFen1.UniqueID));
            //decimal jiFen2 = Utils.GetDecimal(Utils.GetFormValue(txtJiFen2.UniqueID));
            decimal jiFen3 = Utils.GetDecimal(Utils.GetFormValue(txtJiFen3.UniqueID));
            //decimal jinE2 = Utils.GetDecimal(Utils.GetFormValue(txtJinE2.UniqueID));
            decimal jinE3 = Utils.GetDecimal(Utils.GetFormValue(txtJinE3.UniqueID));

            if (jiFen1 > 0)
            {
                fangShi1.FangShi = EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分;
                fangShi1.JiFen = jiFen1;

                info.FangShis.Add(fangShi1);
            }

            /*if (jiFen2 > 0 || jinE2 > 0)
            {
                fangShi2.FangShi = EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分礼品卡;
                fangShi2.JiFen = jiFen2;
                fangShi2.JinE = jinE2;

                info.FangShis.Add(fangShi2);
            }*/

            if (jiFen3 > 0 || jinE3 > 0)
            {
                fangShi3.FangShi = EyouSoft.Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分现金;
                fangShi3.JiFen = jiFen3;
                fangShi3.JinE = jinE3;

                info.FangShis.Add(fangShi3);
            }

            if (info.FangShis.Count == 0)
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "请填写积分兑换方式"));
            }

            int bllRetCode = 0;

            if (string.IsNullOrEmpty(ShangPinId))
            {
                bllRetCode = new EyouSoft.BLL.YlStructure.BDuiHuan().InsertJiFenShangPin(info);
            }
            else
            {
                bllRetCode = new EyouSoft.BLL.YlStructure.BDuiHuan().UpdateJiFenShangPin(info);
            }

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败"));
        }
        #endregion
    }
}
