require("BaseController")
require("UserView")
require("UserModel")

UserController = Class("UserController", BaseController)

function UserController:ctor()
    UserController.super.ctor(self, UserView, "UI/UserInfoPanel")
    self.model = UserModel.new()
end

function UserController:OnReady()
    -- 核心逻辑：数据传给视图
    self.view:UpdateName(self.model.playerName)

    -- 绑定关闭按钮
    local btn = self.view.BtnClose:GetComponent(typeof(CS.UnityEngine.UI.Button))
    btn.onClick:AddListener(function()
        self:Close()
    end)
end