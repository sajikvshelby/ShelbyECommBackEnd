using ShelbyBackEnd.Infrastructure.Models;
using ShelbyEComm.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShelbyBackEnd.Services.Contract
{
    public interface IProductService
    {

        public Task<PaginatedList<Select_All_Products_ListResult>> GetAllProducts(int? pageNumber, int? pageSize, CancellationToken cancellationToken = default);
    }
}
