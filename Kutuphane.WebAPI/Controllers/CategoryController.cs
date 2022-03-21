using Kutuphane.Business.Abstract;
using Kutuphane.Business.Validation.Category;
using Kutuphane.DAL.Dto.Book;
using Kutuphane.DAL.Dto.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        [Route("GetCategoryList")]

        public async Task<ActionResult<List<GetListCategoryDto>>> GetCategoryList()
        {
            try
            {
                return Ok(await _categoryService.GetListCategories());

            }

            catch (Exception hata)
            {

                return BadRequest(hata.Message);
            }
        }

        [HttpGet]
        [Route("GetCategoryById/{id:int}")]

        public async Task<ActionResult<GetCategoryDto>> GetCategory(int id)
        {
            var List = new List<string>();

            if (id <= 0)
            {
                List.Add("Category id geçersiz");
                return Ok(new { code = StatusCode(1001), message = List, type = "error" });
            }
            try
            {
                var currentCategory = await _categoryService.GetCategoryById(id);

                if (currentCategory == null)
                {
                    List.Add("Category bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = List, type = "error" });
                }
                else
                {
                    return currentCategory;
                }

            }
            catch (Exception hata)
            {

                return BadRequest(hata.Message);
            }

        }
        [HttpPost("AddCategory")]
        public async Task<ActionResult<string>> AddCategory(AddCategoryDto addCategoryDto)
        {
            var list = new List<string>();
            var validator = new AddCategoryValidator();
            var validationResults = validator.Validate(addCategoryDto);
            if (!validationResults.IsValid)
            {
                foreach(var error in validationResults.Errors)
                {
                    list.Add(error.ErrorMessage);
                }
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }
            try
            {
                var result = await _categoryService.AddCategory(addCategoryDto);
                if (result > 0)
                {
                    list.Add("EKLEME İŞLEMİ BAŞARILI");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });

                }else
                {
                    list.Add("EKLEME İŞLEMİ BAŞARISIZ.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }

            }
            catch (Exception hata)
            {

                return BadRequest(hata.Message);
            }
        }
        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<ActionResult<string>> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var list = new List<string>();
            var validator = new UpdateCategoryValidator();
            var validationResults = validator.Validate(updateCategoryDto);
            if (!validationResults.IsValid)
            {
                foreach(var error in validationResults.Errors)
                {
                    list.Add(error.ErrorMessage);
                }
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }
            try
            {
                var result = await _categoryService.UpdateCategory(updateCategoryDto);
                if (result > 0)
                {
                    list.Add("Guncelleme basarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("CATEGORY BULUNAMADI");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    list.Add("Guncelleme basarısız");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }


            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteCategory/{Id:int}")]
        public async Task<ActionResult<string>> DeleteCategory(int Id)
        {
            var list = new List<string>(); // mesajın hangı tıpde gerı doncedegını belırttık
            try
            {
                var result = await _categoryService.DeleteCategory(Id);
                if (result > 0)
                {
                    list.Add("SİLİNME BASARILI");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });


                }
                else if (result == -1)
                {
                    list.Add("CATEGORY BULUNAMADI");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });

                }
                else
                {
                    list.Add("SİLİNME BAŞARISIZ");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }

            }
            catch (Exception t)
            {

                return BadRequest(t.Message);
            }

        }
    }
}
