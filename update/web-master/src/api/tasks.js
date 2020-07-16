/*
 * @Date: 2020-03-11 18:56:30
 * @LastEditTime: 2020-03-18 16:51:34
 * @Description: file content
 */
import request from "../utils/request"

// 订单列表
export function getTaskList(params) {
  return request("/user/order/list", params)
}
//订单详情
export function getTaskDetails(params) {
  return request("/user/task/list", params)
}
// 任务饼图数据
export function getOrdersCalc(params) {
  return request("/user/order/calc", params)
}
// 续费
export function renewOrders(params) {
  return request("/user/order/renew", params)
}
// 批量取消
export function batchCancelOrders(params) {
  return request("/user/order/cancel", params)
}
