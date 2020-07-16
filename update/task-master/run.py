import requests,os,json,time,random,queue
from build import Constructor,Producer,Consumer
import asyncio

def run():
    global Queue
    Queue = queue.Queue(10000)
    p = Producer(Queue)
    p.run()
    time.sleep(5)
    for x in range(0,5):
        consumer = Consumer(p)
        consumer.start()


if __name__ == "__main__":
    run()