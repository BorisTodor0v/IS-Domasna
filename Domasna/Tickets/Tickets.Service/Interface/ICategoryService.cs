using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Domain.DomainModels;

namespace Tickets.Services.Interface
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
        void CreateNewCategory(Category category);
        public Category GetCategory(Guid id);
    }
}
