using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using IWshRuntimeLibrary; //添加引用，在 Com 中搜索 Windows Script Host Object Model

using System.Threading.Tasks;

namespace ModernBoxes.Tool
{
    public class AutoOpenSoftware
    {
        /// <summary>
        /// 自定义快捷方式名称
        /// </summary>
        private const string quickName = "ModernBoxes";

        /// <summary>
        /// 自动获取系统自动启动目录
        /// </summary>
        private string systemStartPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            }
        }

        /// <summary>
        /// 自动获取程序完整路径
        /// </summary>
        private string appAllPath
        {
            get
            {
                return Process.GetCurrentProcess().MainModule.FileName;
            }
        }

        /// <summary>
        /// 自动获取桌面目录
        /// </summary>
        private string desktopPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            }
        }

        /// <summary>
        /// 设置开机自启动
        /// </summary>
        /// <param name="onOff">自启开关</param>
        public void SetAutoStart(bool onOff = true)
        {
            if (onOff) //开机启动
            {
                //获取启动路径应用程序快捷方式的路径集合
                List<string> shortcutPaths = GetQuickFromFolder(systemStartPath, appAllPath);
                //存在2个以快捷方式则保留一个快捷方式，避免重复
                if (shortcutPaths.Count >= 2)
                {
                    for (int i = 1; i < shortcutPaths.Count; i++)
                    {
                        DeleteFile(shortcutPaths[i]);
                    }
                }
                else if (shortcutPaths.Count < 1)//不存在则创建快捷方式
                {
                    CreateShortcut(systemStartPath, quickName, appAllPath, "Client");
                }
            }
            else //开机不启动
            {
                //获取启动路径应用程序快捷方式的路径集合
                List<string> shortcutPaths = GetQuickFromFolder(systemStartPath, appAllPath);
                //存在快捷方式则遍历全部删除
                if (shortcutPaths.Count > 0)
                {
                    for (int i = 0; i < shortcutPaths.Count; i++)
                    {
                        DeleteFile(shortcutPaths[i]);
                    }
                }
            }

            //创建桌面快捷方式
            this.CreateDesktopQuick(desktopPath, quickName, appAllPath);
        }

        /// <summary>
        ///  向目标路径创建指定文件的快捷方式
        /// </summary>
        /// <param name="directory">目标目录</param>
        /// <param name="shortcutName">快捷方式名字</param>
        /// <param name="targetPath">文件完全路径</param>
        /// <param name="description">描述</param>
        /// <param name="iconLocation">图标地址</param>
        /// <returns></returns>
        private bool CreateShortcut(string directory, string shortcutName, string targetPath, string description = null, string iconLocation = null)
        {
            try
            {
                //目录不存在则创建
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                //添加引用 Com 中搜索 Windows Script Host Object Model
                string shortcutPath = Path.Combine(directory, string.Format("{0}.lnk", shortcutName));          //合成路径
                WshShell shell = new IWshRuntimeLibrary.WshShell();
                IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath);    //创建快捷方式对象
                shortcut.TargetPath = targetPath;                                                               //指定目标路径
                shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);                                  //设置起始位置
                shortcut.WindowStyle = 1;                                                                       //设置运行方式，默认为常规窗口
                shortcut.Description = description;                                                             //设置备注
                shortcut.IconLocation = string.IsNullOrWhiteSpace(iconLocation) ? targetPath : iconLocation;    //设置图标路径
                shortcut.Save();                                                                                //保存快捷方式

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return false;
        }

        /// <summary>
        /// 获取指定文件夹下指定应用程序的快捷方式路径集合
        /// </summary>
        /// <param name="directory">文件夹C:\\Users\\Machu\\AppData\\Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\Startup</param>
        /// <param name="targetPath">目标应用程序路径D:\\WareHoues\\modern-box\\ModernBoxes\\ModernBoxes\\bin\\Debug\\net5.0-windows\\ModernBoxes.exe</param>
        /// <returns>目标应用程序的快捷方式</returns>
        private List<string> GetQuickFromFolder(string directory, string targetPath)
        {
            List<string> tempStrs = new List<string>();
            tempStrs.Clear();
            string tempStr = null;
            string[] files = Directory.GetFiles(directory, "*.lnk");  //"C:\\Users\\Machu\\AppData\\Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\ModernBoxes.lnk"
            if (files == null || files.Length < 1)
            {
                return tempStrs;
            }

            for (int i = 0; i < files.Length; i++)
            {
                tempStr = this.GetAppPathFromQuick(files[i]);
                if (tempStr == targetPath)
                {
                    tempStrs.Add(files[i]);
                }
            }

            return tempStrs;
        }

        /// <summary>
        /// 获取快捷方式的目标文件路径-用于判断是否已经开启了自动启动
        /// </summary>
        /// <param name="shortcutPath"></param>
        /// <returns></returns>
        private string GetAppPathFromQuick(string shortcutPath)
        {
            //快捷方式文件的路径 = @"d:\Test.lnk";
            if (System.IO.File.Exists(shortcutPath))
            {
                WshShell shell = new WshShell();
                IWshShortcut shortct = (IWshShortcut)shell.CreateShortcut(shortcutPath);
                //快捷方式文件指向的路径.Text = 当前快捷方式文件IWshShortcut类.TargetPath;
                //快捷方式文件指向的目标目录.Text = 当前快捷方式文件IWshShortcut类.WorkingDirectory;
                return shortct.TargetPath;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 根据路径删除文件-用于取消自启时从计算机自启目录删除程序的快捷方式
        /// </summary>
        /// <param name="path">路径</param>
        private void DeleteFile(string path)
        {
            FileAttributes attr = System.IO.File.GetAttributes(path);
            if (attr == FileAttributes.Directory)
            {
                Directory.Delete(path, true);
            }
            else
            {
                System.IO.File.Delete(path);
            }
        }

        /// <summary>
        /// 在桌面上创建快捷方式-如果需要可以调用
        /// </summary>
        /// <param name="desktopPath">桌面地址</param>
        /// <param name="appPath">应用路径</param>
        private void CreateDesktopQuick(string desktopPath = "", string quickName = "", string appPath = "")
        {
            List<string> shortcutPaths = this.GetQuickFromFolder(desktopPath, appPath);
            //如果没有则创建
            if (shortcutPaths.Count < 1)
            {
                this.CreateShortcut(desktopPath, quickName, appPath, "软件描述");
            }
        }

    }
}
