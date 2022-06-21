import Vue from 'vue'
import App from './App'
import {
  router
} from './router'
import {
  store
} from './_store'

import VeeValidate from "vee-validate";
import VueHtmlToPaper from 'vue-html-to-paper';
import Print from 'vue-print-nb';

Vue.use(Print);
const options = {
  name: '_blank',
  specs: [
    'fullscreen=yes',
    'titlebar=yes',
    'scrollbars=yes'
  ],
  styles: [
    'http://localhost:8080/bootstrap.css',
    'https://unpkg.com/kidlat-css/css/kidlat.css'
  ]
}

Vue.use(VueHtmlToPaper, options);
/*import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css'
import 'jquery';*/


Vue.use(require('vue-moment'));


Vue.use(VeeValidate);
//Vue.config.productionTip = false

new Vue({
  el: "#app",
  router,
  store,
  render: h => h(App)
});