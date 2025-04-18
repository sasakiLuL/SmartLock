#pragma once

#include "../../Core/ViewModel/ViewModel.hpp"
#include "../../../Configurations/Configuration.hpp"

namespace SmartLock
{
    class UnactivatedPageModel : public ViewModel
    {
    private:
        Configuration &_config;
    public:
        UnactivatedPageModel(Logger &logger, Configuration &config)
            : ViewModel(logger), _config(config) {}

        std::string thingName() const { return _config.thingName; }
    };
}
