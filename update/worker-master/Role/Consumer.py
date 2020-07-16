import threading,queue,time,random,json,requests
from runner import Baidu,M360,Mbaidu,Msogou,PC360,Sogou
from base import Browser,getConfig,random_pc_resolution,random_pc_ua,random_m_ua,random_m_resolution
from proxy import get_proxy

class Consumer(threading.Thread):
	"""docstring for Consumer"""
	def __init__(self):
		threading.Thread.__init__(self)
		self.queue = queue
		self.api_server = "http://api.shengkuaipai.com"
		self.api_token = "e09b4497f482714d1b7e4460c9333cad"

	def run(self):
		# self.ua = random_pc_ua()
		# self.proxy = get_proxy(0)
		# self.resolution = random_pc_resolution()
		# baidu = Mbaidu("SEO优化","www.shengkuaipai.com",self.proxy["ip"],self.ua,self.resolution,"")
		# baidu.Site_utils([10,20])
		browser = Browser()
		while True:
			task = self.taskGet()
			print(task)
			try:
				if task['code'] != 200:
					browser.log("no task delivery, retry after 5s")
					time.sleep(5)
					continue
				else:
					self.proxy = get_proxy(0)
					if task['platform'] == 0:
						self.ua = random_pc_ua()
						self.resolution = random_pc_resolution()
						baidu = Baidu(task['keyword'],task['domain'], self.proxy['ip'], self.ua, self.resolution,task['title'])
						if task['mode'] == 0:
							status = baidu.Normal(task['times'])
						if task['mode'] == 1:
							status = baidu.Direct(task['times'])
						if (task['mode'] == 2 or task['mode'] == 4):
							status = baidu.Create_keyword(task['optimization'],task['times'])
						if task['mode'] == 3:
							status = baidu.Send_packages(task['times'])
						if task['mode'] == 5:
							status = baidu.Site_search(task['times'])
						if task['mode'] == 6:
							status = baidu.Site_utils(task['times'])
						if task['mode'] == 7:
							status = baidu.Post(task['times'])
						 # status = baidu(task['keyword'], task['domain'], proxy, ua, resolution, task['times'])
					if task['platform'] == 1:
						self.ua = random_pc_ua()
						self.resolution = random_pc_resolution()
						sogou = Sogou(task['keyword'],task['domain'], self.proxy['ip'], self.ua, self.resolution,task['title'])
						if task['mode'] == 0:
							status = sogou.Normal(task['times'])
						if task['mode'] == 1:
							status = sogou.Direct(task['times'])
						if (task['mode'] == 2 or task['mode'] == 4):
							status = sogou.Create_keyword(task['optimization'],task['times'])
						if task['mode'] == 5:
							status = sogou.Site_search(task['times'])
						if task['mode'] == 7:
							status = sogou.Post(task['times'])
						 # status = sogou(task['keyword'], task['domain'], proxy, ua, resolution, task['times'])
					if task['platform'] == 2:
						self.ua = random_pc_ua()
						self.resolution = random_pc_resolution()
						pc360 = PC360(task['keyword'],task['domain'], self.proxy['ip'], self.ua, self.resolution,task['title'])
						if task['mode'] == 0:
							status = pc360.Normal(task['times'])
						if task['mode'] == 1:
							status = pc360.Direct(task['times'])
						if (task['mode'] == 2 or task['mode'] == 4):
							status = pc360.Create_keyword(task['optimization'],task['times'])
						if task['mode'] == 5:
							status = pc360.Site_search(task['times'])
						if task['mode'] == 7:
							status = pc360.Post(task['times'])
						 # status = pc360(task['keyword'], task['domain'], proxy, ua, resolution, task['times'])
					if task['platform'] == 3:
						self.ua = random_m_ua()
						self.resolution = random_m_resolution()
						mbaidu = Mbaidu(task['keyword'],task['domain'], self.proxy['ip'], self.ua, self.resolution,task['title'])
						if task['mode'] == 0:
							status = mbaidu.Normal(task['times'])
						if task['mode'] == 1:
							status = mbaidu.Direct(task['times'])
						if (task['mode'] == 2 or task['mode'] == 4):
							status = mbaidu.Create_keyword(task['optimization'],task['times'])
						if task['mode'] == 5:
							status = mbaidu.Site_search(task['times'])
						if task['mode'] == 6:
							status = baidu.Site_utils(task['times'])
						if task['mode'] == 7:
							status = mbaidu.Post(task['times'])
						 # status = mbaidu(task['keyword'], task['domain'], proxy, ua, resolution, task['times'])
					if task['platform'] == 4:
						self.ua = random_m_ua()
						self.resolution = random_m_resolution()
						msogou = Msogou(task['keyword'],task['domain'], self.proxy['ip'], self.ua, self.resolution,task['title'])
						if task['mode'] == 0:
							status = msogou.Normal(task['times'])
						if task['mode'] == 1:
							status = msogou.Direct(task['times'])
						if (task['mode'] == 2 or task['mode'] == 4):
							status = msogou.Create_keyword(task['optimization'],task['times'])
						if task['mode'] == 5:
							status = msogou.Site_search(task['times'])
						if task['mode'] == 7:
							status = msogou.Post(task['times'])
						 # status = msogou(task['keyword'], task['domain'], proxy, ua, resolution, task['times'])
					if task['platform'] == 5:
						self.ua = random_m_ua()
						self.resolution = random_m_resolution()
						m360 = M360(task['keyword'],task['domain'], self.proxy['ip'], self.ua, self.resolution,task['title'])
						if task['mode'] == 0:
							status = m360.Normal(task['times'])
						if task['mode'] == 1:
							status = m360.Direct(task['times'])
						if (task['mode'] == 2 or task['mode'] == 4):
							status = m360.Create_keyword(task['optimization'],task['times'])
						if task['mode'] == 5:
							status = m360.Site_search(task['times'])
						if task['mode'] == 7:
							status = m360.Post(task['times'])
						 # status = m360(task['keyword'], task['domain'], proxy, ua, resolution, task['times'])
			except Exception as e:
				print(str(e))
			finally:
				browser.log("upload task result")
				if task["code"] != 200:
					pass
				else:
					try:
						requests.post(url="%s/api/task/report" % self.api_server, headers={'Content-Type': 'application/json','Connection':'close'},json={'token': self.api_token, 'ua': self.ua, 'proxy': self.proxy['outip'], 'resolution': self.resolution,'status': 4, 'id': task['id']})
					except Exception as e:
						print(str(e))
						
	def taskGet(self):
		resp = requests.post(url="%s/api/task/check" % self.api_server, json={'Token': self.api_token},
					headers={'Content-Type': 'application/json'})
		return	json.loads(resp.text)