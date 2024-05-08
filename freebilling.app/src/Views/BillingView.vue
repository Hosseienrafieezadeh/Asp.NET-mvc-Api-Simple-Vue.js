<template>
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <h1 class="text-center mb-4">Billing</h1>
                <div v-if="message" class="alert alert-danger">{{ message }}</div>
                <form id="theForm" class="p-4 border rounded-3 bg-light" @submit.prevent="saveBill">
                    <div class="mb-3">
                        <label for="time" class="form-label">Time</label>
                        <input v-model="bill.hoursWorked" type="text" class="form-control" id="time" name="time" placeholder="Enter time">
                    </div>
                    <div class="mb-3">
                        <label for="date" class="form-label">Date</label>
                        <input v-model="bill.date" type="text" class="form-control" id="date" name="date" placeholder="Select date">
                    </div>
                    <div class="mb-3">
                        <label for="workPerformed" class="form-label">Work Performed</label>
                        <textarea v-model="bill.work" class="form-control" rows="4" id="workPerformed" name="workPerformed" placeholder="Enter work performed"></textarea>
                    </div>

                    <div class="mb-3">
                        <label for="client" class="form-label">Client</label>
                        <select v-model="bill.CustomerId" class="form-control form-control-lg" id="customer">
                            <option v-for="c in customers" :value="c.id" :key="c.id">{{ c.companyName }}</option>
                        </select>
                    </div>

                    <div class="mb-3">
                        <label for="rate" class="form-label">Rate</label>
                        <input v-model="bill.rate" type="number" class="form-control" id="rate" name="rate" placeholder="Enter rate">
                    </div>
                    <div class="mb-3">
                        <label for="employee" class="form-label">Employee</label>
                        <select class="form-control form-control-lg" v-model="bill.EmployeeId" id="employee">
                            <option v-for="e in employees" :key="e.id" :value="e.id">{{ e.name }}</option>
                        </select>
                    </div>

                    <div class="text-center mb-3">
                        <button type="submit" class="btn btn-success btn-lg">Save</button>
                    </div>
                    <div class="text-center">
                        <button type="button" class="btn btn-danger btn-lg w-100">Cancel</button>
                    </div>
                </form>
            </div>
        </div>
        <pre>{{ bill }}</pre>
    </div>
</template>

<script setup>
    import { onMounted, ref } from "vue";
    import axios from "axios";
    import state from "@/state";
    import { useRouter } from "vue-router";
    
    const router = useRouter();
    const bill = ref({
        hoursWorked: "",
        work: "",
        CustomerId: null,
        rate: "",
        EmployeeId: null,
        date: "" // Add date property to the bill object
    });
    const employees = ref([]);
    const customers = ref([]);
    const message = ref("");

    onMounted(async () => {
        try {
           
                const [employeeResult, customerResult] = await Promise.all([
                    axios.get("/api/Employees", { headers: { "Authorization": `Bearer ${state.token}` } }),
                     axios.get("/api/Customers", { headers: { "Authorization": `Bearer ${state.token}` } })
                ]);
                employees.value = employeeResult.data;
                customers.value = customerResult.data;
            
        } catch (e) {
            console.error(e);
            message.value = e.message;
        }
    });

    async function saveBill() {
        try {
            const result = await axios.post("/api/timebills", bill.value, {
                headers: {
                    "Authorization": `Bearer ${state.token}`
                }
            });
            router.push("/");
        } catch (e) {
            message.value = e;
        }
    }
</script>

<!-- Add Bootstrap and Bootstrap datepicker CSS files -->
<style>
    @import url('https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css');
    @import url('https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.11.3/css/bootstrap-datepicker.min.css');
</style>

<!-- Add Bootstrap and Bootstrap datepicker JavaScript files -->
<script>
    import 'https://code.jquery.com/jquery-3.6.0.min.js';
    import 'https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js';
    import 'https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.11.3/js/bootstrap-datepicker.min.js';
    import 'https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.11.3/locales/bootstrap-datepicker.en.min.js';
</script>
