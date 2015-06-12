using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using EyouSoft.Model.ComStructure;
using System.Text;

namespace Web.ResourceManage.Path
{
    public partial class PathEidt : BackPage
    {
        /// <summary>
        /// 线路区域编号
        /// </summary>
        protected int AreaID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string doType = Utils.GetQueryStringValue("doType");
            string type = Utils.GetQueryStringValue("type");
            //获得操作ID
            string id = Utils.GetQueryStringValue("id");
            //权限验证
            PowerControl(doType);
            #region 处理AJAX请求
            //获取ajax请求
            //存在ajax请求

            switch (type)
            {
                case "save":
                    Response.Clear();
                    Response.Write(PageSave(id, doType));
                    Response.End();
                    break;
            }

            #endregion

            if (!IsPostBack)
            {
                //根据ID初始化页面
                ForeignQuote1.IsFromTourOrRoute = false;
                PageInit(id, doType);
            }

        }


        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id, string dotype)
        {
            this.CostAccounting1.IsRoute = true;
            this.UploadControl1.CompanyID = this.SiteUserInfo.CompanyId;
            this.UploadControl2.CompanyID = this.SiteUserInfo.CompanyId;
            if (String.Equals(dotype, "update", StringComparison.InvariantCultureIgnoreCase) || String.Equals(dotype, "copy", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.SourceStructure.BSource BLL = new EyouSoft.BLL.SourceStructure.BSource();
                EyouSoft.Model.SourceStructure.MRoute Model = BLL.GetRouteModel(id);
                if (Model != null)
                {
                    txtAdultPrice.Text = Utils.FilterEndOfTheZeroDecimal(Model.AdultPrice);
                    txtChildPrice.Text = Utils.FilterEndOfTheZeroDecimal(Model.ChildrenPrice);
                    txtCollection.Text = Model.SetMode;
                    txtDayCount.Text = Model.Days.ToString();
                    txtEndTraffic.Text = Model.ReturnTraffic;
                    txtPathRemark.Text = Model.PathRemark;
                    //txtFile
                    txtOtherPrice.Text = Utils.FilterEndOfTheZeroDecimal(Model.OtherPrice);
                    txtPathDesc.Text = Model.LineIntro;
                    if (dotype == "update")
                    {
                        txtPathName.Text = Model.RouteName;
                    }
                    if (dotype == "copy")
                    {
                        this.HidRouteAreaID.Value = Model.AreaId.ToString();
                    }
                    txtStartTraffic.Text = Model.DepartureTraffic;
                    txtTotalPrice.Text = Utils.FilterEndOfTheZeroDecimal(Model.TotalPrice);
                    this.radyes.Checked = true;
                    AreaID = Model.AreaId;
                    if (Model.IsShare == false)
                    {
                        this.radyes.Checked = false;
                        this.radno.Checked = true;
                    }

                    if (Model.IsTourOrSubentry)
                    {
                        ForeignQuote1.IsTourOrSubentry = true;//团
                        if (!string.IsNullOrEmpty(Model.Service))
                        {
                            ForeignQuote1.GroupService = Model.Service;
                        }
                    }
                    else
                    {
                        ForeignQuote1.IsTourOrSubentry = false;//分项
                        if (Model.StandardModelList != null)
                        {
                            this.ForeignQuote1.StandardRouteList = Model.StandardModelList;
                        }
                    }
                    if (Model.ServicesModel != null)
                    {
                        this.CostAccounting1.ChildServiceItem = Model.ServicesModel.ChildServiceItem;
                        //this.CostAccounting1.CostCalculation=Model.ServicesModel.
                        this.CostAccounting1.InsiderInfor = Model.ServicesModel.InsiderInfor;
                        //this.CostAccounting1.IsSanPin
                        this.CostAccounting1.NeedAttention = Model.ServicesModel.NeedAttention;
                        this.CostAccounting1.NoNeedItem = Model.ServicesModel.NoNeedItem;
                        this.CostAccounting1.OwnExpense = Model.ServicesModel.OwnExpense;
                        //this.CostAccounting1.ServiceStandard=Model.ServicesModel.
                        this.CostAccounting1.ShoppingItem = Model.ServicesModel.ShoppingItem;
                        this.CostAccounting1.WarmRemind = Model.ServicesModel.WarmRemind;
                    }
                    if (Model.PlanModelList != null)
                    {
                        Journey1.SetPlanList = Model.PlanModelList;
                    }
                    if (Model.Attach != null)
                    {
                        StringBuilder strFile = new StringBuilder();
                        if (Model.Attach.FilePath != "")
                        {
                            strFile.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"PathEditPage.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideFileInfo\" value='{1}|{0}'/></span>", Model.Attach.FilePath, Model.Attach.Name);
                        }
                        this.lbFiles.Text = strFile.ToString();
                    }
                    if (Model.VisaInfoList != null && Model.VisaInfoList.Count > 0)
                    {
                        string visaStr = string.Empty;
                        for (int i = 0; i < Model.VisaInfoList.Count; i++)
                        {
                            visaStr += "<span class='upload_filename'>&nbsp;<a href='" + Model.VisaInfoList[i].FilePath + "' target='_blank'>" + Model.VisaInfoList[i].Name + "</a><a href='javascript:void(0);' onclick='PathEditPage.DelFile(this);return false;'> <img style='vertical-align:middle' src='/images/cha.gif'></a><input type='hidden' name='hideVisaFile' value='" + Model.VisaInfoList[i].Name + "|" + Model.VisaInfoList[i].FilePath + "|" + Model.VisaInfoList[i].Downloads.ToString() + "'></span>";
                        }
                        this.lbqzinfo.Text = visaStr;
                    }
                }
                else
                {
                    Utils.ResponseGoBack();
                }
            }
        }
        /// <summary>
        /// 保存执行方法
        /// </summary>
        private string PageSave(string id, string doType)
        {
            string msg = string.Empty;
            //t为false为编辑，true时为新增
            bool t = String.Equals(doType, "update", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(id) ? false : true;
            string AdultPrice = Utils.GetFormValue(txtAdultPrice.UniqueID);//成人价格
            string ChildPrice = Utils.GetFormValue(txtChildPrice.UniqueID);//儿童价格
            string TotalPrice = Utils.GetFormValue(txtTotalPrice.UniqueID);//合计价格
            string Collection = Utils.GetFormValue(txtCollection.UniqueID);//集合方式
            string DayCount = Utils.GetFormValue(txtDayCount.UniqueID);//天数
            string StartTraffic = Utils.GetFormValue(txtStartTraffic.UniqueID);//出发交通
            string EndTraffic = Utils.GetFormValue(txtEndTraffic.UniqueID);//返程交通
            //txtFile 附件
            string OtherPrice = Utils.GetFormValue(txtOtherPrice.UniqueID);//其他费用
            string PathDesc = Utils.GetFormValue(txtPathDesc.UniqueID);//线路描述
            string PathName = Utils.GetFormValue(txtPathName.UniqueID).Trim();//线路名称
            string AreaID =   Utils.GetFormValue(HidRouteAreaID.UniqueID);//线路区域编号
            string StrRemark = Utils.GetFormValue(txtPathRemark.UniqueID);//获取价格备注
            if (string.IsNullOrEmpty(PathName))
            {
                msg = UtilsCommons.AjaxReturnJson("0", "请输入线路名称");
                return msg;
            }
            EyouSoft.Model.SourceStructure.MRoute ModelRoute = new EyouSoft.Model.SourceStructure.MRoute();
            EyouSoft.BLL.SourceStructure.BSource bll = new EyouSoft.BLL.SourceStructure.BSource();
            if (!string.IsNullOrEmpty(id) && doType == "update")
            {
                ModelRoute = bll.GetRouteModel(id);
                ModelRoute.RouteId = id;
                if (string.IsNullOrEmpty(AreaID))
                {
                    AreaID = ModelRoute.AreaId.ToString();
                }
            }
            if (Utils.GetInt(AreaID) == 0)
            {
                msg = UtilsCommons.AjaxReturnJson("0", "请选择线路区域!");
                return msg;
            }
            //合同附件
            //合同附件(新)
            string[] hdUpload = Utils.GetFormValues(this.UploadControl1.ClientHideID);
            string[] oldhdUpload = Utils.GetFormValues("hideFileInfo");
            #region 合同附件
            EyouSoft.Model.ComStructure.MComAttach hdModel = new EyouSoft.Model.ComStructure.MComAttach();
            if (oldhdUpload.Length > 0)
            {
                for (int i = 0; i < oldhdUpload.Length; i++)
                {
                    hdModel.FilePath = oldhdUpload[i].Split('|')[1];
                    hdModel.ItemType = EyouSoft.Model.EnumType.ComStructure.AttachItemType.线路附件;
                    hdModel.Name = oldhdUpload[i].Split('|')[0];
                    hdModel.Size = 0;
                }
            }
            if (hdUpload.Length > 0)
            {
                for (int i = 0; i < hdUpload.Length; i++)
                {
                    if (hdUpload[i].Trim() != "")
                    {
                        if (hdUpload[i].Split('|').Length > 1)
                        {
                            hdModel.Downloads = 0;
                            hdModel.FilePath = hdUpload[i].Split('|')[1];
                            hdModel.ItemType = EyouSoft.Model.EnumType.ComStructure.AttachItemType.线路附件;
                            hdModel.Name = hdUpload[i].Split('|')[0];
                            hdModel.Size = 0;
                        }
                    }
                }
            }


            ModelRoute.Attach = hdModel;
            #endregion


            #region 签证附件
            //签证附件(新)
            string[] visaUpload = Utils.GetFormValues(this.UploadControl2.ClientHideID);
            string[] oldVisaUpload = Utils.GetFormValues("hideVisaFile");
            IList<EyouSoft.Model.ComStructure.MComAttach> visaList = null;
            if (oldVisaUpload.Length > 0)
            {
                if (visaList == null)
                {
                    visaList = new List<EyouSoft.Model.ComStructure.MComAttach>();
                }
                for (int i = 0; i < oldVisaUpload.Length; i++)
                {
                    EyouSoft.Model.ComStructure.MComAttach visaModel = new EyouSoft.Model.ComStructure.MComAttach();
                    visaModel.FilePath = oldVisaUpload[i].Split('|')[1];
                    visaModel.ItemType = EyouSoft.Model.EnumType.ComStructure.AttachItemType.线路签证资料;
                    visaModel.Name = oldVisaUpload[i].Split('|')[0];
                    visaList.Add(visaModel);
                }
            }
            if (visaUpload.Length > 0)
            {
                if (visaList == null)
                {
                    visaList = new List<EyouSoft.Model.ComStructure.MComAttach>();
                }
                for (int i = 0; i < visaUpload.Length; i++)
                {
                    if (visaUpload[i].Trim() != "")
                    {
                        if (visaUpload[i].Split('|').Length > 1)
                        {
                            EyouSoft.Model.ComStructure.MComAttach visaModel = new EyouSoft.Model.ComStructure.MComAttach();
                            visaModel.FilePath = visaUpload[i].Split('|')[1];
                            visaModel.ItemType = EyouSoft.Model.EnumType.ComStructure.AttachItemType.线路签证资料;
                            visaModel.Name = visaUpload[i].Split('|')[0];
                            visaList.Add(visaModel);
                        }
                    }
                }
            }
            ModelRoute.VisaInfoList = visaList;
            #endregion
            //对外报价
            ModelRoute.PlanModelList = UtilsCommons.GetPlanList();//行程安排
            #region 线路服务赋值
            string costCalculation = string.Empty;
            EyouSoft.Model.TourStructure.MTourService tourService = UtilsCommons.GetTourService(out costCalculation);
            ModelRoute.ServicesModel = tourService;
            #endregion
            ModelRoute.AdultPrice = Utils.GetDecimal(AdultPrice);
            ModelRoute.AreaId = Utils.GetInt(AreaID);
            ModelRoute.ChildrenPrice = Utils.GetDecimal(ChildPrice);
            ModelRoute.OtherPrice = Utils.GetDecimal(OtherPrice);
            ModelRoute.CompanyId = this.SiteUserInfo.CompanyId;
            ModelRoute.Days = Utils.GetInt(DayCount);
            ModelRoute.DepartureTraffic = StartTraffic;
            ModelRoute.ReturnTraffic = EndTraffic;
            ModelRoute.SetMode = Collection;
            ModelRoute.LineIntro = PathDesc;
            ModelRoute.RouteName = PathName;
            ModelRoute.PathRemark = StrRemark;
            ModelRoute.OperatorId = this.SiteUserInfo.UserId;
            ModelRoute.DeptId = this.SiteUserInfo.DeptId;
            ModelRoute.IsShare = true;
            if (radno.Checked)
            {
                ModelRoute.IsShare = false;
            }
            ModelRoute.IssueTime = DateTime.Now;
            #region 对外报价赋值
            ForeignQuote1.IsFromTourOrRoute = false;


            Dictionary<string, object> Standarlist = UtilsCommons.GetServiceType();
            bool IsTourOrSubentry = (bool)Standarlist["IsTourOrSubentry"];
            ModelRoute.IsTourOrSubentry = IsTourOrSubentry;
            ModelRoute.StandardModelList = new List<EyouSoft.Model.SourceStructure.MRouteStandard>();
            if (IsTourOrSubentry)
            {
                ModelRoute.Service = Standarlist["Service"].ToString();
            }
            else
            {
                ModelRoute.StandardModelList = (IList<EyouSoft.Model.SourceStructure.MRouteStandard>)Standarlist["Service"];

            }
            #endregion
            ModelRoute.TotalPrice = Utils.GetDecimal(TotalPrice);
            int result = 0;
            if (t)
            {
                //返回值 -1:已经存在相同的线路名称 -2;同一个行程已经存在相同的景点 -3:已经存在相同的附件名称 -4:公司编号未赋值 -5:线路必填信息不完善 0:事务回滚 1:正确
                result = bll.AddRouteModel(ModelRoute);
            }
            else
            {
                result = bll.UpdateRouteModel(ModelRoute);

            }
            msg = GetResult(result, t ? "新增" : "修改");
            return msg;
        }
        private string GetResult(int result, string type)
        {
            string msg = string.Empty;
            switch (result)
            {
                case 1:
                    msg = "{\"result\":\"1\",\"msg\":\"" + type + "成功!\"}";
                    break;
                case -1:
                    msg = "{\"result\":\"0\",\"msg\":\"已经存在相同的线路名称!\"}";
                    break;
                case -2:
                    msg = "{\"result\":\"0\",\"msg\":\"同一个行程已经存在相同的景点!\"}";
                    break;
                case -3:
                    msg = "{\"result\":\"0\",\"msg\":\"已经存在相同的附件名称!\"}";
                    break;
                case -4:
                    msg = "{\"result\":\"0\",\"msg\":\"公司编号未赋值!\"}";
                    break;
                case -5:
                    msg = "{\"result\":\"0\",\"msg\":\"线路必填信息不完善!\"}";
                    break;
                default:
                    msg = "{\"result\":\"0\",\"msg\":\"" + type + "失败!\"}";
                    break;
            }
            return msg;
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl(string dotype)
        {
            if (String.Equals(dotype, "update", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_线路资源_修改))
                {
                    btnSave.Visible = false;
                }
            }
            else
            {
                if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.资源管理_线路资源_新增))
                {
                    btnSave.Visible = false;
                }
            }
        }
    }
}
