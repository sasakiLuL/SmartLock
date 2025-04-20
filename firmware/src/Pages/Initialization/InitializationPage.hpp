#pragma once

#include "../Core/Page/Page.hpp"
#include "../../Ui/Elements/Labels/Label.hpp"
#include <chrono>

namespace SmartLock
{
    class InitializationPage : public Page
    {
    private:
        Label _logoLabel;
        Label _loadingLable;

        std::chrono::milliseconds _animationTime;
        std::chrono::steady_clock::time_point _lastAnimationPoint;

        std::string _loadingString;

        void shiftLoadingText(int shift);

    public:
        InitializationPage(Controller &controller, Render &render, Logger &logger);
        void loop() override;
    };
}
