using Microsoft.EntityFrameworkCore;
using DemoApp.Domain.Models;

namespace DemoApp.Infra.Repositories
{
    public interface ITimesheetRepository : IRepository<Timesheet> { }

    internal sealed class TimesheetRepository : Repository<Timesheet>, ITimesheetRepository
    {
        public TimesheetRepository(TaskManagerContext context) : base(context) { }
    }
}
