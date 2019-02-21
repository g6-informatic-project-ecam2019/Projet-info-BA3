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
        public void SetPrice(float price) 
        {
            this.price = price;
        }
        public float GetPrice()
        {
            return price;
        }
        public abstract Object GetDescription();
    }
}
