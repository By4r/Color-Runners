using System;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct BuildData
    {
        public GameObject buildingPrefab;

        public int buildRequirement;
    }
}