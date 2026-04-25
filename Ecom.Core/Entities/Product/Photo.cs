using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Entities.Product
{
    public class Photo : BaseEntity
    {
        public string ImageName { get; set; }
        public int ProductId { get; set; }
      //  public virtual Product Product { get; set; }
    }
}
