#pragma once

#include "../Core/Page/Page.hpp"
#include "../../Ui/Elements/Buttons/Button.hpp"
#include "../../Ui/Elements/Labels/Label.hpp"
#include "Model/ActionsPageModel.hpp"

namespace SmartLock
{
    class ActionsPage : public Page
    {
    private:
        ActionsPageModel &_viewModel;

        Button _openButton;

    public:
        ActionsPage(Controller &controller, Render &render, Logger &logger, ActionsPageModel &modelView);
        void bindings() override;
    };
}
