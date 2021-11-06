using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.Model;
using ModernBoxes.Tool;
using ModernBoxes.View.SelfControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBoxes.ViewModel
{
    public class UcCompontentViewModel : ViewModelBase
    {

        /// <summary>
        /// 卡片内容集合
        /// </summary>

        private ObservableCollection<CardContentModel> cardContents = new ObservableCollection<CardContentModel>();

        public ObservableCollection<CardContentModel> CardContents
        {
            get { return cardContents; }
            set { cardContents = value; RaisePropertyChanged("CardContents"); }
        }



        public UcCompontentViewModel()
        {
            loadCardContent();
        }


        /// <summary>
        /// 加载卡片内容
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void loadCardContent()
        {
            CardContents.Add(new CardContentModel() { CardName = "一言", CardContent = new UCOneWord() });
            CardContents.Add(new CardContentModel() { CardName = "应用", CardContent = new UCusedApplications() });
            CardContents.Add(new CardContentModel() { CardName = "文件夹", CardContent = new UCtempDirectory() });
            CardContents.Add(new CardContentModel() { CardName = "文件", CardContent = new UcTempFile() });
            CardContents.Add(new CardContentModel() { CardName = "便签", CardContent = new UCnotes() });
        }
    }
}
