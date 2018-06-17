/*
 * GET home page.
 */
import express = require('express');
import { Response } from '_debugger';
const router = express.Router();

var http = require('http');
//var request = require('request');
var bodyParser = require('body-parser');


function getJSON(getOptions, callback) {
    http.request(getOptions, function (res) {
        var body = '';
        res.on('data', function (chunk) {
            body += chunk;
        });
        res.on('end', function (err) {
            var lastOrder = JSON.parse(body);
            callback(null, {
                drink: lastOrder.Drink,
                sugarQty: lastOrder.SugarAmount,
                ownMug: lastOrder.OwnMug
            });
        });
    }).end();
}

var getOptions = {
    host: 'localhost',
    port: 62431,
    path: '/api/Order',
    method: 'GET'
}

router.get('/', (req: express.Request, res: express.Response) => {
    console.log(res);
    getJSON(getOptions, function (err, result) {
        if (err) {
            return console.log('Error while trying get order logs: ', err);
        }
        res.render('index', {
            title: 'Coffee Star',
            order: result
        })
    });
});



var urlencodedParser = bodyParser.urlencoded({ extended: false });


const querystring = require('querystring');   

router.post('/submit-form-with-post', urlencodedParser, function(req, res){
    // insert order and redirect to fire get-last-order get request
    //function callback(err) {
    //    if (err) {
    //        return console.log('Error while trying post new order: ', err);
    //    }
    //    return res.redirect('/');
    //}
    //function showUpdateTagCallBack() {
    //    res.
    //}
    var postData = querystring.stringify({ 'Drink': req.body.drink, 'SugarAmount': req.body.sugarAmount, 'OwnMug': req.body.ownMug });
    //console.log(postData);
    var options = {
        hostname: 'localhost',
        port: 62431,
        path: '/api/Order',
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'Content-Length': postData.length
        }
    };
    var postReq = http.request(options, (resp) => {
        //console.log('statusCode:', resp.statusCode);
        //console.log('headers:', resp.headers);

        resp.on('data', (d) => {
            process.stdout.write(d);
        });
        resp.on('end', (e) => {
            //res.on('finish', function () {
            //    showUpdateTagCallBack();
            //});
            return res.redirect('/');
            //return callback(null);
        })
    });

    postReq.on('error', (e) => {
        console.error(e);
        return console.log('Error while trying post new order: ', e);
    });

    postReq.write(postData);
    postReq.end();
    //return res.redirect('/');
});


export default router;