--region type
local function istype(value, targetType)
    return type(value) == targetType
end



local function lookupMetatable(t,index)
    local value
    if (istype(t,"table")) then
        value = rawget(t, index)
    end
    
    if value then
        return value
    else
        local meta = getmetatable(t)
        if not(meta) then
            return nil
        end
        return lookupMetatable(meta, index)
    end
end

function readIndex(t, index)
    local value = lookupMetatable(t, index)
    if (istype(index, "string")) then
        if (istype(value, "table")) then
            if (istype(rawget(value, "get"), "function")) then
                value = value.get(t)
            else
                value = rawget(t, index)
            end
        elseif (not(istype(value, "function"))) then
            value = rawget(t, index)
        end
    end
    
    assert(value ~= nil,"value nil attempt to read a private value!")
    return value
end

function writeIndex(t, index, value)
    local preValue = lookupMetatable(t, index)

    if (istype(preValue, "table")) then
        if (istype(preValue["set"], "function")) then
            preValue.set(t, value)
            return
        else
            assert(false,"attempt to write a private value!")
            return
        end
    end
    t[index] = value
end
        
function readonly(targetTable)
    local proxy = { proto = targetTable }
    local mt = {
        __index = function(t,index) return readIndex(targetTable, index) end,
        __newindex = function(t, index, value) writeIndex(targetTable, index, value) end
    }
    setmetatable(proxy, mt)
    return proxy
end

function closeGlobalField()
    local mt = {
        __newindex = function()
            error("attempt to new a global field!")
        end
    }
     setmetatable(_G, mt) 
end
--endregion

--region class
function clone(object)
    local lookup_table = { }
    local function _copy(object)
        if type(object) ~= "table" then
            return object
        elseif lookup_table[object] then
            return lookup_table[object]
        end
        local new_table = { }
        lookup_table[object] = new_table
        for key, value in pairs(object) do
            new_table[_copy(key)] = _copy(value)
        end
        return setmetatable(new_table, getmetatable(object))
    end
    return _copy(object)
end

local function ctor(proto,instance,cls,...)
    setmetatable(instance, cls)
    if cls.super then
        ctor(proto,cls,cls.super,...)
    end
    getmetatable(instance).ctor(proto,instance,...)
end

-- Create an class.
function class(classname, super)
    local superType = type(super)
    local cls

    if superType ~= "function" and superType ~= "table" then
        superType = nil
        super = nil
    end

    if superType == "function" or(super and super.__ctype == 1) then
        -- inherited from native C++ Object
        cls = { }

        if superType == "table" then
            -- copy fields from super
            for k, v in pairs(super) do cls[k] = v end
            cls.__create = super.__create
            cls.super = super
        else
            cls.__create = super
        end

        cls.ctor = function() end
        cls.__cname = classname
        cls.__ctype = 1

        function cls.New(...)
            local instance = cls.__create(...)
            -- copy fields from class to native object
            for k, v in pairs(cls) do instance[k] = v end
            instance.class = cls
            instance:ctor(...)
            return instance
        end

    else
        -- inherited from Lua Object
        if super then
            cls = clone(super)
            cls.super = super
        else
            cls = { ctor = function() end }
        end

        cls.__cname = classname
        cls.__ctype = 2
        -- lua
        cls.__index = cls

        function cls.New(...)
            local instance = {}
            ctor(instance,instance,cls,...)
            return readonly(instance)
        end
    end

    return cls
end

local function GetInstance(t)
    if not(rawget(t, "_instance")) then
        rawset(t, "_instance", t:New())
    end

    return t._instance
end
function singleton(classname, super)
    local instance = readonly(class(classname, super))
    instance.GetInstance = function() 
        return GetInstance(instance)
    end
    return instance
end
--endregion