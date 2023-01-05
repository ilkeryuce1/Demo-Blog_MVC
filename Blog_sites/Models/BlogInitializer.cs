using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Blog_sites.Models
{
    //Initializer sınıfı veri tabanı şeması ıcındekı degisikliklieri kendi otomatık son şemaya uydurması 
    //test verilerini otomatık olarak kedı ekler 
    public class BlogInitializer:DropCreateDatabaseIfModelChanges<BlogContext>
    //DropCreateDatabaseIfModelChanges mevcut tablolardakı tabloları değişiklik olduunda sıler ve tekrar oluşturur
    {
        protected override void Seed(BlogContext context)
        {

            List<Category> kategoriler = new List<Category>
            {
                new Category(){KategoriAdi ="C#"},
                new Category(){KategoriAdi ="ASP.NET MVC"},
                new Category(){KategoriAdi ="ASP.NET WebForms"},
                new Category(){KategoriAdi ="ASP.NET Windows Form"}


            };
            foreach (var item in kategoriler)
            {
                context.Kategoriler.Add(item);
            }
            context.SaveChanges();


            List<Blog> bloglar = new List<Blog>()
            {
                new Blog(){Baslik ="C# Delegates Hakkında",Aciklama="Açıklamamız",EklenmeTarihi=DateTime.Now.AddDays(-10),anasayfa=true,onay=true,Icerik="İçeriklerimiz buardıar",Resim="1.jpg",CategoryId=1},
                new Blog(){Baslik ="C# Delegates Hakkında",Aciklama="Açıklamamız",EklenmeTarihi=DateTime.Now,anasayfa=true,onay=false,Icerik="İçeriklerimiz buardıar",Resim="1.jpg",CategoryId=1},
                new Blog(){Baslik ="C# Delegates Hakkında",Aciklama="Açıklamamız",EklenmeTarihi=DateTime.Now,anasayfa=false,onay=true,Icerik="İçeriklerimiz buardıar",Resim="1.jpg",CategoryId=1},
                new Blog(){Baslik ="C# Delegates Hakkında 2",Aciklama="Açıklamamız",EklenmeTarihi=DateTime.Now,anasayfa=true,onay=false,Icerik="İçeriklerimiz buardıar",Resim="2.jpg",CategoryId=2},
                new Blog(){Baslik ="C# Delegates Hakkında2",Aciklama="Açıklamamız",EklenmeTarihi=DateTime.Now,anasayfa=true,onay=true,Icerik="İçeriklerimiz buardıar",Resim="2.jpg",CategoryId=2},
                new Blog(){Baslik ="C# Delegates Hakkında2",Aciklama="Açıklamamız",EklenmeTarihi=DateTime.Now,anasayfa=false,onay=true,Icerik="İçeriklerimiz buardıar",Resim="2.jpg",CategoryId=2},
                new Blog(){Baslik ="C# Delegates Hakkında 3",Aciklama="Açıklamamız",EklenmeTarihi=DateTime.Now,anasayfa=true,onay=true,Icerik="İçeriklerimiz buardıar",Resim="1.jpg",CategoryId=3},
                new Blog(){Baslik ="C# Delegates Hakkında 3",Aciklama="Açıklamamız",EklenmeTarihi=DateTime.Now,anasayfa=false,onay=true,Icerik="İçeriklerimiz buardıar",Resim="3.jpg",CategoryId=3},
                new Blog(){Baslik ="C# Delegates Hakkında 3",Aciklama="Açıklamamız",EklenmeTarihi=DateTime.Now,anasayfa=true,onay=false,Icerik="İçeriklerimiz buardıar",Resim="3.jpg",CategoryId=3},
                new Blog(){Baslik ="C# Delegates Hakkında 3",Aciklama="Açıklamamız",EklenmeTarihi=DateTime.Now,anasayfa=false,onay=true,Icerik="İçeriklerimiz buardıar",Resim="4.jpg",CategoryId=4},
                new Blog(){Baslik ="C# Delegates Hakkında 4",Aciklama="Açıklamamız",EklenmeTarihi=DateTime.Now,anasayfa=true,onay=true,Icerik="İçeriklerimiz buardıar",Resim="4.jpg",CategoryId=4},
                new Blog(){Baslik ="C# Delegates Hakkında 4 ",Aciklama="Açıklamamız",EklenmeTarihi=DateTime.Now,anasayfa=false,onay=true,Icerik="İçeriklerimiz buardıar",Resim="4.jpg",CategoryId=4}
            };
            foreach (var item in bloglar)
            {
                context.Bloglar.Add(item);
            }
            context.SaveChanges();
             base.Seed(context);//test verileri ekleme imkanı verir
        }
    }
}