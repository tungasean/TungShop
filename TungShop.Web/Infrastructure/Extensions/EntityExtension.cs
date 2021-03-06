﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TungShop.Model.Models;
using TungShop.Web.Models;

namespace TungShop.Web.Infrastructure.Extensions
{
    public static class EntityExtension
    {
        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryVm)
        {
            postCategory.ID = postCategoryVm.ID;
            postCategory.Name = postCategoryVm.Name;
            postCategory.Description = postCategoryVm.Description;
            postCategory.Alias = postCategoryVm.Alias;
            postCategory.ParentID = postCategoryVm.ParentID;
            postCategory.DisplayOrder = postCategoryVm.DisplayOrder;
            postCategory.Image = postCategoryVm.Image;
            postCategory.HomeFlag = postCategoryVm.HomeFlag;

            postCategory.CreatedDate = postCategoryVm.CreatedDate;
            postCategory.CreatedBy = postCategoryVm.CreatedBy;
            postCategory.UpdatedDate = postCategoryVm.UpdatedDate;
            postCategory.UpdatedBy = postCategoryVm.UpdatedBy;
            postCategory.MetaKeyword = postCategoryVm.MetaKeyword;
            postCategory.MetaDescription = postCategoryVm.MetaDescription;
            postCategory.Status = postCategoryVm.Status;

        }
        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryVm)
        {
            productCategory.ID = productCategoryVm.ID;
            productCategory.Name = productCategoryVm.Name;
            productCategory.Description = productCategoryVm.Description;
            productCategory.Alias = productCategoryVm.Alias;
            productCategory.ParentID = productCategoryVm.ParentID;
            productCategory.DisplayOrder = productCategoryVm.DisplayOrder;
            productCategory.Image = productCategoryVm.Image;
            productCategory.HomeFlag = productCategoryVm.HomeFlag;

            productCategory.CreatedDate = productCategoryVm.CreatedDate;
            productCategory.CreatedBy = productCategoryVm.CreatedBy;
            productCategory.UpdatedDate = productCategoryVm.UpdatedDate;
            productCategory.UpdatedBy = productCategoryVm.UpdatedBy;
            productCategory.MetaKeyword = productCategoryVm.MetaKeyword;
            productCategory.MetaDescription = productCategoryVm.MetaDescription;
            productCategory.Status = productCategoryVm.Status;

        }

        public static void UpdateStudent(this Student student, StudentViewModel studentVm)
        {
            student.StudentID = studentVm.StudentID;
            student.Name = studentVm.Name;
            student.Sex = studentVm.Sex;
            student.Address = studentVm.Address;
            student.CardNo = studentVm.CardNo;
            student.BirthDay = studentVm.BirthDay;
            student.IsOut = studentVm.IsOut;

        }

        public static void UpdateStudentDiscipline(this StudentDiscipline student, StudentDisciplineViewModel studentVm)
        {
            student.ID = studentVm.ID;
            student.StudentID = studentVm.StudentID;
            student.Name = studentVm.Name;
            student.InfoDiscipline = studentVm.InfoDiscipline;

        }

        public static void UpdateRoomAsset(this RoomAsset roomAsset, RoomAssetViewModel roomAssetVm)
        {
            roomAsset.Amount = roomAssetVm.Amount;
            roomAsset.AssetsID = roomAssetVm.AssetsID;
            roomAsset.RoomID = roomAssetVm.RoomID;

        }

        public static void UpdateApproval(this Approval approval, ApprovalViewModel approvalVm)
        {
            approval.ApprovalId = approvalVm.ApprovalId;
            approval.StudentId = approvalVm.StudentId;
            approval.Name = approvalVm.Name;
            approval.Sex = approvalVm.Sex;
            approval.Address = approvalVm.Address;
            approval.CardNo = approvalVm.CardNo;
            approval.BirthDay = approvalVm.BirthDay;
            approval.Status = approvalVm.Status;

        }
        public static void UpdateElectricityWater(this ElectricityWater electricityWater, ElectricityWaterViewModel electricityWaterVm)
        {
            electricityWater.RoomID = electricityWaterVm.RoomID;
            electricityWater.EletricityOld = electricityWaterVm.EletricityOld;
            electricityWater.EletricityNew = electricityWaterVm.EletricityNew;
            electricityWater.WaterOld = electricityWaterVm.WaterOld;
            electricityWater.WaterNew = electricityWaterVm.WaterNew;
            electricityWater.Month = electricityWaterVm.Month;
            electricityWater.Money = electricityWaterVm.Money;
            electricityWater.PriceElectricity = electricityWaterVm.PriceElectricity;
            electricityWater.PriceWater = electricityWaterVm.PriceWater;
            electricityWater.UserID = electricityWaterVm.UserID;

        }
        public static void UpdateListAsset(this ListAsset listAsset, ListAssetViewModel listAssetVm)
        {
            listAsset.AssetsID = listAssetVm.AssetsID;
            listAsset.AssetName = listAssetVm.AssetName;
            listAsset.Amount = listAssetVm.Amount;
            listAsset.Status = listAssetVm.Status;
            listAsset.AssetStype = listAssetVm.AssetStype;

        }
        public static void UpdateElectricityWaterHistory(this ElectricityWaterHistory electricityWaterHistory, ElectricityWaterHistoryViewModel electricityWaterHistoryVm)
        {
            electricityWaterHistory.ID = electricityWaterHistoryVm.ID;
            electricityWaterHistory.RoomID = electricityWaterHistoryVm.RoomID;
            electricityWaterHistory.EletricityOld = electricityWaterHistoryVm.EletricityOld;
            electricityWaterHistory.EletricityNew = electricityWaterHistoryVm.EletricityNew;
            electricityWaterHistory.WaterOld = electricityWaterHistoryVm.WaterOld;
            electricityWaterHistory.WaterNew = electricityWaterHistoryVm.WaterNew;
            electricityWaterHistory.Month = electricityWaterHistoryVm.Month;
            electricityWaterHistory.Money = electricityWaterHistoryVm.Money;
            electricityWaterHistory.PriceElectricity = electricityWaterHistoryVm.PriceElectricity;
            electricityWaterHistory.PriceWater = electricityWaterHistoryVm.PriceWater;
            electricityWaterHistory.UserID = electricityWaterHistoryVm.UserID;
            electricityWaterHistory.IsPrint = electricityWaterHistoryVm.IsPrint;
            electricityWaterHistory.TimeChange = electricityWaterHistoryVm.TimeChange;

        }

        public static void UpdateRoom(this Room room, RoomViewModel roomVm)
        {
            room.RoomID = roomVm.RoomID;
            room.RoomName = roomVm.RoomName;
            room.AmountMax = roomVm.AmountMax;
            room.Amount = roomVm.Amount;
            room.Sex = roomVm.Sex;
        }

        public static void UpdateContract(this Contract contract, ContractViewModel contractVm)
        {
            contract.StudentID = contractVm.StudentID;
            contract.RoomID = contractVm.RoomID;
            contract.TimeSign = contractVm.TimeSign;
            contract.Term = contractVm.Term;
            contract.Status = contractVm.Status;
            contract.Note = contractVm.Note;
    }

        public static void UpdatePost(this Post post, PostViewModel postVm)
        {
            post.ID = postVm.ID;
            post.Name = postVm.Name;
            post.Description = postVm.Description;
            post.Alias = postVm.Alias;
            post.CategoryID = postVm.CategoryID;
            post.Content = postVm.Content;
            post.Image = postVm.Image;
            post.HomeFlag = postVm.HomeFlag;
            post.ViewCount = postVm.ViewCount;

            post.CreatedDate = postVm.CreatedDate;
            post.CreatedBy = postVm.CreatedBy;
            post.UpdatedDate = postVm.UpdatedDate;
            post.UpdatedBy = postVm.UpdatedBy;
            post.MetaKeyword = postVm.MetaKeyword;
            post.MetaDescription = postVm.MetaDescription;
            post.Status = postVm.Status;
        }
        

        public static void UpdateApplicationGroup(this ApplicationGroup appGroup, ApplicationGroupViewModel appGroupViewModel)
        {
            appGroup.ID = appGroupViewModel.ID;
            appGroup.Name = appGroupViewModel.Name;
        }

        public static void UpdateApplicationRole(this ApplicationRole appRole, ApplicationRoleViewModel appRoleViewModel, string action = "add")
        {
            if (action == "update")
                appRole.Id = appRoleViewModel.Id;
            else
                appRole.Id = Guid.NewGuid().ToString();
            appRole.Name = appRoleViewModel.Name;
            appRole.Description = appRoleViewModel.Description;
        }
        public static void UpdateUser(this ApplicationUser appUser, ApplicationUserViewModel appUserViewModel, string action = "add")
        {

            appUser.Id = appUserViewModel.Id;
            appUser.FullName = appUserViewModel.FullName;
            appUser.BirthDay = appUserViewModel.BirthDay;
            appUser.Email = appUserViewModel.Email;
            appUser.UserName = appUserViewModel.UserName;
            appUser.PhoneNumber = appUserViewModel.PhoneNumber;
        }
    }
}