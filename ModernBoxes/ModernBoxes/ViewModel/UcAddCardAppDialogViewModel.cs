using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using HandyControl.Controls;
using ModernBoxes.Model;
using ModernBoxes.Tool;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace ModernBoxes.ViewModel
{
    public class UcAddCardAppDialogViewModel : ViewModelBase
    {
        private ObservableCollection<CardContentModel> cards = new ObservableCollection<CardContentModel>();

        public ObservableCollection<CardContentModel> CardApps
        {
            get { return cards; }
            set { cards = value; RaisePropertyChanged("Cards"); }
        }

        public RelayCommand CardHeightChange
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    PreviewSlider previewSlider = o as PreviewSlider;
                    if (previewSlider != null)
                    {
                        String tag = previewSlider.Tag.ToString();
                        Double value = previewSlider.Value;
                        UcCompontentViewModel.DoChangeCardAppHeight(Convert.ToInt32(tag), value);
                    }
                }, x => true);
            }
        }

        public UcAddCardAppDialogViewModel()
        {
            //注册消息，用于关闭保存信息
            Messenger.Default.Register<Boolean>(this, "ClosingDialog", SaveData);
            init();
        }

        private async void init()
        {
            String json = await FileHelper.ReadFile($"{Environment.CurrentDirectory}\\AllCardsConfig.json");
            JArray jArray = JArray.Parse(json);
            jArray.Children().ToList().ForEach(o => CardApps.Add(o.ToObject<CardContentModel>()));
        }

        public async void SaveData(Boolean bol)
        {
            //当点击关闭对话框按钮时，保存已经设置好的数据
            if (File.Exists($"{Environment.CurrentDirectory}\\AllCardsConfig.json"))
            {
                File.Delete($"{Environment.CurrentDirectory}\\AllCardsConfig.json");
            }
            string newJson = JsonConvert.SerializeObject(CardApps);
            await FileHelper.WriteFile($"{Environment.CurrentDirectory}\\AllCardsConfig.json", newJson);
            UcCompontentViewModel.DoloadCardContent();
        }
    }
}