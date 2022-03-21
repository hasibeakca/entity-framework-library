using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entity
{
    /// <summary>
    /// Daha sonra birşey ekleyebilmek için audit oluşturduk
    /// </summary>
    public abstract class Audit
    {
        /// <summary>
        /// CDate Create date demek. OLUŞTURULMA TARİHİ
        /// </summary>
        public DateTime CDate { get; set; } = DateTime.Now; 

        /// <summary>
        /// MDate modify date DEĞİŞTİRİLME TARİHİ
        /// </summary>
        public DateTime MDate { get; set; }
        public int CUserId { get; set; }
        public int MUserId { get; set; }
    }
}
