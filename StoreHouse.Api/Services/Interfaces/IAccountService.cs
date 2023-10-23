﻿using StoreHouse.Api.Model.DTO.AcoountDTO;
using StoreHouse.Api.Model.DTO.ManageDTO;
using StoreHouse.Database.Entities;

namespace StoreHouse.Api.Services.Interfaces;

public interface IAccountService
{
    Task<(bool IsSuccess, string ErrorMessage, ManageUserResponse User)> LoginUser(LoginDataRequest loginData);
}