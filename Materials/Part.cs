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

        public float GetPrice()
        {
            return this.price;
        }

        public bool IsAvailable()
        {
            return available;
        }

        public abstract Dictionary<string, Object> GetDescription();

        public void DescriptionRequest(Stock pricestock)
        {
            //Console.WriteLine(String.Format("name of this part is {0}", this.name));
            this.code = pricestock.GetPartDescription(this)["code"].ToString();
            //Console.WriteLine(String.Format("code of this part is {0}", code));
            SetPrice((float)pricestock.GetPartDescription(this)["client price"]);
            Console.WriteLine(String.Format("part : {0}", this.code));
            Console.WriteLine(this.price);
            Console.WriteLine("\n");
            
            this.available = pricestock.IsAvailable(this);
        }

        public virtual void SetLength(int newLength) { }
    }
}
