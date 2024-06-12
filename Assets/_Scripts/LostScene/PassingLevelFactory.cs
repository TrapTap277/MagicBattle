using _Scripts.DisableGameObject;
using _Scripts.Staff;
using UnityEngine;

namespace _Scripts.LostScene
{
    public class PassingLevelFactory : MonoBehaviour
    {
        public PassingLevel CreateOpenDoor()
        {
            var portalManager = FindObjectOfType<PortalManager>();
            var doorManager = FindObjectOfType<DoorManager>();
            var staffAnimationController = FindObjectOfType<StaffSwitchAnimation>();

            var openDoor = gameObject.AddComponent<PassingLevel>();
            openDoor.SetDependencies(portalManager, doorManager, staffAnimationController);

            return openDoor;
        }
    }
}