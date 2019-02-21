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
        public ClassicDoor(int price, int lenght, string color, int width, bool handle)
        {
            this.price = price;
            this.length = length;
            this.color = color;
            this.width = width;
            this.handle = handle;
        }
        public override Object GetDescription()
        {
            List<Object> Description = new List<Object>();
            Description.Add(price);
            Description.Add(length);
            Description.Add(width);
            Description.Add(color);
            Description.Add(handle);
            return Description;
        }
        public void SetHandle(bool handle1)
        {
            this.handle = handle1;
        }
    }
}
