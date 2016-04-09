﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(CardStack))]
public class CardStackView : MonoBehaviour 
{
	CardStack deck;
	Dictionary<int, GameObject> fetchedCards;
	int lastCount;

	public Vector3 start;
	public float cardOffset;
	public GameObject cardPrefab;
	public bool faceUp = false;
	public bool reverseLayerOrder = false;

	void Start () {
		fetchedCards = new Dictionary<int, GameObject> ();
		deck = GetComponent<CardStack> ();
		ShowCards ();
		lastCount = deck.cardCount;

		deck.CardRemoved += deck_CardRemoved;
	}

	void deck_CardRemoved (object sender, CardRemovedEventArgs e) {
		if (fetchedCards.ContainsKey (e.CardIndex)) {
			Destroy (fetchedCards [e.CardIndex]);
			fetchedCards.Remove (e.CardIndex);
		}
	}

	void Update () {
		if (lastCount != deck.cardCount) {
			lastCount = deck.cardCount;
			ShowCards ();
		}
		deck.CardRemoved += deck_CardRemoved;
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

		fetchedCards.Add (cardIndex, cardCopy);

		Debug.Log ("Hand Value = " + deck.HandValue ());
	}
}
