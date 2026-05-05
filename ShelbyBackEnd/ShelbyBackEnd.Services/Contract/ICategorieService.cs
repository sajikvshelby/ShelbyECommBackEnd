using ShelbyBackEnd.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShelbyBackEnd.Services.Contract
{
    public interface ICategorieService
    {

        public Task<IEnumerable<Select_All_CategoriesResult>> GetAllCategories(int? ParentCatID = 0, int? categoryid = 0, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Select_Sort_By_ListResult>> GetSortByList(CancellationToken cancellationToken = default);
        public  Task<bool> Insert_Categories(Select_All_CategoriesResult category, CancellationToken cancellationToken = default);
        public  Task<bool> Update_Categories(Select_All_CategoriesResult category, CancellationToken cancellationToken = default);

        public  Task<bool> Archive_CategoriesAsync(int category_id, int modified_by, CancellationToken cancellationToken = default);



    }
}
