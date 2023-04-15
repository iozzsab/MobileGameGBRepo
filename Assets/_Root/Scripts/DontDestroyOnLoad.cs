using UnityEngine;

namespace _Root.Scripts
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            if (enabled)
                DontDestroyOnLoad(gameObject);
        }
        
    }
}