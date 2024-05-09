using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace EnrouteAppLibrary.Models
{
    public class ErrorDetails
    {
            public string type { get; set; }
            public string title { get; set; }
            public int status { get; set; }
            public string detail { get; set; }

        public JsonObject errors { get; set; } = new JsonObject();
        

    }
}
