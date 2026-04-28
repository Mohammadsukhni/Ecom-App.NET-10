using Microsoft.AspNetCore.Http;

namespace Ecom.Core.Dtos
{
    public record ProductDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public required List<PhotoDto> Photos { get; set; }
        public required string CategoryName { get; set; }
    }

    public record PhotoDto
    {
        public required string ImageName { get; set; }
        public int ProductId { get; set; }
    }

    public record AddProductDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public int CategoryId { get; set; }
        public required IFormFileCollection Photo { get; set; }
    }

    public record UpdateProductDto : AddProductDto
    {
        public int Id { get; set; }
    }
}