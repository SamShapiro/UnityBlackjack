using UnityEngine;
using System.Collections;


[RequireComponent(typeof(CardStack))]
public class CardStackView : MonoBehaviour 
{
	CardStack deck;

	public Vector3 start;
	public float cardOffset;
	public GameObject cardPrefab;

	void Start () {
		deck = GetComponent<CardStack> ();
		ShowCards ();
	}

	void ShowCards () {
		int cardCount = 0;

		foreach (int i in deck.GetCards()) {
			float co = cardOffset * cardCount;

			GameObject cardCopy = (GameObject)Instantiate (cardPrefab);
			Vector3 temp = start + new Vector3 (co, 0f);
			cardCopy.transform.position = temp;

			CardModel cardModel = cardCopy.GetComponent<CardModel> ();
			cardModel.cardIndex = i;
			cardModel.ToggleFace (true);

			SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer> ();
			spriteRenderer.sortingOrder = cardCount;

			cardCount++;
		}
	}
}
