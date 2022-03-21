using Kutuphane.Business.Abstract;
using Kutuphane.DAL.Context;
using Kutuphane.DAL.Dto.Category;
using Kutuphane.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly KutuphaneDbContext _kutuphaneDbContext; // Constructor - YAPICI BASKASI ULASAMASIN DIYE

        public CategoryService(KutuphaneDbContext kutuphaneDbContext) //BOŞ DEĞERE DEĞER ATADIK.
        {
            _kutuphaneDbContext = kutuphaneDbContext;
        }
        public async Task<int> AddCategory(AddCategoryDto addCategoryDto) // return etmeden addcategory hata verır
        {
            var addingCategory = new Category
            {
                Name = addCategoryDto.Name
            };
            await _kutuphaneDbContext.Categories.AddAsync(addingCategory);
            return await _kutuphaneDbContext.SaveChangesAsync();


        }

        public async Task<int> DeleteCategory(int categoryId)
        {
            var currentCategory = await _kutuphaneDbContext.Categories.Where(p => !p.IsDeleted && p.Id == categoryId).FirstOrDefaultAsync();
            if (currentCategory == null)
            {
                return -1;
            }
            currentCategory.IsDeleted = true;
            _kutuphaneDbContext.Categories.Update(currentCategory);
            return await _kutuphaneDbContext.SaveChangesAsync();

        }


        public async Task<GetCategoryDto> GetCategoryById(int categoryId)
        {
            return await _kutuphaneDbContext.Categories.Where(p => !p.IsDeleted && p.Id == categoryId)
                .Select(p => new GetCategoryDto
                {
                    Id = p.Id,
                    Name = p.Name
                }).FirstOrDefaultAsync();
        }

        public async Task<List<GetListCategoryDto>> GetListCategories()
        {
            return await _kutuphaneDbContext.Categories.Where(p => !p.IsDeleted)
                .Select(p => new GetListCategoryDto
                {
                    Id = p.Id,
                    Name = p.Name,
                }).ToListAsync();
        }

        public async Task<int> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var currentCategory = await _kutuphaneDbContext.Categories.Where(p => !p.IsDeleted && p.Id == updateCategoryDto.Id).FirstOrDefaultAsync();
            if (currentCategory == null)
            {
                return -1;
            }

            currentCategory.Name = updateCategoryDto.Name;

            _kutuphaneDbContext.Categories.Update(currentCategory);
            return await _kutuphaneDbContext.SaveChangesAsync();
        }
    }
}

