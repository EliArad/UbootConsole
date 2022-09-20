using System;
using System.Timers;

namespace GSkinLib
{
    public class ReleaseTimer 
    {
        private System.Timers.Timer aTimer;

        IButton pButton;
        BUTTON_NAME m_buttonName;
        int m_timeInMili;
        public ReleaseTimer(IButton pb, BUTTON_NAME buttonName, int timeInMili)
        {
            m_buttonName = buttonName;
            pButton = pb;
            m_timeInMili = timeInMili;
        }

        public  void StartTimer()
        {          
            if (aTimer == null)
            {
                aTimer = new System.Timers.Timer(m_timeInMili);
                aTimer.Elapsed += OnTimedEvent;
                aTimer.AutoReset = false;
                aTimer.Enabled = true;
            }
            else
            {
                aTimer.Start();
            }
        }

        public void ResetTimer()
        {

            if (aTimer != null)
            {

                aTimer.Stop();
                aTimer.Start();
            }
            else
            {
                StartTimer();
            }          
             
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            pButton.NotifyDown(m_buttonName);
        }

        public void StopTimer()
        {
            if (aTimer != null)
                aTimer.Stop();            
            //aTimer.Dispose();
        }

        
    }
}