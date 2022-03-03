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
	[SerializeField] GameObject selectionSpotsParent;

	Row[] rowChildren;
	[SerializeField] SelectionSpot[] selectionSpots;


	Sprite currentPiece;

	int index = 0;
	private void Awake()
	{
		currentPiece = playerOnePiece;

		rowChildren = rows.GetComponentsInChildren<Row>();
		selectionSpots = selectionSpotsParent.GetComponentsInChildren<SelectionSpot>();

		selectionSpots[index].GetComponent<SpriteRenderer>().sprite = currentPiece;
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			var currentRow = rowChildren[index].transform;
			for (int i = 0; i < 6; i++)
			{
				if (currentRow.GetChild(i).GetComponent<Spot>().isAvailable)
				{
					currentRow.GetChild(i).GetComponent<SpriteRenderer>().sprite = currentPiece;

					currentRow.GetChild(i).GetComponent<Spot>().isAvailable = false;

					break;
				}
			}

			//rowChildren[index].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = currentPiece;
		}

		if (Input.GetKeyDown(KeyCode.A))
		{
			if (index <= 0) return;
			index--;

			for (int i = 0; i < selectionSpots.Length; i++)
			{
				selectionSpots[i].GetComponent<SpriteRenderer>().sprite = null;
			}
			selectionSpots[index].GetComponent<SpriteRenderer>().sprite = currentPiece;
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			if (index >= selectionSpots.Length - 1) return;
			index++;

			for (int i = 0; i < rowChildren.Length; i++)
			{
				selectionSpots[i].GetComponent<SpriteRenderer>().sprite = null;
			}
			selectionSpots[index].GetComponent<SpriteRenderer>().sprite = currentPiece;
		}
	}
}
