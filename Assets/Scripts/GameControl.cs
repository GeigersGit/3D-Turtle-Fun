using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

	public static GameControl control;

	public float health;
	public float experience;

	// Use this for initialization
	void Awake () {
		if(control == null)
		{
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if(control != this)
		{
			Destroy(gameObject);
		}
	}
	
	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

		PlayerData data = new PlayerData();
		data.health = health;
		data.experience = experience;

		bf.Serialize(file, data);
		file.Close();
	}
		
}

[Serializable]
class PlayerData
{
	public float health;
	public float experience;
}
