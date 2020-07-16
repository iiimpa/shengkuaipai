import sys
sys.path.append(r'../..runner')
import threading,queue,time,random,json,requests
from runner import Baidu,M360,Mbaidu,Msogou,PC360,Sogou
from base import Browser,getConfig

class Producer(threading.Thread):
	"""docstring for Producer"""
	def __init__(self,queue):
		threading.Thread.__init__(self)
		self.queue = queue
		self.api_server = "http://api.shengkuaipai.com"
		self.api_token = "e09b4497f482714d1b7e4460c9333cad"

	def run(self):
		while True:
			self.Queue_in(self.taskGet())
			time.sleep(5)

	def Queue_out(self):
		return self.queue.get()

	def Queue_in(self,task):
		self.queue.put(task)

	def taskGet(self):
		resp = requests.post(url="%s/api/task/check" % self.api_server, json={'Token': self.api_token},
					headers={'Content-Type': 'application/json'})
		return	json.loads(resp.text)