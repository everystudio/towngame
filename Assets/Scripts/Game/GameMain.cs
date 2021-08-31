using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anogamelib;
using DG.Tweening;
using TMPro;

public class GameMain : StateMachineBase<GameMain>
{
    public static GameMain Instance;

    public ARROW_DIR m_lastArrowDir;
    public CARD_COLOR m_lastCardColor;

    public int m_iCardNum;
    public int m_iCombo;

    public TextMeshProUGUI m_txtCombo;

    #region イベント関連のメンバー
    public EventString OnGameStartRequest;
	#endregion

	private void Awake()
    {
        Instance = this;

        m_txtCombo.text = "";

        SetState(new Standby(this));

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
            m_iCombo += 1;

            m_txtCombo.text = $"{m_iCombo}Combo!";
            bRet = true;
        }
        else
        {
            Debug.Log("何かが一致してない");
        }
        return bRet;
    }
    public void AddCard()
    {
        m_iCardNum += 1;
    }

    private class Standby : StateBase<GameMain>
    {
        public Standby(GameMain _machine) : base(_machine)
        {
        }
        public override void OnUpdateState()
        {
            base.OnUpdateState();
            if(Input.GetMouseButtonDown(0))
            {
                Debug.Log("click");
                machine.SetState(new GameMain.GameStart(machine));
            }
        }

    }

    private class GameStart : StateBase<GameMain>
    {
        public GameStart(GameMain _machine) : base(_machine)
        {
        }
        public override void OnEnterState()
        {
            Debug.Log("GameStart.OnEnterState");
            base.OnEnterState();
            machine.OnGameStartRequest.Invoke("start");
        }
    }
}
