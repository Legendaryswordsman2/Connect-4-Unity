using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum CurrentTurn{ Player1, Player2, Null, Player1Won, Player2Won}

public class ConnectFour : MonoBehaviour
{
	[SerializeField] CurrentTurn currentTurn = CurrentTurn.Null;

	[SerializeField] Sprite playerOnePiece;
	[SerializeField] Sprite playerTwoPiece;
	[Space]
	[SerializeField] GameObject rows;
	[SerializeField] GameObject selectionSpotsParent;
	[SerializeField] TMP_Text WinText;

	Row[] rowChildren;
	[SerializeField] SelectionSpot[] selectionSpots;


	Sprite currentPiece;

	int index = 0;

	bool gameStillGoing = true;
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
		if (Input.GetKeyDown(KeyCode.Return) && gameStillGoing)
		{
			var currentRow = rowChildren[index].transform;
			for (int i = 0; i < 6; i++)
			{
				if (currentRow.GetChild(i).GetComponent<Spot>().isAvailable)
				{
					currentRow.GetChild(i).GetComponent<SpriteRenderer>().sprite = currentPiece;

					currentRow.GetChild(i).GetComponent<Spot>().isAvailable = false;

					Check(currentRow, i, index);

					SwitchTurn();

					break;
				}
			}

			//rowChildren[index].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = currentPiece;
		}

		if (Input.GetKeyDown(KeyCode.A) && gameStillGoing)
		{
			if (index <= 0) return;
			index--;

			for (int i = 0; i < selectionSpots.Length; i++)
			{
				selectionSpots[i].GetComponent<SpriteRenderer>().sprite = null;
			}
			selectionSpots[index].GetComponent<SpriteRenderer>().sprite = currentPiece;
		}

		if (Input.GetKeyDown(KeyCode.D) && gameStillGoing)
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
		if (currentTurn == CurrentTurn.Player1)
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

