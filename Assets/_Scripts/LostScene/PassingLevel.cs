using UnityEngine;

namespace _Scripts.LostScene
{
    public class PassingLevel : MonoBehaviour
    {
        //public List<CurrentAbility> _allAbilities; // Todo Instantiate random ability

        private IPortalManager _portalManager;
        private IDoorManager _doorManager;
        private IStaffAnimationController _staffAnimationController;

        public void SetDependencies(IPortalManager portalManager, IDoorManager doorManager,
            IStaffAnimationController staffAnimationController)
        {
            _portalManager = portalManager;
            _doorManager = doorManager;
            _staffAnimationController = staffAnimationController;
        }

        public void FadeOrShowPortal(bool isShow)
        {
            if (isShow)
            {
                SetRandomAttackAnimation();
                _portalManager?.Open();
            }

            if (!isShow)
                _portalManager?.Close();
        }

        public void DestroyDoorAsync()
        {
            DestroyDoor();
            //yield return new WaitForSeconds(2f);

            //StartCoroutine(UseMagic());

            //yield return new WaitForSeconds(4f);
        }


        public void SetRandomAttackAnimation()
        {
            _staffAnimationController?.SwitchAnimation();
        }

        public void SetFadeAnimation()
        {
            _staffAnimationController.SetFadeAnimation();
        }  
        
        public void SetShowAnimation()
        {
            _staffAnimationController.SetShowStaff();
        }

        private void DestroyDoor()
        {
            _doorManager?.DestroyDoor();
        }

        // private IEnumerator UseMagic()
        // {
        //     if (_allAbilities.Count > 0)
        //     {
        //         var getRandomAbility = Random.Range(0, _allAbilities.Count);
        //         GameObject newAbility = Instantiate(_allAbilities[getRandomAbility].Effect, 
        //             gameObject.transform.position, Quaternion.identity); // Todo Instantiate random ability
        //
        //         yield return new WaitForSeconds(4f);
        //
        //         var explosion = GameObject.FindGameObjectWithTag("Destroy");
        //
        //         if (newAbility != null) Destroy(newAbility);
        //
        //         if (explosion != null) Destroy(explosion);
        //     }
        // }
    }
}