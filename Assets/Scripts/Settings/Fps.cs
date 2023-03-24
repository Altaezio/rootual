using UnityEngine;
using System.Collections;

public class Fps : MonoBehaviour {

    string label = "";
	float count;
	
	IEnumerator Start()
	{
		// Make the game run as fast as possible
        Application.targetFrameRate = 144;
		QualitySettings.vSyncCount = 0;

		GUI.depth = 2;
		while (true) {
			if (Time.timeScale == 1) {
				yield return new WaitForSeconds (0.1f);
				count = (1 / Time.deltaTime);
				label = "FPS : " + (Mathf.Round (count));
			} else {
				label = "Pause";
			}
			yield return new WaitForSeconds (0.5f);
		}
	}

	void OnGUI ()
	{
		GUI.Label (new Rect (10, 5, 200, 50), label);
	}
}