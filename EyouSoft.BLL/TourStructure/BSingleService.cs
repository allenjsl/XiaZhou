using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.TourStructure;

namespace EyouSoft.BLL.TourStructure
{
    /// <summary>
    /// 描述：单项业务业务层
    /// 修改记录：
    /// 1、2011-09-05 PM 王磊 创建
    /// </summary>
    public class BSingleService : EyouSoft.BLL.BLLBase
    {

        private readonly EyouSoft.IDAL.TourStructure.ISingleService dal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.TourStructure.ISingleService>();

        #region ISingleService 成员

        /// <summary>
        /// 添加单项业务
        /// </summary>
        /// <param name="single"></param>
        /// <returns>True or False</returns>
        public bool AddSingleService(MSingleServiceExtend single)
        {
            BTour tour = new BTour();

            var temp = tour.GenerateTourNo(single.OperatorDeptId, single.CompanyId, EyouSoft.Model.EnumType.TourStructure.TourType.单项服务, single.WeiTuoRiQi);

            single.TourCode = temp[0];
            single.XuHao = EyouSoft.Toolkit.Utils.GetInt(temp[1]);

            single.OrderCode = single.TourCode + "-1";

            single.TourId = Guid.NewGuid().ToString();
            single.OrderId = Guid.NewGuid().ToString();

            int flg = dal.AddSingleService(single);
            if (flg == 1)
            {
                StringBuilder query = new StringBuilder();
                query.AppendFormat("添加单项业务,订单编号：{0}。", single.OrderId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(query.ToString());
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除单项业务，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="tourId">业务编号</param>
        /// <returns></returns>
        public int Delete(string companyId, string tourId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(tourId)) return 0;

            int dalRetCode = dal.Delete(companyId, tourId);

            if (dalRetCode == 1)
            {
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert("删除单项业务，业务编号：" + tourId + "。");
            }

            return dalRetCode;
        }

        /// <summary>
        /// 修改单项业务
        /// </summary>
        /// <param name="single"></param>
        /// <returns>True or False</returns>
        public bool UpdateSingleService(MSingleServiceExtend single)
        {
            int flg = dal.UpdateSingleService(single);
            if (flg == 1)
            {
                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.AppendFormat("修改了单项业务,订单编号：{0}。", single.OrderId);
                EyouSoft.BLL.SysStructure.BSysLogHandle.Insert(str.ToString());
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据计划编号获取单项业务的拓展实体
        /// </summary>
        /// <param name="tourId"></param>
        /// <returns></returns>
        public MSingleServiceExtend GetSingleServiceExtendByTourId(string tourId)
        {
            return dal.GetSingleServiceExtendByTourId(tourId);
        }


        /// <summary>
        /// 查询获取单项业务的列表
        /// </summary>
        /// <param name="search">查询的实体类</param>
        /// <param name="pagesize">每页显示的条数</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<MSingleService> GetSingleServiceList(MSeachSingleService search,
            int pagesize,
            int pageindex,
            ref int recordCount)
        {
            //权限控制
            bool _isOnlySelf;

            if (search == null || string.IsNullOrEmpty(search.CompanyId))
            {
                throw new System.Exception("bll error:查询实体为null或string.IsNullOrEmpty(查询实体.CompanyId)==true。");
            }

            int[] deptIds = this.GetDataPrivs(EyouSoft.Model.EnumType.PrivsStructure.Menu2.单项业务_单项业务, out _isOnlySelf);
          
            return dal.GetSingleServiceList(search, pagesize, pageindex, ref recordCount, this.LoginUserId, deptIds,_isOnlySelf);
        }

        /// <summary>
        /// 取消单项业务，返回1成功，其它失败
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="tourId">计划编号</param>
        /// <param name="yuanYin">取消原因</param>
        /// <param name="caoZuoRenId">操作人编号</param>
        /// <returns></returns>
        public int QuXiao(string companyId, string tourId, string yuanYin, string caoZuoRenId)
        {
            if (string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(tourId)) return 0;

            int dalRetCode = dal.QuXiao(companyId, tourId, yuanYin, caoZuoRenId);

            if (dalRetCode == 1)
            {
                SysStructure.BSysLogHandle.Insert("取消单项业务，业务编号：" + tourId + "。");
            }

            return dalRetCode;
        }

        #endregion
    }
}
