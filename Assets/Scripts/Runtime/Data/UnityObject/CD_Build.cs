using System.Collections.Generic;
using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Build", menuName = "ATM_Rush/CD_Build", order = 0)]
    public class CD_Build : ScriptableObject
    {
        //public BuildData buildData;

        public List<BuildData> BuildDataList;
    }
}