//航期订单相关 汪奇志 2014-03-30
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.YlStructure
{
    /// <summary>
    /// 航期订单相关
    /// </summary>
    public class BHangQiDingDan
    {
        readonly EyouSoft.IDAL.YlStructure.IHangQiDingDan dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.YlStructure.IHangQiDingDan>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BHangQiDingDan() { }
        #endregion

        #region private members

        #endregion

        #region public members
        /// <summary>
        /// 写入航期订单信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertDingDan(EyouSoft.Model.YlStructure.MHangQiDingDanInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.HangQiId) 
                || string.IsNullOrEmpty(info.RiQiId) 
                || string.IsNullOrEmpty(info.XiaDanRenId))
                return 0;

            info.DingDanId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            if (info.LiuWeiDaoQiShiJian == DateTime.MinValue) info.LiuWeiDaoQiShiJian = DateTime.Now;

            if (info.YouKes != null && info.YouKes.Count > 0)
            {
                foreach (var item in info.YouKes)
                {
                    item.YouKeId = Guid.NewGuid().ToString();
                }
            }

            info.DingDanStatus = EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus.成交;

            int dalRetCode = dal.DingDan_C(info);
            if (dalRetCode == 1)
            {
                info = dal.GetDingDanInfo(info.DingDanId);
                var m = new BLL.YlStructure.BHuiYuan().GetHuiYuanInfo(info.XiaDanRenId);
                if (!string.IsNullOrEmpty(m.ShouJi))
                {
                    var r = new BLL.YlStructure.BHangQi().GetRiQiInfo(info.RiQiId);
                    var content = "尊敬的会员" + m.XingMing + "：您预订的" + info.MingCheng + "产品 出发日期：" + (r != null ? r.RiQi.ToShortDateString() : string.Empty) + "，预订人数：" + info.RenShu + "，总价格：" + info.JinE.ToString("F2");
                    switch (info.DingDanStatus)
                    {
                        case EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus.未处理:
                            //订单提交成功短信提醒预订人
                            EyouSoft.Toolkit.Utils.ProxySMS(m.ShouJi, content + "已经提交成功，请您耐心等待审核！我们工作人员会随后联系您，服务热线：400-665-9099【维诗达游轮网】");
                            break;
                        case EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus.成交:
                            EyouSoft.Toolkit.Utils.ProxySMS(m.ShouJi, content + "已经审核成功，请您进入订单中心进行支付！服务热线：400-665-9099【维诗达游轮网】");
                            break;
                    }
                }
            }

            return dalRetCode;
        }

        /// <summary>
        /// 修改航期订单信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns>-100：失败 -94：该订单已付款 1：成功</returns>
        public int UpdateDingDan(EyouSoft.Model.YlStructure.MHangQiDingDanInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.DingDanId))
                return 0;
            info.IssueTime = DateTime.Now;
            return dal.DingDan_M(info);
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MHangQiDingDanInfo GetDingDanInfo(string dingDanId)
        {
            if (string.IsNullOrEmpty(dingDanId)) return null;

            return dal.GetDingDanInfo(dingDanId);
        }

        /// <summary>
        /// 获取订单信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHangQiDingDanInfo> GetDingDans(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHangQiDingDanChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetDingDans(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 设置航期订单状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="status">订单状态</param>
        /// <param name="liuWeiDaoQiShiJian">留位到期时间</param>
        /// <returns></returns>
        public int SheZhiDingDanStatus(string dingDanId, string operatorId, EyouSoft.Model.EnumType.YlStructure.HangQiDingDanStatus status, DateTime? liuWeiDaoQiShiJian)
        {
            if (string.IsNullOrEmpty(dingDanId) || string.IsNullOrEmpty(operatorId)) return 0;
            if (!liuWeiDaoQiShiJian.HasValue) liuWeiDaoQiShiJian = DateTime.Now;

            int dalRetCode = dal.SheZhiDingDanStatus(dingDanId, operatorId, status, liuWeiDaoQiShiJian);
            if (status == Model.EnumType.YlStructure.HangQiDingDanStatus.成交 && dalRetCode == 1)
            {
                var dingdan = dal.GetDingDanInfo(dingDanId);
                if (dingdan != null)
                {
                    var m = new BLL.YlStructure.BHuiYuan().GetHuiYuanInfo(dingdan.XiaDanRenId);
                    if (m != null && !string.IsNullOrEmpty(m.ShouJi))
                    {
                        var content = "尊敬的会员" + m.XingMing + "：您预订的" + dingdan.MingCheng + "产品 出发日期：" + dingdan.RiQi.ToShortDateString() + "，预订人数：" + dingdan.RenShu + "，总价格：" + dingdan.JinE.ToString("F2") + "已经审核成功，请您进入订单中心进行支付！服务热线：400-665-9099【维诗达游轮网】";
                        EyouSoft.Toolkit.Utils.ProxySMS(m.ShouJi, content);
                    }
                }
            }
            return dalRetCode;
        }

        /// <summary>
        /// 设置付款状态，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="status">付款状态</param>
        /// <param name="fuKuanShiJian">付款时间</param>
        /// <param name="info">款项实体</param>
        /// <returns></returns>
        public int SheZhiFuKuanStatus(string dingDanId, string operatorId, EyouSoft.Model.EnumType.YlStructure.FuKuanStatus status, DateTime? fuKuanShiJian, EyouSoft.Model.YlStructure.MHangQiDingDanKuanInfo info)
        {
            if (string.IsNullOrEmpty(dingDanId) || string.IsNullOrEmpty(operatorId)) return 0;
            if (info == null) return 0;
            if (!fuKuanShiJian.HasValue) fuKuanShiJian = DateTime.Now;
            info.FuKuanId = Guid.NewGuid().ToString();
            int dalRetCode = dal.SheZhiFuKuanStatus(dingDanId, operatorId, status, fuKuanShiJian, info);
            if (status == Model.EnumType.YlStructure.FuKuanStatus.已付款 && dalRetCode == 1)
            {
                var dingdan = dal.GetDingDanInfo(dingDanId);
                if (dingdan != null)
                {
                    var m = new BLL.YlStructure.BHuiYuan().GetHuiYuanInfo(dingdan.XiaDanRenId);
                    if (m != null && !string.IsNullOrEmpty(m.ShouJi))
                    {
                        var content = "尊敬的会员" + m.XingMing + "：您预订的" + dingdan.MingCheng + "产品 出发日期：" + dingdan.RiQi.ToShortDateString() + "，预订人数：" + dingdan.RenShu + "，总价格：" + dingdan.JinE.ToString("F2") + "已经支付成功。请在" + dingdan.RiQi.ToShortDateString() + "当天登船出游，祝您旅途愉快！服务热线：400-665-9099【维诗达游轮网】";
                        EyouSoft.Toolkit.Utils.ProxySMS(m.ShouJi, content);
                    }
                }
            }
            return dalRetCode;
        }

        /// <summary>
        /// 获取航期订单编号
        /// </summary>
        /// <param name="identityId">订单自增编号</param>
        /// <returns></returns>
        public string GetDingDanId(int identityId)
        {
            if (identityId < 1) return string.Empty;

            return dal.GetDingDanId(identityId);
        }

        /// <summary>
        /// 更新订单游客信息，返回1成功，其它失败
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="youKes">游客集合</param>
        /// <returns></returns>
        public int UpdateDingDanYouKes(string dingDanId, IList<EyouSoft.Model.YlStructure.MHangQiDingDanYouKeInfo> youKes)
        {
            if(string.IsNullOrEmpty(dingDanId)||youKes==null||youKes.Count==0) return 0;

            int dalRetCode = dal.UpdateDingDanYouKes(dingDanId, youKes);

            return dalRetCode;
        }
        /// <summary>
        /// 更新订单积分，返回1成功，其它失败
        /// </summary>
        /// <param name="dingdanId">订单编号</param>
        /// <param name="dingdanJifen">订单积分</param>
        /// <returns>1：成功 -100：失败 -99：会员可用积分小于0</returns>
        public int UpDateDingDanJiFen(string dingdanId, decimal dingdanJifen)
        {
            if (string.IsNullOrEmpty(dingdanId)) return 0;

            int dalRetCode = dal.UpDateDingDanJiFen(dingdanId, dingdanJifen);

            return dalRetCode;
        }
                /// <summary>
        /// 更新订单操作备注
        /// </summary>
        /// <param name="dingdanid">订单编号</param>
        /// <param name="caozuobeizhu">操作备注</param>
        /// <returns>1：成功 0：失败</returns>
        public int UpdDingDanCaoZuoBeiZhu(string dingdanid, string caozuobeizhu)
        {
            if (string.IsNullOrEmpty(dingdanid)) return 0;
            return dal.UpdDingDanCaoZuoBeiZhu(dingdanid, caozuobeizhu);
        }
        #endregion
    }
}
