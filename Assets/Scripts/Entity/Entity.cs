using UnityEngine;

public class Entity : MonoBehaviour, IEntity
{
    #region
    protected virtual void Awake()
    {
        initialize();
    }

    protected virtual void FixedUpdate()
    {
        updateFixed();
    }

    protected virtual void Update()
    {
        update();
    }

    protected virtual void LateUpdate()
    {
        updateLate();
    }

    protected virtual void OnEnable()
    {
        onEnter();
    }

    protected virtual void OnDisable()
    {
        onExit();
    }
    #endregion

    #region
    public virtual void initialize()
    {
        
    }

    public virtual void release()
    {
        
    }

    public virtual void updateFixed()
    {
    }

    public virtual void update()
    {
    }

    public virtual void updateLate()
    {
    }

    public virtual void onEnter()
    {
    }

    public virtual void onExit()
    {
    }
    #endregion

    public void attach( GameObject child )
    {
        if( null == child )
            return;

        child.transform.parent = gameObject.transform;
    }

    public void detach( string name )
    {
        Transform child = transform.FindChild( name );
        if( child )
            child.parent = null;
    }
}