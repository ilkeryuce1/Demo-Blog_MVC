using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Blog_sites.Models
{
    public class BlogContext:DbContext
        //CodeFirst ile yapıyoruz 
    {
        public BlogContext()
        {
            Database.SetInitializer(new BlogInitializer());
        }

        //public BlogContext():base("yeniblogismi")
        //{
        //    Database.SetInitializer(new BlogInitializer());
        //}
        //yenıblogısmı adında bır verı tabanı olusturdu test verılerı de eklendı 
        public DbSet<Blog> Bloglar { get; set; }
        public DbSet<Category> Kategoriler { get; set; }
    }
}