using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }

    private bool isQuitting = false;

    public List<Drops> drops;

    public void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!isQuitting)
        {
            float randomNumber = UnityEngine.Random.Range(0f, 100f);
            List<Drops> possibleDrops = new List<Drops>();

            foreach (Drops rate in drops)
            {
                if (randomNumber <= rate.dropRate)
                {
                    possibleDrops.Add(rate);
                }
            }

            if (possibleDrops.Count > 0)
            {
                Drops drop = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
                Instantiate(drop.itemPrefab, transform.position, Quaternion.identity);
            }
        }
    }

}
