using System;
using System.Collections.Generic;

namespace Emart.UserService.Models
{
    public partial class Items
    {
        public Items()
        {
            PurchaseHistory = new HashSet<PurchaseHistory>();
        }

        public string Iid { get; set; }
        public string Cid { get; set; }
        public string Scid { get; set; }
        public int Price { get; set; }
        public string Itemname { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public string Remarks { get; set; }
        public byte[] Photo { get; set; }
        public string Sid { get; set; }

        public virtual Category C { get; set; }
        public virtual Seller S { get; set; }
        public virtual SubCategory Sc { get; set; }
        public virtual ICollection<PurchaseHistory> PurchaseHistory { get; set; }
    }
}
