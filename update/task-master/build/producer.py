import os,requests,queue,json,threading

class Producer(object):
	"""docstring for Task"""
	def __init__(self,Queue):
		super(Producer, self).__init__()
		self.queue = Queue
		self.api_server = "http://api.shengkuaipai.com"
		self.api_token = "e09b4497f482714d1b7e4460c9333cad"

	def run(self):
		t = threading.Thread(target=self.addQueue)
		t.start()

	#获取订单
	def getOrder(self):
		response = requests.post(url="%s/public/order/get" % self.api_server, json={'token': self.api_token},
			headers={'Content-Type': 'application/json'})
		if(response.text == ""):
			return ""
		return json.loads(response.text)['data']

	#入队
	def addQueue(self):
		while True:
			item = self.getOrder()
			if(item != ""):
				self.queue.put(item)

	#出队
	def getQueue(self):
		return self.queue.get()

	#空队列
	def emptyQueue(self):
		return self.queue.empty()