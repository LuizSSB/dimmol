using UnityEngine;
using System.Collections;
using System;

public static class ChangeManager {
	public static event EventHandler<MethodParamEventArgs> MethodInvoked;
	public static void DispatchMethodEvent(Type classType, string methodName, object param)
	{
		if (MethodInvoked != null) {
			MethodInvoked (null, new MethodParamEventArgs (
				classType.FullName,
				methodName,
				param
			));
		}
	}
	public static void DispatchMethodEvent(this object obj, string methodName, object param)
	{
		DispatchMethodEvent(obj.GetType(), methodName, param);
	}

	public static event EventHandler<PropertyEventArgs> PropertyChanged;
	public static void DispatchPropertyEvent(Type classType, string propertyName, object oldValue, object newValue)
	{
		if (PropertyChanged != null) {
			PropertyChanged (null, new PropertyEventArgs (
				classType.FullName,
				propertyName,
				newValue
			));
		}
	}
	public static TData ProcessPropertyChanged<TData>(Type classType, string propName, TData oldValue, TData newValue)
	{
		if (!oldValue.Equals (newValue)) {
			DispatchPropertyEvent (classType, propName, oldValue, newValue);
		}
		return newValue;
	}
	public static TData ProcessPropertyChanged<TData>(this object obj, string propName, TData oldValue, TData newValue)
	{
		return ProcessPropertyChanged(obj.GetType(), propName, oldValue, newValue);
	}
}

public class TypeEventArgs : EventArgs
{
	public string TypeName;

	public TypeEventArgs (string typeName)
	{
		this.TypeName = typeName;
	}
}

public class PropertyEventArgs : TypeEventArgs
{
	public string PropertyName;
	public object NewValue;

	public PropertyEventArgs (string typeName, string propertyName, object newValue) : base (typeName)
	{
		this.PropertyName = propertyName;
		this.NewValue = newValue;
	}
	
}

public class MethodParamEventArgs : TypeEventArgs
{
	public string MethodName;
	public object Param;

	public MethodParamEventArgs (string typeName, string methodName, object param) : base (typeName)
	{
		this.MethodName = methodName;
		this.Param = param;
	}
}