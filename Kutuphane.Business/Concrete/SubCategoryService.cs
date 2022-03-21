using Kutuphane.Business.Abstract;
using Kutuphane.DAL.Context;
using Kutuphane.DAL.Dto.SubCategory;
using Kutuphane.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Business.Concrete
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly KutuphaneDbContext _kutuphaneDbContext;
        public SubCategoryService(KutuphaneDbContext kutuphaneDbContext)
        {
            _kutuphaneDbContext = kutuphaneDbContext;
        }

        //SERVİSİMİZE INTERFACESINNI IMPLEMENT ETTIK
        // private Lİ KISMI SOR
        //YAPICIYI YAZIP DEĞİŞKENE ISMINI ATAMIŞIZ ALTINDA DA BOŞ DEĞERE DEĞER ATADIK



        public async Task<int> AddSubCategory(AddSubCategoryDto addSubCategoryDto)

        {
            var addingSubcategory = new SubCategory
            {
                Name = addSubCategoryDto.Name,
                CategoryId = addSubCategoryDto.CategoryId
            };
           await _kutuphaneDbContext.SubCategories.AddAsync(addingSubcategory);
            return await _kutuphaneDbContext.SaveChangesAsync();

            // BURAYA VERI EKLENIYORSA GEREKEN SEY EKLENEN VERILERIN NEREYE ATANACAĞI YANI BIR DEĞİŞKEN BELİRLENMESİ ŞART
            //PARANTEZ DISINA EKLIYORUM KI PARANTEZ KAPANINCA O DEĞİŞKEN KAYBOLMASIN
            //en basta olusturdugumuz değişkenle birlikte add sınıfındakı tanımlananları buraya ıstıyoruz
            // add sınıfı hata verıyorsa bılkı return etmeddıgındendir.
        }

        public async Task<int> DeleteSubCategory(int subcategoryId) // once ıcerısıne yaz
        {
            var currentSubCategory = await _kutuphaneDbContext.SubCategories.Where(p => !p.IsDeleted && p.Id == subcategoryId).FirstOrDefaultAsync();
            if (currentSubCategory == null)
            {
                return -1;
            }
            currentSubCategory.IsDeleted = true;
            _kutuphaneDbContext.SubCategories.Update(currentSubCategory);
            return await _kutuphaneDbContext.SaveChangesAsync();
        }

        public async Task<List<GetListSubCategoryDto>> GetAllSubCategories()
        {
            return await _kutuphaneDbContext.SubCategories.Include(p => p.CategoryFK)
                .Where(p => !p.IsDeleted)
                .Select(p => new GetListSubCategoryDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    CategoryName = p.CategoryFK.Name
                }).ToListAsync();
        }


        public async Task<GetSubCategoryDto> GetSubCategoryById(int subCategoryId)
        {
            return await _kutuphaneDbContext.SubCategories.Include(p => p.CategoryFK)
                .Where(p => !p.IsDeleted && p.Id == subCategoryId)
                .Select(p => new GetSubCategoryDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    CategoryName = p.CategoryFK.Name
                }).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateSubCategory(UpdateSubCategoryDto updateSubCategoryDto)
        {
            var currentSubcategory = await _kutuphaneDbContext.SubCategories.Where(p => !p.IsDeleted && p.Id == updateSubCategoryDto.Id).FirstOrDefaultAsync();
            if (currentSubcategory == null)
            {
                return -1;
            }
            currentSubcategory.Name = updateSubCategoryDto.Name;
            currentSubcategory.Id = updateSubCategoryDto.Id;
            _kutuphaneDbContext.SubCategories.Update(currentSubcategory);
            return await  _kutuphaneDbContext.SaveChangesAsync();
        }

    }
}
