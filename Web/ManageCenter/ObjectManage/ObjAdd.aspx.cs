using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using EyouSoft.BLL.GovStructure;
using EyouSoft.Model.GovStructure;
using System.Text;

namespace Web.ManageCenter.ObjectManage
{
    /// <summary>
    /// 行政中心-物品管理-物品入库
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-03-31
    public partial class ObjAdd : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            #region 处理AJAX请求
            //获取ajax请求
            string save = Utils.GetQueryStringValue("save");
            string id = Utils.GetQueryStringValue("id");
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (save == "save")
            {
                PageSave(doType);
            }
            #endregion

            if (!IsPostBack)
            {
                //根据ID初始化页面
                PageInit(id);
            }
        }

        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {
            this.lbRecorder.Text = this.SiteUserInfo.Name;
            //编辑初始化
            if (String.Equals(Request.QueryString["dotype"], "update", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(id))
            {
                BGood BLL = new BGood();
                MGovGood Model = BLL.GetGovGoodModel(id, this.SiteUserInfo.CompanyId);
                if (null != Model)
                {
                    this.txtName.Text = Model.Name;//物品名称 
                    this.txtCount.Text = Convert.ToString(Model.Number);//物品数量
                    this.hidGoodCount.Value = Convert.ToString(Model.Number);
                    this.hidGoodStock.Value = Model.Stock.ToString();
                    this.txtInTime.Text = String.Format("{0:yyyy-MM-dd}", Model.Time);//入库时间 
                    this.lbRecorder.Text = Model.Operator;//登记人
                    this.txtPrice.Text = Utils.FilterEndOfTheZeroDecimal(Model.Price);
                    this.txtRemark.Text = Model.Remark;//备注
                    this.txtUse.Text = Model.Use;
                    this.hidGoodId.Value = Model.GoodId;
                    if (!this.SiteUserInfo.IsHandleElse && this.SiteUserInfo.UserId != Model.OperatorId)
                    {
                        this.ph_Save.Visible = false;
                    }
                }
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        protected void PageSave(string doType)
        {
            #region 表单取值
            string name = Utils.GetFormValue(txtName.UniqueID);
            string number = Utils.GetFormValue(txtCount.UniqueID);
            string price = Utils.GetFormValue(txtPrice.UniqueID);
            string time = Utils.GetFormValue(txtInTime.UniqueID);
            string use = Utils.GetFormValue(txtUse.UniqueID);
            string remark = Utils.GetFormValue(txtRemark.UniqueID);
            string goodId = Utils.GetFormValue(hidGoodId.UniqueID);
            string stock = Utils.GetFormValue(hidGoodStock.UniqueID);
            string oldnumber = Utils.GetFormValue(hidGoodCount.UniqueID);
            #endregion

            #region 数据验证
            string msg = "";
            bool result = false;
            if (string.IsNullOrEmpty(name))
            {
                msg += "-请输入物品名称！";
            }
            if (string.IsNullOrEmpty(number))
            {
                msg += "-请输入物品数量！";
            }
            if (string.IsNullOrEmpty(time))
            {
                msg += "请输入物品入库时间！";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
                Response.End();
                return;
            }
            #endregion

            #region 实体赋值
            MGovGood model = new MGovGood();
            model.Name = name;
            model.CompanyId = this.SiteUserInfo.CompanyId;
            model.GoodId = goodId;
            model.IssueTime = DateTime.Now;
            model.Number = Utils.GetInt(number);
            model.Operator = this.SiteUserInfo.Name;
            model.OperatorId = this.SiteUserInfo.UserId;
            model.Price = Utils.GetDecimal(price);
            model.Remark = remark;
            model.Time = Utils.GetDateTimeNullable(time);
            model.Stock = Utils.GetInt(number) - Utils.GetInt(oldnumber) + Convert.ToInt32(string.IsNullOrEmpty(stock) ? "0" : stock);
            model.Use = use;
            #endregion

            #region 提交保存
            BGood BLL = new BGood();
            if (doType == "add")
            {
                result = BLL.AddGovGood(model);
                msg = result ? "添加成功！" : "添加失败！";
            }
            if (doType == "update")
            {
                result = BLL.UpdateGovGood(model);
                msg = result ? "修改成功！" : "修改失败！";
            }
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
            Response.End();
            #endregion
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_栏目, false);
            }
            else
            {
                string doType = Utils.GetQueryStringValue("doType");
                if (doType == "add")
                {
                    if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_入库登记))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_入库登记, false);
                    }
                }
                else
                {
                    if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_物品修改))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_物品管理_物品修改, false);
                    }
                }
            }
        }
        #endregion

        #region 重写OnPreInit
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
        #endregion
    }
}