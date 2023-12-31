﻿using StoreHouse.Database.Entities;

namespace StoreHouse.Database.Services.Interfaces;

/*
 * Presents a service for working with Supplier table.
 * The methods of writing, reading and changing table data are implemented.
 */
public interface ISupplierService
{
 //Supplier methods
 Task<(bool IsSuccess, string ErrorMessage, Supplier Supplier)> CreateSupplierAsync(Supplier supplier);
 Task<(bool IsSuccess, string ErrorMessage, Supplier Supplier)> UpdateSupplierAsync(Supplier updatedSupplier);
 Task<(bool IsSuccess, string ErrorMessage)> DeleteSupplierAsync(int supplierId);
 Task<(bool IsSuccess, string ErrorMessage, List<Supplier> SupplierList)> GetAllSuppliersAsync();
 Task<(bool IsSuccess, string ErrorMessage, int Id)> GetIdByName(string name);
}