using EventBus.Abstractions;
using EventBus.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Meeting_Domain.DomainService;
using ZoomNet.Models;
using Meeting_Application.IntegrationEvents.OutgoingEvents;

namespace Meeting_Application.IntegrationEvents.IncomingEvents
{
    public class AppointmentConfirmedByStaffIntegrationEventHandler : IIntegrationEventHandler<AppointmentConfirmedByStaffIntegrationEvent>, IRequestHandler<AppointmentConfirmedByStaffIntegrationEvent>
    {
        private readonly ZoomLinkCreatorService _zoomLinkCreatorService;
        private readonly IMeetingIntegrationEventService _meetingIntegrationEventService;

        public AppointmentConfirmedByStaffIntegrationEventHandler(ZoomLinkCreatorService zoomLinkCreatorService, IMeetingIntegrationEventService meetingIntegrationEventService)
        {
            _zoomLinkCreatorService = zoomLinkCreatorService;
            _meetingIntegrationEventService = meetingIntegrationEventService;
        }

        public async Task<Unit> Handle(AppointmentConfirmedByStaffIntegrationEvent request, CancellationToken cancellationToken)
        {
            InstantMeeting instantMeeting = await _zoomLinkCreatorService.CreateInstantZoomMeeting();

            await _meetingIntegrationEventService.AddAndSaveEventAsync(new ZoomLinkCreatedIntegrationEvent(request.AppointmentId, instantMeeting.JoinUrl));
            return Unit.Value;
        }
    }
}
