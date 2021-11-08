using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ModernBoxes.Tool.convert
{
    public class MenuIconEnumptyConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "组件应用")
            {
                //返回组件应用的图标
                return new BitmapImage(new Uri(@"\Resource\image\menu.png", UriKind.Relative));
            }
            else if (value.ToString().Substring(value.ToString().LastIndexOf('.') + 1) == "ico")
            {
                //返回应用程序的图标
                return value.ToString();
            }
            else if (Directory.Exists(value.ToString()))
            {
                //返回文件夹图标
                return new BitmapImage(new Uri(@"\Resource\image\dir.png", UriKind.Relative));
            }
            //返回文件的图标
            switch (value.ToString().Substring(value.ToString().LastIndexOf('.') + 1).ToLower())
            {
                case "excel":
                case "xlsx":
                    return new BitmapImage(new Uri(@"\Resource\image\excel.png", UriKind.Relative));
                case "word":
                case "docx":
                case "rtf":
                    return new BitmapImage(new Uri(@"\Resource\image\word.png", UriKind.Relative));
                case "ppt":
                case "pptx":
                    return new BitmapImage(new Uri(@"\Resource\image\ppt.png", UriKind.Relative));
                case "mp3":
                    return new BitmapImage(new Uri(@"\Resource\image\music.png", UriKind.Relative));
                case "mp4":
                    return new BitmapImage(new Uri(@"\Resource\image\video.png", UriKind.Relative));
                case "java":
                case "c":
                case "py":
                case "cs":
                case "xml":
                case "xaml":
                case "cpp":
                    return new BitmapImage(new Uri(@"\Resource\image\program.png", UriKind.Relative));
                case "html":
                    return new BitmapImage(new Uri(@"\Resource\image\link.png", UriKind.Relative));
                case "png":
                case "jepg":
                case "jpg":
                case "ico":
                case "img":
                    return new BitmapImage(new Uri(@"\Resource\image\image.png", UriKind.Relative));
                case "gif":
                    return new BitmapImage(new Uri(@"\Resource\image\gif.png", UriKind.Relative));
                case "exe":
                    return new BitmapImage(new Uri(@"\Resource\image\exe.png", UriKind.Relative));
                case "pdf":
                    return new BitmapImage(new Uri(@"\Resource\image\pdf.png", UriKind.Relative));
                case "txt":
                    return new BitmapImage(new Uri(@"\Resource\image\txt.png", UriKind.Relative));
                case "wps":
                    return new BitmapImage(new Uri(@"\Resource\image\wps.png", UriKind.Relative));
                case "sql":
                case "db":
                case "log":
                case "accdb":
                case "mdb":
                    return new BitmapImage(new Uri(@"\Resource\image\db.png", UriKind.Relative));
                case "3ds":
                case "max":
                case "ma":
                case "mb":
                case "dwf":
                case "dwg":
                case "dxf":
                case "nwd":
                case "nwf":
                case "nwc":
                    return new BitmapImage(new Uri(@"\Resource\image\3d.png", UriKind.Relative));
                case "config":
                    return new BitmapImage(new Uri(@"\Resource\image\config.png", UriKind.Relative));
                case "zip":
                case "aar":
                case "7z":
                case "gz":
                case "rar":
                    return new BitmapImage(new Uri(@"\Resource\image\zip.png", UriKind.Relative));
                default:
                    return new BitmapImage(new Uri(@"\Resource\image\normal.png", UriKind.Relative));
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
