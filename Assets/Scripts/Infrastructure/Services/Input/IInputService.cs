using System;

namespace Infrastructure.Services.Input
{
    public interface IInputService
    {
        float XAxis { get; }
        bool OnStartTap { get; }
    }
}