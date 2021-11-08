using GalaSoft.MvvmLight;
using ModernBoxes.Model;
using ModernBoxes.View.SelfControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBoxes.ViewModel
{
    public class UcManngerCardAppViewModel : ViewModelBase
    {
        private ObservableCollection<CardContentModel> needCardApps = new ObservableCollection<CardContentModel>();

        public ObservableCollection<CardContentModel> NeedCardApps
        {
            get { return needCardApps; }
            set { needCardApps = value;RaisePropertyChanged(); }
        }


        private ObservableCollection<CardContentModel> allCardApps = new ObservableCollection<CardContentModel>();

        public ObservableCollection<CardContentModel> AllCardApps
        {
            get { return allCardApps; }
            set { allCardApps = value; RaisePropertyChanged(); }
        }

        public UcManngerCardAppViewModel()
        {

        }
    }
}
