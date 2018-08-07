<template>
<div>
<div class="loginheader">
    <ul class="ui-row" style="width:100%">
    <li class="ui-col ui-col-100">
        <div class="adlogo adsmall"></div>
    </li>
    </ul>
</div>
<section>
<div class="ui-form ui-border-t">
        <div class="ui-form-item ui-border-b">
            <label>
                账号
            </label>
            <input type="text" v-model="account"  placeholder="登录账号">
        </div>
        <div class="ui-form-item ui-border-b">
            <label>
                密码
            </label>
            <input type="password" v-model="pwd" placeholder="登录密码">
        </div>
        <div class="ui-btn-wrap">
            <button class="ui-btn-lg ui-btn-primary" v-on:click="Login">登录</button>
        </div>
</div>
</section>
<div class="ui-tooltips-vip">
    <div class="ui-tooltips-cnt">
        <i class=""></i>
        <span>没有账号？注册试试</span>
        <button class="ui-btn-vip" v-on:click="signup">注册</button>
    </div>
</div>
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
                    no: 1000,
                    account:Base64.encode(this.account.trim()),
                    pwd:Base64.encode(this.pwd.trim())
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
            },
            signup(){
                this.$router.push({ name: 'signup'});
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
.adsmall{
    width: 140px;
    height: 90px;
    margin: 60px auto;
}
</style>