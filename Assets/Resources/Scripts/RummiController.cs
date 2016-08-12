using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;

public class RummiController : MonoBehaviour
{
	public GameObject centerPanel;
	public GameObject sidePanel;
	public UILabel timerLabel;
	public List<AudioClip> timerSoundClips = new List<AudioClip>();
	private int timeRemaining;

	// AdMob
	private BannerView bannerView;
	private InterstitialAd interstitial;

	// Unity Life Cycle
	void Start()
	{
		sidePanel.transform.localPosition = new Vector3(820f, 0f, 0f);
		InitAdMob();
	}

	// NGUI Button Event
	public void OnStart()
	{
		ResetTimer();
		CancelInvoke();
		InvokeRepeating("UpdateTimer", 0.05f, 1f);

//		ShowAdMobInterstitial();
	}

	public void OnReset()
	{
		ResetTimer();
		SetTimer();
		CancelInvoke();
	}

	public void OnInfo()
	{
		TweenPosition.Begin(centerPanel, 0.5f, new Vector3(-205f, 0f, 0f)).method = UITweener.Method.EaseInOut;
		TweenPosition.Begin(sidePanel, 0.5f, Vector3.zero).method = UITweener.Method.EaseInOut;
	}

	public void OnBackInfo()
	{
		TweenPosition.Begin(centerPanel, 0.5f, Vector3.zero).method = UITweener.Method.EaseInOut;
		TweenPosition.Begin(sidePanel, 0.5f, new Vector3(820f, 0f, 0f)).method = UITweener.Method.EaseInOut;
	}

	// Timer Methods
	private void UpdateTimer()
	{
		if (timeRemaining < 0)
			return;

		SetTimer();
		SetSound();
		CountTimer();
	}

	private void ResetTimer()
	{
		timeRemaining = 60;
	}

	private void CountTimer()
	{
		timeRemaining--;
	}

	private void SetTimer()
	{
		TimeSpan timeSpan = TimeSpan.FromSeconds(timeRemaining);
		string timeText = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
		timerLabel.text = timeText;
	}

	private void SetSound()
	{
		int timerSoundClipIndex = 0;

		switch (timeRemaining)
		{
			case 30:
				timerSoundClipIndex = 0;
				break;
			case 20:
				timerSoundClipIndex = 1;
				break;
			case 10:
				timerSoundClipIndex = 2;
				break;
			case 9:
				timerSoundClipIndex = 3;
				break;
			case 8:
				timerSoundClipIndex = 4;
				break;
			case 7:
				timerSoundClipIndex = 5;
				break;
			case 6:
				timerSoundClipIndex = 6;
				break;
			case 5:
				timerSoundClipIndex = 7;
				break;
			case 4:
				timerSoundClipIndex = 8;
				break;
			case 3:
				timerSoundClipIndex = 9;
				break;
			case 2:
				timerSoundClipIndex = 10;
				break;
			case 1:
				timerSoundClipIndex = 11;
				break;
			case 0:
				timerSoundClipIndex = 12;
				break;
			default:
				return;
		}

		GetComponent<AudioSource>().clip = timerSoundClips[timerSoundClipIndex];
		GetComponent<AudioSource>().Play();
	}

	// AdMob
	private void InitAdMob()
	{
		interstitial = new InterstitialAd("ca-app-pub-3876363053826281/8161253254");
		bannerView = new BannerView("ca-app-pub-3876363053826281/6882996455", AdSize.SmartBanner, AdPosition.Bottom);

		AdRequest request = new AdRequest.Builder().Build();

		bannerView.LoadAd(request);
		interstitial.LoadAd(request);
	}

	private void ShowAdMobBanner()
	{
		bannerView.Show();
	}

	private void HideAdMobBanner()
	{
		bannerView.Hide();
	}

	private void ShowAdMobInterstitial()
	{
		if (interstitial.IsLoaded())
			interstitial.Show();
	}
}
