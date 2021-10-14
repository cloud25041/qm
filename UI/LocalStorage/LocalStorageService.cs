using Microsoft.JSInterop;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UI.Models;

namespace UI.LocalStorage
{
    public class LocalStorageService : ILocalStorageService
    {
        private string _serializedObject;

        public AccountInfo GetAccountInfo()
        {
            return JsonConvert.DeserializeObject<AccountInfo>(_serializedObject);
        }

        public void SetAccountInfo(AccountInfo accountInfo)
        {
            _serializedObject = JsonConvert.SerializeObject(accountInfo);
        }
    }
}
