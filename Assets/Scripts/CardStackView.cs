using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(CardStack))]
public class CardStackView : MonoBehaviour 
{
	CardStack deck;
	Dictionary<int, CardView> fetchedCards;
	int lastCount;

	public Vector3 start;
	public float cardOffset;
	public GameObject cardPrefab;
	public bool faceUp = false;
	public bool reverseLayerOrder = false;


	public void Toggle (int card, bool isFaceUp) {
		fetchedCards [card].isFaceUp = isFaceUp;
		ShowCards ();
	}

	void Awake () {
		fetchedCards = new Dictionary<int, CardView> ();
		deck = GetComponent<CardStack> ();
		ShowCards ();
		lastCount = deck.cardCount;

		deck.CardRemoved += deck_CardRemoved;
		deck.CardAdded += Deck_CardAdded;
	}

	void Deck_CardAdded (object sender, CardEventArgs e)
	{
		
		float co = cardOffset * deck.cardCount;

		Vector3 temp = start + new Vector3 (co, 0f);
		AddCard (temp, e.CardIndex, deck.cardCount);
	}

	void deck_CardRemoved (object sender, CardEventArgs e) {
		if (fetchedCards.ContainsKey (e.CardIndex)) {
			Destroy (fetchedCards [e.CardIndex].Card);
			fetchedCards.Remove (e.CardIndex);
		}
	}

	void Update () {
		if (lastCount != deck.cardCount) {
			lastCount = deck.cardCount;
			ShowCards ();
		}
	}

	void ShowCards () {
		int cardCount = 0;
		if (deck.HasCards) {
			
			foreach (int i in deck.GetCards()) {
				float co = cardOffset * cardCount;

				Vector3 temp = start + new Vector3 (co, 0f);
				AddCard (temp, i, cardCount);
				cardCount++;
			}
		}
	}

	void AddCard (Vector3 position, int cardIndex, int positionalIndex) {

		if (fetchedCards.ContainsKey (cardIndex)) {
			if (!faceUp) {
				CardModel model = fetchedCards [cardIndex].Card.GetComponent<CardModel> ();
				model.ToggleFace(fetchedCards[cardIndex].isFaceUp);
			}

			return;
		}

		GameObject cardCopy = (GameObject)Instantiate (cardPrefab);
		cardCopy.transform.position = position;

		CardModel cardModel = cardCopy.GetComponent<CardModel> ();
		cardModel.cardIndex = cardIndex;
		cardModel.ToggleFace (faceUp);

		SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer> ();
		if (reverseLayerOrder) {
			spriteRenderer.sortingOrder = 51 - positionalIndex;
		} else {
			spriteRenderer.sortingOrder = positionalIndex;
		}

		fetchedCards.Add (cardIndex, new CardView(cardCopy));
	}
}
