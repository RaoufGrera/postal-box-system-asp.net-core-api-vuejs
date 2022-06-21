<template>
    <div>ccccccc</div>
</template>
<script>
import { mapState, mapActions } from "vuex";

export default {

  created(){
      console.log("im logout logoutt");
   this.logoutt()
 },
      methods:{
 
            ...mapActions('account', [ "logoutt" ])

  }
}
</script>
