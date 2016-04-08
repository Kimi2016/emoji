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
* Filename: LuaRegister
* Created:  4/8/2016 9:47:16 PM
* Author:   HaYaShi ToShiTaKa
* Purpose:  Lua导出类注册的地方,本类由插件自动生成
* ==============================================================================
*/
namespace LuaInterface {
    using System.Collections.Generic;
    using System;
    using System.Collections;


    public class LuaRegister {
        public static void Register(IntPtr L) {
            LuaDirector.Register(L);
            LuaScheduler.Register(L);
            LuaUIManager.Register(L);
            LuaGameObject.Register(L);
            LuaUIButton.Register(L);
            LuaUILabel.Register(L);
            LuaMonoBehaviour.Register(L);
        }
    }
}