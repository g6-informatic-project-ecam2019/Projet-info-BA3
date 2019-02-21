﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    class Breadth : Piece  /* Class that groups all breadths*/
    {
        public Breadth(float price, int lenght)/*builder*/
        {
            this.price = price;
            this.length = length;
        }
        public override Object GetDescription()/*Returns a dictionary with all panel information*/
        {
            Dictionary<string, Object> Description = new Dictionary<string, Object>();
            Description.Add("price",price);
            Description.Add("length",length);
            return Description;
        }
    }
}
