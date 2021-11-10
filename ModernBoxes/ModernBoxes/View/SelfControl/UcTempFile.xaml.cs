﻿using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.View.SelfControl.dialog;
using ModernBoxes.ViewModel;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace ModernBoxes.View.SelfControl
{
    /// <summary>
    /// UcTempFile.xaml 的交互逻辑
    /// </summary>
    public partial class UcTempFile : UserControl
    {
        public UcTempFile()
        {
            InitializeComponent();
            this.DataContext = new UctempFileViewModel();
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            PropertyInfo? propertyInfo = sender.GetType().GetProperty("CommandParameter");
            String? filePath = propertyInfo.GetValue(sender).ToString();
            Messenger.Default.Send<String>(filePath, "deleteFile");
        }

        /// <summary>
        /// 查看文件属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            PropertyInfo? propertyInfo = sender.GetType().GetProperty("CommandParameter");
            if (propertyInfo != null)
            {
                String? filePath = propertyInfo.GetValue(sender).ToString();
                BaseDialog baseDialog = new BaseDialog();
                baseDialog.SetTitle("文件属性");
                baseDialog.SetHeight(550);
                baseDialog.SetContent(new FilePropertyDialog(filePath));
                baseDialog.ShowDialog();
            }
        }
    }
}