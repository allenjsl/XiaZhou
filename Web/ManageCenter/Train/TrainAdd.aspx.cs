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
using EyouSoft.Model.ComStructure;

namespace Web.ManageCenter.Train
{
    /// <summary>
    /// 行政中心-培训管理-添加
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-04-05
    public partial class TrainAdd : BackPage
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
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="id">操作ID</param>
        protected void PageInit(string id)
        {
            #region 用户控件初始化
            this.SingleFileUpload1.CompanyID = this.SiteUserInfo.CompanyId;
            this.SingleFileUpload2.CompanyID = this.SiteUserInfo.CompanyId;
            this.SingleFileUpload3.CompanyID = this.SiteUserInfo.CompanyId;
            #endregion
            //编辑初始化
            if (!string.IsNullOrEmpty(id))
            {
                BTrain BLL = new BTrain();
                MGovTrain Model = BLL.GetGovTrainModel(id);
                if (null != Model)
                {
                    this.hidKeyId.Value = Model.TrainId;
                    this.txtSTime.Text = Model.StateTime.HasValue ? Model.StateTime.Value.ToString("yyyy-MM-dd HH:mm") : "";
                    this.txtETime.Text = Model.EndTime.HasValue ? Model.EndTime.Value.ToString("yyyy-MM-dd HH:mm") : "";
                    this.txtTheme.Text = Model.Title;
                    this.txtJoinpeo.Text = Model.JoinPeople;
                    this.txtPlace.Text = Model.TrainingLocations;
                    this.txtTrainpeo.Text = Model.TrainPeople;
                    this.txtTraincon.Text = Model.Training;
                    NewGetLableText(Model.ComAttachList);
                    if (!this.SiteUserInfo.IsHandleElse && this.SiteUserInfo.UserId != Model.OperatorId)
                    {
                        this.ph_Save.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// label赋值
        /// </summary>
        /// <param name="list"></param>
        private void NewGetLableText(IList<EyouSoft.Model.ComStructure.MComAttach> list)
        {
            if (list != null && list.Count > 0)
            {
                StringBuilder strFile = new StringBuilder();
                StringBuilder strNotice = new StringBuilder();
                StringBuilder strRemark = new StringBuilder();
                List<EyouSoft.Model.ComStructure.MComAttach> notice = list.Where(item => (item.Name.IndexOf("TZ_") != -1)).ToList();
                List<EyouSoft.Model.ComStructure.MComAttach> remark = list.Where(item => (item.Name.IndexOf("JL_") != -1)).ToList();
                List<EyouSoft.Model.ComStructure.MComAttach> file = list.Where(item => (item.Name.IndexOf("FJ_") != -1)).ToList();
                if (notice != null && notice.Count > 0)
                {
                    for (int i = 0; i < notice.Count; i++)
                    {
                        strNotice.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"PageJsData.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideNotice\" value='{1}|{0}'/></span>", notice[i].FilePath, notice[i].Name.Length > 3 ? notice[i].Name.Substring(3, notice[i].Name.Length - 3) : "");
                    }
                }
                if (remark != null && remark.Count > 0)
                {
                    for (int i = 0; i < remark.Count; i++)
                    {
                        strRemark.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"PageJsData.DelFile(this)\"  title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideRemark\" value='{1}|{0}'/></span>", remark[i].FilePath, remark[i].Name.Length > 3 ? remark[i].Name.Substring(3, remark[i].Name.Length - 3) : "");
                    }
                }
                if (file != null && file.Count > 0)
                {
                    for (int i = 0; i < file.Count; i++)
                    {
                        strFile.AppendFormat("<span class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"PageJsData.DelFile(this)\"  title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideFile\" value='{1}|{0}'/></span>", file[i].FilePath, file[i].Name.Length > 3 ? file[i].Name.Substring(3, file[i].Name.Length - 3) : "");
                    }
                }
                this.lbFiles.Text = strFile.ToString();
                this.lbNotice.Text = strNotice.ToString();
                this.lbRemark.Text = strRemark.ToString();
            }
        }


        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave(string doType)
        {
            #region 表单取值
            string trainid = Utils.GetFormValue(hidKeyId.UniqueID);
            string starttime = Utils.GetFormValue(txtSTime.UniqueID);
            string endtime = Utils.GetFormValue(txtETime.UniqueID);
            string title = Utils.GetFormValue(txtTheme.UniqueID);
            string joinpeople = Utils.GetFormValue(txtJoinpeo.UniqueID);
            string trainpeople = Utils.GetFormValue(txtJoinpeo.UniqueID);
            string train = Utils.GetFormValue(txtTraincon.UniqueID);
            string place = Utils.GetFormValue(txtPlace.UniqueID);
            #endregion
            #region 表单验证
            string msg = "";
            bool result = false;
            if (string.IsNullOrEmpty(starttime))
            {
                msg += "-请输入培训开始时间！";
            }
            if (string.IsNullOrEmpty(endtime))
            {
                msg += "-请输入培训结束时间！";
            }
            if (string.IsNullOrEmpty(title))
            {
                msg += "-请输入培训主题！";
            }
            if (string.IsNullOrEmpty(joinpeople))
            {
                msg += "-请输入培训参与人员！";
            }
            if (string.IsNullOrEmpty(trainpeople))
            {
                msg += "-请输入培训人员！";
            }
            if (string.IsNullOrEmpty(place))
            {
                msg += "-请输入培训地点！";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
                Response.End();

            }
            #endregion
            #region 实体赋值
            MGovTrain Model = new MGovTrain();
            Model.CompanyId = this.SiteUserInfo.CompanyId;
            Model.DepartId = this.SiteUserInfo.DeptId;
            Model.EndTime = Utils.GetDateTimeNullable(endtime);
            Model.IssueTime = DateTime.Now;
            Model.JoinPeople = joinpeople;
            Model.OperatorId = this.SiteUserInfo.UserId;
            Model.StateTime = Utils.GetDateTimeNullable(starttime);
            Model.Title = title;
            Model.TrainId = trainid;
            Model.Training = train;
            Model.TrainingLocations = place;
            Model.TrainPeople = trainpeople;
            Model.ComAttachList = NewGetAttach();
            #endregion
            #region 保存提交
            BTrain BLL = new BTrain();
            if (doType == "add")
            {

                result = BLL.AddGovTrain(Model);
                msg = result ? "添加成功！" : "添加失败！";
            }
            else
            {
                result = BLL.UpdateGovTrain(Model);
                msg = result ? "修改成功！" : "修改失败！";
            }
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", msg));
            Response.End();
            #endregion
        }


        private IList<MComAttach> NewGetAttach()
        {
            //之前上传的培训通知 noticename|path|download,noticename|path|download
            IList<MComAttach> lst = new List<MComAttach>();
            string hideNotice = Utils.GetFormValue("hideNotice");
            if (!string.IsNullOrEmpty(hideNotice))
            {
                string[] arrNotice = hideNotice.Split(',');
                if (arrNotice.Length > 0 && arrNotice != null)
                {
                    for (int i = 0; i < arrNotice.Length; i++)
                    {
                        MComAttach model = new MComAttach();
                        if (!string.IsNullOrEmpty(arrNotice[i]))
                        {
                            string[] arry = arrNotice[i].Split('|');
                            if (arry.Length > 1)
                            {
                                model.Name = arry[0].Insert(0, "TZ_");
                                model.FilePath = arry[1];
                                lst.Add(model);
                            }
                        }
                    }
                }
            }
            //新上传的培训通知
            string[] upNotice = Utils.GetFormValues(this.SingleFileUpload1.ClientHideID);
            for (int i = 0; i < upNotice.Length; i++)
            {
                string[] notice = upNotice[i].Split('|');
                if (notice != null && notice.Length > 1)
                {
                    MComAttach model = new MComAttach();
                    model.Name = notice[0].Insert(0, "TZ_");
                    model.FilePath = notice[1];
                    lst.Add(model);
                }
            }
            //之前上传的培训记录
            string hideRemark = Utils.GetFormValue("hideRemark");
            if (!string.IsNullOrEmpty(hideRemark))
            {
                string[] arrRemark = hideRemark.Split(',');
                if (arrRemark.Length > 0 && arrRemark != null)
                {
                    for (int i = 0; i < arrRemark.Length; i++)
                    {
                        MComAttach model = new MComAttach();
                        if (!string.IsNullOrEmpty(arrRemark[i]))
                        {
                            string[] arry = arrRemark[i].Split('|');
                            if (arry.Length > 1)
                            {
                                model.Name = arry[0].Insert(0, "JL_");
                                model.FilePath = arry[1];
                                lst.Add(model);
                            }
                        }
                    }

                }
            }
            //新上传的培训记录
            string[] upRemark = Utils.GetFormValues(this.SingleFileUpload2.ClientHideID);
            for (int i = 0; i < upRemark.Length; i++)
            {
                string[] remark = upRemark[i].Split('|');
                if (remark != null && remark.Length > 1)
                {
                    MComAttach model = new MComAttach();
                    model.Name = remark[0].Insert(0, "JL_");
                    model.FilePath = remark[1];
                    lst.Add(model);
                }
            }
            //之前上传的培训附件
            string hideFile = Utils.GetFormValue("hideFile");
            if (!string.IsNullOrEmpty(hideFile))
            {
                string[] arrFile = hideFile.Split(',');
                if (arrFile.Length > 0 && arrFile != null)
                {
                    for (int i = 0; i < arrFile.Length; i++)
                    {
                        MComAttach model = new MComAttach();
                        if (!string.IsNullOrEmpty(arrFile[i]))
                        {
                            string[] arry = arrFile[i].Split('|');
                            if (arry.Length > 1)
                            {
                                model.Name = arry[0].Insert(0, "FJ_");
                                model.FilePath = arry[1];
                                lst.Add(model);
                            }
                        }
                    }

                }
            }
            //新上传的附件
            string[] upFile = Utils.GetFormValues(this.SingleFileUpload3.ClientHideID);
            for (int i = 0; i < upFile.Length; i++)
            {
                string[] file = upFile[i].Split('|');
                if (file != null && file.Length > 1)
                {
                    MComAttach model = new MComAttach();
                    model.Name = file[0].Insert(0, "FJ_");
                    model.FilePath = file[1];
                    lst.Add(model);
                }
            }
            return lst;
        }

        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_培训管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_培训管理_栏目, false);
            }
            else
            {
                string doType = Utils.GetQueryStringValue("doType");
                if (doType == "add")
                {
                    if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_培训管理_新增))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_培训管理_新增, false);
                    }
                }
                else
                {
                    if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_培训管理_修改))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_培训管理_修改, false);
                    }
                }
            }
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
    }
}