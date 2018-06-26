using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using biznis.DataLayer;
using biznis.BussinessLayer.DTO;

namespace biznis.BussinessLayer.Operations
{
    public class CategoryCriteria : SelectCriteria { }

    public class OpCategoriesBase : Operation
    {
   
        public CategoryCriteria Criteria = new CategoryCriteria();
        public CategoryDTO Cat = new CategoryDTO();


        public override OperationResult Execute(skiiiEntities entities)
        {

            IQueryable<Category> iqCategory = entities.Categories;

            if (Criteria.Id != 0)
            {
                iqCategory = iqCategory.Where(r => r.idCategory == Criteria.Id);
            }

            IEnumerable<CategoryDTO> ieCat =
                from c in iqCategory
                select new CategoryDTO
                {
                    Id = c.idCategory,
                    Name = c.name,
                    Number = c.Posts.Count
                };

            OperationResult result = new OperationResult
            {
                Items = ieCat.ToArray(),
                Status = true
            };
            return result;
        }
    }

    public class OpCategoryDelete : OpCategoriesBase
    {
        public int IdzaBrisanje { get; set; }

        public override OperationResult Execute(skiiiEntities entities)
        {
            Category cat = entities.Categories.Where(p => p.idCategory == IdzaBrisanje).FirstOrDefault();

            if (cat != null)
            {
                entities.Categories.Remove(cat);
                entities.SaveChanges();
                return base.Execute(entities);
            }
            else
            {
                OperationResult result = new OperationResult();
                result.Status = false;
                result.Message = "Doslo je do greske. Odredjeni postovi se nalaze u kategoriji";
                return result;
            }

        }
    }

    public class OpCategoryUpdate : OpCategoriesBase
    {
        public override OperationResult Execute(skiiiEntities entities)
        {
            Category cat = entities.Categories.Where(r => r.idCategory == Cat.Id).FirstOrDefault();

            if (cat != null)
            {
                cat.name = Cat.Name;
                entities.SaveChanges();
                return base.Execute(entities);
            }

            OperationResult result = new OperationResult();
            result.Status = false;
            result.Message = "Kategorija ne postoji";
            return result;

        }
    }


    public class OpCategoryInsert : OpCategoriesBase
    {
        public override OperationResult Execute(skiiiEntities entities)
        {
            Category cat = new Category();
            cat.name = this.Cat.Name;

            entities.Categories.Add(cat);
            entities.SaveChanges();

            return base.Execute(entities);
        }
    }

}
