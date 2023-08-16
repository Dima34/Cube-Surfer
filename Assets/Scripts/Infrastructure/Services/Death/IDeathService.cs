using System;

namespace Infrastructure.Services.Death
{
    public interface IDeathService
    {
        event Action Happend;
        void Die();
    }
}