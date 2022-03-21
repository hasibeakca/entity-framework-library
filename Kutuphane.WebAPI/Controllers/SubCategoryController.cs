using Kutuphane.Business.Abstract;
using Kutuphane.Business.Validation.SubCategoryValidator;
using Kutuphane.DAL.Dto.SubCategory;
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
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;
        public SubCategoryController(ISubCategoryService subCategoryService) // YUKARDA YAZDIGIMIZ MEMTHOTLARI CONTROLLERDA TANIMLADIK
        {
            _subCategoryService = subCategoryService;
        }

        [HttpGet]
        [Route("GetSubCategoryList")]

        public async Task<ActionResult<List<GetListSubCategoryDto>>> GetListSubCategory()
        {
            try
            {
                return Ok(await _subCategoryService.GetAllSubCategories());
            }

            catch (Exception hata)
            {
                return BadRequest(hata.Message);
                
            }
        }
        [HttpGet]
        [Route("GetSubCategory/{id:int}")]
        public async Task<ActionResult<GetSubCategoryDto>> GetSubCategory(int id)
        {
            var List = new List<string>();
            if (id <= 0)
            {
                List.Add("SubCategory Id gecersiz");

                return Ok(new { code = StatusCode(1001), Message = List, type = "error" });
            }
            try
            {
                var currentSubCategory = await _subCategoryService.GetSubCategoryById(id);

                if (currentSubCategory == null)
                {
                    List.Add("SubCategory bulunamadı.");
                    return Ok(new { code = StatusCode(1001), message = List, type = "error" });
                }
                else
                {
                    return currentSubCategory;
                }

            }
            catch (Exception hata)
            {

                return BadRequest(hata.Message);
            }
        }
        [HttpPost("AddSubCategory")]
        public async Task<ActionResult<string>> AddSubCategory(AddSubCategoryDto addSubCategoryDto)
        {
            var list = new List<string>();
            var validator = new AddSubCategoryValidator();
            var validationResults = validator.Validate(addSubCategoryDto);
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
                var result = await _subCategoryService.AddSubCategory(addSubCategoryDto);
                if (result > 0)
                {
                    list.Add("EKLEME İŞLEMİ BAŞARILI");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });

                }
                else
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
        [Route("UpdateSubCategory")]
        public async Task<ActionResult<string>> UpdateSubCategory(UpdateSubCategoryDto updateSubCategoryDto)
        {
            var list = new List<string>();
            var validator = new UpdateSubCategoryValidator();
            var validationResults = validator.Validate(updateSubCategoryDto);
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
                var result = await _subCategoryService.UpdateSubCategory(updateSubCategoryDto);
                if (result > 0)
                {
                    list.Add("Guncelleme basarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("SUBCATEGORY BULUNAMADI");
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
        [Route("DeleteSubCategory/{Id:int}")]
        public async Task<ActionResult<string>> DeleteSubCategory(int Id)
        {
            var list = new List<string>(); // mesajın hangı tıpde gerı doncedegını belırttık
            try
            {
                var result = await _subCategoryService.DeleteSubCategory(Id);
                if (result > 0)
                {
                    list.Add("SİLİNME BASARILI");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });


                }
                else if (result == -1)
                {
                    list.Add("SUBCATEGORY BULUNAMADI");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });

                }
                else
                {
                    list.Add("SİLİNME BAŞARISIZ");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }

            }
            catch (Exception hata)
            {

                return BadRequest(hata.Message);
            }

        }
    }
       
    }

