using System.Collections.Generic;
using Tool.Analytics.UnityAnalytics;
using UnityEngine;
using UnityEngine.Analytics;

namespace Tool.Analytics
{
    public class AnalyticsManager : MonoBehaviour
    {
        private IAnalyticsService[] _services;
        private void Awake()
        {
           _services = new IAnalyticsService[]
            {
                new UnityAnalyticsService(),
                //new Dev2DevAnalyticsService(),
            };
        }
        public void SendMainMenuOpenEvent() => 
            SentEvent("MainMenuOpened");
        
        public void SendGameStartedEvent() => 
            SentEvent("GameStarted");
        
        public void SentEvent(string eventName)
        {
            foreach (IAnalyticsService service in _services) 
                service.SendEvent(eventName);
        }
        
        public void SentEvent(string eventName, Dictionary<string, object> eventData)
        {
            foreach (IAnalyticsService service in _services) 
                service.SendEvent(eventName, eventData);
        }
    }
}