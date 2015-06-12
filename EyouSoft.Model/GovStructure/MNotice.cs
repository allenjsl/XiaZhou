using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.GovStructure
{
    #region 通知公告实体
    /// <summary>
    /// 通知公告实体
    /// 2011-09-02 邵权江 创建
    /// </summary>
    public class MGovNotice
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGovNotice() { }
        /// <summary>
        /// 主键ID
        /// </summary>
        public string NoticeId{ get; set; }
        /// <summary>
        /// 信息类型 0：通知公告 1：交流中心
        /// </summary>		
        public string NoticeType { get; set; }        
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId{ get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title{ get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content{ get; set; }
        /// <summary>
        /// 是否提醒(1是/0否)
        /// </summary>
        public bool IsRemind { get; set; }
        /// <summary>
        /// 是否发送短信(1是/0否)
        /// </summary>
        public bool IsMsg { get; set; }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string MsgContent { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        public int Views{ get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public int DepartId { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public string OperatorId{ get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime IssueTime{ get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator{ get; set; }
        /// <summary>
        /// 接收人员实体集合
        /// </summary>
        public IList<Model.GovStructure.MGovNoticeReceiver> MGovNoticeReceiverList { get; set; }
        /// <summary>
        /// 附件实体
        /// </summary>
        public IList<Model.ComStructure.MComAttach> ComAttachList { get; set; }
    }
    #endregion

    #region 接收人员实体
    /// <summary>
    /// 接收人员实体
    /// 2011-09-05 邵权江 创建
    /// </summary>
    public class MGovNoticeReceiver
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGovNoticeReceiver() { }
        /// <summary>
        /// 公告ID
        /// </summary>
        public string NoticeId { get; set; }
        /// <summary>
        /// 接收类型
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.ItemType ItemType { get; set; }
        /// <summary>
        /// 接收编号
        /// </summary>
        public string ItemId { get; set; }
    }
    #endregion

    #region 通知公告浏览明细
    /// <summary>
    /// 通知公告浏览明细
    /// 2011-09-05 邵权江 创建
    /// </summary>
    public class MGovNoticeBrowse
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGovNoticeBrowse() { }
        /// <summary>
        /// 通知公告编号
        /// </summary>
        public string NoticeId { get; set; }
        /// <summary>
        /// 浏览人编号
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// 浏览时间
        /// </summary>
        public DateTime IssueTime { get; set; }
        /// <summary>
        /// 浏览人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public string DepartName { get; set; }
    }
    #endregion

    #region 学习交流回复实体
     /// <summary>
    /// 学习交流回复
    /// </summary>
    [Serializable]
    public class MGovNoticeReply
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public MGovNoticeReply() { }
        /// <summary>
        /// 编号
        /// </summary>
        public string ItemId { get; set; }
        /// <summary>
        /// 公告ID
        /// </summary>
        public string NoticeId
        {
            get;
            set;
        }
        /// <summary>
        /// 回复人
        /// </summary>
        public string OperatorId
        {
            get;
            set;
        }
        /// <summary>
        /// 回复人姓名
        /// </summary>
        public string OperatorName
        {
            get;
            set;
        }
        /// <summary>
        /// 回复信息
        /// </summary>
        public string ReplyInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime IssueTime
        {
            get;
            set;
        }
    }
    #endregion
}
