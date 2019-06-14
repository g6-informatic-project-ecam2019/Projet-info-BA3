using System;
using System.Collections.Generic;

namespace Materials
{
    public interface Block
    {
        Dictionary<string, Object> GetDescription();
        float GetPrice();
        void BuildParts();
        Part[] GetParts();
    }
}
