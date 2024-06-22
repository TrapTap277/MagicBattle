using UnityEngine;

namespace _Scripts.LostScene
{
    public class DissolveDoor : MonoBehaviour
    {
        private readonly int _dissolvedState = Animator.StringToHash("Dissolve");

        public void Dissolve()
        {
            gameObject.GetComponent<Animator>().CrossFade(_dissolvedState, 0);

            Destroy();
        }

        private void Destroy()
        {
            Destroy(gameObject, 1f);
        }
    }
}