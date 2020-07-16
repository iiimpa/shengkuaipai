import random,time,json,string
from base import find_urls,Browser,writeConfig,getConfig
from base import Utils

class Baidu(Browser):
    """docstring for ClassName"""
    def __init__(self,keyword,domain,proxy,ua,resolution,title):
        super(Baidu, self).__init__()
        Browser.__init__(self)
        self.keyword = keyword
        self.domain = domain
        self.proxy = proxy
        self.ua = ua
        self.title = title
        self.resolution = resolution
        self.myweb = Browser.get_chromedriver(self,self.proxy, self.ua,self.resolution)
        try:
          url = "https://www.baidu.com"
          # 打开百度
          self.myweb.get(url=url)
          Browser.log(self,"Begin search：%s" % self.keyword)
          time.sleep(10)
          # 定位到text的文本框
          try:
              self.text = self.myweb.find_element_by_id("kw")
          except:
              self.text = self.myweb.find_element_by_id("index-kw")
        except Exception as e:
          self.myweb.quit()
          Browser.log(self,"Proxy ip exception,chromedriver is closed...")
          Browser.close_display(self)
          return 4

    #百度PC普通模式
    def Normal(self,delay):
        try:
           for x in delay:
               url = "https://www.baidu.com"
               # 打开百度
               self.myweb.get(url=url)
               Browser.log(self,"Begin search：%s" % self.keyword)
               time.sleep(10)
               # 定位到text的文本框
               try:
                   self.text = self.myweb.find_element_by_id("kw")
               except:
                   self.text = self.myweb.find_element_by_id("index-kw")
               # 搜索词条
               self.text.send_keys(self.keyword)
               # 定位到搜索键
               button = self.myweb.find_element_by_id("su")
               # 点击搜索键
               button.click()
               Browser.log(self,"Wait for search result...")
               time.sleep(3)
               page = 1
               finded = False
               while page <= 10 and not finded:
                   Browser.log(self,"Search the results... page: %d" % page)
                   time.sleep(10)
                   arr = self.myweb.find_elements_by_class_name('result')
                   for result in arr:
                       try:
                           isClick = False
                           title = result.find_elements_by_xpath("./h3")[0]
                           url = result.find_element_by_class_name("f13").find_element_by_class_name("c-showurl")
                           print(url.text)
                           if self.title != "":
                               if self.title == title.text:
                                   isClick = True
                               else:
                                   isClick = False
                                   continue
                           if self.domain in url.text:
                               isClick = True
                           else:
                               isClick = False
                               continue
                           if isClick:
                               Browser.log(self, "Find target site, Enter ...")
                               try:
                                   title.click()
                                   title.find_element_by_tag_name("a").click()
                               except Exception as e:
                                   pass
                               finded = True
                               break
                       except Exception as e:
                           continue
                   if not finded:
                       Browser.log(self,"Can't find the target site...")
                       next = self.myweb.find_elements_by_xpath("//*[@class=\"page-inner\"]/a")
                       Browser.log(self,"Move to next page...")
                       next[-1].click()
                       page = page + 1
                       time.sleep(5)
               if finded:
                   index = 0
                   Browser.log(self,"Begin to surface the inner page... Seq:%d time: %ds" % (index, x))
                   time.sleep(x)
                   wins = self.myweb.window_handles
                   self.myweb.switch_to.window(wins[-1])
                   arr = self.myweb.find_elements_by_tag_name("a")
                   result = True
                   maxtime = 0
                   while result:
                       try:
                           random.choice(arr).click()
                           result = False
                       except:
                           maxtime += 1
                           if maxtime > 10:
                               result = False
                               continue
                           Browser.log(self,"Unknown error encountered, replacing element...")
                   index = index + 1
                   Browser.log(self,"Finished")
                   continue
               else:
                   continue
        except Exception as e:
           Browser.log(self,"System Error , start new turn...")
           Browser.log(self,str(e))
           return 4
        finally:
           self.myweb.quit()
           Browser.log(self,"chromedriver is closed...")
           Browser.close_display(self)
           return 4

    #百度PC直连模式
    def Direct(self,delay):
        try:
            Browser.log(self,'Start direct connection mode...')
            Browser.log(self,'Start to enter %s' % self.domain)
            self.myweb.get(self.domain)
            Browser.log(self,"Switch to new browser tab...")
            index = 1
            for item in delay:
                Browser.log(self,"Begin to surface the inner page... Seq:%d time: %ds" % (index, item))
                time.sleep(item)
                wins = self.myweb.window_handles
                self.myweb.switch_to.window(wins[-1])
                arr = self.myweb.find_elements_by_tag_name("a")
                result = True
                maxtime = 0
                while result:
                    try:
                        random.choice(arr).click()
                        result = False
                    except:
                        maxtime += 1
                        if maxtime > 10:
                            return 4
                        Browser.log(self,"Unknown error encountered, replacing element...")
                index = index + 1
                Browser.log(self,"Finished")
                return 4
        except Exception as e:
            return 4
        finally:
            self.myweb.quit()
            Browser.log(self,"chromedriver is closed...")
            Browser.close_display(self)
            return 4

    #百度PC造词模式
    def Create_keyword(self,optimization,delay):
          try:
              # 搜索词条
              Browser.log(self,"开始搜索词条")
              self.text.send_keys(optimization)
              # 定位到搜索键
              try:
                  button = self.myweb.find_element_by_id("su")
              except:
                  button = self.myweb.find_element_by_id("index-bn")
              # 点击搜索键
              button.click()
              Browser.log(self,"Wait for search result...")
              time.sleep(3)
              page = 1
              for x in range(1,random.randint(3,5)):
                  Browser.log(self,"Can't find the target site...")
                  next = self.myweb.find_elements_by_xpath("//*[@id=\"page\"]/a")
                  if(len(next)) > 0:
                      Browser.log(self,"Move to next page...")
                      next[-1].click()
                      page = page + 1
                      time.sleep(5)
                  else:
                      time.sleep(10)
              self.text.clear()
              self.text.send_keys(self.keyword)
              # 定位到搜索键
              button = self.myweb.find_element_by_id("su")
              # 点击搜索键
              button.click()
              Browser.log(self,"Wait for search result...")
              time.sleep(3)
              page_other = 1
              for x in range(1,random.randint(3,5)):
                  Browser.log(self,"Can't find the target site...")
                  next = self.myweb.find_elements_by_xpath("//*[@id=\"page\"]/a")
                  Browser.log(self,"Move to next page...")
                  next[-1].click()
                  page_other = page_other + 1
                  time.sleep(5)
              return 4
          except Exception as e:
              Browser.log(self,str(e))
              return 4
          finally:
              self.myweb.quit()
              Browser.log(self,"chromedriver is closed...")
              Browser.close_display(self)
              return 4

    #百度PC发包
    def Send_packages(self,delay):
        try:
           for x in delay:
               url = "https://www.baidu.com/baidu?wd=%s&tn=monline_4_dg&ie=utf-8&et=%s" % (self.keyword,self.domain)
               # 打开百度
               self.myweb.get(url=url)
               Browser.log(self,"Wait for search result...")
               time.sleep(3)
               page = 1
               finded = False
               while page <= 10 and not finded:
                   Browser.log(self,"Search the results... page: %d" % page)
                   time.sleep(10)
                   arr = self.myweb.find_elements_by_class_name('result')
                   for result in arr:
                       try:
                           isClick = False
                           title = result.find_elements_by_xpath("./h3")[0]
                           url = result.find_element_by_class_name("f13").find_element_by_class_name("c-showurl")
                           print(url.text)
                           if self.title != "":
                               if self.title == title.text:
                                   isClick = True
                               else:
                                   isClick = False
                                   continue
                           if self.domain in url.text:
                               isClick = True
                           else:
                               isClick = False
                               continue
                           if isClick:
                               Browser.log(self, "Find target site, Enter ...")
                               try:
                                   title.click()
                                   title.find_element_by_tag_name("a").click()
                               except Exception as e:
                                   pass
                               finded = True
                               break
                       except Exception as e:
                           continue
                   if not finded:
                       Browser.log(self,"Can't find the target site...")
                       next = self.myweb.find_elements_by_xpath("//*[@class=\"page-inner\"]/a")
                       Browser.log(self,"Move to next page...")
                       next[-1].click()
                       page = page + 1
                       time.sleep(5)
               if finded:
                   index = 0
                   Browser.log(self,"Begin to surface the inner page... Seq:%d time: %ds" % (index, x))
                   time.sleep(x)
                   wins = self.myweb.window_handles
                   self.myweb.switch_to.window(wins[-1])
                   arr = self.myweb.find_elements_by_tag_name("a")
                   result = True
                   maxtime = 0
                   while result:
                       try:
                           random.choice(arr).click()
                           result = False
                       except:
                           maxtime += 1
                           if maxtime > 10:
                               result = False
                               continue
                           Browser.log(self,"Unknown error encountered, replacing element...")
                   index = index + 1
                   Browser.log(self,"Finished")
                   continue
               else:
                   continue
        except Exception as e:
           Browser.log(self,"System Error , start new turn...")
           Browser.log(self,str(e))
           return 4
        finally:
           self.myweb.quit()
           Browser.log(self,"chromedriver is closed...")
           Browser.close_display(self)
           return 4

    #et
    def ET(self,optimization,delay):
        pass
        # # 搜索词条
        # self.text.send_keys(optimization)
        # # 定位到搜索键
        # button = self.myweb.find_element_by_id("su")
        # # 点击搜索键
        # button.click()
        # #等待搜索结果
        # Browser.log(self, "Wait for search result...")
        # time.sleep(3)
        # url = self.myweb.current_url
        # et_url = url.replace("utf-8&","utf-8&et=%s&"%self.domain)
        # self.myweb.get(url=et_url)
        # next = self.myweb.find_elements_by_xpath("//*[@id=\"page\"]/a")
        # for x in range(len(next)):
        #     Browser.log(self, "Move to next page...")
        #     next[-1].click()
        #     page = page + 1
        #     time.sleep(5)

    #刷下拉词
    def Select(self,optimization,delay):
        self.Create_keyword(optimization,delay)

    #站内搜索(指令)
    def Site_search(self,delay):
          try:
              for t in delay:
                  url = "https://www.baidu.com"
                  # 打开百度
                  self.myweb.get(url=url)
                  Browser.log(self, "Begin search：%s" % self.keyword)
                  time.sleep(10)
                  # 定位到text的文本框
                  try:
                      self.text = self.myweb.find_element_by_id("kw")
                  except:
                      self.text = self.myweb.find_element_by_id("index-kw")
                  self.text.send_keys("site:%s %s" % (self.domain, self.keyword))
                  button = self.myweb.find_element_by_id("su")
                  # 点击搜索键
                  button.click()
                  Browser.log(self, "Wait for search result...")
                  time.sleep(10)
                  page = 1
                  Browser.log(self,"Search the results... page: %d" % page)
                  time.sleep(10)
                  arr = self.myweb.find_elements_by_class_name('result')
                  for result in arr:
                      try:
                          isClick = False
                          title = result.find_elements_by_xpath("./h3")[0]
                          url = result.find_element_by_class_name("f13").find_element_by_class_name("c-showurl")
                          print(url.text)
                          if self.title != "":
                              if self.title == title.text:
                                  isClick = True
                              else:
                                  isClick = False
                                  continue
                          if self.domain in url.text:
                              isClick = True
                          else:
                              isClick = False
                              continue
                          if isClick:
                              Browser.log(self, "Find target site, Enter ...")
                              try:
                                  title.click()
                                  title.find_element_by_tag_name("a").click()
                              except Exception as e:
                                  pass
                              time.sleep(t)
                              break
                      except Exception as e:
                          continue
              return 4
          except Exception as e:
              Browser.log(self,"System Error , start new turn...")
              time.sleep(5)
              Browser.log(self,str(e))
              return 4
          finally:
              self.myweb.quit()
              Browser.log(self,"chromedriver is closed...")
              Browser.close_display(self)
              return 4

    #站内搜索(工具)
    def Site_utils(self,delay):
          try:
              for t in delay:
                  url = "https://www.baidu.com"
                  # 打开百度
                  self.myweb.get(url=url)
                  Browser.log(self, "Begin search：%s" % self.keyword)
                  time.sleep(5)
                  # 定位到text的文本框
                  try:
                      self.text = self.myweb.find_element_by_id("kw")
                  except:
                      self.text = self.myweb.find_element_by_id("index-kw")
                  # 搜索词条
                  self.text.send_keys(self.keyword)
                  # 定位到搜索键
                  button = self.myweb.find_element_by_id("su")
                  # 点击搜索键
                  button.click()
                  Browser.log(self, "Wait for search result...")
                  time.sleep(5)
                  self.myweb.find_elements_by_xpath('//div[@class="search_tool"]')[0].click()
                  time.sleep(5)
                  self.myweb.find_elements_by_xpath('//span[@class="search_tool_si c-gap-left"]')[0].click()
                  time.sleep(5)
                  text = self.myweb.find_element_by_name('si')
                  text.send_keys(self.domain)
                  submitbtn = self.myweb.find_elements_by_xpath('//a[@class="c-tip-timerfilter-si-submit"]')[0]
                  submitbtn.click()
                  page = 1
                  Browser.log(self,"Search the results... page: %d" % page)
                  arr = self.myweb.find_elements_by_class_name('result')
                  for result in arr:
                      try:
                          isClick = False
                          title = result.find_elements_by_xpath("./h3")[0]
                          url = result.find_element_by_class_name("f13").find_element_by_class_name("c-showurl")
                          print(url.text)
                          if self.title != "":
                              if self.title == title.text:
                                  isClick = True
                              else:
                                  isClick = False
                                  continue
                          if self.domain in url.text:
                              isClick = True
                          else:
                              isClick = False
                              continue
                          if isClick:
                              Browser.log(self, "Find target site, Enter ...")
                              try:
                                  title.click()
                                  title.find_element_by_tag_name("a").click()
                                  time.sleep(t)
                              except Exception as e:
                                  pass
                              break
                      except Exception as e:
                          continue
              return 4
          except Exception as e:
              Browser.log(self, "System Error , start new turn...")
              Browser.log(self, str(e))
              return 4
          finally:
              self.myweb.quit()
              Browser.log(self, "chromedriver is closed...")
              Browser.close_display(self)
              return 4
        # try:
        #    for x in delay:
        #        url = "https://www.baidu.com/baidu?wd=%s&tn=monline_4_dg&ie=utf-8&si=%s&ct=2097152" % (self.keyword,self.domain)
        #        # 打开百度
        #        self.myweb.get(url=url)
        #        Browser.log(self,"Wait for search result...")
        #        time.sleep(3)
        #        page = 1
        #        finded = False
        #        while page <= 10 and not finded:
        #            Browser.log(self,"Search the results... page: %d" % page)
        #            time.sleep(10)
        #            arr = self.myweb.find_elements_by_class_name('result')
        #            for result in arr:
        #                try:
        #                    isClick = False
        #                    title = result.find_elements_by_xpath("./h3")[0]
        #                    url = result.find_element_by_class_name("f13").find_element_by_class_name("c-showurl")
        #                    print(url.text)
        #                    if self.title != "":
        #                        if self.title == title.text:
        #                            isClick = True
        #                        else:
        #                            isClick = False
        #                            continue
        #                    if self.domain in url.text:
        #                        isClick = True
        #                    else:
        #                        isClick = False
        #                        continue
        #                    if isClick:
        #                        Browser.log(self, "Find target site, Enter ...")
        #                        try:
        #                            title.click()
        #                            title.find_element_by_tag_name("a").click()
        #                        except Exception as e:
        #                            pass
        #                        finded = True
        #                        break
        #                except Exception as e:
        #                    continue
        #            if not finded:
        #                Browser.log(self,"Can't find the target site...")
        #                next = self.myweb.find_elements_by_xpath("//*[@class=\"page-inner\"]/a")
        #                Browser.log(self,"Move to next page...")
        #                next[-1].click()
        #                page = page + 1
        #                time.sleep(5)
        #        if finded:
        #            index = 0
        #            Browser.log(self,"Begin to surface the inner page... Seq:%d time: %ds" % (index, x))
        #            time.sleep(x)
        #            wins = self.myweb.window_handles
        #            self.myweb.switch_to.window(wins[-1])
        #            arr = self.myweb.find_elements_by_tag_name("a")
        #            result = True
        #            maxtime = 0
        #            while result:
        #                try:
        #                    random.choice(arr).click()
        #                    result = False
        #                except:
        #                    maxtime += 1
        #                    if maxtime > 10:
        #                        result = False
        #                        continue
        #                    Browser.log(self,"Unknown error encountered, replacing element...")
        #            index = index + 1
        #            Browser.log(self,"Finished")
        #            continue
        #        else:
        #            continue
        # except Exception as e:
        #    Browser.log(self,"System Error , start new turn...")
        #    Browser.log(self,str(e))
        #    return 4
        # finally:
        #    self.myweb.quit()
        #    Browser.log(self,"chromedriver is closed...")
        #    Browser.close_display(self)
        #    return 4

    def Post(self,delay):
        try:
           for x in delay:
               url = "https://www.baidu.com/baidu?wd=%s&tn=monline_4_dg&ie=utf-8&si=%s&ct=2097153" % (self.keyword,self.domain)
               # 打开百度
               self.myweb.get(url=url)
               Browser.log(self,"Wait for search result...")
               time.sleep(3)
               page = 1
               finded = False
               while page <= 10 and not finded:
                   Browser.log(self,"Search the results... page: %d" % page)
                   time.sleep(10)
                   arr = self.myweb.find_elements_by_class_name('result')
                   for result in arr:
                       try:
                           isClick = False
                           title = result.find_elements_by_xpath("./h3")[0]
                           url = result.find_element_by_class_name("f13").find_element_by_class_name("c-showurl")
                           print(url.text)
                           if self.title != "":
                               if self.title == title.text:
                                   isClick = True
                               else:
                                   isClick = False
                                   continue
                           if self.domain in url.text:
                               isClick = True
                           else:
                               isClick = False
                               continue
                           if isClick:
                               Browser.log(self, "Find target site, Enter ...")
                               try:
                                   title.click()
                                   title.find_element_by_tag_name("a").click()
                               except Exception as e:
                                   pass
                               finded = True
                               break
                       except Exception as e:
                           continue
                   if not finded:
                       Browser.log(self,"Can't find the target site...")
                       next = self.myweb.find_elements_by_xpath("//*[@class=\"page-inner\"]/a")
                       Browser.log(self,"Move to next page...")
                       next[-1].click()
                       page = page + 1
                       time.sleep(5)
               if finded:
                   index = 0
                   Browser.log(self,"Begin to surface the inner page... Seq:%d time: %ds" % (index, x))
                   time.sleep(x)
                   wins = self.myweb.window_handles
                   self.myweb.switch_to.window(wins[-1])
                   arr = self.myweb.find_elements_by_tag_name("a")
                   result = True
                   maxtime = 0
                   while result:
                       try:
                           random.choice(arr).click()
                           result = False
                       except:
                           maxtime += 1
                           if maxtime > 10:
                               result = False
                               continue
                           Browser.log(self,"Unknown error encountered, replacing element...")
                   index = index + 1
                   Browser.log(self,"Finished")
                   continue
               else:
                   continue
        except Exception as e:
           Browser.log(self,"System Error , start new turn...")
           Browser.log(self,str(e))
           return 4
        finally:
           self.myweb.quit()
           Browser.log(self,"chromedriver is closed...")
           Browser.close_display(self)
           return 4