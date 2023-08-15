using System;
using System.Collections.Generic;
using Infrastructure.Services.Random;
using Infrastructure.Services.StaticData;
using StaticData;
using UnityEngine;

namespace Infrastructure.Services.WallsProvider
{
    public class WallsProviderService : IWallsProviderService
    {
        private readonly IRandomService _randomService;
        private List<GameObject> _wallsVariants;

        public WallsProviderService(IRandomService randomService, IStaticDataService staticDataService)
        {
            _randomService = randomService;
            SetWallsVariants(staticDataService);
        }

        private void SetWallsVariants(IStaticDataService staticDataService)
        {
            WallsProviderStaticData wallsVariantsStaticData = staticDataService.GetWallsProviderStaticData();
            _wallsVariants = wallsVariantsStaticData.Variants;
        }

        public GameObject GetRandomWallVariant()
        {
            int variantsCount = _wallsVariants.Count;

            if (variantsCount == 0)
                throw new Exception("You have 0 wall variant to spawn. Add it to WallsProviderStaticData");
            
            int randomNumber = (int)_randomService.GetBetween(0, variantsCount);
            return _wallsVariants[randomNumber];
        }
    }
}