using System;
using System.Collections.Generic;

namespace Materials
{
    /*Class that groups all panels*/
    class Panel : Part
    {
        private string color;
        private int width;
        private string position;
        private string determiningDimension1;
        private string determiningDimension2;

        /*Builder*/
        public Panel(float price, int length, string color, int width, string position)
        {
            this.price = price;
            this.length = length;
            this.color = color;
            this.width = width;
            this.position = position;
            this.name = String.Format("Panneau {0}", this.position);
            if (this.position == "GD")
            {
                this.determiningDimension1 = "height";
                this.determiningDimension2 = "depth";
            }
            else if (this.position == "HB")
            {
                this.determiningDimension1 = "width";
                this.determiningDimension2 = "depth";
            }
            else
            {
                this.determiningDimension1 = "height";
                this.determiningDimension2 = "width";
            }
        }

        /*Returns a dictionary with all panel information*/
        public override Dictionary<string, Object> GetDescription()
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>
            {
                { "price", this.price },
                { "length", this.length },
                { "width", this.width },
                { "color", this.color },
                { "code", this.code },
                { "position", this.position },
                { "ref", this.name },
                { "dim1", this.determiningDimension1 },
                { "dim2", this.determiningDimension2 }
            };
            return Description;
        }
    }
}
