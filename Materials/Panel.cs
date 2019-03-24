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
        private string position;
        private string determDim1;
        private string determDim2;
        public Panel(float price, int length, string color, int width, string position) /*builder*/
        {
            this.price = price;
            this.length = length;
            this.color = color;
            this.width = width;
            this.position = position;
            this.name = String.Format("Panneau {0}", this.position);
            if (this.position == "GD")
            {
                this.determDim1 = "heigth";
                this.determDim2 = "depth";
            }
            else if (this.position == "HB")
            {
                this.determDim1 = "depth";
                this.determDim2 = "width";
            }
            else
            {
                this.determDim1 = "heigth";
                this.determDim2 = "width";
            }
        }
        public override Dictionary<string, Object> GetDescription()/*Returns a dictionary with all panel information*/
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("price", this.price);
            Description.Add("length", this.length);
            Description.Add("width", this.width);
            Description.Add("color", "Blanc");
            Description.Add("pos", this.position);
            Description.Add("ref", this.name);
            Description.Add("dim1", this.determDim1);
            Description.Add("dim2", this.determDim2);
            return Description;
        }
    }
}
