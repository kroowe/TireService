using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TireService.Core.Domain.Services;
using TireService.Core.Utils;
using TireService.Dtos.Infos.Category;
using TireService.Dtos.Views.Category;
using TireService.Infrastructure.Entities.Settings;

namespace TireServiceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly IMapper _mapper;
        
        public CategoriesController(CategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryView))]
        public async Task<ActionResult<CategoryView>> Create([FromBody] CategoryInfo categoryCreateInfo)
        {
            var categoryToCreate = _mapper.Map<Category>(categoryCreateInfo);

            if (categoryCreateInfo.ParentCategoryId.HasValue)
            {
                var parentCategory = await _categoryService.GetById(categoryCreateInfo.ParentCategoryId.Value);
                categoryToCreate.CategoryPath = LTreeConvertHelper.ConcatPath(parentCategory.CategoryPath.ToString(),
                    LTreeConvertHelper.NormalizeValue(categoryToCreate.Id.ToString()));
            }
            else
            {
                categoryToCreate.CategoryPath = LTreeConvertHelper.NormalizeValue(categoryToCreate.Id.ToString());
            }
            
            await _categoryService.Create(categoryToCreate);
            return _mapper.Map<CategoryView>(categoryToCreate);
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryView))]
        public async Task<ActionResult<CategoryView>> Update([FromRoute] Guid categoryId, [FromBody] CategoryInfo categoryUpdateInfo)
        {
            var category = await _categoryService.GetById(categoryId);
            var categoryToUpdate = _mapper.Map(categoryUpdateInfo, category);
            await _categoryService.Update(categoryToUpdate);
            return _mapper.Map<CategoryView>(categoryToUpdate);
        }
        
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromRoute] Guid categoryId)
        {
            var category = await _categoryService.GetById(categoryId);
            var allCategoriesToDelete = await _categoryService.GetAllCategoriesByParentCategory( LTreeConvertHelper.NormalizeValue(category.Id.ToString()));
            await _categoryService.DeleteAll(allCategoriesToDelete);
            return Ok();
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<CategoryView>))]
        public async Task<ActionResult<IReadOnlyCollection<CategoryView>>> GetAll()
        {
            var allCategory = await _categoryService.GetAll();
            return Ok(_mapper.Map<IReadOnlyCollection<CategoryView>>(allCategory));
        }
    }
}
