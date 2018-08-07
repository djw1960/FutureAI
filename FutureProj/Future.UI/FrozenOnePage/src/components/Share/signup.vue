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
        <div class="ui-form-item ui-border-b">
            <label>
                代号
            </label>
            <input type="text" v-model="username"  placeholder="姓名或昵称">
        </div>
        <div class="ui-form-item ui-border-b">
            <label>
                邀请码
            </label>
            <input type="text" v-model="invitecode" placeholder="请输入邀请码">
        </div>
        <div class="ui-btn-wrap">
            <button class="ui-btn-lg ui-btn-primary" v-on:click="Login">开启智慧之旅</button>
        </div>
</div>
<div class="ui-grid ui-grid-bisect">
    <ul>
        <li>
            <div class="ui-img">
                <span class="qqinviteCode"></span>
            </div>
            <div class="ui-grid-info ui-border-r">
                <h4>加群获取邀请码</h4>
                <p>QQ:858209118</p>
            </div>
        </li>
        <li>
            <div class="ui-img">
                <a target="_blank" href="//shang.qq.com/wpa/qunwpa?idkey=0afb0478087eb643e92724bcc7ea16b0bbf14e3d910b22436446c694c9eb5b96"><img border="0" src="//pub.idqqimg.com/wpa/images/group.png" alt="期货投资交流" title="期货投资交流"></a>
            </div>
            <div class="ui-grid-info">
                <h4>主要信息</h4>
                <p>辅助信息</p>
            </div>
        </li>
    </ul>
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
                invitecode:'',
                username:'',
                showloading:false
            }
        },
        methods:{

            Login(){
                var self=this;
                self.showloading=true;
                var params = {
                    no: 1003,
                    account:Base64.encode(this.account.trim()),
                    pwd:Base64.encode(this.pwd.trim()),
                    content:this.invitecode,
                    username:this.username,
                };
                getdataPost(params).then(function (resp) {
                    self.showloading=false;
                    if(resp.data.code==0)
                    {
                        //login信息存储
                        self.$router.push({ name: 'login'});
                    }
                }).catch(function (error) {
                    console.log(error);
                    self.showloading=false;
                });
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