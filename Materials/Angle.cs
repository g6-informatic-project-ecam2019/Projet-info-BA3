using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    class Angle : Piece
    {
        private string color;
        private string determiningDimension = "height";

        public Angle(float price, int length, string color) /*builder*/
        {
            this.price = price;
            this.length = length;
            this.color = color;
            this.name = "Cornieres";
        }

        public override Dictionary<string, Object> GetDescription()/*Returns a dictionary with all panel information*/
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("price", this.price);
            Description.Add("length", this.length);
            Description.Add("color", this.color);
            Description.Add("ref", this.name);
            Description.Add("dim", this.determiningDimension);
            return Description;
        }
    }
}
