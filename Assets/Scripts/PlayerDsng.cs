
using UnityEngine;
using DG.Tweening;

public class PlayerDsng : MonoBehaviour {

	protected internal static PlayerDsng playr;
	public bool IsinCollider = false;

	private void Awake()
	{
		if(playr == null)
		{
			playr = this;
		}
		else
		{
			Destroy(this);
		}
	}

	private void Update()
	{
		Debug.DrawRay(transform.position, transform.forward, Color.red, 1000f);
		//RaycastHit hit;

		//if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance: 1f))
		//{

		//}


	}


	private void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Obstacle"))
		{
			Debug.Log(transform.position);
			Debug.Log("hiDude");
			
			DOTween.KillAll();
			
		}
	}
	private void OnTriggerStay(Collider other)
	{
		
		//if (other.CompareTag("Obstacle"))
		//{
		//	IsinCollider = true;
		//}
		
	}

	private void OnTriggerExit(Collider other)
	{
		//if (other.CompareTag("Obstacle"))
		//{
		//	IsinCollider = false;
		//}
	}
}
