using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using ModernBoxes.Model;
using ModernBoxes.Tool;
using ModernBoxes.View.SelfControl;
using ModernBoxes.View.SelfControl.dialog;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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


        /// <summary>
        /// 关闭对话框
        /// </summary>
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
                        if (File.Exists(Menu.Target)||Directory.Exists(Menu.Target))
                        {
                            //获取旧数据
                            String path = $"{Environment.CurrentDirectory}\\MenuConfig.json";
                            String oldJson = await FileHelper.ReadFile(path);
                            if (Menu.Target == "组件应用" || Menu.MenuName == "组件应用")
                            {
                                Menu.Icon = "组件应用";
                            }
                            if (oldJson.Length > 8)
                            {
                                JArray array = JArray.Parse(oldJson);
                                IList<JToken> jTokens = array.Children().ToList();
                                foreach (JToken jToken in jTokens)
                                {
                                    Menus.Add(jToken.ToObject<MenuModel>());
                                }
                                //添加新数据
                                Menus.Add(Menu);
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
                            BaseDialog baseDialog = new BaseDialog();
                            baseDialog.SetTitle("提示");
                            baseDialog.SetContent(new UcMessageDialog("路径所指向的文件或文件夹不存在", MyEnum.MessageDialogState.waring));
                            baseDialog.ShowDialog();
                        }
                    }
                    else
                    {
                        BaseDialog baseDialog = new BaseDialog();
                        baseDialog.SetTitle("提示");
                        baseDialog.SetContent(new UcMessageDialog("路径和名称不能为空", MyEnum.MessageDialogState.waring));
                        baseDialog.ShowDialog();
                    }
                }, x => true);
            }
        }

        /// <summary>
        /// 选择文件
        /// </summary>
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
                        if (Menu.Target.Substring(Menu.Target.LastIndexOf('.')+1)=="exe")
                        {
                            String iconPath = $"{Environment.CurrentDirectory}\\icons\\";
                            String FileName = $"{Convert.ToString(DateTime.Now.Year) + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second}.ico";
                            GetIcon.getFileIcon(Menu.Target, iconPath, FileName);
                            Menu.Icon = $"{Environment.CurrentDirectory}\\icons\\{FileName}";
                        }
                        else
                        {
                            Menu.Icon = Menu.Target.Substring(Menu.Target.LastIndexOf('.') + 1);
                        }
                    }
                },x => true);
            }
        }

        /// <summary>
        /// 选择文件夹
        /// </summary>
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
                        Menu.Icon = dialog.SelectedPath;
                    }
                }, x => true);
            }
        }

        public AddMenuDialogViewModel()
        {
            
        }
    }
}
