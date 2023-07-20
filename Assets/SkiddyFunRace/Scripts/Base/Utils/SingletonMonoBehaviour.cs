using UnityEngine;

namespace GenBase.Utils
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        //private field for caching
        private static T _instance;

        private static bool _shuttingDown;

        public bool isPersistant;

        //getter for instance
        public static T Instance
        {
            get
            {
                if (_shuttingDown)
                    return null;

                return _instance;
            }
        }

        public virtual void Awake()
        {
            if (isPersistant)
            {
                if (!_instance)
                    _instance = this as T;
                else
                    Destroy(gameObject);
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                _instance = this as T;
            }
        }

        private void OnApplicationQuit()
        {
            _shuttingDown = true;
        }
    }
}