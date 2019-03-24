using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    class Cleat : Piece  /* Class that groups all cleats*/
    {
        private string determiningDimension = "heigth";
        public Cleat(float price, int length) /*builder*/
        {
            this.price = price;
            this.length =  length;
            this.name = "Tasseau";
        }
        public override Dictionary<string, Object> GetDescription()/*Returns a dictionary with all panel information*/
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("price", this.price);
            Description.Add("length", this.length);
            Description.Add("ref", this.name);
            Description.Add("dim", this.determiningDimension);
            return Description;
        }
    }
}
