from Role import Producer,Consumer
from base import Browser,getConfig,writeConfig
from urllib import parse
import time, os, json,requests,queue

if __name__ == '__main__':
    for x in range(0,int(getConfig('Thread')['max_count'])):
        consumer = Consumer()
        consumer.start()
        time.sleep(5)