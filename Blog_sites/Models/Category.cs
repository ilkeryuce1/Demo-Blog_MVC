using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog_sites.Models
{
    public class Category
    {
        //ID bolumu yerıne baska bısey yazılırsa bılınclı oalarak yapıldıını bıldırmek ıcın [key] eklenmelidir
        public int Id { get; set; }
        public string KategoriAdi { get; set; }


        public List<Blog> Bloglar { get; set; }

        //kategorıye aıt olan tum bloklar burada geldi

    }
}