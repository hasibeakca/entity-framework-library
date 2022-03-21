using Kutuphane.DAL.Dto.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Business.Abstract
{
    public interface IBookService
    {
        Task<List<GetListBookDto>> GetAllBooks();
        Task<GetBookDto> GetBookById(int bookId);
        Task<int> AddBook(AddBookDto addBookDto);
        Task<int> UpdateBook(UpdateBookDto updateBookDto);
        Task<int>  DeleteBook(int bookId);
    }
}
