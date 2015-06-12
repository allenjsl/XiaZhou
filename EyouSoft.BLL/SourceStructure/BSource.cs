using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EyouSoft.BLL.SourceStructure
{
    using EyouSoft.Model.ComStructure;
    using EyouSoft.Model.SourceStructure;
    using EyouSoft.Model.TourStructure;

    /// <summary>
    /// 供应商(包括,地接社,餐馆,导游,酒店,保险,车队,其他,游轮,购物,景点,票务,线路)
    /// 创建者 钱琦 时间:2011-9-6
    /// 增加导游上团统计，导游排班 邵权江 2011-10-08
    /// </summary>
    public class BSource : BLLBase
    {

        #region dal对象
        EyouSoft.IDAL.SourceStructure.ISource sourceDal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SourceStructure.ISource>();
        EyouSoft.IDAL.SourceStructure.ISourceDining diningDal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SourceStructure.ISourceDining>();
        EyouSoft.IDAL.SourceStructure.ISourceGuide guideDal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SourceStructure.ISourceGuide>();
        EyouSoft.IDAL.SourceStructure.ISourceHotel hotelDal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SourceStructure.ISourceHotel>();
        EyouSoft.IDAL.SourceStructure.ISourceMotorcade motorcadeDal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SourceStructure.ISourceMotorcade>();
        EyouSoft.IDAL.SourceStructure.ISourceOther otherDal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SourceStructure.ISourceOther>();
        EyouSoft.IDAL.SourceStructure.ISourceShip shipDal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SourceStructure.ISourceShip>();
        EyouSoft.IDAL.SourceStructure.ISourceShop shopDal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SourceStructure.ISourceShop>();
        EyouSoft.IDAL.SourceStructure.ISourceSpot spotDal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SourceStructure.ISourceSpot>();
        EyouSoft.IDAL.SourceStructure.ISourceTicket ticketDal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SourceStructure.ISourceTicket>();
        EyouSoft.IDAL.SourceStructure.IRoute routeDal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.SourceStructure.IRoute>();
        EyouSoft.Toolkit.DataProtection.HashCrypto hc = new EyouSoft.Toolkit.DataProtection.HashCrypto();
        #endregion

        #region 地接社
        /// <summary>
        /// 添加地接社信息
        /// </summary>
        /// <param name="model">供应商Model</param>
        /// <returns>返回值 -1:存在相同地接社名称 -2:存在相同的用户名 -3:地接社信息不完善 -4:地接社必填信息不完善 0:事务回滚 1:正确</returns>
        public int AddTravelServiceModel(Model.SourceStructure.MSourceTravel model)
        {
            if (string.IsNullOrEmpty(model.SourceModel.CompanyId))
            {
                return -3;
            }
            if (string.IsNullOrEmpty(model.SourceModel.SourceId))
            {
                model.SourceModel.SourceId = Guid.NewGuid().ToString();
            }
            if (string.IsNullOrEmpty(model.SourceModel.OperatorId))
            {
                return -3;
            }
            if (string.IsNullOrEmpty(model.SourceModel.Name))
            {
                return -4;
            }


            if (string.IsNullOrEmpty(model.SourceModel.OperatorId))
            {
                return -4;
            }

            model.SourceModel.IsDelete = false;

            model.SourceModel.IssueTime = DateTime.Now;



            for (int i = 0; i < model.LinkManList.Count; i++)
            {
                model.LinkManList[i].Id = Guid.NewGuid().ToString();
                model.LinkManList[i].TypeId = model.SourceModel.SourceId;
                model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
                model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
                model.LinkManList[i].IssueTime = DateTime.Now;
            }
            model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.地接社;
            if (model.SourceModel.ContractAttach != null)
            {
                model.SourceModel.ContractAttach.ItemId = model.SourceModel.SourceId;
                model.SourceModel.ContractAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
            }

            model.SourceTravelModel.SourceId = model.SourceModel.SourceId;
            int result = sourceDal.AddTravelModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("新增资源管理地接社数据：地接社编号为：" + model.SourceModel.SourceId);
            }
            return result;

        }



        /// <summary>
        /// 修改地接社信息
        /// </summary>
        /// <param name="model">供应商Model</param>
        /// <returns>返回值 -1:地接社名称已经存在 -2:用户名已经存在 -3:地接社信息不完善 -4:地接社必填信息不完善 0:事务回滚 1:正确</returns>
        public int UpdateTravelServiceModel(Model.SourceStructure.MSourceTravel model)
        {
            if (string.IsNullOrEmpty(model.SourceModel.SourceId))
            {
                return -3;
            }
            if (string.IsNullOrEmpty(model.SourceModel.Name))
            {
                return -4;
            }


            if (string.IsNullOrEmpty(model.SourceModel.OperatorId))
            {
                return -4;
            }
            model.SourceModel.IssueTime = DateTime.Now;
            model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.地接社;
            for (int i = 0; i < model.LinkManList.Count; i++)
            {
                if (string.IsNullOrEmpty(model.LinkManList[i].Id))
                    model.LinkManList[i].Id = Guid.NewGuid().ToString();
                model.LinkManList[i].TypeId = model.SourceModel.SourceId;
                model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
                model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
                model.LinkManList[i].IssueTime = DateTime.Now;
            }
            if (model.SourceModel.ContractAttach != null)
            {
                model.SourceModel.ContractAttach.ItemId = model.SourceModel.SourceId;
                model.SourceModel.ContractAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
            }
            model.SourceTravelModel.SourceId = model.SourceModel.SourceId;
            int result = sourceDal.UpdateTravelModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("修改资源管理地接社数据：地接社编号为：" + model.SourceModel.SourceId);
            }
            return result;
        }

        /// <summary>
        /// 获得地接社Model
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MSourceTravel GetTravelServiceModel(string SourceId)
        {
            if (string.IsNullOrEmpty(SourceId))
            {
                return null;
            }
            return sourceDal.GetTravelModel(SourceId);
        }

        /// <summary>
        /// 给地接社分配账户
        /// </summary>
        /// <param name="sourceId">地接社编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="account">账户</param>
        /// <param name="md5pwd">md5密码</param>
        /// <param name="pwd">明文密码</param>
        /// <param name="operatorId">操作者编号</param>
        /// <param name="operatorNm">操作者姓名</param>
        /// <param name="operDeptId">操作者部门编号</param>
        /// <param name="routeAreaList">线路区域集合</param>
        /// <returns></returns>
        public int AddAccountToTravel(string sourceId, string companyId, string account, string pwd, string operatorId, string operatorNm, int operDeptId, IList<Model.SourceStructure.MSourceTravelRouteArea> routeAreaList)
        {
            string md5pwd = string.IsNullOrEmpty(pwd) ? string.Empty : hc.MD5Encrypt(pwd);
            int result = sourceDal.AddAccountToTravel(sourceId, companyId, account, md5pwd, pwd, operatorId, operatorNm, operDeptId, routeAreaList);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("给地接社分配账户：地接社编号为：" + sourceId + ",帐号为：" + account);
            }

            return result;
        }

        /// <summary>
        /// 修改地接社帐号
        /// </summary>
        /// <param name="sourceId">地接社编号</param>
        /// <param name="Status">帐号状态</param>
        /// <param name="pwd">明文密码</param>
        /// <param name="routeAreaList">线路区域列表</param>
        /// <param name="isUpdAccount">是否修改帐号信息 0：单纯修改状态 1：修改密码和线路区域</param>
        /// <returns>0:失败 1:成功</returns>
        public int UpdateTravelAccountStatus(string sourceId, Model.EnumType.ComStructure.UserStatus Status, string pwd, IList<Model.SourceStructure.MSourceTravelRouteArea> routeAreaList, bool isUpdAccount)
        {
            var md5pwd = string.IsNullOrEmpty(pwd) ? string.Empty : hc.MD5Encrypt(pwd);
            int result = sourceDal.UpdateTravelAccountStatus(sourceId, Status, md5pwd, pwd, routeAreaList, isUpdAccount);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("修改地接社编号为：" + sourceId + "的帐号信息");
            }
            return result;
        }



        #endregion

        #region 餐馆
        /// <summary>
        /// 添加单个供应商餐馆Model
        /// </summary>
        /// <param name="model">供应商餐馆Model</param>
        /// <returns>返回值 -1:已经存在该名称的餐馆 -2:该餐馆已经存在相同的菜系 -3:公司编号未填写 -4:餐馆必填信息不完善 0:事务回滚 1:正确</returns>
        public int AddDiningModel(Model.SourceStructure.MSourceDining model)
        {
            model.SourceId = Guid.NewGuid().ToString();
            model.SourceModel.SourceId = model.SourceId;
            if (string.IsNullOrEmpty(model.SourceModel.CompanyId))
            {
                return -3;
            }
            if (string.IsNullOrEmpty(model.SourceModel.Name))
            {
                return -4;
            }

            if (string.IsNullOrEmpty(model.SourceModel.OperatorId))
            {
                return -4;
            }
            model.IsSystem = false;
            model.SourceModel.IsDelete = false;
            model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.餐馆;
            if (model.LinkManList != null && model.LinkManList.Count > 0)
            {
                for (int i = 0; i < model.LinkManList.Count; i++)
                {
                    model.LinkManList[i].Id = Guid.NewGuid().ToString();
                    model.LinkManList[i].TypeId = model.SourceId;
                    model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
                    model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
                    model.LinkManList[i].IssueTime = DateTime.Now;
                }
            }
            for (int i = 0; i < model.DiningCuisineList.Count; i++)
            {
                model.DiningCuisineList[i].SourceId = model.SourceId;
            }
            if (model.SourceModel.ContractAttach != null)
            {
                model.SourceModel.ContractAttach.ItemId = model.SourceModel.SourceId;
                model.SourceModel.ContractAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
            }
            int result = diningDal.AddDiningModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("新增资源管理餐馆数据：餐馆编号为：" + model.SourceModel.SourceId);
            }
            return result;
        }




        /// <summary>
        /// 修改供应商餐馆Model
        /// </summary>
        /// <param name="model">供应商餐馆Model</param>
        /// <returns>返回值 -1:已经存在相同的餐馆名称 -2:该菜馆已经存在相同的菜系 -3:餐馆编号未填写 -4:餐馆必填信息不完善 0:事务回滚 -1:正确</returns>
        public int UpdateDiningModel(Model.SourceStructure.MSourceDining model)
        {
            if (!string.IsNullOrEmpty(model.SourceId))
            {
                model.SourceModel.SourceId = model.SourceId;
            }
            else if (!string.IsNullOrEmpty(model.SourceModel.SourceId))
            {
                model.SourceId = model.SourceModel.SourceId;
            }
            else
            {
                return -3;
            }

            if (string.IsNullOrEmpty(model.SourceModel.Name))
            {
                return -4;
            }

            if (string.IsNullOrEmpty(model.SourceModel.OperatorId))
            {
                return -4;
            }
            model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.餐馆;
            for (int i = 0; i < model.LinkManList.Count; i++)
            {
                if (string.IsNullOrEmpty(model.LinkManList[i].Id))
                    model.LinkManList[i].Id = Guid.NewGuid().ToString();
                model.LinkManList[i].TypeId = model.SourceModel.SourceId;
                model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
                model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
                model.LinkManList[i].IssueTime = DateTime.Now;
            }
            for (int i = 0; i < model.DiningCuisineList.Count; i++)
            {
                model.DiningCuisineList[i].SourceId = model.SourceId;
            }
            if (model.SourceModel.ContractAttach != null)
            {
                model.SourceModel.ContractAttach.ItemId = model.SourceModel.SourceId;
                model.SourceModel.ContractAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
            }
            int result = diningDal.UpdateDiningModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("修改资源管理餐馆数据：餐馆编号为：" + model.SourceId);
            }
            return result;
        }

        /// <summary>
        /// 获得详细页面上的供应商餐馆Model(包含餐馆菜系,联系人)
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MSourceDining GetDiningModel(string SourceId)
        {
            if (string.IsNullOrEmpty(SourceId))
                return null;
            return diningDal.GetDiningModel(SourceId);
        }
        #endregion

        #region 导游
        /// <summary>
        /// 判断身份证号是否存在
        /// </summary>
        /// <param name="IDNumber">身份证号</param>
        /// <param name="GuideId">供应商编号GuideId,新增GuideId=""</param>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public bool ExistsIDNumber(string IDNumber, string GuideId, string CompanyID)
        {
            if (!string.IsNullOrEmpty(IDNumber) && !string.IsNullOrEmpty(CompanyID))
            {
                return guideDal.ExistsIDNumber(IDNumber, GuideId, CompanyID);
            }
            return false;
        }

        /// <summary>
        /// 添加导游信息
        /// </summary>
        /// <param name="model">供应商导游Model</param>
        /// <returns>返回值 -1:存在相同的用户名 -2:存在相同的身份证号 -3:存在相同的人事档案身份证号 -4:存在相同的导游证号 -5:公司编号未赋值 -6:导游必填信息不完善 小于0:错误 1:正确</returns>
        public int AddGuideModel(Model.SourceStructure.MSourceGuide model)
        {
            if (string.IsNullOrEmpty(model.CompanyId))
            {
                return -5;
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                return -6;
            }
            if (string.IsNullOrEmpty(model.UserModel.UserName))
            {
                return -6;
            }
            if (string.IsNullOrEmpty(model.UserModel.Password))
            {
                return -6;
            }
            if (model.UserModel == null || model.UserModel.DeptId <= 0)
            {
                return -6;
            }
            if (string.IsNullOrEmpty(model.GuideNumber))
            {
                return -6;
            }
            if (string.IsNullOrEmpty(model.IDNumber))
            {
                return -6;
            }
            model.GuideId = Guid.NewGuid().ToString();
            if (model.SourceGuideCategoryList != null && model.SourceGuideCategoryList.Count > 0)
            {
                for (int i = 0; i < model.SourceGuideCategoryList.Count; i++)
                {
                    model.SourceGuideCategoryList[i].GuideId = model.GuideId;
                }
            }
            model.IsDelete = false;
            model.UserModel.IsDelete = false;
            model.UserId = Guid.NewGuid().ToString();
            model.GovFileId = Guid.NewGuid().ToString();
            model.UserModel.UserId = model.UserId;
            model.UserModel.MD5Password = hc.MD5Encrypt(model.UserModel.Password);
            model.UserModel.UserType = Model.EnumType.ComStructure.UserType.供应商;
            int result = guideDal.AddGuideModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("新增导游中心导游档案数据：导游编号为：" + model.GuideId);
            }
            return result;
        }


        /// <summary>
        /// 修改导游Model
        /// </summary>
        /// <param name="model">供应商导游Model</param>
        /// <returns>返回值 -2:存在相同的身份证号 -3:存在相同的人事档案身份证号 -4:存在相同的导游证号 -5:导游编号未赋值 -7:人事编号未赋值  -8:用户编号未赋值 -6:导游必填信息不完善 小于0:错误 1:正确</returns>
        public int UpdateGuideModel(Model.SourceStructure.MSourceGuide model)
        {
            if (string.IsNullOrEmpty(model.GuideId))
            {
                return -5;
            }
            if (string.IsNullOrEmpty(model.GovFileId))
            {
                return -7;
            }
            if (string.IsNullOrEmpty(model.UserId))
            {
                return -8;
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                return -6;
            }
            if (string.IsNullOrEmpty(model.GuideNumber))
            {
                return -6;
            }
            if (model.UserModel == null || model.UserModel.DeptId <= 0)
            {
                return -6;
            }
            if (string.IsNullOrEmpty(model.IDNumber))
            {
                return -6;
            }
            if (model.SourceGuideCategoryList != null && model.SourceGuideCategoryList.Count > 0)
            {
                for (int i = 0; i < model.SourceGuideCategoryList.Count; i++)
                {
                    model.SourceGuideCategoryList[i].GuideId = model.GuideId;
                }
            }
            int result = guideDal.UpdateGuideModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("修改导游中心导游档案数据：导游编号为：" + model.GuideId);
            }
            return result;
        }


        /// <summary>
        /// 删除导游Model
        /// </summary>
        /// <param name="GuideId">供应商编号</param>
        /// <returns></returns>
        //public int DeleteGuideModel(string GuideId)
        //{
        //    int result = guideDal.DeleteGuideModel(GuideId);
        //    if (result > 0)
        //    {
        //        SysStructure.BSysLogHandle.Insert("删除导游中心导游档案数据：导游编号为：" + GuideId);
        //    }
        //    return result;
        //}


        /// <summary>
        /// 删除导游Model
        /// </summary>
        /// <param name="guideIdList">供应商编号List</param>
        /// <returns>负值或0：失败； 正值1：成功；  正值2：导游有安排</returns>
        public int DeleteGuideModel(params string[] guideIdList)
        {
            int result = guideDal.DeleteGuideModel(guideIdList);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < guideIdList.Length; i++)
            {
                sb.AppendFormat("'{0}',", guideIdList[i]);
            }
            sb.Remove(sb.Length - 1, 1);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("删除导游中心导游档案数据：导游编号为：" + sb.ToString());
            }
            return result;
        }


        /// <summary>
        /// 获得显示在列表页面上面的导游信息列表
        /// </summary>
        /// <param name="Searchmodel">导游列表查询Model</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MGuideListModel> GetGuideShowModel(Model.SourceStructure.MGuideSearchModel Searchmodel, string CompanyId, int pageIndex, int pageSize, ref int recordCount)
        {
            if (string.IsNullOrEmpty(CompanyId))
            {
                return null;
            }
            return guideDal.GetGuideShowModel(Searchmodel, CompanyId, pageIndex, pageSize, ref recordCount);
        }


        /// <summary>
        /// 获得导游Model
        /// </summary>
        /// <param name="GuideId">供应商编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MSourceGuide GetGuideModel(string GuideId)
        {
            return guideDal.GetGuideModel(GuideId);
        }

        /// <summary>
        /// 获得导游上团统计信息列表
        /// </summary>
        /// <param name="GuideName">导游姓名</param>
        /// <param name="TimeBegin">查询开始时间</param>
        /// <param name="TimeEnd">查询结束时间</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MGuideListGroup> GetGuideListGroup(string GuideName, DateTime? TimeBegin, DateTime? TimeEnd, string CompanyId, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<EyouSoft.Model.SourceStructure.MGuideListGroup> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<EyouSoft.Model.SourceStructure.MGuideListGroup>();
                list = guideDal.GetGuideListGroup(GuideName, TimeBegin, TimeEnd, CompanyId, pageIndex, pageSize, ref recordCount);
            }
            return list;
        }

        /// <summary>
        /// 获得导游带团详细信息列表
        /// </summary>
        /// <param name="GuideId">导游编号</param>
        /// <param name="TimeBegin">上团查询开始时间</param>
        /// <param name="TimeEnd">上团查询结束时间</param>
        /// <param name="LTimeBegin">出团查询开始时间</param>
        /// <param name="LTimeEnd">出团查询结束时间</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MGuideTourList> GetGuideTourList(string GuideId, DateTime? TimeBegin, DateTime? TimeEnd, DateTime? LTimeBegin, DateTime? LTimeEnd, string CompanyId, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<EyouSoft.Model.SourceStructure.MGuideTourList> list = null;
            if (!string.IsNullOrEmpty(CompanyId) && !string.IsNullOrEmpty(GuideId))
            {
                list = new List<EyouSoft.Model.SourceStructure.MGuideTourList>();
                list = guideDal.GetGuideTourList(GuideId, TimeBegin, TimeEnd, LTimeBegin, LTimeEnd, CompanyId, pageIndex, pageSize, ref recordCount);
            }
            return list;
        }

        /// <summary>
        /// 获得导游排班信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="GuideName">导游姓名</param>
        /// <param name="year">查询年份</param>
        /// <param name="month">查询月份</param>
        /// <param name="NextTimeStart">查询下团时间开始</param>
        /// <param name="NextTimeEnd">查询下团时间结束</param>
        /// <param name="Location">下团地点</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MGuidePlanWork> GetGuidePlanWork(string CompanyId, string GuideName, int year, int month, DateTime? NextTimeStart, DateTime? NextTimeEnd, string Location, int pageIndex, int pageSize, ref int recordCount)
        {
            IList<EyouSoft.Model.SourceStructure.MGuidePlanWork> list = null;
            if (!string.IsNullOrEmpty(CompanyId))
            {
                list = new List<EyouSoft.Model.SourceStructure.MGuidePlanWork>();
                list = guideDal.GetGuidePlanWork(CompanyId, GuideName, year, month, NextTimeStart, NextTimeEnd, Location, pageIndex, pageSize, ref recordCount);
            }
            return list;
        }

        /// <summary>
        /// 获得导游当日排班详细信息列表
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="GuideId">导游编号</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MGuidePlanWork> GetGuidePlanWorkInfo(string CompanyId, string GuideId, DateTime date)
        {
            IList<EyouSoft.Model.SourceStructure.MGuidePlanWork> list = null;
            if (!string.IsNullOrEmpty(CompanyId) && !string.IsNullOrEmpty(GuideId) && date != null)
            {
                list = new List<EyouSoft.Model.SourceStructure.MGuidePlanWork>();
                list = guideDal.GetGuidePlanWorkInfo(CompanyId, GuideId, date);
            }
            return list;
        }


        #endregion

        #region 酒店
        /// <summary>
        /// 添加供应商酒店Model
        /// </summary>
        /// <param name="model">供应商酒店Model</param>
        /// <returns>返回值 -1:已经存在相同的酒店名称 -2:该酒店已经存在相同的酒店房型 -3:已经存在相同的附件名称 -4:公司编号未赋值 -5:酒店必填信息不完善 0:事务回滚 1:正确</returns>
        public int AddHotelModel(Model.SourceStructure.MSourceHotel model)
        {
            model.SourceModel.SourceId = Guid.NewGuid().ToString();
            model.SourceId = model.SourceModel.SourceId;
            if (string.IsNullOrEmpty(model.SourceModel.CompanyId))
            {
                return -4;
            }
            if (string.IsNullOrEmpty(model.SourceModel.OperatorId))
            {
                return -4;
            }
            if (string.IsNullOrEmpty(model.SourceModel.Name))
            {
                return -5;
            }

            model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.酒店;
            model.SourceModel.IsDelete = false;

            for (int i = 0; i < model.LinkManList.Count; i++)
            {
                model.LinkManList[i].Id = Guid.NewGuid().ToString();
                model.LinkManList[i].TypeId = model.SourceId;
                model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
                model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
                model.LinkManList[i].IssueTime = DateTime.Now;
            }
            for (int i = 0; i < model.HotelRoomList.Count; i++)
            {
                model.HotelRoomList[i].RoomId = Guid.NewGuid().ToString();
                model.HotelRoomList[i].SourceId = model.SourceId;
            }
            if (model.AttachList != null && model.AttachList.Count > 0)
            {
                for (int i = 0; i < model.AttachList.Count; i++)
                {
                    model.AttachList[i].ItemId = model.SourceId;
                    model.AttachList[i].ItemType = Model.EnumType.ComStructure.AttachItemType.供应商;
                }
            }
            if (model.SourceModel.ContractAttach != null)
            {
                model.SourceModel.ContractAttach.ItemId = model.SourceModel.SourceId;
                model.SourceModel.ContractAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
            }
            int result = hotelDal.AddHotelModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("新增资源管理酒店数据：酒店编号为：" + model.SourceModel.SourceId);
            }
            return result;
        }


        /// <summary>
        /// 修改供应商酒店Model(包含联系人,价格)
        /// </summary>
        /// <param name="model">供应商酒店Model</param>
        /// <returns>返回值 -1:酒店名称已经存在 -2:已经存在相同的酒店房型名称 -3:存在相同的附件名称 -4:酒店编号未赋值 -5:酒店必填信息不完善 0:事务回滚 1:正确</returns>
        public int UpdateHotelModel(Model.SourceStructure.MSourceHotel model)
        {
            if (!string.IsNullOrEmpty(model.SourceId))
            {
                model.SourceModel.SourceId = model.SourceId;
            }
            else if (!string.IsNullOrEmpty(model.SourceModel.SourceId))
            {
                model.SourceId = model.SourceModel.SourceId;
            }
            else
            {
                return -4;
            }
            if (string.IsNullOrEmpty(model.SourceModel.Name))
            {
                return -5;
            }

            model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.酒店;
            for (int i = 0; i < model.LinkManList.Count; i++)
            {
                if (string.IsNullOrEmpty(model.LinkManList[i].Id))
                    model.LinkManList[i].Id = Guid.NewGuid().ToString();
                model.LinkManList[i].TypeId = model.SourceId;
                model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
                model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
                model.LinkManList[i].IssueTime = DateTime.Now;
            }
            for (int i = 0; i < model.HotelRoomList.Count; i++)
            {
                if (string.IsNullOrEmpty(model.HotelRoomList[i].RoomId))
                    model.HotelRoomList[i].RoomId = Guid.NewGuid().ToString();
                model.HotelRoomList[i].SourceId = model.SourceId;
            }
            if (model.AttachList != null && model.AttachList.Count > 0)
            {
                for (int i = 0; i < model.AttachList.Count; i++)
                {
                    model.AttachList[i].ItemId = model.SourceId;
                    model.AttachList[i].ItemType = Model.EnumType.ComStructure.AttachItemType.供应商;
                }
            }
            if (model.SourceModel.ContractAttach != null)
            {
                model.SourceModel.ContractAttach.ItemId = model.SourceModel.SourceId;
                model.SourceModel.ContractAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
            }
            model.SourceModel.IssueTime = DateTime.Now;
            int result = hotelDal.UpdateHotelModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("修改资源管理酒店数据：酒店编号为：" + model.SourceModel.SourceId);
            }
            return result;
        }

        /// <summary>
        /// 获得酒店详细页面供应商酒店Model(包含联系人列表,房型信息列表)
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MSourceHotel GetOneHotelModel(string SourceId)
        {
            if (string.IsNullOrEmpty(SourceId))
                return null;
            return hotelDal.GetOneHotelModel(SourceId);
        }

        /// <summary>
        /// 获得酒店房型列表
        /// </summary>
        /// <param name="sourceId">酒店编号</param>
        /// <returns>酒店房型列表</returns>
        public IList<Model.SourceStructure.MSourceHotelRoom> GetRoomModelList(string sourceId)
        {
            if (string.IsNullOrEmpty(sourceId))
                return null;
            return hotelDal.GetRoomModelList(sourceId);
        }
        #endregion

        #region 保险
        ///// <summary>
        ///// 添加保险Model
        ///// </summary>
        ///// <param name="model">供应商保险Model</param>
        ///// <returns>返回值 -1:已经存在相同的保险名称 -2:公司编号未赋值 -3:保险必填信息不完善 0:事务回滚 1:正确</returns>
        //public int AddInsuranceModel(Model.SourceStructure.MSourceInsurance model)
        //{
        //    if (string.IsNullOrEmpty(model.SourceModel.CompanyId))
        //    {
        //        return -2;
        //    }
        //    if (string.IsNullOrEmpty(model.SourceModel.Name))
        //    {
        //        return -3;
        //    }

        //    model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.保险;
        //    model.SourceModel.IsDelete = false;
        //    model.SourceModel.SourceId = Guid.NewGuid().ToString();
        //    for (int i = 0; i < model.LinkManList.Count; i++)
        //    {
        //        model.LinkManList[i].Id = Guid.NewGuid().ToString();
        //        model.LinkManList[i].TypeId = model.SourceModel.SourceId;
        //        model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
        //        model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
        //    }
        //    int result = insuranceDal.AddInsuranceModel(model);
        //    if (result > 0)
        //    {
        //        SysStructure.BSysLogHandle.Insert("新增资源管理保险数据：保险编号为：" + model.SourceModel.SourceId);
        //    }
        //    return result;
        //}





        ///// <summary>
        ///// 修改保险Model
        ///// </summary>
        ///// <param name="model">供应商保险Model</param>
        ///// <returns>返回值 -1:已经存在相同的保险名称 -2:保险编号未赋值 -3:保险必填信息不完善 0:事务回滚 1:正确</returns>
        //public int UpdateInsuranceModel(Model.SourceStructure.MSourceInsurance model)
        //{
        //    if (string.IsNullOrEmpty(model.SourceModel.SourceId))
        //    {
        //        return -2;
        //    }
        //    if (string.IsNullOrEmpty(model.SourceModel.Name))
        //    {
        //        return -3;
        //    }
        //    model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.保险;
        //    for (int i = 0; i < model.LinkManList.Count; i++)
        //    {
        //        model.LinkManList[i].Id = Guid.NewGuid().ToString();
        //        model.LinkManList[i].TypeId = model.SourceModel.SourceId;
        //        model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
        //        model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
        //    }
        //    int result = insuranceDal.UpdateInsuranceModel(model);
        //    if (result > 0)
        //    {
        //        SysStructure.BSysLogHandle.Insert("修改资源管理保险数据：保险编号为：" + model.SourceModel.SourceId);
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// 获得显示在列表页面上的保险信息列表
        ///// </summary>
        ///// <param name="model">供应商保险Model</param>
        ///// <param name="pageIndex">当前页</param>
        ///// <param name="pageSize">当前显示记录数</param>
        ///// <param name="recordCount">总记录数</param>
        ///// <returns></returns>
        //public IList<Model.SourceStructure.MInsuranceListModel> GetInsuranceShowModel(Model.SourceStructure.MSourceInsurance model, int pageIndex, int pageSize, ref int pageCount, ref int recordCount)
        //{
        //    if (model.SourceModel == null)
        //    {
        //        return null;
        //    }
        //    if (string.IsNullOrEmpty(model.SourceModel.CompanyId))
        //    {
        //        return null;
        //    }
        //    return insuranceDal.GetInsuranceShowModel(model, pageIndex, pageSize, ref pageCount, ref recordCount);
        //}

        ///// <summary>
        ///// 获得保险信息列表方法
        ///// </summary>
        ///// <param name="model">供应商Model</param>
        ///// <param name="pageIndex">当前页</param>
        ///// <param name="pageSize">当前显示记录数</param>
        ///// <param name="recordCount">总记录数</param>
        ///// <returns></returns>
        //public IList<Model.SourceStructure.MSource> GetInsuranceList(Model.SourceStructure.MSourceInsurance model, int pageIndex, int pageSize, ref int pageCount, ref int recordCount)
        //{
        //    return insuranceDal.GetInsuranceList(model, pageIndex, pageSize, ref pageCount, ref recordCount);
        //}
        #endregion

        #region 车队
        /// <summary>
        /// 添加单个车队Model
        /// </summary>
        /// <param name="model">供应商车队Model</param>
        /// <returns>返回值 -1:已经存在相同的车队名称 -2:公司编号未赋值 -3:车队必填信息不完善 0:事务回滚 1:正确</returns>
        public int AddMotorcadeModel(Model.SourceStructure.MSourceMotorcade model)
        {
            if (string.IsNullOrEmpty(model.SourceModel.CompanyId))
            {
                return -2;
            }
            if (string.IsNullOrEmpty(model.SourceModel.Name))
            {
                return -3;
            }
            model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.车队;
            model.SourceModel.IsDelete = false;
            model.SourceModel.SourceId = Guid.NewGuid().ToString();
            model.SourceModel.IssueTime = DateTime.Now;
            for (int i = 0; i < model.LinkManList.Count; i++)
            {
                model.LinkManList[i].Id = Guid.NewGuid().ToString();
                model.LinkManList[i].TypeId = model.SourceModel.SourceId;
                model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
                model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
                model.LinkManList[i].IssueTime = DateTime.Now;
            }
            for (int i = 0; i < model.CarList.Count; i++)
            {
                model.CarList[i].CarId = Guid.NewGuid().ToString();
                model.CarList[i].SourceId = model.SourceModel.SourceId;
            }
            if (model.SourceModel.ContractAttach != null)
            {
                model.SourceModel.ContractAttach.ItemId = model.SourceModel.SourceId;
                model.SourceModel.ContractAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
            }
            int result = motorcadeDal.AddMotorcadeModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("新增资源管理车队数据：车队编号为：" + model.SourceModel.SourceId);
            }
            return result;
        }




        /// <summary>
        /// 修改车队Model
        /// </summary>
        /// <param name="model">供应商车队Model</param>
        /// <returns>返回值 -1:已经存在相同的车队名称 -2:车队编号未赋值 -3:车队必填信息不完善 0:事务回滚 1:正确</returns>
        public int UpdateMotorcadeModel(Model.SourceStructure.MSourceMotorcade model)
        {
            if (string.IsNullOrEmpty(model.SourceModel.SourceId))
            {
                return -2;
            }
            if (string.IsNullOrEmpty(model.SourceModel.Name))
            {
                return -3;
            }
            model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.车队;
            for (int i = 0; i < model.LinkManList.Count; i++)
            {
                if (string.IsNullOrEmpty(model.LinkManList[i].Id))
                    model.LinkManList[i].Id = Guid.NewGuid().ToString();
                model.LinkManList[i].TypeId = model.SourceModel.SourceId;
                model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
                model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
                model.LinkManList[i].IssueTime = DateTime.Now;
            }
            for (int i = 0; i < model.CarList.Count; i++)
            {
                if (string.IsNullOrEmpty(model.CarList[i].CarId))
                    model.CarList[i].CarId = Guid.NewGuid().ToString();
                model.CarList[i].SourceId = model.SourceModel.SourceId;
            }
            //model.SourceModel.ContractAttach.ItemId = model.SourceModel.SourceId;
            if (model.SourceModel.ContractAttach != null)
            {
                model.SourceModel.ContractAttach.ItemId = model.SourceModel.SourceId;
                model.SourceModel.ContractAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
            }
            int result = motorcadeDal.UpdateMotorcadeModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("修改资源管理车队数据：车队编号为：" + model.SourceModel.SourceId);
            }
            return result;
        }

        /// <summary>
        /// 获得车队Model
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MSourceMotorcade GetMotorcadeModel(string SourceId)
        {
            if (string.IsNullOrEmpty(SourceId))
                return null;
            return motorcadeDal.GetMotorcadeModel(SourceId);
        }
        #endregion

        #region 其他
        /// <summary>
        /// 添加其他Model
        /// </summary>
        /// <param name="model">供应商其他Model</param>
        /// <returns>返回值 -1:已经存在相同的其他名称 -2:公司编号未赋值 -3:其他必填信息不完善 0:事务回滚 1:正确</returns>
        public int AddOtherModel(Model.SourceStructure.MSourceOther model)
        {
            if (string.IsNullOrEmpty(model.SourceModel.CompanyId))
            {
                return -2;
            }
            if (string.IsNullOrEmpty(model.SourceModel.Name))
            {
                return -3;
            }
            model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.其他;
            model.SourceModel.IsDelete = false;
            model.SourceModel.SourceId = Guid.NewGuid().ToString();
            for (int i = 0; i < model.LinkManList.Count; i++)
            {
                model.LinkManList[i].Id = Guid.NewGuid().ToString();
                model.LinkManList[i].TypeId = model.SourceModel.SourceId;
                model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
                model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
                model.LinkManList[i].IssueTime = DateTime.Now;
            }
            for (int i = 0; i < model.OtherTypeList.Count; i++)
            {
                model.OtherTypeList[i].SouceId = model.SourceModel.SourceId;
                model.OtherTypeList[i].OtherId = Guid.NewGuid().ToString();
            }
            if (model.SourceModel.ContractAttach != null)
            {
                model.SourceModel.ContractAttach.ItemId = model.SourceModel.SourceId;
                model.SourceModel.ContractAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
            }
            int result = otherDal.AddOtherModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("新增资源管理其他数据：其他编号为：" + model.SourceModel.SourceId);
            }
            return result;
        }


        /// <summary>
        /// 修改其他Model
        /// </summary>
        /// <param name="model">供应商其他Model</param>
        /// <returns>返回值 -1:已经存在相同的其他名称 -2:其他编号未赋值 -3:其他必填信息不完善 0:事务回滚 1:正确</returns>
        public int UpdateOtherModel(Model.SourceStructure.MSourceOther model)
        {
            if (string.IsNullOrEmpty(model.SourceModel.SourceId))
            {
                return -2;
            }
            if (string.IsNullOrEmpty(model.SourceModel.Name))
            {
                return -3;
            }
            model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.其他;
            for (int i = 0; i < model.LinkManList.Count; i++)
            {
                if (string.IsNullOrEmpty(model.LinkManList[i].Id))
                    model.LinkManList[i].Id = Guid.NewGuid().ToString();
                model.LinkManList[i].TypeId = model.SourceModel.SourceId;
                model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
                model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
                model.LinkManList[i].IssueTime = DateTime.Now;
            }
            for (int i = 0; i < model.OtherTypeList.Count; i++)
            {
                if (string.IsNullOrEmpty(model.OtherTypeList[i].OtherId))
                    model.OtherTypeList[i].OtherId = Guid.NewGuid().ToString();
                model.OtherTypeList[i].SouceId = model.SourceModel.SourceId;
            }
            model.SourceModel.IssueTime = DateTime.Now;
            if (model.SourceModel.ContractAttach != null)
            {
                model.SourceModel.ContractAttach.ItemId = model.SourceModel.SourceId;
                model.SourceModel.ContractAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
            }
            int result = otherDal.UpdateOtherModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("修改资源管理其他数据：其他编号为：" + model.SourceModel.SourceId);
            }
            return result;
        }

        /// <summary>
        /// 获得其他Model
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MSourceOther GetOtherModel(string SourceId)
        {
            if (string.IsNullOrEmpty(SourceId))
                return null;
            return otherDal.GetOtherModel(SourceId);
        }
        #endregion

        #region 游轮
        /// <summary>
        /// 添加单个游轮Model
        /// </summary>
        /// <param name="model">供应商游轮Model</param>
        /// <returns>返回值 -1:已经存在相同的游轮名称 -2:公司编号未赋值 -3:游轮必填信息不完善 0:事务回滚 1:正确</returns>
        public int AddShipModel(Model.SourceStructure.MSourceShip model)
        {
            if (string.IsNullOrEmpty(model.SourceModel.CompanyId))
            {
                return -2;
            }
            if (string.IsNullOrEmpty(model.SourceModel.Name))
            {
                return -3;
            }
            model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.游轮;
            model.SourceModel.IsDelete = false;
            model.SourceId = Guid.NewGuid().ToString();
            model.SourceModel.SourceId = model.SourceId;
            for (int i = 0; i < model.LinkManList.Count; i++)
            {
                model.LinkManList[i].Id = Guid.NewGuid().ToString();
                model.LinkManList[i].TypeId = model.SourceId;
                model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
                model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
                model.LinkManList[i].IssueTime = DateTime.Now;
            }
            for (int i = 0; i < model.SubShipList.Count; i++)
            {
                model.SubShipList[i].SourceId = model.SourceId;
                model.SubShipList[i].SubId = Guid.NewGuid().ToString();
                model.SubShipList[i].AttachModel.ItemId = model.SubShipList[i].SubId;
                model.SubShipList[i].AttachModel.ItemType = Model.EnumType.ComStructure.AttachItemType.游轮旗下游轮图片;
            }
            model.SourceModel.IssueTime = DateTime.Now;
            if (model.SourceModel.ContractAttach != null)
            {
                model.SourceModel.ContractAttach.ItemId = model.SourceModel.SourceId;
                model.SourceModel.ContractAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
            }
            int result = shipDal.AddShipModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("新增资源管理游轮数据：游轮编号为：" + model.SourceModel.SourceId);
            }
            return result;
        }




        /// <summary>
        /// 修改游轮Model
        /// </summary>
        /// <param name="model">供应商游轮Model</param>
        /// <returns>返回值 -1:已经存在相同的游轮名称 -2:游轮编号未赋值 -3:游轮必填信息不完善 0:事务回滚 1:正确</returns>
        public int UpdateShipModel(Model.SourceStructure.MSourceShip model)
        {
            if (!string.IsNullOrEmpty(model.SourceId))
            {
                model.SourceModel.SourceId = model.SourceId;
            }
            else if (!string.IsNullOrEmpty(model.SourceModel.SourceId))
            {
                model.SourceId = model.SourceModel.SourceId;
            }
            else
            {
                return -2;
            }
            if (string.IsNullOrEmpty(model.SourceModel.Name))
            {
                return -3;
            }
            model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.游轮;
            for (int i = 0; i < model.LinkManList.Count; i++)
            {
                if (string.IsNullOrEmpty(model.LinkManList[i].Id))
                    model.LinkManList[i].Id = Guid.NewGuid().ToString();
                model.LinkManList[i].TypeId = model.SourceId;
                model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
                model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
                model.LinkManList[i].IssueTime = DateTime.Now;
            }
            for (int i = 0; i < model.SubShipList.Count; i++)
            {
                if (string.IsNullOrEmpty(model.SubShipList[i].SubId))
                    model.SubShipList[i].SubId = Guid.NewGuid().ToString();
                model.SubShipList[i].SourceId = model.SourceId;

                model.SubShipList[i].AttachModel.ItemId = model.SubShipList[i].SubId;
                model.SubShipList[i].AttachModel.ItemType = Model.EnumType.ComStructure.AttachItemType.游轮旗下游轮图片;
            }
            model.SourceModel.IssueTime = DateTime.Now;
            if (model.SourceModel.ContractAttach != null)
            {
                model.SourceModel.ContractAttach.ItemId = model.SourceModel.SourceId;
                model.SourceModel.ContractAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
            }
            int result = shipDal.UpdateShipModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("修改资源管理游轮数据：游轮编号为：" + model.SourceModel.SourceId);
            }
            return result;
        }

        /// <summary>
        /// 获得详细页面显示游轮Model(包含联系人)
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MSourceShip GetShipModel(string SourceId)
        {
            if (string.IsNullOrEmpty(SourceId))
                return null;
            return shipDal.GetShipModel(SourceId);
        }
        #endregion

        #region 购物
        /// <summary>
        /// 添加购物Model
        /// </summary>
        /// <param name="model">供应商Model</param>
        /// <returns>返回值 -1:已经存在相同的购物点名称 -2:公司编号未赋值 -3:购物必填信息不完善 0:事务回滚 1:正确</returns>
        public int AddShopModel(Model.SourceStructure.MSourceShop model)
        {
            if (string.IsNullOrEmpty(model.SourceModel.CompanyId))
            {
                return -2;
            }
            if (string.IsNullOrEmpty(model.SourceModel.Name))
            {
                return -3;
            }
            model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.购物;
            model.SourceModel.IsDelete = false;
            model.SourceId = Guid.NewGuid().ToString();
            model.SourceModel.SourceId = model.SourceId;
            for (int i = 0; i < model.LinkManList.Count; i++)
            {
                model.LinkManList[i].Id = Guid.NewGuid().ToString();
                model.LinkManList[i].TypeId = model.SourceId;
                model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
                model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
                model.LinkManList[i].IssueTime = DateTime.Now;
            }
            model.SourceModel.IssueTime = DateTime.Now;
            if (model.SourceModel.ContractAttach != null)
            {
                model.SourceModel.ContractAttach.ItemId = model.SourceId;
                model.SourceModel.ContractAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
            }
            int result = shopDal.AddShopModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("新增资源管理购物数据：购物编号为：" + model.SourceModel.SourceId);
            }
            return result;
        }



        /// <summary>
        /// 修改购物Model
        /// </summary>
        /// <param name="model">供应商购物Model</param>
        /// <returns>返回值 -1:已经存在相同的购物点名称 -2:购物编号未赋值 -3:购物必填信息不完善 0:事务回滚 1:正确</returns>
        public int UpdateShopModel(Model.SourceStructure.MSourceShop model)
        {
            if (!string.IsNullOrEmpty(model.SourceId))
            {
                model.SourceModel.SourceId = model.SourceId;
            }
            else if (!string.IsNullOrEmpty(model.SourceModel.SourceId))
            {
                model.SourceId = model.SourceModel.SourceId;
            }
            else
            {
                return -2;
            }
            if (string.IsNullOrEmpty(model.SourceModel.Name))
            {
                return -3;
            }
            model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.购物;
            for (int i = 0; i < model.LinkManList.Count; i++)
            {
                if (string.IsNullOrEmpty(model.LinkManList[i].Id))
                    model.LinkManList[i].Id = Guid.NewGuid().ToString();
                model.LinkManList[i].TypeId = model.SourceId;
                model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
                model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
                model.LinkManList[i].IssueTime = DateTime.Now;
            }
            model.SourceModel.IssueTime = DateTime.Now;
            if (model.SourceModel.ContractAttach != null)
            {
                model.SourceModel.ContractAttach.ItemId = model.SourceModel.SourceId;
                model.SourceModel.ContractAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
            }
            int result = shopDal.UpdateShopModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("修改资源管理购物数据：购物编号为：" + model.SourceModel.SourceId);
            }
            return result;
        }

        /// <summary>
        /// 获得购物Model(包含联系人)
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MSourceShop GetShopModel(string SourceId)
        {
            if (string.IsNullOrEmpty(SourceId))
                return null;
            return shopDal.GetShopModel(SourceId);
        }

        #endregion

        #region 景点
        /// <summary>
        /// 添加景点Model
        /// </summary>
        /// <param name="model">供应商景点Model</param>
        /// <returns>返回值 -1:已经存在相同的景点名称 -2:已经存在相同的附件名称 -3:公司编号未赋值 -4:景点必填信息不完善 -5:景点名称不能重复 0:事务回滚 1:正确</returns>
        public int AddSpotModel(Model.SourceStructure.MSourceSpot model)
        {
            if (string.IsNullOrEmpty(model.SourceModel.CompanyId))
            {
                return -3;
            }
            if (string.IsNullOrEmpty(model.SourceModel.Name))
            {
                return -4;
            }
            model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.景点;
            model.SourceModel.IsDelete = false;
            model.SourceModel.SourceId = Guid.NewGuid().ToString();
            if (model.AttachList != null && model.AttachList.Count > 0)
            {
                for (int i = 0; i < model.AttachList.Count; i++)
                {
                    model.AttachList[i].ItemId = model.SourceModel.SourceId;
                    model.AttachList[i].ItemType = Model.EnumType.ComStructure.AttachItemType.供应商;
                }
            }
            for (int i = 0; i < model.LinkManList.Count; i++)
            {
                model.LinkManList[i].Id = Guid.NewGuid().ToString();
                model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
                model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
                model.LinkManList[i].TypeId = model.SourceModel.SourceId;
                model.LinkManList[i].IssueTime = DateTime.Now;
            }
            for (int i = 0; i < model.PriceSystemList.Count; i++)
            {
                //model.PriceSystemList[i].SourceId = model.SourceModel.SourceId;
                //for (int j = 1; j < model.PriceSystemList.Count; j++)
                //{
                //    if (model.PriceSystemList[i].Name == model.PriceSystemList[j].Name)
                //    {
                //        isExists = true;
                //        break;
                //    }
                //}
                //if (isExists)
                //    break;
                //EyouSoft.Toolkit.Function.EqualityComparer<Model.SourceStructure.MSpotPriceSystemModel> eq = new EyouSoft.Toolkit.Function.EqualityComparer<Model.SourceStructure.MSpotPriceSystemModel>();
                //eq.ComparerString.Add("Name");
                //if (model.PriceSystemList.Contains(model.PriceSystemList[i], eq))
                //{
                //    return -5;
                //}
                var linq = from entity in model.PriceSystemList group entity by new { Name = entity.SpotName } into entityGroup where entityGroup.Count() > 1 select entityGroup;
                if (linq.ToList().Count > 0)
                {
                    return -5;
                }
            }
            for (int i = 0; i < model.PriceSystemList.Count; i++)
            {
                if (string.IsNullOrEmpty(model.PriceSystemList[i].Id))
                    model.PriceSystemList[i].Id = Guid.NewGuid().ToString();
                model.PriceSystemList[i].SourceId = model.SourceModel.SourceId;
            }
            //if (isExists)
            //    return -5;
            model.SourceModel.IssueTime = DateTime.Now;
            if (model.SourceModel.ContractAttach != null)
            {
                model.SourceModel.ContractAttach.ItemId = model.SourceModel.SourceId;
                model.SourceModel.ContractAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
            }
            int result = spotDal.AddSpotModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("新增资源管理景点数据：景点编号为：" + model.SourceModel.SourceId);
            }
            return result;
        }




        /// <summary>
        /// 修改景点Model
        /// </summary>
        /// <param name="model">供应商景点Model</param>
        /// <returns>返回值 -1:已经存在相同的景点名称 -2:已经存在相同的附件名称 -3:景点编号未赋值 -4:景点必填信息不完善 -5:景点名称不能重复 0:事务回滚 1:正确</returns>
        public int UpdateSpotModel(Model.SourceStructure.MSourceSpot model)
        {
            if (string.IsNullOrEmpty(model.SourceModel.SourceId))
            {
                return -3;
            }

            if (string.IsNullOrEmpty(model.SourceModel.Name))
            {
                return -4;
            }
            model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.景点;
            if (model.AttachList != null && model.AttachList.Count > 0)
            {
                for (int i = 0; i < model.AttachList.Count; i++)
                {
                    model.AttachList[i].ItemId = model.SourceModel.SourceId;
                    model.AttachList[i].ItemType = Model.EnumType.ComStructure.AttachItemType.供应商;
                }
            }
            for (int i = 0; i < model.LinkManList.Count; i++)
            {
                if (string.IsNullOrEmpty(model.LinkManList[i].Id))
                    model.LinkManList[i].Id = Guid.NewGuid().ToString();
                model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
                model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
                model.LinkManList[i].TypeId = model.SourceModel.SourceId;
                model.LinkManList[i].IssueTime = DateTime.Now;
            }
            bool isExists = false;
            for (int i = 0; i < model.PriceSystemList.Count; i++)
            {
                //model.PriceSystemList[i].SourceId = model.SourceModel.SourceId;
                //for (int j = 1; j < model.PriceSystemList.Count; j++)
                //{
                //    if (model.PriceSystemList[i].Name == model.PriceSystemList[j].Name)
                //    {
                //        isExists = true;
                //        break;
                //    }
                //}
                //if (isExists)
                //    break;
                //EyouSoft.Toolkit.Function.EqualityComparer<Model.SourceStructure.MSpotPriceSystemModel> eq = new EyouSoft.Toolkit.Function.EqualityComparer<Model.SourceStructure.MSpotPriceSystemModel>();
                //eq.ComparerString.Add("Name");
                //if (model.PriceSystemList.Contains(model.PriceSystemList[i], eq))
                //{
                //    return -5;
                //}
                var linq = from entity in model.PriceSystemList group entity by new { Name = entity.SpotName } into entityGroup where entityGroup.Count() > 1 select entityGroup;
                if (linq.ToList().Count > 0)
                {
                    return -5;
                }
            }
            for (int i = 0; i < model.PriceSystemList.Count; i++)
            {
                if (string.IsNullOrEmpty(model.PriceSystemList[i].Id))
                    model.PriceSystemList[i].Id = Guid.NewGuid().ToString();
                model.PriceSystemList[i].SourceId = model.SourceModel.SourceId;
            }
            //if (isExists)
            //    return -5;
            model.SourceModel.IssueTime = DateTime.Now;
            if (model.SourceModel.ContractAttach != null)
            {
                model.SourceModel.ContractAttach.ItemId = model.SourceModel.SourceId;
                model.SourceModel.ContractAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
            }
            int result = spotDal.UpdateSpotModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("修改资源管理景点数据：景点编号为：" + model.SourceModel.SourceId);
            }
            return result;
        }

        /// <summary>
        /// 获得景点信息(包含联系人)
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MSourceSpot GetSpotModel(string SourceId)
        {
            if (string.IsNullOrEmpty(SourceId))
                return null;
            return spotDal.GetSpotModel(SourceId);
        }

        /// <summary>
        /// 获得景点价格体系
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="sourceId">供应商编号</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MSpotPriceSystemModel> GetSpotPriceSystemModelList(string companyId, string sourceId)
        {
            return spotDal.GetSpotPriceSystemModelList(companyId, sourceId);
        }
        #endregion

        #region 票务
        /// <summary>
        /// 添加票务Model
        /// </summary>
        /// <param name="model">供应商票务Model</param>
        /// <returns>返回值 -1:已经存在相同的票务名称 -2:公司编号未赋值 -3:票务必填信息不完善 0:事务回滚 1:正确</returns>
        public int AddTicketModel(Model.SourceStructure.MSourceTicket model)
        {
            if (string.IsNullOrEmpty(model.SourceModel.CompanyId))
            {
                return -2;
            }
            if (string.IsNullOrEmpty(model.SourceModel.Name))
            {
                return -3;
            }
            model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.票务;
            model.SourceModel.IsDelete = false;
            model.SourceModel.SourceId = Guid.NewGuid().ToString();
            for (int i = 0; i < model.LinkManList.Count; i++)
            {
                model.LinkManList[i].Id = Guid.NewGuid().ToString();
                model.LinkManList[i].TypeId = model.SourceModel.SourceId;
                model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
                model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
                model.LinkManList[i].IssueTime = DateTime.Now;
            }
            model.SourceModel.IssueTime = DateTime.Now;
            if (model.SourceModel.ContractAttach != null)
            {
                model.SourceModel.ContractAttach.ItemId = model.SourceModel.SourceId;
                model.SourceModel.ContractAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
            }
            int result = ticketDal.AddTicketModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("新增资源管理票务数据：票务编号为：" + model.SourceModel.SourceId);
            }
            return result;
        }


        /// <summary>
        /// 修改票务Model
        /// </summary>
        /// <param name="model">供应商票务Model</param>
        /// <returns>返回值 -1:已经存在相同的票务名称 -2:票务编号未赋值 -3:票务必填信息不完善 0:事务回滚 1:正确</returns>
        public int UpdateTicketModel(Model.SourceStructure.MSourceTicket model)
        {
            if (string.IsNullOrEmpty(model.SourceModel.SourceId))
            {
                return -2;
            }
            if (string.IsNullOrEmpty(model.SourceModel.Name))
            {
                return -3;
            }
            model.SourceModel.Type = Model.EnumType.SourceStructure.SourceType.票务;
            for (int i = 0; i < model.LinkManList.Count; i++)
            {
                if (string.IsNullOrEmpty(model.LinkManList[i].Id))
                    model.LinkManList[i].Id = Guid.NewGuid().ToString();
                model.LinkManList[i].TypeId = model.SourceModel.SourceId;
                model.LinkManList[i].Type = Model.EnumType.ComStructure.LxrType.供应商;
                model.LinkManList[i].CompanyId = model.SourceModel.CompanyId;
                model.LinkManList[i].IssueTime = DateTime.Now;
            }
            model.SourceModel.IssueTime = DateTime.Now;
            model.SourceModel.ContractAttach.ItemId = model.SourceModel.SourceId;
            model.SourceModel.ContractAttach.ItemType = Model.EnumType.ComStructure.AttachItemType.供应商合同附件;
            int result = ticketDal.UpdateTicketModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("修改资源管理票务数据：票务编号为：" + model.SourceModel.SourceId);
            }
            return result;
        }

        /// <summary>
        /// 获得票务Model(包含联系人)
        /// </summary>
        /// <param name="SourceId">供应商编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MSourceTicket GetTicketModel(string SourceId)
        {
            if (string.IsNullOrEmpty(SourceId))
                return null;
            return ticketDal.GetTicketModel(SourceId);
        }
        #endregion

        #region 线路
        /// <summary>
        /// 添加线路Model
        /// </summary>
        /// <param name="model">线路Model</param>
        /// <returns>返回值 -1:已经存在相同的线路名称 -2;同一个行程已经存在相同的景点 -3:已经存在相同的附件名称 -4:公司编号未赋值 -5:线路必填信息不完善 0:事务回滚 1:正确</returns>
        public int AddRouteModel(Model.SourceStructure.MRoute model)
        {
            if (string.IsNullOrEmpty(model.CompanyId))
            {
                return -4;
            }
            if (string.IsNullOrEmpty(model.RouteName))
            {
                return -5;
            }
            if (model.AreaId == 0)
            {
                return -5;
            }
            model.IsDelete = false;
            model.RouteId = Guid.NewGuid().ToString();
            if (model.PlanModelList != null && model.PlanModelList.Count > 0)
            {
                foreach (var t in model.PlanModelList)
                {
                    if (string.IsNullOrEmpty(t.PlanId))
                        t.PlanId = Guid.NewGuid().ToString();
                    t.ItemId = model.RouteId;
                }
            }
            if (!model.IsTourOrSubentry)
            {
                if (model.StandardModelList != null && model.StandardModelList.Count > 0)
                {
                    foreach (var t in model.StandardModelList)
                    {
                        t.RouteId = model.RouteId;
                        if (string.IsNullOrEmpty(t.StandardId))
                            t.StandardId = Guid.NewGuid().ToString();
                    }
                }
            }

            if (model.Attach != null)
            {
                model.Attach.ItemId = model.RouteId;
                model.Attach.ItemType = Model.EnumType.ComStructure.AttachItemType.线路附件;
            }

            model.ServicesModel.ItemId = model.RouteId;
            if (model.VisaInfoList != null && model.VisaInfoList.Count > 0)
            {
                foreach (var t in model.VisaInfoList)
                {
                    t.ItemId = model.RouteId;
                    t.ItemType = Model.EnumType.ComStructure.AttachItemType.线路签证资料;
                }
            }
            if (model.PlanModelList != null && model.PlanModelList.Count > 0)
            {
                foreach (var t in model.PlanModelList)
                {
                    foreach (var t1 in t.TourPlanSpot)
                    {
                        t1.PlanId = t.PlanId;
                    }
                }
            }
            model.IssueTime = DateTime.Now;
            int result = routeDal.AddRouteModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("新增资源管理线路库数据：线路库编号为：" + model.RouteId);
            }
            return result;
        }




        /// <summary>
        /// 修改线路Model
        /// </summary>
        /// <param name="model">线路Model</param>
        /// <returns>返回值 -1:已经存在相同的线路名称 -2;同一个行程已经存在相同的景点 -3:已经存在相同的附件名称 -4:线路库编号未赋值 -5:线路必填信息不完善 0:事务回滚 1:正确</returns>
        public int UpdateRouteModel(Model.SourceStructure.MRoute model)
        {
            if (string.IsNullOrEmpty(model.RouteId))
            {
                return -4;
            }
            if (string.IsNullOrEmpty(model.RouteName))
            {
                return -5;
            }
            if (model.AreaId == 0)
            {
                return -5;
            }
            if (model.PlanModelList != null && model.PlanModelList.Count > 0)
            {
                foreach (var t in model.PlanModelList)
                {
                    if (string.IsNullOrEmpty(t.PlanId))
                        t.PlanId = Guid.NewGuid().ToString();
                    t.ItemId = model.RouteId;
                }
            }
            if (!model.IsTourOrSubentry)
            {
                if (model.StandardModelList != null && model.StandardModelList.Count > 0)
                {
                    foreach (var t in model.StandardModelList)
                    {
                        t.RouteId = model.RouteId;
                        if (string.IsNullOrEmpty(t.StandardId))
                            t.StandardId = Guid.NewGuid().ToString();
                    }
                }
            }
            if (model.Attach != null)
            {
                model.Attach.ItemId = model.RouteId;
                model.Attach.ItemType = Model.EnumType.ComStructure.AttachItemType.线路附件;
            }

            model.ServicesModel.ItemId = model.RouteId;
            if (model.VisaInfoList != null && model.VisaInfoList.Count > 0)
            {
                foreach (var t in model.VisaInfoList)
                {
                    t.ItemId = model.RouteId;
                    t.ItemType = Model.EnumType.ComStructure.AttachItemType.线路签证资料;
                }
            }
            if (model.PlanModelList != null && model.PlanModelList.Count > 0)
            {
                foreach (var t in model.PlanModelList)
                {
                    foreach (var t1 in t.TourPlanSpot)
                    {
                        t1.PlanId = t.PlanId;
                    }
                }
            }
            model.IssueTime = DateTime.Now;
            int result = routeDal.UpdateRouteModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("修改资源管理线路库数据：线路库编号为：" + model.RouteId);
            }
            return result;
        }


        /// <summary>
        /// 删除线路Model
        /// </summary>
        /// <param name="RouteId">线路编号</param>
        /// <returns></returns>
        public int DeleteRouteModel(string RouteId)
        {
            int result = routeDal.DeleteRouteModel(RouteId);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("删除资源管理线路库数据：线路库编号为：" + RouteId);
            }
            return result;
        }


        /// <summary>
        /// 批量删除线路Model
        /// </summary>
        /// <param name="routeIdList">线路编号列表</param>
        /// <returns></returns>
        public int DeleteRouteModel(params string[] routeIdList)
        {
            var rtn = 0;
            if (routeIdList == null || routeIdList.Length == 0)
            {
                return rtn;
            }
            foreach (var s in routeIdList)
            {
                rtn = DeleteRouteModel(s);
            }
            return rtn;
        }

        /// <summary>
        /// 获得显示在线路列表上的数据
        /// </summary>
        /// <param name="model">线路Model</param>
        /// <param name="startDate">开始发布日期</param>
        /// <param name="endDate">结束发布日期</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MRouteListModel> GetRouteShowModel(Model.SourceStructure.MRouteListModel model, DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize, ref int recordCount)
        {
            if (model == null)
            {
                return null;
            }
            if (string.IsNullOrEmpty(model.CompanyId))
            {
                return null;
            }
            bool isOnlySeft = false;
            string DepartIdString = GetAllowDepart(EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_线路资源, out isOnlySeft);
            return routeDal.GetRouteShowModel(model, isOnlySeft, base.LoginUserId, DepartIdString, startDate, endDate, pageIndex, pageSize, ref recordCount);
        }



        /// <summary>
        /// 获得线路Model
        /// </summary>
        /// <param name="RouteId">线路编号</param>
        /// <returns></returns>
        public Model.SourceStructure.MRoute GetRouteModel(string RouteId)
        {
            if (string.IsNullOrEmpty(RouteId))
                return null;
            return routeDal.GetRouteModel(RouteId);
        }


        /// <summary>
        /// 判断线路名称是否存在
        /// </summary>
        /// <param name="RouteName">线路名称</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="areaId">线路区域编号</param>
        /// <returns></returns>
        public bool IsExists(string RouteName, string companyId, int areaId)
        {
            if (string.IsNullOrEmpty(RouteName) || string.IsNullOrEmpty(companyId) || string.IsNullOrEmpty(areaId.ToString()))
                return false;
            return routeDal.IsExists(RouteName, companyId, areaId);
        }

        /// <summary>
        /// 获得上团列表
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MTourOnCount> GetSTModelList(string routeId, string companyId, int pageIndex, int pageSize, ref int recordCount)
        {
            if (string.IsNullOrEmpty(routeId) || string.IsNullOrEmpty(companyId))
            {
                return null;
            }
            return routeDal.GetSTModelList(routeId, companyId, pageIndex, pageSize, ref recordCount);
        }

        /// <summary>
        /// 获得收客列表
        /// </summary>
        /// <param name="routeId">线路编号</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前显示记录数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns></returns>
        public IList<Model.SourceStructure.MAcceptGuestModel> GetSKModelList(string routeId, string companyId, int pageIndex, int pageSize, ref int recordCount)
        {
            if (string.IsNullOrEmpty(routeId) || string.IsNullOrEmpty(companyId))
            {
                return null;
            }
            return routeDal.GetSKModelList(routeId, companyId, pageIndex, pageSize, ref recordCount);
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 得到当前用户的浏览权限
        /// </summary>
        /// <param name="Menu2">二级菜单</param>
        /// <param name="isOnlySelf">是否仅浏览自己添加的数据</param>
        /// <returns></returns>
        private string GetAllowDepart(EyouSoft.Model.EnumType.PrivsStructure.Menu2 Menu2, out bool isOnlySelf)
        {
            //能查看到该菜单下面数据的部门编号，NULL为所有部门
            int[] DepartIds;
            DepartIds = GetDataPrivs(Menu2, out  isOnlySelf);
            string DepartIdString = "";
            if (DepartIds != null)
            {
                for (int i = 0; i < DepartIds.Length; i++)
                {
                    DepartIdString += DepartIds[i].ToString() + ",";
                }
                if (!string.IsNullOrEmpty(DepartIdString))
                {
                    DepartIdString = DepartIdString.Remove(DepartIdString.Length - 1);
                }
            }
            return DepartIdString;
        }
        #endregion

    }
}
