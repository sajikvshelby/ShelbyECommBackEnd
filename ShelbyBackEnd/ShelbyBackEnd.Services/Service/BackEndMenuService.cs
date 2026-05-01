using ShelbyBackEnd.Infrastructure.Models;
using ShelbyBackEnd.Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShelbyBackEnd.Services.Service
{
    public class BackEndMenuService : IBackEndMenuService
    {
        private readonly ShelbyECommContext _db;

        public BackEndMenuService(ShelbyECommContext db)
        {
            _db = db;
        }

        public async Task<List<Select_BackEnd_MenuResult>> GetAllMenu(CancellationToken cancellationToken = default)
        {
            var menu= await _db.Procedures.Select_BackEnd_MenuAsync(cancellationToken: cancellationToken);
            return menu;
        }

    }
}
