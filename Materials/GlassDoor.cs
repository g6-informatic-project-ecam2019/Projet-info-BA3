using System;
using System.Collections.Generic;

namespace Materials
{
    /*Inheritance of the abstract class door*/
    class GlassDoor : Door
    {
        /*Builder*/
        public GlassDoor(float price, int length, int width)
        {
            this.price = price;
            this.length = length;
            this.width = width;
            /*Door type information(glass door or classic door)*/
            this.type = "GlassDoor";
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
                { "type", this.type },
                { "ref", this.name },
                { "code", this.code },
                { "dim1", this.determiningDimension1 },
                { "dim2", this.determiningDimension2 }
            };
            return Description;
        }

        /*Checks if the dimension is in the list
         * If so, adds the measures in the list.*/
        public bool Dimension(int lenght)
        {
            List<int> dimensionIOS = new List<int>();

            foreach (int i in dimensionIOS)
            {
                if (dimensionIOS[i] == lenght)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
