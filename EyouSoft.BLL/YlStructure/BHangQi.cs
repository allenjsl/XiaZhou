//航期管理相关 汪奇志 2014-03-27
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.YlStructure
{
    /// <summary>
    /// 航期管理相关
    /// </summary>
    public class BHangQi
    {
        readonly EyouSoft.IDAL.YlStructure.IHangQi dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.YlStructure.IHangQi>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BHangQi() { }
        #endregion

        #region private members

        #endregion

        #region public members
        /// <summary>
        /// 航期新增，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertHangQi(EyouSoft.Model.YlStructure.MHangQiInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.OperatorId) 
                || string.IsNullOrEmpty(info.GysId) 
                || string.IsNullOrEmpty(info.GongSiId) 
                || string.IsNullOrEmpty(info.XiLieId) 
                || string.IsNullOrEmpty(info.ChuanZhiId))
                return 0;

            info.HangQiId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            if (info.FuJiaChanPins != null && info.FuJiaChanPins.Count > 0)
            {
                foreach (var item in info.FuJiaChanPins)
                {
                    item.FuJiaChanPinId = Guid.NewGuid().ToString();
                }
            }

            if (info.XingChengs != null && info.XingChengs.Count > 0)
            {
                foreach (var item in info.XingChengs)
                {
                    item.XingChengId = Guid.NewGuid().ToString();
                }
            }

            int dalRetCode = dal.HangQi_CU(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("写入航期信息，航期编号：" + info.HangQiId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 航期修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateHangQi(EyouSoft.Model.YlStructure.MHangQiInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CompanyId)
                || string.IsNullOrEmpty(info.OperatorId)
                || string.IsNullOrEmpty(info.GysId)
                || string.IsNullOrEmpty(info.GongSiId)
                || string.IsNullOrEmpty(info.XiLieId)
                || string.IsNullOrEmpty(info.ChuanZhiId)
                || string.IsNullOrEmpty(info.HangQiId))
                return 0;

            info.IssueTime = DateTime.Now;

            if (info.FuJiaChanPins != null && info.FuJiaChanPins.Count > 0)
            {
                foreach (var item in info.FuJiaChanPins)
                {
                    if (string.IsNullOrEmpty(item.FuJiaChanPinId))
                        item.FuJiaChanPinId = Guid.NewGuid().ToString();
                }
            }

            if (info.XingChengs != null && info.XingChengs.Count > 0)
            {
                foreach (var item in info.XingChengs)
                {
                    if (string.IsNullOrEmpty(item.XingChengId))
                        item.XingChengId = Guid.NewGuid().ToString();
                }
            }

            int dalRetCode = dal.HangQi_CU(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("修改航期信息，航期编号：" + info.HangQiId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取航期信息
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MHangQiInfo GetHangQiInfo(string hangQiId)
        {
            if (string.IsNullOrEmpty(hangQiId)) return null;

            return dal.GetHangQiInfo(hangQiId);
        }

        /// <summary>
        /// 航期删除，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="hangQiId">航期编号</param>
        /// <returns></returns>
        public int DeleteHangQi(string companyId, string operatorId, string hangQiId)
        {
            if (string.IsNullOrEmpty(companyId) 
                || string.IsNullOrEmpty(operatorId) 
                || string.IsNullOrEmpty(hangQiId)) return 0;

            int dalRetCode = dal.HangQi_D(companyId, operatorId, hangQiId);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("删除航期信息，航期编号：" + hangQiId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取航期信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHangQiInfo> GetHangQis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MHangQiChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetHangQis(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /*/// <summary>
        /// 获取航期信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="top">要获取的记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHangQiInfo> GetHangQis(string companyId, int top, EyouSoft.Model.YlStructure.MHangQiChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            if (top < 1) top = 1;

            int pageSize = top;
            int pageIndex = 1;
            int recordCount = 0;

            return dal.GetHangQis(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }*/

        /// <summary>
        /// 设置航期价格
        /// </summary>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="riQiId">出港日期编号</param>
        /// <param name="items">价格信息</param>
        /// <returns></returns>
        public int SheZhiHangQiJiaGe(string operatorId, string hangQiId, string riQiId, IList<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo> items)
        {
            if (string.IsNullOrEmpty(operatorId) 
                || string.IsNullOrEmpty(hangQiId) 
                || string.IsNullOrEmpty(riQiId)) return 0;

            if (items != null && items.Count > 0)
            {
                foreach (var item in items)
                {
                    if (string.IsNullOrEmpty(item.JiaGeId)) item.JiaGeId = Guid.NewGuid().ToString();
                }
            }

            int dalRetCode = dal.SheZhiHangQiJiaGe(operatorId, hangQiId, riQiId, items);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("设置航期价格，航期编号：" + hangQiId + "，出港日期编号：" + riQiId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 设置航期出港日期
        /// </summary>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="items">出港日期信息</param>
        /// <returns></returns>
        public int SheZhiHangQiRiQi(string operatorId, string hangQiId, IList<EyouSoft.Model.YlStructure.MHangQiRiQiInfo> items)
        {
            if (string.IsNullOrEmpty(operatorId)
                || string.IsNullOrEmpty(hangQiId)
                || items == null
                || items.Count == 0) return 0;


            foreach (var item in items)
            {
                item.RiQiId = Guid.NewGuid().ToString();
            }

            int dalRetCode = dal.SheZhiHangQiRiQi(operatorId, hangQiId, items);

            if (dalRetCode == 1)
            {
                string s = string.Empty;
                string d = string.Empty;
                foreach (var item in items)
                {
                    s += item.RiQiId + ",";
                    d += item.RiQi.ToString("yyyy-MM-dd") + ",";
                }
                s = s.TrimEnd(',');
                d = d.TrimEnd(',');

                SysStructure.BSysLogHandle.Insert("设置航期出港日期，航期编号：" + hangQiId + "出港日期编号：" + s + "出港日期：" + d);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取航期价格信息集合
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="riQiId">出港日期编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo> GetHangQiJiaGes(string hangQiId, string riQiId)
        {
            if (string.IsNullOrEmpty(hangQiId) || string.IsNullOrEmpty(riQiId)) return null;

            return dal.GetHangQiJiaGes(hangQiId, riQiId);
        }

        /// <summary>
        /// 批量添加时获取航期价格信息集合
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="hangxianId">航线编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHangQiJiaGeInfo> GetHangQiJiaGes(string hangQiId,int hangxianId)
        {
            if (string.IsNullOrEmpty(hangQiId)||hangxianId<=0) return null;

            return dal.GetHangQiJiaGes(hangQiId,hangxianId);
        }

        /// <summary>
        /// 获取航期出港日期信息集合
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="d1">开始日期</param>
        /// <param name="d2">截止日期</param>
        /// <param name="isYouXiao">是否有效出港日期</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHangQiRiQiInfo> GetHangQiRiQis(string hangQiId, DateTime? d1, DateTime? d2, bool? isYouXiao)
        {
            if (string.IsNullOrEmpty(hangQiId)) return null;

            return dal.GetHangQiRiQis(hangQiId, d1, d2, isYouXiao);
        }

        /// <summary>
        /// 设置航期优惠规则
        /// </summary>
        /// <param name="operatorId">操作员编号</param>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="fangShi">多个共享优惠时的共享方式</param>
        /// <param name="items">规则集合</param>
        /// <returns></returns>
        public int SheZhiHangQiYouHuiGuiZe(string operatorId, string hangQiId, EyouSoft.Model.EnumType.YlStructure.YouHuiGongXiangFangShi fangShi, IList<EyouSoft.Model.YlStructure.MHangQiYouHuiGuiZeInfo> items)
        {
            if (string.IsNullOrEmpty(operatorId) || string.IsNullOrEmpty(hangQiId)) return 0;

            foreach (var item in items)
            {
                if (string.IsNullOrEmpty(item.GuiZeId))
                    item.GuiZeId = Guid.NewGuid().ToString();
            }

            int dalRetCode = dal.SheZhiHangQiYouHuiGuiZe(operatorId, hangQiId, fangShi, items);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("设置航期优惠规则，航期编号：" + hangQiId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取航期优惠规则信息集合
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="fangShi">多个共享优惠时的共享方式</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHangQiYouHuiGuiZeInfo> GetHangQiyouHuiGuiZes(string hangQiId, out EyouSoft.Model.EnumType.YlStructure.YouHuiGongXiangFangShi fangShi)
        {
            fangShi = EyouSoft.Model.EnumType.YlStructure.YouHuiGongXiangFangShi.同时享有;

            if (string.IsNullOrEmpty(hangQiId)) return null;

            return dal.GetHangQiyouHuiGuiZes(hangQiId, out fangShi);
        }
        /// <summary>
        /// 设置航期标签
        /// </summary>
        /// <param name="info">实体</param>
        /// <param name="fangShi">0:设置 1:取消</param>
        /// <returns></returns>
        public int SheZhiHangQiBiaoQian(EyouSoft.Model.YlStructure.MHangQiBiaoQianInfo info, int fangShi)
        {
            if (info == null
                || string.IsNullOrEmpty(info.OperatorId)
                || string.IsNullOrEmpty(info.HangQiId)) return 0;

            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.SheZhiHangQiBiaoQian(info, fangShi);

            if (dalRetCode == 1)
            {
                if (fangShi == 0) SysStructure.BSysLogHandle.Insert("设置航期标签，航期编号：" + info.HangQiId + "，标签：" + info.BiaoQian);
                else SysStructure.BSysLogHandle.Insert("取消航期标签，航期编号：" + info.HangQiId + "，标签：" + info.BiaoQian);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 写入航期浏览记录信息
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertHangQiLiuLanJiLu(EyouSoft.Model.YlStructure.MHangQiLiuLanJiLuInfo info)
        {
            if (info == null) return 0;
            info.JiLuId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            return dal.HangQiLiuLanJiLu_C(info);
        }

        /// <summary>
        /// 团购新增，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertTuanGou(EyouSoft.Model.YlStructure.MTuanGouInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.HangQiId) 
                || string.IsNullOrEmpty(info.RiQiId) 
                || string.IsNullOrEmpty(info.OperatorId))
                return 0;

            info.TuanGouId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            if (info.JieZhiShiJian == DateTime.MinValue) info.JieZhiShiJian = DateTime.Now;

            int dalRetCode = dal.TuanGou_CU(info);
            return dalRetCode;
        }

        /// <summary>
        /// 团购修改，返回1成功，其它失败
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdateTuanGou(EyouSoft.Model.YlStructure.MTuanGouInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.HangQiId)
                || string.IsNullOrEmpty(info.RiQiId)
                || string.IsNullOrEmpty(info.OperatorId)
                || string.IsNullOrEmpty(info.TuanGouId))
                return 0;

            info.IssueTime = DateTime.Now;

            if (info.JieZhiShiJian == DateTime.MinValue) info.JieZhiShiJian = DateTime.Now;

            int dalRetCode = dal.TuanGou_CU(info);
            return dalRetCode;
        }

        /// <summary>
        /// 删除团购信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="tuanGouId">团购编号</param>
        /// <returns></returns>
        public int DeleteTuanGou(string companyId, string tuanGouId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(tuanGouId))
                return 0;
            int dalRetCode = dal.TuanGou_D(companyId, tuanGouId);

            return dalRetCode;
        }

        /// <summary>
        /// 获取团购信息
        /// </summary>
        /// <param name="tuanGouId">团购编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MTuanGouInfo GetTuanGouInfo(string tuanGouId)
        {
            if (string.IsNullOrEmpty(tuanGouId))
                return null;

            return dal.GetTuanGouInfo(tuanGouId);
        }

        /// <summary>
        /// 获取团购集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MTuanGouInfo> GetTuanGous(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MTuanGouChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;
            return dal.GetTuanGous(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 获取关联航期集合
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MGuanLianHangQiInfo> GetGuanLianHangQis(string hangQiId)
        {
            if (string.IsNullOrEmpty(hangQiId)) return null;

            return dal.GetGuanLianHangQis(hangQiId);
        }

        /// <summary>
        /// 设置出港日期预控人数
        /// </summary>
        /// <param name="riQiId">出港日期编号</param>
        /// <param name="renShu">人数</param>
        /// <param name="operatorId">操作员编号</param>
        /// <returns></returns>
        public int ShenZhiHangQiRiQiRenShu(string riQiId, int renShu,string operatorId)
        {
            if (string.IsNullOrEmpty(riQiId)) return 0;

            int dalRetCode = dal.ShenZhiHangQiRiQiRenShu(riQiId, renShu, operatorId);

            return dalRetCode;
        }

        /// <summary>
        /// 获取出港日期信息
        /// </summary>
        /// <param name="riQiId">日期编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MHangQiRiQiInfo GetRiQiInfo(string riQiId)
        {
            return dal.GetRiQiInfo(riQiId);
        }

        /// <summary>
        /// 获取海洋邮轮价格集合
        /// </summary>
        /// <param name="hangQiId"></param>
        /// <param name="riQiId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MHYJiaGeInfo1> GetHYJiaGes(string hangQiId, string riQiId)
        {
            var items = GetHangQiJiaGes(hangQiId, riQiId);
            if (items == null) return null;

            var items1 = new List<EyouSoft.Model.YlStructure.MHYJiaGeInfo1>();

            foreach (var item in items)
            {
                var item1 = items1.FindLast(tmp =>
                {
                    if (tmp.FangXingId == item.FangXingId
                        && tmp.LouCeng == item.LouCeng) return true;
                    return false;
                });

                item1 = item1 ?? new EyouSoft.Model.YlStructure.MHYJiaGeInfo1();

                if (item1.FangXingId == 0)
                {
                    item1.FangCha = item.FangCha;
                    item1.FangXingId = item.FangXingId;
                    item1.JiaGes = new List<EyouSoft.Model.YlStructure.MHYJiaGeInfo2>();
                    item1.LouCeng = item.LouCeng;
                    item1.RongNaRenShu = item.RongNaRenShu;
                    item1.ShuoMing = item.ShuoMing;

                    item1.JiaGes.Add(new EyouSoft.Model.YlStructure.MHYJiaGeInfo2()
                    {
                        BinKeLeiXingId = item.BinKeLeiXingId,
                        JiaGe = item.JiaGe1
                    });

                    items1.Add(item1);
                }
                else
                {
                    item1.ShuoMing = item1.ShuoMing + item.ShuoMing;
                    item1.JiaGes.Add(new EyouSoft.Model.YlStructure.MHYJiaGeInfo2()
                    {
                        BinKeLeiXingId = item.BinKeLeiXingId,
                        JiaGe = item.JiaGe1
                    });
                }
            }

            return items1;
        }

        /// <summary>
        /// 取消航期日期，返回1成功，其它失败
        /// </summary>
        /// <param name="hangQiId">航期编号</param>
        /// <param name="riQiId">日期编号</param>
        /// <param name="operatorId">操作员编号</param>
        /// <returns></returns>
        public int QuXiaoHangQiRiQi(string hangQiId, string riQiId, string operatorId,DateTime riQi)
        {
            if (string.IsNullOrEmpty(hangQiId) || string.IsNullOrEmpty(riQiId) || string.IsNullOrEmpty(operatorId)) return 0;

            int dalRetCode = dal.QuXiaoHangQiRiQi(hangQiId, riQiId, operatorId);

            SysStructure.BSysLogHandle.Insert("取消航期出港日期，航期编号：" + hangQiId + "出港日期编号：" + riQiId + "出港日期：" + riQi.ToString("yyyy-MM-dd"));

            return dalRetCode;
        }
        #endregion
    }
}
