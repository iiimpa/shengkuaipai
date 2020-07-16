import random,time,json
from base import find_urls,Browser,writeConfig

class Mbaidu(Browser):
    """docstring for Mbaidu"""
    def __init__(self,keyword,domain,proxy,ua,resolution,title):
        super(Mbaidu, self).__init__()
        Browser.__init__(self)
        self.keyword = keyword
        self.domain = domain
        self.proxy = proxy
        self.ua = ua
        self.title = title
        self.resolution = resolution
        self.myweb = Browser.get_chromedriver(self,self.proxy,self.ua,self.resolution)
        try:
            url = "https://m.baidu.com"
            # 打开百度手机端
            self.myweb.get(url=url)
            Browser.log(self,"Begin search：%s" % self.keyword)
            # 定位到text的文本框
            self.text = self.myweb.find_element_by_id("index-kw")
        except Exception as e:
            self.myweb.quit()
            Browser.log(self,"Proxy ip exception,chromedriver is closed...")
            Browser.close_display(self)
            return 4
        
    #百度手机端普通模式
    def Normal(self,delay):
        try:
            for x in delay:
                url = "https://m.baidu.com"
                # 打开百度手机端
                self.myweb.get(url=url)
                Browser.log(self,"Begin search：%s" % self.keyword)
                # 定位到text的文本框
                self.text = self.myweb.find_element_by_id("index-kw")
                # 搜索词条
                self.text.send_keys(self.keyword)
                # 定位到搜索键
                try:
                    button = self.myweb.find_element_by_id("index-bn")
                except:
                    button = self.myweb.find_element_by_id("se-bn")
                time.sleep(3)
                # 点击搜索键
                button.click()
                Browser.log(self,"Wait for search result...")
                time.sleep(3)
                page = 1
                finded = False
                while page < 10 and not finded:
                    btn_more = self.myweb.find_elements_by_class_name("ivk-button")
                    if btn_more:
                        Browser.log(self,"Begin click through more content...")
                        btn_more[0].click()
                        btn_cancel = self.myweb.find_elements_by_class_name("popup-lead-cancel")[0]
                        btn_cancel.click()
                    Browser.log(self,"Search the results... page: %d" % page)
                    arr = self.myweb.find_elements_by_class_name("result")
                    for item in arr:
                        title = item.find_element_by_tag_name("header")
                        url = item.find_element_by_class_name("c-footer-showurl")
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
                        next = []
                        time.sleep(3)
                        result = True
                        if page == 1:
                            next = self.myweb.find_elements_by_xpath("//*[@id=\"page-controller\"]/div/a[last()]")
                        else:
                            next = self.myweb.find_elements_by_xpath("//*[@id=\"page-controller\"]/div/div[last()]/a")
                        Browser.log(self,"Move to next page...")
                        try:
                            next[0].click()
                        except Exception as e:
                            Browser.log(self,str(e))
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

    #百度手机端直连模式
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
                return 4
        except Exception as e:
            return 4
        finally:
            self.myweb.quit()
            Browser.log(self,"chromedriver is closed...")
            Browser.close_display(self)

    #百度手机端造词模式
    def Create_keyword(self,optimization,delay):
        try:
            # 搜索词条
            self.text.send_keys(optimization)
            # 定位到搜索键
            button = self.myweb.find_element_by_id("index-bn")
            time.sleep(3)
            # 点击搜索键
            button.click()
            Browser.log(self,"Wait for search result...")
            time.sleep(3)
            page = 1
            for x in range(1,random.randint(1,5)):
                next = []
                time.sleep(3)
                result = True
                if page == 1:
                    next = self.myweb.find_elements_by_xpath("//*[@id=\"page-controller\"]/div/a[last()]")
                else:
                    next = self.myweb.find_elements_by_xpath("//*[@id=\"page-controller\"]/div/div[last()]/a")
                Browser.log(self,"Move to next page...")
                try:
                    next[0].click()
                except Exception as e:
                    Browser.log(self,str(e))
                page = page + 1
                time.sleep(5)
            self.text = self.myweb.find_element_by_id("kw")
            self.text.clear()
            # 搜索词条
            self.text.send_keys(self.keyword)
            # 定位到搜索键
            try:
                button = self.myweb.find_element_by_id("index-bn")
            except:
                button = self.myweb.find_element_by_id("se-bn")
            time.sleep(3)
            # 点击搜索键
            button.click()
            Browser.log(self,"Wait for search result...")
            time.sleep(3)
            page = 1
            for x in range(1,random.randint(1,5)):
                next = []
                time.sleep(3)
                result = True
                if page == 1:
                    next = self.myweb.find_elements_by_xpath("//*[@id=\"page-controller\"]/div/a[last()]")
                else:
                    next = self.myweb.find_elements_by_xpath("//*[@id=\"page-controller\"]/div/div[last()]/a")
                Browser.log(self,"Move to next page...")
                try:
                    next[0].click()
                except Exception as e:
                    Browser.log(self,str(e))
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

    #站内搜索(工具)
    def Site_utils(self,delay):
        try:
            for x in delay:
                url = "https://m.baidu.com"
                # 打开百度手机端
                self.myweb.get(url=url)
                Browser.log(self,"Begin search：%s" % self.keyword)
                # 定位到text的文本框
                self.text = self.myweb.find_element_by_id("index-kw")
                # 搜索词条
                self.text.send_keys(self.keyword)
                # 定位到搜索键
                try:
                    button = self.myweb.find_element_by_id("index-bn")
                except:
                    button = self.myweb.find_element_by_id("se-bn")
                time.sleep(3)
                # 点击搜索键
                button.click()
                Browser.log(self,"Wait for search result...")
                time.sleep(3)
                btn_more = self.myweb.find_elements_by_class_name("ivk-button")
                if btn_more:
                    Browser.log(self,"Begin click through more content...")
                    btn_more[0].click()
                    btn_cancel = self.myweb.find_elements_by_class_name("popup-lead-cancel")[0]
                    btn_cancel.click()
                filterbtn = self.myweb.find_element_by_link_text("高级筛选")
                c = True
                while c:
                    try:
                        filterbtn.click()
                        c = False
                    except Exception as e:
                        print(str(e))
                self.myweb.find_element_by_class_name("c-input").send_keys(self.domain)
                self.myweb.find_element_by_class_name("filter-panel-confirm").click()
                time.sleep(2)
                page = 1
                finded = False
                while page < 10 and not finded:
                    Browser.log(self,"Search the results... page: %d" % page)
                    arr = self.myweb.find_elements_by_class_name("result")
                    for item in arr:
                        title = item.find_element_by_tag_name("header")
                        url = item.find_element_by_class_name("c-footer-showurl")
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
                        next = []
                        time.sleep(3)
                        result = True
                        if page == 1:
                            next = self.myweb.find_elements_by_xpath("//*[@id=\"page-controller\"]/div/a[last()]")
                        else:
                            next = self.myweb.find_elements_by_xpath("//*[@id=\"page-controller\"]/div/div[last()]/a")
                        Browser.log(self,"Move to next page...")
                        try:
                            next[0].click()
                        except Exception as e:
                            Browser.log(self,str(e))
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
        # try:
        #     for x in delay:
        #         url = "https://m.baidu.com/s?word=%s&tn=monline_4_dg&ie=utf-8&si=%s&ct=2097152" % (self.keyword,self.domain)
        #         # 打开百度手机端
        #         self.myweb.get(url=url)
        #         Browser.log(self,"Wait for search result...")
        #         time.sleep(3)
        #         page = 1
        #         finded = False
        #         while page < 10 and not finded:
        #             btn_more = self.myweb.find_elements_by_class_name("ivk-button")
        #             if btn_more:
        #                 Browser.log(self,"Begin click through more content...")
        #                 btn_more[0].click()
        #                 btn_cancel = self.myweb.find_elements_by_class_name("popup-lead-cancel")[0]
        #                 btn_cancel.click()
        #             Browser.log(self,"Search the results... page: %d" % page)
        #             arr = self.myweb.find_elements_by_class_name("result")
        #             for item in arr:
        #                 title = item.find_element_by_tag_name("header")
        #                 url = item.find_element_by_class_name("c-footer-showurl")
        #                 if self.title != "":
        #                     if self.title == title.text:
        #                         isClick = True
        #                     else:
        #                         isClick = False
        #                         continue
        #                 if self.domain in url.text:
        #                     isClick = True
        #                 else:
        #                     isClick = False
        #                     continue
        #                 if isClick:
        #                     Browser.log(self, "Find target site, Enter ...")
        #                     try:
        #                         title.click()
        #                         title.find_element_by_tag_name("a").click()
        #                     except Exception as e:
        #                         pass
        #                     finded = True
        #                     break
        #             if not finded:
        #                 Browser.log(self,"Can't find the target site...")
        #                 next = []
        #                 time.sleep(3)
        #                 result = True
        #                 if page == 1:
        #                     next = self.myweb.find_elements_by_xpath("//*[@id=\"page-controller\"]/div/a[last()]")
        #                 else:
        #                     next = self.myweb.find_elements_by_xpath("//*[@id=\"page-controller\"]/div/div[last()]/a")
        #                 Browser.log(self,"Move to next page...")
        #                 try:
        #                     next[0].click()
        #                 except Exception as e:
        #                     Browser.log(self,str(e))
        #                 page = page + 1
        #                 time.sleep(5)
        #         if finded:
        #             Browser.log(self,"Switch to new browser tab...")
        #             index = 1
        #             Browser.log(self,"Begin to surface the inner page... Seq:%d time: %ds" % (index, x))
        #             time.sleep(x)
        #             wins = self.myweb.window_handles
        #             self.myweb.switch_to.window(wins[-1])
        #             arr = self.myweb.find_elements_by_tag_name("a")
        #             result = True
        #             maxtime = 0
        #             while result:
        #                 try:
        #                     random.choice(arr).click()
        #                     result = False
        #                 except:
        #                     maxtime += 1
        #                     if maxtime > 10:
        #                        result = False
        #                        continue
        #                     Browser.log(self,"Unknown error encountered, replacing element...")
        #                 index = index + 1
        #                 Browser.log(self,"Finished")
        #             continue
        #         else:
        #             continue
        #     return 4
        # except:
        #     Browser.log(self,"System Error , start new turn...")
        #     return 4
        # finally:
        #     self.myweb.quit()
        #     Browser.log(self,"chromedriver is closed...")
        #     Browser.close_display(self)
        #     return 4

    #站内搜索(指令)
    def Site_search(self,delay):
        try:
            for t in delay:
                url = "https://m.baidu.com"
                # 打开百度手机端
                self.myweb.get(url=url)
                Browser.log(self, "Begin search：%s" % self.keyword)
                # 定位到text的文本框
                self.text = self.myweb.find_element_by_id("index-kw")
                self.text.send_keys("site:%s %s" % (self.domain, self.keyword))
                time.sleep(2)
                try:
                    button = self.myweb.find_element_by_id("index-bn")
                except:
                    button = self.myweb.find_element_by_id("se-bn")
                # 点击搜索键
                print(button)
                button.click()
                Browser.log(self, "Wait for search result...")
                time.sleep(10)
                arr = self.myweb.find_elements_by_class_name("result")
                for item in arr:
                    title = item.find_element_by_tag_name("header")
                    url = item.find_element_by_class_name("c-footer-showurl")
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
                url = "https://m.baidu.com/s?word=%s&tn=monline_4_dg&ie=utf-8&si=%s&ct=2097153" % (self.keyword,self.domain)
                # 打开百度手机端
                self.myweb.get(url=url)
                Browser.log(self,"Wait for search result...")
                time.sleep(3)
                page = 1
                finded = False
                while page < 10 and not finded:
                    btn_more = self.myweb.find_elements_by_class_name("ivk-button")
                    if btn_more:
                        Browser.log(self,"Begin click through more content...")
                        btn_more[0].click()
                        btn_cancel = self.myweb.find_elements_by_class_name("popup-lead-cancel")[0]
                        btn_cancel.click()
                    Browser.log(self,"Search the results... page: %d" % page)
                    arr = self.myweb.find_elements_by_class_name("result")
                    for item in arr:
                        try:
                            title = item.find_element_by_tag_name("header")
                        except Exception as e:
                            continue
                        try:
                            url = item.find_element_by_class_name("c-footer-showurl")
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
                        next = []
                        time.sleep(3)
                        result = True
                        if page == 1:
                            next = self.myweb.find_elements_by_xpath("//*[@id=\"page-controller\"]/div/a[last()]")
                        else:
                            next = self.myweb.find_elements_by_xpath("//*[@id=\"page-controller\"]/div/div[last()]/a")
                        Browser.log(self,"Move to next page...")
                        try:
                            next[0].click()
                        except Exception as e:
                            Browser.log(self,str(e))
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