from base import log
from selenium import webdriver
import zipfile


# 获取配置过的浏览器
def get_chromedriver(proxy=None, ua=None, resolution=None):
    chrome_options = webdriver.ChromeOptions()
    if proxy:
        pluginfile = create_proxy_plugin(proxy)
        chrome_options.add_extension(pluginfile)
    if ua:
        log("Set Browser UA：%s" % ua)
        chrome_options.add_argument('--user-agent=%s' % ua)
    if resolution:
        log("Set Browser resolution：%s" % resolution)
        chrome_options.add_argument('--window-size=%s' % resolution)
        chrome_options.add_argument('--disable-gpu')
        chrome_options.add_argument('lang=zh_CN.UTF-8')
        # chrome_options.add_argument('--headless')
    driver = webdriver.Chrome(options=chrome_options)
    return driver


# 创建代理浏览器代理服务
def create_proxy_plugin(proxy):
    log("Set proxy server：%s" % proxy)
    proxy_arr = proxy.split("@")
    host = ""
    port = ""
    username = ""
    password = ""
    if len(proxy_arr) == 1:
        proxy_dict = proxy_arr[0].split(":")
        host = proxy_dict[0]
        port = proxy_dict[1]
    else:
        proxy_dict2 = proxy_arr[0].split(":")
        proxy_dict1 = proxy_arr[1].split(":")
        host = proxy_dict1[0]
        port = proxy_dict1[1]
        username = proxy_dict2[0]
        password = proxy_dict2[1]
    manifest_json = """
               {
                   "version": "1.0.0",
                   "manifest_version": 2,
                   "name": "Chrome Proxy",
                   "permissions": [
                       "proxy",
                       "tabs",
                       "unlimitedStorage",
                       "storage",
                       "<all_urls>",
                       "webRequest",
                       "webRequestBlocking"
                   ],
                   "background": {
                       "scripts": ["background.js"]
                   },
                   "minimum_chrome_version":"22.0.0"
               }
               """
    background_js = """
               var config = {
                       mode: "fixed_servers",
                       rules: {
                       singleProxy: {
                           scheme: "http",
                           host: "%s",
                           port: parseInt(%s)
                       },
                       bypassList: ["localhost"]
                       }
                   };

               chrome.proxy.settings.set({value: config, scope: "regular"}, function() {});

               function callbackFn(details) {
                   return {
                       authCredentials: {
                           username: "%s",
                           password: "%s"
                       }
                   };
               }

               chrome.webRequest.onAuthRequired.addListener(
                           callbackFn,
                           {urls: ["<all_urls>"]},
                           ['blocking']
               );
               """ % (host, port, username, password)
    pluginfile = 'proxy_auth_plugin.zip'
    with zipfile.ZipFile(pluginfile, 'w') as zp:
        zp.writestr("manifest.json", manifest_json)
        zp.writestr("background.js", background_js)
    return pluginfile
