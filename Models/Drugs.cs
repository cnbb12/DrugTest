﻿using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table(Name = "Drugs")]
    public class Drugs
    {
        [Column(IsPrimaryKey = true)]
        public Guid ID { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Effect { get; set; }

        [Column]
        public string ExpirationDate { get; set; }

        [Column]
        public string Remark { get; set; }

        [Column]
        public Guid OwnerId { get; set; }

        [Column]
        public int RemainDay { get; set; }
    }
}
