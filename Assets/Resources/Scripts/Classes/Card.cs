using UnityEngine;

public class Card
{
    public int rank { get; set; }
    public int suitID;
    public Card() { }
    public Card(int initrank,int initsuitID)
    {
        rank = initrank;
        suitID = initsuitID;    
    }
}
