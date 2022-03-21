using Kutuphane.Business.Abstract;
using Kutuphane.DAL.Context;
using Kutuphane.DAL.Dto.Book;
using Kutuphane.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Business.Concrete
{
    public class BookService : IBookService // Abstract klasorundekı ınterfaceyı ımplement edıyoruz
    {
        private readonly KutuphaneDbContext _kutuphaneDbContext; // veritabanına bağlanmak ve tablolara erişmek için kullanıyoruz. 
        public BookService(KutuphaneDbContext kutuphaneDbContext) // Constructor - YAPICI 
        {
            _kutuphaneDbContext = kutuphaneDbContext; // boş değere değer atadık . x = 5;
        }

        public async Task<List<GetListBookDto>> GetAllBooks() // sankı verıtabanında select*table gosterır gıbı dusun
        {
            return await _kutuphaneDbContext.Books.Include(p => p.SubCategoryFK).ThenInclude(p => p.CategoryFK) // INCLUDE ıkı klası joınlıyor
                .Where(p => !p.IsDeleted).Select(p => new GetListBookDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    SubCategoryName = p.SubCategoryFK.Name,
                    CategoryName = p.SubCategoryFK.CategoryFK.Name
                }).ToListAsync();
            //************* ThenInclude demek aslında normalde baglantılı olmadıgı entitilerimizin arasında dolaylı yoldan ılıskı kurmaya yarıyor.
            //  kaydetmeye calısma varolan lıste geldı değişiklik yok
        }
        public async Task<GetBookDto> GetBookById(int bookId)
        {
            return await _kutuphaneDbContext.Books.Include(p => p.SubCategoryFK) // RETURN GERI DEGER DONDURUR
                .Where(p => !p.IsDeleted && p.Id == bookId).Select(p => new GetBookDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Desc = p.Desc,
                    SubCategoryName = p.SubCategoryFK.Name
                }).FirstOrDefaultAsync(); // İLK BULDUGUNU GETİR (DERLEYICI VE YORUMLAYICI GIBI DUSUN) SINGLE BULDUKTAN SONRADA DEVAM EDER( HEPSİNİ TARAYIP ) FIRST ILK BULDUGUNU GETIRIR
        }
        public async Task<int> AddBook(AddBookDto addBookDto) // kednısı geldı
        {
            var addingBook = new Book  // Yeni bir değişken oluşturuyoruz
            {
                Name = addBookDto.Name, // Book içerisine ne tanımladıysak buraya ekledik. (DTO DAN ATAMA YAPIYORUZ)   DIŞARDAN ALINAN NESNEYI DTO YA GONDERIYORUZ BÖYLE EKLİYORUZ
                Desc = addBookDto.Desc,
                SubCategoryId = addBookDto.SubCategoryId
            };

            await _kutuphaneDbContext.Books.AddAsync(addingBook);//  DBCONTEXT ARACILIĞIYLA BOOK TABLOSUNA EKLEME İŞLEMİ
            return await _kutuphaneDbContext.SaveChangesAsync(); // KAYDETME KOMUTU BU YAZILMADAN KAYDEDILMEZ
        }

        public async Task<int> UpdateBook(UpdateBookDto updateBookDto) // GUNCELLEME OLAYINI SERVISE EKLIYORUZ
        {
            var currentBook = await _kutuphaneDbContext.Books.Where(p => !p.IsDeleted && p.Id == updateBookDto.Id).FirstOrDefaultAsync(); // VERITABANDAN VCERI ALICAZ ULAŞTIĞIMIZ YER DBCONTEXT BUNUDA BIZ YUKARDA _ KUTUPHANEDBCONTEXT OLARAK TANIMLADIK. O YUZDEN DEĞİŞKENİNİ BURAYA YAZIP ALDIK
                                                                                                                                          //SİLİNMEMİŞ VE DIŞARDAN ALDIGIMIZ ID VE VERITABANINDAKI ID EŞLEŞİYOR MU
            if (currentBook == null)
            {
                return -1;
            }
            currentBook.Name = updateBookDto.Name;
            currentBook.Desc = updateBookDto.Desc;
            currentBook.SubCategoryId = updateBookDto.SubCategoryId;

            _kutuphaneDbContext.Books.Update(currentBook);
            return await _kutuphaneDbContext.SaveChangesAsync();
        }


        public async Task<int> DeleteBook(int bookId)
        {
            var currentBook = await _kutuphaneDbContext.Books.Where(p => !p.IsDeleted && p.Id == bookId).FirstOrDefaultAsync();
            if (currentBook == null)
            {
                return -1;
            }

            currentBook.IsDeleted = true;
            _kutuphaneDbContext.Books.Update(currentBook);
            return await _kutuphaneDbContext.SaveChangesAsync();
        }
    }
}
