using Microsoft.AspNetCore.Mvc;
using NewsPortal.Application.Services;

namespace NewsPortal.Web.ViewComponents;

public class CategoryDropdownViewComponent:ViewComponent
{
    private readonly CategoryService _categoryService;

    public CategoryDropdownViewComponent(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        return View(categories);
    }
    
}