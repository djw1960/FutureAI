<template>
    <div>
        <div class="mcatediv">
            <span class="mcate" v-for="cate in catelist" :key="cate.ID">{{cate.CateKey}}:{{cate.CateValue}}</span>
        </div>
        <form @submit.prevent="subform">
        <div class="ui-form-item ui-border-b">
            <label>DT：</label>
            <input type="text" placeholder="2018.07.15 21:00:00" v-model="formObj.DT">
        </div>
        <div class="ui-form-item ui-border-b">
            <label>品种：</label>
            <input type="text" placeholder="螺纹1809" v-model="formObj.Cate">
        </div>
        <div class="ui-form-item ui-border-b">
            <label>编码：</label>
            <input type="text" placeholder="RB" v-model="formObj.CateType">
        </div>
        <div class="ui-form-item ui-border-b">
            <label>当前价：</label>
            <input type="text" placeholder="3889" v-model="formObj.NPrice">
        </div>
        <div class="ui-form-item ui-border-b">
            <label>AValue：</label>
            <input type="text" placeholder="58.00" v-model="formObj.AValue">
        </div>
        <div class="ui-form-item ui-border-b">
            <label>概率：</label>
            <input type="text" placeholder="79.63" v-model="formObj.Star">
        </div>
        <div class="ui-form-item ui-form-item-radio ui-border-b">
            <label class="ui-radio mradio" for="radio">
                <input type="radio" name="radio" v-model="formObj.TurnType" value="up"/>
            </label>
            <p>上涨</p>
            <label class="ui-radio mradio" for="radio">
                <input type="radio" checked="" name="radio" v-model="formObj.TurnType" value="down"/>
            </label>
            <p>下跌</p>
        </div>
        <div class="ui-form-item ui-border-b">
            <label>结果：</label>
            <input type="text" placeholder="0,1-成功,2-失败" v-model="formObj.Status">
        </div>
        <div class="ui-form-item ui-border-b">
            <label>收盘价：</label>
            <input type="text" placeholder="1500.2" v-model="formObj.ResultClose">
        </div>
        <div class="ui-form-item ui-border-b">
            <label>最低价：</label>
            <input type="text" placeholder="1500.2" v-model="formObj.ResultLow">
        </div>
        <div class="ui-form-item ui-border-b">
            <label>最高价：</label>
            <input type="text" placeholder="1500.2" v-model="formObj.ResultHight">
        </div>
        <div class="ui-form-item ui-form-item-switch ui-border-b">
            <p>是否发布：</p>
            <label class="ui-switch">
                <input type="checkbox" checked="" v-model="formObj.IsPublish">
            </label>
        </div>
        <div class="ui-form-item ui-border-b">
            <label>备注：</label>
            <input type="text" placeholder="备注" v-model="formObj.Remark">
        </div>
        <div class="ui-btn-wrap">
            <button  type="submit" class="ui-btn-lg ui-btn-primary">
                确定
            </button>
        </div>
        
    </form>
    </div>
</template>

<script>
    import {getdataPost} from '@/api/ApiList.js'
    import {formatDate} from '@/Common/data.js'
    export default {
        data(){
            return{
                userEntity:{},
                catelist:[],
                id:this.$route.params.id,
                formObj:{},
            }
        },
        filters:{
            formatDate(time){
                let dt=new Date(time);
                return formatDate(dt,'MM.dd hh');
            }
        },
        methods:{
            loadCateList(){
                var self=this;
                var params = {
                    no: 2003,
                    token:self.userEntity.token,
                    cate:'GoodsCate'
                };
                getdataPost(params).then(function (resp) {
                    if(resp.data.code==0){
                        self.catelist=resp.data.data;
                    }
                }).catch(function (error) {
                    console.log(error);
                });
            },
            loadAIInfo(){
                var self=this;
                var params = {
                    no: 2061,
                    token:self.userEntity.token,
                    id:self.id,
                };
                getdataPost(params).then(function (resp) {
                    if(resp.data.code==0){
                        self.formObj=resp.data.data;
                    }
                }).catch(function (error) {
                    console.log(error);
                });
            },
            subform(event){
                var self=this;
                var params = {
                    no: 2062,
                    token:self.userEntity.token,
                    type:'edit',
                    Content:JSON.stringify(self.formObj)
                };
                getdataPost(params).then(function (resp) {
                    if(resp.data.code==0){
                        console.log(resp.data);
                        self.$router.push({name:'ai'});
                    }
                }).catch(function (error) {
                    console.log(error);
                });
            }
        },
        created(){
            // 取值时：把获取到的Json字符串转换回对象
            var userJsonStr = sessionStorage.getItem('user');
            this.userEntity = JSON.parse(userJsonStr);
            this.loadCateList();
            this.loadAIInfo();
        }
    }
</script>

<style scoped>
.mcatediv{
    background-color: white;
}
.mcate{
    background-color: darkslategrey;
    color: white;
    padding: 5px;
    font-size: small;
}
.mradio{
    margin: 5px 5px 5px 30px;
}
.ui-form-item{
    background-color: white;
}
.mgreen{
    color: green;
}
.mlose{
    background-color: darkgray;
    color: white;
}
.mwin{
    background-color: darkorange;
    color: white;
}
</style>