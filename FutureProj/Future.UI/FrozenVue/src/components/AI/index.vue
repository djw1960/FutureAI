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
                    <p><span class="mcate">{{aiitem.Cate}}</span>  <span class="mnprice">{{aiitem.NPrice}}</span> </p>
                    <p class="mstatus">
                        <span class="mstatus">{{aiitem.AValue}} --</span>
                        <span class="mpublish">{{aiitem.Star}}</span>
                        </p>
                    <p class="mdt">{{aiitem.DT|formatDate}}点</p>
                </td>
                <td v-bind:class="{mgreen:aiitem.Status==0,mlose:aiitem.Status==2,mwin:aiitem.Status==1}">
                    <p>{{aiitem.TurnType}}</p>
                    <p>
                        <span class="mstatus">{{aiitem.Status==0?'进行中':aiitem.Status==1?'成功':'失败'}}</span>
                        <span class="mpublish">{{aiitem.IsPublish?aiitem.ReviseLV+'--'+aiitem.ReviseStar:'未发布'}}</span>
                    </p>
                    <p v-bind:class="{mstar:aiitem.ReviseLV<0.9}">
                        <span v-for="star in count(aiitem.Star)" :key="star" class="icon-mstar" ></span>
                    </p>
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
    <v-dialog v-show="showDialog" :dialog-option="dialogOption" ref="dialog"></v-dialog>
    </div>
</template>

<script>
    import {getdataPost} from '@/api/ApiList.js'
    import {formatDate,format} from '@/Common/data.js'
    import mdialog from '@/components/Share/dialog.vue'
    export default {
        components:{
            'v-dialog':mdialog
        },
        data(){
            return{
                userEntity:{},
                menus:[],
                ailist:[],
                showDialog:false,
                dialogOption:{
                    title:'',
                    text:'',
                    cancelButtonText:'',
                    confirmButtonText:''
                }
            }
        },
        filters:{
            formatDate(time){
                let dt=new Date(time);
                return formatDate(dt,'MM.dd hh');
            }
        },
        methods:{
            count(c){
                return c>90?3:c>80?2:1;
            },
            menusfilter(t){
                return this.menus.filter(function (item) {
                    return item.MenuType==t;
                })
            },
            Opt(key,index){
                let item=this.ailist[index];
                switch (key) {
                    case 'add':
                        this.$router.push({name:'aiAdd'});
                        break;
                    case 'publish':
                        
                        break;
                    case 'edit':
                        this.$router.push({name:'aiedit',params:{id:item.ID}});
                        break;
                    case 'detail':
                        this.showDetial(item);
                        break;
                    default:
                        break;
                }
            },
            showDetial(obj){
                this.dialogOption.title="数据信息";
                var x;
                this.dialogOption.text='';
                for (x in obj) {
                   this.dialogOption.text+=format('<label class="ui-label mfield"><span class="mfieldkey">{0}</span>:<span class="mfieldvalue">{1}</span></label>',x,obj[x]);
                }
                this.dialogOption.cancelButtonText="取消";
                this.dialogOption.confirmButtonText="确认";
                this.showDialog = true;
                this.$refs.dialog.confirm().then(() => {
                this.showDialog = false;
                //todo
                console.log("点击关闭");
                }).catch(() => {
                this.showDialog = false;
                //todo
                })
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
</style>