using Kutuphane.DAL.Dto.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Business.Abstract
{
    public interface ISubCategoryService
    {
        Task<List<GetListSubCategoryDto>> GetAllSubCategories();
        Task<GetSubCategoryDto> GetSubCategoryById(int subCategoryId); 
        Task<int> AddSubCategory(AddSubCategoryDto addSubCategoryDto);
        Task<int> UpdateSubCategory(UpdateSubCategoryDto updateSubCategoryDto);
        Task<int> DeleteSubCategory(int subCategoryId);
    }
}
