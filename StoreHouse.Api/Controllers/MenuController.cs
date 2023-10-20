using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse.Api.Model.DTO.MenuDTO;
using StoreHouse.Api.Model.DTO.StorageDTO;
using StoreHouse.Api.Services.Interfaces;

namespace StoreHouse.Api.Controllers;

[ApiController]
[Route($"menu")]
[Authorize]
public class MenuController : Controller
{
    private readonly IMenuService _menuService;
    public MenuController(IMenuService menuService) => _menuService = menuService;
    
    [HttpGet]
    [Route($"products")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _menuService.GetAllProductsAsync();
        if (!products.IsSuccess)
            return BadRequest(products.ErrorMessage);
        
        return Ok(products.AllProducts);
    }
    
    [HttpGet]
    [Route($"dishes")]
    public async Task<IActionResult> GetAllDishes()
    {
        var dishes = await _menuService.GetAllDishesAsync();
        if (!dishes.IsSuccess)
            return BadRequest(dishes.ErrorMessage);
        
        return Ok(dishes.AllDishes);
    }
    
    [HttpGet]
    [Route($"semiproducts")]
    public async Task<IActionResult> GetAllSemiProducts()
    {
        var semiProducts = await _menuService.GetAllSemiProductsAsync();
        if (!semiProducts.IsSuccess)
            return BadRequest(semiProducts.ErrorMessage);
        
        return Ok(semiProducts.AllSemiProducts);
    }
    
    [HttpGet]
    [Route($"ingredients")]
    public async Task<IActionResult> GetAllIngredients()
    {
        var ingredients = await _menuService.GetAllIngredientsAsync();
        if (!ingredients.IsSuccess)
            return BadRequest(ingredients.ErrorMessage);
        
        return Ok(ingredients.AllIngredients);
    }
    
    [HttpGet]
    [Route($"prodcategories")]
    public async Task<IActionResult> GetAllProductCategories()
    {
        var productCategories = await _menuService.GetAllProductCategoriesAsync();
        if (!productCategories.IsSuccess)
            return BadRequest(productCategories.ErrorMessage);
        
        return Ok(productCategories.AllProductCategories);
    }
    
    [HttpGet]
    [Route($"ingrcategories")]
    public async Task<IActionResult> GetAllIngredientCategories()
    {
        var ingredientCategories = await _menuService.GetAllIngredientCategoriesAsync();
        if (!ingredientCategories.IsSuccess)
            return BadRequest(ingredientCategories.ErrorMessage);
        
        return Ok(ingredientCategories.AllIngredientCategories);
    }
    
    [HttpPut]
    [Route("product/update")]
    public async Task<IActionResult> UpdateProduct(MenuProductRequest updatedProduct)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var updatedProductResult = await _menuService.UpdateProductAsync(updatedProduct);
        if (!updatedProductResult.IsSuccess)
            return BadRequest(updatedProductResult.ErrorMessage);

