using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVswap : MonoBehaviour {
	public float speedX = 0.5f;
	public float speedY = 0.5f;

	private void Update()
	{
		float offsetX = Time.time * speedX;
		float offsetY = Time.time * speedY;

		transform.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);
	}
}