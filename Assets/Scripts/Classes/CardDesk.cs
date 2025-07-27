using System;
using System.Collections.Generic;
public class Desk
{
    public List<Card> cardList = new List<Card>();
    public Card TakeRandomCard()
    {
        if(cardList.Count == 0)
        {
            return null;
        }
        Random rdn =  new Random();
        int index = rdn.Next(0, cardList.Count);
        return cardList[index];
    }
}
