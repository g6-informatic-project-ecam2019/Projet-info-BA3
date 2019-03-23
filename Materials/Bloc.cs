using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Materials
{
    interface Bloc
    {
        Dictionary<string, Object> GetDescription();
        double GetPrice();
        void BuildPieces();
        Piece[] GetPieces();
    }
}
