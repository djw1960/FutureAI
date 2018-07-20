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
<v-loading v-show="showloading"></v-loading>
</div>

</template>

<script>
    let Base64=require('js-base64').Base64;
    import {getdataPost} from '@/api/ApiList.js';
    import loading from '@/components/Share/loading.vue'
    export default {
        components:{
            'v-loading':loading
        },
        data(){
            return {
                account:'',
                pwd:'',
                showloading:false
            }
        },
        methods:{

            Login(){
                var self=this;
                self.showloading=true;
                var params = {
                    no: 2000,
                    account:Base64.encode(this.account.trim()),
                    pwd:Base64.encode(this.pwd.trim()),
                };
                getdataPost(params).then(function (resp) {
                    self.showloading=false;
                    if(resp.data.code==0)
                    {
                        // 存储值：将对象转换为Json字符串
                        sessionStorage.setItem('user', JSON.stringify(resp.data.data));
                        //login信息存储
                        self.$router.push({ name: 'home'});
                    }
                }).catch(function (error) {
                    console.log(error);
                    self.showloading=false;
                });
            }
        },
        created(){
            // 取值时：把获取到的Json字符串转换回对象
            var userJsonStr = sessionStorage.getItem('user');
            if (userJsonStr) {
                //login信息存储
                this.$router.push({ name: 'home'});
            }
        }
    }
</script>

<style scoped>

</style>