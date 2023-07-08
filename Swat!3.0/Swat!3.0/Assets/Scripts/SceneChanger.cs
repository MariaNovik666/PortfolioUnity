using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneChanger : MonoBehaviour
{
	public void ChangeScene(int _sceneNumber) {
		SceneManager.LoadScene(_sceneNumber); 
	}
	public void Exit() {
		Application.Quit();
		UnityEditor.EditorApplication.isPlaying = false;
	}
	public void PAUSE()
    {
		Time.timeScale = 0f;
    }
	public void Cont()
	{
		Time.timeScale = 1f;
	}
}
