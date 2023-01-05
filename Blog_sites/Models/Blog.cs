﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog_sites.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string Resim { get; set; }
        public string Icerik { get; set; }
        public DateTime EklenmeTarihi { get; set; }
        public bool onay { get; set; }
        public bool anasayfa { get; set; }


        public int CategoryId { get; set; }
        public Category Category { get; set; }


    }
}