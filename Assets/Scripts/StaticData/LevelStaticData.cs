using System.Collections.Generic;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/LevelData")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelName;

        public int MaxSections;
        [Min(1)] 
        public float TrackWidth;
        [Min(0.1f)] 
        public float TrackHeight;
        [Min(1)] 
        public float TrackLength;
        
        public Vector3 StartSectionPosition;
        public Vector3 PlayerSpawnPoint;
        public Vector3 SectionRespawnerOffset;
        [Min(0)]
        public float BlockSpawnSafezonePercentZ;
        [Min(0)] 
        public float BlockSpawnSafezonePercentX;
        public int LastBlockChance;
        [Min(1)] public int BlockInSectionAmount;
    }
}