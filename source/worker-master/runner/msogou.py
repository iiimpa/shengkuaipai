import random
import time
from base import log, recreate_x11
from base import get_chromedriver


def msogou(keyword, domain, proxy, ua, resolution, delay):
    recreate_x11(resolution)
    myweb = get_chromedriver(proxy, ua)
    url = "https://m.sogou.com"
    # 打开百度
    myweb.get(url=url)
    try:
        log("Begin search：%s" % keyword)
        # 定位到text的文本框
        text = myweb.find_element_by_id("keyword")

        # 搜索词条
        text.send_keys(keyword)

        # 定位到搜索键
        button = myweb.find_element_by_xpath("//*[@id=\"searchform\"]/div/div/div[1]/div[2]/input")
        # 点击搜索键
        button.click()
        log("Wait for search result...")
        time.sleep(3)
        page = 1
        finded = False
        while page < 10 and not finded:
            log("Search the results... page: %d" % page)
            arr = myweb.find_elements_by_class_name("citeurl")
            for item in arr:
                if item.text.find(domain) != -1:
                    log("Find target site, Enter ...")
                    finded = True
                    link = item.find_element_by_xpath("./..")
                    alink = link.find_elements_by_tag_name("a")
                    alink[0].click()
                    break
            if not finded:
                log("Can't find the target site...")
                next = myweb.find_element_by_id('ajax_next_page')
                log("Move to next page...")
                next.click()
                page = page + 1
                time.sleep(5)
        if finded:
            log("Switch to new browser tab...")
            index = 1
            for item in delay:
                log("Begin to surface the inner page... Seq:%d time: %ds" % (index, item))
                time.sleep(item)
                wins = myweb.window_handles
                myweb.switch_to.window(wins[-1])
                arr = myweb.find_elements_by_tag_name("a")
                random.choice(arr).click()
                index = index + 1
            log("Finished")
            return 4
        else:
            return 2
    except:
        log("System Error , start new turn...")
        return 3
    finally:
        myweb.quit()
