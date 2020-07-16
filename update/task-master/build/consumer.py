import os,queue,threading,requests,json,time
from .constructor import Constructor
from .producer import Producer

class Consumer(threading.Thread):
    def __init__(self,p):
        threading.Thread.__init__(self)
        self.p = p
        self.api_server = "http://api.shengkuaipai.com"
        self.api_token = "e09b4497f482714d1b7e4460c9333cad"

    def run(self):
        # time.sleep(5)
        while True:
            try:
                if(self.p.emptyQueue()):
                    print("%d:%s" % (threading.currentThread().ident, "暂无任务"))
                    continue
                order = self.p.getQueue()
                print("%d:%s" %(threading.currentThread().ident,order))
                self.refreshRank(order)
                tasks = self.createTask(order)
                self.uploadTasks(tasks)
                self.reportOrder(order)
                print("[%s] %s" % (time.strftime('%Y-%m-%d %H:%M:%S', time.localtime(time.time())), "Task created successfully!"))
            except Exception as e:
                print(str(e))
                print("[%s] %s" % (time.strftime('%Y-%m-%d %H:%M:%S', time.localtime(time.time())),
                                   "There is no order to generate task, try again in 5 seconds!"))
            finally:
                time.sleep(5)
    # 上报订单
    def reportOrder(self,order):
        response = requests.post(url="%s/public/order/report" % self.api_server,
                                 json={'token': self.api_token, 'order_id': int(order['id'])},
                                 headers={'Content-Type': 'application/json'})

    # 构建任务
    def createTask(self,order):
        creater = Constructor(order, self.api_token)
        creater.createTask()
        return creater.tasks

    # 上传任务
    def uploadTasks(self, data):
        response = requests.post(url="%s/public/task/add" % self.api_server, json=data,
                                 headers={'Content-Type': 'application/json'})

    # 刷新排名
    def refreshRank(self,order):
        try:
            data = {"id": int(order['id'])}
            resp = requests.post(url="%s/public/rank/get" % self.api_server, json=data,
                                 headers={'Content-Type': 'application/json'})
            print(json.loads(resp.text)['message'])
        except Exception as e:
            print(str(e))