using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class MainMenuController : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera vcam = null;
	[SerializeField] private Text desc = null;
	[SerializeField] private AudioSource startAudio = null;

	public float speed = 0.4f;
	public float limitMinPostion = -1;
	public float limitMaxPostion = 4;
	public bool isForward = true;

	private CinemachineTrackedDolly trackedDolly = null;
	private bool isStartGame = false;
    private void Awake()
    {
       // PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        //PlayGamesPlatform.InitializeInstance(config);
       
        //Social.localUser.Authenticate((bool success) =>
        //{
        //    // handle success or failure
        //    if (success)
        //    {
        //        //updateScore();
        //        PlayGamesPlatform.Activate();
        //    }
        //});
    }
    private void Start()
	{
		if(vcam == null) return;
		trackedDolly = vcam.GetCinemachineComponent(CinemachineCore.Stage.Body) as CinemachineTrackedDolly;
	}

	private void Update()
	{
		
	}

	public void StartGame()
	{
		if(isStartGame) return;

		isStartGame = true;
		SceneManager.LoadSceneAsync("Demo", LoadSceneMode.Single);
		desc.text = "Loading...";
		startAudio.Play();
	}
   
    private void FixedUpdate()
	{
		if(trackedDolly == null) return;

		if(isForward)
		{
			trackedDolly.m_PathPosition += Time.fixedDeltaTime * speed;
		}
		else
		{
			trackedDolly.m_PathPosition -= Time.fixedDeltaTime * speed;
		}

		if(trackedDolly.m_PathPosition > limitMaxPostion)
		{
			isForward = false;
		}
		else if(trackedDolly.m_PathPosition <= limitMinPostion)
		{
			isForward = true;
		}
	}
}
