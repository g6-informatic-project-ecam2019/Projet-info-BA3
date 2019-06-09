using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    /*Inheritance of the abstract class door, the classic door*/
    class ClassicDoor : Door
    {
        private string color;

        public ClassicDoor(float price, int length, string color, int width) /*builder*/
        {
            this.price = price;
            this.length = length;
            this.color = color;
            this.width = width;
            this.type = "ClassicDoor";
            this.name = "porte";
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
                { "type", this.type },
                { "ref", this.name },
                { "dim1", this.determiningDimension1 },
                { "dim2", this.determiningDimension2 },
                { "code", this.code }
            };
            return Description;
        }
    }
}
