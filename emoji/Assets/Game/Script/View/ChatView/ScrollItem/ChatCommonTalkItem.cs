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
* Filename: ChatCommonTalkItem
* Created:  2016/3/15 16:39:17
* Author:   HaYaShi ToShiTaKa
* Purpose:  聊天UI 快捷用于Item
* ==============================================================================
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatCommonTalkItem :UIBase {
	public UILabel labelText;
	public GameObject addStatue;
	public GameObject sendGroup;
	public GameObject select;
	public UIInput inputSend;
	public bool isEdit;
	public string text;
	public void FindItem() {
		labelText = transform.Find("name").GetComponent<UILabel>();
		select = transform.Find("xuanzhong").gameObject;
		addStatue = transform.Find("addstatue").gameObject;
		sendGroup = transform.Find("send_group").gameObject;
		inputSend = sendGroup.GetComponent<UIInput>();
	}
	public void SetText(string text) {
		this.text = text;
	}
	public void InitItem() {
		addStatue.SetActive(false);
		sendGroup.SetActive(false);
		select.SetActive(false);
		isEdit = false;
		labelText.text = text;
	}
	public void EditMode() {
		addStatue.SetActive(false);
		sendGroup.SetActive(true);
		select.SetActive(false);
		isEdit = true;
	}
}