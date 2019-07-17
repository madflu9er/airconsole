using System.Collections.Generic;
using UnityEngine;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class GameController : MonoBehaviour
{
	public GameObject playerPrefab;
	
	public Dictionary<int, PlayerController>players = new Dictionary<int, PlayerController>();
	private void Awake()
	{
		AirConsole.instance.onMessage += OnMessage;
		AirConsole.instance.onConnect += OnConnect;
		AirConsole.instance.onReady += OnReady;
	}

	void OnReady(string code)
	{
		List<int> connectedDevices = AirConsole.instance.GetControllerDeviceIds();
		foreach (int deviceId in connectedDevices)
		{
			AddNewPlayer(deviceId);
		}
	}

	void OnConnect(int deviceId)
	{
		AddNewPlayer(deviceId);	
	}

	private void AddNewPlayer(int deviceId)
	{
		if (players.ContainsKey(deviceId))
		{
			return;
		}
       
        GameObject newPlayer = Instantiate(playerPrefab, transform.position, transform.rotation) as GameObject;
        newPlayer.name = "Player" + deviceId;
        if (players.Count == 1)
        {
            newPlayer.GetComponentInChildren<Camera>().rect = new Rect(0f, 0.5f, 1f, 1f);
        }
        players.Add(deviceId, newPlayer.GetComponent<PlayerController>());
     
	}

	void OnMessage(int from, JToken data)
	{
		if (players.ContainsKey(from) && data["action"] != null)
		{
			players[from].ButtonInput(data["action"].ToString());
		}
	}
	
	void OnDestroy () {
		if (AirConsole.instance != null) {
			AirConsole.instance.onMessage -= OnMessage;		
			AirConsole.instance.onReady -= OnReady;		
			AirConsole.instance.onConnect -= OnConnect;		
		}
	}
}
