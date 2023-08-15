using System.Collections.Generic;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "WallsProviderData", menuName = "StaticData/WallsProviderData")]
    public class WallsProviderStaticData : ScriptableObject
    {
        public List<GameObject> Variants;
    }
}