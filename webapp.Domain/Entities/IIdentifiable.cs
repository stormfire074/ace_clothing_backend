using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webapp.Domain
{
    public interface IIdentifiable
    {
        int Id { get; set; }
    }
}
