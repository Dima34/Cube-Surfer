using System;
using Infrastructure.Installer;

namespace Infrastructure.Services.Death
{
    public class DeathService : IDeathService
    {
        public event Action Happend;
        
        public void Die() =>
            Happend?.Invoke();
    }
}