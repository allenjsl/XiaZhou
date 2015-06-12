using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.BLL.SourceStructure;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.SourceStructure;
using EyouSoft.Model.SourceStructure;
using EyouSoft.Model.TourStructure;

namespace Web.UserCenter.WorkAwake.Preview
{
    public partial class RoomUseInfo : BackPage
    {
        /// <summary>
        /// 页面：DOM
        /// </summary>
        /// 创建人：戴银柱
        /// 创建时间：2011-9-20
        /// 说明：事物中心：预控到期提醒：已用房信息


        #region 分页参数
        /// <summary>
        /// 每页显示条数(常量)
        /// </summary>
        /// 当变量需要在前台使用时可换成protected修饰
        private int pageSize = 10;
        /// <summary>
        /// 当前页数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        ///  当变量需要在前台使用时可换成protected修饰
        private int recordCount = 0;
        #endregion

        public int sueType = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            //预控id
            string sueID = Utils.GetQueryStringValue("sueId");
            //预控类型
            sueType = Utils.GetInt(Utils.GetQueryStringValue("sueType"));
            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                if (sueType > 0 && !string.IsNullOrEmpty(sueID))
                {
                    DataInit(sueType, sueID);
                }
                else
                {
                    Response.Clear();
                    Response.Write("<span align='center'>参数为空！</span>");
                    Response.End();
                }
            }
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_栏目, true);
                return;
            }

            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_预控到期提醒栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_预控到期提醒栏目, true);
                return;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit(int type, string id)
        {
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            //实例化预控业务层
            BSourceControl bllBSourceControl = new BSourceControl();
            //实例化预控列表
            IList<MSueUse> list = null;
            MSueUse a = new MSueUse();
            #region 根据预控类型获取相应的预控使用列表
            switch ((SourceControlCategory)type)
            {
                case SourceControlCategory.车辆:
                    list = bllBSourceControl.GetCarUseList(SiteUserInfo.CompanyId, id, pageIndex, pageSize, ref recordCount);
                    if (list != null && list.Count > 0)
                    {
                        if (list.Count <= pageSize)
                            this.ExporPageInfoSelect1.Visible = false;
                        this.rptList.DataSource = list;
                        this.rptList.DataBind();
                    }
                    else
                    {
                        lblMsg.Text = "暂无数据";
                        this.ExporPageInfoSelect1.Visible = false;
                    }
                    list = null;
                    break;
                case SourceControlCategory.酒店:
                    list = bllBSourceControl.GetHotelUseList(id, SiteUserInfo.CompanyId, pageIndex, pageSize, ref recordCount);
                    if (list != null && list.Count > 0)
                    {
                        if (list.Count <= pageSize)
                            this.ExporPageInfoSelect1.Visible = false;
                        this.rptList.DataSource = list;
                        this.rptList.DataBind();
                    }
                    else
                    {
                        lblMsg.Text = "暂无数据";
                        this.ExporPageInfoSelect1.Visible = false;
                    }
                    list = null;
                    break;
                case SourceControlCategory.游轮:
                    list = bllBSourceControl.GetShipUseList(SiteUserInfo.CompanyId, id, pageIndex, pageSize, ref recordCount);
                    if (list != null && list.Count > 0)
                    {
                        if (list.Count <= pageSize)
                            this.ExporPageInfoSelect1.Visible = false;
                        this.rptListShip.DataSource = list;
                        this.rptListShip.DataBind();
                    }
                    else
                    {
                        lblMsg.Text = "暂无数据";
                        this.ExporPageInfoSelect1.Visible = false;
                    }
                    list = null;
                    break;
                case SourceControlCategory.景点:
                    list = bllBSourceControl.GetSightUseList(SiteUserInfo.CompanyId, id, pageIndex, pageSize, ref recordCount);
                    if (list != null && list.Count > 0)
                    {
                        if (list.Count <= pageSize)
                            this.ExporPageInfoSelect1.Visible = false;
                        this.rptList.DataSource = list;
                        this.rptList.DataBind();
                    }
                    else
                    {
                        lblMsg.Text = "暂无数据";
                        this.ExporPageInfoSelect1.Visible = false;
                    }
                    list = null;
                    break;
                case SourceControlCategory.其他:
                    list = bllBSourceControl.GetOtherUseList(SiteUserInfo.CompanyId, id, pageIndex, pageSize, ref recordCount);
                    if (list != null && list.Count > 0)
                    {
                        if (list.Count <= pageSize)
                            this.ExporPageInfoSelect1.Visible = false;
                        this.rptList.DataSource = list;
                        this.rptList.DataBind();
                    }
                    else
                    {
                        lblMsg.Text = "暂无数据";
                        this.ExporPageInfoSelect1.Visible = false;
                    }
                    list = null;
                    break;
                default:
                    break;
            }
            #endregion

            BindPage();
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
        }



        /// <summary>
        /// 根据游客列表绑定游客信息
        /// </summary>
        /// <param name="TravellerList"></param>
        /// <returns></returns>
        protected string GetykInfo(object TravellerList)
        {
            string result = "</td><td align='center'>";
            if (TravellerList != null)
            {
                IList<MTourOrderTraveller> list = TravellerList as IList<MTourOrderTraveller>;
                if (list.Count > 1)
                    result = list[0].CnName + "</td><td align='center'>" + list[0].Contact;
            }
            return result;
        }



        /// <summary>
        /// 根据导游列表绑定导游信息
        /// </summary>
        /// <param name="TravellerList"></param>
        /// <returns></returns>
        protected string GetdyInfo(object GuideList)
        {
            string result = "</td><td align='center'>";
            if (GuideList != null)
            {
                IList<MSourceGuide> list = GuideList as IList<MSourceGuide>;
                if (list.Count > 1)
                    result = list[0].Name + "</td><td align='center'>" + list[0].Mobile;
            }
            return result;
        }
    }
}
