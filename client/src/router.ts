import { createRouter, createWebHashHistory } from 'vue-router';
import Home from './views/Home.vue'

let router = createRouter({
    history: createWebHashHistory(),
    routes: [
        {path: '/', component: Home },
        {path: '/explore', component: Home }
    ]

});
export default router;