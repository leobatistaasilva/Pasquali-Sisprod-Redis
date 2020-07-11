using Flunt.Notifications;
using Pasquali.Sisprods.Domain.Handlers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pasquali.Sisprods.Domain.Handlers
{
    public abstract class Handler : Notifiable
    {
        protected static bool INVALIDATE_ONE_CACHE;
        protected static bool INVALIDATE_ALL_CACHE;
    }
}
