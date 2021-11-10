using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.MyEnum;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ModernBoxes.View.SelfControl.dialog
{
    /// <summary>
    /// UcMessageDialog.xaml 的交互逻辑
    /// </summary>
    public partial class UcMessageDialog : UserControl
    {
        private Boolean State = false;

        public UcMessageDialog(String content, MessageDialogState MessageState)
        {
            InitializeComponent();
            TB_Content.Text = content;
            switch (MessageState)
            {
                case MessageDialogState.danger:
                    TB_Icon.Text = "\xe618";
                    TB_Icon.Foreground = new SolidColorBrush(Color.FromRgb(255, 82, 83));
                    break;

                case MessageDialogState.Info:
                    TB_Icon.Text = "\xe630";
                    TB_Icon.Foreground = new SolidColorBrush(Color.FromRgb(71, 123, 244));
                    break;

                case MessageDialogState.waring:
                    TB_Icon.Text = "\xe619";
                    TB_Icon.Foreground = new SolidColorBrush(Color.FromRgb(255, 224, 59));
                    break;
            }
        }

        public void IsShowBtn_Ok(Boolean value)
        {
            Btn_Ok.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }

        public void IsShowBtn_Canel(Boolean value)
        {
            Btn_Canel.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            State = true;
            Messenger.Default.Send(true, "IsCloseBaseDialog");
        }

        private void Btn_Canel_Click(object sender, RoutedEventArgs e)
        {
            State = false;
            Messenger.Default.Send(true, "IsCloseBaseDialog");
        }

        public Boolean getState()
        {
            return State;
        }
    }
}