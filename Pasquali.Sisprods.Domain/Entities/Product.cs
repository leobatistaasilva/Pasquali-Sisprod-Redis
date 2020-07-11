using System;
using System.Collections.Generic;

namespace Pasquali.Sisprods.Domain.Entities
{
    public class Product 
    {
        public Product()
        {
            CreateDate = LastUpdateDate = DateTime.Now;
        }

        public Product(string name) : base()
        {
            Name = name;
            CreateDate = LastUpdateDate = DateTime.Now;
            GenerateProductCode();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; private set; }
        public DateTime? LastUpdateDate { get; private set; }
        public string ProductSKU { get; private set; }
        public virtual ICollection<Client> Clients { get; set; }

        public void UpdateName(string name)
        {
            if (string.IsNullOrEmpty(name) || Name.Equals(name)) return;

            Name = name;
            EntityModified();
        }

        public void EntityModified()
        {
            LastUpdateDate = DateTime.Now;
        }

        private void GenerateProductCode()
        {
            if (ProductSKU != null) return;

            string secondDataGuid = Guid.NewGuid().ToString().Split('-')[1];
            int? lengthMili = CreateDate?.TimeOfDay.TotalMilliseconds.ToString().Length;
            string lastFourMilisecondsCreation = CreateDate?
                                                    .TimeOfDay
                                                        .TotalMilliseconds
                                                            .ToString()
                                                                .Substring((int)(lengthMili - 3), 3);

            string automaticSystemCode = (secondDataGuid + lastFourMilisecondsCreation).ToUpper();

            ProductSKU = automaticSystemCode;
        }
    }
}