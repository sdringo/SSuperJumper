
using UnityEngine;

public class Request
{
    public string methodName = null;
    public object parameter = null;

    public GameObject callbackObject = null;
    public string callbackMethod = null;

    public static Request create( string method )
    {
        Request request = new Request();
        request.methodName = method;

        return request;
    }

    public static Request create(string method, object param)
    {
        Request request = new Request();
        request.methodName = method;
        request.parameter = param;

        return request;
    }

	public static Request create( string method, GameObject callbackObject, string callbackMethod )
	{
		Request request = new Request();
		request.methodName = method;
		request.callbackObject = callbackObject;
		request.callbackMethod = callbackMethod;
		
		return request;
	}
	
	public static Request create(string method, object param, GameObject callbackObject, string callbackMethod)
	{
		Request request = new Request();
		request.methodName = method;
		request.parameter = param;
		request.callbackObject = callbackObject;
		request.callbackMethod = callbackMethod;
		
		return request;
	}
}