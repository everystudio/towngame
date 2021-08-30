using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;
using DG.Tweening;

public class GameMain : StateMachineBase<GameMain>
{
    public static GameMain Instance;

    public ARROW_DIR m_lastArrowDir;
    public CARD_COLOR m_lastCardColor;

    public int m_iCardNum;

    private void Awake()
    {
        Instance = this;
    }

    public Transform m_tfPlayStage;

    public bool SelectCard(CardController _card)
    {
        bool bRet = false;
        if ( _card.m_arrowDir == m_lastArrowDir || _card.m_cardColor == m_lastCardColor)
        {
            _card.transform.SetParent(m_tfPlayStage);
            _card.transform.DOMove(m_tfPlayStage.position, 0.5f);

            m_lastArrowDir = _card.m_arrowDir;
            m_lastCardColor = _card.m_cardColor;
            bRet = true;
        }
        else
        {
            Debug.Log("��������v���ĂȂ�");
        }
        return bRet;
    }
    public void AddCard()
    {
        m_iCardNum += 1;
    }

}