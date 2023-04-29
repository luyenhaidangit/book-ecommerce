using DTO.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModels.Catalog.Products
{
    public class ProductPagingManageGetRequest : PagingRequestBase
    {
        public string? Name { get; set; }

        public decimal? StartPrice { get; set; }

        public decimal? EndPrice { get; set; }

        public string? OrderBy { get; set; }

        public string? SortBy { get; set; }
    }
}
