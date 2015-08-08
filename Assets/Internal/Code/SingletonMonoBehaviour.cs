using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Singleton qui s'auto-initialise dès qu'un script en a besoin
// Permet de ne pas avoir à gérer des GameObjects dans les Scenes
// et d'oublier d'en mettre dans les scenes de test par exemple!
//
// J'ai emprunté l'idée sur mon projet freelance, j'ai trouvé ça plutot ingénieux! :)

// Les Scripts étendu de cette classe auront le préfixe SMB_ pour qu'on sache facilement
// qu'il s'agit d'un singleton

// TODO Réfléchir à changer cette classe pour qu'elle aille chercher une Prefab pour le singleton
// Ca permettra d'ajouter des properties manuellement dans la Prefab et changer facilement des
// paramètres liés au singleton (TODO prévoir un dossier spécial avec toutes les Prefabs singleton)

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
