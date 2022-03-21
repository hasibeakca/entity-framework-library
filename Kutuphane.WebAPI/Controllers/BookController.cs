using Kutuphane.Business.Abstract;
using Kutuphane.Business.Concrete;
using Kutuphane.Business.Validation.Book;
using Kutuphane.DAL.Dto.Book;
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
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService; //SERVİSİN METOTLARINI KULLANMAK ICIN BOŞ BIR DEGER OLUŞTURDUK DEĞİŞKEN.

        public BookController(IBookService bookService) // METOTU TANIMLADIK
        {
            _bookService = bookService; // METOTUN ICINI DOLDURDUK
        }

        [HttpGet]
        [Route("GetBookList")]

        public async Task<ActionResult<List<GetListBookDto>>> GetBookList() // SERVISTEN TEK FARKI ACTION RESULT İSTEKLERI GETIRIR (ISTEDIGIMIZ ISTEKLERI) STATUC (HATA KODLARINI KULLANMAMIZI SAĞLIYOR) KODLARI KULLANMAMIZA IZIN VERIYOR

        {
            try
            {
                return Ok(await _bookService.GetAllBooks()); // ÇALIŞTIR
            }
            catch (Exception hatabulundu) // YAKALARSAN ADI NE OLSUN DEGISKENINI VER
            {
                return BadRequest(hatabulundu.Message); // SONUCU HATA VARSA BANA DONDUR
            }
        }
        [HttpGet]
        [Route("GetBookById/{Id:int}")]

        public async Task<ActionResult<GetBookDto>> GetBook(int Id)
        {
            var list = new List<string>();
            if (Id <= 0)
            {
                list.Add("Kitap Id geçersiz.");
                return Ok(new { code = StatusCode(1001), message = list, type = "error" }); // OK ICINDE DONDURME SEBEBI KUCUK BIR HATA OLDUGUNDAN BEIRLENEN KODLA (1001) GERI DONUS ALIYORUZ

            }
            try
            {
                var currentBook = await _bookService.GetBookById(Id);
                if (currentBook == null)
                {
                    list.Add("KİTAP BULUNAMADI.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });

                }
                else
                {
                    return currentBook;
                }
            }
            catch (Exception hatabulundu1)
            {
                return BadRequest(hatabulundu1.Message);
            }

        }
        [HttpPost("AddBook")] // roatla yazdıgımızı bu sekılde yazmak aynı sey
        public async Task<ActionResult<string>> AddBook(AddBookDto addBookDto)
        {
            var list = new List<string>();
            var Validator = new AddBookValidator();
            var validationResults = Validator.Validate(addBookDto);
            if (!validationResults.IsValid)
            {
                foreach(var error in validationResults.Errors)
                {
                    list.Add(error.ErrorMessage);
                }
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }
            // değişikliğin adını tek tek yazmak yerıne dızı seklınde tanımlarız hataları dondurmenın ıcıne yazarız

            try
            {
                var result = await _bookService.AddBook(addBookDto);
                if (result > 0)
                {
                    list.Add("Ekleme işlemi başarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else
                {
                    list.Add("EKLEME ISLEMI BASARISIZ.");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }

            }
            catch (Exception hatabulundu1)
            {


                return BadRequest(hatabulundu1.Message);
            }
        }
        [HttpPut]
        [Route("UpdateBook")]
        public async Task<ActionResult<string>> UpdateBook(UpdateBookDto updateBookDto)
        {
            var list = new List<string>();
            var validator = new UpdateBookValidator();
            var validationResults = validator.Validate(updateBookDto);
            if (!validationResults.IsValid)
            {
                foreach(var error in validationResults.Errors)
                {
                    list.Add(error.ErrorMessage);
                }
                return Ok(new { code = StatusCode(1002), messsage = list, type = "error" });
            }

            try
            {
                var result = await _bookService.UpdateBook(updateBookDto);
                if (result > 0)
                {
                    list.Add("Guncelleme basarılı.");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("KITAP BULUNAMADI");
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
        [Route("DeleteBook/{Id:int}")]
        public async Task<ActionResult<string>> DeleteBook(int Id)
        {
            var list = new List<string>(); // mesajın hangı tıpde gerı doncedegını belırttık
            try
            {
                var result = await _bookService.DeleteBook(Id);
                if (result > 0)
                {
                    list.Add("SİLİNME BASARILI");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });


                }
                else if (result == -1)
                {
                    list.Add("KİTAP BULUNAMADI");
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
