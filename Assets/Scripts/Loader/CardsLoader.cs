using NUnit.Framework;
using System.IO;
using System.Xml;
using UnityEngine;
using System.Collections.Generic;

public class CardsLoader
{
    private string CardsConfigUrl = Path.Combine(Application.streamingAssetsPath, "Configs/Cards.xml");

    public List<Card> LoadCardsFromXml()
    {
        List<Card> allCards = new List<Card>(); 
        if (!File.Exists(CardsConfigUrl))
        {
            Debug.LogError("Không tìm thấy file: " + CardsConfigUrl);
            return null;
        }

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(CardsConfigUrl);

        XmlNodeList cardNodes = xmlDoc.GetElementsByTagName("Card");

        foreach (XmlNode cardNode in cardNodes)
        {
            string rank = cardNode.Attributes["rank"].Value;
            int suitId = int.Parse(cardNode.Attributes["suitId"].Value);

            Card card = new Card { rank = rank, suitID = suitId };
            allCards.Add(card);
        }

        Debug.Log("Đã load " + allCards.Count + " lá bài!");
        return allCards;
    }
}
