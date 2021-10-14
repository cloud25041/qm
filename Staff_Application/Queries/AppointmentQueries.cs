using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Dapper;

namespace Staff_Application.Queries
{
    public class AppointmentQueries
    {
        private readonly string _connectionString;
        public AppointmentQueries(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<AppointmentViewModel>> GetAllAppointments()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>("SELECT * FROM \"Appointment\"");
                return MapQueryResultToListOfAppointment(result);
            }
        }

        public async Task<List<AppointmentViewModel>> GetAllAppointmentsByAccountId(Guid accountId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>("SELECT * FROM \"Appointment\" WHERE \"AccountId\" = @accountId ", new { accountId });
                return MapQueryResultToListOfAppointment(result);
            }
        }

        private List<AppointmentViewModel> MapQueryResultToListOfAppointment (dynamic result)
        {
            List<AppointmentViewModel> listOfAppointment = new();
            foreach (var item in result)
            {
                AppointmentViewModel appointment = new AppointmentViewModel()
                {
                    AppointmentId = (Guid)item.AppointmentId,
                    AccountId = (Guid)item.AccountId,
                    AppointmentState = (int)item.AppointmentState,
                    AgencyCode = (string)item.AgencyCode,
                    StartTime = (DateTime)item.StartTime,
                    EndTime = (DateTime)item.EndTime
                };

                listOfAppointment.Add(appointment);
            }
            return listOfAppointment;
        }
    }
}
