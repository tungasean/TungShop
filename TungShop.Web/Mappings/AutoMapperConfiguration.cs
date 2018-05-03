using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using TungShop.Model.Models;
using TungShop.Web.Models;

namespace TungShop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
            Mapper.CreateMap<Tag, TagViewModel>();

            Mapper.CreateMap<ProductCategory, ProductCategoryViewModel>();
            Mapper.CreateMap<Product, ProductViewModel>();
            Mapper.CreateMap<ProductTag, ProductTagViewModel>();

            Mapper.CreateMap<ApplicationGroup, ApplicationGroupViewModel>();
            Mapper.CreateMap<ApplicationRole, ApplicationRoleViewModel>();
            Mapper.CreateMap<ApplicationUser, ApplicationUserViewModel>();

            Mapper.CreateMap<Student, StudentViewModel>();
            Mapper.CreateMap<Approval, ApprovalViewModel>();
            Mapper.CreateMap<Room, RoomViewModel>();
            Mapper.CreateMap<Contract, ContractViewModel>();
            Mapper.CreateMap<ElectricityWater, ElectricityWaterViewModel>();
            Mapper.CreateMap<RoomAsset, RoomAssetViewModel>();
            Mapper.CreateMap<ListAsset, ListAssetViewModel>();
            Mapper.CreateMap<ElectricityWaterHistory, ElectricityWaterHistoryViewModel>();
        }
    }
}