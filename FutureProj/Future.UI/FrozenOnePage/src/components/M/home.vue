<template>
<div>
    <div class="mvmodal" v-for="aiitem in ailist" :key="aiitem.id">
        <div class="dheader">
            <div class="info">
                <span v-for="star in aiitem.Star" :key="star" class="icon-ystar" ></span>
            </div>
        </div>
        <div class="content">
            <div class="citemlist">
                <div class="citem">
                <p><span class="sub"></span><span class="catename">07.20 21 夜盘</span></p>
                <p><span class="catename">{{aiitem.Cate}}</span></p>
                <p><span class="sub">收盘：</span><span class="nprice">{{aiitem.NPrice}}</span></p>
                </div>
                <div class="citem">
                <p><span class="catename cbtn" @click="historylist(aiitem.CateType)">历史发布</span></p>
                <p><span class="sub"></span><span class="catename">{{aiitem.TurnType}}</span></p>
                <p><span class="sub">概率：</span><span class="nprice">95.5%</span></p>
                </div>
            </div>
            <div class="publishdate">
                <p><span class="pubdate">发布时间：</span><span class="date">2018.07.20 14:45</span></p>
            </div>
        </div>
    </div>
    <div class="footer ui-footer">
        <ul class="ui-tiled linklist">
            <li data-href="index.html" class="ui-border-r">资讯</li>
            <li data-href="./c/l.html" class="ui-border-r">仓单</li>
            <li data-href="./t/l.html" class="ui-border-r">统计局</li>
            <li data-href="./f/i.html" class="ui-border-r">圈子</li>
            <li data-href="./a/i.html">AI量化</li>
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
                ailist:[],
            }
        },
        methods:{
            count(c){
                return c>90?3:c>80?2:1;
            },
            historylist(cate){
                
            },
            loadAIList(){
                var self=this;
                self.showloading=true;
                var params = {
                    no: 1060,
                    token:self.userEntity.token,
                    cate:'m',
                    number:1
                    };
                getdataPost(params).then(function (resp) {
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
    background-color: darkgray;
}
.dheader .info{
    margin-top: 10px;
    margin-left: auto;
    margin-right: auto;
    width: 126px;
}
.dheader .info span{
    margin: 5px 5px;
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
</style>