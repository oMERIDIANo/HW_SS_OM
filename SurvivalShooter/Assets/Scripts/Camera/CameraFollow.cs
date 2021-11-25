using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraFollow : NetworkBehaviour
{
	GameObject player;
	public float smoothing = 5f;
	private Vector3 offset;
	PlayerHealth playerHealth;

	void Awake()
	{

	}

	void FixedUpdate()
	{
		if (player == null)
		{
			foreach (PlayerMovement playerMovement in FindObjectsOfType<PlayerMovement>())
			{
				if (playerMovement.isLocalPlayer)
				{
					player = playerMovement.gameObject;
					playerHealth = player.GetComponent<PlayerHealth>();
					offset = transform.position - player.transform.position;
				}
			}
		}

		if (player != null)
		{
			Vector3 playerPosition = player.transform.position;
			Vector3 camPosition = playerPosition + offset;
			transform.position = Vector3.Lerp(transform.position, camPosition, smoothing * Time.deltaTime);
		}
	}
}
