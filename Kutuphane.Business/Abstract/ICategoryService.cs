using Kutuphane.DAL.Dto.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Business.Abstract
{
    public interface ICategoryService
    {
        Task<List<GetListCategoryDto>> GetListCategories();
        Task<GetCategoryDto> GetCategoryById(int CategoryId);

        Task<int> AddCategory(AddCategoryDto addCategoryDto);
        Task<int> UpdateCategory(UpdateCategoryDto updateCategoryDto);
        Task<int> DeleteCategory(int categoryId);
    }
}
