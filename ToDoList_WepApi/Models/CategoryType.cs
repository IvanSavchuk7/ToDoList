using GraphQL.Types;
using ToDoList.Shared.Models;

namespace ToDoList_WepApi.Models
{
    public class CategoryType : ObjectGraphType<CategoryModel>
    {
        public CategoryType()
        {
            Field(x => x.Id, nullable: true);
            Field(x => x.Name, nullable: true);
        }
    }
}
