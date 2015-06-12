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
    public partial class ChuanZhiSheShi : BackPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
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
            var model = new EyouSoft.BLL.YlStructure.BJiChuXinXi().GetChuanZhiInfo(Utils.GetQueryStringValue("id"));
            model.SheShis = GetDataList();
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
            if (model != null && model.SheShis.Count > 0)
            {
                phrPanel.Visible = false;
                rptlist.DataSource = model.SheShis;
                rptlist.DataBind();
            }
        }

        /// <summary>
        /// 获取船只美食列表
        /// </summary>
        IList<MChuanZhiSheShiInfo> GetDataList()
        {
            //设施名称
            string[] MingChengs = Utils.GetFormValues("MingCheng");
            //描述
            string[] MiaoShus = Utils.GetYlEditorValues("MiaoShu");
            //图片
            string[] FilePaths = Utils.GetFormValues("hide_Cruiseimg_file");

            if (MingChengs.Length > 0)
            {
                IList<MChuanZhiSheShiInfo> list = new List<MChuanZhiSheShiInfo>();
                for (int i = 0; i < MingChengs.Length; i++)
                {
                    if (!String.IsNullOrEmpty(MingChengs[i].ToString()))
                    {
                        MChuanZhiSheShiInfo model = new MChuanZhiSheShiInfo();
                        model.MingCheng = MingChengs[i];
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


    }
}
