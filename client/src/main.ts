import './assets/main.css'
import 'bootstrap/dist/css/bootstrap.css'
import bootstrap from 'bootstrap/dist/js/bootstrap.js'


import { createApp } from 'vue'
import App from './App.vue'
import { createRouter, createWebHashHistory } from 'vue-router';

let app = createApp(App);

app.mount('#app')

let router = createRouter({
    history: createWebHashHistory(),
    routes: [
        {path: '/domov', component: }
    ]
});


app.use(bootstrap)
