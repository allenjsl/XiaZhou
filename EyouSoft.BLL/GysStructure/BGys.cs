//供应商相关业务逻辑类 汪奇志 2013-04-25
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.GysStructure
{
    /// <summary>
    /// 供应商相关业务逻辑类
    /// </summary>
    public class BGys: BLLBase
    {
        readonly EyouSoft.IDAL.GysStructure.IGys dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.GysStructure.IGys>();

        #region constructure
        /// <summary>
        /// default constructure
        /// </summary>
        public BGys() { }
        #endregion

        #region private members

        #endregion

        #region public members
        /// <summary>
        /// 获取供应商选用信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MXuanYongInfo> GetXuanYongs(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MXuanYongChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetXuanYongs(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 获取景点选用信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MXuanYongJingDianInfo> GetXuanYongJingDians(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MXuanYongChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            return dal.GetXuanYongJingDians(companyId, pageSize, pageIndex, ref recordCount, chaXun);
        }

        /// <summary>
        /// 获取供应商账号信息
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        public EyouSoft.Model.GysStructure.MGysUserInfo GetGysUserInfo(string gysId)
        {
            if (string.IsNullOrEmpty(gysId)) return null;

            return dal.GetGysUserInfo(gysId);
        }

        /// <summary>
        /// 获取供应商-地接社列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MLBDiJieSheInfo> GetDiJieShes(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            bool isOnlySelf = false;
            int[] depts = null;

            depts = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_地接社, out  isOnlySelf);

            var items = dal.GetDiJieShes(companyId, pageSize, pageIndex, ref recordCount, chaXun, isOnlySelf, LoginUserId, depts);

            if (items != null && items.Count > 0)
            {
                var citybll = new EyouSoft.BLL.ComStructure.BComCity();
                foreach (var item in items)
                {
                    item.CPCD = citybll.GetCPCD(companyId, item.CPCD.CountryId, item.CPCD.ProvinceId, item.CPCD.CityId, item.CPCD.DistrictId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商-酒店列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MLBJiuDianInfo> GetJiuDians(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            bool isOnlySelf = false;
            int[] depts = null;

            depts = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_酒店, out  isOnlySelf);

            var items = dal.GetJiuDians(companyId, pageSize, pageIndex, ref recordCount, chaXun, isOnlySelf, LoginUserId, depts);

            if (items != null && items.Count > 0)
            {
                var citybll = new EyouSoft.BLL.ComStructure.BComCity();
                foreach (var item in items)
                {
                    item.CPCD = citybll.GetCPCD(companyId, item.CPCD.CountryId, item.CPCD.ProvinceId, item.CPCD.CityId, item.CPCD.DistrictId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商-餐馆列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MLBCanGuanInfo> GetCanGuans(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            bool isOnlySelf = false;
            int[] depts = null;

            depts = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_餐馆, out  isOnlySelf);

            var items = dal.GetCanGuans(companyId, pageSize, pageIndex, ref recordCount, chaXun, isOnlySelf, LoginUserId, depts);

            if (items != null && items.Count > 0)
            {
                var citybll = new EyouSoft.BLL.ComStructure.BComCity();
                foreach (var item in items)
                {
                    item.CPCD = citybll.GetCPCD(companyId, item.CPCD.CountryId, item.CPCD.ProvinceId, item.CPCD.CityId, item.CPCD.DistrictId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商-景点列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MLBJingDianInfo> GetJingDians(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            bool isOnlySelf = false;
            int[] depts = null;

            depts = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_景点, out  isOnlySelf);

            var items = dal.GetJingDians(companyId, pageSize, pageIndex, ref recordCount, chaXun, isOnlySelf, LoginUserId, depts);

            if (items != null && items.Count > 0)
            {
                var citybll = new EyouSoft.BLL.ComStructure.BComCity();
                foreach (var item in items)
                {
                    item.CPCD = citybll.GetCPCD(companyId, item.CPCD.CountryId, item.CPCD.ProvinceId, item.CPCD.CityId, item.CPCD.DistrictId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商-游轮列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MLBYouLunInfo> GetYouLuns(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            bool isOnlySelf = false;
            int[] depts = null;

            depts = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_游轮, out  isOnlySelf);

            var items = dal.GetYouLuns(companyId, pageSize, pageIndex, ref recordCount, chaXun, isOnlySelf, LoginUserId, depts);

            if (items != null && items.Count > 0)
            {
                var citybll = new EyouSoft.BLL.ComStructure.BComCity();
                foreach (var item in items)
                {
                    item.CPCD = citybll.GetCPCD(companyId, item.CPCD.CountryId, item.CPCD.ProvinceId, item.CPCD.CityId, item.CPCD.DistrictId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商-车队列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MLBCheDuiInfo> GetCheDuis(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            bool isOnlySelf = false;
            int[] depts = null;

            depts = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_车队, out  isOnlySelf);

            var items = dal.GetCheDuis(companyId, pageSize, pageIndex, ref recordCount, chaXun, isOnlySelf, LoginUserId, depts);

            if (items != null && items.Count > 0)
            {
                var citybll = new EyouSoft.BLL.ComStructure.BComCity();
                foreach (var item in items)
                {
                    item.CPCD = citybll.GetCPCD(companyId, item.CPCD.CountryId, item.CPCD.ProvinceId, item.CPCD.CityId, item.CPCD.DistrictId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商-票务列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MLBPiaoWuInfo> GetPiaoWus(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            bool isOnlySelf = false;
            int[] depts = null;

            depts = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_票务, out  isOnlySelf);

            var items = dal.GetPiaoWus(companyId, pageSize, pageIndex, ref recordCount, chaXun, isOnlySelf, LoginUserId, depts);

            if (items != null && items.Count > 0)
            {
                var citybll = new EyouSoft.BLL.ComStructure.BComCity();
                foreach (var item in items)
                {
                    item.CPCD = citybll.GetCPCD(companyId, item.CPCD.CountryId, item.CPCD.ProvinceId, item.CPCD.CityId, item.CPCD.DistrictId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商-购物列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MLBGouWuInfo> GetGouWus(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            bool isOnlySelf = false;
            int[] depts = null;

            depts = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_购物, out  isOnlySelf);

            var items = dal.GetGouWus(companyId, pageSize, pageIndex, ref recordCount, chaXun, isOnlySelf, LoginUserId, depts);

            if (items != null && items.Count > 0)
            {
                var citybll = new EyouSoft.BLL.ComStructure.BComCity();
                foreach (var item in items)
                {
                    item.CPCD = citybll.GetCPCD(companyId, item.CPCD.CountryId, item.CPCD.ProvinceId, item.CPCD.CityId, item.CPCD.DistrictId);
                }
            }

            return items;
        }

        /// <summary>
        /// 获取供应商-其他列表信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MLBQiTaInfo> GetQiTas(string companyId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MLBChaXunInfo chaXun)
        {
            if (string.IsNullOrEmpty(companyId)) return null;

            bool isOnlySelf = false;
            int[] depts = null;

            depts = GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_其它, out  isOnlySelf);

            var items = dal.GetQiTas(companyId, pageSize, pageIndex, ref recordCount, chaXun, isOnlySelf, LoginUserId, depts);

            if (items != null && items.Count > 0)
            {
                var citybll = new EyouSoft.BLL.ComStructure.BComCity();
                foreach (var item in items)
                {
                    item.CPCD = citybll.GetCPCD(companyId, item.CPCD.CountryId, item.CPCD.ProvinceId, item.CPCD.CityId, item.CPCD.DistrictId);
                }
            }

            return items;
        }

        /// <summary>
        /// 删除供应商信息，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="gysLeiXing">供应商类型</param>
        /// <returns></returns>
        public int Delete(string companyId, string gysId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(gysId)) return 0;

            int dalRetCode = dal.Delete(companyId, gysId);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("删除资源信息，资源编号：" + gysId + "。");
            }

            return dalRetCode;
        }

        /// <summary>
        /// 获取供应商类型
        /// </summary>
        /// <param name="gysId">供应商编号</param>
        /// <returns></returns>
        public EyouSoft.Model.EnumType.SourceStructure.SourceType? GetGysLeiXing(string gysId)
        {
            if (string.IsNullOrEmpty(gysId)) return null;

            return dal.GetGysLeiXing(gysId);
        }

        /// <summary>
        /// 获取供应商交易明细信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="gysId">供应商编号</param>
        /// <param name="pageSize">页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="chaXun">查询实体</param>
        /// <param name="heJi">合计信息[0:数量:int][1:数量:decimal][2:结算金额:decimal][3:已支付金额:decimal]</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.GysStructure.MJiaoYiMingXiInfo> GetJiaoYiMingXis(string companyId, string gysId, int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.GysStructure.MJiaoYiMingXiChaXunInfo chaXun, out object[] heJi)
        {
            heJi = new object[] { 0, 0M, 0M, 0M };
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(gysId)) return null;

            var items = dal.GetJiaoYiMingXis(companyId, gysId, pageSize, pageIndex, ref recordCount, chaXun, out heJi);

            return items;
        }
        #endregion
    }
}
