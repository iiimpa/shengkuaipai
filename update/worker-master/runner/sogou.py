import random,time,json
from base import find_urls,Browser,writeConfig
from selenium.webdriver.common.keys import Keys

class Sogou(Browser):
    """docstring for Sogou"""
    def __init__(self,keyword,domain,proxy,ua,resolution,title):
        super(Sogou, self).__init__()
        Browser.__init__(self)
        self.keyword = keyword
        self.domain = domain
        self.proxy = proxy
        self.title = title
        self.ua = ua
        self.resolution = resolution
        self.myweb = Browser.get_chromedriver(self,self.proxy, self.ua,self.resolution)
        try:
            self.url = "https://www.sogou.com"
            # 打开360PC端
            self.myweb.get(url=self.url)
            Browser.log(self,"Begin search：%s" % self.keyword)
            # 定位到text的文本框
            self.text = self.myweb.find_element_by_id("query")
        except Exception as e:
            self.myweb.quit()
            Browser.log(self,"Proxy ip exception,chromedriver is closed...")
            Browser.close_display(self)
            return 4

    #搜狗PC普通模式
    def Normal(self,delay):
        try:
            for x in delay:
                # 打开360PC端
                self.myweb.get(url=self.url)
                Browser.log(self,"Begin search：%s" % self.keyword)
                # 定位到text的文本框
                try:
                    self.text = self.myweb.find_element_by_id("query")
                except Exception as e:
                    self.text = self.myweb.find_element_by_id("upquery")
                # 搜索词条
                self.text.send_keys(self.keyword)
                # 定位到搜索键
                try:
                    button = self.myweb.find_element_by_id("stb")
                except Exception as e:
                    button = self.myweb.find_element_by_id("searchBtn")
                # 点击搜索键
                button.click()
                Browser.log(self,"Wait for search result...")
                time.sleep(3)
                page = 1
                finded = False
                while page < 10 and not finded:
                    Browser.log(self,"Search the results... page: %d" % page)
                    arr = self.myweb.find_elements_by_class_name("rb")
                    for item in arr:
                        try:
                            title = item.find_element_by_tag_name("h3")
                        except Exception as e:
                            continue
                        url = item.find_element_by_tag_name("cite")
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
                    if not finded:
                        Browser.log(self,"Can't find the target site...")
                        next = self.myweb.find_element_by_id("sogou_next")
                        Browser.log(self,"Move to next page...")
                        next.click()
                        page = page + 1
                        time.sleep(5)
                if finded:
                    Browser.log(self,"Switch to new browser tab...")
                    index = 1
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
            return 4
        except:
            Browser.log(self,"System Error , start new turn...")
            return 4
        finally:
            self.myweb.quit()
            Browser.log(self,"chromedriver is closed...")
            Browser.close_display(self)
            return 4

    #搜狗PC直连模式
    def Direct(self,delay):
        try:
            Browser.log(self,'Start direct connection mode...')
            Browser.log(self,'Start to enter %s' % self.domain)
            self.myweb.get(self.domain)
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
        except Exception as e:
            return 4
        finally:
            self.myweb.quit()
            Browser.log(self,"chromedriver is closed...")
            Browser.close_display(self)
            return 4

    #搜狗PC造词模式
    def Create_keyword(self,optimization,delay):
        try:
            # 搜索词条
            self.text.send_keys(optimization)
            # 定位到搜索键
            button = self.myweb.find_element_by_id("stb")
            time.sleep(3)
            # 点击搜索键
            button.click()
            Browser.log(self,"Wait for search result...")
            time.sleep(3)
            page = 1
            for x in range(1,random.randint(3,5)):
                Browser.log(self,"Can't find the target site...")
                next = self.myweb.find_element_by_id("sogou_next")
                Browser.log(self,"Move to next page...")
                next.click()
                page = page + 1
                time.sleep(5)
            # 搜索词条
            self.text.send_keys(self.keyword)
            # 定位到搜索键
            try:
                self.text = self.myweb.find_element_by_id("query")
            except Exception as e:
                self.text = self.myweb.find_element_by_id("upquery")
            time.sleep(3)
            # 点击搜索键
            button.click()
            Browser.log(self,"Wait for search result...")
            time.sleep(3)
            page = 1
            for x in range(1,random.randint(3,5)):
                Browser.log(self,"Can't find the target site...")
                next = self.myweb.find_element_by_id("sogou_next")
                Browser.log(self,"Move to next page...")
                next.click()
                page = page + 1
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

    #搜狗PC下拉词
    def Select(self,optimization,delay):
        self.Create_keyword(optimization,delay)

    #站内搜索(指令)
    def Site_search(self,delay):
        try:
            for t in delay:
                self.url = "https://www.sogou.com"
                # 打开360PC端
                self.myweb.get(url=self.url)
                Browser.log(self, "Begin search：%s" % self.keyword)
                # 定位到text的文本框
                self.text = self.myweb.find_element_by_id("query")
                self.text.send_keys("site:%s %s" % (self.domain, self.keyword))
                time.sleep(2)
                # 定位到搜索键
                try:
                    button = self.myweb.find_element_by_id("stb")
                except Exception as e:
                    button = self.myweb.find_element_by_id("searchBtn")
                # 点击搜索键
                button.click()
                Browser.log(self, "Wait for search result...")
                time.sleep(10)
                arr = self.myweb.find_elements_by_class_name("rb")
                for item in arr:
                    try:
                        title = item.find_element_by_tag_name("h3")
                    except Exception as e:
                        continue
                    url = item.find_element_by_tag_name("cite")
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
                        title.find_element_by_tag_name("a").click()
                        time.sleep(t)
                        break
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

    def Post(self,delay):
        try:
            for x in delay:
                url = "https://www.sogou.com/tx?site=%s&query=%s" % (self.domain,self.keyword)
                # 打开360PC端
                self.myweb.get(url=url)
                Browser.log(self,"Wait for search result...")
                page = 1
                finded = False
                while page < 10 and not finded:
                    Browser.log(self,"Search the results... page: %d" % page)
                    arr = self.myweb.find_elements_by_class_name("rb")
                    print(arr)
                    for item in arr:
                        print(item)
                        try:
                            title = item.find_element_by_tag_name("h3")
                        except Exception as e:
                            continue
                        try:
                            url = item.find_element_by_tag_name("cite")
                        except Exception as e:
                            continue
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
                    if not finded:
                        Browser.log(self,"Can't find the target site...")
                        next = self.myweb.find_element_by_id("sogou_next")
                        Browser.log(self,"Move to next page...")
                        next.click()
                        page = page + 1
                        time.sleep(5)
                if finded:
                    Browser.log(self,"Switch to new browser tab...")
                    index = 1
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
            return 4
        except:
            Browser.log(self,"System Error , start new turn...")
            return 4
        finally:
            self.myweb.quit()
            Browser.log(self,"chromedriver is closed...")
            Browser.close_display(self)
            return 4