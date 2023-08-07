using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fonarik : MonoBehaviour {
	public TextMeshProUGUI text;
	public static int Energy = 50;
	public GameObject Light;
	public bool onLight;
	private float scet;
	private bool lightButtonDown = false;

	public void onLightButtonClick()
	{
		lightButtonDown = !lightButtonDown;
	}


	
	void Update () {

		text.text = Energy + "%";

		if (onLight == true) {
			scet += 1 * Time.deltaTime;
			if (scet >= 2) {
				Energy -= 1;
				scet = 0;
			}
		}
		if (Energy >= 100) {
			Energy = 100;
		}
		if (Energy <= 0) {
			onLight = false;
			Light.SetActive (false);
			Energy = 0;
		}
		if (lightButtonDown) {
			if (onLight == false && Energy >0) {
					Light.SetActive (true);
					onLight = true;
			}
			else 
			{
				Light.SetActive (false);
				onLight = false;

			}
		}
	}
}
