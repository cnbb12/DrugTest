using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table(Name = "User")]
    public class User
    {
        [Column]
        public Guid ID { get; set; }

        [Column]
        public string PhoneNumber { get; set; }

        [Column]
        public string Password { get; set; }

        [Column]
        public int xUtils_id { get; set; }
    }
}
