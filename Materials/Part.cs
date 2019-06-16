using System;
using System.Collections.Generic;

namespace Materials
{
    /*Abstract class that groups all the parts*/
    public abstract class Part
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
        public void setCode(string code)
        {
            this.code = code;
        }
        public float GetPrice()
        {
            return this.price;
        }

        public bool IsAvailable(Stock pricestock)
        {
            this.available = pricestock.IsAvailable(this);
            return available;
        }

        public abstract Dictionary<string, Object> GetDescription();

        public void DescriptionRequest(Stock pricestock)
        {
            this.available = pricestock.IsAvailable(this);
        }

        public virtual void SetLength(int newLength) { }
    }
}
