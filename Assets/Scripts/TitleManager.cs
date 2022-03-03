using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void Quit()
	{
		Application.Quit();
	}
	public void PlayLocal()
	{
		SceneManager.LoadScene("Connect 4");
	}
}
