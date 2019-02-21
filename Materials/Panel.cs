using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    class Panel : Piece
    {
        private string color;
        private int width;
        public Panel(int price, int lenght, string color, int width)
        {
            this.price = price;
            this.length = length;
            this.color = color;
            this.width = width;
        }
        public override Object GetDescription()
        {
            List<Object> Description = new List<Object>();
            Description.Add(price);
            Description.Add(length);
            Description.Add(width);
            Description.Add(color);
            return Description;
        }
    }
}
