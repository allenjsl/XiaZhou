using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace EyouSoft.Web.UserControl
{
    public partial class YLXingCheng : System.Web.UI.UserControl
    {
        private IList<EyouSoft.Model.YlStructure.MHangQiXingChengInfo> _setPlanList;
        public IList<EyouSoft.Model.YlStructure.MHangQiXingChengInfo> SetPlanList
        {
            get { return _setPlanList; }
            set { _setPlanList = value; }
        }


 

        private bool _isConut = false;
        /// <summary>
        /// 是否供应商平台
        /// </summary>
        public bool isCount
        {
            get { return _isConut; }
            set { _isConut = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SetPlanList != null && SetPlanList.Count > 0)
                {
                    plaVis.Visible = false;
                    this.rptlist.DataSource = SetPlanList;
                    this.rptlist.DataBind();
                }
                else
                {
                    plaVis.Visible = true;

                }
            }
        }



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