using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Shared.Models;

namespace ToDoList.Shared.Interfaces
{
    public interface ICategoryRepository
    {
        public IEnumerable<CategoryModel> GetAllCategories();
    }
}
