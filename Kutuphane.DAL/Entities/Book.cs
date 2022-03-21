using AppCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.DAL.Entities
{

    public class Book : Audit, IEntity, ISoftDeleted // Book için Ientity yi aktarmak ıstedık gelmediyse referansa bakmayı unutma ya lambadan ya da kutuphane sağ tıklayıp add project reference ye tıkla
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; } //Description Kitabın açıklaması.
        public int SubCategoryId { get; set; }
        public SubCategory SubCategoryFK { get; set; } // 
        public bool IsDeleted { get; set; }
    }
}
