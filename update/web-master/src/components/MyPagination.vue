<!--
 * @Date: 2020-03-11 17:54:19
 * @LastEditTime: 2020-03-16 23:42:38
 * @Description: file content
 -->
<template>
	<div class="my-pagination">
		<el-pagination
			:current-page="searchForm.pageindex"
			:page-sizes="[10, 20, 50, 100]"
			:page-size="searchForm.pagesize"
			layout="total, sizes, prev, pager, next, jumper"
			:total="searchForm.total"
			@size-change="handleSizeChange"
			@current-change="handleCurrentChange"
		></el-pagination>
	</div>
</template>

<script>
export default {
	name: 'MyPagination',
	props: {
		searchForm: {
			type: Object,
			default: () => {
				return {
					pageindex: 1,
					pagesize: 20,
					orderId:0,
					total: 20
				}
			}
		}
	},
	data() {
		return {}
	},
	methods: {
		handleSizeChange(val) {
			this.searchForm.pagesize = val
			this.$emit('loadData')
		},
		handleCurrentChange(val) {
			this.searchForm.pageindex = val
			this.$emit('loadData')
		}
	}
}
</script>
<style lang="scss" scoped>
.my-pagination {
	margin-top: 10px;
	text-align: center;
}</style
>>
