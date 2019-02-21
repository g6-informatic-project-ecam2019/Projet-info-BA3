using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    abstract class Door : Piece
    {
        
        protected string color;
        protected int width;
        public override abstract Object GetDescription();
    }
}
