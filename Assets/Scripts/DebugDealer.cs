using UnityEngine;
using System.Collections;

public class DebugDealer : MonoBehaviour {

	public CardStack dealer;
	public CardStack player;


	void OnGUI() {
		if (GUI.Button(new Rect(10, 10, 256, 28), "Hit Me!")) {
			player.Push(dealer.Pop());
		}
	}

}
