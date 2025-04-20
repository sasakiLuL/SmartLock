#include "ActivationPage.hpp"

namespace SmartLock
{
    ActivationPage::ActivationPage(Controller &controller, Render &render, Logger &logger, ActivationPageModel &viewModel)
        : Page(controller, render, logger), _viewModel(viewModel)
    {
        _buffer.add(&_usernameLabel);

        _usernameLabel.textSize(2);
        _usernameLabel.x(ScreenWidth / 2);
        _usernameLabel.y(ScreenHeight / 2 - LetterHeight * _usernameLabel.textSize());
        _usernameLabel.color(TFT_WHITE);

        _buffer.add(&_infoLabel);

        _infoLabel.text("tries to a activate device. Activate?");
        _infoLabel.textSize(1);
        _infoLabel.x(ScreenWidth / 2);
        _infoLabel.y(ScreenHeight / 2 - LetterHeight * _infoLabel.textSize() + LetterHeight * _usernameLabel.textSize());
        _infoLabel.color(TFT_WHITE);

        _buffer.add(&_acceptButton);

        _acceptButton.text("Accept");
        _acceptButton.textColor(TFT_WHITE);
        _acceptButton.color(TFT_ORANGE);
        _acceptButton.height(40);
        _acceptButton.width(100);
        _acceptButton.x((ScreenWidth - _acceptButton.width()) / 4);
        _acceptButton.y(ScreenHeight / 2 - LetterHeight * _usernameLabel.textSize() + LetterHeight * 5);
        _acceptButton.onClick([this]()
        {
            _viewModel.sendActivateMessage();
            _controller.changePage(Path::Actions); 
        });

        _buffer.add(&_rejectButton);

        _rejectButton.text("Reject");
        _rejectButton.textColor(TFT_WHITE);
        _rejectButton.color(TFT_ORANGE);
        _rejectButton.height(40);
        _rejectButton.width(100);
        _rejectButton.x((ScreenWidth - _acceptButton.width()) / 4 * 3);
        _rejectButton.y(ScreenHeight / 2 - LetterHeight * _usernameLabel.textSize() + LetterHeight * 5);
        _rejectButton.onClick([this]() 
        {
            _viewModel.sendRejectMessage();
            _controller.changePage(Path::Unactivated); 
        });
    }

    void ActivationPage::bindings()
    {
        _usernameLabel.text(_viewModel.username());
    }
}