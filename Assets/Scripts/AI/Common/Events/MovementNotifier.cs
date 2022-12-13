using System;

namespace AI.Common.Events
{
    public class MovementNotifier
    {
        public event EventHandler NeedToComeCloser;

        public void SubscribeToSource(EventHandler needToComeCloser)
        {
            needToComeCloser += OnNeedToComeCloser;
        }

        private void OnNeedToComeCloser(object sender, EventArgs args)
        {
            NeedToComeCloser?.Invoke(sender, args);
        }
    }
}