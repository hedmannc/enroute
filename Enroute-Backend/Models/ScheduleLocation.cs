using System;
using System.Collections.Generic;

namespace Enroute_Backend.Models;

public partial class ScheduleLocation
{
    public int? ScheduleId { get; set; }

    public int? LocationId { get; set; }

    public int? DayofTheWeek { get; set; }

    public int? StartTimeHour { get; set; }

    public int? StartTimeMinute { get; set; }

    public int? EndTimeHour { get; set; }

    public int? EndTimeMinute { get; set; }
}
