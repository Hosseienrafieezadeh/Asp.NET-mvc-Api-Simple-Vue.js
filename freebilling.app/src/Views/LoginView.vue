
<template>
    <div class="container mt-5">
        <h3 class="text-center mb-4">Login</h3>
        <form novalidate @submit.prevent="login">
            <div v-if="message" class="alert alert-danger">{{ message }}</div>
            <div class="mb-3">
                <label for="username" class="form-label">Username</label>
                <input v-model="username" type="text" class="form-control" id="username" placeholder="Enter your username">
            </div>
            <div class="mb-3">
                <label for="password" class="form-label">Password</label>
                <input v-model="password" type="password" class="form-control" id="password" placeholder="Enter your password">
            </div>
            <button  type="button" class="btn btn-primary w-100">Login</button>
        </form>
    </div>
</template>

<script setup>
    import axios from "axios";
    import { ref } from "vue";
    import state from "@/state";
    import {useRouter} from "vue-router";;

    const username = ref("");
    const password = ref("");
    const message = ref("");
    const router=useRouter();;

    async function login() {
        event.preventDefault()
        try {
            const result = await axios.post("/api/auth/token", {
                username: username.value,
                password: password.value
            });
            state.token=result.data.token;;

            router.push("/");
            // Assuming the API response contains a token
            message.value = result.data.token;
        } catch (error) {
            // Display error message in case of failure
            message.value = "Wrong username or password";
        }
    }
</script>
