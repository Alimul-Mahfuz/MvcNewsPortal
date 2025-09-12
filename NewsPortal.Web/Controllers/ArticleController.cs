using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Application.DTOs;
using NewsPortal.Application.Interfaces;
using NewsPortal.Application.Services;
using NewsPortal.Web.ViewModels;
using NewsPortal.Web.Filters;
namespace NewsPortal.Web.Controllers;
[SessionAuthorized]
public class ArticleController : Controller
{
    protected readonly TagService _tagService;
    protected readonly CategoryService _categoryService;
    private readonly IFileService _fileService;
    protected readonly ArticleService _articleService;

    public ArticleController(TagService tagService, CategoryService categoryService, IFileService fileService,
        ArticleService articleService)
    {
        _fileService = fileService;
        _tagService = tagService;
        _categoryService = categoryService;
        _articleService = articleService;
    }

    public IActionResult Index()
    {
        return View();
    }


    public async Task<IActionResult> Create()
    {
        var tags = await _tagService.GetTagsAsync();
        var categories = await _categoryService.GetCategoriesAsync();
        var vm = new ArticleViewModel
        {
            Categories = categories,
            Tags = tags
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Store(ArticleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Tags = await _tagService.GetTagsAsync();
            model.Categories = await _categoryService.GetCategoriesAsync();
            return View("Create", model);
        }

        string? ImagePath = null;

        if (model.CoverImage != null && model.CoverImage.Length > 0)
        {
            using (var fs = new MemoryStream())
            {
                model.CoverImage.CopyTo(fs);
                var appFile = new AppFile
                {
                    FileName = model.CoverImage.FileName,
                    ContentType = model.CoverImage.ContentType,
                    Content = fs.ToArray()
                };
                ImagePath = await _fileService.UploadFileAsync(appFile, "Article");
            }
        }

        var acrticledto = new CreateArticleDto
        {
            Title = model.Title,
            Content = model.Content,
            Summery = model.Summery,
            CategoryId = model.CategoryId,
            TagIds = model.SelectedTags.Select(int.Parse).ToList(),
            CoverImage = ImagePath
        };
        try
        {
            var acrticle = await _articleService.CreateArticle(acrticledto);
            return View("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

[HttpGet]
public async Task<IActionResult> DataServer(int? categoryId)
{
    var draw = Request.Query["draw"].FirstOrDefault();
    var start = Request.Query["start"].FirstOrDefault();
    var length = Request.Query["length"].FirstOrDefault();

    int pageSize = string.IsNullOrEmpty(length) ? 10 : Convert.ToInt32(length);
    int skip = string.IsNullOrEmpty(start) ? 0 : Convert.ToInt32(start);

    var query = _articleService.GetArticlesByCategory();

    int recordsFiltered = await query.CountAsync();
    int recordsTotal = await _articleService.GetCount();

    var data = await query
        .Skip(skip)
        .Take(pageSize)
        .Select(o =>new
        {
            o.Title,
            Category = new { o.Category.Name },
            Tags= string.Join(",",o.ArticleTags.Select(t=>t.Tag.Name)),
            o.IsPublished,
            o.CreatedAt,
        })
        .ToListAsync();

    return Json(new
    {
        draw = draw,
        recordsFiltered = recordsFiltered,
        recordsTotal = recordsTotal,
        data = data
    });
}

}