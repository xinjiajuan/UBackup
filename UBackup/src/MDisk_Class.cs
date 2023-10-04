using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Management;

namespace UBackup.src
{
    public class MDisk_Class
    {
        public struct Disk_properties
        {
            /// <summary>
            /// 型号
            /// </summary>
            public string disk_model;
            /// <summary>
            /// PNPDeviceID
            /// </summary>
            public string disk_PNPdeviceID;
            /// <summary>
            /// 驱动器号（盘符）
            /// </summary>
            public string volume_caption;
            /// <summary>
            /// 卷标
            /// </summary>
            public string volume_label;
            /// <summary>
            /// 是否就绪
            /// </summary>
            public bool volume_isReady;
            /// <summary>
            /// 磁盘大小
            /// </summary>
            public long volume_totalSize;
            /// <summary>
            /// 文件系统
            /// </summary>
            public string volume_format;
            /// <summary>
            /// GUID路径
            /// </summary>
            public string volume_GUID;
        }
        public struct Disk_properties_list
        {
            public List<Disk_properties> List_disk_properties;
        }
        public Disk_properties_list disk_info_list;

        public void RefreshDriveVolumeList()
        {
            ManagementClass mgtCls = new ManagementClass("Win32_DiskDrive");
            var disks = mgtCls.GetInstances();
            SelectQuery DiskVolume_selectQuery = new SelectQuery("Win32_Volume");
            ManagementObjectSearcher DiskVolume_searcher = new ManagementObjectSearcher(DiskVolume_selectQuery);
            //
            disk_info_list = new Disk_properties_list();
            disk_info_list.List_disk_properties = new List<Disk_properties>();
            var drivers = DriveInfo.GetDrives();
            foreach (ManagementObject mo in disks)
            {
                if (mo.Properties["MediaType"].Value != null &&
                  (mo.Properties["MediaType"].Value.ToString() != "Fixed hard disk media"))
                {
                    foreach (ManagementObject diskPartition in mo.GetRelated("Win32_DiskPartition"))
                    {
                        Disk_properties diskProList = new Disk_properties();
                        foreach (ManagementBaseObject disk in diskPartition.GetRelated("Win32_LogicalDisk"))
                        {
                            diskProList.volume_caption= disk.Properties["Name"].Value.ToString() + "\\";
                            diskProList.disk_model =  mo.Properties["Caption"].Value.ToString();
                            diskProList.disk_PNPdeviceID = mo.Properties["PNPDeviceID"].Value.ToString();
                            foreach (ManagementObject volume in DiskVolume_searcher.Get())
                            {
                                foreach (var volumeinfo in drivers)
                                {
                                    if (volume.Properties["Caption"].Value.ToString() == diskProList.volume_caption & diskProList.volume_caption==volumeinfo.Name)
                                    {
                                        diskProList.volume_isReady=volumeinfo.IsReady;
                                        diskProList.volume_format = volumeinfo.DriveFormat;
                                        diskProList.volume_totalSize = volumeinfo.TotalSize;
                                        diskProList.volume_label = volumeinfo.VolumeLabel;
                                        diskProList.volume_GUID= volume.Properties["DeviceID"].Value.ToString();
                                    }
                                }

                            }
                            disk_info_list.List_disk_properties.Add(diskProList);
                        }
                    }
                }
            }
        }
        public void Clear()
        {
            disk_info_list.List_disk_properties.Clear();
        }
    }
}
