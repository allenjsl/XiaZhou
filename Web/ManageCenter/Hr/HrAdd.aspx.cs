using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Common.Function;
using System.Text;
using EyouSoft.BLL.GovStructure;
using EyouSoft.Model.GovStructure;
using EyouSoft.BLL.ComStructure;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.EnumType.GovStructure;
namespace Web.ManageCenter.Hr
{
    /// <summary>
    /// 行政中心-人事档案-添加
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2011-9-20
    public partial class HrAdd : BackPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            string doType = Request.QueryString["doType"];
            string save = Utils.GetQueryStringValue("save");
            //检查用户名是否唯一
            if (String.Equals(doType, "checkUserName"))
            {
                this.CheckUserName();
            }
            //检查档案编号是否唯一
            if (String.Equals(doType, "checkFileNum"))
            {
                this.CheckFileNum();
            }
            if (save == "save")
            {
                PageSave(doType);
            }
            if (!IsPostBack)
            {
                string id = Utils.GetQueryStringValue("id");
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
            //性别
            this.selSex.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender));
            this.selSex.DataValueField = "Value";
            this.selSex.DataTextField = "Text";
            this.selSex.DataBind();
            //员工类型
            this.selType.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.StaffType));
            this.selType.DataValueField = "Value";
            this.selType.DataTextField = "Text";
            this.selType.DataBind();
            //员工状态
            this.selState.DataSource = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.StaffStatus));
            this.selState.DataValueField = "Value";
            this.selState.DataTextField = "Text";
            this.selState.DataBind();
            #endregion

            #region 临时变量
            string sex = string.Empty;//性别
            string type = string.Empty;//类型
            string state = string.Empty;//员工状态
            string packState = string.Empty;//合同状态
            #endregion

            //编辑初始化
            if (!string.IsNullOrEmpty(id))
            {
                BArchives BLL = new BArchives();
                MGovFile Model = BLL.GetArchivesModel(id);
                if (null == Model)
                    return;
                #region 表单赋值
                //主键id
                this.hdKeyid.Value = Model.ID;
                this.hdUserid.Value = Model.UserId.Trim();
                this.hidGuid.Value = Model.GuideId.Trim();
                //档案编号
                this.tbNum.Text = Model.FileNumber;
                //姓名
                this.tbName.Text = Model.Name;
                //性别
                sex = Convert.ToString((int)Model.Sex);
                //类型
                type = Convert.ToString((int)Model.StaffType);
                //状态
                state = Convert.ToString((int)Model.StaffStatus);
                //身份证
                this.tbIdcard.Text = Model.IDNumber;
                //出生日期
                this.tbBirthDay.Text = string.Format("{0:yyyy-MM-dd}", Model.BirthDate);
                //入职时间 
                this.tbIntime.Text = string.Format("{0:yyyy-MM-dd}", Model.EntryTime);
                //工龄
                this.tbWorkyear.Text = Convert.ToString(Model.LengthService);
                //婚姻状态 
                this.selWed.SelectedIndex = this.selWed.Items.IndexOf(this.selWed.Items.FindByValue(Model.IsMarriage ? "1" : "0"));
                //民族
                this.tbNation.Text = Model.Nation;
                //籍贯
                this.tbOrigin.Text = Model.Birthplace;
                //政治面貌
                this.tbGovface.Text = Model.PoliticalFace;
                //联系电话
                this.tbTelnum.Text = Model.Contact;
                //电话短号
                this.tbShortel.Text = Model.ContactShort;
                //手机
                this.tbPhonenum.Text = Model.Mobile;
                //手机短号
                this.tbShortphone.Text = Model.MobileShort;
                //QQ
                this.tbQnum.Text = Model.qq;
                //Email
                this.tbEmail.Text = Model.Email;
                //msn
                this.tbMsn.Text = Model.Msn;
                //住址
                this.tbAddress.Text = Model.Address;
                //基本资料备注
                this.tbRemark.Text = Model.Remarks;
                //用户名
                this.tbUsername.Text = Model.UserName;
                //密码
                //this.tbUserpwd.Attributes.Add("value", Model.Password.Trim());
                //重复密码
                //this.tbrepeatpwd.Attributes.Add("value", Model.Password.Trim());
                this.selSex.SelectedValue = sex;
                this.selType.SelectedValue = type;
                this.selState.SelectedValue = state;
                //部门
                this.SelectSection1.SectionID = Model.DepartId.ToString();
                this.SelectSection1.SectionName = Model.DepartName;
                //职务
                this.SelectDuty1.DutyID = GetDutyInfo(Model.GovFilePositionList, 1);
                this.SelectDuty1.DutyName = GetDutyInfo(Model.GovFilePositionList, 2);
                if (!string.IsNullOrEmpty(Model.UserId.Trim()))
                {
                    this.rbSetSysID.SelectedValue = "1";
                }
                if (!string.IsNullOrEmpty(Model.GuideId.Trim()))
                {
                    this.rbGuid.SelectedValue = "1";
                }

                if (!string.IsNullOrEmpty(Model.StaffPhoto))
                {
                    //照片
                    this.lbPhoto.Text = string.Format("<span  class='upload_filename'><a href='{0}' target='_blank'>查看</a><a href=\"javascript:void(0)\"  onclick=\"PageJsData.DelFile(this)\" title='删除照片'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" id=\"hideFileInfo_photo\" name=\"hideFileInfo_photo\" value=\"{0}\"/></span>", Model.StaffPhoto);
                }
                StringBuilder strFile = new StringBuilder();
                IList<EyouSoft.Model.ComStructure.MComAttach> fileLst = Model.ComAttachList;
                if (null != fileLst && fileLst.Count > 0)
                {
                    for (int i = 0; i < fileLst.Count; i++)
                    {
                        strFile.AppendFormat("<span  class='upload_filename'><a href='/CommonPage/FileDownLoad.aspx?doType=downLoad&filePath={0}&name={1}' target='_blank'>{1}</a><a href=\"javascript:void(0)\" onclick=\"PageJsData.DelFile(this)\" title='删除附件'><img style='vertical-align:middle' src='/images/cha.gif'></a><input type=\"hidden\" name=\"hideFileInfo\" value='{1}|{0}'/></span>", fileLst[i].FilePath, fileLst[i].Name);
                    }
                }
                this.lbFiles.Text = strFile.ToString();//附件
                //家庭信息
                if (Model.GovFilehomeList != null && Model.GovFilehomeList.Count > 0)
                {
                    this.rpt_homelist.DataSource = Model.GovFilehomeList;
                    this.rpt_homelist.DataBind();
                    this.ph_homelistUpdate.Visible = true;
                    this.ph_homelistAdd.Visible = false;
                }
                else
                {
                    this.ph_homelistUpdate.Visible = false;
                    this.ph_homelistAdd.Visible = true;
                }
                //学历信息
                if (Model.GovFileEducationList != null && Model.GovFileEducationList.Count > 0)
                {
                    this.rpt_schoollist.DataSource = Model.GovFileEducationList;
                    this.rpt_schoollist.DataBind();
                    this.ph_schoollistUpdate.Visible = true;
                    this.ph_schoollistAdd.Visible = false;
                }
                else
                {
                    this.ph_schoollistUpdate.Visible = false;
                    this.ph_schoollistAdd.Visible = true;
                }

                //履历信息
                if (Model.GovFileCurriculumList != null && Model.GovFileCurriculumList.Count > 0)
                {
                    this.rpt_worllist.DataSource = Model.GovFileCurriculumList;
                    this.rpt_worllist.DataBind();
                    this.ph_worklistUpdate.Visible = true;
                    this.ph_worklistAdd.Visible = false;
                }
                else
                {
                    this.ph_worklistUpdate.Visible = false;
                    this.ph_worklistAdd.Visible = true;
                }

                //劳动合同
                if (Model.GovFileContractList != null && Model.GovFileContractList.Count > 0)
                {
                    this.rpt_contract.DataSource = Model.GovFileContractList;
                    this.rpt_contract.DataBind();
                    this.ph_contractUpdate.Visible = true;
                    this.ph_contractAdd.Visible = false;
                }
                else
                {
                    this.ph_contractUpdate.Visible = false;
                    this.ph_contractAdd.Visible = true;
                }
                #endregion
               
                /*if (!this.SiteUserInfo.IsHandleElse && this.SiteUserInfo.UserId != Model.OperatorId)
                {
                    this.ph_Save.Visible = false;
                }*/
            }
            else
            {
                this.ph_homelistUpdate.Visible = false;
                this.ph_homelistAdd.Visible = true;
                this.ph_schoollistUpdate.Visible = false;
                this.ph_schoollistAdd.Visible = true;
                this.ph_worklistUpdate.Visible = false;
                this.ph_worklistAdd.Visible = true;
                this.ph_contractUpdate.Visible = false;
                this.ph_contractAdd.Visible = true;
            }
        }
        /// <summary>
        /// 保存按钮点击事件执行方法
        /// </summary>
        protected void PageSave(string doType)
        {
            #region 表单取值
            //主键编号
            string Keyid = Utils.GetFormValue(hdKeyid.UniqueID);
            //档案 编号
            string filenumber = Utils.GetFormValue(tbNum.UniqueID);
            //员工姓名
            string name = Utils.GetFormValue(tbName.UniqueID);
            //身份证号码
            string idnumber = Utils.GetFormValue(tbIdcard.UniqueID);
            //出生日期
            string birthday = Utils.GetFormValue(tbBirthDay.UniqueID);
            //所属部门编号
            string deptid = Utils.GetFormValue(SelectSection1.SelectIDClient);
            //所属部门名称
            string deptname = Utils.GetFormValue(SelectSection1.SelectNameClient);
            //职务编号
            string dutyid = Utils.GetFormValue(SelectDuty1.DutyIDClient);
            //职务名称
            string dutyname = Utils.GetFormValue(SelectDuty1.DutyNameClient);
            //入职时间
            string intime = Utils.GetFormValue(tbIntime.UniqueID);
            //工龄
            string lengthservice = Utils.GetFormValue(tbWorkyear.UniqueID);
            //民族
            string nation = Utils.GetFormValue(tbNation.UniqueID);
            //籍贯
            string birthplace = Utils.GetFormValue(tbOrigin.UniqueID);
            //政治面貌
            string govface = Utils.GetFormValue(tbGovface.UniqueID);
            //联系电话
            string contact = Utils.GetFormValue(tbTelnum.UniqueID);
            //联系短号
            string contactshort = Utils.GetFormValue(tbShortel.UniqueID);
            //手机号码
            string phone = Utils.GetFormValue(tbPhonenum.UniqueID);
            //手机短号
            string phoneshort = Utils.GetFormValue(tbShortphone.UniqueID);
            //QQ号码
            string qq = Utils.GetFormValue(tbQnum.UniqueID);
            //MSN帐号
            string msn = Utils.GetFormValue(tbMsn.UniqueID);
            //Email
            string email = Utils.GetFormValue(tbEmail.UniqueID);
            //住址
            string address = Utils.GetFormValue(tbAddress.UniqueID);
            //备注
            string remark = Utils.GetFormValue(tbRemark.UniqueID);
            //是否分配系统帐号
            bool isuser = Utils.GetFormValue(rbSetSysID.UniqueID) == "1" ? true : false;
            //是否同步导游档案
            bool isguid = Utils.GetFormValue(rbGuid.UniqueID) == "1" ? true : false;
            //用户名
            string username = Utils.GetFormValue("u");
            //用户编号
            string userid = Utils.GetFormValue(hdUserid.UniqueID);
            //密码
            string userpwd = Utils.GetFormValue("p").Trim();
            //导游编号
            string guid = Utils.GetFormValue(hidGuid.UniqueID).Trim();
            string sex = Utils.GetFormValue(selSex.UniqueID);
            string type = Utils.GetFormValue(selType.UniqueID);
            string state = Utils.GetFormValue(selState.UniqueID);
            string ismarriage = Utils.GetFormValue(selWed.UniqueID);
            //家庭关系
            List<MGovFilehome> homeLst = new List<MGovFilehome>();
            MGovFilehome homeModel;
            string[] txtHomeRelation = Utils.GetFormValues("txtHomeRelation");//关系
            string[] txtHomeName = Utils.GetFormValues("txtHomeName");//姓名
            string[] txtHomeTel = Utils.GetFormValues("txtHomeTel");//电话
            string[] txtHomeUnit = Utils.GetFormValues("txtHomeUnit");//工作单位
            string[] txtHomeAddress = Utils.GetFormValues("txtHomeAddress");//地址
            for (int i = 0; i < txtHomeRelation.Length; i++)
            {
                homeModel = new MGovFilehome();
                homeModel.Address = txtHomeAddress[i];
                homeModel.Name = txtHomeName[i];
                homeModel.Phone = txtHomeTel[i];
                homeModel.Relationship = txtHomeRelation[i];
                homeModel.WorkUnit = txtHomeUnit[i];
                homeLst.Add(homeModel);
            }
            //学历信息
            List<MGovFileEducation> schoolLst = new List<MGovFileEducation>();
            MGovFileEducation schoolModel;
            string[] txtStartTime = Utils.GetFormValues("txtStartTime");//开始时间
            string[] txtEndTime = Utils.GetFormValues("txtEndTime");//结束时间
            string[] selEdu = Utils.GetFormValues("selEdu");//学历
            string[] txtSpeciality = Utils.GetFormValues("txtSpeciality");//所学专业
            string[] txtSchool = Utils.GetFormValues("txtSchool");//毕业院校
            string[] selEduState = Utils.GetFormValues("selEduState");//状态
            string[] txtEduRemark = Utils.GetFormValues("txtEduRemark");//备注
            for (int i = 0; i < txtStartTime.Length; i++)
            {
                schoolModel = new MGovFileEducation();
                schoolModel.Education = selEdu[i];
                schoolModel.EndTime = Utils.GetDateTimeNullable(txtEndTime[i]);
                schoolModel.Graduated = txtSchool[i];
                schoolModel.Profession = txtSpeciality[i];
                schoolModel.Remarks = txtEduRemark[i];
                schoolModel.StartTime = Utils.GetDateTimeNullable(txtStartTime[i]);
                schoolModel.Statue = (EyouSoft.Model.EnumType.GovStructure.Statue)Utils.GetInt(selEduState[i]);
                schoolLst.Add(schoolModel);
            }
            //履历信息
            List<MGovFileCurriculum> workLst = new List<MGovFileCurriculum>();
            MGovFileCurriculum workModel;
            string[] txtWorkStartTime = Utils.GetFormValues("txtWorkStartTime");//开始时间
            string[] txtWorkEndTime = Utils.GetFormValues("txtWorkEndTime");//结束时间
            string[] txtWorkPlace = Utils.GetFormValues("txtWorkPlace");//工作地点
            string[] txtWorkUnit = Utils.GetFormValues("txtWorkUnit");//工作单位
            string[] txtWorkDut = Utils.GetFormValues("txtWorkDuty");//从事职业
            string[] txtWorkRemark = Utils.GetFormValues("txtWorkRemark");//备注                
            for (int i = 0; i < txtWorkStartTime.Length; i++)
            {
                workModel = new MGovFileCurriculum();
                workModel.EndTime = Utils.GetDateTimeNullable(txtWorkEndTime[i]);
                workModel.Location = txtWorkPlace[i];
                workModel.Occupation = txtWorkDut[i];
                workModel.Remarks = txtWorkRemark[i];
                workModel.StartTime = Utils.GetDateTimeNullable(txtWorkStartTime[i]);
                workModel.WorkUnit = txtWorkUnit[i];
                workLst.Add(workModel);
            }
            //劳动合同
            List<MGovFileContract> contactLst = new List<MGovFileContract>();
            MGovFileContract contactModel;
            string[] txtPackNum = Utils.GetFormValues("txtPackNum");
            string[] txtPackName = Utils.GetFormValues("txtPackName");
            string[] txtPackStartTime = Utils.GetFormValues("txtPackStartTime");
            string[] txtPackEndTime = Utils.GetFormValues("txtPackEndTime");
            string[] selContactState = Utils.GetFormValues("selContactState");
            string[] txtPackRemark = Utils.GetFormValues("txtPackRemark");
            for (int i = 0; i < txtPackNum.Length; i++)
            {
                contactModel = new MGovFileContract();
                contactModel.ContractNumber = txtPackNum[i];
                contactModel.Name = txtPackName[i];
                contactModel.SignedTime = Utils.GetDateTimeNullable(txtPackStartTime[i]);
                contactModel.MaturityTime = Utils.GetDateTimeNullable(txtPackEndTime[i]);
                contactModel.Status = (EyouSoft.Model.EnumType.GovStructure.FileContractStatus)Utils.GetInt(selContactState[i]);
                contactModel.Remarks = txtPackRemark[i];
                contactLst.Add(contactModel);
            }
            #endregion

            #region 数据验证
            string msg = "";
            int result = 0;
            if (string.IsNullOrEmpty(filenumber))
            {
                msg += "-请输入档案编号！<br/>";
            }
            if (string.IsNullOrEmpty(name))
            {
                msg += "-请输入名称！<br/>";
            }
            if (string.IsNullOrEmpty(idnumber))
            {
                msg += "-请输入身份证号码！<br/>";
            }
            if (string.IsNullOrEmpty(idnumber))
            {
                msg += "-请输入身份证号码！<br/>";
            }
            if (string.IsNullOrEmpty(deptid) && string.IsNullOrEmpty(deptname))
            {
                msg += "-请请选择员工所属部门！<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                Response.Clear();
                Response.Write(UtilsCommons.AjaxReturnJson(result.ToString(), msg));
                Response.End();
                return;
            }
            if (string.IsNullOrEmpty(idnumber))
            {
                msg += "-请输入身份证号码！";
            }
            #endregion

            #region 实体赋值
            MGovFile Model = new MGovFile();
            Model.Address = address;
            Model.BirthDate = Utils.GetDateTimeNullable(birthday);
            Model.Birthplace = birthplace;
            Model.DepartId = Utils.GetInt(deptid);
            Model.DepartName = deptname;
            Model.GovFilePositionList = GetDutyList(dutyid, dutyname);
            Model.CompanyId = this.SiteUserInfo.CompanyId;
            Model.Contact = contact;
            Model.ContactShort = contactshort;
            Model.Email = email;
            Model.EntryTime = Utils.GetDateTimeNullable(intime);
            Model.FileNumber = filenumber;
            Model.ID = Keyid;
            Model.IDNumber = idnumber;
            Model.IsMarriage = ismarriage == "1" ? true : false;
            Model.IssueTime = DateTime.Now;
            Model.LengthService = Utils.GetInt(lengthservice);
            Model.Mobile = phone;
            Model.MobileShort = phoneshort;
            Model.Msn = msn;
            Model.Name = name;
            Model.Nation = nation;
            Model.Operator = this.SiteUserInfo.Name;
            Model.OperatorId = this.SiteUserInfo.UserId;
            Model.OperDeptId = this.SiteUserInfo.DeptId;
            Model.Password = userpwd;//密码
            Model.PoliticalFace = govface;
            Model.qq = qq;
            Model.Remarks = remark;
            Model.Sex = (Gender)Utils.GetInt(sex);
            Model.StaffStatus = (StaffStatus)Utils.GetInt(state);
            Model.StaffType = (StaffType)Utils.GetInt(type);
            Model.UserId = userid;
            Model.UserName = username;//用户名
            Model.GuideId = guid;
            //家庭信息
            if (homeLst != null && homeLst.Count > 0)
            {
                Model.GovFilehomeList = homeLst;
            }
            //履历信息
            if (workLst != null && workLst.Count > 0)
            {
                Model.GovFileCurriculumList = workLst;
            }
            //学历信息
            if (schoolLst != null && schoolLst.Count > 0)
            {
                Model.GovFileEducationList = schoolLst;
                Model.Education = schoolLst.Last().Education;
            }
            //合同日期
            if (contactLst != null && contactLst.Count > 0)
            {
                Model.GovFileContractList = contactLst;
                Model.MaturityTime = Utils.GetDateTime(contactLst.Last().MaturityTime.ToString());
            }
            NewGetAttach(Model);
            #endregion

            if (string.IsNullOrEmpty(Model.UserName)) isuser = false;

            #region 保存提交
            BArchives BLL = new BArchives();
            if (doType == "add")
            {
                result = BLL.AddArchives(Model, isuser, isguid);
                switch (result)
                {
                    case 1:
                        msg = "添加成功！";
                        break;
                    case 0:
                        msg = "添加失败！";
                        break;
                    case -1:
                        msg = "存在相同的身份证号!";
                        break;
                    case -6:
                        msg = "导游信息不完整!";
                        break;
                    case -5:
                        msg = "同步导游必须同步用户!";
                        break;
                    case -7: msg = "已经存在相同的用户名"; break;
                    default:
                        break;
                }
            }
            else
            {
                result = BLL.UpdateArchives(Model, isuser, isguid);
                switch (result)
                {
                    case 1:
                        msg = "修改成功！";
                        break;
                    case 0:
                        msg = "修改失败！";
                        break;
                    case -1:
                        msg = "存在相同的身份证号!";
                        break;
                    case -2:
                        msg = "已经存在相同的用户名称!";
                        break;
                    case -3:
                        msg = "已经存在相同的身份证号的导游!";
                        break;
                    case -4:
                        msg = "用户名或密码不能为空!";
                        break;
                    case -5:
                        msg = "同步导游必须同步用户!";
                        break;
                    case -6:
                        msg = "导游信息不完整!";
                        break;
                    case -7: msg = "已经存在相同的用户名"; break;
                    default:
                        break;
                }

            }
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(result.ToString(), msg));
            Response.End();
            #endregion
        }

        private void NewGetAttach(MGovFile Model)
        {
            //之前上传的照片
            string stroldphoto = Utils.GetFormValue("hideFileInfo_photo");
            if (!string.IsNullOrEmpty(stroldphoto))
            {
                Model.StaffPhoto = stroldphoto;
            }
            //新上传的照片
            string[] photo = Utils.GetFormValue(this.SingleFileUpload2.ClientHideID).Split('|');
            if (photo != null && photo.Length > 1)
            {
                Model.StaffPhoto = photo[1];
            }
            //之前上传的附件
            string stroldupload = Utils.GetFormValue("hideFileInfo");
            IList<MComAttach> lst = new List<MComAttach>();
            if (!string.IsNullOrEmpty(stroldupload))
            {
                string[] oldupload = stroldupload.Split(',');
                if (oldupload != null && oldupload.Length > 0)
                {
                    for (int i = 0; i < oldupload.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(oldupload[i]))
                        {
                            string[] uploaditem = oldupload[i].Split('|');
                            MComAttach attModel = new MComAttach();
                            attModel.Name = uploaditem[0];
                            attModel.FilePath = uploaditem[1];
                            lst.Add(attModel);
                        }
                    }
                }
            }
            //新上传附件
            string[] upload = Utils.GetFormValues(this.SingleFileUpload1.ClientHideID);
            for (int i = 0; i < upload.Length; i++)
            {
                string[] newupload = upload[i].Split('|');
                if (newupload != null && newupload.Length > 1)
                {
                    MComAttach attModel = new MComAttach();
                    attModel.FilePath = newupload[1];
                    attModel.Name = newupload[0];
                    lst.Add(attModel);
                }
            }
            Model.ComAttachList = lst;
        }

        #region 获取职务信息
        /// <summary>
        /// 获取职务信息
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private string GetDutyInfo(IList<MGovFilePosition> lst, int i)
        {
            string str = string.Empty;
            if (lst != null && lst.Count > 0)
            {
                if (i == 1)
                {
                    foreach (MGovFilePosition item in lst)
                    {
                        str += item.PositionId + ",";
                    }
                }
                else
                {
                    foreach (MGovFilePosition item in lst)
                    {
                        str += item.Title + ",";
                    }
                }
                return str.Length > 0 ? str.Substring(0, str.Length - 1) : "";
            }
            return string.Empty;
        }
        #endregion

        private IList<MGovFilePosition> GetDutyList(string ids, string names)
        {
            IList<MGovFilePosition> list = new List<MGovFilePosition>();
            if (!string.IsNullOrEmpty(ids) && !string.IsNullOrEmpty(names))
            {

                MGovFilePosition model = new MGovFilePosition();
                string[] id = ids.Split(',');
                string[] name = names.Split(',');
                for (int i = 0; i < id.Length; i++)
                {
                    model.PositionId = Utils.GetInt(id[i]);
                    model.Title = name[i];
                    list.Add(model);
                }
            }
            return list;
        }

        #region 获取学历和状态的option
        /// <summary>
        /// 获取学历和状态
        /// </summary>
        /// <param name="i"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string getOptions(int i, int? value)
        {
            StringBuilder strBld = new StringBuilder();
            switch (i)
            {
                case 0://学历
                    strBld.Append(this.getEnumInfo(typeof(EyouSoft.Model.EnumType.GovStructure.Education), value));
                    break;
                case 1://状态
                    strBld.Append(this.getEnumInfo(typeof(EyouSoft.Model.EnumType.GovStructure.Statue), value));
                    break;
                case 2:
                    strBld.Append(this.getEnumInfo(typeof(EyouSoft.Model.EnumType.GovStructure.FileContractStatus), value));
                    break;
            }
            return strBld.ToString();
        }

        /// <summary>
        /// 获取select的option
        /// </summary>
        /// <param name="t"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string getEnumInfo(Type t, int? value)
        {
            StringBuilder str = new StringBuilder();
            //str.Append("<option value=\"-1\">请选择</option>");
            List<EnumObj> lst = EnumObj.GetList(t);
            if (null != lst && lst.Count > 0)
            {
                str.AppendFormat("<option value='-1'>请选择</option>");
                foreach (EnumObj m in lst)
                {
                    if (null != value && m.Value == Convert.ToString(value))
                        str.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", m.Value, m.Text);
                    else
                        str.AppendFormat("<option value=\"{0}\">{1}</option>", m.Value, m.Text);
                }
            }
            return str.ToString();
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_人事档案_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_人事档案_栏目, false);
            }
            else
            {
                string doType = Utils.GetQueryStringValue("doType");
                if (doType == "add")
                {
                    /*if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_人事档案_新增))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_人事档案_新增, false);
                    }*/

                    ph_Save.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_人事档案_新增);
                }
                else
                {
                    /*if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_人事档案_修改))
                    {
                        Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_人事档案_修改, false);
                    }*/

                    ph_Save.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_人事档案_修改);
                }
            }
        }
        #endregion


        #region 表单提交的同步验证
        /// <summary>
        ///ajax检查用户名是否存在
        /// </summary>
        protected void CheckUserName()
        {
            String str = String.Empty;
            bool result = false;
            string userName = Request.QueryString["name"];
            if (!String.IsNullOrEmpty(userName.Trim()))
            {

                BComUser bll = new BComUser();
                if (!bll.IsExistsUserName(userName, this.SiteUserInfo.CompanyId, string.Empty))
                {
                    str = "该用户名可用！";
                    result = true;
                }
                else
                {
                    str = "该用户名已存在，请重新输入！";
                }
            }
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(result ? "1" : "0", str));
            Response.End();
        }
        /// <summary>
        /// ajax检查档案编号是否存在
        /// </summary>
        protected void CheckFileNum()
        {
            String str = String.Empty;
            String num = Request.QueryString["num"];
            String id = Request.QueryString["id"];
            if (!String.IsNullOrEmpty(num.Trim()))
            {
                BArchives BLL = new BArchives();
                if (String.IsNullOrEmpty(id))
                {//新增时
                    if (BLL.ExistsFileNumber(num, "", this.SiteUserInfo.CompanyId))
                    {
                        str = "1";
                    }
                }
                else
                { //编辑时
                    MGovFile Model = BLL.GetArchivesModel(id);
                    if (null != Model && !String.Equals(num, Model.FileNumber))
                    {
                        if (BLL.ExistsFileNumber(num, "", this.SiteUserInfo.CompanyId))
                        {
                            str = "1";
                        }
                    }
                }
            }
            Response.Clear();
            Response.Write(str);
            Response.End();
        }
        #endregion
    }
}