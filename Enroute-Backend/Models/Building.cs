using System;
using System.Collections.Generic;

namespace Enroute_Backend.Models;

public partial class Building
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Longitude { get; set; }

    public decimal Latitude { get; set; }
}
