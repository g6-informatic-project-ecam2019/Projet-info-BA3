using System;
using System.Collections.Generic;

namespace Materials
{
    public interface Bloc
    {
        Dictionary<string, Object> GetDescription();
        float GetPrice();
        void BuildParts();
        Part[] GetParts();
    }
}
