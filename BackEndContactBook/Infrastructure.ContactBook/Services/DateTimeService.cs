using ContactBook.Application.Common.Interfaces;

namespace MyPlatform.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
