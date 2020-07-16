<template>
  <el-form :model="form" ref="ruleForm" label-width="100px">
    <el-form-item label="域名" prop="domain">
      <el-input v-model="form.domain"></el-input>
    </el-form-item>
    <el-form-item label="关键词" prop="keyword">
      <el-input placeholder="请输入内容" v-model="keyword" class="input-with-select">
        <el-select v-model="form.platform" slot="prepend" placeholder="请选择" style="width: 140px;">
          <el-option v-for="(item,index) in platforms" :key="index"
                     :label="item"
                     :value="index"></el-option>
        </el-select>
        <el-button slot="append" icon="el-icon-search" @click="addKeyword"></el-button>
      </el-input>
      <el-table
        size="mini"
        :data="keywords"
        border
        style="width: 100%">
        <el-table-column
          label="搜索引擎">
          <template slot-scope="scope">
            {{platforms[scope.row.platform]}}
          </template>
        </el-table-column>
        <el-table-column
          prop="keyword"
          label="关键词">
        </el-table-column>
        <el-table-column
          prop="rank"
          label="当前排名">
        </el-table-column>
        <el-table-column
          prop="status"
          label="状态">
        </el-table-column>
        <el-table-column
          label="操作">
          <template slot-scope="scope">
            <el-button @click="reCheck(scope.$index)" type="text" size="small">重新查询</el-button>
            <el-button @click="remoteKeyword(scope.$index)" type="text" size="small">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
    </el-form-item>
    <el-form-item label="点击方案" prop="plan">
      <el-select v-model="form.plan" placeholder="请选择点击方案" style="width:100%">
        <el-option v-for="item in plans" :key="item.id"
                   :label="item.name + ' [单价：'+item.price+'c/次]' + ' 总计浏览内页：'+item.times.split(',').length + ' 页 , 每页浏览时长(s)：' + item.times"
                   :value="item.id"></el-option>
      </el-select>
    </el-form-item>
    <el-form-item label="点击次数" prop="time">
      <el-select v-model="form.clickPlan" @change="clickPlanChange" placeholder="请选择点击次数方案" style="width:100%">
        <el-option v-for="item in clickPlans" :key="item.id"
                   :label="item.name"
                   :value="item"></el-option>
        <el-option label="自定义方案" :value="0"></el-option>
      </el-select>
      <el-row>
        <el-col :span="4">
          <el-input v-model="form.time[0]" type="number" min="0">
            <template slot="prepend">00:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[1]" type="number" min="0">>
            <template slot="prepend">01:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[2]" type="number" min="0">
            <template slot="prepend">02:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[3]" type="number" min="0">
            <template slot="prepend">03:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[4]" type="number" min="0">
            <template slot="prepend">04:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[5]" type="number" min="0">
            <template slot="prepend">05:00</template>
          </el-input>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="4">
          <el-input v-model="form.time[6]" type="number" min="0">
            <template slot="prepend">06:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[7]" type="number" min="0">
            <template slot="prepend">07:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[8]" type="number" min="0">
            <template slot="prepend">08:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[9]" type="number" min="0">
            <template slot="prepend">09:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[10]" type="number" min="0">
            <template slot="prepend">10:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[11]" type="number" min="0">
            <template slot="prepend">11:00</template>
          </el-input>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="4">
          <el-input v-model="form.time[12]" type="number" min="0">
            <template slot="prepend">12:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[13]" type="number" min="0">
            <template slot="prepend">13:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[14]" type="number" min="0">
            <template slot="prepend">14:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[15]" type="number" min="0">
            <template slot="prepend">15:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[16]" type="number" min="0">
            <template slot="prepend">16:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[17]" type="number" min="0">
            <template slot="prepend">17:00</template>
          </el-input>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="4">
          <el-input v-model="form.time[18]" type="number" min="0">
            <template slot="prepend">18:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[19]" type="number" min="0">
            <template slot="prepend">19:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[20]" type="number" min="0">
            <template slot="prepend">20:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[21]" type="number" min="0">
            <template slot="prepend">21:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[22]" type="number" min="0">
            <template slot="prepend">22:00</template>
          </el-input>
        </el-col>
        <el-col :span="4">
          <el-input v-model="form.time[23]" type="number" min="0">
            <template slot="prepend">23:00</template>
          </el-input>
        </el-col>
      </el-row>
    </el-form-item>
    <el-form-item label="每日递增" prop="raise">
      <el-input v-model="form.raise" type="number" min="0">>
        <template slot="append">%</template>
      </el-input>
    </el-form-item>
    <el-form-item label="订单天数" prop="days">
      <el-input v-model="form.days" type="number" min="0">>
        <template slot="append">天</template>
      </el-input>
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="saveIt">立即创建</el-button>
      <el-button @click="form.time = timeTmpl">使用推荐计划</el-button>
    </el-form-item>
  </el-form>
