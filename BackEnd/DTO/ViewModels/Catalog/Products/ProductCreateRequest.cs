using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModels.Catalog.Products
{
    public class ProductCreateRequestProductImage
    {
        public string Url { get; set; }

        public string Title { get; set; }
    }

    public class ProductCreateRequest
    {
        public string Name { get; set; }

        public string? Image { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal DiscountPrice { get; set; }

        public string? Description { get; set; }

        public int Quantity { get; set; }

        public int ProductCategoryId { get; set; }

        public int BrandId { get; set; }

        public List<ProductCreateRequestProductImage> ProductImages { get; set; }
    }
}
