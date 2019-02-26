using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    class GlassDoor : Door /*/Inheritance of the abstract class door, the glass door*/
    {
        public GlassDoor(float price, int lenght, int width) //builder
        {
            this.price = price;
            this.length = length;
            this.width = width;
            this.type = "GlassDoor"; /*Door type information(glassdoor or classicdoor)*/
        }
        public override Dictionary<string, Object> GetDescription()/*Returns a dictionary with all panel information*/
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("price", price);
            Description.Add("length", length);
            Description.Add("width", width);
            Description.Add("type", type);
            return Description;
        }
        public bool Dimention(int lenght)/*Check if the dimension is in the list, so add the measures in the list.*/
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
