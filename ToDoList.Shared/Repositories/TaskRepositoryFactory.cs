using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Shared.Constants;
using ToDoList.Shared.Interfaces;

namespace ToDoList.Shared.Repositories
{
    public class TaskRepositoryFactory : ITaskRepositoryFactory
    {
        private readonly IHttpContextAccessor _contentAccessor;
        private readonly IConfiguration _cfg;

        public TaskRepositoryFactory(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _contentAccessor = httpContextAccessor;
            _cfg = configuration;
        }

        public ITaskRepository CreateRepository(string storageType)
        {
            

            return storageType.ToUpper() switch
            {
                DataStorageTypes.XmlStorageType => new XMLTaskRepository(_cfg),

                DataStorageTypes.MsSqlStorageType => new DbTaskRepository(_cfg),

                _ => throw new Exception("Wrong storage type"),
            };
        }
    }
}
