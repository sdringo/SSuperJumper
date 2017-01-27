﻿using UnityEngine;

public class Entity : MonoBehaviour, IEntity
{
    protected Bounds bounds;

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
    #endregion

    public void attach( GameObject child )
    {
        if( null == child )
            return;

        child.transform.parent = gameObject.transform;
    }

    public void detach( GameObject go )
    {
        Transform child = transform.FindChild( go.name );
        if( child )
            child.parent = null;
    }

    public void detach( string name )
    {
        Transform child = transform.FindChild( name );
        if( child )
            child.parent = null;
    }
}