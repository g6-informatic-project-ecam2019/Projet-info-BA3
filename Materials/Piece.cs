using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    abstract class Piece
    {
        protected int price;
        protected int length;
        public void SetPrice(int price)
        {
            this.price = price;

        }
        public int GetPrice()
        {
            return price;
        }
        public abstract Object GetDescription();
    }
}
