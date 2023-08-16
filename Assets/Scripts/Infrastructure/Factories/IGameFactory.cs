using System.Collections.Generic;
using Cinemachine;
using Infrastructure.Logic;
using Infrastructure.Logic.Player;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IGameFactory
    {
        GameObject SpawnSectionAndAddToActiveList();
        GameObject SpawnSectionRespawner();
        GameObject SpawnPlayerAndRegisterCubeHolder();
        CinemachineVirtualCamera SpawnCamera();
        List<GameObject> ActiveLevelSections { get; }
        GameObject Player { get; }
        GameObject WarpEffect { get; }
        void DestroySection(GameObject levelSection);
        GameObject SpawnGround();
        GameObject CreateCollectableCube();
        GameObject SpawnCollectedCube();
        GameObject SpawnRandomWall();
        void DestroyPlayer();
        void DestroyAllASections();
        void DestroyCamera();
        void DestroySectionRespawner();
        GameObject CreateTrail();
        void DestroyTrail();
        CubePickupText SpawnCubePickupText();
        void DestroyCubePickupText(CubePickupText text);
        void DestroyAllCubePickupText();
        GameObject SpawnWarpEffectDisabled();
        void DestroyWarpEffect();
    }
}