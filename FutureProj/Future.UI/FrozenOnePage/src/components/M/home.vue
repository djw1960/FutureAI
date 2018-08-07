<template>
<div>
    <div class="mvmodal" v-for="n in 5" :key="n">
        <div class="dheader">
            <div class="info">
                <span v-for="star in 3" :key="star" class="icon-ystar" ></span>
            </div>
        </div>
        <div class="content">
            <div class="citemlist">
                <div class="citem">
                <p><span class="sub"></span><span class="catename">07.20 21 夜盘</span></p>
                <p><span class="catename">螺纹钢1809</span></p>
                <p><span class="sub">收盘：</span><span class="nprice">3389.00</span></p>
                </div>
                <div class="citem">
                <p><span class="catename cbtn" @click="historylist('cate')">历史发布</span></p>
                <p><span class="sub"></span><span class="catename">上涨</span></p>
                <p><span class="sub">概率：</span><span class="nprice">95.5%</span></p>
                </div>
            </div>
            <div class="publishdate">
                <p><span class="pubdate">发布时间：</span><span class="date">2018.07.20 14:45</span></p>
            </div>
        </div>
    </div>
</div>
</template>

<script>
    export default {
        data(){
            return{
                
            }
        },
        methods:{
            historylist(cate){
                console.log(cate);
            }
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