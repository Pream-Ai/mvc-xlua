BaseController = Class("BaseController")
function BaseController:ctor(viewClass, addressableKey)
    self.viewClass = viewClass
    self.addressableKey = addressableKey
end
function BaseController:Open()
    CS.UIManager.Instance:OpenPanel(self.addressableKey, function(go)
        self.view = self.viewClass.new(go)
        local bridge = go:GetComponent(typeof(CS.LuaUIBridge))
        if bridge then self.view:InitBridge(bridge) end
        self:OnReady()
    end)
end
function BaseController:Close()
    if self.addressableKey then
        CS.UIManager.Instance:ClosePanel(self.addressableKey)
    end
end