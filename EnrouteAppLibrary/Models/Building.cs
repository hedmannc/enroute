using System;
using System.Collections.Generic;

namespace EnrouteAppLibrary.Models;

public partial class Building
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Longitude { get; set; }

    public decimal Latitude { get; set; }
}
