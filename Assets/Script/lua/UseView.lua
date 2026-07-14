require("BaseView")
UserView = Class("UserView", BaseView)
function UserView:UpdateName(nameStr)
    local textComp = self.TxtName:GetComponent(typeof(CS.UnityEngine.UI.Text))
    textComp.text = nameStr
end