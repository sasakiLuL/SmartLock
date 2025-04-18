#include "InitializationPage.hpp"

namespace SmartLock
{
    InitializationPage::InitializationPage(Controller &controller, Render &render, Logger &logger)
        : Page(controller, render, logger), _animationTime(1000), _lastAnimationPoint(std::chrono::steady_clock::now()), _loadingString("Loading...")
    {
        _buffer.add(&_logoLabel);

        _logoLabel.text("SmartLock");
        _logoLabel.textSize(3);
        _logoLabel.x(ScreenWidth / 2);
        _logoLabel.y(ScreenHeight / 2 - LetterHeight * _logoLabel.textSize() / 2);
        _logoLabel.color(TFT_WHITE);

        _buffer.add(&_loadingLable);

        _loadingLable.text(_loadingString);
        _loadingLable.textSize(2);
        _loadingLable.x(ScreenWidth / 2);
        _loadingLable.y(ScreenHeight / 2 - LetterHeight * _logoLabel.textSize() / 2 + LetterHeight * _logoLabel.textSize());
        _loadingLable.color(TFT_ORANGE);
    }

    void InitializationPage::loop()
    {
        auto now = std::chrono::steady_clock::now();

        if (now - _lastAnimationPoint >= _animationTime)
        {
            _lastAnimationPoint = now;
            _loadingLable.clear(_buffer);
            shiftLoadingText(1);
            _loadingLable.text(_loadingString);
            _loadingLable.render(_buffer);
        }

        Page::loop();
    }

    void InitializationPage::shiftLoadingText(int shift)
    {
        shift %= _loadingString.length();
        if (shift == 0) return;
    
        _loadingString = _loadingString.substr(_loadingString.length() - shift) + _loadingString.substr(0, _loadingString.length() - shift);
    }
}