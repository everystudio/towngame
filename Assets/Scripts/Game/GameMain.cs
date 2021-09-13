using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using anogamelib;
using DG.Tweening;
using TMPro;
using System;

public class GameMain : StateMachineBase<GameMain>
{
    public static GameMain Instance;

    public ARROW_DIR m_lastArrowDir;
    public CARD_COLOR m_lastCardColor;

    public int m_iCardNum;
    public int m_iCombo;

    public DeckController m_deckController;
    public HandCardController m_handCardController;

    public PlayerController m_PlayerController;

    public TextMeshProUGUI m_txtCombo;
    public Button m_btnDelete;

    private UnityEvent OnSelectCardComplete = new UnityEvent();

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

    public bool SelectCard(CardController _card , Action _onSelect)
    {
        bool bRet = false;
        if ( _card.m_arrowDir == m_lastArrowDir || _card.m_cardColor == m_lastCardColor)
        {
            _onSelect.Invoke();

            _card.transform.SetParent(m_tfPlayStage);
            _card.transform.DOMove(m_tfPlayStage.position, 0.5f).OnComplete(()=>
            {
                OnSelectCardComplete.Invoke();
            });

            m_lastArrowDir = _card.m_arrowDir;
            m_lastCardColor = _card.m_cardColor;

            m_PlayerController.Move(m_lastArrowDir);

            m_iCombo += 1;

            m_txtCombo.text = $"{m_iCombo}Combo!";

            m_deckController.Fill();
            AddCard();

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

            machine.SetState(new GameMain.Playing(machine));
        }
    }

    private class Playing : StateBase<GameMain>
    {
        public Playing(GameMain _machine) : base(_machine)
        {
        }
        public override void OnEnterState()
        {
            base.OnEnterState();
            machine.OnSelectCardComplete.AddListener(() =>
            {
                int iNum = machine.m_handCardController.GetCanPlayCardNum(machine.m_lastArrowDir, machine.m_lastCardColor);
                if( 0 < iNum)
                {
                    Debug.Log($"ゲーム継続:{iNum}");
                }
                else
                {
                    machine.SetState(new GameMain.TurnEnd(machine));
                }
            });
        }
    }

    private class TurnEnd : StateBase<GameMain>
    {
        public TurnEnd(GameMain _machine) : base(_machine)
        {
        }
        public override void OnEnterState()
        {
            base.OnEnterState();
            Debug.Log("TurnEnd");

            machine.m_btnDelete.onClick.AddListener(() =>
            {
                machine.m_handCardController.DeleteAll();
            });

        }
    }
}
