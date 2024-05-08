import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '@/Views/HomeView.vue';
import BillingView from '@/Views/BillingView.vue';
import LoginView from '@/Views/LoginView.vue';
import state from '@/state';

const routes = [
    {
        path: '/',
        name: 'Home',
        component: HomeView
    },
    {
        path: '/billing',
        name: 'Billing',
        component: BillingView,
        meta: { requiresAuth: true } // Add meta field to indicate authentication is required
    },
    {
        path: '/login',
        name: 'Login',
        component: LoginView
    }
];

const router = createRouter({
    history: createWebHistory('/customersBilling'), // Set base URL here
    routes
});

router.beforeEach((to) => {
    if (to.name !== "Login")
        if (!state.token) {
            return { name:"Login" }
        }
});

export default router;
