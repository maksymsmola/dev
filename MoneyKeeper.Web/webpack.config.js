var path = require("path");
var ExtractTextPlugin = require("extract-text-webpack-plugin");
var Clean = require("clean-webpack-plugin");
var distDir = path.join(__dirname, "dist");
var NgAnnotatePlugin = require("ng-annotate-webpack-plugin");
var webpack = require("webpack");

module.exports = {
  entry: {
    app: "./client/app.js"
  },
  output: {
    path: distDir,
    filename: "[name].js"
  },
  devtool: "source-map",
  resolve: {
    modulesDirectories: ["node_modules"],
    extensions: ["", ".js"]
  },
  watchOptions: {
    aggregateTimeout: 100
  },
  module: {
    loaders: [{
      test: /\.(png|jpg|gif|svg|ttf|eot|woff|woff2)$/,
      loader: "url?name=[path][name].[ext]&limit=10000"
    }, {
      test: /\.(less|css)$/,
      loader: ExtractTextPlugin.extract("style!css?sourceMap!less?sourceMap")
    }, {
      test: /\.html$/,
      loader: "ngtemplate!html"
    }]
  },
  plugins: [
    new Clean([distDir]),
    new ExtractTextPlugin("main.css"),
    new NgAnnotatePlugin({
      add: true
    }),
    // todo: add production webpack.config
    //new webpack.optimize.UglifyJsPlugin({
    //  compress: true
    //})
  ]
}