--region Rect
Rect = {}

function Rect.New(x, y, width, height)
    local self = setmetatable({}, Vector2)
    assert(type(x) == "number" and type(y) == "number" and type(width) == "number" and type(height) == "number")
    self.x = x
    self.y = y
    self.width = width
    self.height = height
    return self
end

function Rect.__eq(a, b)
    assert(type(a) == "table" and type(b) == "table")
    assert(getmetatable(a) == Rect and getmetatable(b) == Rect)
    assert(type(a.x) == "number" and type(a.y) == "number" and type(a.width) == "number" and type(a.height) == "number")
    assert(type(b.x) == "number" and type(b.y) == "number" and type(b.width) == "number" and type(b.height) == "number")

    return a.x == b.x and a.y == b.y and a.width == b.width and a.height == b.height
end
--endregion
