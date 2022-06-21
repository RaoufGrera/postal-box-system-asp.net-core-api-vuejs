<template>
    <div></div>
</template>
<script>
import { mapState, mapActions } from 'vuex'

export default {

  created(){
      console.log("im logout");
   this.logout()
 },
      methods:{
 
            ...mapActions('account', [ "logout" ])

  }
}
</script>
