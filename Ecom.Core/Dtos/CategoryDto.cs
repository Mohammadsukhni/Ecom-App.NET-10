using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Dtos
{
    public record CategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        


    }
    public record UpdateCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
