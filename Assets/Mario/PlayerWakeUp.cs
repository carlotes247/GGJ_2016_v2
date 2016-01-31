using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerWakeUp : MonoBehaviour {

	 public GameObject panel;
	GameObject firstAnimWakeUp;
	Color color;
	float timer4Animation;
    public bool animationPlay;
	// Use this for initialization
	void Start () {
	
		foreach (Transform child in transform){

			if (child.name == "Object4StartAnim"){
				firstAnimWakeUp = child.gameObject;
			}
		}
	}
	public void StartTheAnimation(){

		firstAnimWakeUp.SetActive(true);
		color =	panel.GetComponent<Graphic>().material.color;
		color.a = 1;
		panel.GetComponent<Graphic>().material.color = color;
        animationPlay = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

		if(firstAnimWakeUp.activeSelf){
			timer4Animation += Time.deltaTime;
		}

		if (timer4Animation > 19.5f){
            // If the animation is playing
            if (animationPlay)
            {
                animationPlay = false;
                // We reset the timer
                timer4Animation = 0f;
                // We activate the player again before giving back control to it
                Toolbox.Instance.GameManager.Player.gameObject.SetActive(true);
                // We set again the position to avoid unexpected behaviour
                Toolbox.Instance.GameManager.gameLogicController.SetPlayerInitialGamePos();
                firstAnimWakeUp.SetActive(false); 
            }
		}

		if (color.a > 0.5f){
			color.a -= Time.deltaTime/16;
			panel.GetComponent<Graphic>().material.color = color;

		}
		else if (color.a > 0f){
			color.a -= Time.deltaTime/4;
			panel.GetComponent<Graphic>().material.color = color;
		}


	}
}
