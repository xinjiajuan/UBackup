using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UBackup.src
{
    public class MessageLog
    {
        public class MessageBox_Class
        {
            public string[] message = {
                "端口不正确，请检查配置。"
            };
            public string[] messagetype = {
                "错误",//0
                "提示",//1
                "警告",//2
                "询问",//3
            };
            public MessageBoxImage[] messageicon = {
                MessageBoxImage.Error,
                MessageBoxImage.Information,
                MessageBoxImage.Warning,
                MessageBoxImage.Question
            };
            /// <summary>
            /// 显示消息框
            /// </summary>
            /// <remarks>消息内容：
            /// 1.端口不正确，请检查配置。
            /// </remarks>
            /// <param name="messageid">消息内容序号</param>
            /// <param name="messagetypeid">消息类型序号                "错误",//0 "提示",//1 "警告",//2 "询问",//3</param>
            public void ShowMessageBox(int messageid, int messagetypeid)
            {
                MessageBox.Show(message[messageid], messagetype[messagetypeid], MessageBoxButton.OK, messageicon[messagetypeid]);
            }
        }

    }
}
