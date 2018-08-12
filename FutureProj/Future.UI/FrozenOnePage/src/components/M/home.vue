<template>
<div>
    <section style="contentb">
        <div class="ui-tab">
            <ul class="ui-tab-nav ui-border-b">
                <li v-bind:class="{current:currendindex==index}" v-for="(item,index) in menus" :key="item.code" @click="menuailist(item.code,index)">{{item.title}}</li>
            </ul>
        </div>
    </section>
    <div class="mvmodal" v-for="aiitem in ailist" :key="aiitem.id">
        <div class="dheader">
            <div class="info">
                <span class="title">{{aiitem.Cate}}</span>
            </div>
            <div class="info">
                <span v-for="star in aiitem.Star" :key="star" class="icon-ystar" ></span>
            </div>
        </div>
        <div class="content">
            <div class="citemlist">
                <div class="citem">
                <p><span class="sub"></span><span class="catename">{{aiitem.DT|formatDate}}</span></p>
                <p><span class="sub">今收:</span><span class="nprice">{{aiitem.NPrice}}</span></p>
                </div>
                <div class="citem">
                <p><span class="catename cbtn" @click="loadAIList(aiitem.CateType)">历史发布</span></p>
                <p><span class="sub">预期:</span><span class="nprice" v-bind:class="{mgreen:aiitem.TurnType=='down',mred:aiitem.TurnType=='up'}">{{aiitem.TurnType|turntitle}}</span></p>
                </div>
            </div>
            <div>
                <p>
                    结果：{{aiitem.Status==2?"失败":aiitem.Status==1?"成功":"进行中"}}
                </p>
            </div>
        </div>
    </div>
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
.mvmodal{
    margin-top: 40px;
}
.mvmodal .dheader{
    margin-left: auto;
    margin-right: auto;
    background-color: teal;
}
.dheader .info{
    margin-top: 10px;
    margin-left: auto;
    margin-right: auto;
    width: 45%;
    display:-webkit-inline-box;
}
.dheader .info .title{
    color: orange;
    font-size: 1.6em;
}
.content{
    background: #f9f9f9;
    font-size: 14px;
    padding: 0.1rem .2rem .1rem .2rem;
    color: cornflowerblue;
}
.content .citemlist{
    clear: both;
    overflow: hidden;
    display: -webkit-box;
    display: -ms-flexbox;
    display: -webkit-flex;
    display: flex;
}
.content .citem{
    padding: .2rem 0;
    width: 0;
    -webkit-box-flex: 1;
    -ms-flex: 1;
    -webkit-flex: 1;
    flex: 1;
    position: relative;
}
.citem .sub{
    font-size: 10px;
}
.citem p{
    margin-bottom: 20px;
}
.content .catename{
    font-size: 18px;
    padding: .1rem 0;
    line-height: .4rem;
}
.citem .cbtn{
    border: 1px solid lightblue;
    padding: 5px;
    border-radius: 3px;
}
.content .nprice{
    font-size: 36px;
    line-height: .4rem;
}
.mgreen{
    color: green;
}
.mred{
    color: red;
}
</style>