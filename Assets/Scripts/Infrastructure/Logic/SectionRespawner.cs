using Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic
{
    public class SectionRespawner : MonoBehaviour
    {
        private LevelBuilder _levelBuilder;

        [Inject]
        public void Construct(LevelBuilder levelBuilder) =>
            _levelBuilder = levelBuilder;

        private void OnTriggerExit(Collider collidedObject)
        {
            if (collidedObject.tag == "LevelSection")
            {
                _levelBuilder.RemoveSection(collidedObject.transform.parent.gameObject);
                _levelBuilder.SpawnLevelSectionIfNotEnoughAnim();
            }
        }
    }
}