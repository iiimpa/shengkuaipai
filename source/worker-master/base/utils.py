import pytz
import datetime
import os
import time
import random


# 打印log
def log(str):
    tz = pytz.timezone('Asia/Shanghai')
    print("[%s] %s" % (datetime.datetime.now(tz).strftime("%Y-%m-%d %H:%M:%S"), str))


# 创建x11桌面环境
def start_xvfb(resolution):
    os.system("Xvfb :99 -screen 0 %sx16 &" % resolution)


# 杀死x11桌面环境
def stop_xvfb():
    os.system("pkill Xvfb")


# 使用指定分辨率创建x11环境
def recreate_x11(resolution):
    stop_xvfb()
    log("Close X11 Xvfb...")
    time.sleep(3)
    start_xvfb(resolution)
    log("Create X11 Session with %s" % resolution)
