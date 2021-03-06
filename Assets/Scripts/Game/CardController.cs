using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using anogamelib;
using DG.Tweening;

public enum CARD_STATUS
{
    DECK,
    HAND,
    FIELD   ,
    MAX     ,
}

public class CardEvent : UnityEvent<CardController>
{
}

public class CardController : StateMachineBase<CardController>,IBeginDragHandler,IEndDragHandler,IDragHandler,IPointerClickHandler
{
    public CARD_STATUS m_eStatus;
    public ARROW_DIR m_arrowDir;
    public CARD_COLOR m_cardColor;

    public Image m_imgCardBase;
    public Image m_imgArrow;

    public bool m_bDebugMove = false;

    public CardEvent OnClick = new CardEvent();


    private void Start()
    {
        SetState(new CardController.Idle(this));
    }

    private void OnValidate()
    {
        if( m_imgCardBase == null)
        {
            m_imgCardBase = transform.Find("areaCenter").GetComponent<Image>();
        }
        if(m_imgArrow == null)
        {
            m_imgArrow = transform.Find("areaCenter/imgArrow").GetComponent<Image>();
        }
        //Debug.Log("OnValidate");
        SetColor(m_cardColor);
        SetDir(m_arrowDir);
    }

    public void SetColor(CARD_COLOR _color )
    {
        m_imgCardBase.color = Defines.card_colors[(int)_color];
    }
    private void SetDir(ARROW_DIR _arrowDir)
    {
        int eular_dir = Defines.arrow_dirs[(int)_arrowDir];
        m_imgArrow.gameObject.transform.localRotation = Quaternion.Euler(0, 0, eular_dir);
    }
    public bool CanPlay(ARROW_DIR _dir, CARD_COLOR _color)
    {
        bool bRet = false;
        if( m_arrowDir == _dir || m_cardColor == _color)
        {
            bRet = true;
        }
        return bRet;
    }
    public void DeleteRequest()
    {
        float fDulation = 0.25f;
        foreach(Graphic g in GetComponentsInChildren<Graphic>())
        {
            g.DOFade(0, fDulation);
        }
        Destroy(gameObject, fDulation);

    }

    [Obsolete]
    public void ShuffleAll()
    {
        int iDir = UnityEngine.Random.RandomRange(0, (int)ARROW_DIR.MAX);
        int iColor = UnityEngine.Random.RandomRange(0, (int)CARD_COLOR.MAX);

        m_cardColor = (CARD_COLOR)iColor;
        m_arrowDir = (ARROW_DIR)iDir;

        SetDir((ARROW_DIR)iDir);
        SetColor( (CARD_COLOR)iColor);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        // ?J?[?h?????????????????????????J?b?g
        if (m_bDebugMove)
        {
            transform.Translate(eventData.delta);
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke(this);
        //GameMain.Instance.SelectCard(this);
    }

    private class Idle : StateBase<CardController>
    {
        public Idle(CardController _machine) : base(_machine)
        {
        }
    }
}
