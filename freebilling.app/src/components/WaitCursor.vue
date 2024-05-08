<script setup>
    defineProps({
        "busy": { required: true, type: Boolean },
        "msg": { type: String,default:"loading... " }
    });
    //const emits = defineEmits(["onHidden"]);
    //function hdie() {
    //    emits.emit("onHidden","hello")
    //}
</script>
<template>
    <div class="progress">
        <div v-if="busy" class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" aria-valuenow="75" aria-valuemin="0" aria-valuemax="100" style="width: 75%">
            {{msg}}
        </div>
    </div>
</template>