		if (currentTurn == CurrentTurn.Player2)
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
	void Check(Transform Row, int spotIndex, int rowIndex)
	{
		int rowIndexForBottomCheck = rowIndex;
		int spotIndexForBottomCheck = spotIndex;

		int rowIndexForHorizontalRightCheck = rowIndex;
		int spotIndexForHorizontalRightCheck = spotIndex;

		int rowIndexForHorizontalLeftCheck = rowIndex;
		int spotIndexForHorizontalLeftCheck = spotIndex;

		int RowIndexForUpRightCheck = rowIndex;
		int spotIndexForUpRightCheck = spotIndex;

		int RowIndexForUpLeftCheck = rowIndex;
		int spotIndexForUpLeftCheck = spotIndex;

		int RowIndexForDownRightCheck = rowIndex;
		int spotIndexForDownRightCheck = spotIndex;

		int RowIndexForDownLeftCheck = rowIndex;
		int spotIndexForDownLeftCheck = spotIndex;


		Sprite spotSprite = Row.GetChild(spotIndex).GetComponent<SpriteRenderer>().sprite;

		int amountInARowVertical = 1;
		for (int i = 0; i < 4; i++) // Check Vertical
		{
			if (spotIndexForBottomCheck > 0 && Row.GetChild(spotIndexForBottomCheck - 1).GetComponent<SpriteRenderer>().sprite == spotSprite)
			{
				spotIndexForBottomCheck--;
				amountInARowVertical++;

				if (amountInARowVertical == 4)
				{
					if (spotSprite == playerOnePiece)
					{
						currentTurn = CurrentTurn.Player1Won;
						Win();
					}

					if (spotSprite == playerTwoPiece)
					{
						currentTurn = CurrentTurn.Player2Won;
						Win();
					}
				}
			}
			else
			{
				break;
			}
		}
		int amountInARowHorizontalRight = 1;
		for (int i = 0; i < 4; i++) // Check Horizontal Right
		{
			if (rowIndexForHorizontalRightCheck < 6 && rows.transform.GetChild(rowIndexForHorizontalRightCheck + 1).GetChild(spotIndexForHorizontalRightCheck).GetComponent<SpriteRenderer>().sprite == spotSprite)
			{
				rowIndexForHorizontalRightCheck++;
				amountInARowHorizontalRight++;

				if (amountInARowHorizontalRight == 4)
				{
					if (spotSprite == playerOnePiece)
					{
						currentTurn = CurrentTurn.Player1Won;
						Win();
					}

					if (spotSprite == playerTwoPiece)
					{
						currentTurn = CurrentTurn.Player2Won;
						Win();
					}
				}
			}
			else
			{
				break;
			}
		}
		int amountInARowHorizontalLeft = 1;
		for (int i = 0; i < 4; i++) // Check Horizontal Left
		{
			if (rowIndexForHorizontalLeftCheck > 0 && rows.transform.GetChild(rowIndexForHorizontalLeftCheck - 1).GetChild(spotIndexForHorizontalRightCheck).GetComponent<SpriteRenderer>().sprite == spotSprite)
			{
				rowIndexForHorizontalLeftCheck--;
				amountInARowHorizontalLeft++;

				if (amountInARowHorizontalLeft == 4)
				{
					if (spotSprite == playerOnePiece)
					{
						currentTurn = CurrentTurn.Player1Won;
						Win();
					}

					if (spotSprite == playerTwoPiece)
					{
						currentTurn = CurrentTurn.Player2Won;
						Win();
					}
				}
			}
			else
			{
				break;
			}
		}
		int amountInARowUpRight = 1;
		for (int i = 0; i < 4; i++) // Check Up Right
		{
			if (spotIndexForUpRightCheck < 5 && RowIndexForUpRightCheck < 6)
			{
				spotIndexForUpRightCheck++;
				if (rows.transform.GetChild(RowIndexForUpRightCheck + 1).GetChild(spotIndexForUpRightCheck).GetComponent<SpriteRenderer>().sprite == spotSprite)
				{
					amountInARowUpRight++;
					RowIndexForUpRightCheck++;

					if (amountInARowUpRight == 4)
					{
						if (spotSprite == playerOnePiece)
						{
							currentTurn = CurrentTurn.Player1Won;
							Win();
						}

						if (spotSprite == playerTwoPiece)
						{
							currentTurn = CurrentTurn.Player2Won;
							Win();
						}
					}
				}
			}
			else
			{
				break;
			}
		}
		int amountInARowUpLeft = 1;
		for (int i = 0; i < 4; i++) // Check Up Left
		{
			if (spotIndexForUpLeftCheck < 5 && RowIndexForUpLeftCheck > 0)
			{
				spotIndexForUpLeftCheck++;
				if (rows.transform.GetChild(RowIndexForUpLeftCheck - 1).GetChild(spotIndexForUpLeftCheck).GetComponent<SpriteRenderer>().sprite == spotSprite)
				{
					amountInARowUpLeft++;
					RowIndexForUpLeftCheck--;

					if (amountInARowUpLeft == 4)
					{
						if (spotSprite == playerOnePiece)
						{
							currentTurn = CurrentTurn.Player1Won;
							Win();
						}

						if (spotSprite == playerTwoPiece)
						{
							currentTurn = CurrentTurn.Player2Won;
							Win();
						}
					}
				}
			}
			else
			{
				break;
			}
		}
		int amountInARowDownRight = 1;
		for (int i = 0; i < 4; i++) // Check Down Right
		{
			if(spotIndexForDownRightCheck > 0 && RowIndexForDownRightCheck < 6)
			{
				spotIndexForDownRightCheck--;
				if(rows.transform.GetChild(RowIndexForDownRightCheck + 1).GetChild(spotIndexForDownRightCheck).GetComponent<SpriteRenderer>().sprite == spotSprite)
				{
					amountInARowDownRight++;
					RowIndexForDownRightCheck++;

						if(amountInARowDownRight == 4)
						{
						if (spotSprite == playerOnePiece)
						{
							currentTurn = CurrentTurn.Player1Won;
							Win();
						}

						if (spotSprite == playerTwoPiece)
						{
							currentTurn = CurrentTurn.Player2Won;
							Win();
						}
					}
					}
				}
				else
				{
					break;
				}
			}
		int amountInARowDownLeft = 1;
		for (int i = 0; i < 4; i++) // Check Down Left
		{
			if (spotIndexForDownLeftCheck > 0 && RowIndexForDownLeftCheck > 0)
			{
				spotIndexForDownLeftCheck--;
				if (rows.transform.GetChild(RowIndexForDownLeftCheck - 1).GetChild(spotIndexForDownLeftCheck).GetComponent<SpriteRenderer>().sprite == spotSprite)
				{
					amountInARowDownLeft++;
					RowIndexForDownLeftCheck--;

					if (amountInARowDownLeft == 4)
					{
						if (spotSprite == playerOnePiece)
						{
							currentTurn = CurrentTurn.Player1Won;
							Win();
						}

						if (spotSprite == playerTwoPiece)
						{
							currentTurn = CurrentTurn.Player2Won;
							Win();
						}
					}
				}
			}
			else
			{
				break;
			}
		}
	}

	void Win()
	{
		gameStillGoing = false;
		selectionSpots[index].GetComponent<SpriteRenderer>().sprite = null;
		if (currentTurn == CurrentTurn.Player1Won)
		{
			WinText.text = "Game Over! Player One Won";
			WinText.gameObject.SetActive(true);
		}

	   if(currentTurn == CurrentTurn.Player2Won)
		{
			WinText.text = "Game Over! Player Two Won";
			WinText.gameObject.SetActive(true);
		}
		//StartCoroutine(EndGame());
	}

	IEnumerator EndGame()
	{
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene("Title");
	}
	public void BackToTitle()
	{
		SceneManager.LoadScene("Title");
	}
}
