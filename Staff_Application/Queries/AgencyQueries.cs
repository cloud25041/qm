using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Npgsql;
using Dapper;
namespace Staff_Application.Queries
{
    public class AgencyQueries
    {
        private string _connectionString;
        public AgencyQueries(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<AgencyViewModel>> GetAllAgency()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>("SELECT * FROM \"Agency\"");
                return MapQueryResultToListOfAgency(result);
            }
        }

        public async Task<List<AgencyViewModel>> GetAgencybyId(int agencyId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>("SELECT * FROM \"Agency\"WHERE \"AgencyId\" = @agencyId ", new { agencyId});
                return MapQueryResultToListOfAgency(result);
            }
        }

        public async Task<List<AgencyViewModel>> GetAgencybyAgencyPin(string agencyPin)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>("SELECT * FROM \"Agency\"WHERE \"AgencyPin\" = @agencyPin ", new { agencyPin });
                if(result != null)
                {
                    return MapQueryResultToListOfAgency(result);
                }
                else
                {
                    return MapQueryResultToListOfAgency(result);
                }
               
            }
        }

        public async Task<bool> CheckAgencyPinValid(string agencyPin)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>("SELECT * FROM \"Agency\"WHERE \"AgencyPin\" = @agencyPin ", new { agencyPin });
                //var count = result.Count();
                if ((result.Count() != 0) && result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }



        private List<AgencyViewModel> MapQueryResultToListOfAgency(dynamic result)
        {
            List<AgencyViewModel> listOfAgency = new();
            foreach (var item in result)
            {
                AgencyViewModel agency = new AgencyViewModel()
                {
                    AgencyId = (int)item.AgencyId,
                    AgencyPin = item.AgencyPin,
                    AgencyName = item.AgencyName,
                    Location = item.Location,
                    VirtualConcurrentUser = item.VirtualConcurrentUser,
                    PhysicalConcurrentUser = item.PhysicalConcurrentUser,
                 
                };

                listOfAgency.Add(agency);
            }
            return listOfAgency;
        }
    }
}
