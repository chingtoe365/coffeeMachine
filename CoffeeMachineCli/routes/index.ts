/*
 * GET home page.
 */
import express = require('express');
import { Response } from '_debugger';
const router = express.Router();

var http = require('http');
var bodyParser = require('body-parser');
var urlencodedParser = bodyParser.urlencoded({ extended: false });
const querystring = require('querystring');   


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
    var updated = false;
    if (req.query.updated) {
        updated = req.query.updated;
    }
    getJSON(getOptions, function (err, result) {
        if (err) {
            return console.log('Error while trying get order logs: ', err);
        }
        res.render('index', {
            title: 'Coffee Star',
            order: result,
            updated: updated
        })
    });
});

router.post('/submit-form-with-post', urlencodedParser, function(req, res){
    var postData = querystring.stringify({ 'Drink': req.body.drink, 'SugarAmount': req.body.sugarAmount, 'OwnMug': req.body.ownMug });
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
        resp.on('data', (d) => {
            process.stdout.write(d);
        });
        resp.on('end', (e) => {
            return res.redirect('/?updated=true');
        })
    });

    postReq.on('error', (e) => {
        console.error(e);
        return console.log('Error while trying post new order: ', e);
    });

    postReq.write(postData);
    postReq.end();
});


export default router;