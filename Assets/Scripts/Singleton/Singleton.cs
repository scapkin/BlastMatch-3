using UnityEngine;

namespace Singleton
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static bool _mShuttingDown = false;
        private static readonly object _mLock = new object();
        private static T _mInstance;

        /// <summary>
        /// Access singleton instance through this propriety.
        /// </summary>
        public static T Instance
        {
            get
            {
                lock (_mLock)
                {
                    if (_mInstance == null || _mShuttingDown)
                    {
                        // Search for existing instance.
                        _mInstance = (T)FindObjectOfType(typeof(T));

                        // Create new instance if one doesn't already exist.
                        if (_mInstance == null)
                        {
                            // Need to create a new GameObject to attach the singleton to.
                            var singletonObject = new GameObject();
                            _mInstance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T).ToString() + " (Singleton)";
                            _mShuttingDown = false;
                            // Make instance persistent.
                        }
                    }

                    return _mInstance;
                }
            }
        }


        private void OnApplicationQuit()
        {
            _mShuttingDown = true;
        }

        private void OnDestroy()
        {
            _mShuttingDown = true;
        }
    }
}