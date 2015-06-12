using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Text;

namespace Web.ManageCenter.Advice
{
    /// <summary>
    /// 行政中心-意见建议箱-列表
    /// </summary>
    /// 创建人：徐从栎
    /// 创建时间：2011-9-15
    public partial class AdviceList : BackPage
    {
        private int pageSize = 10;
        private int pageIndex = 0;
        private int recordCount = 0;
        private int pageCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Request.QueryString["doType"];
            //存在ajax请求
            if (doType != null && doType.Length > 0)
            {
                AJAX(doType);
            }
            #endregion
            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                //初始化
                DataInit();
                //查询参数
                string txtTitle = Utils.GetQueryStringValue("txtTitle");//标题
                string txtPeople = Utils.GetQueryStringValue("txtPeople");//提交人
                string txtTime = Utils.GetQueryStringValue("txtTime");//提交时间
                string txtGetor = Utils.GetQueryStringValue("txtGetor");//接收人
                string txtHandleTime = Utils.GetQueryStringValue("txtHandleTime");//处理时间
                string selState = Utils.GetQueryStringValue("selState");//状态
            }
        }
        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"));
            EyouSoft.BLL.GovStructure.BOpinion BLL = new EyouSoft.BLL.GovStructure.BOpinion();
            EyouSoft.Model.GovStructure.MSearchOpinion Model=new EyouSoft.Model.GovStructure.MSearchOpinion();
            Model.Title = Utils.GetQueryStringValue("txtTitle");//标题
            Model.Submit = Utils.GetQueryStringValue(this.SellsSelect1.ClientID + "_txtSellName");//提交人
            Model.SubmitTime =Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtTime"));//提交时间 
            Model.OpinionUserId = Utils.GetQueryStringValue(this.SellsSelect2.ClientID + "_hideSellID");//接收人
            Model.ProcessTime=Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtHandleTime"));//处理时间
            Model.Status=Utils.GetQueryStringValue("selState");//处理状态
            //IList<EyouSoft.Model.GovStructure.MGovOpinion> lst = BLL.GetGovOpinionList(this.SiteUserInfo.CompanyId, Model, this.pageSize, this.pageIndex, ref this.recordCount);
            //if (null != lst && lst.Count > 0)
            //{
            //    this.RepList.DataSource = lst;
            //    this.RepList.DataBind();
            //    BindPage();
            //}
            //else
            //{
            //    this.RepList.Controls.Add(new Label() { Text = "<tr><td colspan='7' align='center'>对不起，没有相关数据！</td></tr>" });
            //    this.ExporPageInfoSelect1.Visible = false;
            //    this.ExporPageInfoSelect2.Visible = false;
            //}
        }
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect1.intPageSize = pageSize;
            this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect1.intRecordCount = recordCount;
            this.ExporPageInfoSelect2.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
            this.ExporPageInfoSelect2.UrlParams = Request.QueryString;
            this.ExporPageInfoSelect2.intPageSize = pageSize;
            this.ExporPageInfoSelect2.CurrencyPage = pageIndex;
            this.ExporPageInfoSelect2.intRecordCount = recordCount;
        }
        /// <summary>
        /// ajax操作
        /// </summary>
        private void AJAX(string doType)
        {
            string msg = string.Empty;
            //对应执行操作
            switch (doType)
            {
                case "delete":
                    //判断权限
                    //if (this.CheckGrant(Common.Enum.TravelPermission.行政中心_意见建议箱_处理))
                    { 
                        string id = Utils.GetQueryStringValue("id");
                        //执行并获取结果
                        msg = DeleteData(id);                        
                    }
                    break;
                default:
                    break;
            }
            //返回ajax操作结果
            Response.Clear();
            Response.Write(msg);
            Response.End();
        }
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id">删除ID</param>
        /// <returns></returns>
        private string DeleteData(string id)
        {
            bool b = false;
            if (!String.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.GovStructure.BOpinion BLL = new EyouSoft.BLL.GovStructure.BOpinion();
                b = BLL.DeleteGovOpinion(id.Split(','));
            }
            return b ? "1" : "失败！";
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            //if (!this.CheckGrant(Common.Enum.TravelPermission.行政中心_意见建议箱_栏目))
            {
                //Utils.ResponseNoPermit(Common.Enum.TravelPermission.行政中心_意见建议箱_栏目, false);
                return;
            }
        }
        #endregion
        #region 前台调用方法
        protected string getMoreInfo(object o)
        {
            List<string> str = new List<string>();
            if(null!=o)
            {
                IList<EyouSoft.Model.GovStructure.MGovOpinionUser> lst=(IList<EyouSoft.Model.GovStructure.MGovOpinionUser>)o;
                if(null!=lst&&lst.Count>0)
                {
                    for(int i=0;i<lst.Count;i++)
                    {
                        str.Add(lst[i].User);
                    }
                }
            }
            return str.Count==0?String.Join(",",str.ToArray()):"";
        }
        #endregion
    }
}