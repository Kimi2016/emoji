-- region UIManager.lua
--[[
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
* Filename: UIManager
* Created:  2016/5/4 16:48:23
* Author:   HaYaShi ToShiTaKa
* Purpose:  UI管理器
--]]

UIManager = singleton("UIManager")

function UIManager:ctor()
    
    self.m_cachedUINameList = { }        --存在UI的名字
    self.cachedUINameList = {
        get = function(this)
            return self.m_cachedUINameList
        end,
    }
    self.m_achedUIDict = { }            --存储在内存的UI,key为名字
end

Father = class("Father")

function Father:ctor()
    self.m_value = 0
    self.value = {
        get = function(this)
            return self.m_value
        end,
        set = function(this, value)
            self.m_value = value
        end,
    }
end

function Father:GetValue()
    self = self.proto
    self.m_value = self.m_value + 1
    print("father")
    return self.m_value
end

Child = class("Child",Father)

function Child:ctor()
    self.value = {
        get = function(this)
            return self.m_value + 1
        end,
        set = function(this, value)
            self.m_value = value
        end,
    }
end

function Child:GetValue()
    Father.GetValue(self)
    self = self.proto
    self.m_value = self.m_value + 1
    return self.m_value
end
--endregion