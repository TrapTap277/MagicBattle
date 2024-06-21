using _Scripts.DisableGameObject;
using UnityEngine;

namespace _Scripts.LostScene
{
    public class PassingLevelFactory : MonoBehaviour
    {
        public void CreatePassingLevel()
        {
            var portalManager = FindObjectOfType<OpenCloseManager>();

            var passingLevel = gameObject.AddComponent<PassingLevel>();
            passingLevel.SetDependencies(portalManager);
        }
    }
}