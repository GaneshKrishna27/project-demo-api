using System;
using System.Collections.Generic;

namespace Emart.UserService.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            Items = new HashSet<Items>();
        }

        public string Scid { get; set; }
        public string SubCategoryname { get; set; }
        public string Cid { get; set; }
        public string Briefdetails { get; set; }
        public string Gst { get; set; }

        public virtual Category C { get; set; }
        public virtual ICollection<Items> Items { get; set; }
    }
}
