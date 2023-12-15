using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct CollectableData
    {
        public CollectableColorData ColorData;
    }


    [Serializable]
    public struct CollectableColorData
    {
        public List<Material> MaterialsList;
    }
}