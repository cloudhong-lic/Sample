var webpack = require('webpack');
var path = require('path');

// base路径
// __dirname为当前文件所在路径
var ENTRY_DIR = path.resolve(__dirname, 'src/scripts');
var OUTPUT_DIR = path.resolve(__dirname, 'dist/scripts');

var config = {
    // 入口文件路径
    entry: ENTRY_DIR + '/index.jsx',

    // 打包输出文件名和路径, 可以不包括在github中
    output: {
        // path: __dirname, 

        // filename: "./dist/scripts/bundle.js"
        filename: OUTPUT_DIR + '/bundle.js'
    },
    module: {
        loaders: [
            // .jsx 文件使用babel-loader来编译处理
            { test: /\.jsx?$/, loader: "babel-loader", exclude: /node_modules/, query: { presets: ['es2015', 'react'] }
        }]
    }
};

module.exports = config;