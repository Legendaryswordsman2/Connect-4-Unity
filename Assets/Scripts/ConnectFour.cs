using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentTurn{ Player1, Player2, Null}

public class ConnectFour : MonoBehaviour
{
	[SerializeField] CurrentTurn currentTurn = CurrentTurn.Null;

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
		currentTurn = CurrentTurn.Player1;
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

					Check(currentRow, i);

					SwitchTurn();

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

	void SwitchTurn()
	{
		if(currentTurn == CurrentTurn.Player1)
		{
			currentPiece = playerTwoPiece;
			//index = 0;

			//for (int i = 0; i < selectionSpots.Length; i++)
			//{
			//	selectionSpots[i].GetComponent<SpriteRenderer>().sprite = null;
			//}
			selectionSpots[index].GetComponent<SpriteRenderer>().sprite = currentPiece;

			currentTurn = CurrentTurn.Player2;
			return;
		}

		if(currentTurn == CurrentTurn.Player2)
		{
			currentPiece = playerOnePiece;
			//index = 0;

			//for (int i = 0; i < selectionSpots.Length; i++)
			//{
			//	selectionSpots[i].GetComponent<SpriteRenderer>().sprite = null;
			//}
			selectionSpots[index].GetComponent<SpriteRenderer>().sprite = currentPiece;

			currentTurn = CurrentTurn.Player1;
			return;
		}
	}
	void Check(Transform Row, int spotIndex)
	{

		//Debug.Log("Checking: " + Row + "   " + spotIndex);
		Sprite spotSprite = Row.GetChild(spotIndex).GetComponent<SpriteRenderer>().sprite;

		//if (spotIndex > 0 && Row.GetChild(spotIndex - 1).GetComponent<SpriteRenderer>().sprite == spotSprite)
		//{
		//	Debug.Log("Bottom Same As Top");
		//}

		int amountInARow = 1;
		for (int i = 0; i < 4; i++)
		{
			if (spotIndex > 0 && Row.GetChild(spotIndex - 1).GetComponent<SpriteRenderer>().sprite == spotSprite)
			{
				spotIndex--;
				amountInARow++;

				if(amountInARow == 4)
				{
					Debug.Log("FOUR IN A ROW VERTICAL");
				}
			}
		}
	}
}
