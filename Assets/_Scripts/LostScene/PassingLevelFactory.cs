using _Scripts.Animations;
using _Scripts.DisableGameObject;
using UnityEngine;

namespace _Scripts.LostScene
{
    public class PassingLevelFactory : MonoBehaviour
    {
        public void CreatePassingLevel()
        {
            var portalManager = FindObjectOfType<OpenCloseManager>();
            var doorManager = FindObjectOfType<DoorManager>();
            var staffAnimationController = FindObjectOfType<StaffAnimationSwitcher>();

            var passingLevel = gameObject.AddComponent<PassingLevel>();
            passingLevel.SetDependencies(portalManager, doorManager, staffAnimationController);
        }
    }
}