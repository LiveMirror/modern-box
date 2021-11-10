using GalaSoft.MvvmLight;
using ModernBoxes.Model;
using ModernBoxes.Tool;
using ModernBoxes.View.SelfControl;
using ModernBoxes.View.SelfControl.dialog;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace ModernBoxes.ViewModel
{
    public class OneWordViewModel : ViewModelBase
    {
        private OneWordModel oneWord = new OneWordModel();

        public OneWordModel OneWord
        {
            get { return oneWord; }
            set { oneWord = value; RaisePropertyChanged("OneWord"); }
        }

        public RelayCommand RefershOneWord
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    loadOneNote();
                }, x => true);
            }
        }

        public OneWordViewModel()
        {
            loadOneNote();
        }

        private async void loadOneNote()
        {
            try
            {
                var client = new RestClient("https://v1.hitokoto.cn/");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = await client.ExecuteAsync(request);
                if (response != null)
                {
                    OneWord = JsonConvert.DeserializeObject<OneWordModel>(response.Content);
                }
            }
            catch (Exception ex)
            {
                BaseDialog baseDialog = new BaseDialog();
                baseDialog.SetTitle("错误");
                baseDialog.SetContent(new UcMessageDialog(ex.Message, MyEnum.MessageDialogState.danger));
                baseDialog.ShowDialog();
            }
        }
    }
}