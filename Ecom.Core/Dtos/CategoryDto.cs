using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Dtos
{
    public record CategoryDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }

    public record UpdateCategoryDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
