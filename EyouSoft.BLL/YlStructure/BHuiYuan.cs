//游轮网站会员相关 汪奇志 2014-03-29
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.YlStructure
{
    /// <summary>
    /// 游轮网站会员相关
    /// </summary>
    public class BHuiYuan
    {
        readonly EyouSoft.IDAL.YlStructure.IHuiYuan dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.YlStructure.IHuiYuan>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BHuiYuan() { }
        #endregion

        #region private members

        #endregion

        #region public members
        /// <summary>
        /// 是否存在相同的用户名
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public bool IsExistsUsername(string companyId, string username)
        {
            if (string.IsNullOrEmpty(companyId) 
                || string.IsNullOrEmpty(username)) return true;

            return dal.IsExistsUsername(companyId, username);
        }

        /// <summary>
        /// 新增会员信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertHuiYuan(EyouSoft.Model.YlStructure.MHuiYuanInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.Username) 
                || string.IsNullOrEmpty(info.MD5Password)) return 0;

            info.HuiYuanId = Guid.NewGuid().ToString();
            info.KeYongJiFen = 0;
            info.PingTaiLeiXing = EyouSoft.Model.EnumType.YlStructure.HuiYuanPingTaiLeiXing.游轮网站;
            info.Status = EyouSoft.Model.EnumType.YlStructure.HuiYuanStatus.可用;
            info.ZhuCeShiJian = DateTime.Now;

            int dalRetCode = dal.HuiYuan_CU(info);

            return dalRetCode;
        }

        /// <summary>
        /// 修改会员信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateHuiYuan(EyouSoft.Model.YlStructure.MHuiYuanInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.CompanyId)
                || string.IsNullOrEmpty(info.HuiYuanId)) return 0;

            //info.HuiYuanId = Guid.NewGuid().ToString();
            //info.ZhuCeShiJian = DateTime.Now;

            int dalRetCode = dal.HuiYuan_CU(info);

            return dalRetCode;
        }

        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MHuiYuanInfo GetHuiYuanInfo(string huiYuanId)
        {
            if (string.IsNullOrEmpty(huiYuanId)) return null;

            return dal.GetHuiYuanInfo(huiYuanId);
        }

        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="username">用户名或者注册邮箱</param>
        /// <param name="fx">0:username 1:username or email</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MHuiYuanInfo GetHuiYuanInfo(string companyId, string username, int fx)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetHuiYuanInfo(companyId, username, fx);
        }

        /// <summary>
        /// 获取会员信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHuiYuanInfo> GetHuiYuans(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetHuiYuans(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 写入常旅客信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertChangLvKe(EyouSoft.Model.YlStructure.MHuiYuanChangLvKeInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.HuiYuanId)) return 0;

            info.LvkeId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;
            info.ZhuangTai = EyouSoft.Model.EnumType.YlStructure.HuiYuanChangLvKeStatus.可用;

            int dalRetCode = dal.InsertChangLvKe(info);

            return dalRetCode;
        }

        /// <summary>
        /// 更新常旅客信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateChangLvKe(EyouSoft.Model.YlStructure.MHuiYuanChangLvKeInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.HuiYuanId)||string.IsNullOrEmpty(info.LvkeId)) return 0;

            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.UpdateChangLvKe(info);

            return dalRetCode;
        }

        /// <summary>
        /// 删除常旅客信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="lvkeId">常旅客编号</param>
        /// <returns></returns>
        public int DeleteChangLvKe(string companyId, string huiYuanId, string lvkeId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(huiYuanId) || string.IsNullOrEmpty(lvkeId)) return 0;

            int dalRetCode = dal.DeleteChangLvKe(companyId, huiYuanId, lvkeId);

            return dalRetCode;
        }

        /// <summary>
        /// 获取常旅客信息
        /// </summary>
        /// <param name="lvkeId">常旅客编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MHuiYuanChangLvKeInfo GetChangLvKeInfo(string lvkeId)
        {
            if (string.IsNullOrEmpty(lvkeId)) return null;
            return dal.GetChangLvKeInfo(lvkeId);
        }

        /// <summary>
        /// 获取常旅客集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHuiYuanChangLvKeInfo> GetChangLvKes(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanChangLvKeChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetChangLvKes(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 写入地址信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertDiZhi(EyouSoft.Model.YlStructure.MHuiYuanDiZhiInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.HuiYuanId)) return 0;
            info.DiZhiId = Guid.NewGuid().ToString();
            info.IsMoRen = false;
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.InsertDiZhi(info);
            return dalRetCode;
        }

        /// <summary>
        /// 更新地址信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateDiZhi(EyouSoft.Model.YlStructure.MHuiYuanDiZhiInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.HuiYuanId)||string.IsNullOrEmpty(info.DiZhiId)) return 0;
            info.IsMoRen = false;
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.UpdateDiZhi(info);
            return dalRetCode;
        }

        /// <summary>
        /// 删除地址信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="diZhiId">地址编号</param>
        /// <returns></returns>
        public int DeleteDiZhi(string companyId, string huiYuanId, string diZhiId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(huiYuanId) || string.IsNullOrEmpty(diZhiId)) return 0;

            int dalRetCode = dal.DeleteDiZhi(companyId, huiYuanId, diZhiId);

            return dalRetCode;
        }

        /// <summary>
        /// 获取地址信息
        /// </summary>
        /// <param name="diZhiId">地址编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MHuiYuanDiZhiInfo GetDiZhiInfo(string diZhiId)
        {
            if (string.IsNullOrEmpty(diZhiId)) return null;

            return dal.GetDiZhiInfo(diZhiId);
        }

        /// <summary>
        /// 获取地址集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHuiYuanDiZhiInfo> GetDiZhis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanDiZhiChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetDiZhis(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 设置会员默认地址，返回1成功，其它失败
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="diZhiId">地址编号</param>
        /// <returns></returns>
        public int SheZhiMoRenDiZhi(string huiYuanId, string diZhiId)
        {
            if (string.IsNullOrEmpty(huiYuanId) || string.IsNullOrEmpty(diZhiId)) return 0;

            int dalRetCode = dal.SheZhiMoRenDiZhi(huiYuanId, diZhiId);
            return dalRetCode;
        }

        /// <summary>
        /// 设置会员常旅客状态
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="lvKeId">常旅客编号</param>
        /// <param name="status">常旅客状态</param>
        /// <returns></returns>
        public int SheZhiChangLvKeStatus(string huiYuanId, string lvKeId, EyouSoft.Model.EnumType.YlStructure.HuiYuanChangLvKeStatus status)
        {
            if (string.IsNullOrEmpty(huiYuanId) || string.IsNullOrEmpty(lvKeId)) return 0;

            int dalRetCode = dal.SheZhiChangLvKeStatus(huiYuanId, lvKeId, status);
            return dalRetCode;
        }

        /// <summary>
        /// 写入游轮攻略信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertGongLue(EyouSoft.Model.YlStructure.MWzGongLueInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.CompanyId)||string.IsNullOrEmpty(info.OperatorId)) return 0;
            info.GongLueId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;
            info.IsShenHe = false;

            int dalRetCode = dal.InsertGongLue(info);
            return dalRetCode;
        }

        /// <summary>
        /// 更新游轮攻略信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateGongLue(EyouSoft.Model.YlStructure.MWzGongLueInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.GongLueId)) return 0;
            info.IsShenHe = false;
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.UpdateGongLue(info);
            return dalRetCode;
        }

        /// <summary>
        /// 删除游轮攻略信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="gongLueId">攻略编号</param>
        /// <returns></returns>
        public int DeleteGongLue(string companyId, string huiYuanId, string gongLueId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(huiYuanId) || string.IsNullOrEmpty(gongLueId)) return 0;
            int dalRetCode = dal.DeleteGongLue(companyId, huiYuanId, gongLueId);
            return dalRetCode;
        }

        /// <summary>
        /// 获取游轮攻略信息
        /// </summary>
        /// <param name="gongLueId">攻略编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzGongLueInfo GetGongLueInfo(string gongLueId)
        {
            if (string.IsNullOrEmpty(gongLueId)) return null;
            return dal.GetGongLueInfo(gongLueId);
        }

        /// <summary>
        /// 获取游轮攻略集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzGongLueInfo> GetGongLues(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzGongLueChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;
            return dal.GetGongLues(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 审核游轮攻略
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="operatorId">审核人编号</param>
        /// <param name="gongLueId">攻略编号</param>
        /// <returns></returns>
        public int ShenHeGongLue(string companyId, string operatorId, string gongLueId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(operatorId) || string.IsNullOrEmpty(gongLueId)) return 0;

            int dalRetCode = dal.ShenHeGongLue(companyId, operatorId, gongLueId);
            return dalRetCode;
        }

        /// <summary>
        /// 写入用户点评信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertDianPing(EyouSoft.Model.YlStructure.MWzDianPingInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.CompanyId) || string.IsNullOrEmpty(info.OperatorId)) return 0;

            info.DianPingId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;
            //info.IsShenHe = false;

            int dalRetCode = dal.InsertDianPing(info);
            return dalRetCode;
        }

        /// <summary>
        /// 更新用户点评信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateDianPing(EyouSoft.Model.YlStructure.MWzDianPingInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.DianPingId)) return 0;
            //info.IsShenHe = false;
            int dalRetCode = dal.UpdateDianPing(info);
            return dalRetCode;
        }

        /// <summary>
        /// 删除用户点评信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="dianPingId">点评编号</param>
        /// <returns></returns>
        public int DeleteDianPing(string companyId, string huiYuanId, string dianPingId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(huiYuanId) || string.IsNullOrEmpty(dianPingId)) return 0;

            int dalRetCode = dal.DeleteDianPing(companyId, huiYuanId, dianPingId);
            return dalRetCode;
        }

        /// <summary>
        /// 获取用户点评信息
        /// </summary>
        /// <param name="dianPingId">点评编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzDianPingInfo GetDianPingInfo(string dianPingId)
        {
            if (string.IsNullOrEmpty(dianPingId)) return null;
            return dal.GetDianPingInfo(dianPingId);
        }

        /// <summary>
        /// 获取用户点评集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzDianPingInfo> GetDianPings(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzDianPingChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetDianPings(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }
        
        /// <summary>
        /// 审核用户点评
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="operatorId">审核人编号</param>
        /// <param name="dianPingId">点评编号</param>
        /// <returns></returns>
        public int ShenHeDianPing(string companyId, string operatorId, string dianPingId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(operatorId) || string.IsNullOrEmpty(dianPingId)) return 0;
            int dalRetCode = dal.ShenHeDianPing(companyId, operatorId, dianPingId);
            return dalRetCode;
        }

        /// <summary>
        /// 写入咨询问答信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertWenDa(EyouSoft.Model.YlStructure.MWzWenDaInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.CompanyId)) return 0;
            info.WenDaId = Guid.NewGuid().ToString();
            info.WenShiJian = DateTime.Now;
            int dalRetCode = dal.InsertWenDa(info);
            return dalRetCode;
        }

        /// <summary>
        /// 更新咨询问答信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateWenDa(EyouSoft.Model.YlStructure.MWzWenDaInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.WenDaId)) return 0;

            int dalRetCode = dal.UpdateWenDa(info);
            return dalRetCode;
        }

        /// <summary>
        /// 删除咨询问答信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="wenDaId">问答编号</param>
        /// <returns></returns>
        public int DeleteWenDa(string companyId, string huiYuanId, string wenDaId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(huiYuanId) || string.IsNullOrEmpty(wenDaId)) return 0;

            int dalRetCode = dal.DeleteWenDa(companyId, huiYuanId, wenDaId);
            return dalRetCode;
        }

        /// <summary>
        /// 获取咨询问答信息
        /// </summary>
        /// <param name="wenDaId">问答编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzWenDaInfo GetWenDaInfo(string wenDaId)
        {
            if (string.IsNullOrEmpty(wenDaId)) return null;
            return dal.GetWenDaInfo(wenDaId);
        }

        /// <summary>
        /// 获取咨询问答集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzWenDaInfo> GetWenDas(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzWenDaChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetWenDas(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }
        
        /// <summary>
        /// 回复咨询问答，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int HuiFuWenDa(EyouSoft.Model.YlStructure.MWzWenDaInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.DaOperatorId) || string.IsNullOrEmpty(info.WenDaId)) return 0;
            info.DaShiJian = DateTime.Now;
            int dalRetCode = dal.HuiFuWenDa(info);

            return dalRetCode;
        }

        /// <summary>
        /// 写入收藏夹信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertShouCangJia(EyouSoft.Model.YlStructure.MHuiYuanShouCangJiaInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.HuiYuanId) || string.IsNullOrEmpty(info.ChanPinId)) return 0;

            info.ShouCangId = Guid.NewGuid().ToString();
            info.ShiJian = DateTime.Now;

            int dalRetCode = dal.InsertShouCangJia(info);
            return dalRetCode;
        }

        /// <summary>
        /// 删除收藏夹信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="shouCangId">收藏编号</param>
        /// <returns></returns>
        public int DeleteShouCangJia(string companyId, string huiYuanId, string shouCangId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(huiYuanId) || string.IsNullOrEmpty(shouCangId)) return 0;

            int dalRetCode = dal.DeleteShouCangJia(companyId, huiYuanId, shouCangId);
            return dalRetCode;
        }

        /// <summary>
        /// 获取收藏夹信息
        /// </summary>
        /// <param name="shouCangId">收藏编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MHuiYuanShouCangJiaInfo GetShouCangJiaInfo(string shouCangId)
        {
            if (string.IsNullOrEmpty(shouCangId)) return null;
            return dal.GetShouCangJiaInfo(shouCangId);
        }

        /// <summary>
        /// 获取收藏夹集合
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHuiYuanShouCangJiaInfo> GetShouCangJias(string huiYuanId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanShouCangJiaChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(huiYuanId)) return null;

            return dal.GetShouCangJias(huiYuanId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 设置会员密码，返回1成功，其它失败
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="yuanmd5">原密码MD5</param>
        /// <param name="md5">新密码MD5</param>
        /// <returns></returns>
        public int SheZhiHuiYuanMiMa(string huiYuanId, string yuanmd5, string md5)
        {
            if (string.IsNullOrEmpty(huiYuanId) || string.IsNullOrEmpty(yuanmd5) || string.IsNullOrEmpty(md5)) return 0;

            int dalRetCode = dal.SheZhiHuiYuanMiMa(huiYuanId, yuanmd5, md5);
            return dalRetCode;
        }

        /// <summary>
        /// 设置会员头像
        /// </summary>
        /// <param name="huiyuanid">会员编号</param>
        /// <param name="src">头像链接</param>
        /// <returns></returns>
        public int SheZhiHuiYuanTouXiang(string huiyuanid, string src)
        {
            return dal.SheZhiHuiYuanTouXiang(huiyuanid, src);
        }
        /// <summary>
        /// 设置会员状态
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public int SheZhiHuiYuanStatus(string huiYuanId, EyouSoft.Model.EnumType.YlStructure.HuiYuanStatus status)
        {
            if (string.IsNullOrEmpty(huiYuanId)) return 0;

            int dalRetCode = dal.SheZhiHuiYuanStatus(huiYuanId, status);

            return dalRetCode;
        }

        /// <summary>
        /// 获取会员积分明细集合
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHuiYuanJiFenMxInfo> GetHuiYuanJiFenMingXis(string huiYuanId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanJiFenMxChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(huiYuanId)) return null;

            return dal.GetHuiYuanJiFenMxs(huiYuanId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 获取会员预存款明细集合
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHuiYuanYuCunKuanMxInfo> GetHuiYuanYuCunKuanMxs(string huiYuanId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanYuCunKuanMxChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(huiYuanId)) return null;

            return dal.GetHuiYuanYuCunKuanMxs(huiYuanId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 获取会员订单集合
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHuiYuanDingDanInfo> GetHuiYuanDingDans(string huiYuanId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHuiYuanDingDanChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(huiYuanId)) return null;

            return dal.GetHuiYuanDingDans(huiYuanId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 获取点评均分
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <returns></returns>
        public Decimal GetDianPingJunFen(string hangQiId)
        {
            if (string.IsNullOrEmpty(hangQiId)) return 0;
            return dal.GetDianPingJunFen(hangQiId);
        }

        /// <summary>
        /// 获取用户点评信息，按订单编号获取
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzDianPingInfo GetDianPingInfo1(string dingDanId)
        {
            if (string.IsNullOrEmpty(dingDanId)) return null;

            return dal.GetDianPingInfo1(dingDanId);
        }

        /// <summary>
        /// 设置会员账号
        /// </summary>
        /// <param name="huiYuanId">用户编号</param>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public int SetHuiYuanUsername(string huiYuanId, string username)
        {
            if (string.IsNullOrEmpty(huiYuanId) || string.IsNullOrEmpty(username)) return 0;

            int bllRetCode = dal.SetHuiYuanUsername(huiYuanId, username);

            return bllRetCode;
        }

        /// <summary>
        /// 设置会员密码
        /// </summary>
        /// <param name="huiYuanId">用户编号</param>
        /// <param name="md5pwd">md5密码</param>
        /// <returns></returns>
        public int SetHuiYuanMiMa(string huiYuanId, string md5pwd)
        {
            if (string.IsNullOrEmpty(huiYuanId) || string.IsNullOrEmpty(md5pwd)) return 0;

            int bllRetCode = dal.SetHuiYuanMiMa(huiYuanId, md5pwd);
            return bllRetCode;
        }
        #endregion
    }
}
