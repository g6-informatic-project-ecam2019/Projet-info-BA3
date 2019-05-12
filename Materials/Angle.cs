using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    public class Angle : Piece
    {
        private string color;
        private string determiningDimension = "height";

        public Angle(int length, string color) /*builder*/
        {
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
            Description.Add("code", this.code);
            Description.Add("dim", this.determiningDimension);
            return Description;
        }
        public override void setLength (int newHeight) //setter for angle's length => height of cupboard might not be available in stock => select another angle
        {
            Console.WriteLine(String.Format("WARNING : changing the length of the angles from {0} to {1}", this.length, newHeight));
            this.length = newHeight;
        }
    }
}
