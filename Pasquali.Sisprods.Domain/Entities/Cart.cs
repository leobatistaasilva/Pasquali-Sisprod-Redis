using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pasquali.Sisprods.Domain.Entities
{
    public class Cart : Entity
    {
        public Cart() : base()
        {
            
        }

        //public Cart(Guid sessionGuid) : base()
        //{
        //    SessionGuid = sessionGuid;
        //    CreateDate = LastUpdateDate = DateTime.Now;
        //}

        //public int CartId { get; private set; }
        //public Guid SessionGuid { get; private set; }

        //public DateTime? CreateDate { get; private set; }
        //public DateTime? LastUpdateDate { get; private set; }

        //public virtual ICollection<Product> Products { get; set; }


        //public void UpdateName(string name)
        //{
        //    if (Name.Equals(name)) return;

        //    Name = name;
        //    EntityModified();
        //}

        //public void EntityModified()
        //{
        //    LastUpdateDate = DateTime.Now;
        //}

        //public void AddProducts(IEnumerable<Product> products)
        //{
        //    if (products == null || products.ToList().Count <= 0 || products.ToList().Count > 15) return;

        //    Products = products.ToList();
        //    EntityModified();
        //}


        public override void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsNotEmpty(Id, "Id", "Carrinho sem identificador da sessao.")
            );
        }
    }
}
