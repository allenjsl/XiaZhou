using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.SourceStructure
{
    // 增加导游上团统计,导游排班 邵权江 2011-10-08
    public interface ISourceGuide
    {
        /// <summary>
        /// 判断身份证号是否存在
        /// </summary>
        /// <param name="IDNumber">身份证号</param>
        /// <param name="GuideId">供应商编号GuideId,新增GuideId=""</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        bool ExistsIDNumber(string IDNumber, string GuideId, string CompanyID);

        /// <summary>
        /// 添加导游信息
        /// </summary>
        /// <param name="model">供应商导游Model</param>
        /// <returns></returns>
        int AddGuideModel(Model.SourceStructure.MSourceGuide model);

        /// <summary>
        /// 修改导游Model
        /// </summary>
        /// <param name="model">供应商导游Model</param>
        /// <returns></returns>
        int UpdateGuideModel(Model.SourceStructure.MSourceGuide model);


        /// <summary>
        /// 删除导游Model
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        //int DeleteGuideModel(string GuideId);


        /// <summary>
        /// 批量删除导游Model
        /// </summary>
        /// <param name="SourceIdList">供应商编号List</param>
        /// <returns></returns>
        int DeleteGuideModel(params string[] guideIdList);

        /// <summary>
        /// 获得显示在列表页面上面的导游信息列表
        /// </summary>
        /// <param name="Searchmodel">导游列表查询Model</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<Model.SourceStructure.MGuideListModel> GetGuideShowModel(Model.SourceStructure.MGuideSearchModel Searchmodel, string CompanyId, int pageIndex, int pageSize, ref int recordCount);

        /// <summary>
        /// 获得导游Model
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        Model.SourceStructure.MSourceGuide GetGuideModel(string GuideId);

        /// <summary>
        /// 获得导游上团统计信息列表
        /// </summary>
        /// <param name="GuideName">导游姓名</param>
        /// <param name="TimeBegin">查询开始时间</param>
        /// <param name="TimeEnd">查询结束时间</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<Model.SourceStructure.MGuideListGroup> GetGuideListGroup(string GuideName, DateTime? TimeBegin, DateTime? TimeEnd, string CompanyId, int pageIndex, int pageSize, ref int recordCount);

        /// <summary>
        /// 获得导游带团详细信息列表
        /// </summary>
        /// <param name="GuideId">导游编号</param>
        /// <param name="TimeBegin">上团查询开始时间</param>
        /// <param name="TimeEnd">上团查询结束时间</param>
        /// <param name="LTimeBegin">出团查询开始时间</param>
        /// <param name="LTimeEnd">出团查询结束时间</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<Model.SourceStructure.MGuideTourList> GetGuideTourList(string GuideId, DateTime? TimeBegin, DateTime? TimeEnd, DateTime? LTimeBegin, DateTime? LTimeEnd, string CompanyId, int pageIndex, int pageSize, ref int recordCount);
        
        /// <summary>
        /// 获得导游排班信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="GuideName">导游姓名</param>
        /// <param name="year">查询年份</param>
        /// <param name="month">查询月份</param>
        /// <param name="NextTimeStart">查询下团时间开始</param>
        /// <param name="NextTimeEnd">查询下团时间结束</param>
        /// <param name="Location">下团地点</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        IList<Model.SourceStructure.MGuidePlanWork> GetGuidePlanWork(string CompanyId, string GuideName, int year, int month, DateTime? NextTimeStart, DateTime? NextTimeEnd, string Location, int pageIndex, int pageSize, ref int recordCount);
      
        /// <summary>
        /// 获得导游当日排班详细信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="GuideId">导游编号</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        IList<Model.SourceStructure.MGuidePlanWork> GetGuidePlanWorkInfo(string CompanyId, string GuideId, DateTime date);

    }
}
