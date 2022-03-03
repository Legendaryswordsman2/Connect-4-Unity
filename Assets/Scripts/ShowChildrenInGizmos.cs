using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowChildrenInGizmos : MonoBehaviour
{
	[SerializeField] float size = 0.3f;
	private void OnDrawGizmos()
	{
		foreach (Transform child in transform)
		{
			Gizmos.DrawWireSphere(child.position, size);
		}
	}
}
