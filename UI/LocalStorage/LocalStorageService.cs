using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace UI.LocalStorage
{
    public class LocalStorageService : ILocalStorageService
    {
        private IJSRuntime _jsRuntime;
        public LocalStorageService(IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
        }

        public Task<T> GetItem<T>(string username)
        {
            throw new NotImplementedException();
        }

        public Task RemoveItem(string username)
        {
            throw new NotImplementedException();
        }

        public Task SetItem<T>(string username, T userDetails)
        {
            throw new NotImplementedException();
        }
    }
}
