using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public SceneManager theSceneManager;

	public string playGameLevel;

	public void PlayGame()
	{
		//Application.LoadLevel (playGameLevel);
		SceneManager.LoadSceneAsync(playGameLevel);
	}

	public void QuitGame()
	{
		Application.Quit ();
	}

}
