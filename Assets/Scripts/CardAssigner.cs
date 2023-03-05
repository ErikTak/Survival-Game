using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAssigner : MonoBehaviour
{
    public CardScriptableObject[] cards;
    public GameObject[] gameObjects;

    private void Start()
    {
        int[] usedCards = new int[cards.Length];
        for (int i = 0; i < gameObjects.Length; i++)
        {
            int randomIndex = Random.Range(0, cards.Length);
            while (usedCards[randomIndex] == 1)
            {
                randomIndex = Random.Range(0, cards.Length);
            }
            usedCards[randomIndex] = 1;

//            gameObjects[i].GetComponent<CardDisplay>().card = cards[randomIndex];
        }
        FindObjectOfType<CardDisplay>().SetCardDetails();
    }
}

