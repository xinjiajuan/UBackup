using Minio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UBackup.src;

namespace UBackup.src
{
    public class Config
    {
        public enum Objective_index
        {
            Location=0,
            Minio=1
        }
        public struct config
        {
            public string name;
            public string description;
            public int Device_selection_index;
            public MDisk_Class.Disk_properties_list properties_list;
            public Objective_index objective_Index;
            public Objective objective;
        }
        public struct Objective
        {
            public Objective_Location objective_location;
            public Objective_Minio objective_Minio;
        }
        public struct Objective_Location
        {
            public string path;
        }
        public struct Objective_Minio
        {
            public MinioClient client;
            public Minio_Class.minio_client_info client_info;
        }
    }
}
