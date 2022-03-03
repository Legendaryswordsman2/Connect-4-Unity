using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentTurn{ Player1, Player2, Null}

public class ConnectFour : MonoBehaviour
{
	CurrentTurn currentTurn = CurrentTurn.Null;

	[SerializeField] Sprite playerOnePiece;
	[SerializeField] Sprite playerTwoPiece;
	[Space]
	[SerializeField] GameObject rows;

	[SerializeField] Row[] rowChildren;

	Sprite currentPiece;

	int index = 0;
	private void Awake()
	{
		currentPiece = playerOnePiece;

		rowChildren = rows.GetComponentsInChildren<Row>();

		rowChildren[index].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = currentPiece;
	}
	private void Update()
	{
		//rowChildren[index].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = currentPiece;

		if (Input.GetKeyDown(KeyCode.A))
		{
			if (index <= 0) return;
			index--;

			for (int i = 0; i < rowChildren.Length; i++)
			{
				rowChildren[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
			}
			rowChildren[index].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = currentPiece;
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			if (index >= rowChildren.Length - 1) return;
			index++;

			for (int i = 0; i < rowChildren.Length; i++)
			{
				rowChildren[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
			}
			rowChildren[index].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = currentPiece;
		}
	}
}
