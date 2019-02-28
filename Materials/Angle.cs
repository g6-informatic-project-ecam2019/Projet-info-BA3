using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    class Angle
    {
        private float price;
        private int length;
        private string color;

        public Angle(float price, int lenght, string color) /*builder*/
        {
            this.price = price;
            this.length = length;
            this.color = color;
        }

        public void SetPrice(float price)
        {
            this.price = price;
        }

        public Dictionary<string, Object> GetDescription()/*Returns a dictionary with all panel information*/
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("price", this.price);
            Description.Add("length", this.length);
            Description.Add("color", this.color);
            return Description;
        }
    }
}
