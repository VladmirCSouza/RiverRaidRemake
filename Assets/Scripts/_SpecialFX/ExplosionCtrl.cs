using UnityEngine;
using System.Collections;

public class ExplosionCtrl : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		Destroy(transform.gameObject, 0.5f);	
	}
}
