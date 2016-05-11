--require "ui/manager/UIManager"

--local mUIRoot

--local function testLoad()
--    local go = Resources:Load("UI/UI Root","GameObject")
--    mUIRoot = GameObject:Instantiate(go,"GameObject").transform
--    mUIRoot.name = go.name;
--end

--local function testOpenWindow(name)
--    testLoad()
--    local go = Resources:Load("UI/"..name, "GameObject");
--    local ui = GameObject:Instantiate(go,"GameObject")
--    ui.transform.parent = mUIRoot
--    ui.transform.localScale = Vector3.New(1,1,1)
--    ui.transform.localPosition = Vector3.New(0,0,0)
--    ui.name = name
--end

local function main()
    local directory = Director:GetInstance()
    directory.value = 2
    local ma = Director:GetInstance().uiManager
    ma:OpenView(0)
end



-- ±ÜÃâÄÚ´æÐ¹Â©
collectgarbage( "setpause", 100)
collectgarbage( "setstepmul", 5000)
main();

