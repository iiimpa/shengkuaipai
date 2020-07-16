import os,time,json,datetime,random

class Constructor(object):
	"""docstring for ClassName"""
	def __init__(self, data,token):
		super(Constructor, self).__init__()
		self.times = json.loads(data['times'])
		self.id = data['id']
		self.group = data['group_id']
		self.status = data['status']
		self.current_rank = data['current_rank']
		self.platform = data['platform']
		self.start_rank = data['start_rank']
		self.percentage = json.loads(data['percentage'])
		self.keyword = data['keyword']
		self.task_create_time = data['task_create_time']
		self.randomJumpCount = json.loads(data['randomJumpCount'])
		self.create_time = data['create_time']
		self.domain = data['domain']
		self.clickLimtCount = json.loads(data['clickLimtCount'])
		self.randomWaitCount = json.loads(data['randomWaitCount'])
		self.serial_no = data['serial_no']
		self.amount = data['amount']
		self.create_status = data['create_status']
		self.update_time = data['update_time']
		self.xiongzhang = data['xiongzhang']
		self.increase = json.loads(data['increase'])
		self.days = data['days']
		self.user_id = data['user_id']
		self.token = token
		self.tasks = []

	#构建任务
	def createTask(self):
		if(self.clickLimtCount['min'] != '0' or self.clickLimtCount['max'] != '0'):
			click_times = random.randint(int(self.clickLimtCount['min']),int(self.clickLimtCount['max']))
			self.exeCondi('%Y-%m-%d 00:00:00','%Y-%m-%d 23:59:59',click_times)
		else:
			for x in self.times.items():
				#t0执行条件
				if(x[0] == 't0'):
					self.exeCondi('%Y-%m-%d 00:00:00','%Y-%m-%d 00:59:59',int(x[1]))
				#t1执行条件
				if(x[0] == 't1'):
					self.exeCondi('%Y-%m-%d 01:00:00','%Y-%m-%d 01:59:59',int(x[1]))
				#t2执行条件
				if(x[0] == 't2'):
					self.exeCondi('%Y-%m-%d 02:00:00','%Y-%m-%d 02:59:59',int(x[1]))
				#t3执行条件
				if(x[0] == 't3'):
					self.exeCondi('%Y-%m-%d 03:00:00','%Y-%m-%d 03:59:59',int(x[1]))
				#t4执行条件
				if(x[0] == 't4'):
					self.exeCondi('%Y-%m-%d 04:00:00','%Y-%m-%d 04:59:59',int(x[1]))
				#t5执行条件
				if(x[0] == 't5'):
					self.exeCondi('%Y-%m-%d 05:00:00','%Y-%m-%d 05:59:59',int(x[1]))
				#t6执行条件
				if(x[0] == 't6'):
					self.exeCondi('%Y-%m-%d 06:00:00','%Y-%m-%d 06:59:59',int(x[1]))
				#t7执行条件
				if(x[0] == 't7'):
					self.exeCondi('%Y-%m-%d 07:00:00','%Y-%m-%d 07:59:59',int(x[1]))
				#t8执行条件
				if(x[0] == 't8'):
					self.exeCondi('%Y-%m-%d 08:00:00','%Y-%m-%d 08:59:59',int(x[1]))
				#t9执行条件
				if(x[0] == 't9'):
					self.exeCondi('%Y-%m-%d 09:00:00','%Y-%m-%d 09:59:59',int(x[1]))
				#t10执行条件
				if(x[0] == 't10'):
					self.exeCondi('%Y-%m-%d 10:00:00','%Y-%m-%d 10:59:59',int(x[1]))
				#t11执行条件
				if(x[0] == 't11'):
					self.exeCondi('%Y-%m-%d 11:00:00','%Y-%m-%d 11:59:59',int(x[1]))
				#t12执行条件
				if(x[0] == 't12'):
					self.exeCondi('%Y-%m-%d 12:00:00','%Y-%m-%d 12:59:59',int(x[1]))
				#t13执行条件
				if(x[0] == 't13'):
					self.exeCondi('%Y-%m-%d 13:00:00','%Y-%m-%d 13:59:59',int(x[1]))
				#t14执行条件
				if(x[0] == 't14'):
					self.exeCondi('%Y-%m-%d 14:00:00','%Y-%m-%d 14:59:59',int(x[1]))
				#t15执行条件
				if(x[0] == 't15'):
					self.exeCondi('%Y-%m-%d 15:00:00','%Y-%m-%d 15:59:59',int(x[1]))
				#t16执行条件
				if(x[0] == 't16'):
					self.exeCondi('%Y-%m-%d 16:00:00','%Y-%m-%d 16:59:59',int(x[1]))
				#t17执行条件
				if(x[0] == 't17'):
					self.exeCondi('%Y-%m-%d 17:00:00','%Y-%m-%d 17:59:59',int(x[1]))
				#t18执行条件
				if(x[0] == 't18'):
					self.exeCondi('%Y-%m-%d 18:00:00','%Y-%m-%d 18:59:59',int(x[1]))
				#t19执行条件
				if(x[0] == 't19'):
					self.exeCondi('%Y-%m-%d 19:00:00','%Y-%m-%d 19:59:59',int(x[1]))
				#t20执行条件
				if(x[0] == 't20'):
					self.exeCondi('%Y-%m-%d 20:00:00','%Y-%m-%d 20:59:59',int(x[1]))
				#t21执行条件
				if(x[0] == 't21'):
					self.exeCondi('%Y-%m-%d 21:00:00','%Y-%m-%d 21:59:59',int(x[1]))
				#t22执行条件
				if(x[0] == 't22'):
					self.exeCondi('%Y-%m-%d 22:00:00','%Y-%m-%d 22:59:59',int(x[1]))
				#t23执行条件
				if(x[0] == 't23'):
					self.exeCondi('%Y-%m-%d 23:00:00','%Y-%m-%d 23:59:59',int(x[1]))

	#构建任务参数并添加至任务集合
	def createParams(self,time_begin,time_over):
		jump_times = random.randint(int(self.randomJumpCount['min']),int(self.randomJumpCount['max']))
		stay_time = []
		for x in range(0,jump_times):
			stay_time.append(random.randint(int(self.randomWaitCount['min']),int(self.randomWaitCount['max'])))
		cost = 1
		if(jump_times > 4):
			cost += 1+(jump_times-4)*0.5
		else:
			cost += 1
		self.tasks.append({'token':self.token,'order_id':int(self.id),'status':0,'schedule_time':str(self.randomtimes(time_begin,time_over)[0]),'cost':cost,'domain':self.domain,'platform':int(self.platform),'jump_times':jump_times,'stay_time':stay_time,'keyword':self.keyword,'xiongzhang':self.xiongzhang})

	#随机生成时间
	def randomtimes(self,start, end, count=1, format="%Y-%m-%d %H:%M:%S"):
		stime = datetime.datetime.strptime(start, format)
		etime = datetime.datetime.strptime(end, format)
		return [random.random() * (etime - stime) + stime for _ in range(count)]

	def exeCondi(self,time_begin,time_over,times):
		time_begin = time.strftime("%s" % time_begin,time.localtime(time.time()))
		time_over = time.strftime("%s" % time_over,time.localtime(time.time()))
		time_now = time.strftime("%Y-%m-%d %H:%M:%S",time.localtime(time.time()))
		if(time_over<time_now):
			return
		else:
			if (time_begin < time_now):
				time_begin = time_now
		for x in range(0,times):
			self.createParams(time_begin,time_over)