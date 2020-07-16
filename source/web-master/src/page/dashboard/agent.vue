<template>
  <div>
    <table style="border: none; line-height: 50px;">
      <tr>
        <td>
          用户名： {{$root.user.account}}
        </td>
        <td>
          代理佣金: ¥{{$root.user.balance}}元 <a href="javascript:;" @click="withdraw">提现</a>
        </td>
      </tr>
      <tr>
        <td>
          支付宝收款账号：{{$root.user.alipay || "未设置"}} <a href="javascript:;" @click="changeAlipayDialog = true">修改账号</a>
        </td>
      </tr>
      <tr>
        <td colspan="2">
          邀请链接： {{inviteLink}}
        </td>
      </tr>
    </table>

    <hr>
    <div class="custom-tree-container">
      <div class="block left-tree">
        <el-tree
          :props="props"
          :load="loadNode"
          lazy>
        </el-tree>
      </div>
      <div class="block right-table">
        <el-table
          :data="logs"
          border
          style="width: 100%">
          <el-table-column
            prop="createdAt"
            label="日期">
          </el-table-column>
          <el-table-column
            prop="relationId"
            label="账号">
          </el-table-column>
          <el-table-column
            prop="amount"
            label="佣金数">
          </el-table-column>
        </el-table>
      </div>
    </div>
    <el-dialog title="修改账号" :visible.sync="changeAlipayDialog">
      <el-form :model="changeAlipay">
        <el-form-item label="密码" label-width="120">
          <el-input v-model="changeAlipay.Password" type="password" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="支付宝账号" label-width="120">
          <el-input v-model="changeAlipay.Alipay" autocomplete="off"></el-input>
        </el-form-item>

      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="changeAlipayDialog = false">取 消</el-button>
        <el-button type="primary" @click="doChangeAlipay">确 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
  export default {
    name: "agent",
    data() {
      return {
        props: {
          label: 'account',
          isLeaf: 'id'
        },
        changeAlipayDialog: false,
        changeAlipay: {},
        inviteLink: '',
        plans: {},
        logs: [],
        tableData: [
          // {
          //   date: '2020-01-15',
          //   account: 'xrain2',
          //   coins: '23000',
          //   commission: '¥2.30'
          // }
        ]
      }
    },
    methods: {
      doChangeAlipay() {
        this.api("/user/change-alipay", this.changeAlipay).then(resp => {
          this.changeAlipayDialog = false;
          this.$root.user.alipay = this.changeAlipay.Alipay;
          this.changeAlipay = {};
          this.$message({
            showClose: true,
            message: '修改成功',
          });
        })
      },
      loadNode(node, resolve) {
        if (node.level === 0) {
          return resolve([{account: '我的下级'}]);
        }
        if (node.level > 1) return resolve([]);

        console.log(node);
        this.api("/user/downlist", {id: 1}).then(resp => {
          resolve(resp.data);
        });
      },
      getLog() {
        this.api("/user/trades", {type: 4}).then(resp => this.logs = resp.data);
      },
      withdraw() {
        this.api("/user/withdraw/query").then(() => {
          this.$message({
            showClose: true,
            message: '提现成功，请等待审核',
          });
          this.$root.user.balance = 0;
        })
      }
    },
    mounted() {
      this.inviteLink = `${location.protocol}//${location.host}/register?invite=${this.$root.user.id}`;
      this.getLog();
    }
  }
</script>

<style scoped>
  table {
    width: 100%;
  }

  table tr {
    width: 100%;
  }

  table tr td {
    width: 50%;
  }

  .custom-tree-container {
    width: 100%;
    flex: 1;
    display: flex;
    justify-content: space-between;
    font-size: 14px;
    padding-right: 8px;
    min-height: 500px;
  }

  .left-tree {
    width: 180px;
  }

  .right-table {
    width: 880px;
  }
</style>
