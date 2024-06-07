using UnityEngine;

namespace _Scripts.Items
{
    public class FramesActivator
    {
        private static GameObject _frame;

        public FramesActivator(GameObject frame)
        {
            ShowOrFadeFrame(false);
            _frame = frame;
            ShowOrFadeFrame(true);
        }

        public void ShowOrFadeFrame(bool isShow)
        {
            if(_frame != null)
                _frame.SetActive(isShow);
        }
    }
}