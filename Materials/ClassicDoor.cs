using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    class ClassicDoor : Door /*Inheritance of the abstract class door, the classic door*/
    {
        
        private string color;
        public ClassicDoor(float price, int lenght, string color, int width) /*builder*/
        {
            this.price = price;
            this.length = length;
            this.color = color;
            this.width = width;
            this.type = "ClassicDoor";
            this.name = "porte";
        }
        public override Dictionary<string, Object> GetDescription()/*Returns a dictionary with all panel information*/
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("price", this.price);
            Description.Add("length", this.length);
            Description.Add("width", this.width);
            Description.Add("color", this.color);
            Description.Add("type", this.type);
            Description.Add("ref", this.name);
            Description.Add("dim1", this.determDim1);
            Description.Add("dim2", this.determDim2);
            return Description;
        }
    }
}
