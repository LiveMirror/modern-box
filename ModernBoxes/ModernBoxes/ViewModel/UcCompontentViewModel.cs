using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.Model;
using ModernBoxes.Tool;
using ModernBoxes.View.SelfControl;
using ModernBoxes.View.SelfControl.dialog;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModernBoxes.ViewModel
{
    public delegate void RefershCardContentsHandler();
    public delegate void ChangeCardAppHeightHandler(int CardId,Double height);
    public class UcCompontentViewModel : ViewModelBase
    {
        public static event RefershCardContentsHandler RefershCardContentsEvent;
        public static event ChangeCardAppHeightHandler ChangeCardAppHeightEvent;

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
            RefershCardContentsEvent += UcCompontentViewModel_RefershCardContentsEvent;
            ChangeCardAppHeightEvent += UcCompontentViewModel_ChangeCardAppHeightEvent;
            loadCardContent();
        }

        private void UcCompontentViewModel_ChangeCardAppHeightEvent(int CardId, double height)
        {
           CardContents.FirstOrDefault(o => o.CardID == CardId).CardHeight = height;
        }
        public static void DoChangeCardAppHeight(int CardId,Double height)
        {
            ChangeCardAppHeightEvent(CardId,height);
        }

        /// <summary>
        /// 刷新卡片数据
        /// </summary>
        private  void UcCompontentViewModel_RefershCardContentsEvent()
        {
            CardContents.Clear();
             loadCardContent();
        }
        public static void DoloadCardContent()
        {
            RefershCardContentsEvent();
        }


        /// <summary>
        /// 加载卡片内容
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private async void loadCardContent()
        {
            try
            {
                String json = await FileHelper.ReadFile($"{Environment.CurrentDirectory}\\AllCardsConfig.json");
                JArray.Parse(json).Children().ToList().ForEach(o =>
                {
                    if (o.ToObject<CardContentModel>().IsChecked)
                    {
                        switch (o.ToObject<CardContentModel>().CardID)
                        {
                            case 0:
                                CardContents.Add(new CardContentModel() { CardName = "一言",CardID= o.ToObject<CardContentModel>().CardID, CardHeight= o.ToObject<CardContentModel>().CardHeight, CardContent = new UCOneWord()});
                                break;
                            case 1:
                                CardContents.Add(new CardContentModel() { CardName = "应用", CardID = o.ToObject<CardContentModel>().CardID, CardHeight = o.ToObject<CardContentModel>().CardHeight, CardContent = new UCusedApplications() });
                                break;
                            case 2:
                                CardContents.Add(new CardContentModel() { CardName = "文件夹", CardID = o.ToObject<CardContentModel>().CardID, CardHeight = o.ToObject<CardContentModel>().CardHeight, CardContent = new UCtempDirectory() });
                                break;
                            case 3:
                                CardContents.Add(new CardContentModel() { CardName = "文件", CardID = o.ToObject<CardContentModel>().CardID, CardHeight = o.ToObject<CardContentModel>().CardHeight, CardContent = new UcTempFile() });
                                break;
                            case 4:
                                CardContents.Add(new CardContentModel() { CardName = "便签", CardID = o.ToObject<CardContentModel>().CardID, CardHeight = o.ToObject<CardContentModel>().CardHeight, CardContent = new UCnotes() });
                                break;
                        }
                    }
                });
            }catch (Exception ex)
            {
                BaseDialog baseDialog = new BaseDialog();
                baseDialog.SetTitle("错误");
                baseDialog.SetContent(new UcMessageDialog(ex.Message, MyEnum.MessageDialogState.danger));
                baseDialog.ShowDialog();
            }
        }
    }
}
