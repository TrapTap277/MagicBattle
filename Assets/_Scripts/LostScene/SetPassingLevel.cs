using UnityEngine;

namespace _Scripts.LostScene
{
    public class SetPassingLevel : MonoBehaviour
    {
        private PassingLevelFactory _passingLevelFactory;

        private void Awake()
        {
            CreatePassingLevel();
        }

        private void CreatePassingLevel()
        {
            _passingLevelFactory = FindObjectOfType<PassingLevelFactory>();
            _passingLevelFactory.CreatePassingLevel();
        }
    }
}