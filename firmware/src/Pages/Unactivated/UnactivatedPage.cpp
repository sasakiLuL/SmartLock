#include "UnactivatedPage.hpp"
#include "../../Ui/Elements/Labels/Label.hpp"

namespace SmartLock
{
    UnactivatedPage::UnactivatedPage(Controller &controller, Render &render, Logger &logger, UnactivatedPageModel &viewModel)
        : Page(controller, render, logger), _viewModel(viewModel)
    {
        _buffer.add(&_logoLabel);

        _logoLabel.text("SmartLock");
        _logoLabel.textSize(3);
        _logoLabel.x(ScreenWidth / 2);
        _logoLabel.y(ScreenHeight / 2 - LetterHeight * _logoLabel.textSize());
        _logoLabel.color(TFT_WHITE);

        _buffer.add(&_idLabel);

        _idLabel.textSize(1);
        _idLabel.x(ScreenWidth / 2);
        _idLabel.y(ScreenHeight / 2 - LetterHeight * _logoLabel.textSize() + LetterHeight * _logoLabel.textSize());
        _idLabel.color(TFT_ORANGE);
    }

    void UnactivatedPage::bindings()
    {
        _idLabel.text(_viewModel.thingName());
    }
}
