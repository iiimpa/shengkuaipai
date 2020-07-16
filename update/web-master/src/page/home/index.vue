<template>
  <div>
    <upHeader />
    <router-view/>
    <upFooter />
    <fixbtn />
  </div>
</template>

<script>
import upHeader from '../../components/upHeader.vue';
import upFooter from '../../components/upFooter.vue';
import fixbtn from '../../components/fixbtn.vue';
export default {
  components: {
    upHeader,
    upFooter,
    fixbtn
  },
  data() {
    return {};
  },
  methods: {}
};
</script>

<style scoped lang="scss"></style>
