<template>
<div>
    <div class="header">
        
    </div>
<section id="tasklist">
    <table class="ui-table ui-border">
            <tr class="" v-for="n in trcount" :key="n">
                <td v-for="task in tlist" :key="task.ID" @click="loadinfo(task.Url)">
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
                token:this.$route.params.token,
                tlist:[]
            }
        },
        methods:{
            getTaskList(){
                var self=this;
                var params = {
                    no: 2002,
                    token:self.token
                };
                getdataPost(params).then(function (resp) {
                    if(resp.data.code==0){
                        self.tlist=resp.data.data;
                        var c=resp.data.data.length;
                        self.trcount=c%3==0?(c/3):(c/3)+1;
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