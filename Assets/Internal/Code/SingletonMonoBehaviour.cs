using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
	static protected T _instance = null;
	
	static public T Instance
	{
		get
		{
			Initialize();
			return _instance;
		}
	}
	
	//--------------------------------------------------------------------------------------------------
	// PUBLIC PROPERTIES
	//--------------------------------------------------------------------------------------------------
	
	//Public au cas où l'on doit initialiser la classe manuellement
	//(par ex si le singleton doit impérativement etre initialisé au démarrage de l'appli)
	static public void Initialize()
	{
		if(_instance == null)
		{
			_instance = Object.FindObjectOfType(typeof(T)) as T;
			
			if(_instance == null)
			{
				GameObject gameObject = new GameObject(typeof(T).Name);
				_instance = gameObject.AddComponent<T>();
			}
		}
		
		if(_instance != null && _instance.persistentBetweenScenes)
		{
			_instance.transform.parent = null;
			DontDestroyOnLoad(_instance.gameObject);
		}
	}
	
	//--------------------------------------------------------------------------------------------------
	// PRIVATE PROPERTIES
	//--------------------------------------------------------------------------------------------------
	
	//Si true, le GameObject créé pour le singleton restera actif entre les Scenes
	protected virtual bool persistentBetweenScenes
	{
		get { return false; }
	}
	
	//======================================================================================================================================================
	
	//--------------------------------------------------------------------------------------------------
	// UNITY EVENTS
	//--------------------------------------------------------------------------------------------------
	
	protected virtual void Awake()
	{
		if(_instance != null && _instance != this)
		{
			Debug.LogWarning("Tried to create a new instance of a singleton!", this);
			Object.Destroy(this.gameObject);
		}
		else
		{
			Initialize();
		}
	}
	
	protected virtual void OnDestroy()
	{
		if(_instance == this)
		{
			_instance = null;
		}
	}
	
	//--------------------------------------------------------------------------------------------------
	// PUBLIC
	//--------------------------------------------------------------------------------------------------
	
	
	
	//--------------------------------------------------------------------------------------------------
	// PRIVATE
	//--------------------------------------------------------------------------------------------------
	
	
	
}
