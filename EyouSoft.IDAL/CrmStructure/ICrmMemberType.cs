using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.CrmStructure
{
    /// <summary>
    /// 个人会员类型
    /// 创建者:钱琦
    /// 时间 :2011-10-1
    /// </summary>
    public interface ICrmMemberType
    {
        /// <summary>
        /// 添加个人会员类型Model
        /// </summary>
        /// <param name="model">个人会员类型Model</param>
        /// <returns></returns>
        int AddCrmMemberTypeModel(Model.CrmStructure.MCrmMemberType model);

        /// <summary>
        /// 修改个人会员类型Model
        /// </summary>
        /// <param name="model">个人会员类型Model</param>
        /// <returns></returns>
        int UpdateCrmMemberTypeModel(Model.CrmStructure.MCrmMemberType model);



        /// <summary>
        /// 删除个人会员类型Model
        /// </summary>
        /// <param name="TypeId">个人会员类型编号</param>
        /// <returns></returns>
        int DeleteCrmMemberTypeModel(int TypeId);


        /// <summary>
        /// 批量删除个人会员类型Model
        /// </summary>
        /// <param name="TypeIdList">个人会员类型编号List</param>
        /// <returns></returns>
        int DeleteCrmMemberTypeModel(params int[] typeIdList);


        /// <summary>
        /// 获得所有个人会员类型Model
        /// </summary>
        /// <param name="CompanyId">公司编号</param>
        /// <returns></returns>
        IList<Model.CrmStructure.MCrmMemberType> GetCrmMerberTypeModelList(string CompanyId);


        /// <summary>
        /// 获得个人会员类型Model
        /// </summary>
        /// <param name="TypeId">个人会员类型Model</param>
        /// <returns></returns>
        Model.CrmStructure.MCrmMemberType GetCrmMerberTypeModel(int TypeId);


        /// <summary>
        /// 判断是否存在相同的个人会员类型名称
        /// </summary>
        /// <param name="TypeName">个人会员类型名称</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        bool IsExistsSameMemberType(string TypeName,string companyId);
    }
}
