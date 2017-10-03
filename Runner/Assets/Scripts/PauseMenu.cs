using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public SceneManager theSceneManager;

	public string mainMenuLevel;

	public GameObject pauseMenu;

	public PlayerController thePlayer;

	public void PauseGame()
	{
		Time.timeScale = 0f;
		pauseMenu.SetActive (true);
	}

	public void Resume()
	{
		Time.timeScale = 1f;
		pauseMenu.SetActive (false);
	}


	public void RestartGame()
	{
		Time.timeScale = 1f;
		pauseMenu.SetActive (false);
		FindObjectOfType<GameManager> ().Reset ();
	}

	public void QuitToMain()
	{
		Time.timeScale = 1f;
		pauseMenu.SetActive (false);
		//Application.LoadLevel(mainMenuLevel);  //obsolete
		SceneManager.LoadSceneAsync(mainMenuLevel);

	}

	public void EquipGravityInverterButton()
	{
		thePlayer = FindObjectOfType<PlayerController>();
		thePlayer.EquipGravityInverter ();
	}

	public void EquipDoubleJumpButton()
	{
		thePlayer = FindObjectOfType<PlayerController>();
		thePlayer.EquipDoubleJump ();
	}
}
