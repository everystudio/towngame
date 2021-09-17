using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
	public Vector2Int m_PlayerGrid;

	public void Move(ARROW_DIR _dir)
	{
		m_PlayerGrid += Defines.arrow_vecs[(int)_dir];
		Vector2 targetPos = GetPosition(m_PlayerGrid);

		transform.DOMove(
			new Vector3(
				(float)targetPos.x,
				(float)targetPos.y,
				0f), 0.25f);
	}

	private Vector2 GetPosition( Vector2Int _grid)
	{
		Vector2 ret = new Vector2(
			0.5f + _grid.x,
			0.5f + _grid.y
			);
		return ret;
	}

}
