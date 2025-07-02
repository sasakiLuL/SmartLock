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
        _logoLabel.y(ScreenHeight / 2 - 
            LetterHeight * _logoLabel.textSize() / 2);
        _logoLabel.color(TFT_WHITE);

        _buffer.add(&_messageLabel);

        _messageLabel.textSize(1);
        _messageLabel.x(ScreenWidth / 2);
        _messageLabel.y(_logoLabel.y() + 
            LetterHeight * _logoLabel.textSize() / 2 + 
            LetterHeight * _messageLabel.textSize());
        _messageLabel.color(TFT_ORANGE);

        _buffer.add(&_tippLabel);

        _tippLabel.text(_tipp);
        _tippLabel.textSize(1);
        _tippLabel.x(ScreenWidth / 2);
        _tippLabel.y(_messageLabel.y() + 
            LetterHeight * _messageLabel.textSize() / 2 + 
            LetterHeight * _tippLabel.textSize());
        _tippLabel.color(TFT_ORANGE);

        _buffer.add(&_loadingLabel);

        _loadingLabel.text(_loadingString);
        _loadingLabel.textSize(1);
        _loadingLabel.x(ScreenWidth / 2);
        _loadingLabel.y(_tippLabel.y() + 
            LetterHeight * _tippLabel.textSize() / 2 + 
            LetterHeight * _loadingLabel.textSize());
        _loadingLabel.color(TFT_ORANGE);

        
    }

    void InitializationPage::bindings()
    {
        _messageLabel.text(_message);
        _tippLabel.text(_tipp);
    }

    void InitializationPage::loop()
    {
        auto now = std::chrono::steady_clock::now();

        if (now - _lastAnimationPoint >= _animationTime)
        {
            _lastAnimationPoint = now;
            _loadingLabel.clear(_buffer);
            shiftLoadingText(1);
            _loadingLabel.text(_loadingString);
            _loadingLabel.render(_buffer);
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