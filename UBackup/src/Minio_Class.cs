using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minio.Exceptions;
using Minio.DataModel;
using Minio;
using UBackup.src;

namespace UBackup.src
{
    public class Minio_Class
    {
        public struct minio_client_info
        {
            public string host;
            public int port;
            public bool https;
            public string accesskey;
            public string secretkey;
            public string bucket;
            public string location;
        }
    }
}
