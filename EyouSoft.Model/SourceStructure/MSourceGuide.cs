using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SourceStructure
{
    /// <summary>
	/// 实体类tbl_SourceGuide 。(属性说明自动提取数据库字段的描述信息)
    /// 修改：邵权江  2012-03-08
	/// </summary>
    [Serializable]
    public class MSourceGuide
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceGuide() { }
        #region Model
        private string _guideid;
        private string _name;
        
        private string _idnumber;
        private string _guidenumber;
        private string _leadernumber;
        private bool _annualreview;
        private string _language;
        private Model.EnumType.SourceStructure.GuideLevel _level;
        private Model.EnumType.SourceStructure.AnchoredCom _anchoredcom;
        private IList<Model.SourceStructure.MSourceGuideCategory> _sourceGuideCategoryList;
        private string _mobile;
        private string _qq;
        private string _email;
        private string _hometel;
        private string _msn;
        private string _address;
        private string _photo;
        private string _character;
        private string _tourexperience;
        private string _customerreviews;
        private string _specialty;
        private string _remarks;
        private string _operatorid;
        private DateTime _issuetime;
        private bool _isdelete;
        private string _maintainid;
        private DateTime _maintaintime;
        /// <summary>
        /// 导游编号
        /// </summary>
        public string GuideId
        {
            set { _guideid = value; }
            get { return _guideid; }
        }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public Model.EnumType.GovStructure.Gender? Gender
        {
            set;
            get;
        }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNumber
        {
            set { _idnumber = value; }
            get { return _idnumber; }
        }
        /// <summary>
        /// 导游证号
        /// </summary>
        public string GuideNumber
        {
            set { _guidenumber = value; }
            get { return _guidenumber; }
        }
        /// <summary>
        /// 领队证号
        /// </summary>
        public string LeaderNumber
        {
            set { _leadernumber = value; }
            get { return _leadernumber; }
        }
        /// <summary>
        /// 导游证挂靠单位
        /// </summary>
        public Model.EnumType.SourceStructure.AnchoredCom AnchoredCom
        {
            set { _anchoredcom = value; }
            get { return _anchoredcom; }
        }
        /// <summary>
        /// 年审状态(0未审，,1:已审)
        /// </summary>
        public bool AnnualReview
        {
            set { _annualreview = value; }
            get { return _annualreview; }
        }
        /// <summary>
        /// 精通语种
        /// </summary>
        public string Language
        {
            set { _language = value; }
            get { return _language; }
        }
        /// <summary>
        /// 导游级别
        /// </summary>
        public Model.EnumType.SourceStructure.GuideLevel Level
        {
            set { _level = value; }
            get { return _level; }
        }
        /// <summary>
        /// 导游类别实体集合
        /// </summary>
        public IList<Model.SourceStructure.MSourceGuideCategory> SourceGuideCategoryList 
        {
            set { _sourceGuideCategoryList = value; }
            get { return _sourceGuideCategoryList; }
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string Msn
        {
            set { _msn = value; }
            get { return _msn; }
        }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ
        {
            set { _qq = value; }
            get { return _qq; }
        }
        /// <summary>
        /// E-mail
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 家庭电话
        /// </summary>
        public string HomeTel
        {
            set { _hometel = value; }
            get { return _hometel; }
        }
        /// <summary>
        /// 家庭地址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 照片
        /// </summary>
        public string Photo
        {
            set { _photo = value; }
            get { return _photo; }
        }
        /// <summary>
        /// 性格特点
        /// </summary>
        public string Character
        {
            set { _character = value; }
            get { return _character; }
        }
        /// <summary>
        /// 擅长线路
        /// </summary>
        public string TourExperience
        {
            set { _tourexperience = value; }
            get { return _tourexperience; }
        }
        /// <summary>
        /// 客户评价
        /// </summary>
        public string CustomerReviews
        {
            set { _customerreviews = value; }
            get { return _customerreviews; }
        }
        /// <summary>
        /// 特长说明
        /// </summary>
        public string Specialty
        {
            set { _specialty = value; }
            get { return _specialty; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator
        {
            set { _operatorid = value; }
            get { return _operatorid; }
        }
        /// <summary>
        /// 操作人编号
        /// </summary>
        public string OperatorId
        {
            set { _operatorid = value; }
            get { return _operatorid; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime
        {
            set { _issuetime = value; }
            get { return _issuetime; }
        }
        /// <summary>
        /// 标记删除（1删除，0正常）
        /// </summary>
        public bool IsDelete
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId
        {
            get;
            set;
        }
        /// <summary>
        /// 人事档案编号
        /// </summary>
        public string GovFileId
        {
            get;
            set;
        }
        /// <summary>
        /// 用户
        /// </summary>
        public Model.ComStructure.MComUser UserModel
        {
            get;
            set;
        }
        /// <summary>
        /// 操作人部门id
        /// </summary>
        public int DeptId
        {
            get;
            set;
        }
        /// <summary>
        /// 最后维护人编号
        /// </summary>
        public string MaintainId
        {
            set { _maintainid = value; }
            get { return _maintainid; }
        }
        /// <summary>
        /// 最后维护时间
        /// </summary>
        public DateTime MaintainTime
        {
            set { _maintaintime = value; }
            get { return _maintaintime; }
        }
        #endregion Model
    }

    #region 导游类别实体
    /// <summary>
    /// 导游类别实体
    /// 2012-03-08 邵权江 创建
    /// </summary>
    public class MSourceGuideCategory
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MSourceGuideCategory() { }
        /// <summary>
        /// 导游编号
        /// </summary>
        public string GuideId { get; set; }
        /// <summary>
        /// 导游类别
        /// </summary>
        public Model.EnumType.SourceStructure.GuideCategory GuideCategory {get;set;}
    }
    #endregion

    #region 导游列表显示Model
    /// <summary>
    /// 导游列表显示Model
    /// </summary>
    [Serializable]
    public class MGuideListModel : MSourceGuide
    {
        /// <summary>
        /// 带团次数
        /// </summary>
        public int Counts
        {
            get;
            set;
        }
        /// <summary>
        /// 带团天数
        /// </summary>
        public int Days
        {
            get;
            set;
        }
    }
    #endregion

    #region 导游列表查询Model
    /// <summary>
    /// 导游列表查询Model
    /// </summary>
    [Serializable]
    public class MGuideSearchModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            set;
            get;
        }
        /// <summary>
        /// 性别
        /// </summary>
        public Model.EnumType.GovStructure.Gender? Gender
        {
            set;
            get;
        }
        /// <summary>
        /// 精通语种
        /// </summary>
        public string Language
        {
            set;
            get;
        }
        /// <summary>
        /// 导游级别
        /// </summary>
        public Model.EnumType.SourceStructure.GuideLevel? Level
        {
            set;
            get;
        }
        /// <summary>
        /// 导游类别
        /// </summary>
        public Model.EnumType.SourceStructure.GuideCategory? GuideCategory { get; set; }
    }
    #endregion

    #region 带团次数查看列表实体
    /// <summary>
    /// 带团次数查看实体
    /// 2012-03-08 邵权江 创建
    /// </summary>
    public class MGuideTourList
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGuideTourList() { }

        /// <summary>
        /// 团队编号
        /// </summary>
        public string TourId { get; set; }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName { get; set; }
        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime? LDate { get; set; }
        /// <summary>
        /// 回团时间
        /// </summary>
        public DateTime? RDate { get; set; }
        /// <summary>
        /// 天数
        /// </summary>
        public int DayCount { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int RealPeopleNumber { get; set; }
        
    }
    #endregion

    #region 导游上团统计Model
    /// <summary>
    /// 导游上团统计 邵权江 2011-10-08
    /// </summary>
    [Serializable]
    public class MGuideListGroup
    {
        /// <summary>
        /// 导游编号
        /// </summary>
        public string GuideId
        {
            set;
            get;
        }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            set;
            get;
        }

        /// <summary>
        /// 团队数
        /// </summary>
        public int GroupCount
        {
            get;
            set;
        }

        /// <summary>
        /// 团天数
        /// </summary>
        public int DayCount
        {
            get;
            set;
        }

        /// <summary>
        /// 评分
        /// </summary>
        public decimal Score
        {
            get;
            set;
        }

        /// <summary>
        /// 投诉次数
        /// </summary>
        public int ComplaintCount
        {
            get;
            set;
        }

    }
    #endregion

    #region 导游排班显示Model
    /// <summary>
    /// 导游排班显示 邵权江 2011-10-08
    /// </summary>
    [Serializable]
    public class MGuidePlanWork
    {
        /// <summary>
        /// 计调编号
        /// </summary>
        public string PlanId
        {
            set;
            get;
        }
        /// <summary>
        /// 导游编号
        /// </summary>
        public string GuideId
        {
            set;
            get;
        }
        /// <summary>
        /// 导游姓名
        /// </summary>
        public string Name
        {
            set;
            get;
        }
        /// <summary>
        /// 员工状态(在职/离职/兼职/挂靠)
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.StaffStatus StaffStatus { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }
        /// <summary>
        /// 团编号
        /// </summary>
        public string TourId
        {
            get;
            set;
        }
        /// <summary>
        /// 团号
        /// </summary>
        public string TourCode
        {
            get;
            set;
        }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName
        {
            get;
            set;
        }
        /// <summary>
        /// 出团时间
        /// </summary>
        public DateTime LDate
        {
            set;
            get;
        }
        /// <summary>
        /// 回团时间
        /// </summary>
        public DateTime RDate
        {
            set;
            get;
        }
        /// <summary>
        /// 出团天数
        /// </summary>
        public int TourDays
        {
            set;
            get;
        }
        /// <summary>
        /// 上团时间
        /// </summary>
        public DateTime OnTime
        {
            set;
            get;
        }
        /// <summary>
        /// 下团时间
        /// </summary>
        public DateTime NextTime
        {
            set;
            get;
        }
        /// <summary>
        /// 结算费用
        /// </summary>
        public decimal PlanCost
        {
            set;
            get;
        }
        /// <summary>
        /// 带团天数
        /// </summary>
        public int GuideDays
        {
            set;
            get;
        }
        /// <summary>
        /// 上团地点
        /// </summary>
        public string OnLocation
        {
            get;
            set;
        }
        /// <summary>
        /// 下团地点
        /// </summary>
        public string NextLocation
        {
            get;
            set;
        }
        /// <summary>
        /// 安排时间
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
        /// <summary>
        /// 计调状态
        /// </summary>
        public EyouSoft.Model.EnumType.PlanStructure.PlanState PlanState { get; set; }
        /// <summary>
        /// 导游安排类型业务实体集合
        /// </summary>
        public IList<MGuidePlanWorkType> TypeList { get; set; }
    }
    #endregion

    #region 导游安排类型业务实体集合
    /// <summary>
    /// 导游安排类型业务实体集合
    /// </summary>
    public class MGuidePlanWorkType
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? OnTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? NextTime { get; set; }
        /// <summary>
        /// 类型：已安排，假期，停职
        /// </summary>
        public string Type { get; set; }
    }
    #endregion
}
