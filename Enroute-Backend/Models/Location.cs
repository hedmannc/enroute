using System;
using System.Collections.Generic;

namespace Enroute_Backend.Models;

public partial class Location
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? BuildingId { get; set; }

    public string? Description { get; set; }
}
