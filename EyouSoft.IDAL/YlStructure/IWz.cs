//游轮网站相关interface 汪奇志 2014-03-27
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.YlStructure
{
    /// <summary>
    /// 游轮网站相关interface
    /// </summary>
    public interface IWz
    {
        /// <summary>
        /// 设置网站KV信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int SheZhiKvInfo(EyouSoft.Model.YlStructure.MWzKvInfo info);
        /// <summary>
        /// 获取网站KV信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="k">key</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MWzKvInfo GetKvInfo(string companyId, EyouSoft.Model.EnumType.YlStructure.WzKvKey k);
        /// <summary>
        /// 获取网站域名信息
        /// </summary>
        /// <param name="yuMing">域名</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MWzYuMingInfo GetYuMingInfo(string yuMing);

        /// <summary>
        /// 写入友情链接信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertYouQingLianJie(EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo info);
        /// <summary>
        /// 更新友情链接信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int UpdateYouQingLianJie(EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo info);
        /// <summary>
        /// 删除友情链接信息，返回1成功，其它失败
        /// </summary>
        /// <param name="compamyId">公司编号</param>
        /// <param name="lianJieId">链接编号</param>
        /// <returns></returns>
        int DeleteYouQingLianJie(string compamyId, string lianJieId);
        /// <summary>
        /// 获取友情链接信息
        /// </summary>
        /// <param name="lianJieId">链接编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo GetYouQingLianJieInfo(string lianJieId);
        /// <summary>
        /// 获取友情链接信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo> GetYouQingLianJies(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzYouQingLianJieChaXunInfo chaXun);
        /// <summary>
        /// 获取友情链接信息集合
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="chaXun"></param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo> GetYouQingLianJies(string companyId, EyouSoft.Model.YlStructure.MWzYouQingLianJieChaXunInfo chaXun);
        /// <summary>
        /// 新增广告信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertGuangGao(EyouSoft.Model.YlStructure.MWzGuangGaoInfo info);
        /// <summary>
        /// 修改广告信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int UpdateGuangGao(EyouSoft.Model.YlStructure.MWzGuangGaoInfo info);
        /// <summary>
        /// 删除广告信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="guangGaoId">广告编号</param>
        /// <returns></returns>
        int DeleteGuangGao(string companyId,string guangGaoId);
        /// <summary>
        /// 获取广告信息
        /// </summary>
        /// <param name="guangGaoId">广告编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MWzGuangGaoInfo GetGuangGaoInfo(string guangGaoId);
        /// <summary>
        /// 获取广告信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MWzGuangGaoInfo> GetGuangGaos(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzGuangGaoChaXunInfo chaXun);

        /// <summary>
        /// 新增资讯信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertZiXun(EyouSoft.Model.YlStructure.MWzZiXunInfo info);
        /// <summary>
        /// 修改资讯信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int UpdateZiXun(EyouSoft.Model.YlStructure.MWzZiXunInfo info);
        /// <summary>
        /// 删除资讯信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="ziXunId">资讯编号</param>
        /// <returns></returns>
        int DeleteZiXun(string companyId, string ziXunId);
        /// <summary>
        /// 获取资讯信息
        /// </summary>
        /// <param name="ziXunId">资讯编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MWzZiXunInfo GetZiXunInfo(string ziXunId);
        /// <summary>
        /// 获取资讯信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MWzZiXunInfo> GetZiXuns(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzZiXunChaXunInfo chaXun);
        /// <summary>
        /// 新增公司荣誉信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertGongSiRongYu(EyouSoft.Model.YlStructure.MWzGongSiRongYuInfo info);
        /// <summary>
        /// 修改公司荣誉信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int UpdateGongSiRongYu(EyouSoft.Model.YlStructure.MWzGongSiRongYuInfo info);
        /// <summary>
        /// 删除公司荣誉信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="rongYuId">荣誉编号</param>
        /// <returns></returns>
        int DeleteGongSiRongYu(string companyId, string rongYuId);
        /// <summary>
        /// 获取公司荣誉信息
        /// </summary>
        /// <param name="rongYuId">荣誉编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MWzGongSiRongYuInfo GetGongSiRongYuInfo(string rongYuId);
        /// <summary>
        /// 获取公司荣誉信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MWzGongSiRongYuInfo> GetGongSiRongYus(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzGongSiRongYuChaXunInfo chaXun);
        /// <summary>
        /// 新增员工风采信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertYuanGongFengCai(EyouSoft.Model.YlStructure.MWzYuanGongFengCaiInfo info);
        /// <summary>
        /// 修改员工风采信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int UpdateYuanGongFengCai(EyouSoft.Model.YlStructure.MWzYuanGongFengCaiInfo info);
        /// <summary>
        /// 删除员工风采信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="fengCaiId">风采编号</param>
        /// <returns></returns>
        int DeleteYuanGongFengCai(string companyId, string fengCaiId);
        /// <summary>
        /// 获取员工风采信息
        /// </summary>
        /// <param name="fengCaiId">风采编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MWzYuanGongFengCaiInfo GetYuanGongFengCaiInfo(string fengCaiId);
        /// <summary>
        /// 获取员工风采信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MWzYuanGongFengCaiInfo> GetYuanGongFengCais(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzYuanGongFengCaiChaXunInfo chaXun);
        /// <summary>
        /// 新增招聘岗位信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertZhaoPinGangWei(EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiInfo info);
        /// <summary>
        /// 修改招聘岗位信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int UpdateZhaoPinGangWei(EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiInfo info);
        /// <summary>
        /// 删除招聘岗位信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gangWeiId">岗位编号</param>
        /// <returns></returns>
        int DeleteZhaoPinGangWei(string companyId, string gangWeiId);
        /// <summary>
        /// 获取招聘岗位信息
        /// </summary>
        /// <param name="gangWeiId">岗位编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiInfo GetZhaoPinGangWeiInfo(string gangWeiId);
        /// <summary>
        /// 获取招聘岗位信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiInfo> GetZhaoPinGangWeis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiChaXunInfo chaXun);
        /// <summary>
        /// 新增会议案例信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertHuiYiAnLi(EyouSoft.Model.YlStructure.MWzHuiYiAnLiInfo info);
        /// <summary>
        /// 修改会议案例信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int UpdateHuiYiAnLi(EyouSoft.Model.YlStructure.MWzHuiYiAnLiInfo info);
        /// <summary>
        /// 删除会议案例信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="anLiId">案例编号</param>
        /// <returns></returns>
        int DeleteHuiYiAnLi(string companyId, string anLiId);
        /// <summary>
        /// 获取会议案例信息
        /// </summary>
        /// <param name="anLiId">案例编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MWzHuiYiAnLiInfo GetHuiYiAnLiInfo(string anLiId);
        /// <summary>
        /// 获取会议案例信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MWzHuiYiAnLiInfo> GetHuiYiAnLis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzHuiYiAnLiChaXunInfo chaXun);
        /// <summary>
        /// 写入会议申请信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertHuiYiShenQing(EyouSoft.Model.YlStructure.MWzHuiYiShenQingInfo info);
        /// <summary>
        /// 删除会议申请信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="shenQingId">申请编号</param>
        /// <returns></returns>
        int DeleteHuiYiShenQing(string companyId, string shenQingId);
        /// <summary>
        /// 获取会议申请信息
        /// </summary>
        /// <param name="shenQingId">申请编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MWzHuiYiShenQingInfo GetHuiYiShenQingInfo(string shenQingId);
        /// <summary>
        /// 获取会议申请集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MWzHuiYiShenQingInfo> GetHuiYiShenQings(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzHuiYiShenQingChaXunInfo chaXun);
        /// <summary>
        /// 会议申请处理
        /// </summary>
        /// <param name="shenQingId">申请编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="beiZhu">备注</param>
        /// <returns></returns>
        int HuiYiShenQingChuLi(string shenQingId, string operatorId, string beiZhu);
        /// <summary>
        /// 网站意见反馈新增
        /// </summary>
        /// <param name="m">意见反馈实体</param>
        /// <returns>1：成功 0：失败</returns>
        int AddWZYiJianFanKui(Model.YlStructure.MWzYiJianFanKuiInfo m);
        /// <summary>
        /// 删除网站意见反馈
        /// </summary>
        /// <param name="yijianId"></param>
        /// <returns>0：失败 1：成功</returns>
        int DelWZYiJianFanKui(string yijianId);
        /// <summary>
        /// 获取网站意见反馈信息
        /// </summary>
        /// <param name="yijianId">意见编号</param>
        /// <returns></returns>
        Model.YlStructure.MWzYiJianFanKuiInfo GetWZYiJianFanKui(string yijianId);
        /// <summary>
        /// 获取网站意见反馈信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="chaXun"></param>
        /// <returns></returns>
        IList<Model.YlStructure.MWzYiJianFanKuiInfo> GetWZYiJianFanKui(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzYiJianFanKuiChaXun chaXun);
    }
}
