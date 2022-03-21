using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entity
{
    /// <summary>
    /// Db için silindi mi işareti
    /// </summary>
    public interface ISoftDeleted
    {
        public bool IsDeleted { get; set; }
    }
}
