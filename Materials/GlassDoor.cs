using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    class GlassDoor : Door /*/Inheritance of the abstract class door, the glass door*/
    {
        public GlassDoor(float price, int length, int width) //builder
        {
            this.price = price;
            this.length = length;
            this.width = width;
            this.type = "GlassDoor"; /*Door type information(glassdoor or classicdoor)*/
            this.name = "porte";
        }
        public override Dictionary<string, Object> GetDescription()/*Returns a dictionary with all panel information*/
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("price", this.price);
            Description.Add("length", this.length);
            Description.Add("width", this.width);
            Description.Add("type", this.type);
            Description.Add("ref", this.name);
            Description.Add("dim1", this.determDim1);
            Description.Add("dim2", this.determDim2);
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
