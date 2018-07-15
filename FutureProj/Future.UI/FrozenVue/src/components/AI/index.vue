<template>
    <div>
        <h1>{{userEntity.username}}</h1>
        <div class="ui-btn-wrap">
                <button class="ui-btn ui-btn-primary mmenu" v-for="item in menusfilter('menu')" :key="item.MenuCode" @click="Opt(item.MenuCode)">
                    {{item.MenuTitle}}
                </button>
        </div>
        <table class="ui-table ui-border-tb">
            <tr><th>基本信息</th><th>预测</th><th>操作</th></tr>
            <tr v-for="(aiitem,index) in ailist" :key="aiitem.id">
                <td>
                    <p>{{aiitem.Cate}}  {{aiitem.NPrice}}</p>
                    <p>日期:{{aiitem.DT|formatDate}}点</p>
                </td>
                <td v-bind:class="{mgreen:aiitem.Status==0,mlose:aiitem.Status==2,mwin:aiitem.Status==1}">
                    <p>{{aiitem.TurnType}}</p>
                    <p>
                        <span>{{aiitem.Status==0?'进行中':aiitem.Status==1?'成功':'失败'}}</span>
                        <span>{{aiitem.IsPublish?aiitem.ReviseStar:'未发布'}}</span></p>
                </td>
                <td>
                    <div class="mbtnblock">
                    <button class="ui-btn ui-btn-primary mbtn" v-for="item in menusfilter('btn')" :key="item.MenuCode" @click="Opt(item.MenuCode,index)">
                        {{item.MenuTitle}}
                        </button>
                </div>
                </td>
            </tr>
        </table>
    </div>
</template>

<script>
    import {getdataPost} from '@/api/ApiList.js'
    import {formatDate} from '@/Common/data.js'
    export default {
        data(){
            return{
                userEntity:{},
                menus:[],
                ailist:[],
            }
        },
        filters:{
            formatDate(time){
                let dt=new Date(time);
                return formatDate(dt,'MM.dd hh');
            }
        },
        methods:{
            menusfilter(t){
                return this.menus.filter(function (item) {
                    return item.MenuType==t;
                })
            },
            Opt(key,index){
                let item=this.ailist[index];
                console.log(key);
                switch (key) {
                    case 'add':
                        
                        break;
                    case 'publish':
                        
                        break;
                    case 'edit':
                    case 'detail':
                        this.$router.push({name:'aidetial',params:{id:item.ID}});
                        break;
                    default:
                        break;
                }
            },
            loadAIList(){
                var self=this;
                var params = {
                    no: 2060,
                    token:self.userEntity.token,
                    cate:'m',
                    number:1
                };
                getdataPost(params).then(function (resp) {
                    if(resp.data.code==0){
                        self.ailist=resp.data.data;
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
            var mlist= JSON.parse(sessionStorage.getItem('menulist'));
            for (let index = 0; index < mlist.length; index++) {
                const element = mlist[index];
                if (element.Mode=="ai") {
                    this.menus.push(element);
                }
            }
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
</style>