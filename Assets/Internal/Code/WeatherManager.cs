using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WeatherManager : MonoBehaviour {

	public EWeather actualWeather = EWeather.SOLEIL;
	public Dictionary<EWeather, int> consecutiveWeather = new Dictionary<EWeather, int>();

	private config CFG;
	private List<EWeather> probabilitySeason = new List<EWeather>();
	// Use this for initialization
	void Start () {
		CFG = config.CFG;
		consecutiveWeather.Add(EWeather.SOLEIL, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnEnable()
	{
		TurnEvent.EventHandler += OnTurnEvent;
	}
	void OnDisable()
	{
		TurnEvent.EventHandler -= OnTurnEvent;
	}
	void OnTurnEvent(TurnEvent _event)
	{
		switch(_event.type)
		{
			case TurnEvent.Type.NewSeason:
				probabilitySeason.Clear();
				int season = (int)TimeManager.Instance.season;
				for(int i = 0; i < 4; i++)
					{
						probabilitySeason.AddRange(Enumerable.Repeat(CFG.seasons[season].weather[i].weather, CFG.seasons[season].weather[i].probability));
					}
			break;
			
			case TurnEvent.Type.NewDay:
				//Nouvelle météo basée sur la saison
				actualWeather = probabilitySeason.ElementAt(Random.Range(0,probabilitySeason.Count));				
				
				//Si la météo est la même qu'hier
				if(consecutiveWeather.ElementAt(0).Key == actualWeather)
					consecutiveWeather[actualWeather] = consecutiveWeather.ElementAt(0).Value+1;
				else
				{
					consecutiveWeather.Clear();
					consecutiveWeather.Add(actualWeather, 0);				
				}
					
				if(consecutiveWeather.ElementAt(0).Value >= CFG.disastersBeginAt)
					OnDisaster(actualWeather);

			break;
		}
	}
	
	void OnDisaster(EWeather _weather)
	{
		switch(_weather)
		{
			case EWeather.SOLEIL:
				if(TimeManager.Instance.season == ESeasons.ETE)
					Debug.Log("C'est la canicule ! Tout le monde s'arrête boire un verre à l'auberge !");
			break;
			
			case EWeather.PLUIE:
				Debug.Log("La pluie et la boue rendent le voyage pénible et fatiguant, beaucoup de voyageurs s'arrêtent dans votre auberge.");
			break;
			
			case EWeather.NEIGE:
				Debug.Log("La route est impraticable, plus personne ne passe, les voyageurs présents dorment sur place.");
			break;
			
			case EWeather.TEMPETE:
				Debug.Log("Le pont est impraticable et subit des dégâts à cause des troncs flottant dans le fleuve ...");
			break;
		}
	}
}
