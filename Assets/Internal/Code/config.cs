using UnityEngine;
using System.Collections;

public class config : MonoBehaviour {

	public static config CFG;
	
	[Header("Time Settings")]
	public float dayDuration;
	public int daysInAWeek;
	public int weeksInAMonth;
	public int monthsInAYear;
	
	[Space(10)]
	[Header("Seasons and weather settings")]
	public Season[] seasons;
	public int disastersBeginAt;
	
	void Awake()
     {
         if(CFG != null)
             GameObject.Destroy(CFG);
         else
             CFG = this;
         
         DontDestroyOnLoad(this);
     }
}
