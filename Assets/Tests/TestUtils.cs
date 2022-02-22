using System.Reflection;
using UnityEngine;

public class ScriptInstantiator
{
	public T InstantiateScript<T>() where T : MonoBehaviour
	{
		GameObject gameObject = new GameObject();

		gameObject.name = typeof(T).Name + " (Test)";
		T instance = gameObject.AddComponent<T>();

		MethodInfo startMethod = typeof(T).GetMethod("Start");

		if (startMethod != null)
		{
			startMethod.Invoke(instance, null);
		}

		return instance;
	}
}
