using ModernBoxes.Model;
using ModernBoxes.ViewModel;
using System.Windows.Controls;

namespace ModernBoxes.View.SelfControl.dialog
{
    /// <summary>
    /// UcAddCardApplicationDialog.xaml 的交互逻辑
    /// </summary>
    public partial class UcAddCardApplicationDialog : UserControl
    {
        public UcAddCardApplicationDialog()
        {
            InitializeComponent();
            this.DataContext = new UcAddCardAppDialogViewModel();
        }

        /// <summary>
        /// 添加或取消卡片的显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_ChangeCardApp_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            CardContentModel? cardContentModel = new CardContentModel();
            string? id = cb.Tag.ToString();
            System.Type type = cb.DataContext.GetType();
            cardContentModel.CardName = type.GetProperty("CardName").GetValue(cb.DataContext).ToString();
            cardContentModel.CardID = int.Parse(id);
            cardContentModel.CardHeight = double.Parse(type.GetProperty("CardHeight").GetValue(cb.DataContext).ToString());
            if (cb != null)
            {
                UcCompontentViewModel.DoCheckedCardApp(int.Parse(id), (bool)cb.IsChecked);
            }
        }
    }
}