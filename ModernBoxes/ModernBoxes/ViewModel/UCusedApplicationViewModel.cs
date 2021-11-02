using ModernBoxes.Tool;
using ModernBoxes.View.SelfControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBoxes.ViewModel
{
    public class UCusedApplicationViewModel
    {
        /// <summary>
        /// 打开应用添加对话框
        /// </summary>
        public RelayCommand OpenAddApplicationDialog
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    BaseDialog baseDialog = new BaseDialog();
                    baseDialog.SetTitle("添加应用");
                    baseDialog.SetContent(new UCAddApplicationDialog());
                    baseDialog.ShowDialog();
                }, x => true);
            }
        }

        /// <summary>
        /// 管理应用
        /// </summary>
        public RelayCommand ManagerApplication
        {
            get
            {
                return new RelayCommand((o) =>
                {

                }, x => true);
            }
        }
    }
}
