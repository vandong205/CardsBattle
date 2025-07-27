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
        Card gotCard = cardList[index];
        RemoveCard(gotCard);
        return gotCard;
    }
    public void RemoveCard(Card removingCard) {
        for (int i = cardList.Count - 1; i >= 0; i--)
        {
            if (cardList[i].rank == removingCard.rank && cardList[i].suitID == removingCard.suitID)
            {
                cardList.RemoveAt(i);
                return;
            }
        }
    }
    public void Shuffle()
    {
        Random rdn = new Random();
        int number = cardList.Count;
        while(number > 1)
        {
            number--;
            int k = rdn.Next(number+1);
            (cardList[k], cardList[number]) = (cardList[number], cardList[k]);
        }
    }
    public Card GetFirstCard()
    {
        if (cardList.Count > 0) { 
            return cardList[0];
        }else return null;
    }
}
