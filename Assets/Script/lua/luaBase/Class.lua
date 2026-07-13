function Class(classname, super)
    local cls = {}
    cls.__cname = classname
    cls.__index = cls
    if super then
        setmetatable(cls, {__index = super})
        cls.super = super
    else
        cls.ctor = function() end
    end
    function cls.new(...)
        local instance = setmetatable({}, cls)
        instance:ctor(...)
        return instance
    end
    return cls
end