require("Object")
BaseView = Class("BaseView")
function BaseView:ctor(gameObject)
    self.gameObject = gameObject
    self.transform = gameObject.transform
end
function BaseView:InitBridge(bridgeScript)
    bridgeScript:Init(self)
end