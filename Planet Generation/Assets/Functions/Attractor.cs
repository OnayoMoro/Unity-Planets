using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {

	const float G = 66.74f;

	float ISLdistance;

	public static List<Attractor> Attractors;

	public Rigidbody rb;

	bool planet_ting;

	void FixedUpdate ()
	{
		foreach (Attractor attractor in Attractors)
		{
			if (attractor != this)
				Attract(attractor);
		}
	}

	void OnEnable()
	{
		//planet_ting = Attractors.ToString().Contains("Planet");
		if (Attractors == null)
			Attractors = new List<Attractor>();

		planet_ting = rb.gameObject.name.Contains("Planet");

		if (planet_ting)
			Attractors.Add(this);
	}

	void OnDisable ()
	{
		Attractors.Remove(this);
	}

	void Attract (Attractor objToAttract)
	{
		Rigidbody rbToAttract = objToAttract.rb;

		Vector3 direction = rb.position - rbToAttract.position;
		float distance = direction.magnitude;

		if (distance == 0f)
			return;

		float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
		Vector3 force = direction.normalized * forceMagnitude;

		

		//Inverse square law

		// ISLdistance = Vector3.Distance(rb.position, objToAttract.transform.position);

		//Debug.Log("Old Force: " + force);

		// force = force * (1/(ISLdistance*ISLdistance));

		//Debug.Log("New Force: " + force);

		//Debug.Log("Distance: " + ISLdistance);
		
		this.rb.AddForce(force*-1);//Move this its meant to go last
	}

}