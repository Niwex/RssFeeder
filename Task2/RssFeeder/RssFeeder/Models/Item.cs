﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssFeeder.Models
{
    public class Item
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime PubDate { get; set; }
    }
}
