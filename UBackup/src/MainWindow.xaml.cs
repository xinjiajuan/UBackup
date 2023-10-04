using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UBackup.src;

namespace UBackup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //Int16 cloudtype;
        public Config.config MainConfig=new Config.config();
        public MDisk_Class Mdisk=new MDisk_Class();
        public MessageLog.MessageBox_Class messagebox_class=new MessageLog.MessageBox_Class();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Mdisk.RefreshDriveVolumeList();
            Refresh_ComboBox_select_U_Disk_Refresh();
        }

        private void button_refresh_Click(object sender, RoutedEventArgs e)
        {
            Mdisk.Clear();
            Mdisk.RefreshDriveVolumeList();
            Refresh_ComboBox_select_U_Disk_Refresh();
        }

        public void Refresh_ComboBox_select_U_Disk_Refresh()
        {
            comboBox_select_U_Disk.Items.Clear();
            foreach (var disk_info in Mdisk.disk_info_list.List_disk_properties)
            {
                if (disk_info.volume_isReady)
                {
                    comboBox_select_U_Disk.Items.Add(disk_info.volume_caption + " " + disk_info.volume_label + "（" + disk_info.volume_format + "）" + ((((disk_info.volume_totalSize / 1024)) / 1024) / 1024).ToString() + "GiB"+" - "+ disk_info.disk_model );
                    MainConfig.properties_list.List_disk_properties.Add(disk_info);
                }
            }
            comboBox_select_U_Disk.SelectedIndex = 0;
            MainConfig.Device_selection_index = comboBox_select_U_Disk.SelectedIndex;
        }

        private void radioButton_minio_Checked(object sender, RoutedEventArgs e)
        {
            cloudtype = 0;
        }

        private void button_minio_testlink_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void comboBox_select_U_Disk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_select_U_Disk.SelectedIndex == -1)
            {
                MainConfig.Device_selection_index = comboBox_select_U_Disk.SelectedIndex;
                return;
            }
            var diskinfo = Mdisk.disk_info_list.List_disk_properties[comboBox_select_U_Disk.SelectedIndex];
            textBox_diskinfo_DiskModel.Text = diskinfo.disk_model;
            textBox_diskinfo_VolumeLabel.Text = diskinfo.volume_label;
            textBox_diskinfo_VolTotalSize.Text = diskinfo.volume_totalSize.ToString();
            textBox_diskinfo_Volformat.Text = diskinfo.volume_format;
            textBox_diskinfo_VolCaption.Text = diskinfo.volume_caption;
            textBox_diskinfo_PNPDeviceID.Text = diskinfo.disk_PNPdeviceID;
            textBox_diskinfo_GUIDPath.Text=diskinfo.volume_GUID;
            if (diskinfo.volume_isReady)
            {
                comboBox_diskinfo_isready.SelectedIndex=0;
            }
            else
            {
                comboBox_diskinfo_isready.SelectedIndex = 1;
            }
            MainConfig.Device_selection_index = comboBox_select_U_Disk.SelectedIndex;
        }

        private void button_location_selection_folder(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.AutoUpgradeEnabled = true;
            dialog.ShowDialog();
            MainConfig.objective.objective_location.path = dialog.SelectedPath;
            location_folder_path.Text=dialog.SelectedPath;
        }
    }
}
