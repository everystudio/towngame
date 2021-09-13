using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
	public Vector2Int m_PlayerPos;

	public void Move(ARROW_DIR _dir)
	{
		m_PlayerPos += Defines.arrow_vecs[(int)_dir];

		transform.DOMove(new Vector3((float)m_PlayerPos.x, (float)m_PlayerPos.y, 0f), 0.25f);
	}

}
