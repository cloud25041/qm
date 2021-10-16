using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Dapper;

namespace AR_Application.Queries
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
                var result = await connection.QueryAsync<dynamic>("SELECT * FROM \"Appointment\" WHERE \"UserAccountId\" = @accountId ", new { accountId });
                return MapQueryResultToListOfAppointment(result);
            }
        }

        public async Task <AppointmentViewModel> GetAppointmentByAppointmentId(Guid appointmentId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>("SELECT * FROM \"Appointment\" WHERE \"AppointmentId\" = @appointmentId ", new { appointmentId });
                return MapQueryResultToListOfAppointment(result)[0];
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
                    //AccountId = (Guid)item.AccountId,
                    AppointmentState = (int)item.AppointmentState,
                    AppointmentType = (int)item.AppointmentType,
                    AppointmentDate= (DateTime)item.AppointmentDate,
                    AppointmentSlotId = (int)item.AppointmentSlotId,
                    UserAccountId = (Guid)item.UserAccountId,
                    StaffAccountID = (Guid)item.StaffAccountID,
                    AgencyId = (Guid)item.AgencyId, 
                    // should be according to type in database, AgencyId should be int (to change after database is updated) 





                    //AgencyCode = (string)item.AgencyCode,
                    //StartTime = (DateTime)item.StartTime,
                    //EndTime = (DateTime)item.EndTime
                };

                listOfAppointment.Add(appointment);
            }
            return listOfAppointment;
        }

        // All agency related is to get from Staff API
        public async Task<List<AgencyViewModel>> GetAgencyInfo()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>("SELECT * FROM \"Agency");
                return MapQueryResultToListOfAgency(result);
            }
        }

        private List<AgencyViewModel> MapQueryResultToListOfAgency(dynamic result)
        {
            List<AgencyViewModel> listOfAgency = new();
            foreach (var item in result)
            {
                AgencyViewModel agency = new AgencyViewModel()
                {
                    AgencyName = (string)item.AgencyName,
                    AgencyId= (int)item.AgencyId,
                    PhysicalConcurrentUser = (int)item.PhysicalConcurrentUser,
                    VirtualConcurrentUser = (int)item.VirtualConcurrentUser,
                    AgencyPin = (int)item.AgencyPin,
                    Location = (string)item.Location,
                    
                };

                listOfAgency.Add(agency);
            }
            return listOfAgency;
        }
        
    }
}
