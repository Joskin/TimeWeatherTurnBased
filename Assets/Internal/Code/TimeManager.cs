using UnityEngine;
using System.Collections;

public class TimeManager : SingletonMonoBehaviour<TimeManager> {

	private float dayTime = 0;
	
	
	public int daysCounter = 0;
	public int WeeksCounter = 0;
	public int MonthsCounter = 0;
	public int YearsCounter = 0;
	
	public EDays day;
	public EMonths month;
	public ESeasons season;
	
	//Debug
	public GameObject InterfaceDebug;
	
	//Config
	public config CFG;
	
	void Start () {
		
		CFG = config.CFG;
		
		//On random le mois et le jour de départ
		day = (EDays)Random.Range(0, CFG.daysInAWeek);
		month = (EMonths)Random.Range(0, CFG.monthsInAYear);
		
		int actualMonth = (int)month / 3; //Saison de 3 mois
		season = (ESeasons) actualMonth;
	}
	
	void Update () {
		
		dayTime += Time.deltaTime;
		
		if (dayTime >= CFG.dayDuration)
		{
			dayTime = 0;
			TurnEvent.Send(new TurnEvent(TurnEvent.Type.NewSeason));
			TurnEvent.Send(new TurnEvent(TurnEvent.Type.NewDay));
		}
	}
	///
	/// EVENTS
	///
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
			case TurnEvent.Type.NewDay:
				daysCounter++;
				if((int)day == CFG.daysInAWeek-1) day = 0; else day++;
				if(daysCounter%CFG.daysInAWeek == 0)
					TurnEvent.Send(new TurnEvent(TurnEvent.Type.NewWeek));
			break;
			
			case TurnEvent.Type.NewWeek:
				WeeksCounter++;
				if(WeeksCounter%CFG.weeksInAMonth == 0)
					TurnEvent.Send(new TurnEvent(TurnEvent.Type.NewMonth));
			break;
			
			case TurnEvent.Type.NewMonth:
				MonthsCounter++;
				if((int)month == CFG.monthsInAYear-1) month = 0; else month++;
				
				//Check nouvelle saison
				ESeasons prevSeason = season;
				int actualMonth = (int)month / 3; //Saison de 3 mois
				season = (ESeasons) actualMonth;
				if(prevSeason != season)
					TurnEvent.Send(new TurnEvent(TurnEvent.Type.NewSeason));
				
				if(MonthsCounter%CFG.monthsInAYear == 0)
					TurnEvent.Send(new TurnEvent(TurnEvent.Type.NewYear));
			break;
			
			case TurnEvent.Type.NewYear:
				YearsCounter++;
			break;
		}
	}
	
}
