using System;
using System.Collections.Generic;

namespace AplicacionBase.Models
{
    public partial class aspnet_SchemaVersions
    {
        public string Feature { get; set; }
        public string CompatibleSchemaVersion { get; set; }
        public bool IsCurrentVersion { get; set; }
    }
}
