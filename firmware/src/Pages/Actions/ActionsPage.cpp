#include "ActionsPage.hpp"

namespace SmartLock
{
    ActionsPage::ActionsPage(Controller &controller, Render &render, Logger &logger, ActionsPageModel &modelView)
        : Page(controller, render, logger), _viewModel(modelView) 
    {
        _buffer.add(&_openButton);
        
        _openButton.text();
        _openButton.textColor(TFT_WHITE);
        _openButton.color(TFT_ORANGE);
        _openButton.height(40);
        _openButton.width(150);
        _openButton.x((ScreenWidth - _openButton.width()) / 2);
        _openButton.y((ScreenHeight - _openButton.height()) / 2);

        _openButton.onClick([this]()
        {
            _viewModel.toggle();
            this->render();
        });

        _buffer.add(&_deactivateButton);

        _deactivateButton.text("Deactivate");
        _deactivateButton.textColor(TFT_ORANGE);
        _deactivateButton.color(TFT_BLACK);
        _deactivateButton.height(40);
        _deactivateButton.width(150);
        _deactivateButton.x((ScreenWidth - _openButton.width()) / 2);
        _deactivateButton.y(ScreenHeight / 2 + 50);
        _deactivateButton.onClick([this]()
        { 
            _controller.changePage(Path::Deactivation); 
        });
    }

    void ActionsPage::bindings()
    {
        _openButton.text(_viewModel.isOpened() ? "Close" : "Open");
    }
}
