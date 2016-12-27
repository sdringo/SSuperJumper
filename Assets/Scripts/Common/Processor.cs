
using UnityEngine;
using System.Collections.Generic;

public class Processor<T> : SingletonObject<T> where T : class
{
    private readonly int DEFAULT_QUEUE_COUNT = 1000;

    private Queue<Request> _waitQueue;
    private Queue<Request> _processQueue;

    #region ILifeCycle Function
    public override void initialize()
    {
        _waitQueue = new Queue<Request>( DEFAULT_QUEUE_COUNT );
        _processQueue = new Queue<Request>( DEFAULT_QUEUE_COUNT );
    }

    public override void release()
    {
        if( null != _waitQueue )
            _waitQueue.Clear();
        if( null != _processQueue )
            _processQueue.Clear();
    }

    public override void updateFixed()
    {
        prepareProcess();
    }

    public override void update()
    {

    }

    public override void updateLate()
    {
        processRequest();
    }

    public override void onEnter()
    {
    }

    public override void onExit()
    {
    }
    #endregion

    #region Process Request
    public void request( Request request )
    {
        _waitQueue.Enqueue( request );
    }

    private void prepareProcess()
    {
        if( 0 == _waitQueue.Count )
            return;

        Request request = _waitQueue.Dequeue();
        while( null != request ) {
            _processQueue.Enqueue( request );

            if( 0 == _waitQueue.Count )
                return;

            request = _waitQueue.Dequeue();
        }
    }

    private void processRequest()
    {
        if( 0 == _processQueue.Count )
            return;

        Request request = _processQueue.Dequeue();
        while( null != request ) {
            dispatch( request );

            if( 0 == _processQueue.Count )
                return;

            request = _processQueue.Dequeue();
        }
    }

    private bool dispatch( Request request )
    {
        SendMessage( request.methodName, request.parameter, SendMessageOptions.DontRequireReceiver );

        return true;
    }
    #endregion
}