const webpack = require('webpack');
const path = require('path');
const HtmlWebPackPlugin = require('html-webpack-plugin');

module.exports = {    
    devtool: 'source-map',
    entry: path.resolve(__dirname, 'src/Index.tsx'),
    resolve:  {
        extensions: ['.tsx', '.ts', '.js']
    },
    output: {
        path: path.join(__dirname, '../wwwroot'),
        filename: '[name].bundle.[hash].js'
    },
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                loader: "awesome-typescript-loader",
                exclude: [/(node_modules)/],
            }
        ]
    },
    plugins: [
        new HtmlWebPackPlugin({
            template: "./index.html"            
        })
    ]

}