using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    class Breadth : Piece  /* Class that groups all breadths*/
    {
        private string pos;
        public Breadth(float price, int lenght, string pos)/*builder*/
        {
            this.price = price;
            this.length = length;
            this.pos = pos;
            this.name = String.Format("Traverse {0}", pos);
        }
        public override Dictionary<string, Object> GetDescription()/*Returns a dictionary with all panel information*/
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("price", this.price);
            Description.Add("length", this.length);
            Description.Add("pos", this.pos);
            Description.Add("ref", this.name);
            return Description;
        }
    }
}
