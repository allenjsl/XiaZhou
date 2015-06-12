using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.BLL.CrmStructure
{
    /// <summary>
    /// 个人会员类型
    /// 创建者:钱琦
    /// 时间:2011-10-1
    /// </summary>
    public class BCrmMemberType : BLLBase
    {
        #region dal对象
        EyouSoft.IDAL.CrmStructure.ICrmMemberType memberTypeDal = EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.CrmStructure.ICrmMemberType>();
        #endregion

        #region 个人会员类型
        /// <summary>
        /// 添加个人会员类型Model
        /// </summary>
        /// <param name="model">个人会员类型Model</param>
        /// <returns>返回值 -1:已经存在相同的类型名称 -2:公司编号未赋值 1:正确</returns>
        public int AddCrmMemberTypeModel(Model.CrmStructure.MCrmMemberType model)
        {
            if (string.IsNullOrEmpty(model.CompanyId))
            {
                return -2;
            }
            int result= memberTypeDal.AddCrmMemberTypeModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("添加客户管理个人会员类型数据");
            }
            return result;
        }


        /// <summary>
        /// 修改个人会员类型Model
        /// </summary>
        /// <param name="model">个人会员类型Model</param>
        /// <returns></returns>
        public int UpdateCrmMemberTypeModel(Model.CrmStructure.MCrmMemberType model)
        {
            int result= memberTypeDal.UpdateCrmMemberTypeModel(model);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("修改客户管理个人会员类型数据");
            }
            return result;
        }



        /// <summary>
        /// 删除个人会员类型Model
        /// </summary>
        /// <param name="TypeId">个人会员类型编号</param>
        /// <returns></returns>
        public int DeleteCrmMemberTypeModel(int TypeId)
        {
            int result= memberTypeDal.DeleteCrmMemberTypeModel(TypeId);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("删除客户管理个人会员类型数据 个人会员类型编号："+TypeId.ToString());
            }
            return result;
        }


        /// <summary>
        /// 批量删除个人会员类型Model
        /// </summary>
        /// <param name="typeIdList">个人会员类型编号List</param>
        /// <returns></returns>
        public int DeleteCrmMemberTypeModel(params int[] typeIdList)
        {
            int result= memberTypeDal.DeleteCrmMemberTypeModel(typeIdList);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < typeIdList.Length; i++)
            {
                sb.AppendFormat("{0},",typeIdList[i].ToString());
            }
            sb.Remove(sb.Length-1,1);
            if (result > 0)
            {
                SysStructure.BSysLogHandle.Insert("删除客户管理个人会员类型数据 个人会员类型编号：" + sb.ToString());
            }
            return result;
        }


        /// <summary>
        /// 获得所有个人会员类型Model
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        public IList<Model.CrmStructure.MCrmMemberType> GetCrmMerberTypeModelList(string CompanyId)
        {
            return memberTypeDal.GetCrmMerberTypeModelList(CompanyId);
        }


        /// <summary>
        /// 获得个人会员类型Model
        /// </summary>
        /// <param name="TypeId">个人会员类型Model</param>
        /// <returns></returns>
        public Model.CrmStructure.MCrmMemberType GetCrmMerberTypeModel(int TypeId)
        {
            return memberTypeDal.GetCrmMerberTypeModel(TypeId);
        }


        /// <summary>
        /// 判断是否存在相同的个人会员类型名称
        /// </summary>
        /// <param name="TypeName">个人会员类型名称</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public bool IsExistsSameMemberType(string TypeName,string companyId)
        {
            return memberTypeDal.IsExistsSameMemberType(TypeName,companyId);
        }
        #endregion
    }
}
