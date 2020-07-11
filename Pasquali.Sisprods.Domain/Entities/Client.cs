using System;
using System.Collections.Generic;
using System.Linq;

namespace Pasquali.Sisprods.Domain.Entities
{
    public class Client 
    {
        public Client()
        {
            CreateDate = LastUpdateDate = DateTime.Now;
        }
        public Client(string name) :  base()
        {
            Name = name;
            CreateDate = LastUpdateDate = DateTime.Now;
        }

        public int ClientId { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; private set; }
        public DateTime? LastUpdateDate { get; private set; }
        public virtual ICollection<Product> Products { get; set; }


        public void UpdateName(string name)
        {
            if (Name.Equals(name)) return;

            Name = name;
            EntityModified();
        }

        public void EntityModified()
        {
            LastUpdateDate = DateTime.Now;
        }

        public void AddProducts(IEnumerable<Product> products)
        {
            if (products == null || products.ToList().Count <= 0 || products.ToList().Count > 15) return;

            Products = products.ToList();
            EntityModified();
        }

    }
}