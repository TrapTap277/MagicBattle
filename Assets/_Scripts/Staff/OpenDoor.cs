using System.Collections;
using System.Collections.Generic;
using _Scripts.Move;
using UnityEngine;

namespace _Scripts.Staff
{
    public class OpenDoor : MonoBehaviour
    {
        //public List<CurrentAbility> _allAbilities; // Todo Instantiate random ability

        [SerializeField] private Animator _staffAnimation;
        [SerializeField] private GameObject _portal;
        [SerializeField] private List<GameObject> _doors;

        public void DestroyDoor(int index)
        {
            StartCoroutine(DestroyDoorWithTime(index));
        }

        public void OpenPortal()
        {
            StartCoroutine(OpenPortalWithTime());
        }

        public IEnumerator OpenPortalWithTime()
        {
            if (_staffAnimation != null)
            {
                _staffAnimation.enabled = true;

                yield return new WaitForSeconds(2f);

                _staffAnimation.enabled = false;
            }

            if (_portal != null) _portal.SetActive(true);
        }

        public IEnumerator DestroyDoorWithTime(int index)
        {
            if (_staffAnimation != null)
            {
                _staffAnimation.enabled = true;

                yield return new WaitForSeconds(2f);

                //StartCoroutine(UseMagic());

                yield return new WaitForSeconds(2f);
                _staffAnimation.enabled = false;
                yield return new WaitForSeconds(2f);
            }

            if (_doors.Count > index && _doors[index] != null) Destroy(_doors[index]);
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

        private void OnEnable()
        {
            GoToPortal.OnOpenedDoor += DestroyDoor;
            GoToPortal.OnOpenedPortal += OpenPortal;
        }

        private void OnDisable()
        {
            GoToPortal.OnOpenedDoor -= DestroyDoor;
            GoToPortal.OnOpenedPortal -= OpenPortal;
        }
    }
}