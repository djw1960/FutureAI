<template>
<div>
    <div class="header">
        
    </div>
<section id="tasklist">
    <table class="ui-table ui-border">
            <tr class="" v-for="n in trcount" :key="n">
                <td v-if="index<3*n && index>=3*(n-1)" v-for="(task,index) in tlist" :key="task.ID" @click="loadinfo(task.Url)">
                    <div>
                        <div class="icon" :class="true?task.MenuIcon:''"></div>
                        <div class="tit">{{task.MenuTitle}}</div>
                        <div class="sub-tit">{{task.MenuCode}}</div>
                    </div>
                </td>
            </tr>
    </table>
</section>
</div>

</template>

<script>
    import {getdataPost} from '@/api/ApiList.js'
    export default {
        data(){
            return {
                trcount:0,
                tlist:[],
                userEntity:{}
            }
        },
        computed:{
            getmenus(){
                
            }
        },
        methods:{
            getTaskList(){
                var self=this;
                var params = {
                    no: 2002,
                    token:self.userEntity.token
                };
                getdataPost(params).then(function (resp) {
                    if(resp.data.code==0){
                        // 存储值：将对象转换为Json字符串
                        sessionStorage.setItem('menulist', JSON.stringify(resp.data.data));
                        for (let index = 0; index < resp.data.data.length; index++) {
                            const element = resp.data.data[index];
                            if (element.Mode=="index"&&element.MenuType=="menu") {
                                self.tlist.push(element);
                            }
                        }
                        var c=self.tlist.length;
                        self.trcount=c%3==0?(c/3):Math.floor(c/3)+1;
                    }else if(resp.data.code==-4){
                        sessionStorage.removeItem('user');
                       self.$router.push({ name: 'login'}); 
                    }else{
                        console.log(resp.data.msg);
                    }
                }).catch(function (error) {
                    console.log(error);
                });
            },
            loadinfo(url){
                console.log(url);
                this.$router.push({ path:url});
            }
        },
        created(){
            // 取值时：把获取到的Json字符串转换回对象
            var userJsonStr = sessionStorage.getItem('user');
            this.userEntity = JSON.parse(userJsonStr);
            this.getTaskList();
        }
    }
</script>

<style scoped>
#tasklist>table td{
     width: 33.33%;
    position: relative;
}

</style>