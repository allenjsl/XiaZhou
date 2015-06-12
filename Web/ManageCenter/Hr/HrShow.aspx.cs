using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using EyouSoft.Model.GovStructure;
using System.Text;
namespace Web.ManageCenter.Hr
{
    /// <summary>
    /// 行政中心-人事档案-查看
    /// </summary>
    /// 创建人：徐从栎
    /// 创建时间：2011-9-20
    public partial class HrShow : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PowerControl();
                string id = EyouSoft.Common.Utils.GetQueryStringValue("id");
                DataInit(id);
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit(string id)
        {
            EyouSoft.BLL.GovStructure.BArchives BLL = new EyouSoft.BLL.GovStructure.BArchives();
            EyouSoft.Model.GovStructure.MGovFile Model = BLL.GetArchivesModel(id);
            if (null == Model) return;
            /*1.基本信息*/
            this.lbName.Text = Model.Name;//头部姓名
            this.lbName_.Text = Model.Name;//姓名 
            this.lbNum.Text = Model.FileNumber;//档案编号
            this.lbSex.Text = Convert.ToString(Model.Sex);//性别
            if (!string.IsNullOrEmpty(Model.StaffPhoto))
            {
                this.lbImg.Text = String.Format("<img src='{0}' width='100' heigth='150'/>", Model.StaffPhoto);//照片
            }
            else
            {
                this.lbImg.Text = "&nbsp;&nbsp;&nbsp;&nbsp;暂无照片";
            }
            this.lbIDCard.Text = Model.IDNumber;//身份证号
            this.lbBirth.Text = string.Format("{0:yyyy-MM-dd}", Model.BirthDate);//出生年月
            //部门
            this.lbSection.Text = Model.DepartName;
            //职务信息
            IList<MGovFilePosition> postionLst = Model.GovFilePositionList;
            if (null != postionLst && postionLst.Count > 0)
            {
                StringBuilder strPositionName = new StringBuilder();
                StringBuilder strPositionId = new StringBuilder();
                foreach (MGovFilePosition m in postionLst)
                {
                    strPositionName.Append(m.Title + ",");
                }
                this.lbDuty.Text = strPositionName.Length > 0 ? strPositionName.ToString().Substring(0, strPositionName.Length - 1) : "";//职务名
            }
            this.lbType.Text = Convert.ToString(Model.StaffType);//类型
            this.lbState.Text = Convert.ToString(Model.StaffStatus);//员工状态
            this.lbJoinTime.Text = string.Format("{0:yyyy-MM-dd}", Model.EntryTime);//入职时间
            this.lbWorkAge.Text = Convert.ToString(Model.LengthService);//工龄
            this.lbNation.Text = Model.Nation;//民族
            this.lbNativePlace.Text = Model.Birthplace;//籍贯
            this.lbFace.Text = Model.PoliticalFace;//政治面貌
            this.lbWedState.Text = Model.IsMarriage ? "已婚" : "未婚";//婚姻状态
            this.lbTel.Text = Model.Contact;//电话
            this.lbMobile.Text = Model.Mobile;//手机
            if (Model.StaffStatus == EyouSoft.Model.EnumType.GovStructure.StaffStatus.离职)
            {
                EyouSoft.BLL.GovStructure.BGovFilePersonnel leaveBLL = new EyouSoft.BLL.GovStructure.BGovFilePersonnel();
                EyouSoft.Model.GovStructure.MGovFilePersonnel leaveModel = leaveBLL.GetGovFilePersonnelModelByFileId(id);
                if (null != leaveModel)
                {
                    this.lbLeave.Text = String.Format("离职时间：{0:yyyy-MM-dd}", leaveModel.DepartureTime);
                }
            }
            this.lbQQ.Text = Model.qq;//qq
            this.lbMsn.Text = Model.Msn;//msn
            this.lbEmail.Text = Model.Email;//email
            this.lbPlace.Text = Model.Address;//住 址
            this.lbRemark.Text = Model.Remarks;//备注
            /*2.家庭关系*/
            IList<MGovFilehome> homeLst = Model.GovFilehomeList;
            if (null != homeLst && homeLst.Count > 0)
            {
                this.RepRation.DataSource = homeLst;
                this.RepRation.DataBind();
            }
            else
            {
                this.RepRation.Controls.Add(new Label() { Text = "<tr><td colspan='5' align='center'>暂无相关数据！</td></tr>" });
            }
            /*3.学历信息*/
            IList<MGovFileEducation> schoolLst = Model.GovFileEducationList;
            if (null != schoolLst && schoolLst.Count > 0)
            {
                this.RepSchool.DataSource = schoolLst;
                this.RepSchool.DataBind();
            }
            else
            {
                this.RepSchool.Controls.Add(new Label() { Text = "<tr><td colspan='7' align='center'>暂无相关数据！</td></tr>" });
            }
            /*4.履历信息*/
            IList<MGovFileCurriculum> workLst = Model.GovFileCurriculumList;
            if (null != workLst && workLst.Count > 0)
            {
                this.RepWork.DataSource = workLst;
                this.RepWork.DataBind();
            }
            else
            {
                this.RepWork.Controls.Add(new Label() { Text = "<tr><td colspan='6' align='center'>暂无相关数据！</td></tr>" });
            }
            /*5.劳动合同*/
            IList<MGovFileContract> packLst = Model.GovFileContractList;
            if (null != packLst && packLst.Count > 0)
            {
                this.rpt_Contact.DataSource = packLst;
                this.rpt_Contact.DataBind();
            }
            else
            {
                this.rpt_Contact.Controls.Add(new Label() { Text = "<tr><td colspan='6' align='center'>暂无相关数据！</td></tr>" });
            }
        }
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_人事档案_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_人事档案_栏目, false);
            }
        }
    }
}