using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardStack : MonoBehaviour {

	List<int> cards;

	public bool isGameDeck;

	public bool HasCards 
	{
		get { return cards != null && cards.Count > 0; }
	}

	public event CardEventHandler CardRemoved;
	public event CardEventHandler CardAdded;

	public int cardCount {
		get {
			if (cards == null)
				return 0;
			else
				return cards.Count;
		}
	}

	public IEnumerable<int> GetCards() {
		foreach (int i in cards) {
			yield return i;
		}
	}

	public int Pop() {
		int temp = cards [0];
		cards.RemoveAt (0);

		if (CardRemoved != null) {
			CardRemoved (this, new CardEventArgs (temp));
		}

		return temp;
	}

	public void Push(int card){
		cards.Add (card);

		if (CardAdded != null) {
			CardAdded (this, new CardEventArgs (card));
		}
	}

	public int HandValue() {
		int total = 0;
		int aces = 0;

		foreach (int card in GetCards()) {
			int cardRank = (card % 13) + 2;
			if (cardRank >= 10 && cardRank <= 13) {
				total += 10;
			} else if (cardRank == 14) {
				total += 11;
				aces += 1;
			} else {
				total += cardRank;
			}
		}
		while (total > 21 && aces > 0) {
			total -= 10;
			aces -= 1;
		}
		return total;
	}

	public void CreateDeck ()
	{
		cards.Clear ();

		for (int i = 0; i < 52; i++) {
			cards.Add (i);
		}

		int n = cards.Count;
		while (n > 1) {
			n--;
			int k = Random.Range (0, n + 1);
			int temp = cards [k];
			cards [k] = cards [n];
			cards [n] = temp;
		}
	}


	void Awake () 
	{
		cards = new List<int> ();
		if (isGameDeck) {
			CreateDeck ();
		}
	}
}
