import { createRouter, createWebHashHistory } from 'vue-router';
import Home from './views/Home.vue'
import Explore from './views/Explore.vue';
import Tips from './views/Tips.vue';
import BlogPosts from './views/BlogPosts.vue';
import RecipeReadOnlyView from './views/RecipeReadOnlyView.vue';
import BlogReadOnlyView from './views/BlogReadOnlyView.vue';

let router = createRouter({
    history: createWebHashHistory(),
    routes: [
        {path: '/', component: Home },
        {path: '/explore', component: Explore },
        {path: '/tips', component: Tips },
        {path: '/blog', component: BlogPosts },
        {path: '/recipe/:id', component: RecipeReadOnlyView, props: true},
        {path: '/blog/:id', component: BlogReadOnlyView, props: true},

    ]

});
export default router;