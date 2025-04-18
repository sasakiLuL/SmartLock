#pragma once

#include "../../../Logging/Logger.hpp"
#include "../Controller/Controller.hpp"

namespace SmartLock
{
    class ViewModel
    {
    protected:
        Logger &_logger;

    public:
        ViewModel(Logger &logger) : _logger(logger) {}
        virtual ~ViewModel() {}
    };
}