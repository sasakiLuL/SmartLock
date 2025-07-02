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
        Label _loadingLabel;
        Label _messageLabel;
        Label _tippLabel;

        std::chrono::milliseconds _animationTime;
        std::chrono::steady_clock::time_point _lastAnimationPoint;

        std::string _loadingString;

        std::string _message;
        std::string _tipp;

        void shiftLoadingText(int shift);

    public:
        InitializationPage(Controller &controller, Render &render, Logger &logger);
        void message(const std::string& message) { _message = message; }
        void tipp(const std::string& tipp) { _tipp = tipp; }
        void loop() override;
        void bindings() override;
    };
}
