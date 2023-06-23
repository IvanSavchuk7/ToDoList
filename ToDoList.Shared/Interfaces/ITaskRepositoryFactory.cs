using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Shared.Constants;

namespace ToDoList.Shared.Interfaces
{
    public interface ITaskRepositoryFactory
    {
        ITaskRepository CreateRepository(string storageType);
    }
}
