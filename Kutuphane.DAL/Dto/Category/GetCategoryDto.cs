using AppCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.DAL.Dto.Category
{
   public class GetCategoryDto :IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
    }
}
