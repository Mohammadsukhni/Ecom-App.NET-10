using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Infrastructure.Repositries
{
    public class PhotoRepository : GenericRepositories<Photo>, IPhotoRepository
    {
        public PhotoRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
