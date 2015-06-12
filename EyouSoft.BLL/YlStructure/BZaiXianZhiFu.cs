//YL-在线支付 汪奇志 2014-04-19
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.YlStructure
{
    /// <summary>
    /// YL-在线支付
    /// </summary>
    public class BZaiXianZhiFu : BLLBase
    {
        readonly EyouSoft.IDAL.YlStructure.IZaiXianZhiFu dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.YlStructure.IZaiXianZhiFu>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BZaiXianZhiFu() { }
        #endregion

        #region public members
        /// <summary>
        /// 写入在线支付信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int Insert(EyouSoft.Model.YlStructure.MZaiXianZhiFuInfo info)
        {
            if (info == null) return 0;

            return dal.Insert(info);
        }

        /// <summary>
        /// 获取订单支付状态，返回真已支付，返回假未支付
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <param name="dingDanLeiXing">订单类型</param>
        /// <returns></returns>
        public bool IsZhiFu(string dingDanId, EyouSoft.Model.EnumType.YlStructure.DingDanLeiXing dingDanLeiXing)
        {
            if (string.IsNullOrEmpty(dingDanId)) return false;

            return dal.IsZhiFu(dingDanId, dingDanLeiXing);
        }

        /// <summary>
        /// 根据订单编号获取在线支付信息
        /// </summary>
        /// <param name="dingDanId">订单编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MZaiXianZhiFuInfo GetInfo(string dingDanId)
        {
            if (string.IsNullOrEmpty(dingDanId)) return null;
            return dal.GetInfo(dingDanId);
        }
        #endregion
    }
}
