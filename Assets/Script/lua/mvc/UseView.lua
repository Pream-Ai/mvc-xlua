require("BaseView")
UserView = Class("UserView", BaseView)
function UserView:UpdateName(nameStr)
    -- TxtName 是等会要在 Unity 里拖进去绑定的名字
    local textComp = self.TxtName:GetComponent(typeof(CS.UnityEngine.UI.Text))
    textComp.text = nameStr
end