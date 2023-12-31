﻿using StoreHouse.Database.Entities;
using StoreHouse.Database.Services.DTO;

namespace StoreHouse.Database.Services.Interfaces;

/*
 * Presents a service for working with Ingredient table.
 * The methods of writing, reading and changing table data are implemented.
 */
public interface IIngredientService
{
 //Ingredient methods
 Task<(bool IsSuccess, string ErrorMessage, Ingredient Ingredient)> CreateIngredientAsync(Ingredient ingredient);
 Task<(bool IsSuccess, string ErrorMessage, Ingredient Ingredient)> UpdateIngredientAsync(Ingredient updatedIngredient);
 Task<(bool IsSuccess, string ErrorMessage)> DeleteIngredientAsync(int ingredientId);
 Task<(bool IsSuccess, string ErrorMessage, List<Ingredient> IngredientList)> GetAllIngredientsAsync();
 Task<(bool IsSuccess, string ErrorMessage, decimal PrimeCost)> GetPrimeCostByName(string name);
 Task<(bool IsSuccess, string ErrorMessage, List<RelatedSemiProductsDTO> SemiProducts)> GetRelatedSemiProductsAsync(Ingredient ingredient);
}