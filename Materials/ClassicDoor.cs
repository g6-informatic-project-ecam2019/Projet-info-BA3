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
        }
        public override Dictionary<string, Object> GetDescription()/*Returns a dictionary with all panel information*/
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("price", price);
            Description.Add("length", length);
            Description.Add("width", width);
            Description.Add("color", color);
            
            Description.Add("type", type);
            return Description;
        }
        public void SetHandle(bool handle1)/*Adds or removes a handle*/
        {
            this.handle = handle1;
        }
    }
}
