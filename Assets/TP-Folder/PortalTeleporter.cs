using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour {

	public Transform player;
	public Transform reciever;       // Le portail qui reçoit le joueur
	public Transform thrower;		 // Le portail qui envoit le joueur

	private bool playerIsOverlapping = false;

	public bool Rescale;
	

	void Update () {
		if (playerIsOverlapping)
		{
			Vector3 portalToPlayer = player.position - transform.position;
			float dotProduct = Vector3.Dot(transform.up, portalToPlayer);
			
			if (dotProduct < 0f)
			{
				float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
				rotationDiff += 180;
				player.Rotate(Vector3.up, rotationDiff);

				Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
				player.position = reciever.position + positionOffset;

				playerIsOverlapping = false;
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = true;
		}

		if (Rescale)
		{
			Debug.Log("Siez change");
			player.localScale = new Vector3(reciever.localScale.x * player.localScale.x / thrower.localScale.x,
				reciever.localScale.y * player.localScale.y / thrower.localScale.y, 
				reciever.localScale.z * player.localScale.z / thrower.localScale.z);
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = false;
		}
	}
}
