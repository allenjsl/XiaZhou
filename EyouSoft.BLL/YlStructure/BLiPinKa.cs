//YL-礼品卡相关 汪奇志 2014-04-08
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.YlStructure
{
    /// <summary>
    /// YL-礼品卡相关
    /// </summary>
    public class BLiPinKa
    {
        readonly EyouSoft.IDAL.YlStructure.ILiPinKa dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.YlStructure.ILiPinKa>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BLiPinKa() { }
        #endregion

        #region private members

        #endregion

        #region public members
        /// <summary>
        /// 礼品卡新增，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertLiPinKa(EyouSoft.Model.YlStructure.MLiPinKaInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.OperatorId)) return 0;

            info.LiPinKaId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.LiPinKa_CU(info);

            return dalRetCode;
        }

        /// <summary>
        /// 礼品卡修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateLiPinKa(EyouSoft.Model.YlStructure.MLiPinKaInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CompanyId)
                || string.IsNullOrEmpty(info.OperatorId)
                || string.IsNullOrEmpty(info.LiPinKaId)) return 0;

            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.LiPinKa_CU(info);

            return dalRetCode;
        }

        /// <summary>
        /// 礼品卡删除，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="liPinKaId">礼品卡编号</param>
        /// <returns></returns>
        public int DeleteLiPinKa(string companyId, string liPinKaId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(liPinKaId)) return 0;

            int dalRetCode = dal.LiPinKa_D(companyId, liPinKaId);

            return dalRetCode;
        }

        /// <summary>
        /// 获取礼品卡信息
        /// </summary>
        /// <param name="liPinKaId">礼品卡编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MLiPinKaInfo GetLiPinKaInfo(string liPinKaId)
        {
            if (string.IsNullOrEmpty(liPinKaId)) return null;

            return dal.GetLiPinKaInfo(liPinKaId);
        }

        /// <summary>
        /// 获取礼品卡集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MLiPinKaInfo> GetLiPinKas(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MLiPinKaChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetLiPinKas(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 新增礼品卡订单，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertLiPinKaDingDan(EyouSoft.Model.YlStructure.MLiPinKaDingDanInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.LiPinKaId) 
                || string.IsNullOrEmpty(info.XiaDanRenId)) return 0;

            int dalRetCode = dal.LiPinKaDingDan_C(info);

            return dalRetCode;
        }

        /// <summary>
        /// 获取礼品卡订单信息
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MLiPinKaDingDanInfo GetLiPinKaDingDanInfo(string dingDanId)
        {
            if (string.IsNullOrEmpty(dingDanId)) return null;

            return dal.GetLiPinKaDingDanInfo(dingDanId);
        }

        /// <summary>
        /// 获取礼品卡订单集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MLiPinKaDingDanInfo> GetLiPinKaDingDans(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MLiPinKaDingDanChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetLiPinKaDingDans(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 设置礼品卡订单状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="status">订单状态</param>
        /// <returns></returns>
        public int SheZhiLiPinKaDingDanStatus(string dingDanId, string operatorId, EyouSoft.Model.EnumType.YlStructure.LiPinKaDingDanStatus status)
        {
            if (string.IsNullOrEmpty(dingDanId) || string.IsNullOrEmpty(operatorId)) return 0;

            int dalRetCode = dal.SheZhiLiPinKaDingDanStatus(dingDanId, operatorId, status);
            return dalRetCode;
        }

        /// <summary>
        /// 设置礼品卡订单付款状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="status">付款状态</param>
        /// <param name="fuKuanShiJian">付款时间</param>
        /// <param name="info">款项实体</param>
        /// <returns></returns>
        public int SheZhiLiPinKaDingDanFuKuanStatus(string dingDanId, string operatorId, EyouSoft.Model.EnumType.YlStructure.FuKuanStatus status, DateTime? fuKuanShiJian, EyouSoft.Model.YlStructure.MHangQiDingDanKuanInfo info)
        {
            if (string.IsNullOrEmpty(dingDanId) || string.IsNullOrEmpty(operatorId)) return 0;
            if (info == null) return 0;
            if (!fuKuanShiJian.HasValue) fuKuanShiJian = DateTime.Now;
            int dalRetCode = dal.SheZhiLiPinKaDingDanFuKuanStatus(dingDanId, operatorId, status, fuKuanShiJian, info);
            return dalRetCode;
        }
        #endregion
    }
}
