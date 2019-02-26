using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    abstract class Door : Piece /*Abstract class for all types of doors*/
    {
        protected int width;
        protected string type;/*Variable that contains either glassdoor or classicdoor*/
        public override abstract Dictionary<string, Object> GetDescription();
    }
}
