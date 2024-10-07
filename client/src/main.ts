// Import Bootstrap CSS
import "bootstrap/dist/css/bootstrap.min.css";

// Import Bootstrap JS (with Popper.js)
import "bootstrap/dist/js/bootstrap.bundle.min.js";
import "bootstrap-icons/font/bootstrap-icons.css";

import "/src/assets/main.scss";

import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";

import '@fortawesome/fontawesome-free/css/all.min.css'; // Import Font Awesome CSS


let app = createApp(App);

app.use(router);

app.mount("#app");
