import os,requests

class Task(object):
	"""docstring for Task"""
	def __init__(self,order_id,status,schedule_time,cost,create_time,domain,platform,jump_times,stay_time,xiongzhang):
		super(Task, self).__init__()
		self.order_id = order_id
		self.status = status
		self.schedule_time = schedule_time
		self.cost = cost
		self.create_time = create_time
		self.domain = domain
		self.platform = platform
		self.jump_times = jump_time
		self.stay_time = stay_time
		self.xiongzhang = xiongzhang
		self.uploads_url = "http://localhost:5000"

	def uploads(self):
		pass