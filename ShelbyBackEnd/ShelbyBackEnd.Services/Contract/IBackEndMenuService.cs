using ShelbyBackEnd.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShelbyBackEnd.Services.Contract
{
    public interface IBackEndMenuService
    {
        public Task<List<Select_BackEnd_MenuResult>> GetAllMenu(CancellationToken cancellationToken = default);
    }
}
