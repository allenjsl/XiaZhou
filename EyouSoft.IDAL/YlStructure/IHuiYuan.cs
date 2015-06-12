// 游轮网站会员相关interface 汪奇志 2014-03-28
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.YlStructure
{
    /// <summary>
    /// 游轮网站会员相关interface
    /// </summary>
    public interface IHuiYuan
    {
        /// <summary>
        /// 是否存在相同的用户名
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        bool IsExistsUsername(string companyId, string username);
        /// <summary>
        /// 新增、修改会员信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int HuiYuan_CU(EyouSoft.Model.YlStructure.MHuiYuanInfo info);
        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MHuiYuanInfo GetHuiYuanInfo(string huiYuanId);
        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="username">用户名或者注册邮箱</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MHuiYuanInfo GetHuiYuanInfo(string companyId, string username,int fx);
        /// <summary>
        /// 获取会员信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHuiYuanInfo> GetHuiYuans(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanChaXunInfo chaXun);
        /// <summary>
        /// 写入常旅客信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertChangLvKe(EyouSoft.Model.YlStructure.MHuiYuanChangLvKeInfo info);
        /// <summary>
        /// 更新常旅客信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int UpdateChangLvKe(EyouSoft.Model.YlStructure.MHuiYuanChangLvKeInfo info);
        /// <summary>
        /// 删除常旅客信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="lvkeId">常旅客编号</param>
        /// <returns></returns>
        int DeleteChangLvKe(string companyId, string huiYuanId, string lvkeId);
        /// <summary>
        /// 获取常旅客信息
        /// </summary>
        /// <param name="lvkeId">常旅客编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MHuiYuanChangLvKeInfo GetChangLvKeInfo(string lvkeId);
        /// <summary>
        /// 获取常旅客集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHuiYuanChangLvKeInfo> GetChangLvKes(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanChangLvKeChaXunInfo chaXun);
        /// <summary>
        /// 写入地址信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertDiZhi(EyouSoft.Model.YlStructure.MHuiYuanDiZhiInfo info);
        /// <summary>
        /// 更新地址信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int UpdateDiZhi(EyouSoft.Model.YlStructure.MHuiYuanDiZhiInfo info);
        /// <summary>
        /// 删除地址信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="diZhiId">地址编号</param>
        /// <returns></returns>
        int DeleteDiZhi(string companyId, string huiYuanId, string diZhiId);
        /// <summary>
        /// 获取地址信息
        /// </summary>
        /// <param name="diZhiId">地址编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MHuiYuanDiZhiInfo GetDiZhiInfo(string diZhiId);
        /// <summary>
        /// 获取地址集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHuiYuanDiZhiInfo> GetDiZhis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanDiZhiChaXunInfo chaXun);
        /// <summary>
        /// 设置会员默认地址，返回1成功，其它失败
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="diZhiId">地址编号</param>
        /// <returns></returns>
        int SheZhiMoRenDiZhi(string huiYuanId, string diZhiId);
        /// <summary>
        /// 设置会员常旅客状态
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="lvKeId">常旅客编号</param>
        /// <param name="status">常旅客状态</param>
        /// <returns></returns>
        int SheZhiChangLvKeStatus(string huiYuanId, string lvKeId, EyouSoft.Model.EnumType.YlStructure.HuiYuanChangLvKeStatus status);

        /// <summary>
        /// 写入游轮攻略信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertGongLue(EyouSoft.Model.YlStructure.MWzGongLueInfo info);
        /// <summary>
        /// 更新游轮攻略信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int UpdateGongLue(EyouSoft.Model.YlStructure.MWzGongLueInfo info);
        /// <summary>
        /// 删除游轮攻略信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="gongLueId">攻略编号</param>
        /// <returns></returns>
        int DeleteGongLue(string companyId, string huiYuanId, string gongLueId);
        /// <summary>
        /// 获取游轮攻略信息
        /// </summary>
        /// <param name="gongLueId">攻略编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MWzGongLueInfo GetGongLueInfo(string gongLueId);
        /// <summary>
        /// 获取游轮攻略集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MWzGongLueInfo> GetGongLues(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzGongLueChaXunInfo chaXun);
        /// <summary>
        /// 审核游轮攻略
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="operatorId">审核人编号</param>
        /// <param name="gongLueId">攻略编号</param>
        /// <returns></returns>
        int ShenHeGongLue(string companyId, string operatorId, string gongLueId);

        /// <summary>
        /// 写入用户点评信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertDianPing(EyouSoft.Model.YlStructure.MWzDianPingInfo info);
        /// <summary>
        /// 更新用户点评信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int UpdateDianPing(EyouSoft.Model.YlStructure.MWzDianPingInfo info);
        /// <summary>
        /// 删除用户点评信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="dianPingId">点评编号</param>
        /// <returns></returns>
        int DeleteDianPing(string companyId, string huiYuanId, string dianPingId);
        /// <summary>
        /// 获取用户点评信息
        /// </summary>
        /// <param name="dianPingId">点评编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MWzDianPingInfo GetDianPingInfo(string dianPingId);
        /// <summary>
        /// 获取用户点评集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MWzDianPingInfo> GetDianPings(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzDianPingChaXunInfo chaXun);
        /// <summary>
        /// 审核用户点评
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="operatorId">审核人编号</param>
        /// <param name="dianPingId">点评编号</param>
        /// <returns></returns>
        int ShenHeDianPing(string companyId, string operatorId, string dianPingId);
        /// <summary>
        /// 写入咨询问答信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertWenDa(EyouSoft.Model.YlStructure.MWzWenDaInfo info);
        /// <summary>
        /// 更新咨询问答信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int UpdateWenDa(EyouSoft.Model.YlStructure.MWzWenDaInfo info);
        /// <summary>
        /// 删除咨询问答信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="wenDaId">问答编号</param>
        /// <returns></returns>
        int DeleteWenDa(string companyId, string huiYuanId, string wenDaId);
        /// <summary>
        /// 获取咨询问答信息
        /// </summary>
        /// <param name="wenDaId">问答编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MWzWenDaInfo GetWenDaInfo(string wenDaId);
        /// <summary>
        /// 获取咨询问答集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MWzWenDaInfo> GetWenDas(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzWenDaChaXunInfo chaXun);
        /// <summary>
        /// 回复咨询问答，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int HuiFuWenDa(EyouSoft.Model.YlStructure.MWzWenDaInfo info);
        /// <summary>
        /// 写入收藏夹信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertShouCangJia(EyouSoft.Model.YlStructure.MHuiYuanShouCangJiaInfo info);
        /// <summary>
        /// 删除收藏夹信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="shouCangId">收藏编号</param>
        /// <returns></returns>
        int DeleteShouCangJia(string companyId, string huiYuanId, string shouCangId);
        /// <summary>
        /// 获取收藏夹信息
        /// </summary>
        /// <param name="shouCangId">收藏编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MHuiYuanShouCangJiaInfo GetShouCangJiaInfo(string shouCangId);
        /// <summary>
        /// 获取收藏夹集合
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHuiYuanShouCangJiaInfo> GetShouCangJias(string huiYuanId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanShouCangJiaChaXunInfo chaXun);
        /// <summary>
        /// 设置会员密码，返回1成功，其它失败
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="yuanmd5">原密码MD5</param>
        /// <param name="md5">新密码MD5</param>
        /// <returns></returns>
        int SheZhiHuiYuanMiMa(string huiYuanId, string yuanmd5, string md5);
        /// <summary>
        /// 设置会员状态
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        int SheZhiHuiYuanStatus(string huiYuanId, EyouSoft.Model.EnumType.YlStructure.HuiYuanStatus status);
        /// <summary>
        /// 设置会员头像
        /// </summary>
        /// <param name="huiyuanid">会员编号</param>
        /// <param name="src">头像链接</param>
        /// <returns></returns>
        int SheZhiHuiYuanTouXiang(string huiyuanid, string src);
        /// <summary>
        /// 获取会员积分明细集合
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHuiYuanJiFenMxInfo> GetHuiYuanJiFenMxs(string huiYuanId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanJiFenMxChaXunInfo chaXun);
        /// <summary>
        /// 获取会员预存款明细集合
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHuiYuanYuCunKuanMxInfo> GetHuiYuanYuCunKuanMxs(string huiYuanId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanYuCunKuanMxChaXunInfo chaXun);
        /// <summary>
        /// 获取会员订单集合
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MHuiYuanDingDanInfo> GetHuiYuanDingDans(string huiYuanId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanDingDanChaXunInfo chaXun);
        /// <summary>
        /// 获取点评均分
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <returns></returns>
        Decimal GetDianPingJunFen(string hangQiId);
        /// <summary>
        /// 获取用户点评信息，按订单编号获取
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MWzDianPingInfo GetDianPingInfo1(string dingDanId);
        /// <summary>
        /// 设置会员账号
        /// </summary>
        /// <param name="huiYuanId">用户编号</param>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        int SetHuiYuanUsername(string huiYuanId, string username);
        /// <summary>
        /// 设置会员密码
        /// </summary>
        /// <param name="huiYuanId">用户编号</param>
        /// <param name="md5pwd">md5密码</param>
        /// <returns></returns>
        int SetHuiYuanMiMa(string huiYuanId, string md5pwd);
    }
}
