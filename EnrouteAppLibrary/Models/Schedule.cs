using System;
using System.Collections.Generic;

namespace EnrouteAppLibrary.Models;


public partial class Schedule
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public DateOnly? PeriodStart { get; set; }

    public DateOnly? PeriodEnd { get; set; }
}
