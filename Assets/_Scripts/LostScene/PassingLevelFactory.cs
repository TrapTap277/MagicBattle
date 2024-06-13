using _Scripts.DisableGameObject;
using _Scripts.Staff;
using UnityEngine;

namespace _Scripts.LostScene
{
    public class PassingLevelFactory : MonoBehaviour
    {
        public void CreatePassingLevel()
        {
            var useMagic = FindObjectOfType<UseMagic>();
            var portalManager = FindObjectOfType<PortalManager>();
            var doorManager = FindObjectOfType<DoorManager>();
            var staffAnimationController = FindObjectOfType<StaffSwitchAnimation>();

            var passingLevel = gameObject.AddComponent<PassingLevel>();
            passingLevel.SetDependencies(portalManager, doorManager, staffAnimationController, useMagic);
        }
    }
}