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
using System.Windows;

namespace ModernBoxes.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        /// <summary>
        /// 主菜单集合
        /// </summary>
        private ObservableCollection<MenuModel> menus = new ObservableCollection<MenuModel>();

        public ObservableCollection<MenuModel> MenuList
        {
            get { return menus; }
            set { menus = value; RaisePropertyChanged("MenuList"); }
        }

        /// <summary>
        /// 卡片内容集合
        /// </summary>

        private ObservableCollection<CardContentModel> cardContents = new ObservableCollection<CardContentModel>();

        public ObservableCollection<CardContentModel> CardContents
        {
            get { return cardContents; }
            set { cardContents = value;RaisePropertyChanged("CardContents"); }
        }

        /// <summary>
        /// 点击菜单加载命令
        /// </summary>
        public RelayCommand OpenApp
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    switch (o.ToString())
                    {
                        case "组件应用":
                            Messenger.Default.Send<Boolean>(true, "isShow");
                            break;
                    }
                }, x => true);
            }
        }


        public MainViewModel()
        {
            loadMenu();
            loadCardContent();
        }

        /// <summary>
        /// 加载卡片内容
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void loadCardContent()
        {
            CardContents.Add(new CardContentModel() {CardName="每日一言" ,CardContent = new UCOneWord()});
            CardContents.Add(new CardContentModel() { CardName = "日常应用", CardContent = new UCusedApplications() });
            CardContents.Add(new CardContentModel() { CardName = "临时文件夹", CardContent = new UCtempDirectory() });
            CardContents.Add(new CardContentModel() { CardName = "便签", CardContent = new UCnotes() });
        }

        /// <summary>
        /// 加载主菜单项
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void loadMenu()
        {
            MenuList.Add(new MenuModel() { MenuName = "QQ" });
            MenuList.Add(new MenuModel() { MenuName = "Visual Studio" });
            MenuList.Add(new MenuModel() { MenuName = "Android Studio" });
            MenuList.Add(new MenuModel() { MenuName = "Visual Studio Code" });
            MenuList.Add(new MenuModel() { MenuName = "Azure Studio" });
            MenuList.Add(new MenuModel() { MenuName = "Typora" });
            MenuList.Add(new MenuModel() { MenuName = "WeChat" });
            MenuList.Add(new MenuModel() { MenuName = "MMSM" });
            MenuList.Add(new MenuModel() { MenuName = "组件应用" });
        }
    }
}
