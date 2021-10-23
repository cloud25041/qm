using System.Threading.Tasks;

namespace StaffUI.LocalStorage
{
    public interface ILocalStorageService
    {
        Task<T> GetItem<T>(string key);
        Task RemoveItem(string key);
        Task SetItem<T>(string key, T value);
    }
}