<template>
  <div>
    <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
      <div class="card card-signin my-5">
        <div class="card-body">
          <table class="table">
            <thead>
              <th colspan="2">Postal employee</th>
            </thead>
            <thead class="table-primary">
              <th>Username</th>
              <th>Password</th>
            </thead>
            <tbody>
              <tr>
                <td>office1</td>
                <td>123456</td>
              </tr>
              <tr>
                <td>office2</td>
                <td>123456</td>
              </tr>
            </tbody>
          </table>

          <h5 class="card-title text-center">تسجيل الدخول</h5>
          <hr>
          <form @submit.prevent="handleSubmit">
            <div class="form-group">
              <label for="UserName">اسم المستخدم</label>
              <input type="text" v-model="UserName" name="UserName" class="form-control"
                :class="{ 'is-invalid': submitted && !UserName }">
              <div v-show="submitted && !UserName" class="invalid-feedback">الحقل مطلوب</div>
            </div>
            <div class="form-group">
              <label for="Password">الرقم السري</label>
              <input type="Password" v-model="Password" name="Password" class="form-control"
                :class="{ 'is-invalid': submitted && !Password }">
              <div v-show="submitted && !Password" class="invalid-feedback">الرقم السري مطلوب</div>
            </div>
            <div class="form-group">
              <button class="btn btn-block btn-primary" :disabled="status.loggingIn">دخول</button>
              <img class="loading" v-show="status.loggingIn"
                src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA==">
            </div>
            <!--     <template>
  <button v-facebook-signin-button="appId" class="facebook-signin-button"> Continue with Facebook</button>
</template> -->
          </form>

        </div>
      </div>
    </div>
  </div>
</template>
 

<script>
import { mapState, mapActions } from "vuex";
// import FacebookSignInButton from 'vue-facebook-signin-button-directive';
import { userService } from "../../_services/user.service";

export default {
  /*directives: {
    FacebookSignInButton
  },*/
  data() {
    return {
      UserName: "",
      Password: "",
      submitted: false,
      appId: '1389759387825879'

    };
  },
  computed: {
    ...mapState("account", ["status"])
  },
  created() {
    // reset login status
    this.logout();
  },
  methods: {
    ...mapActions("account", ["login", "logout"]),//,"loginFacebook"

    /* OnFacebookAuthSuccess (AccessToken) {
       console.log("ID Token: " + AccessToken);
        userService.loginFacebook(AccessToken)
            .then(
                user => {
                    commit("loginSuccess", user);
                    router.push("/");
                },
                error => {
                    commit("loginFailure", error);
                    dispatch("alert/error", error, {
                        root: true
                    });
                }
            );
      // Receive the idToken and make your magic with the backend
    //  this.loginFacebook({idToken});
    },
    OnFacebookAuthFail (error) {
      console.log(error)
    },*/

    handleSubmit(e) {
      this.submitted = true;
      const { UserName, Password } = this;
      if (UserName && Password) {
        this.login({ UserName, Password });
      }
    },

  }
};
</script>
<style>
.loading {
  display: block;
  margin: 8px auto;
}

.facebook-signin-button {
  color: white;
  background-color: #3b5998;
  height: 50px;
  font-size: 16px;
  border-radius: 10px;
  padding: 10px 20px 25px 20px;
  box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
}
</style>

