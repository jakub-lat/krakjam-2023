using UnityEngine;

namespace Utils
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public static T Current;

        protected virtual void Awake()
        {
            if (Current != null)
            {
                Debug.LogError($"An instance of {typeof(T).Name} already exists!");
                Destroy(gameObject);
                return;
            }

            Current = (T)this;
        }
    }
}
