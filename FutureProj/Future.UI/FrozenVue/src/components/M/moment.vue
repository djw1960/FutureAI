<template>
    <div>
        <h1>{{userEntity}}</h1>
    </div>
</template>

<script>
    export default {
        data(){
            return{
                userEntity:{}
            }
        },
        created(){
            // 取值时：把获取到的Json字符串转换回对象
            var userJsonStr = sessionStorage.getItem('user');
            this.userEntity = JSON.parse(userJsonStr);
        }
    }
</script>

<style scoped>

</style>