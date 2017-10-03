using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Transform platformGenerator;
	private Vector3 platformStartingPoint;

	public PlayerController thePlayer;
	private Vector3 playerStartPoint;

	private PlatformDestroyer[] platformList;

	private ScoreManager theScoreManager;

	public DeathMenu theDeathScreen;

	public bool powerupReset;


	// Use this for initialization
	void Start () {

		platformStartingPoint = platformGenerator.position;
		playerStartPoint = thePlayer.transform.position;

		theScoreManager = FindObjectOfType<ScoreManager>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RestartGame()
	{
		theScoreManager.scoreIncreasing = false;

		thePlayer.gameObject.SetActive(false);

		theDeathScreen.gameObject.SetActive (true);

		//StartCoroutine ("RestartGameCo");
	}

	public void Reset()
	{
		theDeathScreen.gameObject.SetActive (false);

		platformList = FindObjectsOfType<PlatformDestroyer>();
		for(int i = 0; i < platformList.Length; i++)
		{
			platformList[i].gameObject.SetActive(false);
		}

		thePlayer.transform.position = playerStartPoint;
		platformGenerator.position = platformStartingPoint;
		thePlayer.gameObject.SetActive(true);

		theScoreManager.AddCurrency (); //add currency before restting score
		theScoreManager.scoreCount = 0;
		theScoreManager.scoreIncreasing = true;
	
		powerupReset = true;
	}

	/*
	public IEnumerator RestartGameCo()
	{
		theScoreManager.scoreIncreasing = false;

		thePlayer.gameObject.SetActive(false);
		yield return new WaitForSeconds (0.5f);

		platformList = FindObjectsOfType<PlatformDestroyer>();
		for(int i = 0; i < platformList.Length; i++)
		{
			platformList[i].gameObject.SetActive(false);
		}

		thePlayer.transform.position = playerStartPoint;
		platformGenerator.position = platformStartingPoint;
		thePlayer.gameObject.SetActive(true);

		theScoreManager.scoreCount = 0;
		theScoreManager.scoreIncreasing = true;

	}
	*/
}
