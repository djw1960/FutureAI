<template>
<div>
    <section style="contentb">
        <div class="ui-tab">
            <ul class="ui-tab-nav ui-border-b">
                <li v-bind:class="{current:currendindex==index}" v-for="(item,index) in menus" :key="item.code" @click="menuailist(item.code,index)">{{item.title}}</li>
            </ul>
        </div>
    </section>
    <table class="ui-table ui-border-tb">
        <tr><th>基本信息</th><th>预测</th><th>概率</th></tr>
        <tr v-for="(aiitem,index) in ailist" :key="aiitem.id">
            <td>
                <p><span class="mcate" @click="loadAIList(aiitem.CateType)">{{aiitem.Cate}}</span>  <span class="mnprice">{{aiitem.NPrice}}</span> </p>
                <p class="mstatus">
                    <span class="mstatus">  </span>
                    </p>
                <p class="mdt">{{aiitem.DT|formatDate}}点</p>
            </td>
            <td v-bind:class="{mgreen:aiitem.Status==0,mlose:aiitem.Status==2,mwin:aiitem.Status==1}">
                <p>{{aiitem.TurnType}}</p>
                <p>
                    <span class="mstatus">{{aiitem.Status==0?'进行中':aiitem.Status==1?'成功':'失败'}}</span>
                </p>
            </td>
            <td>
                <p>
                    <span v-for="star in aiitem.Star" :key="star" class="icon-mstar" ></span>
                </p>
            </td>
        </tr>
    </table>
    <div class="footer ui-footer">
        <ul class="ui-tiled linklist">
            <li data-href="./a/i.html">热点</li>
            <li data-href="./a/i.html">AI量化</li>
            <li data-href="./f/i.html" class="ui-border-r">圈子</li>
        </ul>
    </div>
    <v-loading v-show="showloading"></v-loading>
</div>
</template>

<script>
    import {getdataPost} from '@/api/ApiList.js'
    import {formatDate,format} from '@/Common/data.js'
    import loading from '@/components/Share/loading.vue'
    export default {
        components:{
            'v-loading':loading
        },
        data(){
            return{
                currendindex:0,
                userEntity:{},
                ailist:[],
                menus:[{title:"最新发布",code:"n"},{title:"历史战绩",code:"m"},{title:"AI技能",code:"d"}],
                params:{
                    no: 1060,
                    token:'',
                    cate:'n',
                    code:'',
                    number:1
                    }
            }
        },
        filters:{
            formatDate(time){
                let dt=new Date(time);
                return formatDate(dt,'MM.dd hh');
            },
            turntitle(c){
                return c=="up"?"上涨":c=="down"?"下跌":"";
            },
        },
        methods:{
            count(c){
                return c>90?3:c>80?2:1;
            },
            menuailist(cate,index){
                this.currendindex=index;
                switch (cate) {
                    case "n":
                    case "m":
                        this.params.cate=cate;
                        this.loadAIList();
                        break;
                    case "d":
                        break;
                    default:
                        break;
                }
            },
            loadAIList(code){
                var self=this;
                self.showloading=true;
                if(code&&code.trim().length>0){
                    self.params.code=code;
                    self.params.number=6;
                }
                getdataPost(self.params).then(function (resp) {
                    self.showloading=false;
                    if(resp.data.code==0){
                        self.ailist=resp.data.data;
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
            this.userEntity = JSON.parse(userJsonStr);
            this.params.token=this.userEntity.token;
            this.loadAIList();
        }
    }
</script>

<style scoped>
.mli{
    line-height: 60px;
}
.mmenu{
    margin-left: 20px;
}
.mbtnblock{
    margin-left: 5px;
}
.mbtn{
    position: relative;
    margin:5px;
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
.mcate{
    font-size: 0.1rem;
    color: darkblue;
}
.mnprice{
    font-size: 0.16rem;
    color: deeppink;
}
.mdt{
    font-size: 0.1rem;
    color: darkgreen;
}
.mpublish{
    font-size: 0.1rem;
}
.mstatus{
    font-size: 0.1rem;
}
.mstar{
    background-color: black;
}
.mabandon{
    background-color:bisque;
}
</style>