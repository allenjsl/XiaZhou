using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.YlStructure;

namespace EyouSoft.Web.YouLun
{
    public partial class ChuanZhiFangXingFuJian : BackPage
    {
        /// <summary>
        /// 1：美食附件 2：设施附件 其他：房型附件
        /// </summary>
        protected int T = Utils.GetInt(Utils.GetQueryStringValue("t"));
        protected string S = "房型";
        protected Model.YlStructure.MChuanZhiInfo ChuanZhiInfo = new BLL.YlStructure.BJiChuXinXi().GetChuanZhiInfo(Utils.GetQueryStringValue("id"));
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (T) { case 1:S = "美食"; break; case 2:S = "设施"; break; }
            if (!IsPostBack)
            {
                initList();
            }
            if (Utils.GetQueryStringValue("save") == "save") BaoCun();
        }
        #region 私有方法
        /// <summary>
        /// 保存操作
        /// </summary>
        void BaoCun()
        {
            var fujians = GetDataList();

            if (new EyouSoft.BLL.YlStructure.BJiChuXinXi().ChuanZhiFangXingFuJian_M(Utils.GetQueryStringValue("id"),Utils.GetInt(Utils.GetQueryStringValue("t")),fujians) == 1)
            {
                RCWE(UtilsCommons.AjaxReturnJson("1", "保存成功"));
            }
            else
            {
                RCWE(UtilsCommons.AjaxReturnJson("0", "保存失败"));
            }

        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        void initList()
        {
            var l = new List<MChuanZhiFangXingFuJian>();
            switch (T)
            {
                case 1:
                    if (ChuanZhiInfo != null && ChuanZhiInfo.MeiShis != null && ChuanZhiInfo.MeiShis.Count > 0)
                    {
                        foreach (var m in ChuanZhiInfo.MeiShis)
                        {
                            if (m.FuJians == null || m.FuJians.Count <= 0) continue;
                            foreach (var f in m.FuJians)
                            {
                                l.Add(new MChuanZhiFangXingFuJian()
                                {
                                    FuJianId = f.FuJianId,
                                    FangXingId = f.FangXingId,
                                    Filepath = f.Filepath,
                                    MiaoShu = f.MiaoShu
                                });
                            }
                        }
                    } break;
                case 2:
                    if (ChuanZhiInfo != null && ChuanZhiInfo.SheShis != null && ChuanZhiInfo.SheShis.Count > 0)
                    {
                        foreach (var m in ChuanZhiInfo.SheShis)
                        {
                            if (m.FuJians == null || m.FuJians.Count <= 0) continue;
                            foreach (var f in m.FuJians)
                            {
                                l.Add(new MChuanZhiFangXingFuJian()
                                {
                                    FuJianId = f.FuJianId,
                                    FangXingId = f.FangXingId,
                                    Filepath = f.Filepath,
                                    MiaoShu = f.MiaoShu
                                });
                            }
                        }
                    } break;
                default:
                    if (ChuanZhiInfo != null && ChuanZhiInfo.FangXings != null && ChuanZhiInfo.FangXings.Count > 0)
                    {
                        foreach (var m in ChuanZhiInfo.FangXings)
                        {
                            if (m.FuJians == null || m.FuJians.Count <= 0) continue;
                            foreach (var f in m.FuJians)
                            {
                                l.Add(new MChuanZhiFangXingFuJian()
                                {
                                    FuJianId = f.FuJianId,
                                    FangXingId = f.FangXingId,
                                    Filepath = f.Filepath,
                                    MiaoShu = f.MiaoShu
                                });
                            }
                        }
                    }
                    break;
            }
            if (l != null && l.Count > 0)
            {
                phrPanel.Visible = false;
                rptlist.DataSource = l.OrderBy(m => m.FuJianId);
                rptlist.DataBind();
            }
        }

        /// <summary>
        /// 获取船只房型附件列表
        /// </summary>
        IList<MChuanZhiFangXingFuJian> GetDataList()
        {
            //附件Id
            var fujianids = Utils.GetFormEditorValues("hidFuJianId");
            //房型编号
            string[] MingChengs = Utils.GetFormValues("MingCheng");
            //描述
            string[] MiaoShus = Utils.GetYlEditorValues("MiaoShu");
            //图片
            string[] FilePaths = Utils.GetFormValues("hide_Cruiseimg_file");

            if (MingChengs.Length > 0)
            {
                IList<MChuanZhiFangXingFuJian> list = new List<MChuanZhiFangXingFuJian>();
                for (int i = 0; i < MingChengs.Length; i++)
                {
                    if (!String.IsNullOrEmpty(MingChengs[i].ToString()))
                    {
                        MChuanZhiFangXingFuJian model = new MChuanZhiFangXingFuJian();
                        model.FuJianId = Utils.GetInt(fujianids[i]);
                        model.FangXingId = MingChengs[i];
                        model.LeiXing = Utils.GetInt(Utils.GetQueryStringValue("t"));
                        model.MiaoShu = MiaoShus[i];
                        model.Filepath = FilePaths[i].Split('|')[1];
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

        protected string GetFangXing(object v)
        {
            var s = new System.Text.StringBuilder("<option value=\"-1\">--请选择--</option>");

            switch (T)
            {
                case 1:
                    if (ChuanZhiInfo != null && ChuanZhiInfo.MeiShis != null && ChuanZhiInfo.MeiShis.Count > 0)
                    {
                        foreach (var m in ChuanZhiInfo.MeiShis)
                        {
                            s.AppendFormat("<option value=\"{0}\" {2}>{1}</option>", m.MeiShiId, m.MingCheng, m.MeiShiId == v.ToString() ? "selected=\"selected\"" : "");
                        }
                    } break;
                case 2:
                    if (ChuanZhiInfo != null && ChuanZhiInfo.SheShis != null && ChuanZhiInfo.SheShis.Count > 0)
                    {
                        foreach (var m in ChuanZhiInfo.SheShis)
                        {
                            s.AppendFormat("<option value=\"{0}\" {2}>{1}</option>", m.SheShiId, m.MingCheng, m.SheShiId == v.ToString() ? "selected=\"selected\"" : "");
                        }
                    } break;
                default:
                    if (ChuanZhiInfo != null && ChuanZhiInfo.FangXings != null && ChuanZhiInfo.FangXings.Count > 0)
                    {
                        foreach (var m in ChuanZhiInfo.FangXings)
                        {
                            s.AppendFormat("<option value=\"{0}\" {2}>{1}</option>", m.FangXingId, m.MingCheng, m.FangXingId == v.ToString() ? "selected=\"selected\"" : "");
                        }
                    }
                    break;
            }
            return s.ToString();
        }
    }
}
