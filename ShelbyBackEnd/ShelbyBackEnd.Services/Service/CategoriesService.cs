using ShelbyBackEnd.Infrastructure.Models;
using ShelbyBackEnd.Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShelbyBackEnd.Services.Service
{
    public class CategoriesService : ICategorieService
    {
        private readonly ShelbyECommContext _db;
     


        public CategoriesService(ShelbyECommContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Select_All_CategoriesResult>> GetAllCategories(int? ParentCatID = 0, int? categoryid=0, CancellationToken cancellationToken = default)
        {
            return await _db.Procedures.Select_All_CategoriesAsync(ParentCatID ?? 0, categoryid??0, cancellationToken: cancellationToken);
        }
         public async Task<IEnumerable<Select_Sort_By_ListResult>> GetSortByList(CancellationToken cancellationToken = default)
        {
            return await _db.Procedures.Select_Sort_By_ListAsync(cancellationToken: cancellationToken);
        }



        public async Task<bool> Insert_Categories(Select_All_CategoriesResult category, CancellationToken cancellationToken = default)
        {
            int returnValue = await _db.Procedures.Insert_CategoriesAsync(
            category.parent_category_id
           , category.category_name
           , category.display_order
           , category.default_sort_by_id
           , category.hidden
           , category.category_placement
           , category.display_subcat_items
           , category.alt_url
           , category.alt_url_target
           , category.link_title
           , category.meta_title
           , category.meta_desc
           , category.meta_keywords
           , category.search_tags
           , category.category_desc
           , category.category_desc_hidden
           , category.category_short_desc
           , category.category_short_desc_hidden
           , category.category_secondary_desc
           , category.category_secondary_desc_hidden
           , category.created_by
           , category.modified_by
            );
            return returnValue > 0;
        }

        public async Task<bool> Update_Categories(Select_All_CategoriesResult category, CancellationToken cancellationToken = default)
        {
            int returnValue = await _db.Procedures.Update_CategoriesAsync(
                category.category_id
               , category.parent_category_id
               , category.category_name
               , category.display_order
               , category.default_sort_by_id
               , category.hidden
               , category.category_placement
               , category.display_subcat_items
               , category.alt_url
               , category.alt_url_target
               , category.link_title
               , category.meta_title
               , category.meta_desc
               , category.meta_keywords
               , category.search_tags
               , category.category_desc
               , category.category_desc_hidden
               , category.category_short_desc
               , category.category_short_desc_hidden
               , category.category_secondary_desc
               , category.category_secondary_desc_hidden
               , category.modified_by
                );
            return returnValue > 0;
        }

        public async Task<bool> Archive_CategoriesAsync(int category_id, int modified_by, CancellationToken cancellationToken = default)
        {
            int returnValue = await _db.Procedures.Archive_CategoriesAsync(category_id,  modified_by, cancellationToken: cancellationToken);
            return returnValue > 0;
        }



    }
}