using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class HandCardController : MonoBehaviour
{
    public Transform m_tfTargetLeft;
    public Transform m_tfTargetRight;
    public List<GameObject> m_goCardList;

    public int GetCanPlayCardNum(ARROW_DIR _dir , CARD_COLOR _color)
    {
        int iRet = 0;
        for (int i = 0; i < m_goCardList.Count; i++)
        {
            CardController card = m_goCardList[i].GetComponent<CardController>();
            if( card.CanPlay(_dir, _color))
            {
                iRet += 1;
            }
        }
        return iRet;
    }

    public void SelectCardListener(CardController _card)
    {
        //Debug.Log("SelectCardListener");
        if (GameMain.Instance.SelectCard(_card, () =>
       {
           _card.m_eStatus = CARD_STATUS.FIELD;
           _card.OnClick.RemoveListener(SelectCardListener);
           m_goCardList.Remove(_card.gameObject);
       }))
        {
            PositionReset();
        }
    }

    public void AddCard(GameObject _obj)
    {
        m_goCardList.Add(_obj);
        _obj.GetComponent<CardController>().OnClick.AddListener(SelectCardListener);
        PositionReset();
    }

    public void PositionReset()
    {
        Vector3 positionLeft = m_tfTargetLeft.GetComponent<RectTransform>().position;
        Vector3 positionDiff = m_tfTargetRight.GetComponent<RectTransform>().position - positionLeft;
        Vector3 positionDelta = positionDiff / (m_goCardList.Count + 1);

        for ( int i = 0; i< m_goCardList.Count; i++)
        {
            Vector3 targetPosition = positionLeft + positionDelta * (i + 1);
            m_goCardList[i].GetComponent<CardController>().m_eStatus = CARD_STATUS.HAND;
            m_goCardList[i].GetComponent<RectTransform>().DOMove(targetPosition, 0.25f).OnComplete(() =>
            {
                m_goCardList[i].transform.SetParent(this.transform);
           });
        }

    }

    public void DeleteAll()
    {
        foreach(GameObject obj in m_goCardList)
        {
            obj.GetComponent<CardController>().DeleteRequest();
            //_card.OnClick.RemoveListener(SelectCardListener);
        }
        m_goCardList.Clear();

    }


}
