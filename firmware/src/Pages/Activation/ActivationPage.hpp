#pragma once

#include "../Core/Page/Page.hpp"
#include "../../Ui/Elements/Buttons/Button.hpp"
#include "../../Ui/Elements/Labels/Label.hpp"
#include "Model/ActivationPageModel.hpp"

namespace SmartLock
{
    class ActivationPage : public Page
    {
    private:
        Label _usernameLabel;
        Label _infoLabel;
        Button _acceptButton;
        Button _rejectButton;

        ActivationPageModel &_viewModel;

    public:
        ActivationPage(Controller &controller, Render &render, Logger &logger, ActivationPageModel &viewModel);
        void bindings() override;
    };
}
