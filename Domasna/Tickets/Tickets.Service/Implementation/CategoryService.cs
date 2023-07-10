using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.Domain.DomainModels;
using Tickets.Repository.Interface;
using Tickets.Services.Interface;

namespace Tickets.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void CreateNewCategory(Category category)
        {
            _categoryRepository.Insert(category);
            _categoryRepository.Update(category);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetCategory(Guid id)
        {
            Category _category = _categoryRepository.Get(id);
            return _category;
        }
    }
}
