using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSingleton<T> : MonoBehaviour where T : TSingleton<T>
{
    private static T m_Instace;

    private static GameObject m_UniqueObject;

    protected TSingleton() { }

    static public T Instance
    {
        get
        {
            if(m_Instace == null)
            {
                m_UniqueObject = new GameObject(typeof(T).Name, typeof(T));
                m_Instace = m_UniqueObject.GetComponent<T>();
                m_Instace.FixedInitialized();
            }
            return m_Instace;
        }
    }

    private void FixedInitialized()
    {
        DefaultSetting();
        DontDestroyOnLoad(gameObject);
    }

    protected virtual void DefaultSetting()
    {

    }

}
