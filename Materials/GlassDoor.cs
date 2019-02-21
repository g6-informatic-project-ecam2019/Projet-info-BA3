using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    class GlassDoor : Door
    {
        public GlassDoor(int price, int lenght, string color, int width)
        {
            this.price = price;
            this.length = length;
            this.color = color;
            this.width = width;
        }
        public override Object GetDescription()
        {
            List<Object> Description = new List<Object>();
            Description.Add(price);
            Description.Add(length);
            Description.Add(width);
            Description.Add(color);
            return Description;
        }
        public bool Dimention(int lenght)
        {
            List<int> dimentionIOS = new List<int>();

            foreach (int i in dimentionIOS)
            {
                if (dimentionIOS[i] == lenght)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
