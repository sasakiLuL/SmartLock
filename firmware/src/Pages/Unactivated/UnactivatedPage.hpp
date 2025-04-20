#pragma once

#include "../Core/Page/Page.hpp"
#include "../../Configurations/Configuration.hpp"
#include "../../DeviceState/DeviceState.hpp"
#include "../../Ui/Elements/Labels/Label.hpp"
#include "Model/UnactivatedPageModel.hpp"

namespace SmartLock
{
    class UnactivatedPage : public Page
    {
    private:
        Label _logoLabel;
        Label _idLabel;

        UnactivatedPageModel _viewModel;
    public:
        UnactivatedPage(Controller &controller, Render &render, Logger &logger, UnactivatedPageModel& viewModel);
        void bindings() override;
    };
}
