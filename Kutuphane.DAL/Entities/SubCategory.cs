using AppCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.DAL.Entities
{
    public class SubCategory : Audit , IEntity ,  ISoftDeleted
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Book> Books { get; set; } //Bire çok ilişkide <book>  yazdık cunku sadece orasıyla sınırlandırıp baska yerden bilgi almasın
        public int CategoryId { get; set; }
        public Category CategoryFK { get; set; }
    }
}
