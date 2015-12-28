using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

internal static class Gvar
{
	internal const int nbPlayerMax = 20;
	internal const int nbZone = 2;
	internal const int scoreMax = 200;

	internal static float movePlayerFactor;
	internal static float throwElemFactor;
	internal static float movePlayerVmax;
	internal static enGameState gameState { get; private set; }

	internal static int[] score = new int[nbZone];

	internal static void setGameState(enGameState state)
	{
		gameState = state;
	}

}

internal enum enGameState
{ 
	None,
	Intro,
	Ready,
	Play,
	End
}
