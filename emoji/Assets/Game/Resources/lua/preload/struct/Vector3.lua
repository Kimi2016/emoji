
--region Vector3
Vector3 = {}

function Vector3.New(x, y, z)
    local self = setmetatable({}, Vector3)
    assert(type(x) == "number" and type(y) == "number" and type(z) == "number")
    self.x = x
    self.y = y
    self.z = z
    return self
end

function Vector3.__sub(a, b)
    assert(type(a) == "table" and type(b) == "table")
    assert(getmetatable(a) == Vector3 and getmetatable(b) == Vector3)
    assert(type(a.x) == "number" and type(a.y) == "number" and type(a.z) == "number")
    assert(type(b.x) == "number" and type(b.y) == "number" and type(b.z) == "number")

    return Vector3.New(a.x - b.x, a.y - b.y, a.z - b.z)
end

function Vector3.__add(a, b)
    assert(type(a) == "table" and type(b) == "table")
    assert(getmetatable(a) == Vector3 and getmetatable(b) == Vector3)
    assert(type(a.x) == "number" and type(a.y) == "number" and type(a.z) == "number")
    assert(type(b.x) == "number" and type(b.y) == "number" and type(b.z) == "number")

    return Vector3.New(a.x + b.x, a.y + b.y, a.z + b.z)
end

function Vector3.__mul(a, b)
    if(type(a) == "table" and type(b) == "number") then
        assert(getmetatable(a) == Vector3)
        assert(type(a.x) == "number" and type(a.y) == "number" and type(a.z) == "number")
        return Vector3.New(a.x * b, a.y * b, a.z * b)
    end
    
    if(type(b) == "table" and type(a) == "number") then
        assert(getmetatable(b) == Vector3)
        assert(type(b.x) == "number" and type(b.y) == "number" and type(b.z) == "number")
        return Vector3.New(b.x * a, b.y * a, b.z * a)
    end

    assert(false)
end

function Vector3.__div(a, b)
    assert(type(a) == "table" and type(b) == "number")
    assert(getmetatable(a) == Vector3)
    assert(type(a.x) == "number" and type(a.y) == "number" and type(a.z) == "number")

    return Vector3.New(a.x / b, a.y / b, a.z / b)
end

function Vector3.__eq(a, b)
    assert(type(a) == "table" and type(b) == "table")
    assert(getmetatable(a) == Vector3 and getmetatable(b) == Vector3)
    assert(type(a.x) == "number" and type(a.y) == "number" and type(a.z) == "number")
    assert(type(b.x) == "number" and type(b.y) == "number" and type(b.z) == "number")

    return a.x == b.x and a.y == b.y and a.z == b.z
end
--endregion