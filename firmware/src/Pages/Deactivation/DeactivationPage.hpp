#pragma once

#include "../Core/Page/Page.hpp"
#include "../../Ui/Elements/Buttons/Button.hpp"
#include "../../Ui/Elements/Labels/Label.hpp"
#include "Model/DeactivationPageModel.hpp"

namespace SmartLock
{
    class DeactivationPage : public Page
    {
    private:
        Label _infoLabel;
        Button _acceptButton;
        Button _rejectButton;

        DeactivationPageModel& _viewModel;

    public:
        DeactivationPage(Controller &controller, Render &render, Logger &logger, DeactivationPageModel& viewModel);
    };
}