</template>

<script>
  export default {
    name: "addOrder",
    data() {
      return {
        timeTmpl: [5, 6, 3, 7, 8, 5, 9, 3, 5, 2, 6, 3, 4, 6, 2, 5, 7, 8, 4, 3, 1, 5, 6, 3],
        zeroTime: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
        plans: [],
        clickPlans: [],
        keywords: [],
        platforms: [
          'PC - 百度',
          'PC - 搜狗',
          'PC - 360',
          '手机 - 百度',
          '手机 - 搜狗',
          '手机 - 360',
        ],
        form: {
          platform: 0,
          keyword: '',
          domain: '',
          time: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
          raise: 10,
          days: 7
        },
        keyword: ''
      }
    },
    mounted() {
      this.form.domain = this.$route.query.domain;
      this.getPlans();
      this.getClickPlans();
    },
    methods: {
      log(a) {
        console.log(a)
      },
      clickPlanChange() {
        if (this.form.clickPlan == 0) {
          this.form.time = this.zeroTime;
        } else {
          var tmp = this.form.clickPlan;
          this.form.time = [tmp.t0, tmp.t1, tmp.t2, tmp.t3, tmp.t4, tmp.t5, tmp.t6, tmp.t7, tmp.t8, tmp.t9, tmp.t10, tmp.t11, tmp.t12, tmp.t13, tmp.t14, tmp.t15, tmp.t16, tmp.t17, tmp.t18, tmp.t19, tmp.t20, tmp.t21, tmp.t22, tmp.t23,]
        }
      },
      remoteKeyword(index) {
        this.keywords.splice(index, 1)
      },
      addKeyword() {
        for (var i in this.keywords) {
          var item = this.keywords[i];
          if (item.keyword == this.keyword && item.platform == this.form.platform) {
            this.$message({
              showClose: true,
              message: '已经存在相同记录',
              type: 'warning'
            });
            return;
          }
        }
        var line = {
          platform: this.form.platform,
          keyword: this.keyword,
          rank: '查询中',
          status: '查询中',
        };
        this.keywords.push(line);
        this.reCheck(this.keywords.indexOf(line));
      },
      reCheck(index) {
        this.keywords[index].rank = '查询中';
        this.keywords[index].status = '查询中';
        this.api("/user/rank", {
          platform: parseInt(this.keywords[index].platform),
          domain: this.form.domain,
          keyword: this.keywords[index].keyword
        }).then(resp => {
          this.keywords[index].rank = resp.data == 0 ? '无' : resp.data;
          this.keywords[index].status = (resp.data > 0 && resp.data < 101) ? '可以优化' : '不可优化';
        }).catch(() => {
          this.keywords[index].rank = '查询失败';
          this.keywords[index].status = '查询失败';
        })
      },
      getPlans() {
        this.api("/user/plan/list").then(resp => {
          this.plans = resp.data;
        })
      },
      getClickPlans() {
        this.api("/user/click-plan/list").then(resp => {
          this.clickPlans = resp.data;
        })
      },
      saveIt() {
        if (this.keywords.length == 0) {
          this.$message({
            showClose: true,
            message: '必须添加关键词才能下单！！',
            type: 'error'
          });
          return;
        }
        var sum = 0
        var arr = this.form.time.map(Number);
        for (var i = 0; i < arr.length; i++) {
          sum = sum + arr[i]
        }
        if (sum <= 0) {
          this.$message({
            showClose: true,
            message: '点击次数不正确！！',
            type: 'error'
          });
          return;
        }
        // for (var k in this.keywords) {
        //   if (this.keywords[k].status == '不可优化') {
        //     this.$message({
        //       showClose: true,
        //       message: '请手动删除不可优化的记录，再进行下单！',
        //       type: 'error'
        //     });
        //     return;
        //   }
        // }
        for (var k in this.keywords) {
          // if (this.keywords[k].status == '不可优化') {
          //   continue;
          // }
          const loading = this.$loading({
            lock: true,
            text: '正在创建订单...<' + this.keywords[k].keyword + '>',
            spinner: 'el-icon-loading',
            background: 'rgba(0, 0, 0, 0.7)'
          });

          this.api("/user/order/add", {
            keyword: this.keywords[k].keyword,
            time: this.form.time.map(Number),
            raise: parseInt(this.form.raise),
            days: parseInt(this.form.days),
            domain: this.form.domain,
            platform: this.keywords[k].platform,
            plan: this.form.plan,
            rank: this.keywords[k].rank == '无' ? 1000 : parseInt(this.keywords[k].rank)
          }).then(resp => {
            loading.close();
            this.$router.push("/user/order");
          }).catch(() => loading.close());
        }
      }
    }
  }
</script>

<style scoped>
  template {
    width: 100px;
  }
</style>
