using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EyouSoft.Model.TourStructure;
using EyouSoft.Model.SSOStructure;

namespace EyouSoft.Web.UserControl
{
    /// <summary>
    /// 预设车型用户控件
    /// 日期：2012-08-14
    /// </summary>
    public partial class PresetBusType : System.Web.UI.UserControl
    {
        protected MUserInfo SiteUserInfo = null;

        private string _setPriv = string.Empty;
        /// <summary>
        /// 指定控件Name前缀，默认为控件ClientID
        /// </summary>
        public string SetPriv
        {
            get { return string.IsNullOrEmpty(_setPriv) ? this.ClientID : _setPriv; }
            set { _setPriv = value; }
        }

        /// <summary>
        /// 操作模式
        /// </summary>
        public string ContrloModel { get; set; }

        /// <summary>
        /// 计划编号
        /// </summary>
        public string TourId { get; set; }

        /// <summary>
        /// 计划车型编号
        /// </summary>
        public string hidTourCarTypeId { get; set; }

        /// <summary>
        /// 计划车型编号客户端ClientID
        /// </summary>
        public string hideTourCarTypeIdClientID { get { return SetPriv + "_hidTourCarTypeId"; } }

        /// <summary>
        /// 车型编号
        /// </summary>
        public string hidCarTypeId { get; set; }

        /// <summary>
        /// 车型编号客户端ClientID
        /// </summary>
        public string hidCarTypeIdClientID { get { return SetPriv + "_hidCarTypeId"; } }

        /// <summary>
        /// 车型名称
        /// </summary>
        public string hidCarTypeName { get; set; }

        /// <summary>
        /// 车型名称客户端ClientID
        /// </summary>
        public string hidCarTypeNameClientID { get { return SetPriv + "_hidCarTypeName"; } }

        /// <summary>
        /// 描述
        /// </summary>
        public string hidDesc { get; set; }

        /// <summary>
        /// 描述客户端ClientID
        /// </summary>
        public string hidDescClientID { get { return SetPriv + "_hidDesc"; } }

        /// <summary>
        /// 座位数
        /// </summary>
        public string hidSeatNum { get; set; }

        /// <summary>
        /// 座位数客户端ClientID
        /// </summary>
        public string hidSeatNumClientID { get { return SetPriv + "_hidSeatNum"; } }

        /// <summary>
        /// 预设车型列表
        /// </summary>
        public IList<MTourCarType> PreSetTypeList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool _IsLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out SiteUserInfo);
            if (!this.IsPostBack)
            {
                InitPage();
            }
        }
        /// <summary>
        /// 列表加载
        /// </summary>
        private void InitPage()
        {
            if (ContrloModel == "add" || ContrloModel == "copy") //计划添加或复制
            {
                this.ph_AddPreSetBusType.Visible = true;
                this.ph_UpdateFalsePresetBusType.Visible = false;
                this.ph_UpdateTruePresetBusType.Visible = false;
                this.ph_Contrltr.Visible = true;
                this.phdMsg.Visible = false;
            }
            else if (ContrloModel == "update") //计划修改
            {
                this.ph_UpdateFalsePresetBusType.Visible = false;
                this.ph_Contrltr.Visible = false;
                if (this.PreSetTypeList != null && this.PreSetTypeList.Count > 0)
                {
                    this.ph_UpdateFalsePresetBusType.Visible = true;
                    this.phdMsg.Visible = false;
                }
                else {
                    this.phdMsg.Visible = true;
                }
               
                ph_AddPreSetBusType.Visible = false;
                this.ph_UpdateTruePresetBusType.Visible = false;
            }
            else  //单独预设车型修改
            {
                if (this.PreSetTypeList == null)
                {
                    this.ph_AddPreSetBusType.Visible = true;
                }
                else
                {
                    this.ph_AddPreSetBusType.Visible = false;
                }
                this.ph_UpdateFalsePresetBusType.Visible = false;
                this.ph_UpdateTruePresetBusType.Visible = true;
                this.ph_Contrltr.Visible = true;
            }
            //绑定预设车型列表
            if (this.PreSetTypeList != null && this.PreSetTypeList.Count > 0)
            {
                this.prt_PreSetTypeList1.DataSource = PreSetTypeList;
                this.prt_PreSetTypeList1.DataBind();
                this.prt_PreSetTypeList2.DataSource = PreSetTypeList;
                this.prt_PreSetTypeList2.DataBind();
            }
        }

        /// <summary>
        /// 可选车型option
        /// </summary>
        /// <returns></returns>
        protected string GetPreSetBus()
        {
            return GetPreSetBus("");
        }

        /// <summary>
        /// 可选车型option
        /// </summary>
        /// <param name="busType">选中预设车型</param>
        /// <returns>select下拉选项</returns>
        protected string GetPreSetBus(string busType)
        {
            string temp = busType.ToString();
            StringBuilder sb = new StringBuilder();
            IList<EyouSoft.Model.ComStructure.MComCarType> list =
                new EyouSoft.BLL.ComStructure.BComCarType().GetList(SiteUserInfo.CompanyId);
            sb.AppendFormat("<option data-num='-' value='' >请选择</option>");
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    if (item.CarTypeId == temp)
                    {
                        sb.AppendFormat("<option  data-carTypeName='{0}' data-num='{1}'  value='{2}' data-desc='{3}' data-temid='{4}' selected='selected'>{0}</option>", item.CarTypeName, item.SeatNum, item.CarTypeId, item.Desc, item.TemplateId);
                    }
                    else
                    {
                        sb.AppendFormat("<option data-carTypeName='{0}' data-num='{1}' value='{2}' data-desc='{3}' data-temid='{4}'>{0}</option>", item.CarTypeName, item.SeatNum, item.CarTypeId, item.Desc, item.TemplateId);
                    }
                }
            }
            return sb.ToString();
        }
    }
}