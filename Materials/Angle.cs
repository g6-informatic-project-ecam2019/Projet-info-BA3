using System;
using System.Collections.Generic;

namespace Materials
{
    public class Angle : Part
    {
        private string color;
        private string determiningDimension = "height";

        /*Builder*/
        public Angle(int length, string color)
        {
            this.length = length;
            this.color = color;
            this.name = "Cornieres";
        }

        /*Returns a dictionary with all panel information*/
        public override Dictionary<string, Object> GetDescription()
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

        /*Setter for angle's length
         * Height of cupboard might not be available in stock => select another angle*/
        public override void SetLength (int newHeight)
        {
            Console.WriteLine(String.Format("WARNING : changing the length of the angles from {0} to {1}", this.length, newHeight));
            this.length = newHeight;
        }
    }
}
