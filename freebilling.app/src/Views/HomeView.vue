<script setup>
    import { computed, onMounted, reactive, ref } from "vue";
    import { formatMoney } from "@/Formatters";
    import axios from "axios";
    
    import WaitCursor from "@/components/WaitCursor.vue";
    import state from "@/state";
    const isBusy = ref(false);
    const name = ref("hossein");
    const onePiece = ref("onePiece");

    // Define reactive data using reactive() function
    const bills = reactive([
        //{
        //    "hours": 2.5,
        //    "employeeId": 2,
        //    "customerId": 1,
        //    "billingRate": 125.00,
        //    "date": "2024-11-11T13:23:44",
        //    "clientRequested": true,
        //    "workPerformed": "I did thing...",
        //    "category": "ddd"
        //},
        //{
        //    "hours": 4,
        //    "employeeId": 2,
        //    "customerId": 1,
        //    "billingRate": 125.00,
        //    "date": "2024-11-11T13:23:44",
        //    "clientRequested": true,
        //    "workPerformed": "I did another thing...",
        //    "category": "ddd"
        //},
        //{
        //    "hours": 7,
        //    "employeeId": 2,
        //    "customerId": 1,
        //    "billingRate": 125.00,
        //    "date": "2024-11-11T13:23:44",
        //    "clientRequested": true,
        //    "workPerformed": "I did wwwwwwwwwww thing...",
        //    "category": "ddd"
        //}
    ]);
    onMounted( async()=>{
        try
        {
        isBusy.value=true;
        const result=await axios("/api/customers/10/timebills");
       setTimeout(()=>isBusy.value=false,1000);
            if (result.status = 200) {
                state.timeBills.splice(0, state.timeBill.length, ...result.data)
            }

       }catch
       {

           console.log("faild")
            }finally
            {
                 setTimeout(()=>isBusy.value=false,1000);

            }

    }
    );
    // Define total computed property
    const total = computed(() => {
        return state.timeBill.map(item => item.hours * item.billingRate).reduce((acc, curr) => acc + curr, 0);
    });

    // Define functions
    function changeMe() {
        name.value += "+";
    }

    function newItem() {
        state.timeBill.push({
            "hours": 7.21,
            "employeeId": 2,
            "customerId": 1,
            "billingRate": 125.00,
            "date": "2024-11-11T13:23:44",
            "clientRequested": true,
            "workPerformed": "salam ",
            "category": "ddd"
        });
    }
</script>

<template>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
    <header>
        <div class="container">
            <h3 class="display-3">Our App</h3>
        </div>
    </header>

    <main>
        <div class="container">
            <h1 class="display-1">Hello from Vue</h1>

            <WaitCursor :busy="isBusy" msg="please wait..." @onHidden="handelHidden"></WaitCursor>

            <!--<div>{{ name.toUpperCase() }}</div>
            <button class="btn btn-primary" type="submit" @click="changeMe">Change me</button>
            <br />
            <img src="" :alt="onePiece" :title="onePiece" />
            <br />
            <button class="btn btn-danger" @click="newItem">New Item</button>-->
            <div>
                Customers:
                <select>
                    <option v-for="c in state.customers" :value="c.id">{{c.companyName}}</option>
                </select>
            </div>
            <table class="table table-striped table-dark">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th>Hours</th>
                        <th>Date</th>
                        <th>Description</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(b, index) in  state.timeBill" :key="index">
                        <td>{{ index + 1 }}</td>
                        <td>{{ b.hours }}</td>
                        <td>{{ b.date }}</td>
                        <td>{{ b.workPerformed }}</td>
                    </tr>
                </tbody>
            </table>
            <!-- Display total using formatMoney function -->
            <p>Total: {{ formatMoney(total) }}</p>
        </div>
    </main>
</template>
