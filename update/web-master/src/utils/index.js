/*
 * @Date: 2020-03-12 10:02:04
 * @LastEditTime: 2020-03-12 10:02:05
 * @Description: file content
 */

export function formatDate(date, format) {
  if (!date) {
    return ''
  }
  date = new Date(date)
  var year = date.getFullYear() // 年
  var month = date.getMonth() + 1 < 10 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1 // 月
  var day = date.getDate() < 10 ? '0' + date.getDate() : date.getDate() // 日
  var hour = date.getHours() < 10 ? '0' + date.getHours() : date.getHours() // 时
  var minutes = date.getMinutes() < 10 ? '0' + date.getMinutes() : date.getMinutes() // 分
  var seconds = date.getSeconds() < 10 ? '0' + date.getSeconds() : date.getSeconds() // 秒
  var milliseconds = date.getMilliseconds() < 10 ? '0' + date.getMilliseconds() : date.getMilliseconds() // 毫秒
  if (!format) {
    return year + '-' + month + '-' + day + ' ' + hour + ':' + minutes + ':' + seconds
  }
  switch (format) {
    case 'yyyy-mm-dd hh:MM:ss.fff':
      return year + '-' + month + '-' + day + ' ' + hour + ':' + minutes + ':' + seconds + '.' + milliseconds
    case 'yyyymmddhhMMss':
      return year + '' + month + '' + day + '' + hour + '' + minutes + '' + seconds
    case 'yyyy-mm-dd hh:MM:ss':
      return year + '-' + month + '-' + day + ' ' + hour + ':' + minutes + ':' + seconds
    case 'yyyy-mm-dd hh:MM':
      return year + '-' + month + '-' + day + ' ' + hour + ':' + minutes
    case 'yyyymmdd':
      return year + '' + month + '' + day
    case 'yyyy.mm.dd':
      return year + '.' + month + '.' + day
    case 'yyyy-mm-dd':
      return year + '-' + month + '-' + day
  }
}
