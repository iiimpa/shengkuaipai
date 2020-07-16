import requests


def get_proxy():
    url = "http://http.tiqu.alicdns.com/getip3?num=1&type=2&pro=&city=0&yys=0&port=1&time=1&ts=0&ys=0&cs=1&lb=1&sb=0&pb=4&mr=2&regions=&gm=4"
    response = requests.get(url)
    jobj = response.json()
    if jobj['success']:
        return "%s:%d" % (jobj['data'][0]['ip'], jobj['data'][0]['port'])
    else:
        return False