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
        <div class="ui-form-item ui-border-b">
            <label>
                Login
            </label>
            <input type="text" v-model="account"  placeholder="请输入账号">
        </div>
        <div class="ui-form-item ui-border-b">
            <label>
                PWD
            </label>
            <input type="password" v-model="pwd" placeholder="请输入密码">
        </div>
        <div class="ui-btn-wrap">
            <button class="ui-btn-lg ui-btn-primary" v-on:click="Login">登录</button>
        </div>
</div>
</section>

</div>

</template>

<script>
    let Base64=require('js-base64').Base64;
    import {getdataPost} from '@/api/ApiList.js'
    export default {
        components:{
        },
        data(){
            return {
                account:'',
                pwd:'',
            }
        },
        methods:{

            Login(){
                var self=this;
                var params = {
                    no: 2000,
                    account:Base64.encode(this.account.trim()),
                    pwd:Base64.encode(this.pwd.trim()),
                };
                getdataPost(params).then(function (resp) {
                    if(resp.data.code==0)
                    {
                        //login信息存储
                        self.$router.push({ name: 'home',params:{token:resp.data.data.token}});
                    }
                }).catch(function (error) {
                    console.log(error);
                });
            }
        }
    }
</script>

<style scoped>

</style>