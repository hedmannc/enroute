using System;
using System.Collections.Generic;

namespace EnrouteAppLibrary.Models;

public partial class UserLocationHistory
{
    public int Id { get; set; }
    public string? UserId { get; set; }

    public DateTime? Date { get; set; }

    public decimal? Longitude { get; set; }

    public decimal? Latitude { get; set; }
}
