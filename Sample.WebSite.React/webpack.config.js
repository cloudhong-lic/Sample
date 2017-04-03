var webpack = require('webpack');
var path = require('path');

var APP_DIR = path.resolve(__dirname, 'src/scripts');

var config = {
    entry: APP_DIR + '/index.jsx',
    output: {
        filename: "./dist/scripts/bundle.js"
    },
    module: {
        loaders: [{
            test: /\.jsx?$/,
            loader: "babel-loader",
            exclude: /node_modules/,
            query: {
                presets: ['es2015', 'react']
            }
        }]
    }
};

module.exports = config;