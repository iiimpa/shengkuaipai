import os

class Utils(object):
	"""docstring for Utils"""
	def __init__(self):
		super(Utils, self).__init__()

	def getPID(self,commandName):
		command = 'ps aux' #可以直接在命令行中执行的命令
		r = os.popen(command) #执行该命令
		info = r.readlines() #读取命令行的输出到一个list
		auxs = []
		for line in info: #按行遍历
			line = line.strip('\r\n')
			print(line)
			result = line.split()
			item = {"PID":result[0],"PPID":result[1],"PGID":result[2],"WINPID":result[3],"TTY":result[4],"UID":result[5],"STIME":result[6],"COMMAND":result[7]}
			auxs.append(item)
		for aux in auxs:
			if aux['COMMAND'] == commandName:
				return aux['PID']
		return 0

	def getAuxStatus(self,commandName):
		command = 'ps aux' #可以直接在命令行中执行的命令
		r = os.popen(command) #执行该命令
		info = r.readlines() #读取命令行的输出到一个list
		auxs = []
		for line in info: #按行遍历
			line = line.strip('\r\n')
			print(line)
			result = line.split()
			item = {"PID":result[0],"PPID":result[1],"PGID":result[2],"WINPID":result[3],"TTY":result[4],"UID":result[5],"STIME":result[6],"COMMAND":result[7]}
			auxs.append(item)
		for aux in auxs:
			if aux['COMMAND'] == commandName:
				return True
		return False
