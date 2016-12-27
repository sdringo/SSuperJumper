using UnityEngine;
using System.Collections;

public class Singleton<T> : Entity where T : class
{
    protected static T _instance = null;
    public static T instance
    {
        get
        {
            if( _instance == null )
                _instance = SingletonManager.gameObject.AddComponent( typeof( T ) ) as T;
            return _instance;
        }
    }

    public static void Instantiate()
    {
        _instance = instance;
    }

    public Singleton()
    {
        _instance = this as T;
    }

    public void ExecuteAfterCoroutine( IEnumerator coroutine, System.Action action )
    {
        StartCoroutine( ExecuteAfterCoroutineActual( coroutine, action ) );
    }

    public IEnumerator ExecuteAfterCoroutineActual( IEnumerator coroutine, System.Action action )
    {
        yield return StartCoroutine( coroutine );
        action();
    }
}

public class SingletonManager
{
    private static GameObject _gameObject = null;
    public static GameObject gameObject
    {
        get
        {
            if( _gameObject == null ) {
                _gameObject = GameObject.Find( "-SingletonManager" );
                if( null == _gameObject ) {
                    _gameObject = new GameObject( "-SingletonManager" );
                    Object.DontDestroyOnLoad( _gameObject );
                }
            }
            return _gameObject;
        }
    }
}

public class SingletonObject<T> : Entity where T : class
{
    protected static T _instance = null;
    public static T instance
    {
        get
        {
            if( _instance == null )
                _instance = instanceObject.AddComponent( typeof( T ) ) as T;
            return _instance;
        }
    }

    protected static GameObject _instanceObject = null;
    public static GameObject instanceObject
    {
        get
        {
            if( _instanceObject == null ) {
                _instanceObject = GameObject.Find( typeof( T ).Name );
                if( null == _instanceObject ) {
                    _instanceObject = new GameObject( typeof( T ).Name );
                    Object.DontDestroyOnLoad( _instanceObject );
                }
            }
            return _instanceObject;
        }
    }

    public static void Instantiate()
    {
        _instance = instance;
    }

    public SingletonObject()
    {
        _instance = this as T;
    }

    public void ExecuteAfterCoroutine( IEnumerator coroutine, System.Action action )
    {
        StartCoroutine( ExecuteAfterCoroutineActual( coroutine, action ) );
    }

    public IEnumerator ExecuteAfterCoroutineActual( IEnumerator coroutine, System.Action action )
    {
        yield return StartCoroutine( coroutine );
        action();
    }

    virtual protected void OnApplicationQuit()
    {
        Object.Destroy( _instanceObject );
        //_instance = null;
        //_instanceObject = null;
    }
}
