using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Classes d'Events

// Chacune doit avoir:
// - un EventHandler static pour ajouter/supprimer les events
// - une fonction Send pour envoyer les events


public class TurnEvent
{
	public delegate void TurnEventHandler(TurnEvent _event);
	static public event TurnEventHandler EventHandler;
	static public void Send(TurnEvent _event) { EventHandler.Invoke(_event); }

	//--------------------------------------------------------------------------------------------------

	public enum Type
	{
		NewDay,				
		NewWeek,				
		NewMonth,
		NewYear,
		NewSeason,
	}

	//--------------------------------------------------------------------------------------------------
	
	public string EventID() { return "TurnEvent"; }
	
	public Type type { get { return this.mType; } }


	private Type mType;



	public TurnEvent(Type _type)
	{
		this.mType = _type;
	}
}
