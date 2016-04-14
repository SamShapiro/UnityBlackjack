using UnityEngine;

public class CardView {

	public GameObject Card { get; private set; }
	public bool isFaceUp { get; set; }
	//CardFlipper flipper;


	public CardView (GameObject card) {
		Card = card;
		isFaceUp = false;
		//flipper = card.GetComponent<CardFlipper> ();
	}

}