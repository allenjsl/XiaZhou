using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using System.Text;

namespace Web.PageT
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：柴逸宁
    /// 创建时间：2011-9-6
    /// 修改人：.........
    /// 修改时间：.......
    /// 修改备注：.......
    public partial class WebList : BackPage
    {
        #region attributes
        /// <summary>
        /// 每页显示条数
        /// </summary>
        private int pageSize = 10;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        private int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region ajax request
            //ajax request
            string doType = Utils.GetQueryStringValue("doType");

            switch (doType)
            {
                case "DELETE": F1(); break;
                case "GET": F2(); break;
                default: break;
            }
            #endregion

            //导出处理
            if (UtilsCommons.IsToXls()) ToXls();

            //用户控件初始化
            InitWUC();

            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                //初始化
                DataInit();
            }
        }

        #region private members
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //获取分页参数并强转
            pageIndex = UtilsCommons.GetPadingIndex();
            //绑定分页
            BindPage();
        }

        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            //this.ExportPageInfo1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            //this.ExportPageInfo1.UrlParams = Request.QueryString;
            //this.ExportPageInfo1.intPageSize = pageSize;
            //this.ExportPageInfo1.CurrencyPage = pageIndex;
            //this.ExportPageInfo1.intRecordCount = recordCount;
        }

        /// <summary>
        /// F1
        /// </summary>
        /// <returns></returns>
        void  F1()
        {
            string param = Utils.GetQueryStringValue("param");

            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson("SUCCESS"));
            Response.End();
        }

        /// <summary>
        /// F2
        /// </summary>
        /// <returns></returns>
        void F2()
        {
            string param = Utils.GetQueryStringValue("param");

            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson("ERROR"));
            Response.End();
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {

        }

        /// <summary>
        /// 导出到xls
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            int _recordCount = 0;
            StringBuilder s = new StringBuilder();           

            //在这里拼接导出到xls的数据 以下是实例
            s.Append("列1\t列2\n");
            var items = new List<object>();//bll.gets(toXlsRecordCount, 1, ref _recordCount, searchInfo);

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    s.AppendFormat("{0}\t{1}\n", item, item);
                }
            }

            ResponseToXls(s.ToString());
        }

        /// <summary>
        /// 用户控件初始化
        /// </summary>
        void InitWUC()
        {

        }
        #endregion

        #region protected members
        /// <summary>
        /// F3
        /// </summary>
        /// <param name="i">参数1</param>
        /// <param name="s">参数2</param>
        protected void F3(int i, string s)
        {

        }
        #endregion
    }
}
