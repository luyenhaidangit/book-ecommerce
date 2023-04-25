using DTO.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModels.Catalog.ProductCategories
{
    public class ProductCategoryPagingManageGetRequest : PagingRequestBase
    {
        public int? ParentProductCategoryId { get; set; }

        public string? Name { get; set; }

        public string? SortBy { get; set; }

        public string? OrderBy { get; set; }
    }
}
