using UnityEngine;

namespace Channel3.Utils
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject(typeof(T).Name);
                        instance = obj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        protected virtual void OnEnable()
        {
            if (instance == null)
            {
                instance = this as T;
                instance.transform.SetParent(null);
            }
            else if (instance != this as T)
            {
                Destroy(gameObject);
            }

            if(instance.transform.parent == null)
                DontDestroyOnLoad(instance);
        }
    }
}