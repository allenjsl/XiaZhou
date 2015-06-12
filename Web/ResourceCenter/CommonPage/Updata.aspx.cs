using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Model.SourceStructure;
using EyouSoft.BLL.SourceStructure;
using EyouSoft.Common;
using EyouSoft.Model.EnumType.SourceStructure;
using EyouSoft.Model.EnumType.PrivsStructure;

namespace Web.ResourceCenter.CommonPage
{
    /// <summary>
    /// 资源预控-预控变更-公共页面
    /// </summary>
    /// 创建人:柴逸宁
    /// 创建时间:2012.3.15
    public partial class Updata : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.GetFormValue("doType").Length > 0)
            {
                Save();
            }
            SourceControlCategory sourceControlCategory = (SourceControlCategory)Utils.GetInt(Utils.GetQueryStringValue("sourceControlCategory"));
            //权限验证
            PowerControl(sourceControlCategory);
            //根据ID初始化页面
            PageInit(sourceControlCategory);

        }
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(SourceControlCategory sourceControlCategory)
        {
            string sourceId = Utils.GetQueryStringValue("sourceId");
            if (sourceId.Length > 0)
            {

                BSourceControl bll = new BSourceControl();
                switch (sourceControlCategory)
                {
                    case SourceControlCategory.酒店:
                        //预控Model
                        MSourceSueHotel hotelModel = bll.GetModelByHotelId(sourceId, CurrentUserCompanyID);
                        if (hotelModel != null)
                        {
                            SetVal(
                                hotelModel.SourceName,//酒店名称
                                hotelModel.RoomType,//房型
                                hotelModel.ControlNum.ToString(),//预控数量
                                hotelModel.AlreadyNum.ToString(),//已使用数量
                                (hotelModel.ControlNum - hotelModel.AlreadyNum).ToString()//剩余数量
                                );

                        }
                        break;
                    case SourceControlCategory.车辆:
                        //预控Model
                        MSourceSueCar carModel = bll.GetModelByCarId(sourceId, CurrentUserCompanyID);
                        if (carModel != null)
                        {

                            SetVal(
                                carModel.SourceName,//车队名称
                                carModel.TypeName,//车型
                                carModel.ControlNum.ToString(),//预控数量
                                carModel.AlreadyNum.ToString(),//已使用数量
                                (carModel.ControlNum - carModel.AlreadyNum).ToString()//剩余数量
                                );
                        }
                        break;
                    case SourceControlCategory.游轮:
                        //预控Model
                        MSourceSueShip shipModel = bll.GetModelByShipId(sourceId, CurrentUserCompanyID);
                        if (shipModel != null)
                        {

                            SetVal(
                                shipModel.ShipCompany,//游船公司名称
                                shipModel.ShipName,//游船名称
                                shipModel.ControlNum.ToString(),//预控数量
                                shipModel.AlreadyNum.ToString(),//已使用数量
                                (shipModel.ControlNum - shipModel.AlreadyNum).ToString()//剩余数量
                                );
                        }
                        break;
                    case SourceControlCategory.景点:
                        //预控Model
                        var sightModel = bll.GetModelBySightId(sourceId, CurrentUserCompanyID);
                        if (sightModel != null)
                        {

                            SetVal(
                                sightModel.SourceName,//景点公司名称
                                sightModel.SpotName,//景点名称
                                sightModel.ControlNum.ToString(),//预控数量
                                sightModel.AlreadyNum.ToString(),//已使用数量
                                (sightModel.ControlNum - sightModel.AlreadyNum).ToString()//剩余数量
                                );
                        }
                        break;
                    case SourceControlCategory.其他:
                        //预控Model
                        var otherModel = bll.GetModelByOtherId(sourceId, CurrentUserCompanyID);
                        if (otherModel != null)
                        {

                            SetVal(
                                otherModel.SourceName,//其他公司名称
                                otherModel.TypeName,//支出项目名称
                                otherModel.ControlNum.ToString(),//预控数量
                                otherModel.AlreadyNum.ToString(),//已使用数量
                                (otherModel.ControlNum - otherModel.AlreadyNum).ToString()//剩余数量
                                );
                        }
                        break;
                }

                //预控变更记录
                IList<MSourceSueHotelChange> ls = bll.GetSourceSueChangeList(sourceId, sourceControlCategory);
                if (ls != null && ls.Count > 0)
                {
                    rpt_list.DataSource = ls;
                    rpt_list.DataBind();
                }
                else
                {
                    this.phdFr.Visible = false;
                }

            }

        }
        /// <summary>
        /// 设置页面数据
        /// </summary>
        /// <param name="SourceName">游船公司名称,酒店名称,车队名称</param>
        /// <param name="RoomType">游船名称,房型,车型</param>
        /// <param name="ControlNum">预控数量</param>
        /// <param name="AlreadyNum">已使用数量</param>
        /// <param name="Surplus">剩余数量</param>
        private void SetVal(string SourceName, string RoomType, string ControlNum, string AlreadyNum, string Surplus)
        {
            lbl_sourceName.Text = SourceName;
            lbl_typeName.Text = RoomType;
            lbl_controlNum.Text = ControlNum;
            lbl_alreadyNum.Text = AlreadyNum;
            lbl_surplus.Text = Surplus;
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl(SourceControlCategory sourceControlCategory)
        {
            switch (sourceControlCategory)
            {
                case SourceControlCategory.酒店:
                    if (!CheckGrant(Privs.资源预控_酒店预控_预控变更))
                    {
                        Utils.ResponseNoPermit(Privs.资源预控_酒店预控_预控变更, true);
                        return;
                    }
                    break;
                case SourceControlCategory.车辆:
                    if (!CheckGrant(Privs.资源预控_车辆预控_预控变更))
                    {
                        Utils.ResponseNoPermit(Privs.资源预控_车辆预控_预控变更, true);
                        return;
                    }

                    break;
                case SourceControlCategory.游轮:
                    if (!CheckGrant(Privs.资源预控_游船预控_预控变更))
                    {
                        Utils.ResponseNoPermit(Privs.资源预控_游船预控_预控变更, true);
                        return;
                    }

                    break;
                case SourceControlCategory.景点:
                    if (!CheckGrant(Privs.资源预控_景点预控_预控变更))
                    {
                        Utils.ResponseNoPermit(Privs.资源预控_景点预控_预控变更, true);
                        return;
                    }

                    break;
                case SourceControlCategory.其他:
                    if (!CheckGrant(Privs.资源预控_其他预控_预控变更))
                    {
                        Utils.ResponseNoPermit(Privs.资源预控_其他预控_预控变更, true);
                        return;
                    }

                    break;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            //酒店变更实体
            MSourceSueHotelChange model = new MSourceSueHotelChange();
            string msg = string.Empty;
            //获取页面数据
            if (GetPageVal(model, ref msg))
            {
                model.Type = (SourceControlCategory)Utils.GetInt(Utils.GetFormValue("sourceControlCategory"));
                PageResponse(UtilsCommons.AjaxReturnJson(new BSourceControl().AddSueHotelChange(model) ? "1" : "-1", "变更失败!"));
            }
            else
            {
                PageResponse(UtilsCommons.AjaxReturnJson("-1", msg));
            }
        }
        /// <summary>
        /// 获取页面数据
        /// </summary>
        /// <param name="model">变更实体</param>
        /// <param name="msg">验证提示</param>
        /// <returns></returns>
        private bool GetPageVal(MSourceSueHotelChange model, ref string msg)
        {
            int plusOrCut = Utils.GetIntSign(Utils.GetFormValue("plusOrCut"), 0);
            msg += plusOrCut != 0 ? string.Empty : "请选择增减类型!<br/>";
            if (plusOrCut > 0)
            {
                model.Plus = Utils.GetInt(Utils.GetFormValue("num"));
                msg += model.Plus > 0 ? string.Empty : "增加数量不能为空!<br/>";
            }
            else if (plusOrCut < 0)
            {
                model.Cut = Utils.GetInt(Utils.GetFormValue("num"));
                msg += model.Cut > 0 ? string.Empty : "减少数量不能为空!<br/>";
            }
            model.Remark = Utils.GetFormValue("remark");
            model.IssueTime = DateTime.Now;
            model.OperatorId = SiteUserInfo.UserId;
            model.Operator = SiteUserInfo.Name;
            model.SueId = Utils.GetFormValue("sourceId");

            return msg.Length <= 0;
        }
        /// <summary>
        /// 页面返回
        /// </summary>
        /// <param name="ret"></param>
        private void PageResponse(string ret)
        {
            Response.Clear();
            Response.Write(ret);
            Response.End();
        }
    }
}
