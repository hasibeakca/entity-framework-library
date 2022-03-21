using AppCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.DAL.Dto.Book
{
    public class UpdateBookDto : IDto
    {
        // Kendı değişkenlerimizi verebılırız ama mapleme yaptıgımız zaman bıze kolaylık sağlıyor
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int SubCategoryId { get; set; }

    }
}
