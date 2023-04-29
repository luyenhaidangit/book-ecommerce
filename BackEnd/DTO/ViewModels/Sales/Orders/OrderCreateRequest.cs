using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModels.Sales.Orders
{
    public class OrderCreateRequestOrderDetail
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }

    public class OrderCreateRequest
    {
        public int CustomerId { get; set; }

        public string Andress { get; set; }

        public string Note { get; set; }

        //public DateTime OrderDate { get; set; }

        public int Status { get; set; }

        public List<OrderCreateRequestOrderDetail> OrderDetails { get; set; }

    }
}
