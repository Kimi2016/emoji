--region Vector4
Vector4 = {}

function Vector4.New(x, y, z, w)
    local self = setmetatable({}, Vector4)
    assert(type(x) == "number" and type(y) == "number" and type(z) == "number" and type(w) == "number")
    self.x = x
    self.y = y
    self.z = z
    self.w = w
    return self
end

function Vector4.__sub(a, b)
    assert(type(a) == "table" and type(b) == "table")
    assert(getmetatable(a) == Vector4 and getmetatable(b) == Vector4)
    assert(type(a.x) == "number" and type(a.y) == "number" and type(a.z) == "number" and type(a.w) == "number")
    assert(type(b.x) == "number" and type(b.y) == "number" and type(b.z) == "number" and type(b.w) == "number")

    return Vector4.New(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w)
end

function Vector4.__add(a, b)
    assert(type(a) == "table" and type(b) == "table")
    assert(getmetatable(a) == Vector4 and getmetatable(b) == Vector4)
    assert(type(a.x) == "number" and type(a.y) == "number" and type(a.z) == "number" and type(a.w) == "number")
    assert(type(b.x) == "number" and type(b.y) == "number" and type(b.z) == "number" and type(b.w) == "number")

    return Vector4.New(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w)
end

function Vector4.__mul(a, b)
    if(type(a) == "table" and type(b) == "number") then
        assert(getmetatable(a) == Vector4)
        assert(type(a.x) == "number" and type(a.y) == "number" and type(a.z) == "number" and type(a.w) == "number")
        return Vector4.New(a.x * b, a.y * b, a.z * b, a.w * b)
    end
    
    if(type(b) == "table" and type(a) == "number") then
        assert(getmetatable(b) == Vector4)
        assert(type(b.x) == "number" and type(b.y) == "number" and type(b.z) == "number" and type(b.w) == "number")
        return Vector4.New(b.x * a, b.y * a, b.z * a, b.w * a)
    end

    assert(false)
end

function Vector4.__div(a, b)
    assert(type(a) == "table" and type(b) == "number")
    assert(getmetatable(a) == Vector4)
    assert(type(a.x) == "number" and type(a.y) == "number" and type(a.z) == "number" and type(a.w) == "number")

    return Vector4.New(a.x / b, a.y / b, a.z / b, a.w / b)
end

function Vector4.__eq(a, b)
    assert(type(a) == "table" and type(b) == "table")
    assert(getmetatable(a) == Vector4 and getmetatable(b) == Vector4)
    assert(type(a.x) == "number" and type(a.y) == "number" and type(a.z) == "number" and type(a.w) == "number")
    assert(type(b.x) == "number" and type(b.y) == "number" and type(b.z) == "number" and type(b.w) == "number")

    return a.x == b.x and a.y == b.y and a.z == b.z and a.w == b.w
end
--endregion
