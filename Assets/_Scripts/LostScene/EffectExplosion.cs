using UnityEngine;

namespace _Scripts.LostScene
{
    public class EffectExplosion : MonoBehaviour
    {
        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}