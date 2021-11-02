using GalaSoft.MvvmLight;
using ModernBoxes.Model;
using ModernBoxes.Tool;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void loadOneNote()
        {
            var client = new RestClient("https://api.muxiaoguo.cn/api/yiyan");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "__yjs_duid=1_c7c57d75a2a44f7fb435906e302ceb061635836089220; PHPSESSID=cv64br18t9sdfid1ec96u87pn9");
            IRestResponse response = client.Execute(request);
            if (response != null)
            {
                OneWord = JsonConvert.DeserializeObject<OneWordModel>(response.Content);
            }
        }
    }
}
