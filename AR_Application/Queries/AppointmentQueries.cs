using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using AR_Application.Model;


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

        public async Task<List<AvailableSlotsViewModel>> GetAvailableAppointment(int? AgencyId, int? AppointmentTypeId, int? ConcurrentUser, DateTime? SelectedDate)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>("SELECT * FROM \"Appointment\" WHERE \"AgencyId\" = @AgencyId AND \"AppointmentType\" = @AppointmentTypeId AND \"AppointmentDate\" = @SelectedDate ", new { AgencyId ,AppointmentTypeId, ConcurrentUser, SelectedDate });
                List<AppointmentViewModel> appointmentList = MapQueryResultToListOfAppointment(result);
                List<AvailableSlotsViewModel> availableSlotList = new List<AvailableSlotsViewModel>();
                int[] count = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                foreach (var appointment in appointmentList)
                {
                    count[appointment.AppointmentSlotId]++;
                }
                for (int i=0; i <= 15; i++)
                {
                    if (count[i] < ConcurrentUser)
                    {
                        availableSlotList.Add(new AvailableSlotsViewModel() { Date = SelectedDate, SlotId = i + 1 });
                    }
                }
                return availableSlotList;
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
                    StaffAccountID = (item.StaffAccountID == null) ? Guid.Empty : (Guid)item.StaffAccountID,
                    AgencyId = (int)item.AgencyId, 
                    ZoomLink = (item.ZoomLink == null) ? "" : (string)item.ZoomLink,
                    // should be according to type in database, AgencyId should be int (to change after database is updated) 





                    //AgencyCode = (string)item.AgencyCode,
                    //StartTime = (DateTime)item.StartTime,
                    //EndTime = (DateTime)item.EndTime
                };

                listOfAppointment.Add(appointment);
            }
            return listOfAppointment;
        }

        
        
    }
}
