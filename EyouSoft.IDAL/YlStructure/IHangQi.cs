//游轮航期interface 汪奇志 2014-03-26
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.YlStructure
{
    /// <summary>
    /// 游轮航期interface
    /// </summary>
    public interface IHangQi
    {
        /// <summary>
        /// 航期新增、修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int HangQi_CU(EyouSoft.Model.YlStructure.MHangQiInfo info);
        /// <summary>
        /// 获取航期信息
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MHangQiInfo GetHangQiInfo(string hangQiId);
        /// <summary>
        /// 航期删除，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="hangQiId">航期编号</param>
        /// <returns></returns>
        int HangQi_D(string companyId, string operatorId, string hangQiId);
        /// <summary>
        /// 获取航期信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHangQiInfo> GetHangQis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHangQiChaXunInfo chaXun);
        /// <summary>
        /// 设置航期价格
        /// </summary>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="riQiId">出港日期编号</param>
        /// <param name="items">价格信息</param>
        /// <returns></returns>
        int SheZhiHangQiJiaGe(string operatorId, string hangQiId, string riQiId, IList<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo> items);
        /// <summary>
        /// 设置航期出港日期
        /// </summary>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="items">出港日期信息</param>
        /// <returns></returns>
        int SheZhiHangQiRiQi(string operatorId, string hangQiId, IList<EyouSoft.Model.YlStructure.MHangQiRiQiInfo> items);
        /// <summary>
        /// 获取航期价格信息集合
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="riQiId">出港日期编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo> GetHangQiJiaGes(string hangQiId, string riQiId);
        /// <summary>
        /// 批量添加时获取航期价格信息集合
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="hangxianId">航线编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo> GetHangQiJiaGes(string hangQiId,int hangxianId);
        /// <summary>
        /// 获取航期出港日期信息集合
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="d1">开始日期</param>
        /// <param name="d2">截止日期</param>
        /// <param name="isYouXiao">是否有效出港日期</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHangQiRiQiInfo> GetHangQiRiQis(string hangQiId, DateTime? d1, DateTime? d2, bool? isYouXiao);
        /// <summary>
        /// 设置航期优惠规则
        /// </summary>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="fangShi">多个共享优惠时的共享方式</param>
        /// <param name="items">规则集合</param>
        /// <returns></returns>
        int SheZhiHangQiYouHuiGuiZe(string operatorId, string hangQiId, EyouSoft.Model.EnumType.YlStructure.YouHuiGongXiangFangShi fangShi, IList<EyouSoft.Model.YlStructure.MHangQiYouHuiGuiZeInfo> items);
        /// <summary>
        /// 获取航期优惠规则信息集合
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="fangShi">多个共享优惠时的共享方式</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHangQiYouHuiGuiZeInfo> GetHangQiyouHuiGuiZes(string hangQiId, out EyouSoft.Model.EnumType.YlStructure.YouHuiGongXiangFangShi fangShi);
        /// <summary>
        /// 设置航期标签
        /// </summary>
        /// <param name="info">实体</param>
        /// <param name="fangShi">0:设置 1:取消</param>
        /// <returns></returns>
        int SheZhiHangQiBiaoQian(EyouSoft.Model.YlStructure.MHangQiBiaoQianInfo info, int fangShi);
        /// <summary>
        /// 写入航期浏览记录信息
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int HangQiLiuLanJiLu_C(EyouSoft.Model.YlStructure.MHangQiLiuLanJiLuInfo info);
        
        /// <summary>
        /// 团购新增、修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int TuanGou_CU(EyouSoft.Model.YlStructure.MTuanGouInfo info);
        /// <summary>
        /// 删除团购信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="tuanGouId">团购编号</param>
        /// <returns></returns>
        int TuanGou_D(string companyId, string tuanGouId);
        /// <summary>
        /// 获取团购信息
        /// </summary>
        /// <param name="tuanGouId">团购编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MTuanGouInfo GetTuanGouInfo(string tuanGouId);
        /// <summary>
        /// 获取团购集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MTuanGouInfo> GetTuanGous(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MTuanGouChaXunInfo chaXun);
        /// <summary>
        /// 获取关联航期集合
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MGuanLianHangQiInfo> GetGuanLianHangQis(string hangQiId);
        /// <summary>
        /// 设置出港日期预控人数
        /// </summary>
        /// <param name="riQiId">出港日期编号</param>
        /// <param name="renShu">人数</param>
        /// <param name="operatorId">操作员编号</param>
        /// <returns></returns>
        int ShenZhiHangQiRiQiRenShu(string riQiId, int renShu, string operatorId);
        /// <summary>
        /// 获取出港日期信息
        /// </summary>
        /// <param name="riQiId">日期编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MHangQiRiQiInfo GetRiQiInfo(string riQiId);
        /// <summary>
        /// 取消航期日期，返回1成功，其它失败
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="riQiId">日期编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <returns></returns>
        int QuXiaoHangQiRiQi(string hangQiId, string riQiId, string operatorId);
    }
}
