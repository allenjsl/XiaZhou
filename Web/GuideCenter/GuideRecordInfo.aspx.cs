using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using System.Text;

namespace Web.GuideCenter
{
    /// <summary>
    /// 导游中心-新增，修改导游信息
    /// 创建人：李晓欢
    /// 创建时间：2011-09-14
    /// </summary>
    public partial class GuideRecordInfo : EyouSoft.Common.Page.BackPage
    {
        protected string PhonePath = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPrivs();

            string doType = Utils.GetQueryStringValue("action");
            if (doType == "save")
            {
                Response.Clear();
                Response.Write(PageSave());
                Response.End();
            }
            if (doType == "CheckIDNumber")
            {
                Response.Clear();
                Response.Write(CheckIDNumber());
                Response.End();
            }
            PageInit();
            DataInit();
        }

        #region 性别
        /// <summary>
        /// 性别
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected string GetGender(string index)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append("<option value='-1'>--请选择--</option>");
            List<EnumObj> Gender = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender));
            if (string.IsNullOrEmpty(index))
            {
                index = "3";
            }
            if (Gender != null && Gender.Count > 0)
            {
                for (int i = 0; i < Gender.Count; i++)
                {
                    if (Gender[i].Value == index)
                    {
                        sb.Append("<option selected='selected' value='" + Gender[i].Value + "'>" + Gender[i].Text + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value='" + Gender[i].Value + "'>" + Gender[i].Text + "</option>");
                    }
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 等级
        /// <summary>
        /// 等级
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected string GetGuidLeavel(string index)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append("<option value='-1'>--请选择--</option>");
            List<EnumObj> leavel = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SourceStructure.GuideLevel));
            if (leavel != null && leavel.Count > 0)
            {
                for (int i = 0; i < leavel.Count; i++)
                {
                    if (leavel[i].Value == index)
                    {
                        sb.Append("<option selected='selected' value='" + leavel[i].Value + "'>" + leavel[i].Text + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value='" + leavel[i].Value + "'>" + leavel[i].Text + "</option>");
                    }
                }
            }
            return sb.ToString();
        }
        #endregion

        public string CheckIDNumber()
        {
            string msg = "";
            if (new EyouSoft.BLL.SourceStructure.BSource().ExistsIDNumber(Utils.GetQueryStringValue("IDNumber"), Utils.GetQueryStringValue("guideID"), this.SiteUserInfo.CompanyId))
            {
                msg = UtilsCommons.AjaxReturnJson("1", "身份证号码已经存在!");
            }
            else
            {
                msg = UtilsCommons.AjaxReturnJson("0", "身份证号码不存在!");
            }
            return msg;
        }

        #region 保存导游档案信息
        protected void PageInit()
        {
            //绑定导游级别
            this.ddlleaver.Items.Clear();
            List<EnumObj> levelList = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SourceStructure.GuideLevel));
            if (levelList != null && levelList.Count > 0)
            {
                for (int i = 0; i < levelList.Count; i++)
                {
                    ListItem levelltem = new ListItem();
                    levelltem.Value = levelList[i].Value;
                    levelltem.Text = levelList[i].Text;
                    this.ddlleaver.Items.Add(levelltem);
                }
            }

            //绑定导游性别
            this.seGenerlist.Items.Clear();
            List<EnumObj> guidGener = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender));
            if (guidGener != null && guidGener.Count > 0)
            {
                for (int j = 0; j < guidGener.Count; j++)
                {
                    ListItem item = new ListItem();
                    item.Value = guidGener[j].Value;
                    item.Text = guidGener[j].Text;
                    this.seGenerlist.Items.Add(item);
                }
            }

            //导游挂牌单位
            this.dAnchoredComlist.Items.Clear();
            List<EnumObj> AnchoredComlist = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SourceStructure.AnchoredCom));
            if (AnchoredComlist != null && AnchoredComlist.Count > 0)
            {
                for (int i = 0; i < AnchoredComlist.Count; i++)
                {
                    ListItem item = new ListItem();
                    item.Value = AnchoredComlist[i].Value;
                    item.Text = AnchoredComlist[i].Text;
                    this.dAnchoredComlist.Items.Add(item);
                }
            }
        }

        //保存
        protected string PageSave()
        {
            string msg = string.Empty;
            string returnMsg = string.Empty;
            //用户名
            string username = Utils.GetFormValue(this.txtUserNames.UniqueID);
            //密码
            string userpwd = Utils.GetFormValue(this.txtuserpwd.UniqueID);
            //确认密码
            string confirmPwd = Utils.GetFormValue(this.txtConfirmPwd.UniqueID);
            //姓名
            string name = Utils.GetFormValue(this.txtName.UniqueID);
            //性别
            string sex = Utils.GetFormValue(this.seGenerlist.UniqueID);
            //级别
            string leaver = Utils.GetFormValue(this.ddlleaver.UniqueID);
            //导游类别
            string[] guidType = Utils.GetFormValues("guidType");
            //精通语种
            string language = Utils.GetFormValue(this.txtLanguage.UniqueID);
            //身份证号
            string CardCode = Utils.GetFormValue(this.txtCardCode.UniqueID);
            //导游证号
            string guidCode = Utils.GetFormValue(this.txtGuidCode.UniqueID);
            //领带证号
            string headTeamCode = Utils.GetFormValue(this.txtTeamHeadCode.UniqueID);
            //导游证挂靠单位
            string LinkedUnit = Utils.GetFormValue(this.dAnchoredComlist.UniqueID);
            //年审状态
            string AnnualReview = Utils.GetFormValue(this.ddlAnnualReview.UniqueID);
            //家庭电话
            string homephone = Utils.GetFormValue(this.txtHomeTel.UniqueID);
            //手机
            string phone = Utils.GetFormValue(this.txtMoblie.UniqueID);
            //qq
            string qq = Utils.GetFormValue(this.txtQQ.UniqueID);
            //E-mail
            string email = Utils.GetFormValue(this.txtEmail.UniqueID);
            //家庭地址
            string homeaddress = Utils.GetFormValue(this.txtHomeAddress.UniqueID);

            //性格特点
            string character = Utils.GetFormValue(this.txtcharacter.UniqueID);
            //擅长路线
            string ExperienceRoute = Utils.GetFormValue(this.txtExperienceRoute.UniqueID);
            //客户评价
            string CommentAssess = Utils.GetFormValue(this.txtCommentAssess.UniqueID);
            //特长说明
            string Specialty = Utils.GetFormValue(this.txtSpecialty.UniqueID);
            //备注说明
            string remark = Utils.GetFormValue(this.txtRemark.UniqueID);
            //部门编号
            int DeptId = Utils.GetInt(Utils.GetFormValue(BelongDepart.SelectIDClient));
            if (string.IsNullOrEmpty(username))
            {
                msg += "请输入用户名！<br/>";
            }
            if (string.IsNullOrEmpty(userpwd) && string.IsNullOrEmpty(this.hidUserPwd.UniqueID))
            {
                msg += "请输入密码!<br/>";
            }
            if (string.IsNullOrEmpty(name))
            {
                msg += "请输入姓名！<br/>";
            }
            if (leaver == "-1")
            {
                msg += "请选择导游级别！<br/>";
            }
            if (string.IsNullOrEmpty(phone))
            {
                msg += "请输入手机号码！<br/>";
            }
            if (string.IsNullOrEmpty(qq))
            {
                msg += "请输入qq号码!<br/>";
            }
            if (string.IsNullOrEmpty(email))
            {
                msg += "请输入email地址!<br/>";
            }
            if (DeptId.Equals(0))
            {
                msg += "请选择部门编号!<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                returnMsg = UtilsCommons.AjaxReturnJson("0", "" + msg + "");
                return returnMsg;
            }

            EyouSoft.Model.SourceStructure.MSourceGuide GuidModel = new EyouSoft.Model.SourceStructure.MSourceGuide();
            GuidModel.Address = homeaddress;
            GuidModel.AnchoredCom = (EyouSoft.Model.EnumType.SourceStructure.AnchoredCom)Enum.Parse(typeof(EyouSoft.Model.EnumType.SourceStructure.AnchoredCom), LinkedUnit);
            GuidModel.AnnualReview = AnnualReview == "0" ? false : true;
            GuidModel.Character = character;
            GuidModel.CompanyId = this.SiteUserInfo.CompanyId;
            GuidModel.CustomerReviews = CommentAssess;
            GuidModel.DeptId = this.SiteUserInfo.DeptId;
            GuidModel.Email = email;
            GuidModel.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)Enum.Parse(typeof(EyouSoft.Model.EnumType.GovStructure.Gender), sex);
            GuidModel.GuideNumber = guidCode;
            GuidModel.HomeTel = homephone;
            GuidModel.IDNumber = CardCode;
            GuidModel.IssueTime = System.DateTime.Now;
            GuidModel.Language = language;
            GuidModel.LeaderNumber = headTeamCode;
            GuidModel.Level = (EyouSoft.Model.EnumType.SourceStructure.GuideLevel)Enum.Parse(typeof(EyouSoft.Model.EnumType.SourceStructure.GuideLevel), leaver);
            GuidModel.MaintainId = this.SiteUserInfo.UserId;
            GuidModel.MaintainTime = System.DateTime.Now;
            GuidModel.Mobile = phone;
            GuidModel.Name = name;
            GuidModel.OperatorId = this.SiteUserInfo.UserId;
            GuidModel.QQ = qq;
            GuidModel.Remarks = remark;
           
            if (guidType.Length > 0)
            {
                GuidModel.SourceGuideCategoryList = new List<EyouSoft.Model.SourceStructure.MSourceGuideCategory>();
                for (int i = 0; i < guidType.Length; i++)
                {
                    EyouSoft.Model.SourceStructure.MSourceGuideCategory category = new EyouSoft.Model.SourceStructure.MSourceGuideCategory();
                    category.GuideCategory = (EyouSoft.Model.EnumType.SourceStructure.GuideCategory)Enum.Parse(typeof(EyouSoft.Model.EnumType.SourceStructure.GuideCategory), guidType[i]);
                    GuidModel.SourceGuideCategoryList.Add(category);
                }
            }

            GuidModel.Specialty = Specialty;
            GuidModel.TourExperience = ExperienceRoute;
            GuidModel.UserModel = new EyouSoft.Model.ComStructure.MComUser();
            GuidModel.UserModel.UserName = username;
            if (userpwd == "")
            {
                GuidModel.UserModel.Password = Utils.GetFormValue(this.hidUserPwd.UniqueID);
            }
            else
            {
                GuidModel.UserModel.Password = userpwd;
            }

            GuidModel.UserModel.CompanyId = this.SiteUserInfo.CompanyId;
            GuidModel.UserModel.ContactEmail = email;
            GuidModel.UserModel.ContactMobile = phone;
            GuidModel.UserModel.ContactName = name;
            GuidModel.UserModel.Operator = this.SiteUserInfo.Name;
            GuidModel.UserModel.DeptId = DeptId;
            #region 文件上传
            if (!string.IsNullOrEmpty(Utils.GetFormValue("hidFilePath")))
            {
                GuidModel.Photo = Utils.GetFormValue("hidFilePath");
            }
            else
            {
                string[] visaUpload = Utils.GetFormValues(this.SingleFileUpload1.ClientHideID);
                string[] oldVisaUpload = Utils.GetFormValues("hideFileInfo");
                if (visaUpload.Length > 0)
                {
                    for (int i = 0; i < visaUpload.Length; i++)
                    {
                        if (visaUpload[i].Split('|').Length > 1)
                        {
                            GuidModel.Photo = visaUpload[i].Split('|')[1];
                        }
                    }
                }
                else
                {
                    if (oldVisaUpload.Length > 0)
                    {
                        for (int i = 0; i < oldVisaUpload.Length; i++)
                        {
                            if (oldVisaUpload[i].Split('|').Length > 1)
                            {
                                GuidModel.Photo = oldVisaUpload[i].Split('|')[1];
                            }
                        }
                    }
                }
            }
            #endregion


            string id = Utils.GetQueryStringValue("guideID");
            if (!string.IsNullOrEmpty(id))
            {
                GuidModel.UserId = this.hidUserId.Value;
                GuidModel.UserModel.UserId = this.hidUserId.Value;
                GuidModel.GuideId = id;
                //人事档案编号
                GuidModel.GovFileId = Utils.GetFormValue(this.GovFileId.UniqueID);

                //返回值 -1:存在相同的用户名 -2:存在相同的身份证号 -3:存在相同的导游证号 小于0:错误 1:正确
                int result = new EyouSoft.BLL.SourceStructure.BSource().UpdateGuideModel(GuidModel);
                switch (result)
                {
                    case -1:
                        returnMsg = UtilsCommons.AjaxReturnJson("0", "用户名已存在!");
                        break;
                    case -2:
                        returnMsg = UtilsCommons.AjaxReturnJson("0", "身份证号已存在!");
                        break;
                    case -3:
                        returnMsg = UtilsCommons.AjaxReturnJson("0", "导游证号已存在!");
                        break;
                    case 1:
                        returnMsg = UtilsCommons.AjaxReturnJson("1", "修改成功!");
                        break;
                    default:
                        returnMsg = UtilsCommons.AjaxReturnJson("0", "修改失败!");
                        break;
                }
            }
            else //添加
            {
                //返回值 -1:存在相同的用户名 -2:存在相同的身份证号 -3:存在相同的导游证号
                int result = new EyouSoft.BLL.SourceStructure.BSource().AddGuideModel(GuidModel);
                switch (result)
                {
                    case -1:
                        returnMsg = UtilsCommons.AjaxReturnJson("0", "用户名已存在!");
                        break;
                    case -2:
                        returnMsg = UtilsCommons.AjaxReturnJson("0", "身份证号已存在!");
                        break;
                    case -3:
                        returnMsg = UtilsCommons.AjaxReturnJson("0", "导游证号已存在!");
                        break;
                    case 1:
                        returnMsg = UtilsCommons.AjaxReturnJson("1", "添加成功!");
                        break;
                    default:
                        returnMsg = UtilsCommons.AjaxReturnJson("0", "添加失败!");
                        break;
                }
            }

            return returnMsg;
        }
        #endregion

        #region 页面初始化
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void DataInit()
        {

            string guID = Utils.GetQueryStringValue("Guid");
            if (!string.IsNullOrEmpty(guID))
            {
                EyouSoft.Model.SourceStructure.MSourceGuide model = new EyouSoft.BLL.SourceStructure.BSource().GetGuideModel(guID); if (model != null)
                {
                    if (model.UserModel != null)
                    {
                        this.hidUserId.Value = model.UserModel.UserId;
                        this.txtUserNames.Text = model.UserModel.UserName;
                        //密码
                        this.txtuserpwd.Value = model.UserModel.Password;
                        this.hidUserPwd.Value = model.UserModel.Password;
                        //确认密码
                        this.txtConfirmPwd.Value = model.UserModel.Password;
                        this.hidConfirmPwd.Value = model.UserModel.Password;
                    }
                    //人事档案编号
                    this.GovFileId.Value = model.GovFileId;
                    this.txtName.Value = model.Name;
                    this.seGenerlist.Items.FindByValue(((int)model.Gender).ToString()).Selected = true;
                    this.ddlleaver.Items.FindByValue(((int)model.Level).ToString()).Selected = true;
                    //类型
                    this.txtLanguage.Value = model.Language;
                    this.txtCardCode.Value = model.IDNumber;
                    this.txtGuidCode.Value = model.GuideNumber;
                    this.txtTeamHeadCode.Value = model.LeaderNumber;
                    this.dAnchoredComlist.Items.FindByValue(((int)model.AnchoredCom > 0 ? (int)model.AnchoredCom : 3).ToString()).Selected = true;
                    this.ddlAnnualReview.Items.FindByValue(model.AnnualReview == true ? "1" : "0").Selected = true;
                    this.txtHomeTel.Value = model.HomeTel;
                    this.txtMoblie.Value = model.Mobile;
                    this.txtQQ.Value = model.QQ;
                    this.txtEmail.Value = model.Email;
                    this.txtHomeAddress.Value = model.Address;
                    this.txtcharacter.Value = model.Character;
                    this.txtExperienceRoute.Value = model.TourExperience;
                    this.txtCommentAssess.Value = model.CustomerReviews;
                    this.txtSpecialty.Value = model.Specialty;
                    this.txtRemark.Value = model.Remarks;
                    //照片                    
                    this.hidFilePath.Value = model.Photo;
                    PhonePath = model.Photo;
                    //导游类别
                    this.litguidType.Text = GetGuideCategory(model.SourceGuideCategoryList);

                    this.BelongDepart.SectionID = model.UserModel.DeptId.ToString();
                    this.BelongDepart.SectionName = model.UserModel.DeptName;
                }
                else
                {
                    this.litguidType.Text = GetGuideCategory(null);
                }
            }
            else
            {
                this.litguidType.Text = GetGuideCategory(null);
            }
        }

        #region 导游类别
        /// <summary>
        /// 导游类别
        /// </summary>
        /// <returns></returns>
        public string GetGuideCategory(IList<EyouSoft.Model.SourceStructure.MSourceGuideCategory> listType)
        {
            List<EnumObj> List = EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SourceStructure.GuideCategory));
            StringBuilder s = new StringBuilder();
            if (List != null && List.Count > 0)
            {
                string chk = "";
                for (int i = 0; i < List.Count; i++)
                {
                    if (listType != null && listType.Count > 0)
                    {
                        for (int j = 0; j < listType.Count; j++)
                        {
                            if (List[i].Value == ((int)listType[j].GuideCategory).ToString())
                            {
                                chk = "checked=\"checked\"";
                                break;
                            }
                        }
                    }
                    s.Append("<label><input type=\"checkbox\" " + chk + " name=\"guidType\" value=\"" + List[i].Value + "\" style=\"border: none;\" />" + List[i].Text + "</label>&nbsp;&nbsp;");
                    chk = "";
                }
            }
            return s.ToString();
        }
        #endregion 导游类别
        #endregion

        #region 弹窗页面设置
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = EyouSoft.Common.Page.PageType.boxyPage;
        }
        #endregion

        /// <summary>
        /// init privs
        /// </summary>
        void InitPrivs()
        {
            if (string.IsNullOrEmpty(Utils.GetQueryStringValue("Guid")))
            {
                phBtnSave.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游档案_新增);
            }
            else
            {
                phBtnSave.Visible = CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.导游中心_导游档案_修改);
            }
        }
    }
}
