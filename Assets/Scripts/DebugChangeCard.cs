using UnityEngine;
using System.Collections;

public class DebugChangeCard : MonoBehaviour {

	CardFlipper flipper;
	CardModel cardModel;
	int cardIndex = 0;

	public GameObject card;


	void Start() 
	{
		cardModel = card.GetComponent<CardModel> ();
		flipper = card.GetComponent<CardFlipper> ();
	}

	void OnGUI()
	{
		if (GUI.Button (new Rect (10, 10, 100, 28), "Hit Me!")) {

			if (cardIndex >= cardModel.faces.Length) {
				cardIndex = 0;
				flipper.flipCard (cardModel.faces [cardModel.faces.Length - 1], cardModel.cardBack, -1);
			}
			else 
			{
				if (cardIndex > 0) {
					flipper.flipCard (cardModel.faces [cardIndex - 1], cardModel.faces [cardIndex], cardIndex);
				}
				else 
				{
					flipper.flipCard (cardModel.cardBack, cardModel.faces [cardIndex], cardIndex);
				}
				cardIndex++;
			}
		}
	}

}
