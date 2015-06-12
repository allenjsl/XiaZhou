//游轮网站相关 汪奇志 2014-03-27
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.YlStructure
{
    /// <summary>
    /// 游轮网站相关
    /// </summary>
    public class BWz: BLLBase
    {
        readonly EyouSoft.IDAL.YlStructure.IWz dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.YlStructure.IWz>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BWz() { }
        #endregion

        #region private members

        #endregion

        #region public members
        /// <summary>
        /// 设置网站KV信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int SheZhiKvInfo(EyouSoft.Model.YlStructure.MWzKvInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.CompanyId)) return 0;

            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.SheZhiKvInfo(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("设置网站信息，KEY：" + (int)info.K);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取网站KV信息
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="k">key</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzKvInfo GetKvInfo(string companyId, EyouSoft.Model.EnumType.YlStructure.WzKvKey k)
        {
            if (string.IsNullOrEmpty(companyId)) return null;
            return dal.GetKvInfo(companyId, k);
        }

        /// <summary>
        /// 获取网站域名信息
        /// </summary>
        /// <param name="yuMing">域名</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzYuMingInfo GetYuMingInfo(string yuMing)
        {
            if (string.IsNullOrEmpty(yuMing)) return null;
            yuMing = yuMing.ToLower();
            var info = dal.GetYuMingInfo(yuMing);
            info.CompanyId = "0c962db0-690f-452d-9857-225eb7d5412d";
            return info;
        }

        /// <summary>
        /// 写入友情链接信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertYouQingLianJie(EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.OperatorId)) return 0;

            info.LianJieId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.InsertYouQingLianJie(info);

            return dalRetCode;
        }

        /// <summary>
        /// 更新友情链接信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateYouQingLianJie(EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CompanyId)
                || string.IsNullOrEmpty(info.OperatorId)
                || string.IsNullOrEmpty(info.LianJieId)) return 0;

            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.UpdateYouQingLianJie(info);

            return dalRetCode;
        }

        /// <summary>
        /// 删除友情链接信息，返回1成功，其它失败
        /// </summary>
        /// <param name="compamyId">公司编号</param>
        /// <param name="lianJieId">链接编号</param>
        /// <returns></returns>
        public int DeleteYouQingLianJie(string compamyId, string lianJieId)
        {
            if (string.IsNullOrEmpty(compamyId) || string.IsNullOrEmpty(lianJieId)) return 0;

            int dalRetCode = dal.DeleteYouQingLianJie(compamyId, lianJieId);

            return dalRetCode;
        }

        /// <summary>
        /// 获取友情链接信息
        /// </summary>
        /// <param name="lianJieId">链接编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo GetYouQingLianJieInfo(string lianJieId)
        {
            if (string.IsNullOrEmpty(lianJieId)) return null;
            return dal.GetYouQingLianJieInfo(lianJieId);
        }

        /// <summary>
        /// 获取友情链接信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo> GetYouQingLianJies(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzYouQingLianJieChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetYouQingLianJies(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

                /// <summary>
        /// 获取友情链接信息集合
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="chaXun"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzYouQingLianJieInfo> GetYouQingLianJies(string companyId, EyouSoft.Model.YlStructure.MWzYouQingLianJieChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetYouQingLianJies(companyId, chaXun);
        }

        /// <summary>
        /// 新增广告信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertGuangGao(EyouSoft.Model.YlStructure.MWzGuangGaoInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.OperatorId)) return 0;

            info.IssueTime = DateTime.Now;
            info.GuangGaoId = Guid.NewGuid().ToString();

            int dalRetCode = dal.InsertGuangGao(info);

            return dalRetCode;
        }

        /// <summary>
        /// 修改广告信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateGuangGao(EyouSoft.Model.YlStructure.MWzGuangGaoInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CompanyId)
                || string.IsNullOrEmpty(info.OperatorId)
                || string.IsNullOrEmpty(info.GuangGaoId)) return 0;

            int dalRetCode = dal.UpdateGuangGao(info);

            return dalRetCode;
        }

        /// <summary>
        /// 删除广告信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="guangGaoId">广告编号</param>
        /// <returns></returns>
        public int DeleteGuangGao(string companyId, string guangGaoId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(guangGaoId)) return 0;

            int dalRetCode = dal.DeleteGuangGao(companyId, guangGaoId);

            return dalRetCode;
        }

        /// <summary>
        /// 获取广告信息
        /// </summary>
        /// <param name="guangGaoId">广告编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzGuangGaoInfo GetGuangGaoInfo(string guangGaoId)
        {
            if (string.IsNullOrEmpty(guangGaoId)) return null;

            return dal.GetGuangGaoInfo(guangGaoId);
        }

        /// <summary>
        /// 获取广告信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzGuangGaoInfo> GetGuangGaos(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzGuangGaoChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetGuangGaos(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 新增资讯信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertZiXun(EyouSoft.Model.YlStructure.MWzZiXunInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.CompanyId) || string.IsNullOrEmpty(info.OperatorId)) return 0;

            info.ZiXunId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.InsertZiXun(info);
            return dalRetCode;
        }

        /// <summary>
        /// 修改资讯信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateZiXun(EyouSoft.Model.YlStructure.MWzZiXunInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.OperatorId) 
                || string.IsNullOrEmpty(info.ZiXunId)) return 0;

            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.UpdateZiXun(info);

            return dalRetCode;
        }

        /// <summary>
        /// 删除资讯信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="ziXunId">资讯编号</param>
        /// <returns></returns>
        public int DeleteZiXun(string companyId, string ziXunId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(ziXunId)) return 0;

            int dalRetCode = dal.DeleteZiXun(companyId, ziXunId);

            return dalRetCode;
        }

        /// <summary>
        /// 获取资讯信息
        /// </summary>
        /// <param name="ziXunId">资讯编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzZiXunInfo GetZiXunInfo(string ziXunId)
        {
            if (string.IsNullOrEmpty(ziXunId)) return null;

            return dal.GetZiXunInfo(ziXunId);
        }

        /// <summary>
        /// 获取资讯信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzZiXunInfo> GetZiXuns(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzZiXunChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;
            return dal.GetZiXuns(companyId, pageSize, pageIndex,ref recordCount, chaXun);
        }

        /// <summary>
        /// 新增公司荣誉信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertGongSiRongYu(EyouSoft.Model.YlStructure.MWzGongSiRongYuInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CompanyId)
                || string.IsNullOrEmpty(info.OperatorId)) return 0;

            info.RongYuId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.InsertGongSiRongYu(info);

            return dalRetCode;
        }


        /// <summary>
        /// 修改公司荣誉信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateGongSiRongYu(EyouSoft.Model.YlStructure.MWzGongSiRongYuInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.OperatorId) 
                || string.IsNullOrEmpty(info.RongYuId)) return 0;

            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.UpdateGongSiRongYu(info);

            return dalRetCode;
        }


        /// <summary>
        /// 删除公司荣誉信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="rongYuId">荣誉编号</param>
        /// <returns></returns>
        public int DeleteGongSiRongYu(string companyId, string rongYuId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(rongYuId)) return 0;

            int dalRetCode = dal.DeleteGongSiRongYu(companyId, rongYuId);
            return dalRetCode;
        }


        /// <summary>
        /// 获取公司荣誉信息
        /// </summary>
        /// <param name="rongYuId">荣誉编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzGongSiRongYuInfo GetGongSiRongYuInfo(string rongYuId)
        {
            if (string.IsNullOrEmpty(rongYuId)) return null;

            return dal.GetGongSiRongYuInfo(rongYuId);
        }

        /// <summary>
        /// 获取公司荣誉信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzGongSiRongYuInfo> GetGongSiRongYus(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzGongSiRongYuChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;
            return dal.GetGongSiRongYus(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 新增员工风采信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertYuanGongFengCai(EyouSoft.Model.YlStructure.MWzYuanGongFengCaiInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CompanyId)
                || string.IsNullOrEmpty(info.OperatorId)) return 0;

            info.FengCaiId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.InsertYuanGongFengCai(info);
            return dalRetCode;
        }

        /// <summary>
        /// 修改员工风采信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateYuanGongFengCai(EyouSoft.Model.YlStructure.MWzYuanGongFengCaiInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.OperatorId) 
                || string.IsNullOrEmpty(info.FengCaiId)) return 0;
            int dalRetCode = dal.UpdateYuanGongFengCai(info);
            return dalRetCode;
        }

        /// <summary>
        /// 删除员工风采信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="fengCaiId">风采编号</param>
        /// <returns></returns>
        public int DeleteYuanGongFengCai(string companyId, string fengCaiId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(fengCaiId)) return 0;

            int dalRetCode = dal.DeleteYuanGongFengCai(companyId, fengCaiId);
            return dalRetCode;
        }

        /// <summary>
        /// 获取员工风采信息
        /// </summary>
        /// <param name="fengCaiId">风采编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzYuanGongFengCaiInfo GetYuanGongFengCaiInfo(string fengCaiId)
        {
            if (string.IsNullOrEmpty(fengCaiId)) return null;

            return dal.GetYuanGongFengCaiInfo(fengCaiId);
        }

        /// <summary>
        /// 获取员工风采信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzYuanGongFengCaiInfo> GetYuanGongFengCais(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzYuanGongFengCaiChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetYuanGongFengCais(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 新增招聘岗位信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertZhaoPinGangWei(EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CompanyId)
                || string.IsNullOrEmpty(info.OperatorId)) return 0;

            info.GangWeiId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCdoe = dal.InsertZhaoPinGangWei(info);
            return dalRetCdoe;
        }

        /// <summary>
        /// 修改招聘岗位信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateZhaoPinGangWei(EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.OperatorId) 
                || string.IsNullOrEmpty(info.GangWeiId)) return 0;

            info.IssueTime = DateTime.Now;

            int dalRetCdoe = dal.UpdateZhaoPinGangWei(info);
            return dalRetCdoe;
        }

        /// <summary>
        /// 删除招聘岗位信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gangWeiId">岗位编号</param>
        /// <returns></returns>
        public int DeleteZhaoPinGangWei(string companyId, string gangWeiId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(gangWeiId)) return 0;

            int dalRetCode = dal.DeleteZhaoPinGangWei(companyId, gangWeiId);
            return dalRetCode;
        }

        /// <summary>
        /// 获取招聘岗位信息
        /// </summary>
        /// <param name="gangWeiId">岗位编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiInfo GetZhaoPinGangWeiInfo(string gangWeiId)
        {
            if (string.IsNullOrEmpty(gangWeiId)) return null;

            return dal.GetZhaoPinGangWeiInfo(gangWeiId);
        }

        /// <summary>
        /// 获取招聘岗位信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiInfo> GetZhaoPinGangWeis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzZhaoPinGangWeiChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;
            return dal.GetZhaoPinGangWeis(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 新增会议案例信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertHuiYiAnLi(EyouSoft.Model.YlStructure.MWzHuiYiAnLiInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CompanyId)
                || string.IsNullOrEmpty(info.OperatorId)) return 0;

            if (info.ShiJian1 == DateTime.MinValue) info.ShiJian1 = DateTime.Now;
            if (info.ShiJian2 == DateTime.MinValue) info.ShiJian2 = DateTime.Now;
            info.IssueTime = DateTime.Now;
            info.AnLiId = Guid.NewGuid().ToString();

            int dalRetCode = dal.InsertHuiYiAnLi(info);
            return dalRetCode;
        }

        /// <summary>
        /// 修改会议案例信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateHuiYiAnLi(EyouSoft.Model.YlStructure.MWzHuiYiAnLiInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.OperatorId) 
                || string.IsNullOrEmpty(info.AnLiId)) return 0;

            if (info.ShiJian1 == DateTime.MinValue) info.ShiJian1 = DateTime.Now;
            if (info.ShiJian2 == DateTime.MinValue) info.ShiJian2 = DateTime.Now;
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.UpdateHuiYiAnLi(info);
            return dalRetCode;
        }

        /// <summary>
        /// 删除会议案例信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="anLiId">案例编号</param>
        /// <returns></returns>
        public int DeleteHuiYiAnLi(string companyId, string anLiId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(anLiId)) return 0;
            int dalRetCode = dal.DeleteHuiYiAnLi(companyId, anLiId);
            return dalRetCode;
        }

        /// <summary>
        /// 获取会议案例信息
        /// </summary>
        /// <param name="anLiId">案例编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzHuiYiAnLiInfo GetHuiYiAnLiInfo(string anLiId)
        {
            if (string.IsNullOrEmpty(anLiId)) return null;
            return dal.GetHuiYiAnLiInfo(anLiId);
        }

        /// <summary>
        /// 获取会议案例信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzHuiYiAnLiInfo> GetHuiYiAnLis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzHuiYiAnLiChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;
            return dal.GetHuiYiAnLis(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 写入会议申请信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertHuiYiShenQing(EyouSoft.Model.YlStructure.MWzHuiYiShenQingInfo info)
        {
            if (info == null || string.IsNullOrEmpty(info.CompanyId)) return 0;

            info.ShenQingId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.InsertHuiYiShenQing(info);
            return dalRetCode;
        }

        /// <summary>
        /// 删除会议申请信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="shenQingId">申请编号</param>
        /// <returns></returns>
        public int DeleteHuiYiShenQing(string companyId, string shenQingId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(shenQingId)) return 0;

            int dalRetCode = dal.DeleteHuiYiShenQing(companyId, shenQingId);
            return dalRetCode;
        }

        /// <summary>
        /// 获取会议申请信息
        /// </summary>
        /// <param name="shenQingId">申请编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzHuiYiShenQingInfo GetHuiYiShenQingInfo(string shenQingId)
        {
            if (string.IsNullOrEmpty(shenQingId)) return null;

            return dal.GetHuiYiShenQingInfo(shenQingId);
        }

        /// <summary>
        /// 获取会议申请集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzHuiYiShenQingInfo> GetHuiYiShenQings(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzHuiYiShenQingChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetHuiYiShenQings(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 会议申请处理
        /// </summary>
        /// <param name="shenQingId">申请编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="beiZhu">备注</param>
        /// <returns></returns>
        public int HuiYiShenQingChuLi(string shenQingId, string operatorId, string beiZhu)
        {
            if (string.IsNullOrEmpty(shenQingId) || string.IsNullOrEmpty(operatorId)) return 0;
            int dalRetCode = dal.HuiYiShenQingChuLi(shenQingId, operatorId, beiZhu);

            return dalRetCode;
        }
        /// <summary>
        /// 网站意见反馈新增
        /// </summary>
        /// <param name="m">意见反馈实体</param>
        /// <returns>1：成功 0：失败</returns>
        public int AddWZYiJianFanKui(Model.YlStructure.MWzYiJianFanKuiInfo m)
        {
            if (m == null) return 0;
            return dal.AddWZYiJianFanKui(m);
        }
        /// <summary>
        /// 删除网站意见反馈
        /// </summary>
        /// <param name="yijianId"></param>
        /// <returns>0：失败 1：成功</returns>
        public int DelWZYiJianFanKui(string yijianId)
        {
            if (string.IsNullOrEmpty(yijianId)) return 0;
            return dal.DelWZYiJianFanKui(yijianId);
        }
        /// <summary>
        /// 获取网站意见反馈信息
        /// </summary>
        /// <param name="yijianId">意见编号</param>
        /// <returns></returns>
        public Model.YlStructure.MWzYiJianFanKuiInfo GetWZYiJianFanKui(string yijianId)
        {
            if (string.IsNullOrEmpty(yijianId)) return null;
            return dal.GetWZYiJianFanKui(yijianId);
        }
        /// <summary>
        /// 获取网站意见反馈信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="chaXun"></param>
        /// <returns></returns>
        public IList<Model.YlStructure.MWzYiJianFanKuiInfo> GetWZYiJianFanKui(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzYiJianFanKuiChaXun chaXun)
        {
            if (chaXun == null) return null;
            return dal.GetWZYiJianFanKui(pageSize, pageIndex, ref recordCount, chaXun);
        }
        #endregion
    }
}
