using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardStack : MonoBehaviour {

	List<int> cards;

	public IEnumerable<int> GetCards() {
		foreach (int i in cards) {
			yield return i;
		}
	}

	public void CreateDeck ()
	{
		if (cards == null) {
			cards = new List<int> ();
		} else {
			cards.Clear ();
		}

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
		CreateDeck ();
	}
}
