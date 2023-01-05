using Blog_sites.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog_sites.Controllers
{
    public class HomeController : Controller
    {
        private BlogContext context=new BlogContext();
        public ActionResult Index()
        {
            var bloglar = context.Bloglar
                                .Where(i => i.onay == true && i.anasayfa == true)

                .Select(i => new BlogModel()
                //Her blogun ılgılı modellerı ile sayfa dolduruulacak 
                {
                    Id = i.Id,
                    Baslik = i.Baslik.Length > 100 ? i.Baslik.Substring(0, 50) + "..." : i.Baslik,
                    Aciklama = i.Aciklama,
                    EklenmeTarihi = i.EklenmeTarihi,
                    anasayfa = i.anasayfa,
                    onay = i.onay,
                    Resim = i.Resim
                });
            
            return View(bloglar.ToList());
        }


    }
}