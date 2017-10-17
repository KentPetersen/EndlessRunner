using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public ScoreManager theScoreManager;

	public Text levelText;
	public int levelCount;
	public int levelIncreaseMilestone;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//level up every X points
		if (theScoreManager.scoreCount > levelIncreaseMilestone * levelCount) {
			levelCount++;
		}

		levelText.text = "Level: " + levelCount;
		
	}
}
