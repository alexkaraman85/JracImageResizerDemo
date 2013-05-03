$(function () {
    var $form = $('#js-image-settings');
    
    $('img').jrac({
        'viewport_resize': false
        , 'viewport_width': 650
        , 'viewport_height': 650
        , 'zoom_max': 650
        , 'viewport_onload': function () {
        var $viewport = this;
        var inputs = $form.find('input:text');
        var events = [
            'jrac_crop_x'
            , 'jrac_crop_y'
            , 'jrac_crop_width'
            , 'jrac_crop_height'
            , 'jrac_image_width'
            , 'jrac_image_height'
        ];

        for (var i = 0; i < events.length; i++) {
            var eventName = events[i];
            $viewport.observator.register(eventName, inputs.eq(i));
            inputs
                .eq(i)
                .bind(eventName, function (event, $vp, value) { $(this).val(value); })
                .change(eventName, function(event) {
                    $viewport.observator.set_property(event.data, $(this).val());
                });
        }
    }
    });
});
