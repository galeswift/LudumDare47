using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_starfield : MonoBehaviour
{
	public int MaxStars = 100;
	public float StarSize = 0.1f;
	public float StarSizeRange = 0.5f;
	public float FieldWidth = 20f;
	public float FieldHeight = 25f;
	public bool Colorize = true;

	float xOffset;
	float yOffset;

	ParticleSystem Particles;
	ParticleSystem.Particle[] Stars;
	Transform theCamera;

	void Start()
    {
		theCamera = Camera.main.transform;
	}

	void Awake()
	{
		Stars = new ParticleSystem.Particle[MaxStars];
		Particles = GetComponent<ParticleSystem>();


		xOffset = FieldWidth * 0.5f;                                                                                                        // Offset the coordinates to distribute the spread
		yOffset = FieldHeight * 0.5f;                                                                                                       // around the object's center

		for (int i = 0; i < MaxStars; i++)
		{
			float randSize = Random.Range(StarSizeRange, StarSizeRange + 1f);                       // Randomize star size within parameters
			float scaledColor = (true == Colorize) ? (randSize)/(StarSizeRange+1): 1f;         // If coloration is desired, color based on size

			Stars[i].position = GetRandomInRectangle(FieldWidth, FieldHeight) + transform.position;
			Stars[i].startSize = StarSize * randSize;
			Stars[i].startColor = new Color(1f, scaledColor, scaledColor, Random.Range(0.9f,1.0f));
		}
		Particles.SetParticles(Stars, Stars.Length);                                                                // Write data to the particle system
	}

	void Update()
    {
		for (int i = 0; i < MaxStars; i++)
		{
			Vector3 pos = Stars[i].position;

			//Debug.Log(string.Format("Pos is {0}", pos.ToString()));
			if (pos.y + transform.position.y < theCamera.position.y - yOffset)
			{
				pos.y += FieldHeight;
			}			

			Stars[i].position = pos;
		}
		Particles.SetParticles(Stars, Stars.Length);                                                                // Write data to the particle system
	}

	// GetRandomInRectangle
	//----------------------------------------------------------
	// Get a random value within a certain rectangle area
	//
	Vector3 GetRandomInRectangle(float width, float height)
	{
		float x = Random.Range(0, width);
		float y = Random.Range(0, height);
		return new Vector3(x - xOffset, y - yOffset, 0);
	}
}
