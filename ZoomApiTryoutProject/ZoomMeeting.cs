using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZoomNet;
using ZoomNet.Models;

namespace ZoomApiTryoutProject
{
    public class ZoomMeeting
    {
        private readonly JwtConnectionInfo _JwtConnectionInfo;

        public ZoomMeeting(string apiKey, string apiSecret)
        {
            _JwtConnectionInfo = new JwtConnectionInfo(apiKey, apiSecret);
        }

        public async Task<InstantMeeting> CreateInstantZoomMeeting()
        {
            ZoomClient zoomClient = new ZoomClient(_JwtConnectionInfo);
            return await zoomClient.Meetings.CreateInstantMeetingAsync("eost002@gmail.com", "testTopic", "testAgenda");
        }

        public async Task<ScheduledMeeting> Create5MinScheduledMeeting()
        {
            ZoomClient zoomClient = new ZoomClient(_JwtConnectionInfo);
            return await zoomClient.Meetings.CreateScheduledMeetingAsync("eost002@gmail.com", "testTopic", "testAgenda", DateTime.UtcNow, 5, TimeZones.Asia_Singapore, "pw");
        } 
    }
}
