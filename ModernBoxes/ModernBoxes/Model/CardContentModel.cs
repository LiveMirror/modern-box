using GalaSoft.MvvmLight;
using System;

namespace ModernBoxes.Model
{
    public class CardContentModel : ViewModelBase
    {
        private int cardId;

        public int CardID
        {
            get { return cardId; }
            set { cardId = value; }
        }

        /// <summary>
        /// 卡片名称
        /// </summary>
        private String cardName;

        public String CardName
        {
            get { return cardName; }
            set { cardName = value; }
        }

        /// <summary>
        /// 卡片内容
        /// </summary>
        private Object cardContent;

        public Object CardContent
        {
            get { return cardContent; }
            set { cardContent = value; }
        }

        /// <summary>
        /// 卡片高度
        /// </summary>
        private Double cardHeight = 200;

        public Double CardHeight
        {
            get { return cardHeight; }
            set { cardHeight = value; RaisePropertyChanged("CardHeight"); }
        }

        /// <summary>
        /// 预览图
        /// </summary>
        private String priview;

        public String Priview
        {
            get { return priview; }
            set { priview = value; }
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        private Boolean isChecked;

        public Boolean IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; RaisePropertyChanged("IsChecked"); }
        }
    }
}