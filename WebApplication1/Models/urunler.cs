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
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;
    using System.Web.Mvc;

    public partial class urunler
    {
        public int id { get; set; }
        public string foto { get; set; }
        [NotMapped]
        public virtual HttpPostedFileBase fotoFile { get; set; }
        public string baslik { get; set; }
        public string ustbaslik { get; set; }
        [AllowHtml]
        public string icerik { get; set; }
        public byte aktifmi { get; set; }

        public virtual bool aktif
        {
            get { return aktifmi > 0; }
            set
            {
                if (value)
                {
                    aktifmi = 1;
                }
                else
                {
                    aktifmi = 0;
                }
            }
        }
        public int sira { get; set; }
        public int katId { get; set; }
    
        public virtual kategori kategori { get; set; }
    }
}
