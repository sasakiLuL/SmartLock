#include "DeactivationPage.hpp"

namespace SmartLock
{
    DeactivationPage::DeactivationPage(Controller &controller, Render &render, Logger &logger, DeactivationPageModel &viewModel)
        : Page(controller, render, logger), _viewModel(viewModel)
    {
        _buffer.add(&_infoLabel);

        _infoLabel.text("Do you want to deactivate device?");
        _infoLabel.textSize(1);
        _infoLabel.x(ScreenWidth / 2);
        _infoLabel.y(ScreenHeight / 2 - LetterHeight * _infoLabel.textSize());
        _infoLabel.color(TFT_WHITE);

        _buffer.add(&_acceptButton);

        _acceptButton.text("Accept");
        _acceptButton.textColor(TFT_WHITE);
        _acceptButton.color(TFT_ORANGE);
        _acceptButton.height(40);
        _acceptButton.width(100);
        _acceptButton.x((ScreenWidth - _acceptButton.width()) / 4);
        _acceptButton.y(ScreenHeight / 2 - LetterHeight * _infoLabel.textSize() + LetterHeight * 5);
        _acceptButton.onClick([this]()
        {
            _viewModel.sendDeactivationMessage();
            _controller.changePage(Path::Unactivated); 
        });

        _buffer.add(&_rejectButton);

        _rejectButton.text("Reject");
        _rejectButton.textColor(TFT_ORANGE);
        _rejectButton.color(TFT_BLACK);
        _rejectButton.height(40);
        _rejectButton.width(100);
        _rejectButton.x((ScreenWidth - _acceptButton.width()) / 4 * 3);
        _rejectButton.y(ScreenHeight / 2 - LetterHeight * _infoLabel.textSize() + LetterHeight * 5);
        _rejectButton.onClick([this]()
        { 
            _controller.changePage(Path::Actions); 
        });
    }
}