using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.LostScene
{
    public class DoorManager : MonoBehaviour, IDoorManager
    {
        [SerializeField] private List<GameObject> _doors = new List<GameObject>();

        private readonly int _dissolvedState = Animator.StringToHash("Dissolve");
        
        public void PlayDissolvedAnimation()  
        {
            _doors[0].GetComponent<Animator>().CrossFade(_dissolvedState, 0);
            
            Destroy();
        }

        private void Destroy()
        {
            Destroy(_doors[0], 1f);
            _doors.RemoveAt(0);
        }
    }
}