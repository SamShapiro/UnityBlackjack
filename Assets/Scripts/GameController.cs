using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour {

	public CardStack player;
	public CardStack dealer;
	public CardStack deck;

	public Button hitButton;
	public Button standButton;
	public Button playAgainButton;
	public Text winText;

	int dealersFirstCard = -1;

	#region Public methods

	public void Hit() {
		player.Push (deck.Pop ());
		if (player.HandValue() > 21) {
			hitButton.interactable = false;
			standButton.interactable = false;
			StartCoroutine (DealersTurn ());
		}
	}

	public void Stand() {
		hitButton.interactable = false;
		standButton.interactable = false;
		StartCoroutine (DealersTurn ());
	}

	public void PlayAgain() {
		StopCoroutine (DealersTurn ());
		while (player.cardCount > 0) {
			player.Pop ();
		}
		while (dealer.cardCount > 0) {
			dealer.Pop ();
		}
		winText.text = "";
		hitButton.interactable = true;
		standButton.interactable = true;
		dealersFirstCard = -1;
		StartGame ();
	}

	#endregion


	#region Unity messages

	void Start() {
		StartGame ();
	}

	#endregion


	void StartGame() {
		playAgainButton.interactable = false;
		for (int i = 0; i < 2; i++) {
			player.Push (deck.Pop ());
			HitDealer ();
		}
	}

	void HitDealer() {
		int card = deck.Pop ();

		if (dealersFirstCard < 0) {
			dealersFirstCard = card;
		}

		dealer.Push (card);
		if (dealer.cardCount >= 2) {
			CardStackView view = dealer.GetComponent<CardStackView>();
			view.Toggle (card, true);
		}
	}

	IEnumerator DealersTurn() {
		CardStackView view = dealer.GetComponent<CardStackView> ();
		view.Flip (dealersFirstCard);
		yield return new WaitForSeconds (1f);

		while (dealer.HandValue () < 17 && player.HandValue() <= 21) {
			HitDealer ();
			yield return new WaitForSeconds (1f);
		} 
		if ((dealer.HandValue () > player.HandValue () && dealer.HandValue() <= 21) || player.HandValue() > 21) {
			winText.text = "YOU LOSE";
		} else if (player.HandValue () > dealer.HandValue () || dealer.HandValue() > 21) {
			winText.text = "YOU WIN";
		} else {
			winText.text = "IT'S A TIE";
		}
		playAgainButton.interactable = true;
	}

}
