<template>
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <div class="container">
            <router-link to="/" class="navbar-brand">Home</router-link>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav ms-auto">
                    <li class="nav-item">
                        <router-link to="/billing" class="nav-link" :class="{ active: isActive('/billing') }">Billing</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link v-if="!state.token" to="/login" class="nav-link" :class="{ active: isActive('/billing') }">Billing</router-link>
                    </li>
                    <li class="nav-item">
                     <a href=""onclick="logout"></a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>

    <div class="container mt-3">
        <router-view></router-view>
    </div>
</template>

<script>
import { useRouter } from "vue-router";
import state from "./state";
import router from "./router";

function logout(){

    state.token="";
    router.push("/")


}
    
    export default {
        methods: {
            isActive(route) {
                return this.$route.path === route;
            }
        }
    }
</script>

<style scoped>
    .nav-link.active {
        color: red; /* Change to your desired color */
    }
</style>
