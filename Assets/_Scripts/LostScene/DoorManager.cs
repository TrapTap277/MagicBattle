using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.LostScene
{
    public class DoorManager : MonoBehaviour, IDoorManager
    {
        [SerializeField] private List<GameObject> _doors = new List<GameObject>();

        public void DestroyDoor()
        {
            Destroy(_doors[0]);
            _doors.RemoveAt(0);
        }
    }
}