using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.Model;
using ModernBoxes.Tool;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModernBoxes.ViewModel
{
    public class AddMenuDialogViewModel : ViewModelBase
    {

        private MenuModel menuModel = new MenuModel();

        public MenuModel Menu
        {
            get { return menuModel; }
            set { menuModel = value;RaisePropertyChanged("Menu"); }
        }


        private List<MenuModel> menuModels = new List<MenuModel>();

        public List<MenuModel> Menus
        {
            get { return menuModels; }
            set { menuModels = value; }
        }



        public RelayCommand CloseDialog
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    if (o!=null)
                    {
                        Window window = o as Window;
                        window.Close();
                    }
                }, x => true);
            }
        }

        /// <summary>
        /// 添加菜单选项
        /// </summary>
        public RelayCommand AddMenu
        {
            get
            {
                return new RelayCommand(async (o) =>
                {
                    if (Menu.MenuName!=String.Empty&&Menu.MenuName!=null&&Menu.Target!=String.Empty)
                    {
                        //获取旧数据
                        String path = $"{Environment.CurrentDirectory}\\MenuConfig.json";
                        String oldJson = await FileHelper.ReadFile(path);
                        if (oldJson.Length>8)
                        {
                            JArray array = JArray.Parse(oldJson);
                            IList<JToken> jTokens = array.Children().ToList();
                            foreach (JToken jToken in jTokens)
                            {
                                Menus.Add(jToken.ToObject<MenuModel>());
                            }
                            //添加新数据
                            Menus.Add(Menu);
                            //的到新的json
                        }
                        else
                        {
                            //添加新数据
                            Menus.Add(Menu);
                        }
                        String newJson = JsonConvert.SerializeObject(Menus);
                        FileHelper.WriteFile(path, newJson);
                        //刷新数据
                        MainViewModel.DoRefershMenu();
                        //关闭对话框
                        Messenger.Default.Send<Boolean>(true, "IsCloseDialog");
                    }
                    else
                    {

                    }
                }, x => true);
            }
        }

        public RelayCommand ChooseFilePath
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                    openFileDialog.Filter = "*|*";
                    openFileDialog.Title = "选择一个文件吧";
                    if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Menu.Target = openFileDialog.FileName;
                    }
                },x => true);
            }
        }

        public RelayCommand ChooseDirPath
        {
            get
            {
                return new RelayCommand((o) =>
                {
                    System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Menu.Target = dialog.SelectedPath;
                    }
                }, x => true);
            }
        }

        public AddMenuDialogViewModel()
        {
            
        }
    }
}
