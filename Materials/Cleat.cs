using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    class Cleat : Piece
    {
        public Cleat(float price, int lenght)
        {
            this.price = price;
            this.length =  length;
        }
        public override Object GetDescription()
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("price", price);
            Description.Add("length", length);
            return Description;
        }
    }
}
