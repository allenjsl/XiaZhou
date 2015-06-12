using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;
using EyouSoft.Model.SourceStructure;
using EyouSoft.BLL.SourceStructure;
using System.Text;

namespace Web.ResourceManage.Ground
{
    /// <summary>
    /// 页面：DOM
    /// </summary>
    /// 创建人：刘飞
    /// 创建时间：2011-9-22
    /// 说明: 资源管理： 地接社
    public partial class GroundList : BackPage
    {
        #region attributes
        /// <summary>
        /// 每页显示条数
        /// </summary>
        private int pageSize = 20;
        /// <summary>
        /// 当前页数
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 总记录条数
        /// </summary>
        public int recordCount = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("dotype");
            string id = Utils.GetQueryStringValue("id");
            //存在ajax请求
            if (doType != null && doType.Length > 0)
            {
                AJAX(doType, id);
            }
            #endregion
            
            if (UtilsCommons.IsToXls()) ToXls();

            PowerControl();
            InitRpt();
        }

        #region private members
        /// <summary>
        /// init repeater
        /// </summary>
        private void InitRpt()
        {
            pageIndex = UtilsCommons.GetPadingIndex();

            var chaXun = GetChaXunInfo();
            var items = new EyouSoft.BLL.GysStructure.BGys().GetDiJieShes(SiteUserInfo.CompanyId, pageSize, pageIndex, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                rptList.DataSource = items;
                rptList.DataBind();

                this.ExporPageInfoSelect1.PageLinkURL = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?";
                this.ExporPageInfoSelect1.UrlParams = Request.QueryString;
                this.ExporPageInfoSelect1.intPageSize = pageSize;
                this.ExporPageInfoSelect1.CurrencyPage = pageIndex;
                this.ExporPageInfoSelect1.intRecordCount = recordCount;
            }
            else
            {
                this.lbMsg.Visible = true;
                this.lbMsg.Text = "<tr class=\"odd\"><td height=\"30px\" colspan=\"11\" align=\"center\">暂无数据!</td></tr>";
                ExporPageInfoSelect1.Visible = false;
            }
        }

        /// <summary>
        /// ajax操作
        /// </summary>
        private void AJAX(string doType, string id)
        {
            string msg = string.Empty;
            //对应执行操作
            switch (doType.ToLower())
            {
                case "stop":
                    msg = this.StopData(id);
                    break;
                case "start":
                    msg = this.StartData(id);
                    break;
                case "black":
                    msg = this.HmdData(id);
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
        /// 停用操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string StopData(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.SourceStructure.BSource bll = new BSource();
                if (bll.UpdateTravelAccountStatus(id, EyouSoft.Model.EnumType.ComStructure.UserStatus.已停用, "", null, false) == 1)
                    return UtilsCommons.AjaxReturnJson("1", "已停用");
                else
                {
                    return UtilsCommons.AjaxReturnJson("0", "停用失败");
                }
            }
            return UtilsCommons.AjaxReturnJson("0", "未选择任何行");
        }

        /// <summary>
        /// 启用操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string StartData(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.SourceStructure.BSource bll = new BSource();
                if (bll.UpdateTravelAccountStatus(id, EyouSoft.Model.EnumType.ComStructure.UserStatus.正常, "", null, false) == 1)
                    return UtilsCommons.AjaxReturnJson("1", "已启用");
                else
                {
                    return UtilsCommons.AjaxReturnJson("0", "启用失败");
                }
            }
            return UtilsCommons.AjaxReturnJson("0", "未选择任何行");
        }

        /// <summary>
        /// 黑名单操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string HmdData(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.SourceStructure.BSource bll = new BSource();
                if (bll.UpdateTravelAccountStatus(id, EyouSoft.Model.EnumType.ComStructure.UserStatus.黑名单, "", null, false) == 1)
                    return UtilsCommons.AjaxReturnJson("1", "已加入黑名单");
                else
                {
                    return UtilsCommons.AjaxReturnJson("0", "黑名单设置失败");
                }
            }
            return UtilsCommons.AjaxReturnJson("0", "未选择任何行");
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_地接社_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_地接社_栏目, false);
                return;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_地接社_新增))
            {
                this.phForAdd.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_地接社_修改))
            {
                this.phForUpdate.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_地接社_删除))
            {
                this.phForDel.Visible = false;
            }
        }

        /// <summary>
        /// 获取查询实体
        /// </summary>
        /// <returns></returns>
        EyouSoft.Model.GysStructure.MLBChaXunInfo GetChaXunInfo()
        {
            var info = new EyouSoft.Model.GysStructure.MLBChaXunInfo();
            info.CityId = Utils.GetIntNull(Utils.GetQueryStringValue("ddlCity"));
            info.GysName = Utils.GetQueryStringValue("txtUnitName");
            info.ProvinceId = Utils.GetIntNull(Utils.GetQueryStringValue("ddlProvice"));
            info.GysId = Utils.GetQueryStringValue("gysid");

            return info;
        }

        /// <summary>
        /// to xls
        /// </summary>
        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);

            int _recordCount = 0;
            var s = new StringBuilder();
            s.Append("所在地\t单位名称\t联系人\t交易次数\t交易人数\t结算金额\t未付金额\t质检均分\t账号分配\t账号状态\n");

            var chaXun = GetChaXunInfo();
            var items = new EyouSoft.BLL.GysStructure.BGys().GetDiJieShes(SiteUserInfo.CompanyId, toXlsRecordCount, 1, ref _recordCount, chaXun);


            if (items != null && items.Count > 0)
            {
                foreach (var t in items)
                {
                    s.AppendFormat(
                        "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\n",
                        t.CPCD.ProvinceName + "-" + t.CPCD.CityName,
                        t.GysName,
                        t.LxrName,
                        t.JiaoYiXX.JiaoYiCiShu,
                        t.JiaoYiXX.JiaoYiShuLiang,
                        t.JiaoYiXX.JieSuanJinE,
                        t.JiaoYiXX.WeiZhiFuJinE,
                        t.JiaoYiXX.ZhiJianJunFen,
                        string.IsNullOrEmpty(t.UserInfo.UserId) ? "未分配帐号" : t.UserInfo.Username,
                        t.UserInfo.Status.ToString());
                }
            }

            ResponseToXls(s.ToString());
        }
        #endregion
    }
}
