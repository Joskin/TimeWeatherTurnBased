using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Classes d'Events

// Chacune doit avoir:
// - un EventHandler static pour ajouter/supprimer les events
// - une fonction Send pour envoyer les events

/*
public class PlayerEvent
{
	public delegate void PlayerEventHandler(PlayerEvent _event);
	static public event PlayerEventHandler EventHandler;
	static public void Send(PlayerEvent _event) { EventHandler.Invoke(_event); }
	
	//--------------------------------------------------------------------------------------------------
	
	public enum Type
	{
		Stun_Start,			// Un Joueur vient d'etre Stun
		Stun_End,			// Un Joueur vient de sortir du Stun
	}
	
	//--------------------------------------------------------------------------------------------------
	
	public Type type { get { return this.mType; } }
	public Player player { get { return this.mPlayer; } }
	
	private Type mType;
	private Player mPlayer;
	
	public PlayerEvent(Type _type, Player _player)
	{
		this.mType = _type;
		this.mPlayer = _player;
	}
}
*/
//================================================================================================================================

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
