
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Npgsql;
using Dapper;

namespace AR_Application.Queries
{
    public class AccountQueries
    {
        private string _connectionString;
        public AccountQueries(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<AccountViewModel>> GetAllAccounts()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>("select * from \"Account\"");
                return MapQueryResultToListOfAccount(result);
            }
        }

        public async Task<AccountViewModel> GetAccountByUsername(string username)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>("SELECT * FROM \"Account\" WHERE \"Username\" = @username LIMIT 1", new { username });
                return MapQueryResultToListOfAccount(result)[0];
            }
        }

        public async Task<AccountViewModel> GetAccountByUsernameAndVerifyPassword(string username, string password)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>("SELECT * FROM \"Account\" WHERE \"Username\" = @username LIMIT 1", new { username });
                string qPassword = string.Empty;
                foreach (var item in result)
                {
                    qPassword = item.Password;
                }
                if (qPassword != string.Empty && qPassword == password)
                {
                    return MapQueryResultToListOfAccount(result)[0];
                }
                else
                {
                    return null;
                }
            }
        }

        private List<AccountViewModel> MapQueryResultToListOfAccount(dynamic result)
        {
            List<AccountViewModel> listOfAccount = new();
            foreach (var item in result)
            {
                AccountViewModel account = new AccountViewModel()
                {
                    username = (string)item.Username,
                    accountId = (Guid)item.AccountID,
                    name = (string)item.AccountDetails_Name,
                    email = (string)item.AccountDetails_Email,
                    mobileNo = (int)item.AccountDetails_Mobile
                };

                listOfAccount.Add(account);
            }
            return listOfAccount;
        }
    }
}
