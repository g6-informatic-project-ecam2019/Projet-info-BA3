using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    class GlassDoor : Door
    {
        public GlassDoor(float price, int lenght, int width)
        {
            this.price = price;
            this.length = length;
            this.width = width;
        }
        public override Object GetDescription()
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("price", price);
            Description.Add("length", length);
            Description.Add("width", width);
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
