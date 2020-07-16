<template>
  <div id="app">
    <router-view/>
  </div>
</template>
<script>
  export default {
    name: 'seo',
    methods: {
      checkLogin() {
        if (!localStorage.getItem("token")) {
          this.$router.replace("/login");
        }
        this.api("/user/info", null, false).then(res => {
          this.$root.user = res.data;
        })
      }
    },
    mounted() {
      this.checkLogin()
    },
    watch: {
      '$route'(to, from) {
        if (["/register", "/login", "/home"].indexOf(to.path) === -1) {
          this.checkLogin();
        }
      }
    }
  }
</script>
<style>
  body {
    margin: 0;
    padding: 0;
  }

  body.modalOpen {
    -webkit-overflow-scrolling: touch;
    position: fixed;
    width: 100%;
  }

  #app {
    font-family: 'Avenir', Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    /* text-align: center; */
    color: #2c3e50;
    min-height: 100vh;
    min-width: 100vw;
  }

  .flexs {
    display: flex;
    align-items: center;
  }

  .j_center {
    justify-content: center;
  }

  .j_bew {
    justify-content: space-between;
  }

  body, h1, h2, h3, h4, h5, h6, hr, p, blockquote, /* structural elements 结构元素 */
  dl, dt, dd, ul, ol, li, /* list elements 列表元素 */
  pre, /* text formatting elements 文本格式元素 */
  form, fieldset, legend, button, input, textarea, /* form elements 表单元素 */
  th, td, div, span /* table elements 表格元素 */
  {
    font-family: 'Adobe 黑体 Std R';
  }

  button {
    border: none !important;
  }

  input {
    outline: none !important;
    border: none;
    padding: 0;
  }

  p {
    margin: 0;
  }

  a {
    text-decoration: none;
    cursor: pointer;
  }

  button {
    cursor: pointer;
  }

  li {
    list-style: none;
  }

  textarea {
    outline: none !important;
    resize: none;
  }

  html {
    /*隐藏滚动条，当IE下溢出，仍然可以滚动*/
    -ms-overflow-style: none;
    /*火狐下隐藏滚动条*/
    overflow: -moz-scrollbars-none;
  }

  ::-webkit-scrollbar {
    display: none;
  }

  .el-popover {
    padding: 0 !important;
  }

  .cur_p {
    cursor: pointer;
  }

  .cur_drop {
    cursor: no-drop;
  }

  .cur_help {
    cursor: help;
  }

  .cur_wait {
    cursor: wait;
  }

  .textflow {
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
  }

  .noselect {
    -webkit-touch-callout: none; /* iOS Safari */
    -webkit-user-select: none; /* Chrome/Safari/Opera */
    -khtml-user-select: none; /* Konqueror */
    -moz-user-select: none; /* Firefox */
    -ms-user-select: none; /* Internet Explorer/Edge */
    user-select: none; /* Non-prefixed version, currently*/
  }

  .clearfloat:after {
    display: block;
    clear: both;
    content: '';
    visibility: hidden;
    height: 0;
  }

  @media screen and (max-width: 1200px) {
    #app {
      width: 1200px;
    }
  }
</style>
