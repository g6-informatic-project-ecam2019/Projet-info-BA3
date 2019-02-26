using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    class Panel : Piece /*Class that groups all panels*/
    {
        private string color;
        private int width;
        public Panel(float price, int lenght, string color, int width) /*builder*/
        {
            this.price = price;
            this.length = length;
            this.color = color;
            this.width = width;
        }
        public override Dictionary<string, Object> GetDescription()/*Returns a dictionary with all panel information*/
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("price", price);
            Description.Add("length", length);
            Description.Add("width", width);
            Description.Add("color", color);
            return Description;
        }
    }
}
