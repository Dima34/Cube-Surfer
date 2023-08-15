using UnityEngine;

namespace Infrastructure.Services.Random
{
    public interface IRandomService
    {
        bool Happend(int percentChance);
        float GetBetween(float from, float to);
    }
}