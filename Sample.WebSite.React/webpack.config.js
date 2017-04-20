// const webpack = require('webpack');
const path = require('path');

const ExtractTextPlugin = require('extract-text-webpack-plugin');

const extractLess = new ExtractTextPlugin({
    // less输出的css文件路径
    // TODO: 如果想得到多个css怎么办?
  filename: './dist/styles/app.css',
});

// base路径
// __dirname为当前文件所在路径
// var ENTRY_DIR = path.resolve(__dirname, 'src/scripts');
// var OUTPUT_DIR = path.resolve(__dirname, 'dist/scripts');

const config = {
    // 入口文件路径
  entry: [
        // ENTRY_DIR + '/main.jsx',
    path.resolve(__dirname, 'src/scripts/main.jsx'),
  ],

    // 打包输出文件名和路径, 可以不包括在github中
  output: {
        // 不能在这里使用path, 如果使用了path, extractLess中的相对路径会受到影响
        // 但是如果不是有多个css文件的话, 又似乎可以考虑将js和css文件全部放到一个dist目录下
        // path: __dirname,

        // 在这里似乎只能使用绝对路径, 不能使用注释掉的那种
        // filename: path.resolve(__dirname, 'dist/scripts/bundle.js')
    filename: './dist/scripts/bundle.js',
  },

  devtool: 'source-map',

  module: {
    loaders: [
            // .jsx 文件使用babel-loader来编译处理
      {
        test: /\.jsx?$/,
        loader: 'babel-loader',
        exclude: /node_modules/,
        query: { presets: ['es2015', 'react'] },
      },

            // LESS
      {
        test: /\.less$/,
        use: extractLess.extract({
          use: [{
            loader: 'css-loader',
          }, {
            loader: 'less-loader',
          }],
                    // use style-loader in development
          fallback: 'style-loader',
        }),
      },
    ],
  },

  plugins: [
    extractLess,
  ],

  resolve: {
        // you can now require('file') instead of require('file.jsx')
        // 在这里设置可省略后缀, 这样在JS文件中就可以import '../styles/app'而不是import '../styles/app.less'了
        // 不过觉得还是把后缀加上比较好, 看着清楚
    extensions: ['.js', '.jsx', '.json', '.less'],
  },
};

module.exports = config;
