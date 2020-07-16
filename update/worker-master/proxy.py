import requests,os

def get_proxy(type):
	if type == 0:
		url = "http://t.11jsq.com/index.php/api/entry?method=proxyServer.generate_api_url&packid=0&fa=0&fetch_key=&groupid=0&qty=1&time=101&pro=&city=&port=1&format=json&ss=5&css=&ipport=1&dt=1&specialTxt=3&specialJson=&usertype=2"
	else:
		url = "http://ip.11jsq.com/index.php/api/entry?method=proxyServer.generate_api_url&packid=0&fa=0&fetch_key=&groupid=0&qty=1&time=100&pro=&city=&port=1&format=json&ss=5&css=&ipport=1&dt=1&specialTxt=3&specialJson=&usertype=2"
	response = requests.get(url)
	jobj = response.json()
	print(jobj)
	if jobj['success'] == "true":
		print(jobj["data"][0]["IP"])
		return {
			'ip':"%s" % jobj['data'][0]['IP'],
			'outip':"%s" % jobj['data'][0]['IP']
		}
	else:
		return False

if __name__ == '__main__':
	print(get_proxy(0))