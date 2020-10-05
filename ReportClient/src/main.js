// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from "vue";
// import App from "./App.vue";
import i18n from "./lang/lang";
import store from "./store";
import main1 from "./components/main/main";
import router from "./router";
import BootstrapVue from "bootstrap-vue/dist/bootstrap-vue.esm";
import Notifications from "vue-notification";
import moment from "moment";
import bootbox from "bootbox";
import VueToast from "vue-toast-notification";
import ElementUI from "element-ui";
import VueExcelXlsx from "vue-excel-xlsx";
import VCalendar from "v-calendar";
import "element-ui/lib/theme-chalk/index.css";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import "@fortawesome/fontawesome-free/css/all.css";
import "@fortawesome/fontawesome-free/js/all.js";
import "@fortawesome/fontawesome-free/css/fontawesome.min.css";
import "@fortawesome/fontawesome-free/css/fontawesome.css";
import "pc-bootstrap4-datetimepicker/build/css/bootstrap-datetimepicker.css";
import "moment";
import "jquery";
import "bootstrap";
import VueResource from "vue-resource";
Vue.use(VueResource);
Vue.use(ElementUI, {
  i18n: (key, value) => i18n.t(key, value)
});
Vue.use(bootbox);
Vue.use(BootstrapVue);
Vue.use(Notifications);
Vue.use(VueToast);
Vue.use(VueExcelXlsx);
Vue.prototype.moment = moment;
// Use v-calendar & v-date-picker components // Use <vc-calendar /> instead of <v-calendar />
Vue.use(VCalendar, {
  componentPrefix: "vc",
  screens: {
    tablet: "576px",
    laptop: "992px",
    desktop: "1200px"
  },
  locales: {
    vi: {
      id: "vi",
      firstDayOfWeek: 1,
      masks: {
        L: "YYYY-MM-DD"
      }
    },
    en: {
      id: "en",
      firstDayOfWeek: 1,
      masks: {
        L: "YYYY-MM-DD",
        weekdays: "WW"
      }
    }
  }
});

/* eslint-disable no-new */
const app = new Vue({
  el: "#app",
  store,
  i18n,
  router,
  components: {
    main1
  },
  template: "<main1/>"
});

export default app;
