using System;

public class CardRemovedEventArgs : EventArgs
{
	public int CardIndex { get; private set; }

	public CardRemovedEventArgs (int cardIndex)
	{
		CardIndex = cardIndex;
	}
}