        return Ok(updatedProductResult.UpdatedId);
    }
    
    [HttpPut]
    [Route("dish/update")]
    public async Task<IActionResult> UpdateDish(MenuDishRequest updatedDish)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var updatedDishResult = await _menuService.UpdateDishAsync(updatedDish);
        if (!updatedDishResult.IsSuccess)
            return BadRequest(updatedDishResult.ErrorMessage);

        return Ok(updatedDishResult.UpdatedId);
    }
    
    [HttpPut]
    [Route("semiproduct/update")]
    public async Task<IActionResult> UpdateSemiProduct(MenuSemiProductRequest updatedSemiProduct)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var updatedSemiProductResult = await _menuService.UpdateSemiProductAsync(updatedSemiProduct);
        if (!updatedSemiProductResult.IsSuccess)
            return BadRequest(updatedSemiProductResult.ErrorMessage);

        return Ok(updatedSemiProductResult.UpdatedId);
    }
    
    [HttpPut]
    [Route("ingredient/update")]
    public async Task<IActionResult> UpdateIngredient(MenuIngredientUpdateRequest updatedIngredient)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var updatedIngredientResult = await _menuService.UpdateIngredientAsync(updatedIngredient);
        if (!updatedIngredientResult.IsSuccess)
            return BadRequest(updatedIngredientResult.ErrorMessage);

        return Ok(updatedIngredientResult.UpdatedId);
    }
    
    [HttpPost]
    [Route("product/create")]
    public async Task<IActionResult> AddProduct(MenuProductRequest product)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var addProductResult = await _menuService.AddProductAsync(product);
        if (!addProductResult.IsSuccess)
            return BadRequest(addProductResult.ErrorMessage);

        return Ok();
    }
    
    [HttpPost]
    [Route("dish/create")]
    public async Task<IActionResult> AddDish(MenuDishRequest dish)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var addDishResult = await _menuService.AddDishAsync(dish);
        if (!addDishResult.IsSuccess)
            return BadRequest(addDishResult.ErrorMessage);

        return Ok();
    }
    
    [HttpPost]
    [Route("semiproduct/create")]
    public async Task<IActionResult> AddSemiProduct(MenuSemiProductRequest semiProduct)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var addSemiProductResult = await _menuService.AddSemiProductAsync(semiProduct);
        if (!addSemiProductResult.IsSuccess)
            return BadRequest(addSemiProductResult.ErrorMessage);

        return Ok();
    }
    
    [HttpPost]
    [Route("ingredient/create")]
    public async Task<IActionResult> AddIngredient(MenuIngredientAddRequest ingredient)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var addIngredientResult = await _menuService.AddIngredientAsync(ingredient);
        if (!addIngredientResult.IsSuccess)
            return BadRequest(addIngredientResult.ErrorMessage);

        return Ok();
    }
    
    [HttpPost]
    [Route("prodcategory/create")]
    public async Task<IActionResult> AddProductCategory(MenuProductCategoryRequest productCategory)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var addProductCategoryResult = await _menuService.AddProductCategoryAsync(productCategory);
        if (!addProductCategoryResult.IsSuccess)
            return BadRequest(addProductCategoryResult.ErrorMessage);

        return Ok();
    }
    
    [HttpPost]
    [Route("ingrcategory/create")]
    public async Task<IActionResult> AddIngredientCategory(MenuIngredientCategoryRequest ingredientCategory)
    {
        if (!ModelState.IsValid)
        {
            var errorMessages = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(errorMessages);
        }

        var addIngredientCategoryResult = await _menuService.AddIngredientCategoryAsync(ingredientCategory);
        if (!addIngredientCategoryResult.IsSuccess)
            return BadRequest(addIngredientCategoryResult.ErrorMessage);

        return Ok();
    }
    
    [HttpDelete]
    [Route("product/delete")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var deletedProduct = await _menuService.DeleteProductAsync(id);
        if (!deletedProduct.IsSuccess)
            return BadRequest(deletedProduct.ErrorMessage);

        return Ok();
    }
    
    [HttpDelete]
    [Route("dish/delete")]
    public async Task<IActionResult> DeleteDish(int id)
    {
        var deletedDish = await _menuService.DeleteDishAsync(id);
        if (!deletedDish.IsSuccess)
            return BadRequest(deletedDish.ErrorMessage);

        return Ok();
    }
    
    [HttpDelete]
    [Route("semiproduct/delete")]
    public async Task<IActionResult> DeleteSemiProduct(int id)
    {
        var deletedSemiProduct = await _menuService.DeleteSemiProductAsync(id);
        if (!deletedSemiProduct.IsSuccess)
            return BadRequest(deletedSemiProduct.ErrorMessage);

        return Ok();
    }
    
    [HttpDelete]
    [Route("ingredient/delete")]
    public async Task<IActionResult> DeleteIngredient(int id)
    {
        var deletedIngredient = await _menuService.DeleteIngredientAsync(id);
        if (!deletedIngredient.IsSuccess)
            return BadRequest(deletedIngredient.ErrorMessage);

        return Ok();
    }
    
    [HttpDelete]
    [Route("prodcategory/delete")]
    public async Task<IActionResult> DeleteProductCategory(int id)
    {
        var deletedProductCategory = await _menuService.DeleteProductCategoryAsync(id);
        if (!deletedProductCategory.IsSuccess)
            return BadRequest(deletedProductCategory.ErrorMessage);

        return Ok();
    }
    
    [HttpDelete]
    [Route("ingrcategory/delete")]
    public async Task<IActionResult> DeleteIngredientCategory(int id)
    {
        var deletedIngredientCategory = await _menuService.DeleteIngredientCategoryAsync(id);
        if (!deletedIngredientCategory.IsSuccess)
            return BadRequest(deletedIngredientCategory.ErrorMessage);

        return Ok();
    }
}