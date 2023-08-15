using UnityEngine;

namespace Infrastructure.Services.Random
{
    class RandomService : IRandomService
    {
        public bool Happend(int percentChance)
        {
            int rangeResult = UnityEngine.Random.Range(0, 100);
            return rangeResult < percentChance;
        }

        public float GetBetween(float from, float to) =>
            UnityEngine.Random.Range(from, to);
    }
}