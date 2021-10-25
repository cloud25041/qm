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

        public async Task<List<ViewAllAppointmentViewModel>> GetAllAppointmentByAgencyId(int agencyId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                int appointmentState = 1;
                var result = await connection.QueryAsync<dynamic>("SELECT * FROM \"Appointment\" WHERE \"AgencyId\" = @agencyId  AND \"AppointmentState\" = @appointmentState", new { agencyId, appointmentState });
                return MapQueryResultToListOfViewAppointment(result);
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

        public async Task<List<AppointmentViewModel>> GetAllAppointmentsByStaffAccountId(Guid accountId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>("SELECT * FROM \"Appointment\" WHERE \"StaffAccountId\" = @accountId ", new { accountId });
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
                    AgencyId = (int)item.AgencyId,
                    AppointmentSlotId = (int)item.AppointmentSlotId,
                    StaffAccountId = (Guid)item.StaffAccountId,
                    AppointmentDate = (DateTime)item.AppointmentDate,
                    CustomerId = (Guid)item.CustomerAccountId,
                    CustomerName = (string)item.CustomerName,
                    AppointmentState = (int)item.AppointmentState,
                    ZoomId = (string)item.ZoomLink,
                    
                };

                listOfAppointment.Add(appointment);
            }
            return listOfAppointment;
        }


        private List<ViewAllAppointmentViewModel> MapQueryResultToListOfViewAppointment(dynamic result)
        {
            List<ViewAllAppointmentViewModel> listOfAppointment = new();
            foreach (var item in result)
            {
                ViewAllAppointmentViewModel appointment = new ViewAllAppointmentViewModel()
                {
                    AppointmentId = (Guid)item.AppointmentId,
                    UserAccountId = (Guid)item.CustomerAccountId,
                    CustomerName = (string)item.CustomerName,
                    AppointmentSlotID = (int)item.AppointmentSlotId,
                    AppointmentDate = (DateTime)item.AppointmentDate,


                    
                };

                listOfAppointment.Add(appointment);
            }
            return listOfAppointment;
        }
      

        public async Task<List<AgencyViewModel>> GetAgencyInfo()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>("SELECT * FROM \"Agency\"");
                var a = MapQueryResultToListOfAgency(result);
                return MapQueryResultToListOfAgency(result);
            }
        }

        private List<AgencyViewModel> MapQueryResultToListOfAgency(dynamic result)
        {
            List<AgencyViewModel> listOfAgency = new List<AgencyViewModel>();
            foreach (var item in result)
            {
                AgencyViewModel agency = new AgencyViewModel()
                {
                    AgencyName = (string)item.AgencyName,
                    AgencyId = (int)item.AgencyId,
                    PhysicalConcurrentUser = (int)item.PhysicalConcurrentUser,
                    VirtualConcurrentUser = (int)item.VirtualConcurrentUser,
                    AgencyPin = (string)item.AgencyPin,
                    Location = (string)item.Location,

                };

                listOfAgency.Add(agency);
            }
            return listOfAgency;
        }
    }
}
