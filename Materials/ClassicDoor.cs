using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    class ClassicDoor : Door
    {
        private bool handle;
        public ClassicDoor(float price, int lenght, string color, int width, bool handle)
        {
            this.price = price;
            this.length = length;
            this.color = color;
            this.width = width;
            this.handle = handle;
        }
        public override Object GetDescription()
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("price", price);
            Description.Add("length", length);
            Description.Add("width",width);
            Description.Add("color",color);
            Description.Add("handle",handle);
            return Description;
        }
        public void SetHandle(bool handle1)
        {
            this.handle = handle1;
        }
    }
}
