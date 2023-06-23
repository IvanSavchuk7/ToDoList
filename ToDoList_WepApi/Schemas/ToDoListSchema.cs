using GraphQL.Types;
using ToDoList_WepApi.Queries;

namespace ToDoList_WepApi.Schemas
{
    public class ToDoListSchema : Schema
    {
        public ToDoListSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<Query>();

            Mutation = provider.GetRequiredService<Mutation>();
        }
    }
}
