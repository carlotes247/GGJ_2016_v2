using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerWinTheGame : MonoBehaviour {


	public GameObject panel;
	GameObject lastAnimClip;
	Color color;
	float timer4Animation; 
	bool animationPlay = true;
	bool fadeOut;
	Animator anim;
	// Use this for initialization
	void Start () {
		
		anim = gameObject.GetComponent<Animator>();
//		foreach (Transform child in transform){
//			if (child.name == "HoldingTheCanvasAndCamera"){
//				lastAnimClip = child.gameObject;
//			}
//		}
	}
	public void StartTheAnimation(){
        Toolbox.Instance.GameManager.Player.gameObject.SetActive(false);
        GetComponent<Camera>().depth = 100;
        anim.SetTrigger("StartEnding");
		color =	panel.GetComponent<Graphic>().material.color;
		color = Color.white;
		color.a = 1;
		panel.GetComponent<Graphic>().material.color = color;
		animationPlay = true;
	}

	public void TheAnimationStops(){
	
		animationPlay = false;
		color = Color.black;
		color.a = 0;
		panel.GetComponent<Graphic>().material.color = color;
		fadeOut = true;
        Toolbox.Instance.GameManager.Player.gameObject.SetActive(true);
        Toolbox.Instance.GameManager.gameLogicController.GoToMainMenu();


	}

	// Update is called once per frame
	void FixedUpdate () {

		if (!fadeOut){
			
			if (color.a > 0.9f){
				color.a -= Time.deltaTime/20;
				panel.GetComponent<Graphic>().material.color = color;

			}
			else if (color.a > 0f){
				color.a -= Time.deltaTime/4;
				panel.GetComponent<Graphic>().material.color = color;
			}
		}
		else if (fadeOut){
			if (color.a < 1f){
				color.a += Time.deltaTime/4;
				panel.GetComponent<Graphic>().material.color = color;

			}
//			else if (color.a < 1f){
//				color.a += Time.deltaTime/4;
//				panel.GetComponent<Graphic>().material.color = color;
//			}
		}


	}
}

