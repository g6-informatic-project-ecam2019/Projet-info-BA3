using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    abstract class Piece /*Abstract class that groups all the parts*/
    {
        protected float price;
        protected int length;
        protected string name;
        protected string code;
        protected bool available;
        public void SetPrice(float price)
        {
            this.price = price;
        }
        public float GetPrice()
        {
            return this.price;
        }
        public abstract Dictionary<string, Object> GetDescription();
        public void DescriptionRequest(Stock pricestock)
        {
            SetPrice((float)pricestock.getPieceDescription(this)["client price"]);
            this.code = pricestock.getPieceDescription(this)["code"].ToString();
            this.available = pricestock.isAvailable(this);
        }
    }
}
