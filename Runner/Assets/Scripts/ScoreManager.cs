using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText;
	public Text highScoreText;
	public Text currencyText;

	public float scoreCount;
	public float highScoreCount;
	public int currencyCount;

	public float pointsPerSecond;

	public bool scoreIncreasing;

	public bool shouldDouble;


	// Use this for initialization
	void Start () {

		if(PlayerPrefs.HasKey("HighScore"))
		{
			highScoreCount = PlayerPrefs.GetFloat("HighScore");
		}

		if(PlayerPrefs.HasKey("CurrencyCount"))
		{
			currencyCount = PlayerPrefs.GetInt("CurrencyCount");
			currencyText.text = "Currency: " + currencyCount;
		}



	}
	
	// Update is called once per frame
	void Update () {

		if (scoreIncreasing) {
			scoreCount += pointsPerSecond * Time.deltaTime;
		}

		if (scoreCount > highScoreCount) {
			highScoreCount = scoreCount;
			PlayerPrefs.SetFloat ("HighScore", highScoreCount);
		}

		scoreText.text = "Score: " + Mathf.Round(scoreCount);
		highScoreText.text = "High Score: " + Mathf.Round(highScoreCount);
		currencyText.text = "Currency: " + currencyCount;

	}

	public void AddScore(int pointsToAdd)
	{

		if (shouldDouble) {
			pointsToAdd = pointsToAdd * 2;
		}	

		scoreCount += pointsToAdd;

	}

	public void AddCurrency()
	{
		currencyCount += Mathf.RoundToInt (scoreCount);
		PlayerPrefs.SetInt ("CurrencyCount", currencyCount);
	}
}
