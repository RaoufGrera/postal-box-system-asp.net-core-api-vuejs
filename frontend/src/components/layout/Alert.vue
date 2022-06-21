<template>
     
          <div v-if="alert.message" :class="`alert ${alert.type}`"> {{alert.message}}
             <button type="button" class="close" @click="clearAlert(null)">
    <span aria-hidden="true">&times;</span>
  </button>
        </div>
</template>
<script>
import { mapState, mapActions } from "vuex";

export default {
     computed: {
    ...mapState({
      alert: state => state.alert
    }),
     },
       methods: {
    ...mapActions({
      clearAlert: "alert/clear"
    })
  },
  watch: {
    $route(to, from) {
      // clear alert on location change
      this.clearAlert();
    }
  },
}
</script>
<style>
/* .alert{
 
    position: fixed;
    z-index:9999;
}
*/
</style>
