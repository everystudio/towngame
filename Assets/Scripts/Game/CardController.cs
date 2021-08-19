using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CardController : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler,IPointerClickHandler
{
    public ARROW_DIR m_arrowDir;
    public CARD_COLOR m_cardColor;

    public Image m_imgCardBase;
    public Image m_imgArrow;

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


    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.Translate(eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameMain.Instance.SelectCard(this);
    }
}
