<template>
<div>
<div class="loginheader">
    <ul class="ui-row" style="width:100%">
    <li class="ui-col ui-col-100">
        <div class="adlogo"></div>
    </li>
    </ul>
</div>
<section>
<div class="ui-form ui-border-t">
    <form action="">
        <div class="ui-form-item ui-border-b">
            <label>
                Login
            </label>
            <input type="text" placeholder="请输入6位账号">
        </div>
        <div class="ui-form-item ui-border-b">
            <label>
                PWD
            </label>
            <input type="text" placeholder="请输入9位密码">
        </div>
        <div class="ui-btn-wrap">
            <button class="ui-btn-lg ui-btn-primary">登录</button>
        </div>
    </form>
</div>
</section>

</div>

</template>

<script>
    export default {
        components:{
        },
        data(){
            return {
            }
        },
        methods:{

        }
    }
</script>

<style scoped>

</style>