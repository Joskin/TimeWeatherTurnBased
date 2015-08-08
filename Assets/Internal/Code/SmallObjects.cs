using UnityEngine;
using System;

[Serializable]
public class Season {
	public ESeasons season;
	public Weather[] weather = new Weather[4];
}

[Serializable]
public class Weather {
	public EWeather weather; 
	public int probability;
}

[Serializable]
public class Disaster  {
	public EWeather weather;
}
