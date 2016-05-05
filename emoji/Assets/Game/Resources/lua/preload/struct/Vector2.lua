--region Vector2
Vector2 = {}

function Vector2.New(x, y)
    local self = setmetatable({}, Vector2)
    assert(type(x) == "number" and type(y) == "number")
    self.x = x
    self.y = y
    return self
end

function Vector2.__sub(a, b)
    assert(type(a) == "table" and type(b) == "table")
    assert(getmetatable(a) == Vector2 and getmetatable(b) == Vector2)
    assert(type(a.x) == "number" and type(a.y) == "number")
    assert(type(b.x) == "number" and type(b.y) == "number")

    return Vector2.New(a.x - b.x, a.y - b.y)
end

function Vector2.__add(a, b)
    assert(type(a) == "table" and type(b) == "table")
    assert(getmetatable(a) == Vector2 and getmetatable(b) == Vector2)
    assert(type(a.x) == "number" and type(a.y) == "number")
    assert(type(b.x) == "number" and type(b.y) == "number")

    return Vector2.New(a.x + b.x, a.y + b.y)
end

function Vector2.__mul(a, b)
    if(type(a) == "table" and type(b) == "number") then
        assert(getmetatable(a) == Vector2)
        assert(type(a.x) == "number" and type(a.y) == "number")
        return Vector2.New(a.x * b, a.y * b)
    end
    
    if(type(b) == "table" and type(a) == "number") then
        assert(getmetatable(b) == Vector2)
        assert(type(b.x) == "number" and type(b.y) == "number")
        return Vector2.New(b.x * a, b.y * a)
    end

    assert(false)
end

function Vector2.__div(a, b)
    assert(type(a) == "table" and type(b) == "number")
    assert(getmetatable(a) == Vector2)
    assert(type(a.x) == "number" and type(a.y) == "number")

    return Vector2.New(a.x / b, a.y / b)
end

function Vector2.__eq(a, b)
    assert(type(a) == "table" and type(b) == "table")
    assert(getmetatable(a) == Vector2 and getmetatable(b) == Vector2)
    assert(type(a.x) == "number" and type(a.y) == "number")
    assert(type(b.x) == "number" and type(b.y) == "number")

    return a.x == b.x and a.y == b.y
end
--endregion