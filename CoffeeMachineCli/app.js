"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var debug = require("debug");
var express = require("express");
var path = require("path");
var index_1 = require("./routes/index");
var user_1 = require("./routes/user");
var app = express();
//var http = require('http');
// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'pug');
app.use(express.static(path.join(__dirname, 'public')));
app.use('/', index_1.default);
app.use('/users', user_1.default);
//app.get('/', function (req, res) {
//    return res.redirect('/get-last-order');
//});
//app.get('/get-last-order', function (req, res) {
//    // fetch last order from API
//    //req.baseUrl = "http://localhost:62431/";
//    var order;
//    getJSON(getOptions, function (err, result) {
//        if (err) {
//            return console.log('Error while trying get order logs: ', err);
//        }
//        //console.log(result);
//        order = result;
//        order = {
//            "drink": "coffee",
//            "sugarQty": "220",
//            "ownMug": "yes"
//        };
//    });
//    //req.
//    return res.render('index', {
//        order: order
//    });
//});
//app.post('/submit-form-with-post', function (req, res) {
//    // insert order and redirect to fire get-last-order get request
//    //res.send();
//    return res.redirect('/get-last-order');
//});
// catch 404 and forward to error handler
app.use(function (req, res, next) {
    var err = new Error('Not Found');
    err['status'] = 404;
    next(err);
});
// error handlers
// development error handler
// will print stacktrace
if (app.get('env') === 'development') {
    app.use(function (err, req, res, next) {
        res.status(err['status'] || 500);
        res.render('error', {
            message: err.message,
            error: err
        });
    });
}
// production error handler
// no stacktraces leaked to user
app.use(function (err, req, res, next) {
    res.status(err.status || 500);
    res.render('error', {
        message: err.message,
        error: {}
    });
});
app.set('port', process.env.PORT || 3000);
var server = app.listen(app.get('port'), function () {
    debug('Express server listening on port ' + server.address().port);
});
//function getJSON(getOptions, cb) {
//    http.request(getOptions, function (res) {
//        var body = '';
//        res.on('data', function (chunk) {
//            body += chunk;
//        });
//        res.on('end', function () {
//            var lastOrder = JSON.parse(body);
//            console.log(lastOrder);
//        })
//    }).end();
//}
//var getOptions = {
//    host: 'localhost',
//    port: 62431,
//    path: '/api/Order',
//    method: 'GET'
//}
//# sourceMappingURL=app.js.map