using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeckController : MonoBehaviour
{
    public RectTransform m_target;
    public Transform m_tfTargetLeft;
    public Transform m_tfTargetRight;
    public List<GameObject> cardList;

    public GameObject m_prefCard;

    public HandCardController m_handCardController;

    [System.Obsolete]
    public void Fill()
    {
        GameObject obj = Instantiate(m_prefCard, transform) as GameObject;
        obj.transform.localPosition = Vector3.zero;

        CardController card = obj.GetComponent<CardController>();
        card.ShuffleAll();

        m_handCardController.AddCard(obj);

        /*
        m_handCardController.AddCard(cardList[0]);
        cardList.RemoveAt(0);
        */
    }

    void Update()
    {
    }
}
