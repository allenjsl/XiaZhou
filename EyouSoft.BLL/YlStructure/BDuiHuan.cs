//积分兑换商品相关 汪奇志 2014-03-30
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.YlStructure
{
    /// <summary>
    /// 积分兑换商品相关
    /// </summary>
    public class BDuiHuan
    {
        readonly EyouSoft.IDAL.YlStructure.IDuiHuan dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.YlStructure.IDuiHuan>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BDuiHuan() { }
        #endregion

        #region private members

        #endregion

        #region public members
        /// <summary>
        /// 积分商品新增，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertJiFenShangPin(EyouSoft.Model.YlStructure.MWzJiFenShangPinInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.OperatorId) 
                || info.FangShis == null 
                || info.FangShis.Count == 0) return 0;

            info.ShangPinId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.JiFenShangPin_CU(info);

            return dalRetCode;
        }

        /// <summary>
        /// 积分商品修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateJiFenShangPin(EyouSoft.Model.YlStructure.MWzJiFenShangPinInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CompanyId)
                || string.IsNullOrEmpty(info.OperatorId)
                || info.FangShis == null
                || info.FangShis.Count == 0
                || string.IsNullOrEmpty(info.ShangPinId)) return 0;

            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.JiFenShangPin_CU(info);

            return dalRetCode;
        }

        /// <summary>
        /// 积分商品删除，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="shangPinId">商品编号</param>
        /// <returns></returns>
        public int DeleteJiFenShangPin(string companyId, string shangPinId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(shangPinId)) return 0;

            return dal.JiFenShangPin_D(companyId, shangPinId);
        }

        /// <summary>
        /// 获取积分商品信息
        /// </summary>
        /// <param name="shangPinId">商品编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzJiFenShangPinInfo GetJiFenShangPinInfo(string shangPinId)
        {
            if (string.IsNullOrEmpty(shangPinId)) return null;

            return dal.GetJiFenShangPinInfo(shangPinId);
        }

        /// <summary>
        /// 获取积分商品信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzJiFenShangPinInfo> GetJiFenShangPins(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzJiFenShangPinChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetJiFenShangPins(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 新增积分订单，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertJiFenDingDan(EyouSoft.Model.YlStructure.MWzJiFenDingDanInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.XiaDanRenId) 
                || string.IsNullOrEmpty(info.ShangPinId)) return 0;

            info.DingDanId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;
            info.DingDanStatus = EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus.已成交;

            int dalRetCode = dal.JiFenDingDan_C(info);
            if (info.FangShi != Model.EnumType.YlStructure.JiFenDuiHuanFangShi.积分现金 && dalRetCode == 1)
            {
                var m = new BLL.YlStructure.BHuiYuan().GetHuiYuanInfo(info.XiaDanRenId);
                var s = dal.GetJiFenShangPinInfo(info.ShangPinId);
                if (m != null && !string.IsNullOrEmpty(m.ShouJi))
                {
                    var content = "尊敬的会员" + m.XingMing + "：您在积分商城成功兑换" + (s != null ? s.MingCheng : string.Empty) + "，该商品将在5个工作日内为您寄出，请保持手机畅通！服务热线：400-665-9099【维诗达游轮网】";
                    EyouSoft.Toolkit.Utils.ProxySMS(m.ShouJi, content);
                }
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取积分订单信息
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MWzJiFenDingDanInfo GetJiFenDingDanInfo(string dingDanId)
        {
            if (string.IsNullOrEmpty(dingDanId)) return null;

            return dal.GetJiFenDingDanInfo(dingDanId);
        }

        /// <summary>
        /// 获取积分订单集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MWzJiFenDingDanInfo> GetJiFenDingDans(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MWzJiFenDingDanChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetJiFenDingDans(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 设置积分兑换订单状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="status">订单状态</param>
        /// <returns></returns>
        public int SheZhiJiFenDingDanStatus(string dingDanId, string operatorId, EyouSoft.Model.EnumType.YlStructure.JiFenDingDanStatus status)
        {
            if (string.IsNullOrEmpty(dingDanId) 
                || string.IsNullOrEmpty(operatorId)) return 0;

            int dalRetCode = dal.SheZhiJiFenDingDanStatus(dingDanId, operatorId, status);
            return dalRetCode;
        }

        /// <summary>
        /// 设置积分兑换订单付款状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="status">付款状态</param>
        /// <param name="fuKuanShiJian">付款时间</param>
        /// <param name="info">款项实体</param>
        /// <returns></returns>
        public int SheZhiJiFenDingDanFuKuanStatus(string dingDanId, string operatorId, EyouSoft.Model.EnumType.YlStructure.FuKuanStatus status, DateTime? fuKuanShiJian, EyouSoft.Model.YlStructure.MHangQiDingDanKuanInfo info)
        {
            if (string.IsNullOrEmpty(dingDanId) || string.IsNullOrEmpty(operatorId)) return 0;
            if (info == null) return 0;
            if (!fuKuanShiJian.HasValue) fuKuanShiJian = DateTime.Now;
            int dalRetCode = dal.SheZhiJiFenDingDanFuKuanStatus(dingDanId, operatorId, status, fuKuanShiJian, info);
            if (status == Model.EnumType.YlStructure.FuKuanStatus.已付款 && dalRetCode == 1)
            {
                var dingdan = dal.GetJiFenDingDanInfo(dingDanId);
                if (dingdan != null)
                {
                    var m = new BLL.YlStructure.BHuiYuan().GetHuiYuanInfo(dingdan.XiaDanRenId);
                    if (m != null && !string.IsNullOrEmpty(m.ShouJi))
                    {
                        var content = "尊敬的会员" + m.XingMing + "：您在积分商城成功兑换" + dingdan.ShangPinMingCheng + "，该商品将在5个工作日内为您寄出，请保持手机畅通！服务热线：400-665-9099【维诗达游轮网】";
                        EyouSoft.Toolkit.Utils.ProxySMS(m.ShouJi, content);
                    }
                }
            }
            return dalRetCode;
        }

        /// <summary>
        /// 获取积分订单编号
        /// </summary>
        /// <param name="identityId">订单自增编号</param>
        /// <returns></returns>
        public string GetDingDanId(int identityId)
        {
            if (identityId < 1) return string.Empty;

            return dal.GetDingDanId(identityId);
        }
        #endregion
    }
}
