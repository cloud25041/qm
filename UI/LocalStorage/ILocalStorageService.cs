using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Models;

namespace UI.LocalStorage
{
    interface ILocalStorageService
    {
        AccountInfo GetAccountInfo();
        void SetAccountInfo(AccountInfo accountInfo);
    }
}
