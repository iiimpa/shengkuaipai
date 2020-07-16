import requests,os,json,time,random
from build import Constructor,Task

class Runner(object):
	"""docstring for ClassName"""
	def __init__(self):
		super(Runner, self).__init__()
		# self.api_token = os.getenv("AIP_TOKEN")
		# self.api_server = os.getenv("API_SERVER")
		self.api_token = "e09b4497f482714d1b7e4460c9333cad"
		self.api_server = "http://api.shengkuaipai.com"

	#获取订单
	def getOrder(self):
		response = requests.post(url="%s/public/order/get" % self.api_server, json={'token': self.api_token},
			headers={'Content-Type': 'application/json'})
		self.data = json.loads(response.text)['data']

	#上报订单
	def reportOrder(self,price,user_id):
		response = requests.post(url="%s/public/order/report" % self.api_server, json={'token': self.api_token,'price':price,'id':int(self.data['id'])},
			headers={'Content-Type': 'application/json'})

	#构建任务
	def createTask(self):
		creater = Constructor(self.data,self.api_token)
		creater.createTask()
		return creater.tasks

	#计算价格
	def totalPrice(self,tasks):
		price = 0.00
		for task in tasks:
			for x in task.items():
				if(x[0] == 'cost'):
					price += float(x[1])
		return price

	#扣费
	def Charging(self,price):
		data = {'token':self.api_token,'price':price,'user_id':self.data['user_id']}
		response = requests.post(url="%s/public/charging" % self.api_server, json=data,
			headers={'Content-Type': 'application/json'})
		if(json.loads(response.text)['message'] == '成功！'):
			return True
		else:
			print(json.loads(response.text)['message'])
			return False

	#上传任务
	def uploadTasks(self,data):
		response = requests.post(url="%s/public/task/add" % self.api_server,json = data, headers={'Content-Type': 'application/json'})

if __name__ == '__main__':
	while True:
		runner = Runner()
		try:
			runner.getOrder()
			print(runner.data)
			tasks = runner.createTask()
			price = runner.totalPrice(tasks)
			if runner.Charging(price):
				runner.uploadTasks(tasks)
				print("[%s] %s" % (time.strftime('%Y-%m-%d %H:%M:%S',time.localtime(time.time())),"Task created successfully!"))
				runner.reportOrder()
		except:
			print("[%s] %s" % (time.strftime('%Y-%m-%d %H:%M:%S',time.localtime(time.time())),"There is no order to generate task, try again in 5 seconds!"))
		finally:
			time.sleep(5) 