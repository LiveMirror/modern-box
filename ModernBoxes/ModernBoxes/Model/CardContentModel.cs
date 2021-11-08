using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernBoxes.Model
{
    public class CardContentModel
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
            set { cardHeight = value; }
        }


    }
}
