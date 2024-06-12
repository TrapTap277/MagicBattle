using UnityEngine;

namespace _Scripts.LostScene
{
    public class PassingLevelController : MonoBehaviour
    {
        private PassingLevelFactory _passingLevelFactory;
        private PassingLevel _passingLevel;

        private const bool ClosePortal = false;

        private void Awake()
        {
            SetPassingLevel();

            OpenOrClosePortal(ClosePortal);
        }

        public void OpenOrClosePortal(bool isShow)
        {
            _passingLevel.FadeOrShowPortal(isShow);
        }

        public void OpenDoor()
        {
            _passingLevel.DestroyDoorAsync();
        }

        public void SetAnimation()
        {
            _passingLevel.SetRandomAttackAnimation();
        }

        public void SetFadeAnimation()
        {
            _passingLevel.SetFadeAnimation();
        }   
        
        public void SetShowAnimation()
        {
            _passingLevel.SetShowAnimation();
        }
        private void SetPassingLevel()
        {
            _passingLevelFactory = FindObjectOfType<PassingLevelFactory>();
            _passingLevel = _passingLevelFactory.CreateOpenDoor();
        }
    }
}