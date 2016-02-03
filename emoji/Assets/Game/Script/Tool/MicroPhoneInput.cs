/*
               #########                       
              ############                     
              #############                    
             ##  ###########                   
            ###  ###### #####                  
            ### #######   ####                 
           ###  ########## ####                
          ####  ########### ####               
         ####   ###########  #####             
        #####   ### ########   #####           
       #####   ###   ########   ######         
      ######   ###  ###########   ######       
     ######   #### ##############  ######      
    #######  #####################  ######     
    #######  ######################  ######    
   #######  ###### #################  ######   
   #######  ###### ###### #########   ######   
   #######    ##  ######   ######     ######   
   #######        ######    #####     #####    
    ######        #####     #####     ####     
     #####        ####      #####     ###      
      #####       ###        ###      #        
        ###       ###        ###              
         ##       ###        ###               
__________#_______####_______####______________

                我们的未来没有BUG              
* ==============================================================================
* Filename: MicroPhoneInput
* Created:  2016/1/21 10:29:29
* Author:   HaYaShi ToShiTaKa
* Purpose:  
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class MicroPhoneInput : MonoBehaviour {

	public float sensitivity = 100;
	public float loudness = 0;
	public float playTime = 0;
	public Action<Byte[]> onRecordTimeOut = null;
	private static string[] micArray = null;
	private AudioSource _audio;

	const int HEADER_SIZE = 44;
	const int RECORD_TIME = 20;
	const int INT16_SCALE = 32767;
	const int FRENQUENCY = 2756;
	// Use this for initialization
	void Awake() {
		_audio = GetComponent<AudioSource>();
		micArray = Microphone.devices;
		foreach (string deviceStr in Microphone.devices) {
			Debug.Log("device name = " + deviceStr);
		}
		if (micArray.Length == 0) {
			Debug.Log("no mic device");
		}
	}
	public void StartRecord() {

		_audio.Stop();
		if (micArray.Length == 0) {
			Debug.Log("No Record Device!");
			return;
		}
		_audio.loop = false;
		_audio.mute = true;
		_audio.clip = Microphone.Start(null, false, RECORD_TIME, FRENQUENCY); //2756 5512 11025 22050   44100
		while (!(Microphone.GetPosition(null) > 0)) {
		}
		_audio.Play();
		//倒计时  
		StartCoroutine(TimeDown());
	}
	public Byte[] StopRecord() {
		if (micArray.Length == 0) {
			Debug.Log("No Record Device!");
			return null;
		}
		Microphone.End(null);
		_audio.Stop();

		return GetClipData();
	}
	public Byte[] GetClipData() {
		if (_audio.clip == null) {
			return null;
		}
		
		float[] samples = new float[_audio.clip.samples];
		_audio.clip.GetData(samples, 0);

		float[] compressSamples = null;
		bool isRecord = false;
		for (int i = samples.Length - 1; i >= 0; i--) {
			if (isRecord) {
				compressSamples[i] = samples[i];
			}
			else {
				if (samples[i] == 0 && !isRecord) {
					continue;
				}
				else {
					isRecord = true;
					compressSamples = new float[i + 1];
					compressSamples[i] = samples[i];
				}
			}
		}
		samples = null;
		samples = compressSamples;

		Byte[] outData = new byte[samples.Length * 2];

		for (int i = 0; i < samples.Length; i++) {
			Int16 temshort = (Int16)(samples[i] * INT16_SCALE);
			Byte[] temdata = BitConverter.GetBytes(temshort);
			outData[i * 2] = temdata[0];
			outData[i * 2 + 1] = temdata[1];
		}

		if (outData == null || outData.Length <= 0) {
			return null;
		}
		print("out data:" + outData.Length);
		Byte[] compressData = SevenZipCompress.Compress(outData);
		print("compress data" + compressData.Length);
		return compressData;
	}
	public Byte[] ConvertInt16ToByte(Int16[] intData) {
		Byte[] outData = new byte[intData.Length * 2];

		for (int i = 0; i < intData.Length; i++) {
			Byte[] temdata = BitConverter.GetBytes(intData[i]);
			outData[i * 2] = temdata[0];
			outData[i * 2 + 1] = temdata[1];
		}

		return outData;
	}
	public Int16[] ConvetByteToInt16(Byte[] byteData) {
		if (byteData.Length % 2 != 0) {
			return null;
		}
		Int16[] outData = new Int16[byteData.Length / 2];

		for (int i = 0; i < outData.Length; i++) {
			Byte[] tmpByte = { byteData[i * 2], byteData[i * 2 + 1] };
			Int16 tmpdata = BitConverter.ToInt16(tmpByte, 0);
			outData[i] = tmpdata;
		}
		return outData;
	}
	public void PlayClipData(Byte[] byteArr) {
		PlayClipData(ConvetByteToInt16(byteArr));
	}
	public void PlayClipData(Int16[] intArr) {
		if (intArr == null || intArr.Length == 0) {
			return;
		}
		//从Int16[]到float[]  
		float[] samples = new float[intArr.Length];
		for (int i = 0; i < intArr.Length; i++) {
			samples[i] = (float)intArr[i] / INT16_SCALE;
		}
		//从float[]到Clip  
		ResetAudio();
		_audio.clip.SetData(samples, 0);
		_audio.loop = false;
		_audio.mute = false;
		_audio.Play();
	}
	public void ResetAudio() {
		if (_audio.isPlaying) {
			_audio.Stop();
		}
	}
	void PlayRecord() {
		if (_audio.clip == null) {
			Debug.Log("audio.clip=null");
			return;
		}
		_audio.mute = false;
		_audio.loop = false;
		_audio.Play();
		Debug.Log("PlayRecord");
	}
	public void LoadAndPlayRecord() {
		string recordPath = "your path";

		//SavWav.LoadAndPlay (recordPath);  
	}
	public float GetAveragedVolume() {
		float[] data = new float[256];
		float a = 0;
		_audio.GetOutputData(data, 0);
		for (int i = 0; i < data.Length; i++) {
			a += Mathf.Abs(data[i]);
		}
		return a;
	}

	private IEnumerator TimeDown() {
		int time = 0;
		while (time < RECORD_TIME) {
			if (!Microphone.IsRecording(null)) { //如果没有录制  
				Debug.Log("IsRecording false");
				yield break;
			}
			yield return new WaitForSeconds(1);
			time++;
		}
		if (time >= RECORD_TIME) {
			Byte[] data = StopRecord();
			if (onRecordTimeOut != null) {
				onRecordTimeOut(data);
			}
		}
		yield return 0;
	}
}