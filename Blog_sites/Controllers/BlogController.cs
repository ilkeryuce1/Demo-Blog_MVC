using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog_sites.Models;

namespace Blog_sites.Controllers
{
    public class BlogController : Controller
    {
        private BlogContext db = new BlogContext();

        public ActionResult List(int? id ,string q)//idsiz islemde yapılacagını anlatır 
        {
            var bloglar = db.Bloglar
                                .Where(i => i.onay == true)

                .Select(i => new BlogModel()
                //Her blogun ılgılı modellerı ile sayfa dolduruulacak 
                {
                    Id = i.Id,
                    Baslik = i.Baslik.Length > 100 ? i.Baslik.Substring(0, 50) + "..." : i.Baslik,
                    Aciklama = i.Aciklama,
                    EklenmeTarihi = i.EklenmeTarihi,
                    anasayfa = i.anasayfa,
                    onay = i.onay,
                    Resim = i.Resim,
                    CategoryId=i.CategoryId
                }).AsQueryable();//sorgudan sonra ekstra wherelerı eklemımızı saglar 

            if (string.IsNullOrEmpty("q")==false)
            {
                bloglar = bloglar.Where(i => i.Baslik.Contains(q) || i.Aciklama.Contains(q));
            }

            if (id!=null)
            {
                bloglar = bloglar.Where(i => i.CategoryId == id);
            }

            return View(bloglar.ToList());
        }

        // GET: Blog
        public ActionResult Index()
        {
            var bloglar = db.Bloglar.Include(b => b.Category).OrderByDescending(i=>i.EklenmeTarihi);
            return View(bloglar.ToList());
        }

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Bloglar.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Kategoriler, "Id", "KategoriAdi");
            // kategorı adının ıdsını selectlistolusturup viewbagın categoryıdsıne aktarıyor
            //Create view de dew baktıgımızda dropdownlistte ıd yerıne ısımlerı getırıyor 
           
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Baslik,Aciklama,Resim,Icerik,CategoryId")] Blog blog)
            //eklenırken kontrol edılmesı gereken degerler yazıldı
        {
            if (ModelState.IsValid)
            {
                blog.EklenmeTarihi=DateTime.Now;
              

                db.Bloglar.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
                //eklenme ıslemı yapılcak eklenme ıslemı yapıldıaktan sonra kullanıcı anasayfaya yoneldnrırılıceek
            }

            ViewBag.CategoryId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", blog.CategoryId);
            return View(blog);
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //KULLANICININ GIRDIGI VEILER HATALI DA OLSA FORM TEKRAR YUKLENECEK VE FORM DOLDURULUCAK
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Bloglar.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", blog.CategoryId);
            return View(blog);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Baslik,Aciklama,Resim,Icerik,onay,anasayfa,CategoryId")] Blog blog)
            //Blogdan gelen degerı tuttugumuz yer blog nesnesıdır 
        {
            if (ModelState.IsValid)
            {
            //    blog.EklenmeTarihi = DateTime.Now;
            //    db.Entry(blog).State = EntityState.Modified;  
            //Buradaki kodları asagıdakıyle degeistirirsek sadece istedıgımız yerlerı guncelleriz

                var  entıty=db.Bloglar.Find(blog.Id);
                //finde metoduyla sorulama yapıcagız
                //WebClient tabanındakı kaydımız 
                if (entıty!=null)
                {
                    entıty.Baslik=blog.Baslik;
                    entıty.Aciklama=blog.Aciklama;
                    entıty.Resim=blog.Resim;
                    entıty.Icerik=blog.Icerik;
                    entıty.onay=blog.onay;
                    entıty.anasayfa=blog.anasayfa;
                    entıty.CategoryId=blog.CategoryId;

                    //eger ıd null degil ise formdan gonderilen degerı entity den gelen degerle degistiricez

                    db.SaveChanges();



                    TempData["Blog"] = entıty;//Editlediğimiz ogeyı tempdata ıle tasiyacagız

                    //!!ViewBag ile tasınmak ıstendıgınde redirectoActıon metodunda vıec-vbadakı verıler sıfırlanır
                    


                    //GUNCELLEME GERCEKLESTIGINDE HANGİ KAYDIN GUNCELLENDIGINI KULLANICIYA OSTERMEK ICIN 
                    return RedirectToAction("Index"); //kULLANILYOR SONRA BLOG INDEX 10. SATIRA GECTIK
                    //veriler kaydedılıyor ve kullanıcı ındexe gonderiliyor

                }

            }
            ViewBag.CategoryId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", blog.CategoryId);
            return View(blog);
            //modelstateye gırmez ıse degerler yanlıs dahı olsa deerler paketlenıp edıt vıevıne tekrar gonderılecek ilk defa httpget calısıyormus gıbı calısır fakat guncelleme ıslemı yapmaz 
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Bloglar.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Bloglar.Find(id);
            db.Bloglar.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
