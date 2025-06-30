using UniverSys.Application.Common.Interfaces;
using System;

namespace UniverSys.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
