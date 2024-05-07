using System;
using System.Collections.Generic;

namespace EnrouteAppLibrary.Models;

public partial class UserLocationHistory
{
    public string? UserId { get; set; }

    public DateOnly? Date { get; set; }

    public decimal? Longitude { get; set; }

    public decimal? Latitude { get; set; }
}
