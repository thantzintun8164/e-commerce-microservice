using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly ICustomersService customersService;

        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.customersService = customersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            var productResult = await productsService.GetProductsAsnyc();
            var customersResult = await customersService.GetCustomerAsnyc(customerId);
            //if(ordersResult.IsSuccess)
            //{
            //    foreach(var order in ordersResult.Orders)
            //    {
            //        foreach(var product in productResult.Products)
            //        {
            //            product.Name = productResult.IsSuccess ?
            //                productResult!.Products!.FirstOrDefault(p => p.Id == product.Id)?.Name :
            //                "Product Information is not available";
            //        }
            //    }
                
            //    var result = new
            //    {
            //        Customer = customersResult.IsSuccess ?
            //                    customersResult.Customer :
            //                    null,
            //        Orders = ordersResult.Orders
            //    };
            //    return (true, result);

            //}
            if(customersResult.IsSuccess)
            {
                var result = new
                {
                    Customer = customersResult.IsSuccess ?
                                customersResult.Customer :
                                null,
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
