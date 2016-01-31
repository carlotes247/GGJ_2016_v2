using UnityEngine;
using System.Collections;

public class PuzzleManager : MonoBehaviour {

	public bool alarmPuzzle;
	public bool greenBookPuzzle;
	public bool blueBookPuzzle;
	public bool brownBookPuzzle;

	AudioSource audioSource;
	public AudioClip achievementSFX;

	public bool SFX;

    /// <summary>
    /// Flag controlling if the player won already
    /// </summary>
    public bool PlayerWon;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {

		if (SFX == true) {
			AchievSFX ();
			SFX = false;
		}

		// ADD ALL THE OTHER PUZZLE BOOLS HERE, WHEN THEY ALL ARE TRUE HE WINS
		if (alarmPuzzle == true && greenBookPuzzle == true && blueBookPuzzle == true && !PlayerWon) {
            PlayerWon = true;
            Debug.Log ("WIN");
            // Fade In to WHITE the screen
            Toolbox.Instance.GameManager.gameLogicController.DayCountAnim.Play("Fade_In_White");
            // We call the animation from the script of Mario
            Toolbox.Instance.GameManager.gameLogicController.PlayerWinAnimScript.StartTheAnimation();
            // We go back to the main Menu
			//Toolbox.Instance.GameManager.gameLogicController.GoToMainMenu ();
		}
	}

	void AchievSFX() {
		audioSource.PlayOneShot (achievementSFX, 0.75f);
	}
}
