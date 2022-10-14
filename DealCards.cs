
using UnityEngine;
using UdonSharp;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public class DealCards : UdonSharpBehaviour
{
    public GameObject CardParentObject;
    public GameObject[] Aces;
    public int HandsToDeal;
    public float radius;
    public Text ResetByField;

    private double cardWidth = 0.13;

    public void DealShitty()
    {
        ResetDeck();

        var shuffledCards = ShuffleCards();
        PlaceCardsAroundPoint(HandsToDeal, Vector3.zero, radius, 4, shuffledCards);

        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SetResetByText");
    }

    public void PlaceCardsAroundPoint(int num, Vector3 point, float radius, int cardsPerHand, GameObject[] cards)
    {
        int cardIndex = 0;
        for (int i = 0; i < num; i++)
        {
            /* Distance around the circle */
            var radians = 2 * Mathf.PI / num * i;

            /* Get the vector direction */
            var vertrical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector3((float)horizontal, 0, (float)vertrical);

            /* Get the spawn position */
            var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            /* Move cards */
            for (int c = 0; c < cardsPerHand; c++)
            {
                var card = cards[cardIndex];
                card.transform.localPosition = spawnPos;
                /* Rotate the cards towards the center */
                card.transform.LookAt(CardParentObject.transform.position);
                var rotation = card.transform.localRotation.eulerAngles;
                rotation.x = -180;
                card.transform.localRotation = Quaternion.Euler(rotation);

                card.transform.Translate(((float)cardWidth * c), 0, 0);
                Debug.Log(card.transform.localPosition.x + (cardWidth * c));
                cardIndex++;
            }
        }
    }

    public void ResetCards()
    {
        ResetDeck();
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SetResetByText");
    }


    public void DealCircle()
    {
        ResetDeck();

        var shuffledCards = ShuffleCards();
        PlaceCardsAroundPoint(52, Vector3.zero, radius, 1, shuffledCards);

        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SetResetByText");
    }

    public void DealHorseRase()
    {
        ResetDeckWithOffset(new Vector3(0, 0, 0.7f));

        var shuffledCards = ShuffleCards();

        // Lay out Aces in a row
        for (int i = 1; i <= Aces.Length; i++)
        {
            var ace = Aces[i - 1];
            ace.transform.Translate((float)(cardWidth) * i, 0, 0);
            ace.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        var cardsDelt = 0;
        for (int i = 0; cardsDelt < 6; i++)
        {
            var card = shuffledCards[i];
            if (!card.name.StartsWith("Ace"))
            {
                card.transform.Translate(0, 0, -(float)(cardWidth * 1.4) * (cardsDelt + 1));
                cardsDelt++;
            }
        }

        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SetResetByText");
    }


    private void ResetDeck()
    {
        Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
        Networking.SetOwner(Networking.LocalPlayer, CardParentObject);

        for (int i = 0; i < CardParentObject.transform.childCount; i++)
        {
            var child = CardParentObject.transform.GetChild(i);

            var pos = CardParentObject.transform.position;
            Networking.SetOwner(Networking.LocalPlayer, child.gameObject);

            //pos.z += (UnityEngine.Random.Range(0, 30) / 1000);
            child.transform.position = pos;
            child.transform.localRotation = Quaternion.Euler(-180, 0, 0);

            child.transform.Translate((float)(Random.Range(-52, 52) * 0.0001), 0, (float)(Random.Range(-52, 52) * 0.0001));
        }
    }

    private void ResetDeckWithOffset(Vector3 offset)
    {
        Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
        Networking.SetOwner(Networking.LocalPlayer, CardParentObject);

        for (int i = 0; i < CardParentObject.transform.childCount; i++)
        {
            var child = CardParentObject.transform.GetChild(i);

            var pos = CardParentObject.transform.position;
            Networking.SetOwner(Networking.LocalPlayer, child.gameObject);

            //pos.z += (UnityEngine.Random.Range(0, 30) / 1000);
            child.transform.position = pos;
            child.transform.localRotation = Quaternion.Euler(-180, 0, 0);

            child.transform.Translate((float)(Random.Range(-52, 52) * 0.0001), 0, (float)(Random.Range(-52, 52) * 0.0001));
            child.transform.Translate(offset);
        }
    }

    private GameObject[] ShuffleCards()
    {
        //Put all the cards into an array so we can shuffle the array
        GameObject[] cards = new GameObject[CardParentObject.transform.childCount];
        for (int i = 0; i < CardParentObject.transform.childCount; i++)
        {
            cards[i] = CardParentObject.transform.GetChild(i).gameObject;
        }
        // Shuffle the cards
        Shuffle(cards);
        Shuffle(cards);

        return cards;
    }

    private void Shuffle(GameObject[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = Random.Range(0, n--);
            GameObject temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    public void SetResetByText()
    {
        this.ResetByField.text = Networking.GetOwner(this.gameObject).displayName;
    }
}
