using ToDoList.Shared.Constants;

namespace ToDoList_WepApi.Helpers
{
    public class DataStorageHelper
    {
        private IHttpContextAccessor _contextAccessor;

        public DataStorageHelper(IHttpContextAccessor contextAccessor)
        {

            _contextAccessor = contextAccessor;
        }

        public string GetStorageHeaderValue()
        {
            string storageValue = _contextAccessor.HttpContext.Request.Headers["Storage-Type"];

            if (storageValue == null)
            {
                storageValue = DataStorageTypes.XmlStorageType;
                _contextAccessor.HttpContext.Request.Headers.TryAdd("Storage-Type", storageValue);
            }

            return storageValue;
        }

    }
}
