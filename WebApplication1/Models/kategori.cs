//------------------------------------------------------------------------------
// <auto-generated>
//    Bu kod bir şablondan oluşturuldu.
//
//    Bu dosyada el ile yapılan değişiklikler uygulamanızda beklenmedik davranışa neden olabilir.
//    Kod yeniden oluşturulursa, bu dosyada el ile yapılan değişikliklerin üzerine yazılacak.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class kategori
    {
        public kategori()
        {
            this.urunler = new HashSet<urunler>();
        }
    
        public int id { get; set; }
        public string ad { get; set; }
    
        public virtual ICollection<urunler> urunler { get; set; }
    }
}
