//游轮基础信息 汪奇志 2014-03-24
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.YlStructure
{
    /// <summary>
    /// 游轮基础信息
    /// </summary>
    public class BJiChuXinXi : BLLBase
    {
        readonly EyouSoft.IDAL.YlStructure.IJiChuXinXi dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.YlStructure.IJiChuXinXi>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BJiChuXinXi() { }
        #endregion

        #region private members

        #endregion

        #region public members
        /// <summary>
        /// 写入基础信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertJiChuXinXi(EyouSoft.Model.YlStructure.MJiChuXinXiInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.OperatorId) 
                || string.IsNullOrEmpty(info.MingCheng)) return 0;

            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.JiChuXinXi_CU(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("写入基础信息，信息编号：" + info.XinXiId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 修改基础信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateJiChuXinXi(EyouSoft.Model.YlStructure.MJiChuXinXiInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CompanyId)
                || string.IsNullOrEmpty(info.OperatorId)
                || string.IsNullOrEmpty(info.MingCheng)
                || info.XinXiId < 1) return 0;

            int dalRetCode = dal.JiChuXinXi_CU(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("修改基础信息，信息编号：" + info.XinXiId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取基础信息
        /// </summary>
        /// <param name="jiChuXinXiId">基础信息编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MJiChuXinXiInfo GetJiChuXinXiInfo(int jiChuXinXiId)
        {
            return dal.GetJiChuXinXiInfo(jiChuXinXiId);
        }

        /// <summary>
        /// 删除基础信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="jiChuXinXiId">基础信息编号</param>
        /// <returns></returns>
        public int DeleteJiChuXinXi(string companyId, int jiChuXinXiId)
        {
            if (string.IsNullOrEmpty(companyId) || jiChuXinXiId < 1) return 0;
            int dalRetCode = dal.DeleteJiChuXinXi(companyId, jiChuXinXiId);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("删除基础信息，信息编号：" + jiChuXinXiId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取基础信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MJiChuXinXiInfo> GetJiChuXinXis(string companyId, EyouSoft.Model.YlStructure.MJiChuXinXiChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId) 
                || chaXun == null) return null;

            return dal.GetJiChuXinXis(companyId, chaXun);
        }


        /// <summary>
        /// 写入游轮公司信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertGongSi(EyouSoft.Model.YlStructure.MGongSiInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.OperatorId)) return 0;

            info.GongSiId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.GongSi_CU(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("新增游轮公司，公司编号：" + info.GongSiId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 修改游轮公司信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateGongSi(EyouSoft.Model.YlStructure.MGongSiInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CompanyId)
                || string.IsNullOrEmpty(info.OperatorId)
                ||string.IsNullOrEmpty(info.GongSiId)) return 0;

            int dalRetCode = dal.GongSi_CU(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("修改游轮公司，公司编号：" + info.GongSiId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取游轮公司信息
        /// </summary>
        /// <param name="gongSiId">游轮公司编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MGongSiInfo GetGongSiInfo(string gongSiId)
        {
            return dal.GetGongSiInfo(gongSiId);
        }

        /// <summary>
        /// 删除游轮公司信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gongSiId">游轮公司编号</param>
        /// <returns></returns>
        public int DeleteGongSi(string companyId, string gongSiId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(gongSiId)) return 0;
            int dalRetCode = dal.DeleteGongSi(companyId, gongSiId);
            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("删除游轮公司，公司编号：" + gongSiId);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 获取游轮公司信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MGongSiInfo> GetGongSis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MGongSiChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetGongSis(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 获取游轮公司信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MGongSiInfo> GetGongSis(string companyId, EyouSoft.Model.YlStructure.MGongSiChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetGongSis(companyId,  chaXun);
        }

        /// <summary>
        /// 写入游轮系列信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertXiLie(EyouSoft.Model.YlStructure.MXiLieInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.OperatorId) 
                || string.IsNullOrEmpty(info.GongSiId))
                return 0;

            info.IssueTime = DateTime.Now;
            info.XiLieId = Guid.NewGuid().ToString();

            int dalRetCode = dal.XiLie_CU(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("新增游轮系列信息，系列编号：" + info.XiLieId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 修改游轮系列信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateXiLie(EyouSoft.Model.YlStructure.MXiLieInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CompanyId)
                || string.IsNullOrEmpty(info.OperatorId)
                || string.IsNullOrEmpty(info.GongSiId)
                || string.IsNullOrEmpty(info.XiLieId))
                return 0;

            int dalRetCode = dal.XiLie_CU(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("修改游轮系列信息，系列编号：" + info.XiLieId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取游轮系列信息
        /// </summary>
        /// <param name="xiLieId">系列编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MXiLieInfo GetXiLieInfo(string xiLieId)
        {
            return dal.GetXiLieInfo(xiLieId);
        }

        /// <summary>
        /// 删除游轮系列信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="xiLieId">系列编号</param>
        /// <returns></returns>
        public int DeleteXiLie(string companyId, string xiLieId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(xiLieId)) return 0;

            int dalRetCode = dal.DeleteXiLie(companyId, xiLieId);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("删除游轮系列信息，系列编号：" + xiLieId);
            }

            return dalRetCode;
        }


        /// <summary>
        /// 获取游轮系列信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MXiLieInfo> GetXiLies(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MXiLieChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetXiLies(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 获取游轮系列信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MXiLieInfo> GetXiLies(string companyId, EyouSoft.Model.YlStructure.MXiLieChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetXiLies(companyId, chaXun);
        }
        /// <summary>
        /// 写入游轮船只信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertChuanZhi(EyouSoft.Model.YlStructure.MChuanZhiInfo info)
        {
            if (info == null
               || string.IsNullOrEmpty(info.CompanyId)
               || string.IsNullOrEmpty(info.OperatorId)
               || string.IsNullOrEmpty(info.GongSiId)
               || string.IsNullOrEmpty(info.XiLieId))
                return 0;

            info.IssueTime = DateTime.Now;
            info.ChuanZhiId = Guid.NewGuid().ToString();

            if (info.FangXings != null && info.FangXings.Count > 0)
            {
                foreach (var item in info.FangXings)
                {
                    if (string.IsNullOrEmpty(item.FangXingId))
                        item.FangXingId = Guid.NewGuid().ToString();
                }
            }

            if (info.SheShis != null && info.SheShis.Count > 0)
            {
                foreach (var item in info.SheShis)
                {
                    if (string.IsNullOrEmpty(item.SheShiId))
                        item.SheShiId = Guid.NewGuid().ToString();
                }
            }

            if (info.MeiShis != null && info.MeiShis.Count > 0)
            {
                foreach (var item in info.MeiShis)
                {
                    if (string.IsNullOrEmpty(item.MeiShiId))
                        item.MeiShiId = Guid.NewGuid().ToString();
                }
            }


            int dalRetCode = dal.ChuanZhi_CU(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("新增游轮船只信息，船只编号：" + info.ChuanZhiId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 修改游轮船只信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateChuanZhi(EyouSoft.Model.YlStructure.MChuanZhiInfo info)
        {
            if (info == null
              || string.IsNullOrEmpty(info.CompanyId)
              || string.IsNullOrEmpty(info.OperatorId)
              || string.IsNullOrEmpty(info.GongSiId)
              || string.IsNullOrEmpty(info.XiLieId)
              || string.IsNullOrEmpty(info.ChuanZhiId))
                return 0;

            if (info.FangXings != null && info.FangXings.Count > 0)
            {
                foreach (var item in info.FangXings)
                {
                    if (string.IsNullOrEmpty(item.FangXingId))
                        item.FangXingId = Guid.NewGuid().ToString();
                }
            }

            if (info.SheShis != null && info.SheShis.Count > 0)
            {
                foreach (var item in info.SheShis)
                {
                    if (string.IsNullOrEmpty(item.SheShiId))
                        item.SheShiId = Guid.NewGuid().ToString();
                }
            }

            if (info.MeiShis != null && info.MeiShis.Count > 0)
            {
                foreach (var item in info.MeiShis)
                {
                    if (string.IsNullOrEmpty(item.MeiShiId))
                        item.MeiShiId = Guid.NewGuid().ToString();
                }
            }

            int dalRetCode = dal.ChuanZhi_CU(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("修改游轮船只信息，船只编号：" + info.ChuanZhiId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取游轮船只信息
        /// </summary>
        /// <param name="chuanZhiId">船只编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MChuanZhiInfo GetChuanZhiInfo(string chuanZhiId)
        {
            return dal.GetChuanZhiInfo(chuanZhiId);
        }

        /// <summary>
        /// 根据房型编号获取船只房型附件信息集合
        /// </summary>
        /// <param name="fangxingId">房型编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MChuanZhiFangXingFuJian> GetChuanZhiFangXingFuJian(string fangxingId)
        {
            return dal.GetChuanZhiFangXingFuJian(fangxingId);
        }

        /// <summary>
        /// 根据船只编号获取船只美食附件集合
        /// </summary>
        /// <param name="chuanZhiId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MChuanZhiMeiShiInfo> GetChuanZhiMeiShis(string chuanZhiId)
        {
            return dal.GetChuanZhiMeiShis(chuanZhiId);
        }

        /// <summary>
        /// 根据船只编号获取船只设施附件集合
        /// </summary>
        /// <param name="chuanZhiId"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MChuanZhiSheShiInfo> GetChuanZhiSheShis(string chuanZhiId)
        {
            return dal.GetChuanZhiSheShis(chuanZhiId);
        }

        /// <summary>
        /// 根据出港日期和航线编号获取船只信息
        /// </summary>
        /// <param name="companyid">公司编号</param>
        /// <param name="riqi">出港日期</param>
        /// <param name="hangxianid">航线编号</param>
        /// <returns>船只列表</returns>
        public IList<EyouSoft.Model.YlStructure.MChuanZhiInfo> GetChuanZhiInfo(string companyid, DateTime riqi, int hangxianid)
        {
            return dal.GetChuanZhiInfo(companyid, riqi, hangxianid);
        }
        /// <summary>
        /// 删除游轮船只信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="chuanZhiId">船只编号</param>
        /// <returns></returns>
        public int DeleteChuanZhi(string companyId, string chuanZhiId)
        {
            if (string.IsNullOrEmpty(chuanZhiId) || string.IsNullOrEmpty(chuanZhiId)) return 0;

            int dalRetCode = dal.DeleteChuanZhi(companyId, chuanZhiId);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("删除游轮船只信息，船只编号：" + chuanZhiId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取游轮船只信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MChuanZhiInfo> GetChuanZhis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MChuanZhiChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetChuanZhis(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 获取游轮船只信息集合
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="chaXun"></param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MChuanZhiInfo> GetChuanZhis(string companyId, EyouSoft.Model.YlStructure.MChuanZhiChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetChuanZhis(companyId, chaXun);
        }


        /*/// <summary>
        /// 写入游轮目的地信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int InsertMuDiDi(EyouSoft.Model.YlStructure.MMuDiDiInfo info);

        /// <summary>
        /// 修改游轮目的地信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        int UpdateMuDiDi(EyouSoft.Model.YlStructure.MMuDiDiInfo info);
        /// <summary>
        /// 获取游轮目的地信息
        /// </summary>
        /// <param name="muDiDiId">目的地编号</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MMuDiDiInfo GetMuDiDiInfo(string muDiDiId);
        /// <summary>
        /// 删除游轮目的地信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="muDiDiId">目的地编号</param>
        /// <returns></returns>
        int DeleteMuDiDi(string companyId, string muDiDiId);
        /// <summary>
        /// 获取游轮目的地信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        IList<EyouSoft.Model.YlStructure.MMuDiDiInfo> GetMuDiDis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MMuDiDiChaXunInfo chaXun);*/


        /// <summary>
        /// 写入游轮视频信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int InsertShiPin(EyouSoft.Model.YlStructure.MShiPinInfo info)
        {
            if (info == null 
                || string.IsNullOrEmpty(info.CompanyId) 
                || string.IsNullOrEmpty(info.OperatorId) 
                || string.IsNullOrEmpty(info.GongSiId) 
                || string.IsNullOrEmpty(info.XiLieId) 
                || string.IsNullOrEmpty(info.ChuanZhiId)) return 0;

            info.ShiPinId = Guid.NewGuid().ToString();
            info.IssueTime = DateTime.Now;

            int dalRetCode = dal.InsertShiPin(info);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("新增游轮视频信息，视频编号：" + info.ShiPinId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 修改游轮视频信息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">实体</param>
        /// <returns></returns>
        public int UpdateShiPin(EyouSoft.Model.YlStructure.MShiPinInfo info)
        {
            if (info == null
                || string.IsNullOrEmpty(info.CompanyId)
                || string.IsNullOrEmpty(info.OperatorId)
                || string.IsNullOrEmpty(info.GongSiId)
                || string.IsNullOrEmpty(info.XiLieId)
                || string.IsNullOrEmpty(info.ChuanZhiId)
                ||string.IsNullOrEmpty(info.ShiPinId)) return 0;

            int dalRetCode = dal.UpdateShiPin(info);
            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("修改游轮视频信息，视频编号：" + info.ShiPinId);
            }
            return dalRetCode;
        }

        /// <summary>
        /// 获取游轮视频信息
        /// </summary>
        /// <param name="shiPinId">视频编号</param>
        /// <returns></returns>
        public EyouSoft.Model.YlStructure.MShiPinInfo GetShiPinInfo(string shiPinId)
        {
            return dal.GetShiPinInfo(shiPinId);
        }

        /// <summary>
        /// 删除游轮视频信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="shiPinId">视频编号</param>
        /// <returns></returns>
        public int DeleteShiPin(string companyId, string shiPinId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(shiPinId)) return 0;

            int dalRetCode = dal.DeleteShiPin(companyId, shiPinId);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("删除游轮视频信息，视频编号：" + shiPinId);
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取游轮视频信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页序号</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MShiPinInfo> GetShiPins(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.YlStructure.MShiPinChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetShiPins(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 根据船只编号获取基础价格列表
        /// </summary>
        /// <param name="chuanzhiId">船只编号</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.YlStructure.MChuanZhiBasePrice> GetBasePrice(string chuanzhiId)
        {
            if (string.IsNullOrEmpty(chuanzhiId)) return null;

            return dal.GetBasePrice(chuanzhiId);
        }

        /// <summary>
        /// 根据船只编号和基础价格列表新增、修改
        /// </summary>
        /// <param name="chuanzhiId">船只编号</param>
        /// <param name="l">基础价格列表</param>
        /// <returns></returns>
        public int BasePrice(string chuanzhiId, IList<EyouSoft.Model.YlStructure.MChuanZhiBasePrice> l)
        {
            if (string.IsNullOrEmpty(chuanzhiId)) return 0;

            return dal.BasePrice(chuanzhiId, l);
        }
        /// <summary>
        /// 根据船只编号和附件类型新增、修改船只房型、美食、设施附件列表
        /// </summary>
        /// <param name="chuanzhiId">船只编号</param>
        /// <param name="t">附件类型【0：房型 1：美食 2：设施】</param>
        /// <param name="l">船只房型、美食、设施附件列表</param>
        /// <returns></returns>
        public int ChuanZhiFangXingFuJian_M(string chuanzhiId,int t, IList<EyouSoft.Model.YlStructure.MChuanZhiFangXingFuJian> l)
        {
            if (string.IsNullOrEmpty(chuanzhiId)) return 0;

            return dal.ChuanZhiFangXingFuJian_M(chuanzhiId,t, l);
        }
        #endregion
    }
}
