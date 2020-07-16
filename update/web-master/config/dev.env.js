'use strict'
const merge = require('webpack-merge')
const prodEnv = require('./prod.env')

module.exports = merge(prodEnv, {
  NODE_ENV: '"development"',
  SERVER_URL:'"http://fei.shengkuaipai.com"'
  // SERVER_URL:'"http://api.shengkuaipai.com"'
})
