import requests,re,time

def find_urls(target,domain,ua):
	time.sleep(1)
	resp = requests.get(target)
	resp.encoding = resp.apparent_encoding
	links = re.findall(r"http[s]?://(?:[a-zA-Z]|[0-9]|[$-_@.&+]|[!*\(\),]|(?:%[0-9a-fA-F][0-9a-fA-F]))+",resp.text)
	for link in links:
		print(link)
		if link == None:
			continue
		if domain in link:
			return True
		return False