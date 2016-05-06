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
* Created:  5/6/2016 3:57:08 PM
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
            LuaObject.Register(L);
            LuaTransform.Register(L);
            LuaGameObject.Register(L);
            LuaComponent.Register(L);
            LuaMonoBehaviour.Register(L);
            LuaResources.Register(L);
            LuaUIButton.Register(L);
            LuaUILabel.Register(L);
            LuaDirector.Register(L);
            LuaScheduler.Register(L);
            LuaUIManager.Register(L);
        }
    }
}