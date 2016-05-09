require "ui/manager/UIManager"

testtable = {}
function testtable:test(arg)
    print(arg)
    return "table hellow"
end

test = function(arg)
    print(arg)
    return "hellow"
end

jit.on()

local function test(go)
    local v = go:New()
    v:AddComponent('UIButton')
end

local function main()
    local directory = Director:GetInstance()
    print(directory.value)
    directory.value = 2
    print(directory.value)

    directory:print(112)

    local ma = Director.GetInstance().uiManager
    ma:OpenView(0)
    directory:LogTest('Hellow World')

--    local go = GameObject
--    local time = os.clock()
--    print('begin creat 20000 gameObject now:'.. time)
--    for i = 1,20000 do
--        test(go)
--    end
--    print('cost time:' .. os.clock() - time..'ms  now:'..os.clock())
end

local mUIRoot

local function testLoad()
    local go = Resources:Load("UI/UI Root","GameObject")
    mUIRoot = GameObject:Instantiate(go,"GameObject").transform
    mUIRoot.name = go.name;
end

local function testOpenWindow(name)
    testLoad()
    local go = Resources:Load("UI/"..name, "GameObject");
    local ui = GameObject:Instantiate(go,"GameObject")
    ui.transform.parent = mUIRoot
    ui.transform.localScale = Vector3.New(1,1,1)
    ui.transform.localPosition = Vector3.New(0,0,0)
    ui.name = name
end

-- ±ÜÃâÄÚ´æÐ¹Â©
collectgarbage( "setpause", 100)
collectgarbage( "setstepmul", 5000)

testOpenWindow("ChatView")
local mn = Child:New()
local mn1 = Child:New()

print(mn1.value)
print(mn:GetValue())
print(mn1.value)

mUIRoot = nil

