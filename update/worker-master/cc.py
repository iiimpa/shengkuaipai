import requests,re

# session = requests.session()
# files = {'file':'cc'}
# resp = session.get("http://tj.00s4.com/",files=files)
# print(resp.text)
file = '.手机UA.txt'
uas = open(file=file,mode='r',encoding='utf8')
print(uas.readlines()[0])
# newFile = 'C:\\Users\\iiimp\\Desktop\\0a08a035-2cf1-49d2-9f09-cb3cdef7c738.txt'
# stream = open(file=newFile,mode='a+')
# for ua in uas.readlines():
#     ua = ua.strip("\n")
#     stream.write("\"%s\"," % ua)