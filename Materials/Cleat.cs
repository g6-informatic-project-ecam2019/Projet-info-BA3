using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    class Cleat : Piece
    {
        public Cleat(int price, int lenght)
        {
            this.price = price;
            this.length =  length;
        }
        public override Object GetDescription()
        {
            List<Object> Description = new List<Object>();
            Description.Add(price);
            Description.Add(length);
            return Description;
        }
    }
}
