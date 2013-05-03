define(['jquery', 'studioApp'], function ($, studio) {
    var module = {};

    module.init = function () {
        if ($('#js-image-settings')) {
            studio.setup();
        }
    };

    return module;
});