using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IcebergLog.Core.Config
{
    public class StorageConfig
    {
        public string StorageType { get; set; } = "MongoDB"; // Default MongoDB
        public string ConnectionString { get; set; } = string.Empty;
    }
}
