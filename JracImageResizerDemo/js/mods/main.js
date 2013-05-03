require.config({
    paths: {
        jquery: '../libs/jquery'
        , jqueryui: '../libs/jquery-ui'
        , bootstrap: '../libs/bootstrap'
        , jrac: '../libs/jrac/jquery.jrac'
    }
    , waitSeconds: 150
    , shim: {
        'bootstrap': ['jquery']
        , 'jqueryui': ['jquery']
        , 'jrac': ['jquery']
    }
});

require([
    'loader'
], function (loader) {
    loader.init();
});
