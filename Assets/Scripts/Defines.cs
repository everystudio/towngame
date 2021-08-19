using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ARROW_DIR{
	UP,
	LEFT,
	DOWN,
	RIGHT,
	MAX
}
public enum CARD_COLOR
{
	RED,
	GREEN,
	BLUE,
	YELLOW,
	PURPLE,
	CYAN,
	MAX
}

public class Defines
{
	public static readonly int[] arrow_dirs = new int[(int)ARROW_DIR.MAX]
	{
		0,
		90,
		180,
		270
	};
	public static readonly Color[] card_colors = new Color[(int)CARD_COLOR.MAX]
	{
		new Color(1.0f,0.0f,0.0f),
		new Color(0.0f,1.0f,0.0f),
		new Color(0.0f,0.0f,1.0f),
		new Color(1.0f,1.0f,0.0f),
		new Color(1.0f,0.0f,1.0f),
		new Color(0.0f,1.0f,1.0f),
	};
}
