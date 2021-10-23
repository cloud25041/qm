using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZoomNet;
using ZoomNet.Models;

namespace Meeting_Domain.DomainService
{
    public class ZoomLinkCreatorService
    {
        private readonly JwtConnectionInfo _JwtConnectionInfo;

        public ZoomLinkCreatorService(string apiKey, string apiSecret)
        {
            _JwtConnectionInfo = new JwtConnectionInfo(apiKey, apiSecret);
        }

        public async Task<InstantMeeting> CreateInstantZoomMeeting()
        {
            ZoomClient zoomClient = new ZoomClient(_JwtConnectionInfo);
            return await zoomClient.Meetings.CreateInstantMeetingAsync("eost002@gmail.com", "testTopic", "testAgenda");
        }
    }
}
