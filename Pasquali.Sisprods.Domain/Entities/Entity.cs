using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace Pasquali.Sisprods.Domain.Entities
{
    public abstract class Entity : Notifiable, IEquatable<Entity>, IValidatable
    {
        public Entity()
        {
            Id = Guid.NewGuid();
            CreateDate = LastUpdateDate = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public DateTime? CreateDate { get; private set; }
        public DateTime? LastUpdateDate { get; private set; }

        public abstract void Validate();

        public bool Equals(Entity other)
        {
            return Id == other.Id;
        }
        public void EntityModified()
        {
            LastUpdateDate = DateTime.Now;
        }
    }
}